using Avalonia.Input;
using FallenTally.Services;
using FallenTallyAvalon.Helper;
using SharpHook;
using SharpHook.Reactive;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using FallenTally.ViewModels;
using System.Reactive.Concurrency;
using SharpHook.Data;

namespace FallenTally.Controller
{
    public class HotkeyController : IDisposable
    {
        private IReactiveGlobalHook? _globalHook;
        private List<HotkeyHelper> _hotkeys = new();

        private HotkeyHelper? _currentHotkey;
        private TallyViewModel _tallyViewModel;

        public HotkeyController(TallyViewModel tallyViewModel)
        {
            _tallyViewModel = tallyViewModel;
            ReloadKeysFromOptions();
            StartGlobalHook();
        }

        public void StopHotkeys()
        {
            if (_globalHook?.IsRunning ?? false)
            {
                _globalHook.Stop();
            }
        }

        public void StartHotkeys()
        {
            if (_globalHook?.IsRunning ?? true)
            {
                _globalHook.RunAsync();
            }
        }

        public void ReloadKeysFromOptions()
        {
            // Load hotkeys from settings
            _hotkeys = JsonSettingsService.Load<List<HotkeyHelper>>(SettingsViewModel.HotkeyFileName) ?? new List<HotkeyHelper>();
        }

        private void StartGlobalHook()
        {
            _globalHook = new SimpleReactiveGlobalHook(defaultScheduler: TaskPoolScheduler.Default);
            _globalHook.KeyPressed
                .Subscribe(OnGlobalKeyPressed);
            _globalHook.RunAsync();
        }

        private void OnGlobalKeyPressed(KeyboardHookEventArgs e)
        {
            // Map SharpHook key/modifiers to Avalonia.Input.Key/KeyModifiers
            
            
            if(_currentHotkey == null)
            {
                _currentHotkey = new HotkeyHelper();
                _currentHotkey.Name = "Current Hotkey"; // Default name, can be changed later
            }

            bool checkHotkey = false;
            KeyCode key = e.Data.KeyCode;
            switch (key)
            {
                case KeyCode.VcLeftControl:
                case KeyCode.VcRightControl:
                    // Handle Control key
                    _currentHotkey._strgKey = true;
                    break;
                case KeyCode.VcLeftAlt:
                case KeyCode.VcRightAlt:
                    _currentHotkey._altKey = true;
                    // Handle Alt key
                    break;
                default:
                    _currentHotkey.SetKey( key );
                    checkHotkey = true;
                    break;

            }

            if (!checkHotkey) return;

            foreach (var hotkey in _hotkeys)
            {
                if(hotkey.Equals(_currentHotkey) )
                {
                    TallyViewModel.HotkeysActions.TryGetValue(hotkey.Name, out Action? action);
                    action?.Invoke();
                    _currentHotkey = null; // Reset after triggering
                    return;
                }
            }
        }



        public void Dispose()
        {
            _globalHook?.Dispose();
        }
    }
}