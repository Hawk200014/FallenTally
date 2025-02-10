using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Controller.Model;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    /// <summary>
    /// Controller for managing game-related operations.
    /// </summary>
    public class GameController : ISingleton
    {
        private readonly SQLiteDBContext? _context;
        private GameStatsModel? _activeGame;
        private readonly Singleton _singleton = Singleton.GetInstance();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GameController()
        {
            _context = _singleton.GetValue(SQLiteDBContext.GetSingletonName()) as SQLiteDBContext;
        }

        /// <summary>
        /// Adds a new game to the database.
        /// </summary>
        /// <param name="gamename">The name of the game.</param>
        /// <param name="prefix">The prefix for the game.</param>
        /// <returns>True if the game was added successfully, otherwise false.</returns>
        public bool AddGame(string gamename, string prefix)
        {
            if (string.IsNullOrEmpty(gamename) || string.IsNullOrEmpty(prefix) || IsDupeName(gamename)) return false;

            _context.GameStats.Add(new GameStatsModel
            {
                GameName = gamename,
                Prefix = prefix
            });
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Edits the name of the active game.
        /// </summary>
        /// <param name="editText">The new name for the game.</param>
        /// <returns>True if the name was edited successfully, otherwise false.</returns>
        public bool EditName(string editText)
        {
            if (IsDupeName(editText) || _activeGame == null) return false;

            _activeGame.GameName = editText;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Gets a list of all game statistics.
        /// </summary>
        /// <returns>A list of <see cref="GameStatsModel"/>.</returns>
        public List<GameStatsModel> GetGameStats() => _context.GameStats.ToList();

        /// <summary>
        /// Sets the active game based on the game name.
        /// </summary>
        /// <param name="gameName">The name of the game to set as active.</param>
        public void SetActiveGame(string gameName)
        {
            _activeGame = _context.GameStats.FirstOrDefault(x => x.GameName == gameName);
        }

        /// <summary>
        /// Gets the active game.
        /// </summary>
        /// <returns>The active <see cref="GameStatsModel"/>.</returns>
        public GameStatsModel? GetActiveGame() => _activeGame;

        /// <summary>
        /// Gets a game based on the game name.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <returns>The <see cref="GameStatsModel"/> if found, otherwise null.</returns>
        public GameStatsModel? GetGame(string gameName) => _context.GameStats.FirstOrDefault(x => x.GameName == gameName);

        /// <summary>
        /// Checks if a game name is a duplicate.
        /// </summary>
        /// <param name="editText">The game name to check.</param>
        /// <returns>True if the name is a duplicate, otherwise false.</returns>
        public bool IsDupeName(string editText) => _context.GameStats.Any(x => x.GameName == editText);

        /// <summary>
        /// Gets the prefix of the active game.
        /// </summary>
        /// <returns>The prefix of the active game.</returns>
        internal string GetPrefix() => _activeGame?.Prefix ?? string.Empty;

        /// <summary>
        /// Gets the total number of deaths for the active game.
        /// </summary>
        /// <returns>The total number of deaths.</returns>
        internal int GetAllDeaths()
        {
            if (_activeGame == null) return 0;

            return _context.Locations
                .Where(x => x.GameID == _activeGame.GameId)
                .SelectMany(x => x.Deaths)
                .Count();
        }

        /// <summary>
        /// Gets a list of all game names.
        /// </summary>
        /// <returns>A list of game names.</returns>
        internal List<string> GetAllGameNames() => _context.GameStats.Select(x => x.GameName).ToList();

        /// <summary>
        /// Removes the active game from the database.
        /// </summary>
        internal void RemoveGame()
        {
            if (_activeGame == null) return;

            _context.GameStats.Remove(_activeGame);
            _activeGame = null;
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the singleton name for the <see cref="GameController"/>.
        /// </summary>
        /// <returns>The singleton name.</returns>
        public static string GetSingletonName() => "GameController";
    }
}
