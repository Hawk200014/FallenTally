using Avalonia.Interactivity;
using FallenTallyAvalon.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FallenTally.Controller
{
    /// <summary>
    /// Handles Twitch OAuth2 Implicit Grant flow for desktop applications, including a local web server to receive the redirect.
    /// </summary>
    public class TwitchAuthController
    {
        // Replace with your actual Twitch client ID
        public static string ClientId = "y62p20ic4fuqq0ojr115ynf5nqd6e2";
        private const string Scope = "channel:manage:broadcast";
        private HttpListener? _httpListener;
        private CancellationTokenSource? _cts;
        private int _listeningPort = 3000;
        private string? _redirectUri;
        private Action<string>? _onAccessTokenReceived;


        public void SetOnAccessTokenreceived(Action<string> onAccessTokenReceived)
        {
            _onAccessTokenReceived = onAccessTokenReceived ?? throw new ArgumentNullException(nameof(onAccessTokenReceived));
        }

        /// <summary>
        /// Starts the Twitch OAuth2 Implicit Grant flow by opening the authorization URL in the browser and starting a local web server.
        /// </summary>
        public async Task StartImplicitGrantFlowAsync()
        {
            //_listeningPort = GetRandomUnusedPort();
            _redirectUri = $"http://localhost:{_listeningPort}/";
            string state = GenerateRandomState(32);

            string authorizationUrl =
                "https://id.twitch.tv/oauth2/authorize" +
                "?response_type=token" +
                $"&client_id={Uri.EscapeDataString(ClientId)}" +
                $"&redirect_uri={Uri.EscapeDataString(_redirectUri)}" +
                $"&scope={Uri.EscapeDataString(Scope)}" +
                $"&state={Uri.EscapeDataString(state)}" +
                "&force_verify=true";

            // Start local web server to listen for the redirect
            _cts = new CancellationTokenSource();
            Task listenTask = ListenForRedirectAsync(_cts.Token);

            // Open the authorization URL in the user's default browser
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = authorizationUrl,
                UseShellExecute = true
            });

            await listenTask;
        }

        /// <summary>
        /// Listens for the OAuth2 redirect and extracts the access token from the fragment.
        /// </summary>
        private async Task ListenForRedirectAsync(CancellationToken cancellationToken)
        {
            if (_redirectUri == null)
                throw new InvalidOperationException("Redirect URI not set.");

            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(_redirectUri);
            _httpListener.Start();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var context = await _httpListener.GetContextAsync();

                    // The access token is in the URL fragment, which is not sent to the server.
                    // Instead, serve a page with JavaScript to extract the fragment and POST it to the server.
                    string html = @"
<!DOCTYPE html>
<html>
<head>
    <title>Twitch Auth</title>
    <script>
        window.onload = function() {
            if (window.location.hash) {
                var xhr = new XMLHttpRequest();
                xhr.open('POST', '/', true);
                xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                xhr.send('fragment=' + encodeURIComponent(window.location.hash));
                document.body.innerHTML = 'You may now close this window.';
            }
        }
    </script>
</head>
<body>
    Authenticating with Twitch...
</body>
</html>";

                    if (context.Request.HttpMethod == "GET")
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(html);
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.ContentType = "text/html";
                        await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else if (context.Request.HttpMethod == "POST")
                    {
                        using var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);
                        string body = await reader.ReadToEndAsync();
                        string? fragment = null;
                        if (body.StartsWith("fragment="))
                        {
                            fragment = Uri.UnescapeDataString(body.Substring("fragment=".Length));
                        }

                        // Extract access_token from fragment
                        string? accessToken = ExtractAccessTokenFromFragment(fragment);

                        // Respond to the browser
                        string response = "Authentication complete. You may close this window.";
                        byte[] buffer = Encoding.UTF8.GetBytes(response);
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.ContentType = "text/plain";
                        await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();

                        // Stop the listener after receiving the token
                        _httpListener.Stop();
                        _cts?.Cancel();

                        // Use the access token as needed
                        OnAccessTokenReceived(accessToken);
                    }
                }
            }
            catch (HttpListenerException)
            {
                // Listener stopped
            }
            finally
            {
                _httpListener.Close();
            }
        }

        /// <summary>
        /// Gets a random unused local TCP port.
        /// </summary>
        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        /// <summary>
        /// Called when the access token is received.
        /// </summary>
        private void OnAccessTokenReceived(string? accessToken)
        {
            _onAccessTokenReceived?.Invoke(accessToken ?? "");
        }

        /// <summary>
        /// Extracts the access_token from the URL fragment.
        /// </summary>
        private static string? ExtractAccessTokenFromFragment(string? fragment)
        {
            if (string.IsNullOrEmpty(fragment) || !fragment.StartsWith("#"))
                return null;

            var parts = fragment.Substring(1).Split('&');
            foreach (var part in parts)
            {
                var kv = part.Split('=');
                if (kv.Length == 2 && kv[0] == "access_token")
                    return kv[1];
            }
            return null;
        }

        /// <summary>
        /// Generates a random state string for CSRF protection.
        /// </summary>
        private static string GenerateRandomState(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlEncode(bytes);
        }

        /// <summary>
        /// Encodes bytes to a base64url string (no padding).
        /// </summary>
        private static string Base64UrlEncode(byte[] bytes)
        {
            string base64 = Convert.ToBase64String(bytes);
            return base64.Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        /// <summary>
        /// Validates a Twitch OAuth access token using the /validate endpoint.
        /// </summary>
        /// <param name="accessToken">The access token to validate.</param>
        /// <returns>
        /// A TwitchTokenValidationResult object if valid, or null if invalid or on error.
        /// </returns>
        public static async Task<TwitchTokenValidationResultModel?> ValidateAccessTokenAsync(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException("Access token must not be null or empty.", nameof(accessToken));

            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://id.twitch.tv/oauth2/validate");
            request.Headers.Add("Authorization", $"OAuth {accessToken}");

            try
            {
                var response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Token is valid
                    var result = JsonSerializer.Deserialize<TwitchTokenValidationResultModel>(content);
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // Token is invalid
                    var result = JsonSerializer.Deserialize<TwitchTokenValidationResultModel>(content);
                    return result;
                }
                else
                {
                    // Unexpected error
                    return null;
                }
            }
            catch
            {
                // Network or deserialization error
                return null;
            }
        }
    }
}
