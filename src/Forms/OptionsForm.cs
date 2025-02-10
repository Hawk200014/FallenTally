using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Forms;
using FallenTally.Enums;
using System.Data;
using System.Drawing;
using System.Drawing.Text;

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
            int index = languageCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.LANGUAGE)));
            languageCombo.SelectedIndex = index;
            index = increaseCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.INCREASE_HOTKEY)));
            increaseCombo.SelectedIndex = index;
            index = decreaseCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.DECREASE_HOTKEY)));
            decreaseCombo.SelectedIndex = index;
            index = switchLocationCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.SWITCH_LOCATION_HOTKEY)));
            switchLocationCombo.SelectedIndex = index;
            index = quickAddLocationCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.QUICKADD_LOCATION_HOTKEY)));
            quickAddLocationCombo.SelectedIndex = index;
            index = finishLocationCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FINISH_LOCATION_HOTKEY)));
            finishLocationCombo.SelectedIndex = index;
            index = recordingStartCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.START_RECORDING_TIMER)));
            recordingStartCombo.SelectedIndex = index;
            index = markerNormalCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.MARKER_NORMAL_HOTKEY)));
            markerNormalCombo.SelectedIndex = index;
            index = markerFunnyCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.MARKER_FUNNY_HOTKEY)));
            markerFunnyCombo.SelectedIndex = index;
            index = markerGamingCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.MARKER_GAMING_HOTKEY)));
            markerGamingCombo.SelectedIndex = index;
            index = markerTalkCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.MARKER_TALK_HOTKEY)));
            markerTalkCombo.SelectedIndex = index;
            index = markerPauseCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.MARKER_PAUSE_HOTKEY)));
            markerPauseCombo.SelectedIndex = index;
            twitchNameTb.Text = _controller.GetSetting(nameof(OPTIONS.TWITCH_NAME));
            index = worldAsAllDeathsCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.WORLD_AS_ALL))) == -1 ? 0 : worldAsAllDeathsCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.WORLD_AS_ALL)));
            worldAsAllDeathsCombo.SelectedIndex = index;
            index = fontCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FONTFAMILY))) == -1 ? 0 : fontCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FONTFAMILY)));
            fontCombo.SelectedIndex = index;
            index = fontStyleCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FONTSTYLE))) == -1 ? 0 : fontStyleCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FONTSTYLE)));
            fontStyleCombo.SelectedIndex = index;
            index = fontWeightCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FONTWEIGHT))) == -1 ? 0 : fontWeightCombo.Items.IndexOf(_controller.GetSetting(nameof(OPTIONS.FONTWEIGHT)));
            fontWeightCombo.SelectedIndex = index;
            string tmpStr = _controller.GetSetting(nameof(OPTIONS.FONTSIZE));
            int size = 1;
            if (!string.IsNullOrEmpty(tmpStr))
            {
                size = int.Parse(tmpStr);
            }
            fontSizeNumeric.Value = size;
            tmpStr = _controller.GetSetting(nameof(OPTIONS.BORDERSIZE));
            double borderSize = 1;
            if (!string.IsNullOrEmpty(tmpStr))
            {
                borderSize = double.Parse(tmpStr) * 100;
            }
            borderSizeNumeric.Value = (decimal)borderSize;
            tmpStr = _controller.GetSetting(nameof(OPTIONS.SHADOWSIZE));
            size = 1;
            if (!string.IsNullOrEmpty(tmpStr))
            {
                size = int.Parse(tmpStr);
            }
            shadowSizeNumeric.Value = size;
            string color = _controller.GetSetting(nameof(OPTIONS.TEXTCOLOR)) == "" ? "#000000" : _controller.GetSetting(nameof(OPTIONS.TEXTCOLOR));
            textColorTB.Text = color;
            color = _controller.GetSetting(nameof(OPTIONS.SHADOWCOLOR)) == "" ? "#000000" : _controller.GetSetting(nameof(OPTIONS.SHADOWCOLOR));
            shadowColorTB.Text = color;
            color = _controller.GetSetting(nameof(OPTIONS.BORDERCOLOR)) == "" ? "#000000" : _controller.GetSetting(nameof(OPTIONS.BORDERCOLOR));
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
            this.recordingStartCombo.Items.Clear();
            this.recordingStartCombo.Items.AddRange(GetKeys());
            this.markerNormalCombo.Items.Clear();
            this.markerNormalCombo.Items.AddRange(GetKeys());
            this.markerFunnyCombo.Items.Clear();
            this.markerFunnyCombo.Items.AddRange(GetKeys());
            this.markerGamingCombo.Items.Clear();
            this.markerGamingCombo.Items.AddRange(GetKeys());
            this.markerTalkCombo.Items.Clear();
            this.markerTalkCombo.Items.AddRange(GetKeys());
            this.markerPauseCombo.Items.Clear();
            this.markerPauseCombo.Items.AddRange(GetKeys());

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
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.LANGUAGE), languageCombo.Text))
            {
                SetErrMsg("Incrase can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.INCREASE_HOTKEY), increaseCombo.Text))
            {
                SetErrMsg("Incrase can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.DECREASE_HOTKEY), decreaseCombo.Text))
            {
                SetErrMsg("Decrease can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.SWITCH_LOCATION_HOTKEY), switchLocationCombo.Text))
            {
                SetErrMsg("Switch can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.QUICKADD_LOCATION_HOTKEY), quickAddLocationCombo.Text))
            {
                SetErrMsg("Quick add can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.FINISH_LOCATION_HOTKEY), finishLocationCombo.Text))
            {
                SetErrMsg("Finish location can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.START_RECORDING_TIMER), recordingStartCombo.Text))
            {
                SetErrMsg("Recording can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.MARKER_NORMAL_HOTKEY), markerNormalCombo.Text))
            {
                SetErrMsg("Marker normal can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.MARKER_FUNNY_HOTKEY), markerFunnyCombo.Text))
            {
                SetErrMsg("Marker funny can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.MARKER_GAMING_HOTKEY), markerGamingCombo.Text))
            {
                SetErrMsg("Marker gaming can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.MARKER_TALK_HOTKEY), markerTalkCombo.Text))
            {
                SetErrMsg("Marker talk can't be set");
                return;
            }
            if (!_controller.SetOrEditSetting(nameof(OPTIONS.MARKER_PAUSE_HOTKEY), markerPauseCombo.Text))
            {
                SetErrMsg("Marker pause can't be set");
                return;
            }

            _controller.SetOrEditSetting(nameof(OPTIONS.TWITCH_NAME), twitchNameTb.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.WORLD_AS_ALL), worldAsAllDeathsCombo.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.FONTFAMILY), fontCombo.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.FONTSIZE), "" + fontSizeNumeric.Value);
            _controller.SetOrEditSetting(nameof(OPTIONS.BORDERSIZE), "" + borderSizeNumeric.Value / 100);
            _controller.SetOrEditSetting(nameof(OPTIONS.SHADOWSIZE), "" + shadowSizeNumeric.Value);
            _controller.SetOrEditSetting(nameof(OPTIONS.FONTSTYLE), fontStyleCombo.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.FONTWEIGHT), fontWeightCombo.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.TEXTCOLOR), textColorTB.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.SHADOWCOLOR), shadowColorTB.Text);
            _controller.SetOrEditSetting(nameof(OPTIONS.BORDERCOLOR), borderColorTB.Text);

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