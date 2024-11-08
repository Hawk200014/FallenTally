using DeathCounterHotkey.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeathCounterHotkey
{
    public partial class AddGameForm : Form
    {
        private Action? _action;
        private Settings _settings;

        public AddGameForm(Settings settings, Action? updateListAction)
        {
            InitializeComponent();
            this._action = updateListAction;
            this._settings = settings;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string gamename = gameNameTxtb.Text.Trim();
            string prefix = prefixTxtb.Text.Trim();
            if (!MainForm.GameExists(gamename))
            {
                GameStatsModel model = new GameStatsModel(gamename, prefix, 0);
                this._settings.SaveGameStats(model);
                _action?.Invoke();
                this.Close();
            }
            
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
