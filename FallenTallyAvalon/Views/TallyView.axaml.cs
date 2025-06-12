using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FallenTallyAvalon.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FallenTallyAvalon.Views;

public partial class TallyView : UserControl
{
    public TallyView()
    {

        DataContext = ServiceLocator.Provider.GetRequiredService<TallyViewModel>();

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
