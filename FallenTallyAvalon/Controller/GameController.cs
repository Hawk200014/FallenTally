using FallenTally.Database;
using FallenTally.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Controller
{
    public class GameController
    {
        private SQLiteDBContext _context;

        public GameController(SQLiteDBContext context) 
        {
            _context = context;
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

        public bool EditName(GameStatsModel oldGame, GameStatsModel newGame)
        {
            if (IsDupeName(newGame.GameName)) return false;
            GameStatsModel? gameModel = _context.GameStats.Where(x => x.GameName.Equals(oldGame.GameName)).FirstOrDefault();
            if (gameModel == null) return false;
            gameModel.GameName = newGame.GameName;
            gameModel.Prefix = newGame.Prefix;
            _context.SaveChanges();
            return true;
        }

        public List<GameStatsModel> GetGameStats()
        {
                return _context.GameStats.ToList();

        }





        public GameStatsModel? GetGame(string gameName)
        {
            return _context.GameStats.Where(x => x.GameName.Equals(gameName)).FirstOrDefault();
        }

        public bool IsDupeName(string editText)
        {
            return _context.GameStats.Where(x=>x.GameName == editText).Any();
        }


        public int GetAllDeaths(GameStatsModel gameStatsModel)
        {

            int deaths = 0;
            List<DeathLocationModel> list = _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ToList();
            foreach(var location in list)
            {
                deaths += _context.Deaths.Where(x => x.LocationId == location.LocationId).Count();
            }
            return deaths;
        }

        public List<string> GetAllGameNames()
        {
            return _context.GameStats.Select(x => x.GameName).ToList();
        }

        public void RemoveGame(GameStatsModel gameStatsModel)
        {
            if(gameStatsModel == null) return;
            GameStatsModel? game = _context.GameStats.FirstOrDefault(x => x.GameName == gameStatsModel.GameName);
            if(game == null) return;
            _context.GameStats.Remove(game);
            _context.SaveChanges();
        }
    }
}
