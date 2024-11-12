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
    public class AddLocationController
    {
        private AddGameController _gameController;
        private SQLiteDBContext _context;

        public AddLocationController(AddGameController gameController, SQLiteDBContext context)
        {
            this._gameController = gameController;
            this._context = context;
        }

        public bool AddLocation(string locationName)
        {
            if (string.IsNullOrEmpty(locationName)) return false;
            GameStatsModel gameStatsModel = _gameController.GetActiveGame();
            if (gameStatsModel.deathLocations.Where(x => x.Name == locationName).Count() > 0) return false;
            _context.Locations.Add(new DeathLocationModel(locationName));
            _context.SaveChanges();
            return true;
        }
    }
}
