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
        public string GameName { get; set; }
        public string Prefix { get; set; }



    }
}
