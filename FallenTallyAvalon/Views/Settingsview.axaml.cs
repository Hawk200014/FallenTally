using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using DocumentFormat.OpenXml.Drawing.Charts;
using FallenTally.ViewModels;
using FallenTallyAvalon.Helper;
using Microsoft.Extensions.DependencyInjection;
using SharpHook;
using SharpHook.Data;
using SharpHook.Reactive;
using System;
using System.Reactive.Concurrency;

namespace FallenTally.Views;

public partial class SettingsView : UserControl
{
    private HotkeyHelper? _hotkeyHelper;
    private SettingsViewModel? _viewModel;
    private static SimpleReactiveGlobalHook? _globalHook;
    private TextBox? _currentTB;

    public static void StopGlobalHook()
    {
        if(_globalHook?.IsRunning ?? false)
        {
            _globalHook?.Stop();
        }
    }

    public SettingsView()
    {
        InitializeComponent();
        _viewModel = ServiceLocator.Provider.GetRequiredService<SettingsViewModel>();
        DataContext = _viewModel;
        _globalHook = new SimpleReactiveGlobalHook(defaultScheduler: TaskPoolScheduler.Default);
        _globalHook.KeyPressed.Subscribe(OnGlobalKeyPressed);
        _globalHook.Stop();
    }

    private void TextBox_GotFocus(object? sender, Avalonia.Input.GotFocusEventArgs e)
    {
        if (sender is null || _currentTB?.Name == ((TextBox)sender).Name)
        {
            e.Handled = false;
            return;
        }
        TextBox tb = (TextBox)sender;
        _currentTB = tb;
        tb.Text = "";
        if (_hotkeyHelper == null)
        {
            _hotkeyHelper = new HotkeyHelper();
        }
        _hotkeyHelper.Name = tb.Name ?? "";
        if (!(_globalHook?.IsRunning)??true)
        {
            _globalHook?.RunAsync();
        }
        
        e.Handled = true;
    }

    private void OnGlobalKeyPressed(KeyboardHookEventArgs e)
    {
        if (_hotkeyHelper == null)
            return;
        // Map SharpHook key/modifiers to Avalonia.Input.Key/KeyModifiers
        KeyCode key = e.Data.KeyCode;
        switch (key)
        {
            case KeyCode.VcLeftControl:
            case KeyCode.VcRightControl:
                // Handle Control key
                _hotkeyHelper._strgKey = true;
                break;
            case KeyCode.VcLeftAlt:
            case KeyCode.VcRightAlt:
                _hotkeyHelper._altKey = true;
                // Handle Alt key
                break;
            default:
                _hotkeyHelper.SetKey(key);
                Dispatcher.UIThread.Invoke(() =>
                {
                    _currentTB.Text = _hotkeyHelper?.ToString();
                    _viewModel?.AddHotkey(_hotkeyHelper!);
                    _hotkeyHelper = null;
                    //_globalHook?.Stop();
                });
                break;

        }
    }

}