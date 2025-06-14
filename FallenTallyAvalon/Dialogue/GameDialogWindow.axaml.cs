using Avalonia;
using Avalonia.Controls;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue.ViewModel;

namespace FallenTallyAvalon.Dialogue;

public partial class GameDialogWindow : Window
{

    private GameStatsModel? _data;
    private GameController _gameController;
    private GameDialogWindowViewModel _gameDialogWindowViewModel;
    

    public GameDialogWindow()
    {
        InitializeComponent();
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