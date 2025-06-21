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

        private IQueryable<MarkerModel> _filterQuery;

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
            this._filterQuery = _context.Markers.AsQueryable();
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
            _filterQuery = _context.Markers;
            return _filterQuery;
        }

        public IQueryable<MarkerModel> Filter(GameStatsModel gameStatsModel)
        {
            _filterQuery = _filterQuery.Where(marker => marker.GameId == gameStatsModel.GameId);
            return _filterQuery;
        }

        public IQueryable<MarkerModel> Filter(DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            if (fromDate != null)
            {
                // Convert DateOnly? to DateTime for comparison
                DateTime fromDateTime = fromDate.Value.ToDateTime(TimeOnly.MinValue);
                _filterQuery = _filterQuery.Where(x => x.TimeStamp >= fromDateTime);
            }

            if (toDate != null)
            {
                // Convert DateOnly? to DateTime for comparison
                DateTime toDateTime = toDate.Value.ToDateTime(TimeOnly.MaxValue);
                _filterQuery = _filterQuery.Where(x => x.TimeStamp <= toDateTime);
            }
            return _filterQuery;
        }

        public IQueryable<MarkerModel> Filter(int recordingSession)
        {
            _filterQuery = _filterQuery.Where(marker => marker.RecordingSession == recordingSession);
            return _filterQuery;
        }

        public IQueryable<MarkerModel> Filter(string markerType)
        {
            _filterQuery = _filterQuery.Where(marker => marker.categorie == markerType);
            return _filterQuery;
        }

        public IQueryable<MarkerModel> GetFilter()
        {
            return _filterQuery;
        }

        #endregion



        internal List<MarkerModel> GetAllMarkers()
        {
            return _context.Markers.OrderByDescending(x => x.MarkerId).ToList();
        }
    }
}
