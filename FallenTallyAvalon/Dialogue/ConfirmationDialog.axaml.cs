using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FallenTally.Database.Models;
using FallenTally.Controller;
using FallenTally.Dialogue.ViewModel;

namespace FallenTally.Dialogue;

public partial class ConfirmationDialog : Window
{
    private ConfirmationDialogViewModel _viewModel;

    public ConfirmationDialog()
    {
        InitializeComponent();
        _viewModel = new ConfirmationDialogViewModel(CloseWindow);
        DataContext = _viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void CloseWindow()
    {
        this.Close(_viewModel.DialogResult);
    }
}