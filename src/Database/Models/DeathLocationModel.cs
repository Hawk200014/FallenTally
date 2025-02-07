using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class DeathLocationModel
    {
        [Key]
        public int LocationId { get; set; }
        public bool Finish { get; set; }
        public int GameID { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation properties
        public GameStatsModel Game { get; set; }
        public ICollection<DeathModel> Deaths { get; set; }
    }
}
