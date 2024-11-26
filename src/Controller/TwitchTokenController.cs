using DeathCounterHotkey.Controller.Forms;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using TwitchLib.Api;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration.Json;

namespace DeathCounterHotkey.Controller
{
    public class TwitchTokenController
    {
        private static readonly HttpClient client = new HttpClient();
        private byte[] s_additionalEntropy = { 9, 1, 7, 8, 5 };
        private OptionsController _optionsController;

        public TwitchTokenController(OptionsController optionsController)
        {
            _optionsController = optionsController;
        }

        public string EncryptStr(string decrypted)
        {
            byte[] data = Encoding.UTF8.GetBytes(decrypted);
            byte[] encrypted = ProtectedData.Protect(data, s_additionalEntropy, DataProtectionScope.CurrentUser);
            //return System.Text.Encoding.UTF8.GetString(encrypted);
            return System.Convert.ToBase64String(encrypted);
        }

        public string DecryptStr(string encrypted)
        {
            byte[] encryptedbyte = System.Convert.FromBase64String(encrypted);
            //byte[] encryptedbyte = Encoding.UTF8.GetBytes(encrypted);
            byte[] data = ProtectedData.Unprotect(encryptedbyte, s_additionalEntropy, DataProtectionScope.CurrentUser);
            return System.Text.Encoding.UTF8.GetString(data);
        }


        public double GetStreamTime()
        {
            string name = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.TWITCH_NAME));
            if (string.IsNullOrEmpty(name)) return -1;
            (string, string) twitchAppCred = GetCurrentTwitchAppId();
            string clientID = twitchAppCred.Item1;
            string clientSecret = twitchAppCred.Item2;

            var api = new TwitchAPI();

            api.Settings.ClientId = clientID;
            api.Settings.Secret = clientSecret;

            var stream = api.Helix.Streams.GetStreamsAsync(userLogins: new List<string> { name }, first: 1, type: "live").Result;
            if (stream.Streams.Length > 0)
            {
                DateTime start = stream.Streams[0].StartedAt;
                double seconsLive = (  DateTime.UtcNow - start).TotalSeconds;
                return seconsLive;
            }
            return -2;
        }


        private (string, string) GetCurrentTwitchAppId()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string clientId = configuration.GetSection("Twitch").GetSection("AppId").Value ?? "";
            string clientSecret = configuration.GetSection("Twitch").GetSection("AppSecret").Value ?? "";
            int encrypted = int.Parse(configuration.GetSection("Twitch").GetSection("Encrypted").Value ?? "0");

            if (encrypted == 0)
            {
                string encClientId = EncryptStr(clientId);
                string encClientSecret = EncryptStr(clientSecret);

                string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                jsonObj["Twitch"]["AppSecret"] = encClientSecret;
                jsonObj["Twitch"]["AppId"] = encClientId;
                jsonObj["Twitch"]["Encrypted"] = "1";
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), output);
                


            }
            if (encrypted == 1)
            {
                clientId = DecryptStr(clientId);
                clientSecret = DecryptStr(clientSecret);
            }
            return (clientId, clientSecret);
        }




    }
}
