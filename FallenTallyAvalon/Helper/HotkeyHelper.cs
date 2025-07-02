using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.Helper
{
    public class HotkeyHelper
    {
        public bool _altKey { get; set; } = false;
        public bool _strgKey { get; set; } = false;
        public Avalonia.Input.Key _key { get; set; }
        public string Name { get; set; }

        public HotkeyHelper(string name)
        {
            Name = name;
        }


        public void SetKey(Avalonia.Input.Key key )
        {
            _key = key;
        }

        public void SetKeyModifier(Avalonia.Input.KeyModifiers modifiers)
        {
            if(modifiers == Avalonia.Input.KeyModifiers.Alt)
            {
                _altKey = true;
            }
            if(modifiers == Avalonia.Input.KeyModifiers.Control)
            {
                _strgKey = true;
            }
        }

        public override string ToString()
        {
            string strg = _strgKey ? "STRG + " : "";
            string alt = _altKey ? "ALT + " : "";
            string key = _key.ToString().ToUpper();
            return strg + alt + key;
        }

    }
}
