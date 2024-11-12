using System.ComponentModel.DataAnnotations;

namespace DeathCounterHotkey.Database.Models
{
    public class SettingsModel
    {
        [Key]
        public int SettingsId;
        public string SettingsName;
        public string SettingsValue;

        public SettingsModel(string settingsName, string settingsValue)
        {
            this.SettingsName = settingsName;
            this.SettingsValue = settingsValue;
        }

    }
}
