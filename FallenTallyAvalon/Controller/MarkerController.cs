using FallenTally.Controller.Timers;
using FallenTally.Database;
using FallenTally.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallenTally.Controller
{
    public class MarkerController
    {
        private SQLiteDBContext _context;
        private RecordingController _recordingController;
        private StreamingController _streamingController;

        public enum MARKER
        {
            NORMAL,
            FUNNY,
            GAME,
            TALK,
            PAUSE
        }

        public MarkerController(SQLiteDBContext context , RecordingController recordingController, StreamingController streamingController) 
        { 
            this._context = context;
            this._recordingController = recordingController;
            this._streamingController = streamingController;
        }

        internal void SetMark(MARKER markerType, GameStatsModel? gameStatsModel)
        {
            string categorie = markerType.ToString();
            MarkerModel markerModel = new MarkerModel()
            {
                categorie = categorie,
                GameId = gameStatsModel?.GameId ?? -1,
                TimeStamp = DateTime.Now,
                RecordingTime = _recordingController.GetTime(),
                StreamTime = _streamingController.GetTime(),
                StreamSession = _streamingController.GetStreamingNumber(),
                RecordingSession = _recordingController.GetRecordingNumber()
            };
            _context.Markers.Add(markerModel);
            _context.SaveChanges();

        }

        internal List<MarkerModel> GetMarkerModels(string? gamename = null, DateOnly? date = null, int? recordingSession = null)
        {
            var query = _context.Markers.AsQueryable();

            if (!string.IsNullOrEmpty(gamename))
            {
                query = query.Where(marker => marker.GameId == _context.GameStats
                    .Where(game => game.GameName == gamename)
                    .Select(game => game.GameId)
                    .FirstOrDefault());
            }

            if (date.HasValue)
            {
                query = query.Where(marker => DateOnly.FromDateTime(marker.TimeStamp) == date.Value);
            }

            if (recordingSession.HasValue)
            {
                query = query.Where(marker => marker.RecordingSession == recordingSession.Value);
            }

            return query.ToList();
        }
    }
}
