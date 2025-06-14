using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
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
