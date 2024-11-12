using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class DeathLocationModel
    {
        [Key]
        public int LocationId;
        public string Name;

        public DeathLocationModel(string locationName)
        {
            this.Name = locationName;
        }

        public List<DeathModel> Deaths { get; set; }
    }
}
