using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Resources;
using FallenTally.Controller.Model;
using FallenTally.Enums;
using FallenTally.Utility.ResultSets;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class MainController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();

        private bool _isRecording = false;
        private bool _isStreaming;

        private GameController? _gameController;
        private LocationController? _locationcontroller;
        private EditController? _editController;
        private OptionsController? _optionsController;
        private TwitchTokenController? _twitchTokenController;
        private HotkeyController? _hotkeyController;
        private MainForm? _mainForm;
        private TimerController? _streamTimeController;
        private TimerController? _recordTimeController;
        private TextController? _textController;
        private ExportController? _exportController;
        private MarkerController? _markerController;
        private RecordingController? _recordingController;
        
        private DeathModelController _deathModelController = new DeathModelController();

        public MainController() 
        {
            this._gameController = _singleton.GetValue(GameController.GetSingletonName()) as GameController;
            this._locationcontroller = _singleton.GetValue(LocationController.GetSingletonName()) as LocationController;
            this._editController = _singleton.GetValue(EditController.GetSingletonName()) as EditController;
            this._optionsController = _singleton.GetValue(OptionsController.GetSingletonName()) as OptionsController;
            this._streamTimeController = _singleton.GetValue("StreamTimerController") as TimerController;
            this._recordTimeController = _singleton.GetValue("RecordTimerController") as TimerController;
            this._textController = _singleton.GetValue(TextController.GetSingletonName()) as TextController;
            this._exportController = _singleton.GetValue(ExportController.GetSingletonName()) as ExportController;
            this._markerController = _singleton.GetValue(MarkerController.GetSingletonName()) as MarkerController;
            this._recordingController = _singleton.GetValue(RecordingController.GetSingletonName()) as RecordingController;
            this._twitchTokenController = _singleton.GetValue(TwitchTokenController.GetSingletonName()) as TwitchTokenController;

            this._hotkeyController = new HotkeyController(
                IncreaseHotkeyPressed, DecreaseHotkeyPressed, SwitchHotkeyPressed, 
                QuickHotkeyPressed, FinishHotkeyPressed, StartRecordingHotkeyPressed, MarkerNormalHotkeyPressed,
                MarkerFunnyHotkeyPressed, MarkerGamingHotkeyPressed, MarkerTalkHotkeyPressed, MarkerPauseHotkeyPressed);
            _hotkeyController.LoadHotkeys();

        }

        #region Getter
        public ExportController GetExportController()
        {
            return _exportController;
        }

        public GameController GetGameController()
        {
            return _gameController;
        }

        internal EditController GetEditController()
        {
            return _editController;
        }

        internal LocationController GetLocationController()
        {
            return _locationcontroller;
        }

        internal OptionsController GetOptionController()
        {
            return _optionsController;
        }

        internal TimerController GetStreamTimeController()
        {
            return _streamTimeController;
        }

        internal TimerController GetRecordTimeController()
        {
            return _recordTimeController;
        }

        internal TextController GetTextController()
        {
            return _textController;
        }

        internal MarkerController GetMarkerController()
        {
            return _markerController;
        }

        internal RecordingController GetRecordingController()
        {
            return _recordingController;
        }

        #endregion

        #region Setter

        public void SetStreaming(bool isStreaming)
        {
            _isStreaming = isStreaming;
        }

        internal void SetRecordTimeController(TimerController recordTimerController)
        {
            this._recordTimeController = recordTimerController;
        }

        internal void SetStreamTimeController(TimerController controller)
        {
            this._streamTimeController = controller;
        }

        #endregion

        #region Hotkey Methods

        private void StartRecordingHotkeyPressed()
        {
            if (_isRecording)
            {
                _mainForm?.StopRecordTimer();
                _isRecording = false;
                return;
            }
            _isRecording = true;
            _recordingController.AddRecording(RECORDINGTYPE.recording);
            _mainForm?.StartRecordingTimer();
        }

        private void MarkerNormalHotkeyPressed()
        {
            if ((_isRecording || _isStreaming) && _gameController.GetActiveGame() != null)
            {
                _markerController.SetMark(MARKER.NORMAL, _gameController.GetActiveGame(), _streamTimeController, _recordTimeController);
                _mainForm?.UpdateMarkers();
            }
        }

        private void MarkerFunnyHotkeyPressed()
        {
            if ((_isRecording || _isStreaming) && _gameController.GetActiveGame() != null)
            {
                _markerController.SetMark(MARKER.FUNNY, _gameController.GetActiveGame(), _streamTimeController, _recordTimeController);
                _mainForm?.UpdateMarkers();
            }
        }

        private void MarkerGamingHotkeyPressed()
        {
            if ((_isRecording || _isStreaming) && _gameController.GetActiveGame() != null)
            {
                _markerController.SetMark(MARKER.GAME, _gameController.GetActiveGame(), _streamTimeController, _recordTimeController);
                _mainForm?.UpdateMarkers();
            }
        }

        private void MarkerTalkHotkeyPressed()
        {
            if ((_isRecording || _isStreaming) && _gameController.GetActiveGame() != null)
            {
                _markerController.SetMark(MARKER.TALK, _gameController.GetActiveGame(), _streamTimeController, _recordTimeController);
                _mainForm?.UpdateMarkers();
            }
        }

        private void MarkerPauseHotkeyPressed()
        {
            if ((_isRecording || _isStreaming) && _gameController.GetActiveGame() != null)
            {
                _markerController.SetMark(MARKER.PAUSE, _gameController.GetActiveGame(), _streamTimeController, _recordTimeController);
                _mainForm?.UpdateMarkers();
            }
        }

        private void FinishHotkeyPressed()
        {
            if (_locationcontroller.GetActiveLocation() is null) return;
            if (_locationcontroller.GetActiveLocation()?.Name == GLOBALVARS.DEFAULT_LOCATION) return;
            _locationcontroller.SetFinish(true);
        }


        private void IncreaseHotkeyPressed()
        {
            IncreaseDeaths();
            _mainForm?.UpdateDeaths();
        }

        private void DecreaseHotkeyPressed()
        {
            DecreaseDeaths();
            _mainForm?.UpdateDeaths();
        }

        private void SwitchHotkeyPressed()
        {
            List<DeathLocationModel> locations = _locationcontroller.GetListOfLocations();
            locations = locations.Where(x => x.Finish == false).ToList();
            int locLength = locations.Count;
            if (locLength == 0) return;
            int index = GetIndexOfListWithItemName(locations, _locationcontroller.GetActiveLocation()?.Name);
            if (index == -1) return;
            if (index == locLength - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            string newLocName = locations[index].Name;
            _locationcontroller.SetActiveLocation(newLocName);
            _mainForm?.SetLocation(newLocName);
            _mainForm?.UpdateDeaths();
        }

        private void QuickHotkeyPressed()
        {
            QuickAddLocation();
        }

        public void DecreaseDeaths()
        {
            if (_gameController.GetActiveGame() is null) return;
            DeathLocationModel? model = _locationcontroller.GetActiveLocation();
            if (model is null) return;

            ResultSet<List<DeathModel>?> resultSet = _deathModelController.GetItems(model);
            if (resultSet.GetResult() == RESULT.FAILURE || resultSet.GetData()?.Count == 0) return;

            DeathModel deahModel = resultSet.GetData()!.OrderByDescending(x => x.DeathId).First();
            _deathModelController.RemoveItem(deahModel);
        }

        #endregion

        


        private int GetIndexOfListWithItemName(List<DeathLocationModel> locations, string? name)
        {
            if(name is null) return -1;
            for (int i = 0; i < locations.Count; i++)
            {
                if (locations[i].Name == name)
                {
                    return i;
                }
            }
            return 0;
        }

        

        public string GameChanged(string? gameName)
        {
            if (gameName is null) return "";
            _gameController.SetActiveGame(gameName);
            return _gameController.GetPrefix();
        }

        

        public int GetAllDeaths()
        {
            return this._gameController.GetAllDeaths();
        }

        

        internal List<string> GetGameNames()
        {
            return _gameController.GetAllGameNames();
        }

        internal TwitchTokenController GetTwitchTokenController()
        {
            return _twitchTokenController;
        }

        

        internal int GetLocationDeaths()
        {
            if( _locationcontroller.GetActiveLocation() is null) return 0;
            return _locationcontroller.GetDeathsAtLocation();
        }

        internal List<string> GetLocationNames()
        {
            //if(_locationcontroller.GetActiveLocation() is null) return new List<string>();
            return _locationcontroller.GetListOfLocations().Select(x=> x.Name).ToList();
        }

        

        internal void OptionsChangedAction()
        {
            _hotkeyController.UnregisterHotkeys();
            _hotkeyController.LoadHotkeys();
            _mainForm?.UpdateDeaths();
        }

        internal void IncreaseDeaths()
        {
            if (_gameController.GetActiveGame() is null) return;
            if (_locationcontroller.GetActiveLocation() is null) return;

            DeathModel death = new DeathModel()
            {
                LocationId = _locationcontroller.GetActiveLocation().LocationId,
                TimeStamp = DateTime.Now,
                StreamTime = _streamTimeController.GetTime(),
                RecordingTime = _recordTimeController.GetTime()
            };

            ResultSet<DeathModel?> resultSet = _deathModelController.AddItem(death);
            if (resultSet.GetResult() == RESULT.FAILURE)
            {

                //_mainForm?.ShowError(resultSet.GetMessage());
            }
        }

        internal void RemoveGame( )
        {
            _locationcontroller.RemoveAllLocations(_gameController.GetActiveGame());
            _gameController.RemoveGame();
        }

        internal bool RemoveLocation()
        {
            return _locationcontroller.RemoveLocation();
        }

        internal void ResetDeahts()
        {
            //todo text with nope rope
        }

        internal void UnregisterHotkeys()
        {
            _hotkeyController.UnregisterHotkeys();
        }

        internal void LocationChanged(string locationName)
        {
            if (locationName is null) return ;
            _locationcontroller.SetActiveLocation(locationName);
        }

        public void QuickAddLocation()
        {
            string timestamp = GetTimestamp(DateTime.Now);
            string locationName = "Loc" + timestamp.Substring(timestamp.Length / 2);
            _locationcontroller.AddLocation(locationName);
            _mainForm?.UpdateLocationList();
            _mainForm?.SetLocation(locationName);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        internal void SetForm(MainForm mainForm)
        {
            this._mainForm = mainForm;
        }

        public static string GetSingletonName()
        {
            return "MainController";
        }
    }
}
