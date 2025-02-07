using System.ComponentModel.DataAnnotations;

namespace DeathCounterHotkey.Database.Models
{
    public class SettingsModel
    {
        [Key]
        public int SettingsId { get; set; }
        [Required]
        public string SettingsName { get; set; }
        [Required]
        public string SettingsValue { get; set; }
    }
}
