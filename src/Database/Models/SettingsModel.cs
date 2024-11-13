using System.ComponentModel.DataAnnotations;

namespace DeathCounterHotkey.Database.Models
{
    public class SettingsModel
    {
        [Key]
        public int SettingsId { get; set; }
        public string SettingsName { get; set; }
        public string SettingsValue { get; set; }


    }
}
