using FallenTally.Controller.Timers;
using System;
using System.ComponentModel.DataAnnotations;

namespace FallenTally.Database.Models
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
