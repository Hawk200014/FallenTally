using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
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
            _context.Locations.Add(new DeathLocationModel(locationName));
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
            return _context.GameStats.Where(x => x.GameId == _gameController.GetActiveGame().GameId).FirstOrDefault().deathLocations;
        }

        public DeathLocationModel? GetActiveLocation()
        {
            return _activeDeathLocation;
        }


        internal bool IsDupeName(string editText)
        {
            GameStatsModel? activeGame = _gameController.GetActiveGame();
            if(activeGame == null) return true;
            return activeGame.deathLocations.Where(x => x.Name.Equals(editText)).Any();
        }
    }
}
