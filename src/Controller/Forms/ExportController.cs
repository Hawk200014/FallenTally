using ClosedXML.Excel;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Resources;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using FallenTally.Enums;
using FallenTally.Utility.Singletons;
using System.Data;
using System.Globalization;


namespace DeathCounterHotkey.Controller.Forms
{
    public class ExportController : ISingleton
    {
        

        private readonly MarkerController? _markercontroller;
        private readonly Singleton _singleton = Singleton.GetInstance();
        private readonly SQLiteDBContext? _context;
        private CultureInfo _culture = new CultureInfo("de-DE");

        public ExportController()
        {
            _context = _singleton.GetValue(SQLiteDBContext.GetSingletonName()) as SQLiteDBContext;
            _markercontroller = _singleton.GetValue(MarkerController.GetSingletonName()) as MarkerController;
        }

        internal int GetDeathCount(string? gameName, string? locationName, string? timeStamp)
        {
            return GetDeathModels(gameName, locationName, timeStamp).Count;
        }

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

        internal string[] GetDistinctDeathDates(string? gameName = null, string? locationName = null, string? timeStamp = null)
        {
            return GetDeathModels(gameName, locationName, timeStamp)
                .Select(x => DateOnly.FromDateTime(x.TimeStamp).ToString("d", _culture))
                .Distinct()
                .ToArray();
        }

        internal string[] GetGameDeathList()
        {
            return _context.GameStats.Select(x => x.GameName).Distinct().ToArray();
        }

        internal string[] GetLocationDeathList(string gameName)
        {
            return _context.Locations
                .Join(_context.GameStats, l => l.GameID, g => g.GameId, (l, g) => new { l, g })
                .Where(x => x.g.GameName == gameName)
                .Select(x => x.l.Name)
                .Distinct()
                .ToArray();
        }

        internal async Task<string> Export(EXPORTTYPE exportType, string gameName, string locationName, string date, string exportPath)
        {
            DataTable dt = CreateDataTableFromFilter(gameName, locationName, date);
            int result = exportType switch
            {
                EXPORTTYPE.CSV => await ExportCSV(dt, exportPath),
                EXPORTTYPE.EXCEL => await ExportToExcel(dt, exportPath),
                EXPORTTYPE.FTSTAMPS => await ExportToFTStamps(CreateDataTableFromFilterForFTS(gameName, locationName, date), exportPath),
                _ => 0
            };

            string message = result == 1 ? "Export successful" : "Export failed";
            MessageBox.Show(message, "Export", MessageBoxButtons.OK);
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

        private Task<int> ExportToFTStamps(DataTable dt, string exportPath)
        {
            return ExportToFile(dt, exportPath, async (writer, line) => await writer.WriteLineAsync(line));
        }

        private async Task<int> ExportToExcel(DataTable dt, string exportPath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Deaths");
                    worksheet.Cell(1, 1).InsertTable(dt);
                    workbook.SaveAs(exportPath);
                }
                return 1;
            }
            catch (Exception ex)
            {
                // Log exception
                return 0;
            }
        }

        private Task<int> ExportCSV(DataTable dt, string exportPath)
        {
            return ExportToFile(dt, exportPath, async (writer, line) => await writer.WriteLineAsync(line));
        }

