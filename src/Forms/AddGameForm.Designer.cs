namespace DeathCounterHotkey
{
    partial class AddGameForm
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
            label1 = new Label();
            gameNameTxtb = new TextBox();
            saveBtn = new Button();
            label2 = new Label();
            prefixTxtb = new TextBox();
            cancelBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(92, 20);
            label1.TabIndex = 0;
            label1.Text = "Game Name";
            // 
            // gameNameTxtb
            // 
            gameNameTxtb.Location = new Point(106, 12);
            gameNameTxtb.Name = "gameNameTxtb";
            gameNameTxtb.Size = new Size(230, 27);
            gameNameTxtb.TabIndex = 1;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(142, 82);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 2;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 3;
            label2.Text = "Prefix";
            // 
            // prefixTxtb
            // 
            prefixTxtb.Location = new Point(106, 51);
            prefixTxtb.Name = "prefixTxtb";
            prefixTxtb.Size = new Size(230, 27);
            prefixTxtb.TabIndex = 4;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(242, 82);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(94, 29);
            cancelBtn.TabIndex = 5;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // AddGameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 123);
            Controls.Add(cancelBtn);
            Controls.Add(prefixTxtb);
            Controls.Add(label2);
            Controls.Add(saveBtn);
            Controls.Add(gameNameTxtb);
            Controls.Add(label1);
            Name = "AddGameForm";
            Text = "DeathHotkey- Add Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox gameNameTxtb;
        private Button saveBtn;
        private Label label2;
        private TextBox prefixTxtb;
        private Button cancelBtn;
    }
}