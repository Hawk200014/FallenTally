using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FallenTally.Controller;
using FallenTally.Database.Models;
using FallenTally.Dialogue.ViewModel;
using System;

namespace FallenTally.Dialogue;

public partial class GameDialogWindow : Window
{

    private GameStatsModel? _data;
    private GameController _gameController;
    private GameDialogWindowViewModel _gameDialogWindowViewModel;
    

    public GameDialogWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public GameDialogWindow(GameController gameController) : this()
    {
        _gameDialogWindowViewModel = new GameDialogWindowViewModel(gameController, CloseWindow);
        DataContext = _gameDialogWindowViewModel;
    }

    public GameDialogWindow(GameController gameController, GameStatsModel data) : this(gameController)
    {

        _gameDialogWindowViewModel.GameName = data.GameName;
        _gameDialogWindowViewModel.GamePrefix = data.Prefix;
    }


    public void CloseWindow()
    {
        this.Close(_gameDialogWindowViewModel.Result);
    }


}