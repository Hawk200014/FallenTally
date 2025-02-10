using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Resources;
using FallenTally.Controller.Model;
using FallenTally.Utility.Singletons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    /// <summary>
    /// Controller for managing location-related operations.
    /// </summary>
    public class LocationController : ISingleton
    {
        private readonly GameController? _gameController;
        private readonly SQLiteDBContext? _context;
        private DeathLocationModel? _activeDeathLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        public LocationController()
        {
            _gameController = Singleton.GetInstance().GetValue(GameController.GetSingletonName()) as GameController;
            _context = Singleton.GetInstance().GetValue(SQLiteDBContext.GetSingletonName()) as SQLiteDBContext;
        }

        /// <summary>
        /// Adds a new location to the database.
        /// </summary>
        /// <param name="locationName">The name of the location.</param>
        /// <returns>True if the location was added successfully, otherwise false.</returns>
        public bool AddLocation(string locationName)
        {
            if (string.IsNullOrEmpty(locationName)) return false;
            var gameStatsModel = _gameController?.GetActiveGame();
            if (gameStatsModel == null || IsDupeName(locationName)) return false;

            _context.Locations.Add(new DeathLocationModel
            {
                Name = locationName,
                GameID = gameStatsModel.GameId
            });
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Edits the name of the active location.
        /// </summary>
        /// <param name="editText">The new name for the location.</param>
        /// <returns>True if the name was edited successfully, otherwise false.</returns>
        public bool EditName(string editText)
        {
            if (IsDupeName(editText) || _activeDeathLocation == null) return false;

            _activeDeathLocation.Name = editText;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Gets a list of all locations for the active game.
        /// </summary>
        /// <returns>A list of <see cref="DeathLocationModel"/>.</returns>
        public List<DeathLocationModel> GetListOfLocations()
        {
            var activeGame = _gameController?.GetActiveGame();
            return activeGame == null ? new List<DeathLocationModel>() : _context.Locations.Where(x => x.GameID == activeGame.GameId).ToList();
        }

        /// <summary>
        /// Gets the active location.
        /// </summary>
        /// <returns>The active <see cref="DeathLocationModel"/>.</returns>
        public DeathLocationModel? GetActiveLocation() => _activeDeathLocation;

        /// <summary>
        /// Sets the active location based on the location name.
        /// </summary>
        /// <param name="name">The name of the location to set as active.</param>
        public void SetActiveLocation(string name)
        {
            var activeGame = _gameController?.GetActiveGame();
            if (activeGame == null) return;

            _activeDeathLocation = _context.Locations.FirstOrDefault(x => x.Name == name && x.GameID == activeGame.GameId);
        }

        /// <summary>
        /// Checks if a location name is a duplicate.
        /// </summary>
        /// <param name="editText">The location name to check.</param>
        /// <returns>True if the name is a duplicate, otherwise false.</returns>
        internal bool IsDupeName(string editText)
        {
            var activeGame = _gameController?.GetActiveGame();
            return activeGame != null && _context.Locations.Any(x => x.Name == editText && x.GameID == activeGame.GameId);
        }

        /// <summary>
        /// Removes the active location from the database.
        /// </summary>
        /// <returns>True if the location was removed successfully, otherwise false.</returns>
        internal bool RemoveLocation()
        {
            if (_activeDeathLocation == null || _activeDeathLocation.Name == GLOBALVARS.DEFAULT_LOCATION) return false;

            _context.Deaths.Where(x => x.LocationId == _activeDeathLocation.LocationId).ExecuteDelete();
            _context.Locations.Remove(_activeDeathLocation);
            _activeDeathLocation = null;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Gets the count of deaths at the active location.
        /// </summary>
        /// <returns>The count of deaths at the active location.</returns>
        internal int GetDeathsAtLocation() => _activeDeathLocation == null ? 0 : _context.Deaths.Count(x => x.LocationId == _activeDeathLocation.LocationId);

        /// <summary>
        /// Removes all locations for a given game.
        /// </summary>
        /// <param name="gameStatsModel">The game statistics model.</param>
        internal void RemoveAllLocations(GameStatsModel? gameStatsModel)
        {
            if (gameStatsModel == null) return;

            var locations = _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ToList();
            foreach (var location in locations)
            {
                _context.Deaths.Where(x => x.LocationId == location.LocationId).ExecuteDelete();
            }
            _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ExecuteDelete();
            _activeDeathLocation = null;
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the finish state of a location based on its old value.
        /// </summary>
        /// <param name="oldValue">The old value of the location.</param>
        /// <returns>True if the location is finished, otherwise false.</returns>
        internal bool GetFinishState(string oldValue)
        {
            var activeGame = _gameController?.GetActiveGame();
            var location = activeGame == null ? null : _context.Locations.FirstOrDefault(x => x.Name == oldValue && x.GameID == activeGame.GameId);
            return location?.Finish ?? false;
        }

        /// <summary>
        /// Sets the finish state of the active location.
        /// </summary>
        /// <param name="finished">The finish state to set.</param>
        internal void SetFinish(bool? finished)
        {
            if (_activeDeathLocation == null || finished == null) return;

            _activeDeathLocation.Finish = finished.Value;
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the singleton name for the <see cref="LocationController"/>.
        /// </summary>
        /// <returns>The singleton name.</returns>
        public static string GetSingletonName() => "LocationController";
    }
}
