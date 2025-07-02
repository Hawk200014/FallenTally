using System.ComponentModel.DataAnnotations;

namespace FallenTally.Database.Models
{
    public class SettingsModel
    {
        [Key]
        public int SettingsId { get; set; }
        public string SettingsName { get; set; }
        public string SettingsValue { get; set; }


    }
}
