using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FallenTally.Controller;
using FallenTally.Services;
using FallenTallyAvalon.Helper;
using FallenTallyAvalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private const string CONNECTTEXT = "Connect";
        private const string CONNECTEDTEXT = "Connected";
        private List<HotkeyHelper> _hotkeyHelpers;
        private TwitchAuthController _twitchAuthController;
        private HotkeyController _hotkeyController;
        public static string HotkeyFileName = "hotkeys.json";
        private const string _twitchFileName = "twitch.json";
        private TwitchInfoModel _twitchInfoModel;

        [ObservableProperty]
        private string twitchChannelName;

        [ObservableProperty]
        private string twitchConnectButtonText;



        public SettingsViewModel(TwitchAuthController twitchAuthController, HotkeyController hotkeyController)
        {
            _hotkeyHelpers = JsonSettingsService.Load<List<HotkeyHelper>>(HotkeyFileName) ?? new List<HotkeyHelper>();
            _twitchAuthController = twitchAuthController;
            _hotkeyController = hotkeyController;
            TwitchConnectButtonText = CONNECTTEXT;
            LoadTwitchModel();
        }

        private void LoadTwitchModel()
        {
            _twitchInfoModel = JsonSettingsService.Load<TwitchInfoModel>(_twitchFileName) ?? new();
            TwitchChannelName = _twitchInfoModel.TwitchChannelName;
            if (_twitchInfoModel.AccessToken != "")
            {
                ValidateTwitchAccess();
            }
            JsonSettingsService.Save(_twitchInfoModel, _twitchFileName);

        }

        private async Task ValidateTwitchAccess()
        {
            TwitchTokenValidationResultModel? tokenValidationResultModel =await TwitchAuthController.ValidateAccessTokenAsync(_twitchInfoModel.AccessToken);
            if (tokenValidationResultModel == null || tokenValidationResultModel.status == 401)
            {
                TwitchConnectButtonText = CONNECTTEXT;
            }
            else
            {
                TwitchConnectButtonText = CONNECTEDTEXT;
            }
        }



        [RelayCommand]
        public async Task ConnectToTwitch()
        {
            _twitchAuthController.SetOnAccessTokenreceived(OnAccessTokenReceived);
            await _twitchAuthController.StartImplicitGrantFlowAsync();
        }

        private void OnAccessTokenReceived(string accessToken)
        {
            _twitchInfoModel.AccessToken = accessToken;
            ValidateTwitchAccess();
            JsonSettingsService.Save(_twitchInfoModel, _twitchFileName);
        }

        partial void OnTwitchChannelNameChanged(string? oldValue, string newValue)
        {
            _twitchInfoModel.TwitchChannelName = newValue;
            JsonSettingsService.Save(_twitchInfoModel, _twitchFileName);
        }

        public void AddHotkey(HotkeyHelper hotkeyHelper)
        {
            var nameField = typeof(HotkeyHelper).GetField("_name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var newName = nameField?.GetValue(hotkeyHelper) as string;

            bool existing = _hotkeyHelpers.Where(x => x.Name.Equals(hotkeyHelper.Name)).Count() > 0;

            if (existing)
            {
                _hotkeyHelpers.Remove(_hotkeyHelpers.Where(x => x.Name.Equals(hotkeyHelper.Name)).FirstOrDefault()!);
            }
            _hotkeyHelpers.Add(hotkeyHelper);
            JsonSettingsService.Save(_hotkeyHelpers, HotkeyFileName);
            _hotkeyController.ReloadKeysFromOptions();
        }

    }
}
