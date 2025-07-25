﻿using FallenTally.Database;
using FallenTally.Database.Models;
using System;
using System.Linq;

namespace FallenTally.Controller.Timers
{
    public class RecordingController : TimerController
    {
        private SQLiteDBContext _context;

        public RecordingController(SQLiteDBContext context)
        {
            _context = context;
        }

        public void AddRecording()
        {
            if (_context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type.Equals("recording")).Count() == 0)
            {
                _context.Recordings.Add(new RecordingModel
                {
                    SessionCount = 1,
                    SessionDate = DateOnly.FromDateTime(DateTime.Now),
                    Type = "recording"
                });
            }
            else
            {
                var recording = _context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type.Equals("recording")).First();
                recording.SessionCount++;
                _context.Recordings.Update(recording);
            }
            _context.SaveChanges();
        }

        public int GetRecordingNumber()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var recording = _context.Recordings
                .Where(x => x.SessionDate == today && x.Type.Equals("recording"))
                .FirstOrDefault();

            return recording?.SessionCount ?? 0;
        }


    }
}
