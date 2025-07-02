using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.Controls.Shapes;
using System;

namespace FallenTally;

public partial class TemporaryMessageDialog : Window
{
    private readonly Rectangle _progressRectangle;
    private readonly int _durationSeconds;
    private int _elapsedSeconds = 0;
    private readonly DispatcherTimer _timer;

    public TemporaryMessageDialog(int durationSeconds, string text)
    {
        _durationSeconds = durationSeconds;
        InitializeComponent();

        _progressRectangle = this.FindControl<Rectangle>("ProgressRectangle");
        var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock") ?? throw new ArgumentNullException();
        messageTextBlock.Text = text;

        // Remove window chrome (top bar)
        SystemDecorations = SystemDecorations.None;
        CanResize = false;

        // Center the window on the owner or screen
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _elapsedSeconds++;
        double remaining = Math.Max(0, _durationSeconds - _elapsedSeconds);
        double percent = remaining / _durationSeconds;

        if (_progressRectangle != null)
        {
            // Animate width shrink
            double initialWidth = this.Bounds.Width - 40; // 20 margin left/right
            _progressRectangle.Width = initialWidth * percent;
        }

        if (_elapsedSeconds >= _durationSeconds)
        {
            _timer.Stop();
            Close();
        }
    }
}