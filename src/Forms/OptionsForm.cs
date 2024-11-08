using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeathCounterHotkey
{
    public partial class OptionsForm : Form
    {
        private Action _optionsChangedAction;
        private Settings _settings;
        public OptionsForm(Settings settings, Action optionsChangedAction)
        {
            InitializeComponent();
            this._optionsChangedAction = optionsChangedAction;
            this._settings = settings;
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.increaseCombo.Items.Clear();
            this.increaseCombo.Items.AddRange(GetKeys());
            this.decreaseCombo.Items.Clear();
            this.decreaseCombo.Items.AddRange(GetKeys());
            this.languageCombo.Items.Clear();
            this.languageCombo.Items.AddRange(GetLanguages());
            SetLanguage();
            this.ResumeLayout(false);
        }

        private void SetLanguage()
        {
            switch (this._settings.GetLanguage())
            {
                case "de":
                    this.languageCombo.SelectedIndex = 0;
                    break;
                case "en":
                    this.languageCombo.SelectedIndex = 1;
                    break;
            }
            
        }
        
        private string[] GetLanguages()
        {
            return new[]
            {
                "Deutsch",
                "English"
            };
        }

        private string[] GetKeys()
        {
            return new[]
            {
                "F13",
                "F14",
                "F15",
                "F16",
                "F17",
                "F18",
                "F19",
                "F20",
                "F21",
                "F22",
                "F23",
                "F24",
            };
        }

        private void increaseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.increaseCombo.SelectedIndex;

        }

        private void decreaseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.increaseCombo.SelectedIndex;

        }
    }
}
