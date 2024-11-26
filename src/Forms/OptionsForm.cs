using DeathCounterHotkey.Controller.Forms;
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
        private OptionsController _controller;

        public OptionsForm(OptionsController controller, Action optionsChangedAction)
        {
            InitializeComponent();
            this._optionsChangedAction = optionsChangedAction;
            this._controller = controller;

        }

        private void LoadOptions()
        {
            int index = languageCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.LANGUAGE)));
            languageCombo.SelectedIndex = index;
            index = increaseCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.INCREASE_HOTKEY)));
            increaseCombo.SelectedIndex = index;
            index = decreaseCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.DECREASE_HOTKEY)));
            decreaseCombo.SelectedIndex = index;
            index = switchLocationCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.SWITCH_LOCATION_HOTKEY)));
            switchLocationCombo.SelectedIndex = index;
            index = quickAddLocationCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.QUICKADD_LOCATION_HOTKEY)));
            quickAddLocationCombo.SelectedIndex = index;
            twitchNameTb.Text = _controller.GetSetting(nameof(OptionsController.OPTIONS.TWITCH_NAME));
            index = worldAsAllDeathsCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.WORLD_AS_ALL)));
            worldAsAllDeathsCombo.SelectedIndex = index;
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
            this.switchLocationCombo.Items.Clear();
            this.switchLocationCombo.Items.AddRange(GetKeys());
            this.quickAddLocationCombo.Items.Clear();
            this.quickAddLocationCombo.Items.AddRange(GetKeys());
            this.worldAsAllDeathsCombo.Items.Clear();
            this.worldAsAllDeathsCombo.Items.AddRange(GetYesNo());
            this.worldAsAllDeathsCombo.SelectedIndex = 1;
            SetLanguage();
            LoadOptions();
            this.ResumeLayout(false);

        }

        private void SetLanguage()
        {
            switch (this._controller.GetLanguage())
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

        private string[] GetYesNo() 
        {
            return new[]
            {
                "Yes",
                "No"
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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!_controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.LANGUAGE), languageCombo.Text))
            {
                SetErrMsg("Incrase can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.INCREASE_HOTKEY), increaseCombo.Text))
            {
                SetErrMsg("Incrase can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.DECREASE_HOTKEY), decreaseCombo.Text))
            {
                SetErrMsg("Decrease can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.SWITCH_LOCATION_HOTKEY), switchLocationCombo.Text))
            {
                SetErrMsg("Switch can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.QUICKADD_LOCATION_HOTKEY), quickAddLocationCombo.Text))
            {
                SetErrMsg("Quick add can't be set");
                return;
            }
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.TWITCH_NAME), twitchNameTb.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.WORLD_AS_ALL), worldAsAllDeathsCombo.Text);
            
            _optionsChangedAction?.Invoke();
            this.Close();
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetErrMsg(string msg)
        {
            this.errLbl.Text = msg;
            this.errLbl.Visible = true;
        }

    }
}
