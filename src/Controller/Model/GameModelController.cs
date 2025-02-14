using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Utility.ResultSets;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Controller.Model
{
    public class GameModelController
    {
        private SQLiteDBContext? _context;
        private Singleton _singleton = Singleton.GetInstance();


        public GameModelController()
        {
            _context = _singleton.GetValue("DBContext") as SQLiteDBContext;
        }



        #region Getter


        public ResultSet<GameStatsModel?> GetItem(int gameID)
        {
            GameStatsModel? game = _context?.GameStats.Find(gameID);

            if (game is null)
            {
                return new ResultSet<GameStatsModel?>(RESULT.FAILURE, null, "Game not found");
            }

            return new ResultSet<GameStatsModel?>(RESULT.SUCCESS, game);
        }

        public ResultSet<GameStatsModel?> GetItem(string name)
        {
            GameStatsModel? game = _context?.GameStats.Where(x=> x.GameName.Equals(name)).FirstOrDefault();

            if (game is null)
            {
                return new ResultSet<GameStatsModel?>(RESULT.FAILURE, null, "Game not found");
            }

            return new ResultSet<GameStatsModel?>(RESULT.SUCCESS, game);
        }

        public ResultSet<GameStatsModel?> GetItem(GameStatsModel filter)
        {
            return GetItem(filter.GameId);
        }

        public ResultSet<List<GameStatsModel>?> GetItems()
        {
            List<GameStatsModel>? games = _context?.GameStats.ToList();
            if (games is null || games.Count == 0)
            {
                return new ResultSet<List<GameStatsModel>?>(RESULT.FAILURE, null, "No games found");
            }
            return new ResultSet<List<GameStatsModel>?>(RESULT.SUCCESS, games);
        }

        #endregion

        #region Adder

        public ResultSet<GameStatsModel?> AddItem(GameStatsModel game)
        {
            _context?.GameStats.Add(game);
            _context?.SaveChanges();
            return new ResultSet<GameStatsModel?>(RESULT.SUCCESS, game);
        }

        #endregion

        #region Remover

        public ResultSet<int> RemoveItem(int gameID)
        {
            ResultSet<GameStatsModel?> model = GetItem(gameID);
            if (model.GetResult() == RESULT.FAILURE)
            {
                return new ResultSet<int>(RESULT.FAILURE, -1, "Game not found");
            }
            return RemoveItem(model.GetData()!);
        }

        public ResultSet<int> RemoveItem(GameStatsModel gameFilter)
        {

            if (GetItem(gameFilter).GetResult() == RESULT.FAILURE)
            {
                return new ResultSet<int>(RESULT.FAILURE, -1, "Game not found");
            }
            _context?.GameStats.Remove(gameFilter);
            _context?.SaveChanges();
            return new ResultSet<int>(RESULT.SUCCESS, 0);
        }

        #endregion

        #region Updater

        public ResultSet<GameStatsModel?> UpdateItem(GameStatsModel gameFilter)
        {
            RESULT result = GetItem(gameFilter).GetResult();
            if (result == RESULT.FAILURE)
            {
                return new ResultSet<GameStatsModel?>(RESULT.FAILURE, null, "Death not found");
            }
            _context?.GameStats.Update(gameFilter);
            _context?.SaveChanges();
            return new ResultSet<GameStatsModel?>(RESULT.SUCCESS, gameFilter);
        }

        #endregion
    }
}
