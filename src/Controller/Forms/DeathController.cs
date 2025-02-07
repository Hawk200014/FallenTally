using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Utility.Singletons;

namespace DeathCounterHotkey.Controller.Forms
{
    public class DeathController : ISingleton
    {
        private SQLiteDBContext? _context;
        private readonly Singleton _singleton = Singleton.GetInstance();
        public DeathController() 
        {
            _context = _singleton.GetValue(SQLiteDBContext.GetSingletonName()) as SQLiteDBContext;
        }



        public void AddDeath(int locationId, TimerController streamcontroller, TimerController recordingController)
        {
            DeathModel deathModel = new DeathModel();
            deathModel.TimeStamp = DateTime.Now;
            deathModel.StreamTime = (int)streamcontroller.GetTime();
            deathModel.RecordingTime = (int)recordingController.GetTime();
            deathModel.LocationId = locationId;

            _context.Deaths.Add(deathModel);
            _context.SaveChanges();
        }

        public void RemoveDeath()
        {
            DeathModel? latest = _context.Deaths.OrderByDescending(x => x.DeathId).FirstOrDefault();
            if (latest is null) return;
            _context.Deaths.Remove(latest);
            _context.SaveChanges();
        }

        public int GetDeaths(int locId)
        {
            return _context.Locations.Find(locId).Deaths.Count();
            //return _context.Deaths.Where(x => x.LocationId == locId).Count();
        }

        public static string GetSingletonName()
        {
            return "DeathController";
        }
    }
}
