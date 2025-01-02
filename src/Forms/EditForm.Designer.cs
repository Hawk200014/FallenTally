namespace DeathCounterHotkey.Forms
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            editTextBox = new TextBox();
            cancelBtn = new Button();
            saveBtn = new Button();
            finishedCB = new CheckBox();
            SuspendLayout();
            // 
            // editTextBox
            // 
            editTextBox.BackColor = SystemColors.ControlDark;
            editTextBox.BorderStyle = BorderStyle.FixedSingle;
            editTextBox.Location = new Point(12, 12);
            editTextBox.Name = "editTextBox";
            editTextBox.Size = new Size(324, 27);
            editTextBox.TabIndex = 0;
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = SystemColors.ControlDark;
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.Location = new Point(241, 45);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(94, 29);
            cancelBtn.TabIndex = 1;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.BackColor = SystemColors.ControlDark;
            saveBtn.FlatAppearance.BorderSize = 0;
            saveBtn.FlatStyle = FlatStyle.Flat;
            saveBtn.Location = new Point(141, 45);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 2;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = false;
            saveBtn.Click += saveBtn_Click;
            // 
            // finishedCB
            // 
            finishedCB.AutoSize = true;
            finishedCB.FlatStyle = FlatStyle.Flat;
            finishedCB.Location = new Point(12, 48);
            finishedCB.Name = "finishedCB";
            finishedCB.Size = new Size(81, 24);
            finishedCB.TabIndex = 3;
            finishedCB.Text = "Finished";
            finishedCB.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(347, 85);
            Controls.Add(finishedCB);
            Controls.Add(saveBtn);
            Controls.Add(cancelBtn);
            Controls.Add(editTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditForm";
            Text = "DeathHotkey - Edit";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox editTextBox;
        private Button cancelBtn;
        private Button saveBtn;
        private CheckBox finishedCB;
    }
}