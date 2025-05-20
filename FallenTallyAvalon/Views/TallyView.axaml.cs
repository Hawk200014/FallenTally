using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FallenTallyAvalon.Views;

public partial class TallyView : UserControl
{
    public TallyView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
