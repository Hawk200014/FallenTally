using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue.ViewModel;

namespace FallenTallyAvalon.Dialogue;

public partial class LocationDialogWindow : Window
{
    private LocationController _locationController;
    private GameStatsModel _activeGame;
    private LocationDialogWindowViewModel _locationDialogWindowViewModel;

    public LocationDialogWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public LocationDialogWindow(LocationController locationController, GameStatsModel activeGame) : this()
    {
        _locationController = locationController;
        _activeGame = activeGame;
        _locationDialogWindowViewModel = new LocationDialogWindowViewModel(locationController, activeGame, CloseWindow);
        DataContext = _locationDialogWindowViewModel;
    }

    public LocationDialogWindow(LocationController locationController, GameStatsModel activeGame, DeathLocationModel data) : this(locationController, activeGame)
    {

        _locationDialogWindowViewModel.LocationName = data.Name;
    }

    public void CloseWindow()
    {
        this.Close(_locationDialogWindowViewModel.Result);
    }
}