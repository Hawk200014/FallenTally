using System;
using System.ComponentModel.DataAnnotations;

namespace FallenTally.Database.Models
{
    public class DeathModel
    {
        [Key]
        public int DeathId { get; set; }
        public int LocationId { get; set; }
        public int StreamTime { get; set; }
        public int RecordingTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
