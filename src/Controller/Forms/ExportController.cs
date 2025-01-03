using ClosedXML.Excel;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class ExportController
    {
        public enum ExportType
        {
            CSV,
            EXCEL
        }

        private readonly SQLiteDBContext _context;

        public ExportController(SQLiteDBContext context)
        {
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
                result = result.Where(x => x.dl.d.TimeStamp.ToLongDateString() == timeStamp).ToList();
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
            var query = GetDeathModels(gameName, locationName, timeStamp).Select(x => x.TimeStamp.ToLongDateString());
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
                _ => 0
            };

            string message = result switch
            {
                1 => "Export successful",
                _ => "Export failed"
            };

            MessageBox.Show(message, "Export", MessageBoxButtons.OK);
            return message;
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
                        .Where(x => string.IsNullOrEmpty(date) || x.TimeStamp.ToLongDateString() == date)
                        .ToList();

                    foreach (var death in deathModels)
                    {
                        deathInGame++;
                        deathAtLocation++;
                        dataTable.Rows.Add(gameStats.GameName, deathInGame, deathLocation.Name, deathLocation.Finish, deathAtLocation, death.TimeStamp.ToString(), ConvertTimeToReadableTime(death.StreamTime));
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Converts time in seconds to a readable format.
        /// </summary>
        /// <param name="time">Time in seconds.</param>
        /// <returns>A string representing the time in HH:mm:ss format.</returns>
        private string ConvertTimeToReadableTime(double time)
        {
            TimeSpan t = TimeSpan.FromSeconds(time);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }

        /// <summary>
        /// Gets the available export formats.
        /// </summary>
        /// <returns>An array of export format names.</returns>
        internal string[] GetExportFormats()
        {
            return Enum.GetNames(typeof(ExportType));
        }
    }
}
