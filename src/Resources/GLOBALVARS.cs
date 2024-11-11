using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Resources
{
    internal class GLOBALVARS
    {
        private static readonly string PATHTOEXE = System.Reflection.Assembly.GetEntryAssembly().Location;
        private static readonly string PATHTODATA = Path.Combine(PATHTOEXE, "\\Data");
        private static readonly string PATHTODB = Path.Combine(PATHTODATA, "\\data.db");
    }
}
