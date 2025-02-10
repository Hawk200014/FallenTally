using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Enums;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class OptionsController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();
        private SQLiteDBContext? _context;
        public OptionsController()
        {
            this._context = _singleton.GetValue(SQLiteDBContext.GetSingletonName()) as SQLiteDBContext;
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
            if (model is null) return false;
            model.SettingsValue = value;
            _context.SaveChanges();
            return true;
        }

        public bool IsDupeEntry(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(key)).Any();
        }



        public string GetSetting(string key)
        {
            return _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(key)).FirstOrDefault()?.SettingsValue ?? "";
        }

        internal string GetLanguage()
        {
            SettingsModel? language = _context.Settings.AsEnumerable().Where(x => x.SettingsName.Equals(nameof(OPTIONS.LANGUAGE))).FirstOrDefault();
            if (language is null) return "en";
            return language.SettingsValue;
        }

        public static string GetSingletonName()
        {
            return "OptionsController";
        }
    }
}
