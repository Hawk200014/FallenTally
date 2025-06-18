using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Database.Models;
using FallenTally.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Controller.Timer;
using FallenTallyAvalon.Dialogue;
using FallenTallyAvalon.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FallenTallyAvalon.ViewModels
{
    public partial class TallyViewModel : ObservableObject
    {
        private readonly GameController _gameController;
        private readonly LocationController _locationController;
        private readonly DeathController _deathController;
        private readonly StreamingController _streamingController;
        private readonly RecordingController _recordingController;

        [ObservableProperty]
        private ObservableCollection<GameStatsModel> gameStats = new();

        [ObservableProperty]
        private ObservableCollection<DeathLocationModel> deathLocations = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(GameEditButtonEnabled))]
        [NotifyPropertyChangedFor(nameof(GameRemoveButtonEnabled))]
        [NotifyPropertyChangedFor(nameof(LocationAddButtonEnabled))]
        private GameStatsModel? activeGame;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocationEditButtonEnabled))]
        [NotifyPropertyChangedFor(nameof(LocationRemoveButtonEnabled))]
        [NotifyPropertyChangedFor(nameof(DeathAddButtonEnabled))]
        [NotifyPropertyChangedFor(nameof(DeathRemoveButtonEnabled))]
        private DeathLocationModel? activeLocation;

        [ObservableProperty]
        private string counterValue = string.Empty;

        [ObservableProperty]
        private ObservableCollection<MarkerModel> markers = new()
        {
            new MarkerModel { categorie = "cat1", MarkerId = 1, StreamSession = 1 },
            new MarkerModel { categorie = "cat2", MarkerId = 2, StreamSession = 1 },
            new MarkerModel { categorie = "cat3", MarkerId = 3, StreamSession = 1 }
        };

        [ObservableProperty]
        private string streamTime = "00:00:00";

        [ObservableProperty]
        private string recordingTime = "00:00:00";

        [ObservableProperty]
        private bool isStreamRunning = false;

        [ObservableProperty]
        private bool isRecordingRunning = false;

        // Enabled properties for buttons
        public bool GameAddButtonEnabled => true;
        public bool GameEditButtonEnabled => true;
        public bool GameRemoveButtonEnabled => true;
        public bool LocationAddButtonEnabled => ActiveGame != null;
        public bool LocationEditButtonEnabled => ActiveLocation != null;
        public bool LocationRemoveButtonEnabled => ActiveLocation != null;
        public bool DeathAddButtonEnabled => ActiveLocation != null;
        public bool DeathRemoveButtonEnabled => ActiveLocation != null;

        public TallyViewModel(
            GameController gameController,
            LocationController locationController,
            DeathController deathController,
            StreamingController streamingController,
            RecordingController recordingController)
        {
            _gameController = gameController;
            _locationController = locationController;
            _deathController = deathController;
            _streamingController = streamingController;
            _recordingController = recordingController;

            GameStats = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
        }

        // Game commands
        [RelayCommand]
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

        [RelayCommand]
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
            }
        }

        [RelayCommand]
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
            }
        }

        // Location commands
        [RelayCommand]
        private async Task AddLocationAsync()
        {
            if (ActiveGame == null) return;
            var dialog = new LocationDialogWindow(_locationController, ActiveGame);
            var newLocation = await dialog.ShowDialog<DeathLocationModel?>(MainWindow.Instance);
            if (newLocation != null)
            {
                DeathLocations.Add(newLocation);
                _locationController.AddLocation(ActiveGame, newLocation.Name);
                ActiveLocation = DeathLocations.FirstOrDefault(x => x.Name == newLocation.Name);
            }
        }

        [RelayCommand]
        private async Task EditLocationAsync()
        {
            if (ActiveLocation == null) return;
            var dialog = new LocationDialogWindow(_locationController, ActiveGame, ActiveLocation);
            var editedLocation = await dialog.ShowDialog<DeathLocationModel?>(MainWindow.Instance);
            if (editedLocation != null)
            {
                _locationController.EditName(ActiveGame, ActiveLocation, editedLocation.Name);
                DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(ActiveGame));
                ActiveLocation = DeathLocations.FirstOrDefault(x => x.Name == editedLocation.Name);
            }
        }

        [RelayCommand]  
        private async Task RemoveLocationAsync()
        {
            if (ActiveLocation == null) return;
            var dialog = new ConfirmationDialog();
            var yes = await dialog.ShowDialog<bool?>(MainWindow.Instance);
            if (yes == true)
            {
                _locationController.RemoveLocation(ActiveLocation);
                DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(ActiveGame));
                ActiveLocation = null;
            }
        }

        // Death commands
        [RelayCommand]
        private async Task AddDeathAsync()
        {
            if (ActiveLocation == null) return;
            _deathController.AddDeath(ActiveLocation);
            CounterValue = _locationController.GetDeathsAtLocation(ActiveLocation).ToString();
        }

        [RelayCommand]
        private async Task RemoveDeathAsync()
        {
            if (ActiveLocation == null) return;
            _deathController.RemoveDeath();
            CounterValue = _locationController.GetDeathsAtLocation(ActiveLocation).ToString();
        }

        // Timer commands
        [RelayCommand]
        public void StartStream()
        {
            _streamingController.StartTimer();
            StreamTime = _streamingController.GetFormattedTime();
            _streamingController.Tick += (timer) =>
            {
                StreamTime = _streamingController.GetFormattedTime();
            };
            IsStreamRunning = true;
        }

        [RelayCommand]
        public void StopStream()
        {
            _streamingController.StopTimer();
            IsStreamRunning = false;
            StreamTime = "00:00:00";
        }

        [RelayCommand]
        public void StartRecording()
        {
            _recordingController.StartTimer();
            RecordingTime = _recordingController.GetFormattedTime();
            _recordingController.Tick += (timer) => RecordingTime = _recordingController.GetFormattedTime();
            IsRecordingRunning = true;
        }

        [RelayCommand]
        public void StopRecording()
        {
            _recordingController.StopTimer();
            IsRecordingRunning = false;
            RecordingTime = "00:00:00";
        }

        // Partial methods for enabled state updates
        partial void OnActiveGameChanged(GameStatsModel? value)
        {
            OnPropertyChanged(nameof(GameEditButtonEnabled));
            OnPropertyChanged(nameof(GameRemoveButtonEnabled));
            OnPropertyChanged(nameof(LocationAddButtonEnabled));
            OnPropertyChanged(nameof(LocationEditButtonEnabled));
            OnPropertyChanged(nameof(LocationRemoveButtonEnabled));
            OnPropertyChanged(nameof(DeathAddButtonEnabled));
            OnPropertyChanged(nameof(DeathRemoveButtonEnabled));
            DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(value));
            CounterValue = "0";
        }

        partial void OnActiveLocationChanged(DeathLocationModel? value)
        {
            OnPropertyChanged(nameof(LocationEditButtonEnabled));
            OnPropertyChanged(nameof(LocationRemoveButtonEnabled));
            OnPropertyChanged(nameof(DeathAddButtonEnabled));
            OnPropertyChanged(nameof(DeathRemoveButtonEnabled));
            CounterValue = value != null
                ? _locationController.GetDeathsAtLocation(value).ToString()
                : "0";
        }
    }
}
