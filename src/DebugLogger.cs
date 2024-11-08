using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey
{
    public class DebugLogger
    {
        public DebugLogger() 
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public static void Debug(string message)
        {
            if (!Program.debug) return;
            Console.WriteLine(message);
        }

    }
}
