using DeathCounterHotkey.Database.Models;
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
        private HotkeyController _hotkeyController;
        private MainForm _mainForm;

        public MainController(GameController gameController, LocationController locationController,
            DeathController deathController, EditController editController, OptionsController optionsController, StreamTimeController streamTimeController) 
        {
            this._gameController = gameController;
            this._locationcontroller = locationController;
            this._deathController = deathController;
            this._editController = editController;
            this._optionsController = optionsController;

            this._hotkeyController = new HotkeyController(this._optionsController, this, IncreaseHotkeyPressed, DecreaseHotkeyPressed, SwitchHotkeyPressed, QuickHotkeyPressed);
            _hotkeyController.LoadHotkeys();

        }

        private void IncreaseHotkeyPressed(object sender, KeyPressedEventArgs e)
        {
            IncreaseDeaths();
            _mainForm.UpdateDeaths();
        }

        private void DecreaseHotkeyPressed(object sender, KeyPressedEventArgs e)
        {
            DecreaseDeaths();
            _mainForm.UpdateDeaths();
        }

        private void SwitchHotkeyPressed(object sender, KeyPressedEventArgs e)
        {
            List<DeathLocationModel> locations = _locationcontroller.GetListOfLocations();
            int locLength = locations.Count;
            if (locLength == 0) return;
            int index = _mainForm.GetLocationIndex();
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
            _mainForm.SetLocation(newLocName);
            _mainForm.UpdateDeaths();
        }

        private void QuickHotkeyPressed(object sender, KeyPressedEventArgs e)
        {
            QuickAddLocation();
        }

        public void DecreaseDeaths()
        {
            if (_gameController.GetActiveGame() == null) return;
            if (_locationcontroller.GetActiveLocation() == null) return;
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
        }

        internal void IncreaseDeaths()
        {
            if (_gameController.GetActiveGame() == null) return;
            if (_locationcontroller.GetActiveLocation() == null) return;
            _deathController.AddDeath(_locationcontroller.GetActiveLocation().LocationId);
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
            string locationName = "Location" + GetTimestamp(DateTime.Now);
            _locationcontroller.AddLocation(locationName);
            _mainForm.UpdateLocationList();
            _mainForm.SetLocation(locationName);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        internal void SetForm(MainForm mainForm)
        {
            this._mainForm = mainForm;
        }
    }
}
