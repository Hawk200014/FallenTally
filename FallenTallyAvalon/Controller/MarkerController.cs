using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Database.Models;
using FallenTallyAvalon.Controller.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
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

        internal void SetMark(MARKER markerType, GameStatsModel? gameStatsModel, TimerController? streamTimer = null, TimerController? recordController = null)
        {
            string categorie = markerType.ToString();
            MarkerModel markerModel = new MarkerModel()
            {
                categorie = categorie,
                GameId = gameStatsModel?.GameId ?? -1,
                TimeStamp = DateTime.Now,
                RecordingTime = recordController?.GetTime() ?? 0,
                StreamTime = streamTimer?.GetTime() ?? 0,
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
