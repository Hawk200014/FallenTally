using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class AddGameController
    {
        private SQLiteDBContext _context;

        public AddGameController(SQLiteDBContext context) 
        {
            this._context = context;
        }

        public bool AddGame(string gamename, string prefix)
        {
            if (string.IsNullOrEmpty(gamename) || string.IsNullOrEmpty(prefix)) return false;
            if (_context.GameStats.Where(x => x.GameName == gamename).Count() > 0) return false;
            _context.GameStats.Add(new GameStatsModel(gamename, prefix));
            _context.SaveChanges();
            return true;
            
        }

        internal GameStatsModel GetActiveGame()
        {
            throw new NotImplementedException();
        }
    }
}
