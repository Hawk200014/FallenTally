using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class DeathController
    {
        private SQLiteDBContext _context;
        private StreamTimeController _streamTimeController;

        public DeathController(SQLiteDBContext context, StreamTimeController streamTimeController) 
        {
            this._context = context;
            this._streamTimeController = streamTimeController;
        }



        public void AddDeath(int locationId)
        {
            DeathModel deathModel = new DeathModel();
            deathModel.TimeStamp = DateTime.Now;
            deathModel.StreamTime = _streamTimeController.GetTime();
            deathModel.LocationId = locationId;

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

        public int GetDeaths(int locId)
        {
            return _context.Deaths.Where(x => x.LocationId == locId).Count();
        }
    }
}
