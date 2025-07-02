using FallenTallyAvalon.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.ViewModels
{
    internal class SettingsViewModel
    {
        private List<HotkeyHelper> _hotkeyHelpers;


        public void AddHotkey(HotkeyHelper hotkeyHelper)
        {
            if (_hotkeyHelpers == null)
                _hotkeyHelpers = new List<HotkeyHelper>();

            var nameField = typeof(HotkeyHelper).GetField("_name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var newName = nameField?.GetValue(hotkeyHelper) as string;

            var existing = _hotkeyHelpers.FirstOrDefault(h =>
                string.Equals(
                    nameField?.GetValue(h) as string,
                    newName,
                    StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                _hotkeyHelpers.Remove(existing);
            }
            _hotkeyHelpers.Add(hotkeyHelper);
        }

    }
}
