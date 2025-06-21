using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FallenTallyAvalon.Helper;

namespace FallenTally.Views;

public partial class SettingsView : UserControl
{
    private bool waitingForInput = false;
    private HotkeyHelper? _hotkeyHelper;
    public SettingsView()
    {
        InitializeComponent();
    }

    private void TextBox_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (!waitingForInput)
        {
            e.Handled = true;
            return;
        }
        if (sender is null)
        {
            e.Handled = true;
            return;
        }
        TextBox tb = (TextBox)sender;

        if(e.KeyModifiers == Avalonia.Input.KeyModifiers.None)
        {
            _hotkeyHelper?.SetKey(e.Key);
            TI.Focus();
        }
        else
        {
            _hotkeyHelper?.SetKeyModifier(e.KeyModifiers);
        }

            e.Handled = false;
    }

    private void TextBox_GotFocus(object? sender, Avalonia.Input.GotFocusEventArgs e)
    {
        if (sender is null)
        {
            e.Handled = true;
            return;
        }
        TextBox tb = (TextBox)sender;
        tb.Text = "Waiting for input...";
        _hotkeyHelper = new HotkeyHelper(tb.Name ?? "");
        waitingForInput = true;
        e.Handled = true;
    }

    private void TextBox_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is null)
        {
            e.Handled = true;
            return;
        }
        TextBox tb = (TextBox)sender;
        tb.Text = _hotkeyHelper?.ToString();
        _hotkeyHelper = null;
        waitingForInput = false;

        e.Handled = false;

    }
}