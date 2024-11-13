namespace DeathCounterHotkey
{
    partial class OptionsForm
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
            increaseHotkeyLbl = new Label();
            decreaseHotkeyLbl = new Label();
            increaseCombo = new ComboBox();
            decreaseCombo = new ComboBox();
            languageCombo = new ComboBox();
            languageLbl = new Label();
            switchLocationCombo = new ComboBox();
            label1 = new Label();
            quickAddLocationCombo = new ComboBox();
            label2 = new Label();
            saveBtn = new Button();
            cancleBtn = new Button();
            errLbl = new Label();
            SuspendLayout();
            // 
            // increaseHotkeyLbl
            // 
            increaseHotkeyLbl.AutoSize = true;
            increaseHotkeyLbl.Location = new Point(12, 50);
            increaseHotkeyLbl.Name = "increaseHotkeyLbl";
            increaseHotkeyLbl.Size = new Size(114, 20);
            increaseHotkeyLbl.TabIndex = 1;
            increaseHotkeyLbl.Text = "Increase Hotkey";
            // 
            // decreaseHotkeyLbl
            // 
            decreaseHotkeyLbl.AutoSize = true;
            decreaseHotkeyLbl.Location = new Point(12, 84);
            decreaseHotkeyLbl.Name = "decreaseHotkeyLbl";
            decreaseHotkeyLbl.Size = new Size(121, 20);
            decreaseHotkeyLbl.TabIndex = 2;
            decreaseHotkeyLbl.Text = "Decrease Hotkey";
            // 
            // increaseCombo
            // 
            increaseCombo.FormattingEnabled = true;
            increaseCombo.Location = new Point(142, 47);
            increaseCombo.Name = "increaseCombo";
            increaseCombo.Size = new Size(229, 28);
            increaseCombo.TabIndex = 3;
            // 
            // decreaseCombo
            // 
            decreaseCombo.FormattingEnabled = true;
            decreaseCombo.Location = new Point(142, 81);
            decreaseCombo.Name = "decreaseCombo";
            decreaseCombo.Size = new Size(229, 28);
            decreaseCombo.TabIndex = 4;
            // 
            // languageCombo
            // 
            languageCombo.FormattingEnabled = true;
            languageCombo.Location = new Point(142, 13);
            languageCombo.Name = "languageCombo";
            languageCombo.Size = new Size(229, 28);
            languageCombo.TabIndex = 6;
            // 
            // languageLbl
            // 
            languageLbl.AutoSize = true;
            languageLbl.Location = new Point(12, 16);
            languageLbl.Name = "languageLbl";
            languageLbl.Size = new Size(74, 20);
            languageLbl.TabIndex = 5;
            languageLbl.Text = "Language";
            // 
            // switchLocationCombo
            // 
            switchLocationCombo.FormattingEnabled = true;
            switchLocationCombo.Location = new Point(142, 115);
            switchLocationCombo.Name = "switchLocationCombo";
            switchLocationCombo.Size = new Size(229, 28);
            switchLocationCombo.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 118);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 7;
            label1.Text = "Switch Location";
            // 
            // quickAddLocationCombo
            // 
            quickAddLocationCombo.FormattingEnabled = true;
            quickAddLocationCombo.Location = new Point(142, 149);
            quickAddLocationCombo.Name = "quickAddLocationCombo";
            quickAddLocationCombo.Size = new Size(229, 28);
            quickAddLocationCombo.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 152);
            label2.Name = "label2";
            label2.Size = new Size(108, 20);
            label2.TabIndex = 9;
            label2.Text = "Quick Add Loc.";
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(177, 185);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 11;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // cancleBtn
            // 
            cancleBtn.Location = new Point(277, 185);
            cancleBtn.Name = "cancleBtn";
            cancleBtn.Size = new Size(94, 29);
            cancleBtn.TabIndex = 12;
            cancleBtn.Text = "Cancel";
            cancleBtn.UseVisualStyleBackColor = true;
            cancleBtn.Click += cancleBtn_Click;
            // 
            // errLbl
            // 
            errLbl.AutoSize = true;
            errLbl.ForeColor = Color.Red;
            errLbl.Location = new Point(12, 189);
            errLbl.Name = "errLbl";
            errLbl.Size = new Size(47, 20);
            errLbl.TabIndex = 13;
            errLbl.Text = "ErrLbl";
            errLbl.Visible = false;
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(383, 226);
            Controls.Add(errLbl);
            Controls.Add(cancleBtn);
            Controls.Add(saveBtn);
            Controls.Add(quickAddLocationCombo);
            Controls.Add(label2);
            Controls.Add(switchLocationCombo);
            Controls.Add(label1);
            Controls.Add(languageCombo);
            Controls.Add(languageLbl);
            Controls.Add(decreaseCombo);
            Controls.Add(increaseCombo);
            Controls.Add(decreaseHotkeyLbl);
            Controls.Add(increaseHotkeyLbl);
            Name = "OptionsForm";
            Text = "DeathHotkey - Options";
            Load += OptionsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label increaseHotkeyLbl;
        private Label decreaseHotkeyLbl;
        private ComboBox increaseCombo;
        private ComboBox decreaseCombo;
        private ComboBox languageCombo;
        private Label languageLbl;
        private ComboBox switchLocationCombo;
        private Label label1;
        private ComboBox quickAddLocationCombo;
        private Label label2;
        private Button saveBtn;
        private Button cancleBtn;
        private Label errLbl;
    }
}