        private async Task<int> ExportToFile(DataTable dt, string exportPath, Func<StreamWriter, string, Task> writeLineAsync)
        {
            try
            {
                using (var writer = new StreamWriter(exportPath))
                {
                    var header = string.Join(";", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    await writeLineAsync(writer, header);

                    foreach (DataRow row in dt.Rows)
                    {
                        var line = string.Join(";", row.ItemArray.Select(field => field.ToString()));
                        await writeLineAsync(writer, line);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                // Log exception
                return 0;
            }
        }

        private DataTable CreateDataTableFromFilter(string gameName, string locationName, string date)
        {
            return CreateDataTable(gameName, locationName, date, (dataTable, death, deathNumber) =>
            {
                dataTable.Rows.Add(death.Location.Game.GameName, deathNumber, death.Location.Name, death.Location.Finish, 0, death.TimeStamp.ToString(), TimerController.ConvertTimeToReadableTime(death.StreamTime), TimerController.ConvertTimeToReadableTime(death.RecordingTime));
            });
        }

        private DataTable CreateDataTable(string gameName, string locationName, string date, Action<DataTable, DeathModel, int> addRow)
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
                int deathNumber = 0;
                var deathLocationModels = _context.Locations
                    .Where(x => x.GameID == gameStats.GameId && (string.IsNullOrEmpty(locationName) || x.Name == locationName))
                    .OrderBy(x => x.LocationId)
                    .ToList();

                foreach (var deathLocation in deathLocationModels)
                {
                    var deathModels = _context.Deaths
                        .Where(x => x.LocationId == deathLocation.LocationId)
                        .AsEnumerable()
                        .Where(x => string.IsNullOrEmpty(date) || DateOnly.FromDateTime(x.TimeStamp).ToString("d", _culture) == date)
                        .ToList();

                    foreach (var death in deathModels)
                    {
                        deathNumber++;
                        addRow(dataTable, death, deathNumber);
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
            return Enum.GetNames(typeof(EXPORTTYPE));
        }

        internal int GetMarkerCount(string? game, int? session, string? date)
        {
            DateOnly? dateOnly = string.IsNullOrEmpty(date) ? null : DateOnly.Parse(date);
            return _markercontroller.GetMarkerModels(game, dateOnly, session).Count;
        }

        internal string[] GetGameMarkerList()
        {
            return _context.GameStats
                .Where(x => _context.Markers.Select(m => m.GameId).Contains(x.GameId))
                .Select(x => x.GameName)
                .Distinct()
                .ToArray();
        }

        internal string[] GetDistinctMarkerDates(string gamename = "")
        {
            var query = _context.Markers.AsQueryable();

            if (!string.IsNullOrEmpty(gamename))
            {
                query = query.Where(m => m.GameId == _context.GameStats.Where(x => x.GameName == gamename).Select(x => x.GameId).FirstOrDefault());
            }

            return query.Select(m => DateOnly.FromDateTime(m.TimeStamp).ToString("d", _culture)).Distinct().ToArray();
        }

        internal string[] GetDistinctMarkerSessions(string? gamename = null, string? date = null)
        {
            var query = _context.Markers.AsQueryable();

            if (!string.IsNullOrEmpty(gamename))
            {
                query = query.Where(m => m.GameId == _context.GameStats.Where(x => x.GameName == gamename).Select(x => x.GameId).FirstOrDefault());
            }

            if (!string.IsNullOrEmpty(date))
            {
                query = query.Where(m => DateOnly.FromDateTime(m.TimeStamp) == DateOnly.Parse(date));
            }
            return query.Select(m => m.RecordingSession.ToString()).Distinct().ToArray();
        }

        internal async Task<string> ExportMarker(EXPORTTYPE exportType, string gameName, string date, string session, string fileName)
        {
            DataTable dt = CreateDataTableFromFilterMarker(gameName, date, session);
            int result = exportType switch
            {
                EXPORTTYPE.CSV => await ExportCSV(dt, fileName),
                EXPORTTYPE.EXCEL => await ExportToExcel(dt, fileName),
                EXPORTTYPE.FTSTAMPS => await ExportToFTStamps(CreateDataTableFromFilterMarkerFTS(gameName, date, session), fileName),
                _ => 0
            };

            string message = result == 1 ? "Export successful" : "Export failed";
            MessageBox.Show(message, "Export", MessageBoxButtons.OK);
            return message;
        }

        private DataTable CreateDataTableFromFilterMarker(string gameName, string date, string session)
        {
            return CreateDataTableMarker(gameName, date, session, (dataTable, marker, markerNumber) =>
            {
                dataTable.Rows.Add(marker.Game.GameName, marker.Categorie, marker.TimeStamp.ToString(), marker.RecordingSession, TimerController.ConvertTimeToReadableTime(marker.RecordingTime), marker.StreamSession, TimerController.ConvertTimeToReadableTime(marker.StreamTime));
            });
        }

        private DataTable CreateDataTableFromFilterMarkerFTS(string gameName, string date, string session)
        {
            return CreateDataTableMarker(gameName, date, session, (dataTable, marker, markerNumber) =>
            {
                dataTable.Rows.Add(marker.Categorie, marker.RecordingTime, marker.StreamTime, markerNumber);
            });
        }

        private DataTable CreateDataTableMarker(string gameName, string date, string session, Action<DataTable, MarkerModel, int> addRow)
        {
            var gameStatsModels = _context.GameStats
                .Where(x => string.IsNullOrEmpty(gameName) || x.GameName == gameName)
                .OrderBy(x => x.GameId)
                .ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Game", typeof(string));
            dataTable.Columns.Add("MarkerType", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Recording Session", typeof(int));
            dataTable.Columns.Add("Recording Time", typeof(string));
            dataTable.Columns.Add("Streaming Session", typeof(int));
            dataTable.Columns.Add("Stream Time", typeof(string));

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
                    addRow(dataTable, marker, markerNumber);
                }
            }
            return dataTable;
        }

        public static string GetSingletonName()
        {
            return "ExportController";
        }
    }
}
