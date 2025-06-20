using System.ComponentModel.DataAnnotations;

namespace FallenTally.Database.Models
{
    public class DeathLocationModel
    {
        [Key]
        public int LocationId { get; set; }
        public int GameID { get; set; }
        public string Name { get; set; }
        public bool Finish { get; set; }



    }
}
