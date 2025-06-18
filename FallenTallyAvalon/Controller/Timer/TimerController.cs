using DeathCounterHotkey.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class TimerController
    {
        private int timeInSec;
        private Timer? _timer;

        public event Action<int>? Tick; // Optional: subscribe to get notified every second

        public TimerController() { }
        public TimerController(int seconds) 
        { 
            this.timeInSec = seconds;
        }

        public int GetTime()
        {
            return timeInSec;
        }



        public void Init(int seconds)
        {
            this.timeInSec = seconds;
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

        public string GetFormattedTime()
        {
            return ConvertTimeToReadableTime(timeInSec);
        }

        public void StartTimer()
        {
            StopTimer(); // Ensure no duplicate timers
            _timer = new Timer(_ =>
            {
                timeInSec++;
                Tick?.Invoke(timeInSec);
            }, null, 1000, 1000); // Start after 1s, repeat every 1s
        }

        public void StopTimer()
        {
            _timer?.Dispose();
            _timer = null;
            timeInSec = 0;
        }
    }
}
