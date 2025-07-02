using FallenTally.Database;
using FallenTally.Database.Models;
using System.Linq;

namespace FallenTally.Controller.Forms
{
    public class OptionsController
    {
        private SQLiteDBContext _context;
        public OptionsController(SQLiteDBContext context)
        {
            this._context = context;

        }

        public bool SetSetting(OPTIONS key, string value)
        {
            if (IsDupeEntry(key)) return false;
            _context.Settings.Add(new SettingsModel()
            {
                SettingsName = nameof(key),
                SettingsValue = value
            });
            _context.SaveChanges();
            return true;
        }

        public bool SetOrEditSetting(OPTIONS key, string value)
        {
            if (SetSetting(key, value)) return true;
            if (EditSetting(key, value)) return true;
            return false;
        }

        public bool EditSetting(OPTIONS key, string value)
        {
            SettingsModel? model = _context.Settings.Where(x => x.SettingsName.Equals(nameof(key))).FirstOrDefault();
            if (model == null) return false;
            model.SettingsValue = value;
            _context.SaveChanges();
            return true;
        }

        public bool IsDupeEntry(OPTIONS key)
        {
            return _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(nameof(key))).Any();
        }

        public enum OPTIONS
        {
            LANGUAGE,

            TWITCH_NAME,
            WORLD_AS_ALL,
            TALLY_INCREASE_HOTKEY,
            TALLY_DECREASE_HOTKEY,
            TALLY_SWITCH_LOCATION_HOTKEY,
            TALLY_QUICKADD_LOCATION_HOTKEY,
            TALLY_FINISH_LOCATION_HOTKEY,
            TIMER_START_RECORDING_HOTKEY,
            TIMER_START_STREAM_HOTKEY,
            TIMER_STOP_RECORDING_HOTKEY,
            TIMER_STOP_STREAM_HOTKEY,
            MARKER_NORMAL_HOTKEY,
            MARKER_FUNNY_HOTKEY,
            MARKER_GAMING_HOTKEY,
            MARKER_TALK_HOTKEY,
            MARKER_PAUSE_HOTKEY
        }

        public string GetSetting(OPTIONS key)
        {
            return _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(nameof(key))).FirstOrDefault()?.SettingsValue ?? "";
        }

        internal string GetLanguage()
        {
            SettingsModel? language = _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(nameof(OptionsController.OPTIONS.LANGUAGE))).FirstOrDefault();
            if (language == null) return "en";
            return language.SettingsValue;
        }
    }
}
