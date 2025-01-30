using DeathCounterHotkey.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database.Models
{
    public class MarkerModel
    {
        [Key]
        public int MarkerId { get; set; }
        public int GameId { get; set; }
        public string categorie { get; set; }
        public DateTime TimeStamp { get; set; }
        public int StreamTime { get; set; }
        public int StreamSession { get; set; }
        public int RecordingTime { get; set; }
        public int RecordingSession { get; set; }


        public override string ToString()
        {
            return RecordingSession + ", " + categorie + ", R:" + TimerController.ConvertTimeToReadableTime(RecordingTime) + ",  S:" + TimerController.ConvertTimeToReadableTime(StreamTime);
        }
    }
}
