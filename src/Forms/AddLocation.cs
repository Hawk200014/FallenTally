using DeathCounterHotkey.Controller.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeathCounterHotkey.Forms
{
    public class AddLocation : Form
    {
        private Action _action;
        private AddLocationController _controller;

        public AddLocation(AddLocationController controller, Action updateListAction)
        {
            InitializeComponent();
            this._action = updateListAction;
            this._controller = controller;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string locationName = locationNameTxtb.Text.Trim();
            if (this._controller.AddLocation(locationName))
            {
                _action?.Invoke();
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
