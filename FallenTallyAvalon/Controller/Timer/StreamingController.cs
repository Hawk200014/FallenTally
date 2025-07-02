using FallenTally.Database;
using FallenTally.Database.Models;
using System;
using System.Linq;

namespace FallenTally.Controller.Timers
{
    public class StreamingController : TimerController
    {
        private SQLiteDBContext _context;

        public StreamingController(SQLiteDBContext context)
        {
            _context = context;
        }

        public void AddStream()
        {
            if (_context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type.Equals("stream")).Count() == 0)
            {
                _context.Recordings.Add(new RecordingModel
                {
                    SessionCount = 1,
                    SessionDate = DateOnly.FromDateTime(DateTime.Now),
                    Type = "stream"
                });
            }
            else
            {
                var recording = _context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type.Equals("stream")).First();
                recording.SessionCount++;
                _context.Recordings.Update(recording);
            }
            _context.SaveChanges();
        }

        public int GetStreamingNumber()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var recording = _context.Recordings
                .Where(x => x.SessionDate == today && x.Type.Equals("stream"))
                .FirstOrDefault();

            return recording?.SessionCount ?? 0;
        }
    }
}
