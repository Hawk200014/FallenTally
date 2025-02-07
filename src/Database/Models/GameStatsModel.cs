using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class GameStatsModel
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        public string Prefix { get; set; }

        // Navigation properties
        public ICollection<DeathLocationModel> Locations { get; set; }
        public ICollection<MarkerModel> Markers { get; set; }
    }
}
