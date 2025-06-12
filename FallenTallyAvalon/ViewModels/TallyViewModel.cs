using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using DeathCounterHotkey.Database.Models;
using FallenTally.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue;

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
        private IDialog<GameStatsModel> _addGameDialog;
        private GameController _gameController;

        #region RelayCommands
        public IRelayCommand AddGameCommand { get; }

        #endregion

        #region Constructors
        public TallyViewModel(IDialog<GameStatsModel> addGameDialog, GameController gameController)
        {
            _addGameDialog = addGameDialog;
            _gameController = gameController;
            AddGameCommand = new RelayCommand(AddGame);
        }
        #endregion

        #region RelayFunctions

        private void AddGame()
        {
            _addGameDialog.Init();
            _addGameDialog.ShowDialogue();
            var newGame = _addGameDialog.GetData();
            if (newGame != null)
            {
                _activeGame = newGame;
                OnPropertyChanged(nameof(_activeGame));
                GameStats.Add(newGame);
                _gameController.AddGame(newGame.GameName, newGame.Prefix);
            }
        }

        #endregion

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
