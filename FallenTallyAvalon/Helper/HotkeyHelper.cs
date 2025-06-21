using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.Helper
{
    public class HotkeyHelper
    {
        private bool _altKey = false;
        private bool _strgKey = false;
        private Avalonia.Input.Key _key;
        public string Name;

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
