using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FallenTally.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FallenTally.Views;

public partial class ExportView : UserControl
{
    public ExportView()
    {
        DataContext = ServiceLocator.Provider.GetRequiredService<ExportViewModel>();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
