using System;
using System.ComponentModel.DataAnnotations;

namespace FallenTally.Database.Models
{
    public class RecordingModel
    {
        [Key]
        public int RecordingId { get; set; }
        public int SessionCount { get; set; }
        public DateOnly SessionDate { get; set; }
        public string Type { get; set; }
    }
}
