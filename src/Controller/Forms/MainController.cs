using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Resources;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class MainController
    {
        private GameController _gameController;
        private LocationController _locationcontroller;
        private DeathController _deathController;
        private EditController _editController;
        private OptionsController _optionsController;
        private TwitchTokenController _twitchTokenController;
        private HotkeyController _hotkeyController;
        private MainForm? _mainForm;
        private StreamTimeController _streamTimeController;
        private TextController _textController;
        private ExportController _exportController;

        public MainController(GameController gameController, LocationController locationController,
            DeathController deathController, EditController editController, OptionsController optionsController, 
            StreamTimeController streamTimeController, TwitchTokenController tokenController, TextController textController, ExportController exportController) 
        {
            this._gameController = gameController;
            this._locationcontroller = locationController;
            this._deathController = deathController;
            this._editController = editController;
            this._optionsController = optionsController;
            this._twitchTokenController = tokenController;
            this._streamTimeController = streamTimeController;
            this._textController = textController;
            this._exportController = exportController;

            this._hotkeyController = new HotkeyController(this._optionsController, this, IncreaseHotkeyPressed, DecreaseHotkeyPressed, SwitchHotkeyPressed, QuickHotkeyPressed, FinishHotkeyPressed);
            _hotkeyController.LoadHotkeys();

        }

        public ExportController GetExportController()
        {
            return _exportController;
        }

        private void FinishHotkeyPressed()
        {
            if (_locationcontroller.GetActiveLocation() == null) return;
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

        private int GetIndexOfListWithItemName(List<DeathLocationModel> locations, string? name)
        {
            if(name == null) return -1;
            for (int i = 0; i < locations.Count; i++)
            {
                if (locations[i].Name == name)
                {
                    return i;
                }
            }
            return 0;
        }

        private void SwitchHotkeyPressed()
        {
            List<DeathLocationModel> locations = _locationcontroller.GetListOfLocations();
            locations = locations.Where(x => x.Finish == false).ToList();
            int locLength = locations.Count;
            if (locLength == 0) return;
            int index = GetIndexOfListWithItemName(locations, _locationcontroller.GetActiveLocation()?.Name);
            if (index == -1) return;
            if (index == locLength -1)
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
            if (_gameController.GetActiveGame() == null) return;
            DeathLocationModel? model = _locationcontroller.GetActiveLocation();
            if (model == null) return;
            if (_deathController.GetDeaths(model.LocationId) == 0) return;
            _deathController.RemoveDeath();
        }

        public string GameChanged(string? gameName)
        {
            if (gameName == null) return "";
            _gameController.SetActiveGame(gameName);
            return _gameController.GetPrefix();
        }

        public GameController GetGameController()
        {
            return _gameController;
        }

        public int GetAllDeaths()
        {
            return this._gameController.GetAllDeaths();
        }

        internal EditController GetEditController()
        {
            return _editController;
        }

        internal List<string> GetGameNames()
        {
            return _gameController.GetAllGameNames();
        }

        internal TwitchTokenController GetTwitchTokenController()
        {
            return _twitchTokenController;
        }

        internal LocationController GetLocationController()
        {
            return _locationcontroller;
        }

        internal int GetLocationDeaths()
        {
            if( _locationcontroller.GetActiveLocation() == null) return 0;
            return _locationcontroller.GetDeathsAtLocation();
        }

        internal List<string> GetLocationNames()
        {
            //if(_locationcontroller.GetActiveLocation() == null) return new List<string>();
            return _locationcontroller.GetListOfLocations().Select(x=> x.Name).ToList();
        }

        internal OptionsController GetOptionController()
        {
            return _optionsController;
        }

        internal void OptionsChangedAction()
        {
            _hotkeyController.UnregisterHotkeys();
            _hotkeyController.LoadHotkeys();
            _mainForm?.UpdateDeaths();
        }

        internal void IncreaseDeaths()
        {
            if (_gameController.GetActiveGame() == null) return;
            if (_locationcontroller.GetActiveLocation() == null) return;
            _deathController.AddDeath(_locationcontroller.GetActiveLocation()!.LocationId);
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
            if (locationName == null) return ;
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

        internal StreamTimeController GetStreamTimeController()
        {
            return _streamTimeController;
        }

        internal TextController GetTextController()
        {
            return _textController;
        }
    }
}
