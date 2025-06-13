using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using DeathCounterHotkey.Database.Models;
using FallenTally.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue;
using FallenTallyAvalon.Views;

namespace FallenTallyAvalon.ViewModels
{
    public class TallyViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GameStatsModel> _gameStats = new();
        private ObservableCollection<DeathLocationModel> _deathLocations = new();
        private GameStatsModel? _activeGame;
        private DeathLocationModel? _activeLocation;
        private string _counterValue = string.Empty;
        private int counterValueInt = 0;
        private ObservableCollection<MarkerModel> _markers = new();
        private GameController _gameController;

        #region RelayCommands
        public IRelayCommand AddGameCommand { get; }

        #endregion

        #region Constructors
        public TallyViewModel( GameController gameController)
        {
            _gameController = gameController;
            AddGameCommand = new AsyncRelayCommand(AddGameAsync);
            GameStats = new ObservableCollection<GameStatsModel>(gameController.GetGameStats());
        }
        #endregion

        #region RelayFunctions

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
