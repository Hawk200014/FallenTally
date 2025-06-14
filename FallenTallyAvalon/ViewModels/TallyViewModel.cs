using CommunityToolkit.Mvvm.Input;
using DeathCounterHotkey.Database.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FallenTally.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue;
using FallenTallyAvalon.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FallenTallyAvalon.ViewModels
{
    public class TallyViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GameStatsModel> _gameStats = new();
        private ObservableCollection<DeathLocationModel> _deathLocations = new();
        private GameStatsModel? _activeGame;
        private DeathLocationModel? _activeLocation;
        private string _counterValue = string.Empty;
        private ObservableCollection<MarkerModel> _markers = new();
        private GameController _gameController;
        private LocationController _locationController;
        private DeathController _deathController;

        #region RelayCommands
        public IRelayCommand AddGameCommand { get; }
        public IRelayCommand EditGameCommand { get; }
        public IRelayCommand RemoveGameCommand { get; }

        public IRelayCommand AddLocationCommand { get; }
        public IRelayCommand EditLocationCommand { get; }
        public IRelayCommand RemoveLocationCommand { get; }

        public IRelayCommand AddDeathCommand { get; }
        public IRelayCommand RemoveDeathCommand { get; }

        #endregion

        #region Constructors
        public TallyViewModel(GameController gameController, LocationController locationController, DeathController deathController)
        {
            _gameController = gameController;
            _locationController = locationController;
            _deathController = deathController;

            AddGameCommand = new AsyncRelayCommand(AddGameAsync);
            EditGameCommand = new AsyncRelayCommand(EditGameAsync);
            RemoveGameCommand = new AsyncRelayCommand(RemoveGameAsync);

            AddLocationCommand = new AsyncRelayCommand(AddLocationAsync);
            EditLocationCommand = new AsyncRelayCommand(EditLocationAsync);
            RemoveLocationCommand = new AsyncRelayCommand(RemoveLocationAsync);

            AddDeathCommand = new AsyncRelayCommand(AddDeathAsync);
            RemoveDeathCommand = new AsyncRelayCommand(RemoveDeathAsync);

            GameStats = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
        }
        #endregion

        #region RelayFunctions
        #region GameStats

        private async Task AddGameAsync()
        {
            var dialog = new GameDialogWindow(_gameController);
            var newGame = await dialog.ShowDialog<GameStatsModel?>(MainWindow.Instance);

            if (newGame != null)
            {
                GameStats.Add(newGame);
                ActiveGame = GameStats.FirstOrDefault(x => x.GameName == newGame.GameName);
                _gameController.AddGame(newGame.GameName, newGame.Prefix);
            }
        }

        private async Task EditGameAsync()
        {
            if (ActiveGame == null) return;
            var dialog = new GameDialogWindow(_gameController, ActiveGame);
            var editedGame = await dialog.ShowDialog<GameStatsModel?>(MainWindow.Instance);
            if (editedGame != null)
            {
                _ = _gameController.EditName(ActiveGame, editedGame);
                GameStats = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
                ActiveGame = GameStats.FirstOrDefault(x => x.GameName == editedGame.GameName);
                OnPropertyChanged(nameof(ActiveGame));
            }
        }

        private async Task RemoveGameAsync()
        {
            if (ActiveGame == null) return;
            var dialog = new ConfirmationDialog();
            var yes = await dialog.ShowDialog<bool?>(MainWindow.Instance);
            if (yes == true)
            {
                _gameController.RemoveGame(ActiveGame);
                GameStats = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
                ActiveGame = null;
                OnPropertyChanged(nameof(ActiveGame));
            }
        }

        #endregion

        #region DeathLocations
        private async Task AddLocationAsync()
        {
            if (ActiveGame == null) return;
            var dialog = new LocationDialogWindow(_locationController, ActiveGame);
            var newLocation = await dialog.ShowDialog<DeathLocationModel?>(MainWindow.Instance);
            if (newLocation != null)
            {
                DeathLocations.Add(newLocation);
                _locationController.AddLocation(ActiveGame, newLocation.Name);
                OnPropertyChanged(nameof(DeathLocations));
                ActiveLocation = DeathLocations.FirstOrDefault(x => x.Name == newLocation.Name);
            }
        }

        private async Task EditLocationAsync()
        {
            if (ActiveLocation == null) return;
            var dialog = new LocationDialogWindow(_locationController, ActiveGame, ActiveLocation);
            var editedLocation = await dialog.ShowDialog<DeathLocationModel?>(MainWindow.Instance);
            if (editedLocation != null)
            {
                _locationController.EditName(ActiveGame, ActiveLocation, editedLocation.Name);
                DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(ActiveGame));
                OnPropertyChanged(nameof(DeathLocations));
                ActiveLocation = DeathLocations.FirstOrDefault(x => x.Name == editedLocation.Name);
            }
        }

        private async Task RemoveLocationAsync()
        {
            if (ActiveLocation == null) return;
            var dialog = new ConfirmationDialog();
            var yes = await dialog.ShowDialog<bool?>(MainWindow.Instance);
            if (yes == true)
            {
                _locationController.RemoveLocation(ActiveLocation);
                DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(ActiveGame));
                OnPropertyChanged(nameof(DeathLocations));
                ActiveLocation = null;
            }
        }
        #endregion

        #region Deaths

        private async Task AddDeathAsync()
        {
            if (ActiveLocation == null) return;
            _deathController.AddDeath(ActiveLocation);
            CounterValue = _locationController.GetDeathsAtLocation(ActiveLocation).ToString();
        }

        private async Task RemoveDeathAsync()
        {
            if (ActiveLocation == null) return;
            _deathController.RemoveDeath();
            CounterValue = _locationController.GetDeathsAtLocation(ActiveLocation).ToString();
        }

        #endregion

        #endregion

        public GameStatsModel? ActiveGame
        {
            get => _activeGame;
            set
            {
                if (_activeGame != value)
                {
                    _activeGame = value;
                    OnPropertyChanged();
                    DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(_activeGame));
                    CounterValue = "0";
                }
                else
                {
                    CounterValue = "0";
                }
            }
        }

        public DeathLocationModel? ActiveLocation
        {
            get => _activeLocation;
            set
            {
                if (_activeLocation != value)
                {
                    _activeLocation = value;
                    OnPropertyChanged();
                    if (_activeLocation != null)
                    {
                        CounterValue = _locationController.GetDeathsAtLocation(_activeLocation).ToString();
                    }
                }
                else
                {
                    CounterValue = "0";
                }
            }
        }


        public ObservableCollection<GameStatsModel> GameStats
        {
            get => _gameStats;
            set { _gameStats = value; OnPropertyChanged(); }
        }

        public ObservableCollection<DeathLocationModel> DeathLocations
        {
            get => _deathLocations;
            set { _deathLocations = value; OnPropertyChanged(); }
        }


        public string CounterValue
        {
            get => _counterValue;
            set { _counterValue = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MarkerModel> Markers
        {
            get => _markers;
            set { _markers = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
