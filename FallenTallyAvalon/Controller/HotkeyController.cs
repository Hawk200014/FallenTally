using Avalonia.Input;
using FallenTally.Services;
using FallenTallyAvalon.Helper;
using SharpHook;
using SharpHook.Reactive;
using SharpHook.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace FallenTally.Controller
{
    public class HotkeyController : IDisposable
    {
        private IReactiveGlobalHook? _globalHook;
        private List<HotkeyHelper> _hotkeys = new();
        public event Action<HotkeyHelper>? HotkeyTriggered;

        public HotkeyController()
        {
            ReloadKeysFromOptions();
            StartGlobalHook();
        }

        public void ReloadKeysFromOptions()
        {
            // Load hotkeys from settings
            _hotkeys = JsonSettingsService.Load<List<HotkeyHelper>>(SettingsViewModel.HotkeyFileName) ?? new List<HotkeyHelper>();
        }

        private void StartGlobalHook()
        {
            _globalHook = new SimpleReactiveGlobalHook();
            _globalHook.KeyPressed
                .Subscribe(OnGlobalKeyPressed);
            _globalHook.RunAsync();
        }

        private void OnGlobalKeyPressed(KeyboardHookEventArgs e)
        {
            // Map SharpHook key/modifiers to Avalonia.Input.Key/KeyModifiers
            var key = (Key)e.Data.KeyCode;
            var modifiers = MapModifiers(e.Data.Modifiers);

            foreach (var hotkey in _hotkeys)
            {
                if (hotkey._key == key &&
                    hotkey._altKey == modifiers.HasFlag(KeyModifiers.Alt) &&
                    hotkey._strgKey == modifiers.HasFlag(KeyModifiers.Control))
                {
                    HotkeyTriggered?.Invoke(hotkey);
                    break;
                }
            }
        }

        private KeyModifiers MapModifiers(Modifier mask)
        {
            KeyModifiers modifiers = KeyModifiers.None;
            if (mask.HasFlag(ModifierMask.Alt))
                modifiers |= KeyModifiers.Alt;
            if (mask.HasFlag(ModifierMask.Control))
                modifiers |= KeyModifiers.Control;
            if (mask.HasFlag(ModifierMask.Shift))
                modifiers |= KeyModifiers.Shift;
            return modifiers;
        }

        public void Dispose()
        {
            _globalHook?.Dispose();
        }
    }
}