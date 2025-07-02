using System.ComponentModel.DataAnnotations;

namespace FallenTally.Database.Models
{
    public class GameStatsModel
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Prefix { get; set; }



    }
}
