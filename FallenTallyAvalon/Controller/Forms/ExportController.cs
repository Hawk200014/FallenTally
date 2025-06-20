using ClosedXML.Excel;
using FallenTally.Controller.Timers;
using FallenTally.Database;
using FallenTally.Database.Models;
using FallenTally.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FallenTally.Controller.Forms
{
    public class ExportController
    {
        public enum ExportType
        {
            CSV,
            EXCEL,
            FTSTAMPS
        }

        private readonly MarkerController _markercontroller;
        private readonly SQLiteDBContext _context;
        private CultureInfo _culture = new CultureInfo("de-DE");

        public ExportController(SQLiteDBContext context, MarkerController markerController)
        {
            _markercontroller = markerController;
            _context = context;
        }

        /// <summary>
        /// Gets the count of deaths based on the provided filters.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="locationName">The name of the location.</param>
        /// <param name="timeStamp">The timestamp to filter by.</param>
        /// <returns>The count of deaths.</returns>
        internal int GetDeathCount(string? gameName, string? locationName, string? timeStamp)
        {
            return GetDeathModels(gameName, locationName, timeStamp).Count;
        }

        /// <summary>
        /// Retrieves death models based on the provided filters.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="locationName">The name of the location.</param>
        /// <param name="timeStamp">The timestamp to filter by.</param>
        /// <returns>A list of death models.</returns>
        private List<DeathModel> GetDeathModels(string? gameName, string? locationName, string? timeStamp)
        {
            var query = _context.Deaths
                .Join(_context.Locations, d => d.LocationId, l => l.LocationId, (d, l) => new { d, l })
                .Join(_context.GameStats, dl => dl.l.GameID, g => g.GameId, (dl, g) => new { dl, g });

            if (!string.IsNullOrEmpty(gameName))
            {
                query = query.Where(x => x.g.GameName == gameName);
            }

            if (!string.IsNullOrEmpty(locationName))
            {
                query = query.Where(x => x.dl.l.Name == locationName);
            }

            var result = query.ToList();

            if (!string.IsNullOrEmpty(timeStamp))
            {
                result = result.Where(x => string.IsNullOrEmpty(timeStamp) || DateOnly.FromDateTime(x.dl.d.TimeStamp).ToString("d", _culture) == timeStamp).ToList();
            }

            return result.Select(x => x.dl.d).ToList();
        }

        /// <summary>
        /// Gets distinct death dates based on the provided filters.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="locationName">The name of the location.</param>
        /// <param name="timeStamp">The timestamp to filter by.</param>
        /// <returns>An array of distinct death dates.</returns>
        internal string[] GetDistinctDeathDates(string? gameName = null, string? locationName = null, string? timeStamp = null)
        {
            var query = GetDeathModels(gameName, locationName, timeStamp).Select(x => DateOnly.FromDateTime(x.TimeStamp).ToString("d", _culture));
            return query.Distinct().ToArray();
        }

        /// <summary>
        /// Retrieves a list of distinct game names.
        /// </summary>
        /// <returns>An array of game names.</returns>
        internal string[] GetGameDeathList()
        {
            return _context.GameStats.Select(x => x.GameName).Distinct().ToArray();
        }

        /// <summary>
        /// Retrieves a list of distinct locations for a given game.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <returns>An array of location names.</returns>
        internal string[] GetLocationDeathList(string gameName)
        {
            return _context.Locations
                .Join(_context.GameStats, l => l.GameID, g => g.GameId, (l, g) => new { l, g })
                .Where(x => x.g.GameName == gameName)
                .Select(x => x.l.Name)
                .Distinct()
                .ToArray();
        }

        /// <summary>
        /// Exports the death data to the specified format.
        /// </summary>
        /// <param name="exportType">The type of export (CSV or EXCEL).</param>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="locationName">The name of the location.</param>
        /// <param name="date">The date to filter by.</param>
        /// <param name="exportPath">The path to save the exported file.</param>
        /// <returns>A message indicating the result of the export.</returns>
        internal string Export(ExportType exportType, string gameName, string locationName, string date, string exportPath)
        {
            DataTable dt = CreateDataTableFromFilter(gameName, locationName, date);
            int result = exportType switch
            {
                ExportType.CSV => ExportCSV(dt, exportPath),
                ExportType.EXCEL => ExportToExcel(dt, exportPath),
                ExportType.FTSTAMPS => ExportToFTStamps(CreateDataTableFromFilterForFTS(gameName, locationName, date), exportPath), // Not implemented
                _ => 0
            };

            string message = result switch
            {
                1 => "Export successful",
                _ => "Export failed"
            };

            //MessageBox.Show(message, "Export", MessageBoxButtons.OK);
            return message;
        }

        private DataTable CreateDataTableFromFilterForFTS(string gameName, string locationName, string date)
        {
            var query = _context.Deaths
                .Join(_context.Locations, d => d.LocationId, l => l.LocationId, (d, l) => new { d, l })
                .Join(_context.GameStats, dl => dl.l.GameID, g => g.GameId, (dl, g) => new { dl, g });

            if (!string.IsNullOrEmpty(gameName))
            {
                query = query.Where(x => x.g.GameName == gameName);
            }

            if (!string.IsNullOrEmpty(locationName))
            {
                query = query.Where(x => x.dl.l.Name == locationName);
            }
            var result = query.ToList();

            if (!string.IsNullOrEmpty(date))
            {
                result = result.Where(x => string.IsNullOrEmpty(date) || DateOnly.FromDateTime(x.dl.d.TimeStamp).ToString("d", _culture) == date).ToList();
            }

            var deaths = result.Select(x => new { x.dl.l.Name, x.dl.d.RecordingTime, x.dl.d.StreamTime, x.dl.d.TimeStamp }).OrderBy(x => x.TimeStamp);

            //var gameStatsModels = _context.GameStats
            //.Where(x => string.IsNullOrEmpty(gameName) || x.GameName == gameName)
            //.OrderBy(x => x.GameId)
            //.ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Location", typeof(string));
            dataTable.Columns.Add("Death stream time", typeof(string));
            dataTable.Columns.Add("Death recording time", typeof(string));
            dataTable.Columns.Add("Death Number", typeof(int));

            int deathNumber = 0;

            foreach (var death in deaths)
            {

                string deahLocStr = death.Name == GLOBALVARS.DEFAULT_LOCATION ? "WORLD" : "LOCATION";


                deathNumber++;

                dataTable.Rows.Add(deahLocStr, death.RecordingTime, death.StreamTime, deathNumber);

            }
            return dataTable;
        }

        private int ExportToFTStamps(DataTable dt, string exportPath)
        {
            try
            {
                using (var writer = new StreamWriter(exportPath))
                {
                    // Write the header
                    //var header = string.Join(";", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));

                    //dt.Columns.Cast<DataColumn>().Where(x => x.ColumnName.Equals())

                    //writer.WriteLine(header);

                    // Write the data
                    foreach (DataRow row in dt.Rows)
                    {
                        var line = string.Join(";", row.ItemArray.Select(field => field.ToString()));
                        writer.WriteLine(line);
                    }
                }
                return 1; // Success
            }
            catch
            {
                return 0; // Failure
            }
        }

        /// <summary>
        /// Exports the data to an Excel file.
        /// </summary>
        /// <param name="dt">The data to export.</param>
        /// <param name="exportPath">The path to save the Excel file.</param>
        /// <returns>1 for success, 0 for failure.</returns>
        private int ExportToExcel(DataTable dt, string exportPath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Deaths");
                    worksheet.Cell(1, 1).InsertTable(dt);
                    workbook.SaveAs(exportPath);
                }
                return 1; // Success
            }
            catch
            {
                return 0; // Failure
            }
        }

        /// <summary>
        /// Exports the data to a CSV file.
        /// </summary>
        /// <param name="dt">The data to export.</param>
        /// <param name="exportPath">The path to save the CSV file.</param>
        /// <returns>1 for success, 0 for failure.</returns>
        private int ExportCSV(DataTable dt, string exportPath)
        {
            try
            {
                using (var writer = new StreamWriter(exportPath))
                {
                    // Write the header
                    var header = string.Join(";", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    writer.WriteLine(header);

                    // Write the data
                    foreach (DataRow row in dt.Rows)
                    {
                        var line = string.Join(";", row.ItemArray.Select(field => field.ToString()));
                        writer.WriteLine(line);
                    }
                }
                return 1; // Success
            }
            catch
            {
                return 0; // Failure
            }
        }

        /// <summary>
        /// Creates a DataTable from the filtered death data.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="locationName">The name of the location.</param>
        /// <param name="date">The date to filter by.</param>
        /// <returns>A DataTable containing the filtered data.</returns>
        private DataTable CreateDataTableFromFilter(string gameName, string locationName, string date)
        {
            var gameStatsModels = _context.GameStats
                .Where(x => string.IsNullOrEmpty(gameName) || x.GameName == gameName)
                .OrderBy(x => x.GameId)
                .ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Game", typeof(string));
            dataTable.Columns.Add("Death number in game", typeof(int));
            dataTable.Columns.Add("Location", typeof(string));
            dataTable.Columns.Add("Location finished", typeof(int));
            dataTable.Columns.Add("Death number at location", typeof(int));
            dataTable.Columns.Add("Death date and time", typeof(string));
            dataTable.Columns.Add("Death stream time", typeof(string));
            dataTable.Columns.Add("Death recording time", typeof(string));

            foreach (var gameStats in gameStatsModels)
            {
                int deathInGame = 0;
                var deathLocationModels = _context.Locations
                    .Where(x => x.GameID == gameStats.GameId && (string.IsNullOrEmpty(locationName) || x.Name == locationName))
                    .OrderBy(x => x.LocationId)
                    .ToList();

                foreach (var deathLocation in deathLocationModels)
                {
                    int deathAtLocation = 0;
                    var deathModels = _context.Deaths
                        .Where(x => x.LocationId == deathLocation.LocationId)
                        .AsEnumerable()
                        .Where(x => string.IsNullOrEmpty(date) || DateOnly.FromDateTime(x.TimeStamp).ToString("d", _culture) == date)
                        .ToList();

                    foreach (var death in deathModels)
                    {
                        deathInGame++;
                        deathAtLocation++;
                        dataTable.Rows.Add(gameStats.GameName, deathInGame, deathLocation.Name, deathLocation.Finish, deathAtLocation, death.TimeStamp.ToString(), TimerController.ConvertTimeToReadableTime(death.StreamTime), TimerController.ConvertTimeToReadableTime(death.RecordingTime));
                    }
                }
            }
            return dataTable;
        }


        /// <summary>
        /// Gets the available export formats.
        /// </summary>
        /// <returns>An array of export format names.</returns>
        internal string[] GetExportFormats()
        {
            return Enum.GetNames(typeof(ExportType));
        }

        internal int GetMarkerCount(GameStatsModel? game = null, int? session = null, DateOnly? date = null)
        {
            var query = _markercontroller.InitFilter();
            if (date != null) {
                //query = _markercontroller.Filter(date);
            }
            if (game != null) {
                query = _markercontroller.Filter(game);
            }
            if( session != null) {
                query = _markercontroller.Filter(session.Value);
            }
            if(query == null) {
                return 0;
            }
            return query.Count();
        }

        internal string[] GetGameMarkerList()
        {

            return _context.GameStats
                .Where(x => _context.Markers
                                   .Select(m => m.GameId)
                                                      .Contains(x.GameId))
                .Select(x => x.GameName).Distinct().ToArray();
        }

        internal string[] GetDistinctMarkerDates(GameStatsModel? game = null)
        {
            var query = _context.Markers.AsQueryable();

            if (game != null)
            {
                query = query.Where(m => m.GameId == _context.GameStats
                    .Where(x => x.GameName == game.GameName)
                    .Select(x => x.GameId)
                    .FirstOrDefault()
                );
            }

            return query.Select(m => DateOnly.FromDateTime(m.TimeStamp).ToString("d",
                  _culture)).Distinct().ToArray();
        }

        internal string[] GetDistinctMarkerSessions(GameStatsModel? game = null, DateOnly? date = null)
        {
            var query = _context.Markers.AsQueryable();

            if (game != null)
            {
                query = query.Where(m => m.GameId == _context.GameStats
                    .Where(x => x.GameName == game.GameName)
                    .Select(x => x.GameId)
                    .FirstOrDefault()
                );
            }

            if (date != null)
            {
                query = query.Where(m => DateOnly.FromDateTime(m.TimeStamp) == date);
            }
            return query.Select(m => m.RecordingSession.ToString()).Distinct().ToArray();
        }

        internal string ExportMarker(ExportType exportType, GameStatsModel game, string date, string session, string fileName)
        {
            DataTable dt = CreateDataTableFromFilterMarker(game, date, session);
            int result = exportType switch
            {
                ExportType.CSV => ExportCSV(dt, fileName),
                ExportType.EXCEL => ExportToExcel(dt, fileName),
                ExportType.FTSTAMPS => ExportToFTStamps(CreateDataTableFromFilterMarkerFTS(game.GameName, date, session), fileName),
                _ => 0
            };

            string message = result switch
            {
                1 => "Export successful",
                _ => "Export failed"
            };

            //MessageBox.Show(message, "Export", MessageBoxButtons.OK);
            return message;
        }

        private DataTable CreateDataTableFromFilterMarker(GameStatsModel? game, string date, string session)
        {
            IQueryable<GameStatsModel> gameStatsModels = _context.GameStats.AsQueryable();

            if (game != null)
            {
                gameStatsModels = gameStatsModels.Where(x => x.GameName == game.GameName);
            }

            gameStatsModels = gameStatsModels
               .OrderBy(x => x.GameId);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Game", typeof(string));
            dataTable.Columns.Add("MarkerType", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Recording Session", typeof(int));
            dataTable.Columns.Add("Recording Time", typeof(string));
            dataTable.Columns.Add("Streaming Session", typeof(int));
            dataTable.Columns.Add("Stream Time", typeof(string));

            foreach (var gameStats in gameStatsModels)
            {
                var markers = _context.Markers
                    .Where(x => x.GameId == gameStats.GameId)
                    .Where(x => string.IsNullOrEmpty(date) || DateOnly.FromDateTime(x.TimeStamp).ToString("d", _culture) == date)
                    .Where(x => string.IsNullOrEmpty(session) || x.RecordingSession.ToString() == session)
                    .ToList();
                foreach (var marker in markers)
                {
                    dataTable.Rows.Add(gameStats.GameName, marker.categorie, marker.TimeStamp.ToString(), marker.RecordingSession, TimerController.ConvertTimeToReadableTime(marker.RecordingTime), marker.StreamSession, TimerController.ConvertTimeToReadableTime(marker.StreamTime));
                }

            }
            return dataTable;
        }

        private DataTable CreateDataTableFromFilterMarkerFTS(string gameName, string date, string session)
        {
            var gameStatsModels = _context.GameStats
               .Where(x => string.IsNullOrEmpty(gameName) || x.GameName == gameName)
               .OrderBy(x => x.GameId)
               .ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MarkerType", typeof(string));
            dataTable.Columns.Add("Recording Time", typeof(string));
            dataTable.Columns.Add("Stream Time", typeof(string));
            dataTable.Columns.Add("Marker Number", typeof(int));

            int markerNumber = 0;

            foreach (var gameStats in gameStatsModels)
            {
                var markers = _context.Markers
                    .Where(x => x.GameId == gameStats.GameId)
                    .Where(x => string.IsNullOrEmpty(date) || DateOnly.FromDateTime(x.TimeStamp).ToString("d", _culture) == date)
                    .Where(x => string.IsNullOrEmpty(session) || x.RecordingSession.ToString() == session)
                    .OrderBy(x => x.TimeStamp)
                    .ToList();
                foreach (var marker in markers)
                {
                    markerNumber++;
                    dataTable.Rows.Add(marker.categorie, marker.RecordingTime, marker.StreamTime, markerNumber);
                }

            }
            return dataTable;
        }
    }
}
