using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class OptionsController
    {
        private SQLiteDBContext _context;
        public OptionsController(SQLiteDBContext context)
        {
            this._context = context;

        }

        public bool SetSetting(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return false;
            if (IsDupeEntry(key)) return false;
            _context.Settings.Add(new SettingsModel()
            {
                SettingsName = key,
                SettingsValue = value
            });
            _context.SaveChanges();
            return true;
        }

        public bool SetOrEditSetting(string key, string value)
        {
            if (SetSetting(key, value)) return true;
            if (EditSetting(key, value)) return true;
            return false;
        }

        public bool EditSetting(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return false;
            SettingsModel? model = _context.Settings.Where(x => x.SettingsName.Equals(key)).FirstOrDefault();
            if (model == null) return false;
            model.SettingsValue = value;
            _context.SaveChanges();
            return true;
        }

        public bool IsDupeEntry(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(key)).Any();
        }

        public enum OPTIONS
        {
            LANGUAGE,
            INCREASE_HOTKEY,
            DECREASE_HOTKEY,
            SWITCH_LOCATION_HOTKEY,
            QUICKADD_LOCATION_HOTKEY,
            TWITCH_NAME,
            WORLD_AS_ALL,
            FONTFAMILY,
            FONTSIZE,
            FONTSTYLE,
            FONTWEIGHT,
            BORDERSIZE,
            SHADOWSIZE,
            TEXTCOLOR,
            SHADOWCOLOR,
            BORDERCOLOR,
            FINISH_LOCATION_HOTKEY,
            START_RECORDING_TIMER,
            MARKER_NORMAL_HOTKEY
        }

        public string GetSetting(string key)
        {
            return _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(key)).FirstOrDefault()?.SettingsValue ?? "";
        }

        internal string GetLanguage()
        {
            SettingsModel? language = _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(nameof(OptionsController.OPTIONS.LANGUAGE))).FirstOrDefault();
            if (language == null) return "en";
            return language.SettingsValue;
        }
    }
}
