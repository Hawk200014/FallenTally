using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class DeathModel
    {
        [Key]
        public int DeathId;
        public int GameId;
        public int LocationId;
        public int StreamTime;
        public DateTime TimeStamp;
    }
}
