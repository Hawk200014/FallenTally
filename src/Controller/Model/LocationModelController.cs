using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Database;
using FallenTally.Utility.ResultSets;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Helix;

namespace FallenTally.Controller.Model
{
    public class LocationModelController
    {
        private SQLiteDBContext? _context;
        private Singleton _singleton = Singleton.GetInstance();


        public LocationModelController()
        {
            _context = _singleton.GetValue("DBContext") as SQLiteDBContext;
        }



        #region Getter


        public ResultSet<DeathLocationModel?> GetItem(int locationID)
        {
            DeathLocationModel? location = _context?.Locations.Find(locationID);

            if (location is null)
            {
                return new ResultSet<DeathLocationModel?>(RESULT.FAILURE, null, "Location not found");
            }

            return new ResultSet<DeathLocationModel?>(RESULT.SUCCESS, location);
        }

        public ResultSet<DeathLocationModel?> GetItem(string name)
        {
            DeathLocationModel? location = _context?.Locations.Where(x => x.Name.Equals(name)).FirstOrDefault();

            if (location is null)
            {
                return new ResultSet<DeathLocationModel?>(RESULT.FAILURE, null, "Location not found");
            }

            return new ResultSet<DeathLocationModel?>(RESULT.SUCCESS, location);
        }

        public ResultSet<DeathLocationModel?> GetItem(DeathLocationModel filter)
        {
            return GetItem(filter.LocationId);
        }

        public ResultSet<List<DeathLocationModel>?> GetItems()
        {
            List<DeathLocationModel>? locations = _context?.Locations.ToList();
            if (locations is null || locations.Count == 0)
            {
                return new ResultSet<List<DeathLocationModel>?>(RESULT.FAILURE, null, "No locations found");
            }
            return new ResultSet<List<DeathLocationModel>?>(RESULT.SUCCESS, locations);
        }

        public ResultSet<List<DeathLocationModel>?> GetItems(int gameID)
        {
            List<DeathLocationModel>? locations = _context?.Locations.Where(x => x.GameID == gameID).ToList();
            if (locations is null || locations.Count == 0)
            {
                return new ResultSet<List<DeathLocationModel>?>(RESULT.FAILURE, null, "No locations found");
            }
            return new ResultSet<List<DeathLocationModel>?>(RESULT.SUCCESS, locations);
        }

        public ResultSet<List<DeathLocationModel>?> GetItems(GameStatsModel game)
        {
            List<DeathLocationModel>? locations = _context?.Locations.Where(x => x.GameID == game.GameId).ToList();
            if (locations is null || locations.Count == 0)
            {
                return new ResultSet<List<DeathLocationModel>?>(RESULT.FAILURE, null, "No locations found");
            }
            return new ResultSet<List<DeathLocationModel>?>(RESULT.SUCCESS, locations);
        }

        #endregion

        #region Adder

        public ResultSet<DeathLocationModel?> AddItem(DeathLocationModel location)
        {
            _context?.Locations.Add(location);
            _context?.SaveChanges();
            return new ResultSet<DeathLocationModel?>(RESULT.SUCCESS, location);
        }

        #endregion

        #region Remover

        public ResultSet<int> RemoveItem(int locationID)
        {
            ResultSet<DeathLocationModel?> model = GetItem(locationID);
            if (model.GetResult() == RESULT.FAILURE)
            {
                return new ResultSet<int>(RESULT.FAILURE, -1, "Location not found");
            }
            return RemoveItem(model.GetData()!);
        }

        public ResultSet<int> RemoveItem(DeathLocationModel locationFilter)
        {

            if (GetItem(locationFilter).GetResult() == RESULT.FAILURE)
            {
                return new ResultSet<int>(RESULT.FAILURE, -1, "Location not found");
            }
            _context?.Locations.Remove(locationFilter);
            _context?.SaveChanges();
            return new ResultSet<int>(RESULT.SUCCESS, 0);
        }

        #endregion

        #region Updater

        public ResultSet<DeathLocationModel?> UpdateItem(DeathLocationModel locationFilter)
        {
            RESULT result = GetItem(locationFilter).GetResult();
            if (result == RESULT.FAILURE)
            {
                return new ResultSet<DeathLocationModel?>(RESULT.FAILURE, null, "Death not found");
            }
            _context?.Locations.Update(locationFilter);
            _context?.SaveChanges();
            return new ResultSet<DeathLocationModel?>(RESULT.SUCCESS, locationFilter);
        }

        #endregion

    }
}
