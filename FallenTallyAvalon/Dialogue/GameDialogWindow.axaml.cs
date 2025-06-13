using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue.ViewModel;
using FallenTallyAvalon.Views;
using System.Threading.Tasks;

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

    public void CloseWindow()
    {
        this.Close(_gameDialogWindowViewModel.Result);
    }


}