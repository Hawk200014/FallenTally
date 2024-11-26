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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
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
            label3 = new Label();
            twitchNameTb = new TextBox();
            label4 = new Label();
            worldAsAllDeathsCombo = new ComboBox();
            SuspendLayout();
            // 
            // increaseHotkeyLbl
            // 
            increaseHotkeyLbl.AutoSize = true;
            increaseHotkeyLbl.Location = new Point(19, 83);
            increaseHotkeyLbl.Name = "increaseHotkeyLbl";
            increaseHotkeyLbl.Size = new Size(114, 20);
            increaseHotkeyLbl.TabIndex = 1;
            increaseHotkeyLbl.Text = "Increase Hotkey";
            // 
            // decreaseHotkeyLbl
            // 
            decreaseHotkeyLbl.AutoSize = true;
            decreaseHotkeyLbl.Location = new Point(19, 117);
            decreaseHotkeyLbl.Name = "decreaseHotkeyLbl";
            decreaseHotkeyLbl.Size = new Size(121, 20);
            decreaseHotkeyLbl.TabIndex = 2;
            decreaseHotkeyLbl.Text = "Decrease Hotkey";
            // 
            // increaseCombo
            // 
            increaseCombo.BackColor = SystemColors.ControlDark;
            increaseCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            increaseCombo.FlatStyle = FlatStyle.Flat;
            increaseCombo.FormattingEnabled = true;
            increaseCombo.Location = new Point(254, 80);
            increaseCombo.Name = "increaseCombo";
            increaseCombo.Size = new Size(229, 28);
            increaseCombo.TabIndex = 3;
            // 
            // decreaseCombo
            // 
            decreaseCombo.BackColor = SystemColors.ControlDark;
            decreaseCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            decreaseCombo.FlatStyle = FlatStyle.Flat;
            decreaseCombo.FormattingEnabled = true;
            decreaseCombo.Location = new Point(254, 114);
            decreaseCombo.Name = "decreaseCombo";
            decreaseCombo.Size = new Size(229, 28);
            decreaseCombo.TabIndex = 4;
            // 
            // languageCombo
            // 
            languageCombo.BackColor = SystemColors.ControlDark;
            languageCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            languageCombo.FlatStyle = FlatStyle.Flat;
            languageCombo.FormattingEnabled = true;
            languageCombo.Location = new Point(254, 46);
            languageCombo.Name = "languageCombo";
            languageCombo.Size = new Size(229, 28);
            languageCombo.TabIndex = 6;
            // 
            // languageLbl
            // 
            languageLbl.AutoSize = true;
            languageLbl.Location = new Point(19, 49);
            languageLbl.Name = "languageLbl";
            languageLbl.Size = new Size(74, 20);
            languageLbl.TabIndex = 5;
            languageLbl.Text = "Language";
            // 
            // switchLocationCombo
            // 
            switchLocationCombo.BackColor = SystemColors.ControlDark;
            switchLocationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            switchLocationCombo.FlatStyle = FlatStyle.Flat;
            switchLocationCombo.FormattingEnabled = true;
            switchLocationCombo.Location = new Point(254, 148);
            switchLocationCombo.Name = "switchLocationCombo";
            switchLocationCombo.Size = new Size(229, 28);
            switchLocationCombo.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 151);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 7;
            label1.Text = "Switch Location";
            // 
            // quickAddLocationCombo
            // 
            quickAddLocationCombo.BackColor = SystemColors.ControlDark;
            quickAddLocationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            quickAddLocationCombo.FlatStyle = FlatStyle.Flat;
            quickAddLocationCombo.FormattingEnabled = true;
            quickAddLocationCombo.Location = new Point(254, 182);
            quickAddLocationCombo.Name = "quickAddLocationCombo";
            quickAddLocationCombo.Size = new Size(229, 28);
            quickAddLocationCombo.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 185);
            label2.Name = "label2";
            label2.Size = new Size(108, 20);
            label2.TabIndex = 9;
            label2.Text = "Quick Add Loc.";
            // 
            // saveBtn
            // 
            saveBtn.BackColor = SystemColors.ControlDark;
            saveBtn.FlatAppearance.BorderSize = 0;
            saveBtn.FlatStyle = FlatStyle.Flat;
            saveBtn.Location = new Point(289, 261);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 11;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = false;
            saveBtn.Click += saveBtn_Click;
            // 
            // cancleBtn
            // 
            cancleBtn.BackColor = SystemColors.ControlDark;
            cancleBtn.FlatAppearance.BorderSize = 0;
            cancleBtn.FlatStyle = FlatStyle.Flat;
            cancleBtn.Location = new Point(389, 261);
            cancleBtn.Name = "cancleBtn";
            cancleBtn.Size = new Size(94, 29);
            cancleBtn.TabIndex = 12;
            cancleBtn.Text = "Cancel";
            cancleBtn.UseVisualStyleBackColor = false;
            cancleBtn.Click += cancleBtn_Click;
            // 
            // errLbl
            // 
            errLbl.AutoSize = true;
            errLbl.ForeColor = Color.Red;
            errLbl.Location = new Point(19, 265);
            errLbl.Name = "errLbl";
            errLbl.Size = new Size(47, 20);
            errLbl.TabIndex = 13;
            errLbl.Text = "ErrLbl";
            errLbl.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 14);
            label3.Name = "label3";
            label3.Size = new Size(121, 20);
            label3.TabIndex = 14;
            label3.Text = "Twitch Username";
            // 
            // twitchNameTb
            // 
            twitchNameTb.BackColor = SystemColors.ControlDark;
            twitchNameTb.BorderStyle = BorderStyle.FixedSingle;
            twitchNameTb.Location = new Point(254, 12);
            twitchNameTb.Name = "twitchNameTb";
            twitchNameTb.Size = new Size(229, 27);
            twitchNameTb.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 219);
            label4.Name = "label4";
            label4.Size = new Size(231, 20);
            label4.TabIndex = 16;
            label4.Text = "Show World Deaths As All Deaths";
            // 
            // worldAsAllDeathsCombo
            // 
            worldAsAllDeathsCombo.BackColor = SystemColors.ControlDark;
            worldAsAllDeathsCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            worldAsAllDeathsCombo.FlatStyle = FlatStyle.Flat;
            worldAsAllDeathsCombo.FormattingEnabled = true;
            worldAsAllDeathsCombo.Location = new Point(254, 216);
            worldAsAllDeathsCombo.Name = "worldAsAllDeathsCombo";
            worldAsAllDeathsCombo.Size = new Size(229, 28);
            worldAsAllDeathsCombo.TabIndex = 17;
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(495, 309);
            Controls.Add(worldAsAllDeathsCombo);
            Controls.Add(label4);
            Controls.Add(twitchNameTb);
            Controls.Add(label3);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Label label3;
        private TextBox twitchNameTb;
        private Label label4;
        private ComboBox worldAsAllDeathsCombo;
    }
}