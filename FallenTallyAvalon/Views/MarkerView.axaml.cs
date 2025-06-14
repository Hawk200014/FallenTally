using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FallenTallyAvalon.Views;

public partial class MarkerView : UserControl
{
    public MarkerView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
