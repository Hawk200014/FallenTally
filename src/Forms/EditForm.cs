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
    public partial class EditForm : Form
    {
        private EditController _controller;
        private Action<string> _action;
        private EditController.EDITCATEGORIE _editCat;
        private string oldValue;

        public EditForm(string oldValue, EditController controller, EditController.EDITCATEGORIE editCategorie, Action<string> action)
        {
            InitializeComponent();
            this._controller = controller;
            this._action = action;
            this._editCat = editCategorie;
            this.editTextBox.Text = oldValue;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string editText = editTextBox.Text.Trim();
            if (this._controller.AddEdit(editText, _editCat))
            {
                _action?.Invoke("");
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
