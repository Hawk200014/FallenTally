using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.Controller
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
    }
}
