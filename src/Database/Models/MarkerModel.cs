using DeathCounterHotkey.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class MarkerModel
    {
        [Key]
        public int MarkerId { get; set; }
        public int GameId { get; set; }
        [Required]
        public string Categorie { get; set; }
        public DateTime TimeStamp { get; set; }
        public int StreamTime { get; set; }
        public int RecordingTime { get; set; }
        public int StreamSession { get; set; }
        public int RecordingSession { get; set; }

        // Navigation properties
        public GameStatsModel Game { get; set; }

    }
}
