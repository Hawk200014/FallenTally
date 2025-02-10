using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Enums;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class MarkerController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();
        private SQLiteDBContext? _context;
        private RecordingController? _recordingController;



        public MarkerController() 
        {   
            this._context = _singleton.GetValue(SQLiteDBContext.GetSingletonName()) as SQLiteDBContext;
            this._recordingController = _singleton.GetValue(RecordingController.GetSingletonName()) as RecordingController;
        }

        internal void SetMark(MARKER markerType, GameStatsModel? gameStatsModel, TimerController? streamTimer = null, TimerController? recordController = null)
        {
            string categorie = markerType.ToString();
            MarkerModel markerModel = new MarkerModel()
            {
                Categorie = categorie,
                GameId = gameStatsModel?.GameId ?? -1,
                TimeStamp = DateTime.Now,
                RecordingTime = recordController?.GetTime() ?? 0,
                StreamTime = streamTimer?.GetTime() ?? 0,
                StreamSession = _recordingController.GetRecordingNumber(RECORDINGTYPE.stream),
                RecordingSession = _recordingController.GetRecordingNumber(RECORDINGTYPE.recording)
            };
            _context.Markers.Add(markerModel);
            _context.SaveChanges();

        }

        internal string[] GetLatestMarkers()
        {
            return _context.Markers
                .Where(x => x.TimeStamp.Date == DateTime.Now.Date)
                .OrderByDescending(x => x.MarkerId)
                .Select(marker => marker.ToString())
                .ToArray();
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

        public static string GetSingletonName()
        {
            return "MarkerController";
        }
    }
}
