using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class LocationController
    {
        private GameController _gameController;
        private SQLiteDBContext _context;

        private DeathLocationModel? _activeDeathLocation;

        public LocationController(GameController gameController, SQLiteDBContext context)
        {
            this._gameController = gameController;
            this._context = context;
        }

        public bool AddLocation(string locationName)
        {
            if (string.IsNullOrEmpty(locationName)) return false;
            GameStatsModel? gameStatsModel = _gameController.GetActiveGame();
            if (gameStatsModel == null) return false;
            if (IsDupeName(locationName)) return false;
            _context.Locations.Add(new DeathLocationModel()
            {
                Name = locationName,
                GameID = gameStatsModel.GameId
            }) ;
            _context.SaveChanges();
            return true;
        }

        public bool EditName(string editText)
        {
            if (IsDupeName(editText)) return false;
            DeathLocationModel? locationModel = GetActiveLocation();
            if (locationModel == null) return false;
            locationModel.Name = editText;
            _context.SaveChanges();
            return true;
        }

        public List<DeathLocationModel> GetListOfLocations()
        {
            if(_gameController.GetActiveGame() == null) return new List<DeathLocationModel>();
            GameStatsModel? model = _gameController.GetActiveGame();
            if (model == null) return new List<DeathLocationModel>();
            return _context.Locations.Where(x => x.GameID == model.GameId).ToList();
        }

        public DeathLocationModel? GetActiveLocation()
        {
            return _activeDeathLocation;
        }

        public void SetActiveLocation(string name)
        {
            GameStatsModel? activeGame = _gameController.GetActiveGame();
            if (activeGame == null) return;
            this._activeDeathLocation = _context.Locations.Where(x => x.Name.Equals(name) && x.GameID == activeGame.GameId).FirstOrDefault();
        }

        internal bool IsDupeName(string editText)
        {
            GameStatsModel? activeGame = _gameController.GetActiveGame();
            if (activeGame == null) return true;
            return _context.Locations.Where(x => x.Name.Equals(editText) && x.GameID == activeGame.GameId).Any();
        }

        internal bool RemoveLocation()
        {
            if (_activeDeathLocation == null) return false;
            if (_activeDeathLocation.Name.Equals(GLOBALVARS.DEFAULT_LOCATION)) return false;
            _context.Deaths.Where(x => x.LocationId == _activeDeathLocation.LocationId).ExecuteDelete();
            _context.Locations.Remove(_activeDeathLocation);
            _activeDeathLocation = null;
            _context.SaveChanges();
            return true;
        }

        internal int GetDeathsAtLocation()
        {
            if(_activeDeathLocation == null) return 0;
            return _context.Deaths.Where(x => x.LocationId == _activeDeathLocation.LocationId).Count();
        }

        internal void RemoveAllLocations(GameStatsModel? gameStatsModel)
        {
            if(gameStatsModel == null) return;
            
            List<DeathLocationModel> locations = _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ToList();
            foreach(DeathLocationModel location in locations)
            {
                _context.Deaths.Where(x => x.LocationId == location.LocationId).ExecuteDelete();
            }
            _activeDeathLocation = null;
            _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ExecuteDelete();
            _context.SaveChanges();
        }
    }
}
