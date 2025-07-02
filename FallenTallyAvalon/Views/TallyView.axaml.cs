using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FallenTally.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FallenTally.Views;

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
