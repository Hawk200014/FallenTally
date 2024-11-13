using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class DeathModel
    {
        [Key]
        public int DeathId { get; set; }
        public int LocationId { get; set; }
        public int StreamTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
