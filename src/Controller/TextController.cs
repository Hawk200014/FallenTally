using DeathCounterHotkey.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class TextController
    {

        public static void CreateDirectory()
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            Directory.CreateDirectory(path);
        }

        public static void WriteDeaths(string prefix, int allDeaths, string location, int locDeaths)
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            path = Path.Combine(path, "Overlay.txt");
            string textContent = prefix + ": " + allDeaths;
            textContent += "\n"+location + ": " + locDeaths;
            File.WriteAllText(path, textContent);
        }
    }
}
