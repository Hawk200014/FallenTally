using Avalonia.Input;
using FallenTally.Services;
using FallenTally.ViewModels;
using FallenTallyAvalon.Helper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if WINDOWS
using AnyHook;
#endif

namespace FallenTally.Controller
{
    public class HotkeyController : IDisposable
    {
#if WINDOWS
        private readonly List<GlobalHotkey> _registeredHotKeys = new();
        private readonly Dictionary<GlobalHotkey, HotkeyHelper> _hotkeyMap = new();
        private readonly IGlobalHotkeyManager _hotkeyManager;
#endif

        public event Action<HotkeyHelper>? HotkeyPressed;

        public HotkeyController()
        {
#if WINDOWS
            _hotkeyManager = GlobalHotkeyManager.Create();
            ReloadKeysFromOptions();
#endif
        }

        /// <summary>
        /// Unregisters all hotkeys, loads the hotkey list from settings, and registers them again.
        /// </summary>
        public void ReloadKeysFromOptions()
        {
#if WINDOWS
            // Unregister all current hotkeys
            foreach (var hotkey in _registeredHotKeys)
            {
                _hotkeyManager.Unregister(hotkey);
                hotkey.Dispose();
            }
            _registeredHotKeys.Clear();
            _hotkeyMap.Clear();

            // Load hotkeys from settings
            var hotkeyHelpers = JsonSettingsService.Load<List<HotkeyHelper>>(SettingsViewModel.HotkeyFileName) ?? new List<HotkeyHelper>();

            foreach (var helper in hotkeyHelpers)
            {
                if (TryCreateHotKey(helper, out var hotkey))
                {
                    hotkey.Pressed += (s, e) => {
                        Console.WriteLine(e)
                        HotkeyPressed?.Invoke(helper);
                    }
                    _hotkeyManager.Register(hotkey);
                    _registeredHotKeys.Add(hotkey);
                    _hotkeyMap[hotkey] = helper;
                }
            }
#endif
        }

#if WINDOWS
        private static bool TryCreateHotKey(HotkeyHelper helper, out GlobalHotkey hotkey)
        {
            hotkey = null!;
            try
            {
                var modifiers = AnyHook.ModifierKeys.None;
                if (helper._altKey)
                    modifiers |= AnyHook.ModifierKeys.Alt;
                if (helper._strgKey)
                    modifiers |= AnyHook.ModifierKeys.Control;
                // Add shift/win if needed

                // Convert Avalonia.Input.Key to System.Windows.Forms.Keys
                var key = AvaloniaKeyToWinFormsKey(helper._key);

                hotkey = new GlobalHotkey(modifiers, key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Helper to convert Avalonia.Input.Key to System.Windows.Forms.Keys
        private static System.Windows.Forms.Keys AvaloniaKeyToWinFormsKey(Key key)
        {
            // Letters
            if (key >= Key.A && key <= Key.Z)
                return System.Windows.Forms.Keys.A + (key - Key.A);
            // Digits
            if (key >= Key.D0 && key <= Key.D9)
                return System.Windows.Forms.Keys.D0 + (key - Key.D0);
            // Function keys
            if (key >= Key.F1 && key <= Key.F24)
                return System.Windows.Forms.Keys.F1 + (key - Key.F1);
            // Common keys
            return key switch
            {
                Key.Escape => System.Windows.Forms.Keys.Escape,
                Key.Space => System.Windows.Forms.Keys.Space,
                Key.Enter => System.Windows.Forms.Keys.Enter,
                Key.Tab => System.Windows.Forms.Keys.Tab,
                Key.Left => System.Windows.Forms.Keys.Left,
                Key.Up => System.Windows.Forms.Keys.Up,
                Key.Right => System.Windows.Forms.Keys.Right,
                Key.Down => System.Windows.Forms.Keys.Down,
                Key.Delete => System.Windows.Forms.Keys.Delete,
                Key.Back => System.Windows.Forms.Keys.Back,
                _ => (System.Windows.Forms.Keys)((int)key)
            };
        }
#endif

        public void Dispose()
        {
#if WINDOWS
            foreach (var hotkey in _registeredHotKeys)
            {
                _hotkeyManager.Unregister(hotkey);
                hotkey.Dispose();
            }
            _registeredHotKeys.Clear();
            _hotkeyMap.Clear();
#endif
        }
    }
}