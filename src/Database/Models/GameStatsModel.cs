using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class GameStatsModel
    {
        public GameStatsModel(string gameName, string prefix) 
        { 
            GameName = gameName;
            Prefix = prefix;
        }


        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Prefix { get; set; }

        public List<DeathLocationModel> deathLocations { get; set; }

        public string ToTextFileString()
        {
            return Prefix + " " + CalcDeaths();
        }

        public int CalcDeaths()
        {
            int deaths = 0;
            foreach (var location in deathLocations) 
            {
                deaths += location.Deaths.Count();
            }
            return deaths;
        }
    }
}
