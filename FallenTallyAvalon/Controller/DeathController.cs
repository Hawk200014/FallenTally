using FallenTally.Controller;
using FallenTally.Controller.Timers;
using FallenTally.Database;
using FallenTally.Database.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Controller
{
    public class DeathController
    {
        private SQLiteDBContext _context;
        public DeathController(SQLiteDBContext context) 
        {
            _context = context;
        }



        public void AddDeath(DeathLocationModel? locationModel, TimerController? streamcontroller = null, TimerController? recordingController = null)
        {
            if (locationModel == null) return;
            DeathModel deathModel = new DeathModel();
            deathModel.TimeStamp = DateTime.Now;
            deathModel.StreamTime = streamcontroller?.GetTime() ?? 0;
            deathModel.RecordingTime = recordingController?.GetTime() ?? 0;
            deathModel.LocationId = locationModel.LocationId;

            _context.Deaths.Add(deathModel);
            _context.SaveChanges();
        }

        public void RemoveDeath()
        {
            DeathModel? latest = _context.Deaths.OrderByDescending(x => x.DeathId).FirstOrDefault();
            if (latest == null) return;
            _context.Deaths.Remove(latest);
            _context.SaveChanges();
        }

        public int GetDeaths(DeathLocationModel? loc)
        {
            if (loc == null) return 0;
            return _context.Deaths.Where(x => x.LocationId == loc.LocationId).Count();
        }

        #region Filter

        private IQueryable<DeathModel> _filterQuery;

        public IQueryable<DeathModel> InitFilter()
        {
            _filterQuery = _context.Deaths;
            return _filterQuery;
        }

        public IQueryable<DeathModel> Filter(GameStatsModel game)
        {
            // Join Deaths with Locations, filter by game.GameId, select deaths for those locations
            _filterQuery = from death in _filterQuery
                           join location in _context.Locations
                           on death.LocationId equals location.LocationId
                           where location.GameID == game.GameId
                           select death;
            return _filterQuery;
        }

        public IQueryable<DeathModel> Filter(DeathLocationModel location)
        {
            _filterQuery = _filterQuery.Where(x => x.LocationId == location.LocationId);
            return _filterQuery;
        }

        public IQueryable<DeathModel> Filter(DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            if (fromDate != null)
            {
                // Convert DateOnly? to DateTime for comparison
                DateTime fromDateTime = fromDate.Value.ToDateTime(TimeOnly.MinValue);
                _filterQuery = _filterQuery.Where(x => x.TimeStamp >= fromDateTime);
            }

            if (toDate != null)
            {
                // Convert DateOnly? to DateTime for comparison
                DateTime toDateTime = toDate.Value.ToDateTime(TimeOnly.MaxValue);
                _filterQuery = _filterQuery.Where(x => x.TimeStamp <= toDateTime);
            }

            return _filterQuery;
        }

        public IQueryable<DeathModel> GetFilter()
        {
            return _filterQuery;
        }

        #endregion
    }
}
