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
using static DeathCounterHotkey.Controller.Forms.EditController;

namespace DeathCounterHotkey.Forms
{
    public partial class EditForm : Form
    {
        private EditController _controller;
        private Action<string> _action;
        private EditController.EDITCATEGORIE _editCat;

        public EditForm(string oldValue, EditController controller, EditController.EDITCATEGORIE editCategorie, Action<string> action)
        {
            InitializeComponent();
            this._controller = controller;
            this._action = action;
            this._editCat = editCategorie;
            this.editTextBox.Text = oldValue;
            if(editCategorie == EditController.EDITCATEGORIE.LOCATION)
            {
                this.finishedCB.Visible = true;
                finishedCB.Checked =  _controller.GetCheckedState(oldValue);
            }
            else
            {
                this.finishedCB.Visible = false;
            }

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string editText = editTextBox.Text.Trim();
            bool closeForm;
            if (_editCat == EditController.EDITCATEGORIE.LOCATION)
            {
                closeForm = this._controller.AddEdit(editText, _editCat, finishedCB.Checked);
            }
            else
            {
                closeForm = this._controller.AddEdit(editText, _editCat);
            }


            if (closeForm)
            {
                _action?.Invoke(editText);
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
