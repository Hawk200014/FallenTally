using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using DocumentFormat.OpenXml.Drawing.Charts;
using FallenTally.Services;
using FallenTally.ViewModels;
using FallenTallyAvalon.Helper;
using Microsoft.Extensions.DependencyInjection;
using SharpHook;
using SharpHook.Data;
using SharpHook.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
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
        LoadHotkeyText();
    }

    private void LoadHotkeyText()
    {
        var _hotkeys = JsonSettingsService.Load<List<HotkeyHelper>>(SettingsViewModel.HotkeyFileName) ?? new List<HotkeyHelper>();

        AddDeathHK.Text = _hotkeys.Where(x => x.Name.Equals("AddDeathHK")).FirstOrDefault()?.ToString() ?? "";
        RemoveDeathHK.Text = _hotkeys.Where(x => x.Name.Equals("RemoveDeathHK")).FirstOrDefault()?.ToString() ?? "";
        QuickAddLocationHK.Text = _hotkeys.Where(x => x.Name.Equals("QuickAddLocationHK")).FirstOrDefault()?.ToString() ?? "";
        SwitchLocationHK.Text = _hotkeys.Where(x => x.Name.Equals("SwitchLocationHK")).FirstOrDefault()?.ToString() ?? "";
        FinishLocationHK.Text = _hotkeys.Where(x => x.Name.Equals("FinishLocationHK")).FirstOrDefault()?.ToString() ?? "";

        GeneralMarkerHK.Text = _hotkeys.Where(x => x.Name.Equals("GeneralMarkerHK")).FirstOrDefault()?.ToString() ?? "";
        FunnyMarkerHK.Text = _hotkeys.Where(x => x.Name.Equals("FunnyMarkerHK")).FirstOrDefault()?.ToString() ?? "";
        TalkMarkerHK.Text = _hotkeys.Where(x => x.Name.Equals("TalkMarkerHK")).FirstOrDefault()?.ToString() ?? "";
        GameplayMarkerHK.Text = _hotkeys.Where(x => x.Name.Equals("GameplayMarkerHK")).FirstOrDefault()?.ToString() ?? "";
        PauseMarkerHK.Text = _hotkeys.Where(x => x.Name.Equals("PauseMarkerHK")).FirstOrDefault()?.ToString() ?? "";

        StartRecordingHK.Text = _hotkeys.Where(x => x.Name.Equals("StartRecordingHK")).FirstOrDefault()?.ToString() ?? "";
        StopRecordingHK.Text = _hotkeys.Where(x => x.Name.Equals("StopRecordingHK")).FirstOrDefault()?.ToString() ?? "";
        StartStreamHK.Text = _hotkeys.Where(x => x.Name.Equals("StartStreamHK")).FirstOrDefault()?.ToString() ?? "";
        StopStreamHK.Text = _hotkeys.Where(x => x.Name.Equals("StopStreamHK")).FirstOrDefault()?.ToString() ?? "";




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