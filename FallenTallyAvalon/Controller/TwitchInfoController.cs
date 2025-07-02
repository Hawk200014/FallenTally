using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FallenTallyAvalon.Models;
using FallenTally.Services;

namespace FallenTally.Controller
{
    public class TwitchInfoController
    {
        /// <summary>
        /// Gets the current live stream's uptime in seconds for the configured Twitch channel.
        /// Returns a negative value if the stream is not live or an error occurs.
        /// </summary>
        /// <param name="accessToken">The OAuth access token for the Twitch API.</param>
        /// <returns>Live time in seconds, or a negative value on error/not live.</returns>
        public static async Task<int> GetCurrentLiveTimeSecondsAsync()
        {
            string? twitchAccessToken = LoadAccessToken();
            if (string.IsNullOrWhiteSpace(twitchAccessToken))
                return -1;
            var result = await TwitchAuthController.ValidateAccessTokenAsync(twitchAccessToken);
            if (result == null || result.status == 401)
                return -2;


            string? channelName = LoadChannelNameFromSettings();
            if (string.IsNullOrWhiteSpace(channelName))
                return -3;

            var streamJson = await GetStreamInfoJsonAsync(twitchAccessToken, channelName);
            if (streamJson == null)
                return -4;

            var startedAt = ExtractStartedAtFromStreamJson(streamJson);
            if (startedAt == null)
                return -5;

            return CalculateUptimeSeconds(startedAt.Value);
        }

        private static string? LoadAccessToken()
        {
            var twitchInfo = JsonSettingsService.Load<TwitchInfoModel>("twitch.json");
            return twitchInfo?.AccessToken;
        }

        private static string? LoadChannelNameFromSettings()
        {
            var twitchInfo = JsonSettingsService.Load<TwitchInfoModel>("twitch.json");
            return twitchInfo?.TwitchChannelName;
        }

        private static async Task<JsonElement?> GetStreamInfoJsonAsync(string accessToken, string channelName)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            httpClient.DefaultRequestHeaders.Add("Client-Id", TwitchAuthController.ClientId);

            string url = $"https://api.twitch.tv/helix/streams?user_login={Uri.EscapeDataString(channelName)}";

            try
            {
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var data = doc.RootElement.GetProperty("data");
                if (data.GetArrayLength() == 0)
                    return null;

                return data[0];
            }
            catch
            {
                return null;
            }
        }

        private static DateTime? ExtractStartedAtFromStreamJson(JsonElement? stream)
        {
            if (stream == null || !stream.Value.TryGetProperty("started_at", out var startedAtElement))
                return null;

            var startedAtStr = startedAtElement.GetString();
            if (string.IsNullOrWhiteSpace(startedAtStr))
                return null;

            if (!DateTime.TryParse(startedAtStr, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out var startedAt))
                return null;

            return startedAt;
        }

        private static int CalculateUptimeSeconds(DateTime startedAt)
        {
            var now = DateTime.UtcNow;
            var uptime = now - startedAt;
            return (int)uptime.TotalSeconds;
        }
    }
}
