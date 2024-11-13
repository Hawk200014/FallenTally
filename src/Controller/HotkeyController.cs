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
        private Action<object, KeyPressedEventArgs> _increaseAction;
        private Action<object, KeyPressedEventArgs> _decreaseAction;
        private Action<object, KeyPressedEventArgs> _switchAction;
        private Action<object, KeyPressedEventArgs> _quickAction;
        private List<KeyboardHook> _hotkeysHook = new List<KeyboardHook>();
        private List<EventHandler<KeyPressedEventArgs>> _keyboardEventHandler = new List<EventHandler<KeyPressedEventArgs>>();

        public HotkeyController(OptionsController optionsController, MainController mainController,
            Action<object, KeyPressedEventArgs> increaseAction,
            Action<object, KeyPressedEventArgs> decreaseAction,
            Action<object, KeyPressedEventArgs> switchAction,
            Action<object, KeyPressedEventArgs> quickAction)
        {
            this._optionsController = optionsController;
            this._mainController = mainController;
            this._increaseAction = increaseAction;
            this._decreaseAction = decreaseAction;
            this._switchAction = switchAction;
            this._quickAction = quickAction;
        }

        public void LoadHotkeys()
        {
            string setting = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.INCREASE_HOTKEY));
            if (!string.IsNullOrEmpty(setting))
            {
                Enum.TryParse(setting, out Keys hotkey);
                KeyboardHook hook = new KeyboardHook();
                EventHandler<KeyPressedEventArgs> keyPressedEventHandler = new EventHandler<KeyPressedEventArgs>(_increaseAction);
                hook.KeyPressed += keyPressedEventHandler;
                hook.RegisterHotKey(hotkey);
                _hotkeysHook.Add(new KeyboardHook());
                _keyboardEventHandler.Add(keyPressedEventHandler);
            }

            setting = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.DECREASE_HOTKEY));
            if (!string.IsNullOrEmpty(setting))
            {
                Enum.TryParse(setting, out Keys hotkey);
                KeyboardHook hook = new KeyboardHook();
                EventHandler<KeyPressedEventArgs> keyPressedEventHandler = new EventHandler<KeyPressedEventArgs>(_decreaseAction);
                hook.KeyPressed += keyPressedEventHandler;
                hook.RegisterHotKey(hotkey);
                _hotkeysHook.Add(new KeyboardHook());
                _keyboardEventHandler.Add(keyPressedEventHandler);
            }

            setting = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.SWITCH_LOCATION_HOTKEY));
            if (!string.IsNullOrEmpty(setting))
            {
                Enum.TryParse(setting, out Keys hotkey);
                KeyboardHook hook = new KeyboardHook();
                EventHandler<KeyPressedEventArgs> keyPressedEventHandler = new EventHandler<KeyPressedEventArgs>(_switchAction);
                hook.KeyPressed += keyPressedEventHandler;
                hook.RegisterHotKey(hotkey);
                _hotkeysHook.Add(new KeyboardHook());
                _keyboardEventHandler.Add(keyPressedEventHandler);
            }

            setting = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.QUICKADD_LOCATION_HOTKEY));
            if (!string.IsNullOrEmpty(setting))
            {
                Enum.TryParse(setting, out Keys hotkey);
                KeyboardHook hook = new KeyboardHook();
                EventHandler<KeyPressedEventArgs> keyPressedEventHandler = new EventHandler<KeyPressedEventArgs>(_quickAction);
                hook.KeyPressed += keyPressedEventHandler;
                hook.RegisterHotKey(hotkey);
                _hotkeysHook.Add(new KeyboardHook());
                _keyboardEventHandler.Add(keyPressedEventHandler);
            }

        }



        internal void UnregisterHotkeys()
        {
            for (int i = 0; i < _hotkeysHook.Count; i++)
            {
                _hotkeysHook.ElementAt(i).KeyPressed -= _keyboardEventHandler.ElementAt(i);
                _hotkeysHook.ElementAt(i).Dispose();

            }

            _hotkeysHook.Clear();
            _keyboardEventHandler.Clear();
        }
    }
}
