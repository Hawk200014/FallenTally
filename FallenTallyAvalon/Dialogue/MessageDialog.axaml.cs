using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace FallenTally;

public partial class MessageDialog : Window
{
    public MessageDialog(string text)
    {
        InitializeComponent();

        var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock") ?? throw new ArgumentNullException();
        messageTextBlock.Text = text;

        // Remove window chrome (top bar)
        SystemDecorations = SystemDecorations.None;
        CanResize = false;

        // Center the window on the owner or screen
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }
}