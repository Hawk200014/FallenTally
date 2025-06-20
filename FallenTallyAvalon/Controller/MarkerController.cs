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

        private IQueryable<MarkerModel>? _searchQuery;

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
            this._searchQuery = _context.Markers.AsQueryable();
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

        #region Filtering
        public IQueryable<MarkerModel> InitFilter()
        {
            return _context.Markers.AsQueryable();
        }

        public IQueryable<MarkerModel>? Filter(GameStatsModel gameStatsModel)
        {
            _searchQuery = _searchQuery?.Where(marker => marker.GameId == _context.GameStats
                    .Where(game => game.GameName == gameStatsModel.GameName)
                    .Select(game => game.GameId)
                    .FirstOrDefault());
            return _searchQuery;
        }

        public IQueryable<MarkerModel>? Filter(DateOnly date)
        {
            _searchQuery = _searchQuery?.Where(marker => DateOnly.FromDateTime(marker.TimeStamp) == date);
            return _searchQuery;
        }

        public IQueryable<MarkerModel>? Filter(int recordingSession)
        {
            _searchQuery = _searchQuery?.Where(marker => marker.RecordingSession == recordingSession);
            return _searchQuery;
        }

        #endregion



        internal List<MarkerModel> GetAllMarkers()
        {
            return _context.Markers.OrderByDescending(x => x.MarkerId).ToList();
        }
    }
}
