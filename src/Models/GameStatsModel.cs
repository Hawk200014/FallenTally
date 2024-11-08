using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Models
{
    public class GameStatsModel
    { 
        public GameStatsModel(string gameName, string prefix, int deaths, Guid? gameId = null)
        {
            this.GameName = gameName;
            this.Prefix = prefix;
            this.Deaths = deaths;
            if (gameId == null) gameId = Guid.NewGuid();
        }


        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public string Prefix { get; set; }
        public int Deaths { get; set; }

        public string ToTextFileString()
        {
            return Prefix + " " + Deaths;
        }
    }
}
