using SharpHook.Data;

namespace FallenTallyAvalon.Helper
{
    public class HotkeyHelper
    {
        public bool _altKey { get; set; } = false;
        public bool _strgKey { get; set; } = false;
        public KeyCode _key { get; set; }
        public string Name { get; set; }


        public HotkeyHelper()
        {
        }


        public void SetKey(KeyCode key )
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

        public override bool Equals(object? obj)
        {
            if (obj is not HotkeyHelper other)
                return false;
            return _key == other._key &&
                   _altKey == other._altKey &&
                   _strgKey == other._strgKey;
        }

    }
}
