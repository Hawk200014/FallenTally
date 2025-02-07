using DeathCounterHotkey.Database.Models;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class TimerController : ISingleton
    {

        public TimerController() { }

        private int timeInSec;

        public int GetTime()
        {
            return timeInSec;
        }

        public void AddTime(int seconds)
        {
            this.timeInSec += seconds;
        }

        public static string ConvertTimeToReadableTime(int time)
        {
            TimeSpan t = TimeSpan.FromSeconds(time);
            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds);
            return answer;
        }

        internal void SetTime(int time)
        {
            timeInSec = time;
        }

        public static string GetSingletonName()
        {
            return "TimerController";
        }
    }
}
