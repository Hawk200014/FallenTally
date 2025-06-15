using Avalonia.Controls;
using FallenTallyAvalon.ViewModels;
using WebViewControl;

namespace FallenTallyAvalon.Views;

public partial class OverlayView : UserControl
{
    public OverlayView()
    {
        WebView.Settings.LogFile = "ceflog.txt";
        InitializeComponent();
        DataContext = new OverlayViewModel();
    }
}