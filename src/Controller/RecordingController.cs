﻿using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class RecordingController
    {
        public enum RecordingType
        {
            recording,
            stream
        }
        private SQLiteDBContext _context;

        public RecordingController(SQLiteDBContext context)
        {
            this._context = context;
        }

        public void AddRecording(RecordingType type)
        {
            if (_context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type == type.ToString()).Count() == 0)
            {
                _context.Recordings.Add(new RecordingModel
                {
                    SessionCount = 1,
                    SessionDate = DateOnly.FromDateTime(DateTime.Now),
                    Type = type.ToString()
                });
            }
            else
            {
                var recording = _context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type == type.ToString()).First();
                recording.SessionCount++;
                _context.Recordings.Update(recording);
            }
            _context.SaveChanges();
        }

        public int GetRecordingNumber(RecordingType type)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var recording = _context.Recordings
                .Where(x => x.SessionDate == today && x.Type == type.ToString())
                .FirstOrDefault();

            return recording?.SessionCount ?? 0;
        }
    }
}
