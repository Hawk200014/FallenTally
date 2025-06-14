using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue.ViewModel;

namespace FallenTallyAvalon;

public partial class ConfirmationDialog : Window
{
    private ConfirmationDialogViewModel _viewModel;

    public ConfirmationDialog()
    {
        InitializeComponent();
        _viewModel = new ConfirmationDialogViewModel(CloseWindow);
        DataContext = _viewModel;
    }

    public void CloseWindow()
    {
        this.Close(_viewModel.DialogResult);
    }


}