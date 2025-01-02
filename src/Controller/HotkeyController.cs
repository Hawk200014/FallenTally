using DeathCounterHotkey.Controller.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class HotkeyController
    {
        private OptionsController _optionsController;
        private MainController _mainController;
        private Action _increaseAction;
        private Action _decreaseAction;
        private Action _switchAction;
        private Action _quickAction;
        private Action _finishAction;
        private KeyboardHook _hotkeysHook;
        EventHandler<KeyPressedEventArgs> keyPressedEventHandler;
        private string _increaseHKStr = "";
        private string _decreaseHKStr = "";
        private string _switchHKStr = "";
        private string _quickAddHKStr = "";
        private string _finishHKStr = "";

        public HotkeyController(OptionsController optionsController, MainController mainController,
            Action increaseAction,
            Action decreaseAction,
            Action switchAction,
            Action quickAction,
            Action finishAction)
        {
            this._optionsController = optionsController;
            this._mainController = mainController;
            this._increaseAction = increaseAction;
            this._decreaseAction = decreaseAction;
            this._switchAction = switchAction;
            this._quickAction = quickAction;
            this._finishAction = finishAction;

            keyPressedEventHandler = new EventHandler<KeyPressedEventArgs>(HotkeyPressedEvent);
            _hotkeysHook = new KeyboardHook();
            _hotkeysHook.KeyPressed += keyPressedEventHandler;

            ReloadKeysFromOptions();

        }

        public void ReloadKeysFromOptions()
        {
            _increaseHKStr = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.INCREASE_HOTKEY));
            _decreaseHKStr = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.DECREASE_HOTKEY));
            _switchHKStr = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.SWITCH_LOCATION_HOTKEY));
            _quickAddHKStr = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.QUICKADD_LOCATION_HOTKEY));
            _finishHKStr = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.FINISH_LOCATION_HOTKEY));
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
        }



        internal void UnregisterHotkeys()
        {
            _hotkeysHook.KeyPressed -= keyPressedEventHandler;
            _hotkeysHook.Dispose();
            _hotkeysHook = new KeyboardHook();
            _hotkeysHook.KeyPressed += keyPressedEventHandler;
        }
    }
}
