namespace DeathCounterHotkey.Forms
{
    partial class AddLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLocation));
            cancelBtn = new Button();
            saveBtn = new Button();
            locationNameTxtb = new TextBox();
            locationLbl = new Label();
            SuspendLayout();
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(264, 45);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(94, 29);
            cancelBtn.TabIndex = 11;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(164, 45);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 8;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // locationNameTxtb
            // 
            locationNameTxtb.Location = new Point(128, 12);
            locationNameTxtb.Name = "locationNameTxtb";
            locationNameTxtb.Size = new Size(230, 27);
            locationNameTxtb.TabIndex = 7;
            // 
            // locationLbl
            // 
            locationLbl.AutoSize = true;
            locationLbl.Location = new Point(12, 15);
            locationLbl.Name = "locationLbl";
            locationLbl.Size = new Size(110, 20);
            locationLbl.TabIndex = 6;
            locationLbl.Text = "Location Name";
            // 
            // AddLocation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 82);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(locationNameTxtb);
            Controls.Add(locationLbl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddLocation";
            Text = "DeathHotkey - Add Location";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancelBtn;
        private Button saveBtn;
        private TextBox locationNameTxtb;
        private Label locationLbl;
    }
}