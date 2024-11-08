using DeathCounterHotkey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey
{
    public static class Utilities
    {
        public static string EscapeSpecialChars(string s)
        {
            return s.Replace("ä", "ae")
                .Replace("ö", "oe")
                .Replace("ü", "ue")
                .Replace(" ", "_")
                .Replace("ß", "ss")
                .Replace("-", "_");
        }
    }
}
