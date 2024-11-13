using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class StreamTimeController
    {

        public StreamTimeController() { }

        public int GetTime()
        {
            return 0;
        }

        public string ConvertTimeToReadableTime(int time)
        {
            return "" + Math.Floor((double)time / 3600) + ":" + Math.Floor((double)time / 60) + ":" + time % 60;
        }
    }
}
