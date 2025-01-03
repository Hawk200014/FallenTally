using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace DeathCounterHotkey
{
    public partial class OptionsForm : Form
    {
        private Action _optionsChangedAction;
        private OptionsController _controller;
        private ExportController _exportController;

        public OptionsForm(OptionsController controller, Action optionsChangedAction, ExportController exportController)
        {
            InitializeComponent();
            this._optionsChangedAction = optionsChangedAction;
            this._controller = controller;
            this._exportController = exportController;
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
            index = finishLocationCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FINISH_LOCATION_HOTKEY)));
            finishLocationCombo.SelectedIndex = index;
            twitchNameTb.Text = _controller.GetSetting(nameof(OptionsController.OPTIONS.TWITCH_NAME));
            index = worldAsAllDeathsCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.WORLD_AS_ALL))) == -1 ? 0 : worldAsAllDeathsCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.WORLD_AS_ALL)));
            worldAsAllDeathsCombo.SelectedIndex = index;
            index = fontCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FONTFAMILY))) == -1 ? 0 : fontCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FONTFAMILY)));
            fontCombo.SelectedIndex = index;
            index = fontStyleCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FONTSTYLE))) == -1 ? 0 : fontStyleCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FONTSTYLE)));
            fontStyleCombo.SelectedIndex = index;
            index = fontWeightCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FONTWEIGHT))) == -1 ? 0 : fontWeightCombo.Items.IndexOf(_controller.GetSetting(nameof(OptionsController.OPTIONS.FONTWEIGHT)));
            fontWeightCombo.SelectedIndex = index;
            string tmpStr = _controller.GetSetting(nameof(OptionsController.OPTIONS.FONTSIZE));
            int size = 1;
            if (!string.IsNullOrEmpty(tmpStr))
            {
                size = int.Parse(tmpStr);
            }
            fontSizeNumeric.Value = size;
            tmpStr = _controller.GetSetting(nameof(OptionsController.OPTIONS.BORDERSIZE));
            double borderSize = 1;
            if (!string.IsNullOrEmpty(tmpStr))
            {
                borderSize = double.Parse(tmpStr) * 100;
            }
            borderSizeNumeric.Value = (decimal)borderSize;
            tmpStr = _controller.GetSetting(nameof(OptionsController.OPTIONS.SHADOWSIZE));
            size = 1;
            if (!string.IsNullOrEmpty(tmpStr))
            {
                size = int.Parse(tmpStr);
            }
            shadowSizeNumeric.Value = size;
            string color = _controller.GetSetting(nameof(OptionsController.OPTIONS.TEXTCOLOR)) == "" ? "#000000" : _controller.GetSetting(nameof(OptionsController.OPTIONS.TEXTCOLOR));
            textColorTB.Text = color;
            color = _controller.GetSetting(nameof(OptionsController.OPTIONS.SHADOWCOLOR)) == "" ? "#000000" : _controller.GetSetting(nameof(OptionsController.OPTIONS.SHADOWCOLOR));
            shadowColorTB.Text = color;
            color = _controller.GetSetting(nameof(OptionsController.OPTIONS.BORDERCOLOR)) == "" ? "#000000" : _controller.GetSetting(nameof(OptionsController.OPTIONS.BORDERCOLOR));
            borderColorTB.Text = color;



        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.increaseCombo.Items.Clear();
            this.increaseCombo.Items.AddRange(GetKeys());
            this.decreaseCombo.Items.Clear();
            this.decreaseCombo.Items.AddRange(GetKeys());
            this.finishLocationCombo.Items.Clear();
            this.finishLocationCombo.Items.AddRange(GetKeys());
            this.languageCombo.Items.Clear();
            this.languageCombo.Items.AddRange(GetLanguages());
            this.switchLocationCombo.Items.Clear();
            this.switchLocationCombo.Items.AddRange(GetKeys());
            this.quickAddLocationCombo.Items.Clear();
            this.quickAddLocationCombo.Items.AddRange(GetKeys());
            this.worldAsAllDeathsCombo.Items.Clear();
            this.worldAsAllDeathsCombo.Items.AddRange(GetYesNo());
            this.worldAsAllDeathsCombo.SelectedIndex = 1;
            this.fontCombo.Items.Clear();
            this.fontCombo.Items.AddRange(GetInstalledFonts());
            this.fontCombo.SelectedIndex = 0;
            this.fontStyleCombo.Items.Clear();
            this.fontStyleCombo.Items.AddRange(GetFontStyles());
            this.fontCombo.SelectedIndex = 0;
            this.fontWeightCombo.Items.Clear();
            this.fontWeightCombo.Items.AddRange(GetFontWeight());
            this.fontCombo.SelectedIndex = 0;
            SetLanguage();
            LoadOptions();
            this.ResumeLayout(false);
        }

        private string[] GetFontStyles()
        {
            return new[]
            {
                "normal",
                "italic",
                "oblique"
            };
        }

        private string[] GetFontWeight()
        {
            return new[]
            {
                "normal",
                "bold"
            };
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

        private string[] GetInstalledFonts()
        {
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            var fontFamilies = installedFontCollection.Families;
            return fontFamilies.Select(f => f.Name).ToArray();
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
            if (!_controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.FINISH_LOCATION_HOTKEY), finishLocationCombo.Text))
            {
                SetErrMsg("Finish location can't be set");
                return;
            }
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.TWITCH_NAME), twitchNameTb.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.WORLD_AS_ALL), worldAsAllDeathsCombo.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.FONTFAMILY), fontCombo.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.FONTSIZE), "" + fontSizeNumeric.Value);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.BORDERSIZE), "" + borderSizeNumeric.Value / 100);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.SHADOWSIZE), "" + shadowSizeNumeric.Value);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.FONTSTYLE), fontStyleCombo.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.FONTWEIGHT), fontWeightCombo.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.TEXTCOLOR), textColorTB.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.SHADOWCOLOR), shadowColorTB.Text);
            _controller.SetOrEditSetting(nameof(OptionsController.OPTIONS.BORDERCOLOR), borderColorTB.Text);

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

        private void textColorTB_TextChanged(object sender, EventArgs e)
        {
            textColorTB.Text = GetColorWithPicker(textColorTB.Text);
        }

        private void outlineColorTB_TextChanged(object sender, EventArgs e)
        {
            borderColorTB.Text = GetColorWithPicker(borderColorTB.Text);
        }

        private void shadowColorTB_TextChanged(object sender, EventArgs e)
        {
            shadowColorTB.Text = GetColorWithPicker(shadowColorTB.Text);
        }

        private string GetColorWithPicker(string oldColor)
        {
            if (string.IsNullOrEmpty(oldColor))
            {
                oldColor = "#000000";
            }
            ColorDialog colorDialog = new ColorDialog();
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(oldColor);
            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            colorDialog.Color = newColor;
            DialogResult result = colorDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                return ToHex(colorDialog.Color);
            }
            return oldColor;
        }

        private static String ToHex(System.Drawing.Color c)
            => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

        private void textColorTB_Click(object sender, EventArgs e)
        {
            textColorTB.Text = GetColorWithPicker(textColorTB.Text);
        }

        private void borderColorTB_Click(object sender, EventArgs e)
        {
            borderColorTB.Text = GetColorWithPicker(borderColorTB.Text);
        }

        private void shadowColorTB_Click(object sender, EventArgs e)
        {
            shadowColorTB.Text = GetColorWithPicker(shadowColorTB.Text);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            new ExportForm(this._exportController).Show(this);
        }
    }
}