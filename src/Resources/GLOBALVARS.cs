using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Resources
{
    public class GLOBALVARS
    {
        public static readonly string PATHTOEXE = System.Reflection.Assembly.GetEntryAssembly().Location;
        public static readonly string PATHTODATA = Path.Combine(PATHTOEXE, "\\Data");
        public static readonly string PATHTODB = Path.Combine(PATHTODATA, "\\data.db");
    }
}
