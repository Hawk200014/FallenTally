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
            _context.GameStats.Add(new GameStatsModel() {
                GameName = gamename,
                Prefix = prefix
            }); 
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
            return this._activeGame;
        }

        public GameStatsModel? GetGame(string gameName)
        {
            return _context.GameStats.Where(x => x.GameName.Equals(gameName)).FirstOrDefault();
        }

        public bool IsDupeName(string editText)
        {
            return _context.GameStats.Where(x=>x.GameName == editText).Any();
        }

        internal string GetPrefix()
        {
            if (_activeGame == null) return "";
            return _activeGame.Prefix;
        }

        internal int GetAllDeaths()
        {
            if(_activeGame == null) return 0;
            int deaths = 0;
            List<DeathLocationModel> list = _context.Locations.Where(x => x.GameID == _activeGame.GameId).ToList();
            foreach(var location in list)
            {
                deaths += _context.Deaths.Where(x => x.LocationId == location.LocationId).Count();
            }
            return deaths;
        }

        internal List<string> GetAllGameNames()
        {
            return _context.GameStats.Select(x => x.GameName).ToList();
        }

        internal void RemoveGame()
        {
            if(_activeGame == null) return;
            _context.GameStats.Remove(_activeGame);
            _activeGame = null;
            _context.SaveChanges();
        }
    }
}
