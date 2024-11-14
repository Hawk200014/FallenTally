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
            SuspendLayout();
            // 
            // editTextBox
            // 
            editTextBox.Location = new Point(12, 12);
            editTextBox.Name = "editTextBox";
            editTextBox.Size = new Size(324, 27);
            editTextBox.TabIndex = 0;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(241, 45);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(94, 29);
            cancelBtn.TabIndex = 1;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(141, 45);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 2;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 85);
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
    }
}