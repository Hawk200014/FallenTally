using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class GameController
    {
        private SQLiteDBContext _context;

        private GameStatsModel? _activeGame;

        public GameController(SQLiteDBContext context) 
        {
            this._context = context;
        }

        public bool AddGame(string gamename, string prefix)
        {
            if (string.IsNullOrEmpty(gamename) || string.IsNullOrEmpty(prefix)) return false;
            if (IsDupeName(gamename)) return false;
            _context.GameStats.Add(new GameStatsModel(gamename, prefix));
            _context.SaveChanges();
            return true;
            
        }

        public bool EditName(string editText)
        {
            if (IsDupeName(editText)) return false;
            GameStatsModel? gameModel = GetActiveGame();
            if (gameModel == null) return false;
            gameModel.GameName = editText;
            _context.SaveChanges();
            return true;
        }

        public List<GameStatsModel> GetGameStats()
        {
            return _context.GameStats.ToList();
        }

        public void SetActiveGame(string gameName)
        {
            this._activeGame = _context.GameStats.Where(x => x.GameName == gameName).FirstOrDefault();
        }

        public GameStatsModel? GetActiveGame()
        {
            return this._activeGame
        }

        public bool IsDupeName(string editText)
        {
            return _context.GameStats.Where(x=>x.GameName == editText).Any();
        }
    }
}
