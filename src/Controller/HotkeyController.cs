using DeathCounterHotkey.Controller.Forms;
using FallenTally.Enums;
using FallenTally.Utility.Singletons;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class HotkeyController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();
        private OptionsController? _optionsController;
        //private MainController _mainController;
        private Action _increaseAction;
        private Action _decreaseAction;
        private Action _switchAction;
        private Action _quickAction;
        private Action _finishAction;
        private Action _recordingStartAction;
        private Action _markerNormalAction;
        private Action _markerFunnyAction;
        private Action _markerGameAction;
        private Action _markerTalkAction;
        private Action _markerPlayAction;
        private KeyboardHook _hotkeysHook;
        EventHandler<KeyPressedEventArgs> keyPressedEventHandler;
        private string _increaseHKStr = "";
        private string _decreaseHKStr = "";
        private string _switchHKStr = "";
        private string _quickAddHKStr = "";
        private string _finishHKStr = "";
        private string _recordingStartHKStr = "";
        private string _markerNormalHKStr = "";
        private string _markerFunnyHKStr = "";
        private string _markerGameHKStr = "";
        private string _markerTalkHKStr = "";
        private string _markerPauseHKStr = "";

        public HotkeyController(
            Action increaseAction,
            Action decreaseAction,
            Action switchAction,
            Action quickAction,
            Action finishAction, 
            Action recordingAction, 
            Action markerNormal,
            Action markerfunny,
            Action markerGame,
            Action markerTalk,
            Action markerPause
            )
        {
            this._optionsController = _singleton.GetValue(OptionsController.GetSingletonName()) as OptionsController;
            this._increaseAction = increaseAction;
            this._decreaseAction = decreaseAction;
            this._switchAction = switchAction;
            this._quickAction = quickAction;
            this._finishAction = finishAction;
            this._recordingStartAction = recordingAction;
            this._markerNormalAction = markerNormal;
            this._markerFunnyAction = markerfunny;
            this._markerGameAction = markerGame;
            this._markerTalkAction = markerTalk;
            this._markerPlayAction = markerPause;


            keyPressedEventHandler = new EventHandler<KeyPressedEventArgs>(HotkeyPressedEvent);
            _hotkeysHook = new KeyboardHook();
            _hotkeysHook.KeyPressed += keyPressedEventHandler;

            ReloadKeysFromOptions();

        }

        public void ReloadKeysFromOptions()
        {
            _increaseHKStr = _optionsController.GetSetting(nameof(OPTIONS.INCREASE_HOTKEY));
            _decreaseHKStr = _optionsController.GetSetting(nameof(OPTIONS.DECREASE_HOTKEY));
            _switchHKStr = _optionsController.GetSetting(nameof(OPTIONS.SWITCH_LOCATION_HOTKEY));
            _quickAddHKStr = _optionsController.GetSetting(nameof(OPTIONS.QUICKADD_LOCATION_HOTKEY));
            _finishHKStr = _optionsController.GetSetting(nameof(OPTIONS.FINISH_LOCATION_HOTKEY));
            _recordingStartHKStr = _optionsController.GetSetting(nameof(OPTIONS.START_RECORDING_TIMER));
            _markerNormalHKStr = _optionsController.GetSetting(nameof(OPTIONS.MARKER_NORMAL_HOTKEY));
            _markerFunnyHKStr = _optionsController.GetSetting(nameof(OPTIONS.MARKER_FUNNY_HOTKEY));
            _markerGameHKStr = _optionsController.GetSetting(nameof(OPTIONS.MARKER_GAMING_HOTKEY));
            _markerTalkHKStr = _optionsController.GetSetting(nameof(OPTIONS.MARKER_TALK_HOTKEY));
            _markerPauseHKStr = _optionsController.GetSetting(nameof(OPTIONS.MARKER_PAUSE_HOTKEY));
        }

        public void LoadHotkeys()
        {
            if (!string.IsNullOrEmpty(_increaseHKStr))
            {
                Enum.TryParse(_increaseHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_decreaseHKStr))
            {
                Enum.TryParse(_decreaseHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_switchHKStr))
            {
                Enum.TryParse(_switchHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_quickAddHKStr))
            {
                Enum.TryParse(_quickAddHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_quickAddHKStr))
            {
                Enum.TryParse(_finishHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_recordingStartHKStr))
            {
                Enum.TryParse(_recordingStartHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_markerNormalHKStr))
            {
                Enum.TryParse(_markerNormalHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_markerFunnyHKStr))
            {
                Enum.TryParse(_markerFunnyHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_markerGameHKStr))
            {
                Enum.TryParse(_markerGameHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_markerTalkHKStr))
            {
                Enum.TryParse(_markerTalkHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }

            if (!string.IsNullOrEmpty(_markerPauseHKStr))
            {
                Enum.TryParse(_markerPauseHKStr, out Keys hotkey);
                _hotkeysHook.RegisterHotKey(hotkey);
            }
        }

        private void HotkeyPressedEvent(object? sender, KeyPressedEventArgs e)
        {
            string keyname = e.Key.ToString();

            if (_increaseHKStr.Equals(keyname))
            {
                _increaseAction.Invoke();
            }
            else if (_decreaseHKStr.Equals(keyname))
            {
                _decreaseAction.Invoke();
            }
            else if (_switchHKStr.Equals(keyname))
            {
                _switchAction.Invoke();
            }
            else if (_quickAddHKStr.Equals(keyname))
            {
                _quickAction.Invoke();
            }
            else if (_finishHKStr.Equals(keyname))
            {
                _finishAction.Invoke();
            }
            else if (_recordingStartHKStr.Equals(keyname))
            {
                _recordingStartAction.Invoke();
            }
            else if (_markerNormalHKStr.Equals(keyname))
            {
                _markerNormalAction.Invoke();
            }
            else if (_markerFunnyHKStr.Equals(keyname))
            {
                _markerFunnyAction.Invoke();
            }
            else if (_markerGameHKStr.Equals(keyname))
            {
                _markerGameAction.Invoke();
            }
            else if (_markerTalkHKStr.Equals(keyname))
            {
                _markerTalkAction.Invoke();
            }
            else if (_markerPauseHKStr.Equals(keyname))
            {
                _markerPlayAction.Invoke();
            }
        }



        internal void UnregisterHotkeys()
        {
            _hotkeysHook.KeyPressed -= keyPressedEventHandler;
            _hotkeysHook.Dispose();
            _hotkeysHook = new KeyboardHook();
            _hotkeysHook.KeyPressed += keyPressedEventHandler;
        }

        public static string GetSingletonName()
        {
            return "HotkeyController";
        }
    }
}
