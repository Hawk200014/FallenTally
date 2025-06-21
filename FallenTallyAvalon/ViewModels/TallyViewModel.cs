using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FallenTally.Controller;
using FallenTally.Database.Models;
using FallenTally.Database.Models;
using FallenTally.Controller;
using FallenTally.Controller.Timers;
using FallenTally.Dialogue;
using FallenTally.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FallenTally.ViewModels
{
    public partial class TallyViewModel : ObservableObject
    {
        private readonly GameController _gameController;
        private readonly LocationController _locationController;
        private readonly DeathController _deathController;
        private readonly StreamingController _streamingController;
        private readonly RecordingController _recordingController;
        private readonly MarkerController _markerController;
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
        private ObservableCollection<MarkerModel> markers = new();

        [ObservableProperty]
        private string streamTime = "00:00:00";

        [ObservableProperty]
        private string recordingTime = "00:00:00";

        [ObservableProperty]
        private bool isStreamRunning = false;

        [ObservableProperty]
        private bool isRecordingRunning = false;

        public bool GameEditButtonEnabled => ActiveGame != null;
        public bool GameRemoveButtonEnabled => ActiveGame != null;
        public bool LocationAddButtonEnabled => ActiveGame != null;
        public bool LocationEditButtonEnabled => ActiveLocation != null;
        public bool LocationRemoveButtonEnabled => ActiveLocation != null;
        public bool DeathAddButtonEnabled => ActiveLocation != null;
        public bool DeathRemoveButtonEnabled => ActiveLocation != null;
        public bool StartStreamButtonEnabled => !IsStreamRunning;
        public bool StopStreamButtonEnabled => IsStreamRunning;
        public bool StartRecordingButtonEnabled => !IsRecordingRunning;
        public bool StopRecordingButtonEnabled => IsRecordingRunning;

        public TallyViewModel(
            GameController gameController,
            LocationController locationController,
            DeathController deathController,
            StreamingController streamingController,
            RecordingController recordingController,
            MarkerController markerController)
        {
            _gameController = gameController;
            _locationController = locationController;
            _deathController = deathController;
            _streamingController = streamingController;
            _recordingController = recordingController;
            _markerController = markerController;

            GameStats = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
            Markers = new ObservableCollection<MarkerModel>(_markerController.GetAllMarkers());
        }


        [RelayCommand]
        public async Task AddGame()
        {
            var dialog = new GameDialogWindow(_gameController);
            var newGame = await dialog.ShowDialog<GameStatsModel?>(MainWindow.Instance);

            if (newGame != null)
            {
                _gameController.AddGame(newGame.GameName, newGame.Prefix);
                GameStats = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
                ActiveGame = GameStats.FirstOrDefault(x => x.GameName == newGame.GameName);
            }
        }

        [RelayCommand]
        public async Task EditGame()
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
        public async Task RemoveGame()
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
        public async Task AddLocation()
        {
            if (ActiveGame == null) return;
            var dialog = new LocationDialogWindow(_locationController, ActiveGame);
            var newLocation = await dialog.ShowDialog<DeathLocationModel?>(MainWindow.Instance);
            if (newLocation != null)
            {
                _locationController.AddLocation(ActiveGame, newLocation.Name);
                DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(ActiveGame));
                ActiveLocation = DeathLocations.FirstOrDefault(x => x.Name == newLocation.Name);
            }
        }

        [RelayCommand]
        public async Task EditLocation()
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
        public async Task RemoveLocation()
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
        public void AddDeath()
        {
            if (ActiveLocation == null) return;
            _deathController.AddDeath(ActiveLocation, _streamingController, _recordingController);
            CounterValue = _deathController.GetDeaths(ActiveLocation).ToString();
        }

        [RelayCommand]
        public void RemoveDeath()
        {
            if (ActiveLocation == null) return;
            _deathController.RemoveDeath();
            CounterValue = _deathController.GetDeaths(ActiveLocation).ToString();
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

        [RelayCommand]
        public void AddGeneralMarker()
        {
            _markerController.SetMark(MarkerController.MARKER.NORMAL, ActiveGame);
            Markers = new ObservableCollection<MarkerModel>(_markerController.GetAllMarkers());
        }

        [RelayCommand]
        public void AddFunnyMarker()
        {
            _markerController.SetMark(MarkerController.MARKER.FUNNY, ActiveGame);
            Markers = new ObservableCollection<MarkerModel>(_markerController.GetAllMarkers());
        }

        [RelayCommand]
        public void AddTalkMarker()
        {
            _markerController.SetMark(MarkerController.MARKER.TALK, ActiveGame);
            Markers = new ObservableCollection<MarkerModel>(_markerController.GetAllMarkers());
        }

        [RelayCommand]
        public void AddGamePlayMarker()
        {
            _markerController.SetMark(MarkerController.MARKER.GAME, ActiveGame);
            Markers = new ObservableCollection<MarkerModel>(_markerController.GetAllMarkers());
        }

        [RelayCommand]
        public void AddPauseMarker()
        {
            _markerController.SetMark(MarkerController.MARKER.PAUSE, ActiveGame);
            Markers = new ObservableCollection<MarkerModel>(_markerController.GetAllMarkers());
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
                ? _deathController.GetDeaths(value).ToString()
                : "0";
        }
    }
}
