using DeathCounterHotkey.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeathCounterHotkey
{
    public class Settings
    {
        private static string _settingsPath = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Settings\\");
        private static string _settingsFileName = Path.Combine(_settingsPath, "settings.csv");
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        private string _delemiter = ";";
        private static string _TextFile = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Overlay\\", "Overlay.txt");
        public string language = "en";


        public Settings()
        {
            string methodPrefix = "Settings.Constuctor: ";
            Directory.CreateDirectory(_settingsPath);
        }

        public void LoadSettings()
        {
            string methodPrefix = "Settings.LoadSettings: ";
            if(!File.Exists(_settingsFileName)) { return; }
            string[] filecontent = File.ReadAllLines(_settingsFileName);
            _settings.Clear();
            foreach (string line in filecontent)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string key = line.Split(";")[0];
                    string value = line.Split(";")[1];
                    _settings.Add(key, value);
                }
            }
            DebugLogger.Debug(methodPrefix + "settings loaded");
        }

        public void SaveSettings()
        {
            string methodPrefix = "Settings.SaveSettings: ";
            List<string> fileLines = new List<string> { };
            foreach (string key in _settings.Keys)
            {
                string value = _settings[key];
                fileLines.Add( key + ";" + value);
            }
            File.WriteAllLines(_settingsFileName, fileLines.ToArray());
            DebugLogger.Debug(methodPrefix + "settings saved");
        }

        public void AddSetting(string key, string value)
        {
            string methodPrefix = "Settings.AddSetting: ";
            _settings.Add(key, value);
            DebugLogger.Debug(methodPrefix + "key: " + key + "; value:" + value);
        }

        public void SaveGameStats(GameStatsModel stats)
        {
            string methodPrefix = "Settings.SaveGameStats: ";
            
            string gameName = stats.GameName;

            string escapedFileName = Path.Combine(_settingsPath, Utilities.EscapeSpecialChars(gameName.ToLower()));
            DebugLogger.Debug(methodPrefix + "Savepath: " + escapedFileName);
            string[] fileContent = new string[3];
            fileContent[0] = stats.GameName;
            fileContent[1] = stats.Prefix;
            fileContent[2] = "" + stats.Deaths;
            File.WriteAllLines(escapedFileName, fileContent);

        }

        public void RemoveGameStats(GameStatsModel stats)
        {
            string methodPrefix = "Settings.RemoveGameStats: ";
            string gameName = stats.GameName;
            string escapedFileName = Path.Combine(_settingsPath, Utilities.EscapeSpecialChars(gameName.ToLower()));
            DebugLogger.Debug(methodPrefix + "Deleting File: " + escapedFileName);
            File.Delete(escapedFileName);

        }

        public GameStatsModel? GetGameStats(string gameDataPath)
        {
            string methodPrefix = "Settings.GetGameStats: ";
            string[] fileContent = File.ReadAllLines(gameDataPath);
            return new GameStatsModel(fileContent[0], fileContent[1], int.Parse(fileContent[2]));
            DebugLogger.Debug(methodPrefix + "File not found while loading: " + gameDataPath);
            return null;
        }

        public List<GameStatsModel> GetAllGameStats()
        {
            string methodPrefix = "Settings.GetAllGameStats: ";
            DebugLogger.Debug(methodPrefix + "Loading All GameStats");
            string[] files = Directory.GetFiles(_settingsPath);
            DebugLogger.Debug(methodPrefix + "Loaded " + files.Length + " total files");
            List<GameStatsModel> gameStatsModels = new List<GameStatsModel>();
            foreach (string file in files)
            {
                if(file != _settingsFileName)
                {
                    GameStatsModel? game = GetGameStats(file);
                    if (game != null)
                    {
                        gameStatsModels.Add(game);
                    }
                }
            }
            DebugLogger.Debug(methodPrefix + "Loaded " + gameStatsModels.Count() + " total games");
            return gameStatsModels;
        }


        public void WriteTextFile(GameStatsModel model)
        {
            Directory.CreateDirectory(Path.GetDirectoryName( _TextFile));
            File.WriteAllText(_TextFile, model.ToTextFileString());
        }

        public void LoadLanguage()
        {
            try {
                this.language = _settings["language"];
            }
            catch (Exception)
            {
                this.language = "en";
            }
        }

        public string GetLanguage()
        {
            return this.language;
        }

        private void SaveLanguage()
        {
            AddSetting("language", this.language);
            SaveSettings();
        }
    }
}
