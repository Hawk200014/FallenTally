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
    internal class MakerModelController
    {
        private SQLiteDBContext? _context;
        private Singleton _singleton = Singleton.GetInstance();


        public MakerModelController()
        {
            _context = _singleton.GetValue("DBContext") as SQLiteDBContext;
        }



        #region Getter


        public ResultSet<MarkerModel?> GetItem(int markerId)
        {
            MarkerModel? marker = _context?.Markers.Find(markerId);

            if (marker is null)
            {
                return new ResultSet<MarkerModel?>(RESULT.FAILURE, null, "Marker not found");
            }

            return new ResultSet<MarkerModel?>(RESULT.SUCCESS, marker);
        }

        public ResultSet<List<MarkerModel>?> GetItems(DateOnly date)
        {
            List<MarkerModel>? markers = _context?.Markers.Where(x => DateOnly.FromDateTime(x.TimeStamp) == date).ToList();

            if (markers is null)
            {
                return new ResultSet<List<MarkerModel>?>(RESULT.FAILURE, null, "Marker not found");
            }

            return new ResultSet<List<MarkerModel>?>(RESULT.SUCCESS, markers);
        }

        public ResultSet<List<MarkerModel>?> GetItems(DateTime date)
        {
            return GetItems(DateOnly.FromDateTime(date));
        }

        public ResultSet<MarkerModel?> GetItem(MarkerModel filter)
        {
            return GetItem(filter.MarkerId);
        }

        public ResultSet<List<MarkerModel>?> GetItems()
        {
            List<MarkerModel>? locations = _context?.Locations.ToList();
            if (locations is null || locations.Count == 0)
            {
                return new ResultSet<List<MarkerModel>?>(RESULT.FAILURE, null, "No locations found");
            }
            return new ResultSet<List<MarkerModel>?>(RESULT.SUCCESS, games);
        }

        public ResultSet<List<MarkerModel>?> GetItems(int gameID)
        {
            List<MarkerModel>? locations = _context?.Locations.Where(x => x.GameID == gameID).ToList();
            if (locations is null || locations.Count == 0)
            {
                return new ResultSet<List<MarkerModel>?>(RESULT.FAILURE, null, "No locations found");
            }
            return new ResultSet<List<MarkerModel>?>(RESULT.SUCCESS, locations);
        }

        public ResultSet<List<MarkerModel>?> GetItems(GameStatsModel game)
        {
            List<MarkerModel>? locations = _context?.Locations.Where(x => x.GameID == game.GameId).ToList();
            if (locations is null || locations.Count == 0)
            {
                return new ResultSet<List<MarkerModel>?>(RESULT.FAILURE, null, "No locations found");
            }
            return new ResultSet<List<MarkerModel>?>(RESULT.SUCCESS, locations);
        }

        #endregion

        #region Adder

        public ResultSet<MarkerModel?> AddItem(DeathLocationModel location)
        {
            _context?.Locations.Add(location);
            _context?.SaveChanges();
            return new ResultSet<MarkerModel?>(RESULT.SUCCESS, location);
        }

        #endregion

        #region Remover

        public ResultSet<int> RemoveItem(int locationID)
        {
            ResultSet<MarkerModel?> model = GetItem(locationID);
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

        public ResultSet<MarkerModel?> UpdateItem(DeathLocationModel locationFilter)
        {
            RESULT result = GetItem(locationFilter).GetResult();
            if (result == RESULT.FAILURE)
            {
                return new ResultSet<MarkerModel?>(RESULT.FAILURE, null, "Death not found");
            }
            _context?.Locations.Update(locationFilter);
            _context?.SaveChanges();
            return new ResultSet<MarkerModel?>(RESULT.SUCCESS, locationFilter);
        }

        #endregion

    }
}
