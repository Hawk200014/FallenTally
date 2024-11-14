using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database.Models;
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
        private Action<GameStatsModel?> _action;
        private Action _addDefaultLocationAction;
        private GameController _controller;

        public AddGameForm(GameController controller, Action<GameStatsModel?> updateListAction, Action addDefaultLocationAction)
        {
            InitializeComponent();
            this._action = updateListAction;
            this._addDefaultLocationAction = addDefaultLocationAction;
            this._controller = controller;
            this.prefixTxtb.Text = "Death Counter";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string gamename = gameNameTxtb.Text.Trim();
            string prefix = prefixTxtb.Text.Trim();
            if (this._controller.AddGame(gamename, prefix))
            {                
                _action.Invoke(_controller.GetGame(gamename));
                _addDefaultLocationAction.Invoke();
                this.Close();
            }
            else
            {
                //Todo Message of double Entry
            }

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
