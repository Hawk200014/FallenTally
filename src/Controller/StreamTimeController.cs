using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class StreamTimeController
    {

        public StreamTimeController() { }

        private double timeInSec;

        public double GetTime()
        {
            return timeInSec;
        }

        public string ConvertTimeToReadableTime(double time)
        {
            this.timeInSec = time;
            TimeSpan t = TimeSpan.FromSeconds(time);
            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds);
            return answer;
        }
    }
}
