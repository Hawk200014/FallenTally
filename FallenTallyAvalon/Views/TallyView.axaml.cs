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


        InitializeComponent();


        TallyViewModel? viewModel = ServiceLocator.Provider.GetRequiredService<TallyViewModel>();
        viewModel.SetShowTempMessageDialogAction(ShowTempMessageDialog);
        DataContext = viewModel;

    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void ShowTempMessageDialog(int durationSeconds, string text)
    {
        var dialog = new TemporaryMessageDialog(durationSeconds, text);
        dialog.ShowDialog(MainWindow.Instance);
    }
}
