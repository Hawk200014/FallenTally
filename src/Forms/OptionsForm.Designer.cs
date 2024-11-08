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
            increaseCombo.SelectedIndexChanged += increaseCombo_SelectedIndexChanged;
            // 
            // decreaseCombo
            // 
            decreaseCombo.FormattingEnabled = true;
            decreaseCombo.Location = new Point(142, 81);
            decreaseCombo.Name = "decreaseCombo";
            decreaseCombo.Size = new Size(229, 28);
            decreaseCombo.TabIndex = 4;
            decreaseCombo.SelectedIndexChanged += decreaseCombo_SelectedIndexChanged;
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
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(383, 121);
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
    }
}