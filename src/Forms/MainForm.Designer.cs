namespace DeathCounterHotkey;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        pretextTxtb = new TextBox();
        deathCountTxtb = new TextBox();
        gameSelectCombo = new ComboBox();
        locationCombo = new ComboBox();
        locationDeathCountTxtb = new TextBox();
        syncTimerBtn = new FontAwesome.Sharp.IconButton();
        panel1 = new Panel();
        panel4 = new Panel();
        optionsBtn = new FontAwesome.Sharp.IconButton();
        panel3 = new Panel();
        recordingTimeLbl = new Label();
        streamTimeLbl = new Label();
        panel2 = new Panel();
        editGameBtn = new FontAwesome.Sharp.IconButton();
        addGameBtn = new FontAwesome.Sharp.IconButton();
        removeGame = new FontAwesome.Sharp.IconButton();
        editLocationBtn = new FontAwesome.Sharp.IconButton();
        resetBtn = new FontAwesome.Sharp.IconButton();
        increaseBtn = new FontAwesome.Sharp.IconButton();
        addLocationBtn = new FontAwesome.Sharp.IconButton();
        decreaseBtn = new FontAwesome.Sharp.IconButton();
        removeLocationbtn = new FontAwesome.Sharp.IconButton();
        markerRTB = new RichTextBox();
        panel1.SuspendLayout();
        panel4.SuspendLayout();
        panel3.SuspendLayout();
        panel2.SuspendLayout();
        SuspendLayout();
        // 
        // pretextTxtb
        // 
        pretextTxtb.BackColor = SystemColors.ControlDark;
        pretextTxtb.BorderStyle = BorderStyle.FixedSingle;
        pretextTxtb.Font = new Font("Segoe UI", 10F);
        pretextTxtb.Location = new Point(10, 170);
        pretextTxtb.Name = "pretextTxtb";
        pretextTxtb.ReadOnly = true;
        pretextTxtb.Size = new Size(312, 30);
        pretextTxtb.TabIndex = 1;
        // 
        // deathCountTxtb
        // 
        deathCountTxtb.BackColor = SystemColors.ControlDark;
        deathCountTxtb.BorderStyle = BorderStyle.FixedSingle;
        deathCountTxtb.Font = new Font("Segoe UI", 10F);
        deathCountTxtb.Location = new Point(328, 170);
        deathCountTxtb.Name = "deathCountTxtb";
        deathCountTxtb.ReadOnly = true;
        deathCountTxtb.RightToLeft = RightToLeft.No;
        deathCountTxtb.Size = new Size(128, 30);
        deathCountTxtb.TabIndex = 2;
        // 
        // gameSelectCombo
        // 
        gameSelectCombo.BackColor = SystemColors.ControlDark;
        gameSelectCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        gameSelectCombo.FlatStyle = FlatStyle.Flat;
        gameSelectCombo.Font = new Font("Segoe UI", 10F);
        gameSelectCombo.ForeColor = SystemColors.WindowText;
        gameSelectCombo.FormattingEnabled = true;
        gameSelectCombo.Location = new Point(10, 96);
        gameSelectCombo.Name = "gameSelectCombo";
        gameSelectCombo.Size = new Size(446, 31);
        gameSelectCombo.TabIndex = 6;
        gameSelectCombo.SelectedIndexChanged += gameSelectCombo_SelectedIndexChanged;
        // 
        // locationCombo
        // 
        locationCombo.BackColor = SystemColors.ControlDark;
        locationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        locationCombo.FlatStyle = FlatStyle.Flat;
        locationCombo.Font = new Font("Segoe UI", 10F);
        locationCombo.FormattingEnabled = true;
        locationCombo.Location = new Point(10, 133);
        locationCombo.Name = "locationCombo";
        locationCombo.Size = new Size(312, 31);
        locationCombo.TabIndex = 10;
        locationCombo.SelectedIndexChanged += locationCombo_SelectedIndexChanged;
        // 
        // locationDeathCountTxtb
        // 
        locationDeathCountTxtb.BackColor = SystemColors.ControlDark;
        locationDeathCountTxtb.BorderStyle = BorderStyle.FixedSingle;
        locationDeathCountTxtb.Font = new Font("Segoe UI", 10F);
        locationDeathCountTxtb.Location = new Point(328, 133);
        locationDeathCountTxtb.Name = "locationDeathCountTxtb";
        locationDeathCountTxtb.ReadOnly = true;
        locationDeathCountTxtb.Size = new Size(128, 30);
        locationDeathCountTxtb.TabIndex = 14;
        // 
        // syncTimerBtn
        // 
        syncTimerBtn.Anchor = AnchorStyles.Top;
        syncTimerBtn.BackColor = SystemColors.ControlDark;
        syncTimerBtn.BackgroundImageLayout = ImageLayout.Center;
        syncTimerBtn.FlatAppearance.BorderSize = 0;
        syncTimerBtn.FlatStyle = FlatStyle.Flat;
        syncTimerBtn.IconChar = FontAwesome.Sharp.IconChar.ArrowsSpin;
        syncTimerBtn.IconColor = Color.Black;
        syncTimerBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        syncTimerBtn.IconSize = 30;
        syncTimerBtn.Location = new Point(11, 21);
        syncTimerBtn.Name = "syncTimerBtn";
        syncTimerBtn.Size = new Size(50, 50);
        syncTimerBtn.TabIndex = 15;
        syncTimerBtn.UseVisualStyleBackColor = false;
        syncTimerBtn.Click += syncTimerBtn_Click;
        // 
        // panel1
        // 
        panel1.Controls.Add(panel4);
        panel1.Controls.Add(panel3);
        panel1.Controls.Add(panel2);
        panel1.Dock = DockStyle.Top;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(859, 90);
        panel1.TabIndex = 16;
        // 
        // panel4
        // 
        panel4.Controls.Add(optionsBtn);
        panel4.Dock = DockStyle.Right;
        panel4.Location = new Point(789, 0);
        panel4.Name = "panel4";
        panel4.Size = new Size(70, 90);
        panel4.TabIndex = 2;
        // 
        // optionsBtn
        // 
        optionsBtn.Anchor = AnchorStyles.Top;
        optionsBtn.BackColor = SystemColors.ControlDark;
        optionsBtn.BackgroundImageLayout = ImageLayout.Center;
        optionsBtn.FlatAppearance.BorderSize = 0;
        optionsBtn.FlatStyle = FlatStyle.Flat;
        optionsBtn.IconChar = FontAwesome.Sharp.IconChar.Sliders;
        optionsBtn.IconColor = Color.Black;
        optionsBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        optionsBtn.IconSize = 30;
        optionsBtn.Location = new Point(8, 21);
        optionsBtn.Name = "optionsBtn";
        optionsBtn.Size = new Size(50, 50);
        optionsBtn.TabIndex = 16;
        optionsBtn.UseVisualStyleBackColor = false;
        optionsBtn.Click += optionsBtn_Click;
        // 
        // panel3
        // 
        panel3.Controls.Add(recordingTimeLbl);
        panel3.Controls.Add(streamTimeLbl);
        panel3.Dock = DockStyle.Fill;
        panel3.Location = new Point(73, 0);
        panel3.Name = "panel3";
        panel3.Size = new Size(786, 90);
        panel3.TabIndex = 1;
        // 
        // recordingTimeLbl
        // 
        recordingTimeLbl.Anchor = AnchorStyles.Top;
        recordingTimeLbl.AutoSize = true;
        recordingTimeLbl.FlatStyle = FlatStyle.Flat;
        recordingTimeLbl.Font = new Font("Segoe UI", 12F);
        recordingTimeLbl.Location = new Point(256, 49);
        recordingTimeLbl.Name = "recordingTimeLbl";
        recordingTimeLbl.Size = new Size(231, 28);
        recordingTimeLbl.TabIndex = 1;
        recordingTimeLbl.Text = "Recording Time: 00:00:00";
        recordingTimeLbl.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // streamTimeLbl
        // 
        streamTimeLbl.Anchor = AnchorStyles.Top;
        streamTimeLbl.AutoSize = true;
        streamTimeLbl.FlatStyle = FlatStyle.Flat;
        streamTimeLbl.Font = new Font("Segoe UI", 17F);
        streamTimeLbl.Location = new Point(224, 9);
        streamTimeLbl.Name = "streamTimeLbl";
        streamTimeLbl.Size = new Size(298, 40);
        streamTimeLbl.TabIndex = 0;
        streamTimeLbl.Text = "Stream Time: 00:00:00";
        streamTimeLbl.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel2
        // 
        panel2.Controls.Add(syncTimerBtn);
        panel2.Dock = DockStyle.Left;
        panel2.Location = new Point(0, 0);
        panel2.Name = "panel2";
        panel2.Size = new Size(73, 90);
        panel2.TabIndex = 0;
        // 
        // editGameBtn
        // 
        editGameBtn.BackColor = SystemColors.ControlDark;
        editGameBtn.FlatAppearance.BorderSize = 0;
        editGameBtn.FlatStyle = FlatStyle.Flat;
        editGameBtn.IconChar = FontAwesome.Sharp.IconChar.Pencil;
        editGameBtn.IconColor = Color.Black;
        editGameBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        editGameBtn.IconSize = 30;
        editGameBtn.Location = new Point(499, 96);
        editGameBtn.Name = "editGameBtn";
        editGameBtn.Size = new Size(31, 31);
        editGameBtn.TabIndex = 17;
        editGameBtn.UseVisualStyleBackColor = false;
        editGameBtn.Click += editGameBtn_Click;
        // 
        // addGameBtn
        // 
        addGameBtn.BackColor = SystemColors.ControlDark;
        addGameBtn.FlatAppearance.BorderSize = 0;
        addGameBtn.FlatStyle = FlatStyle.Flat;
        addGameBtn.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
        addGameBtn.IconColor = Color.Black;
        addGameBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        addGameBtn.IconSize = 30;
        addGameBtn.Location = new Point(462, 96);
        addGameBtn.Name = "addGameBtn";
        addGameBtn.Size = new Size(31, 31);
        addGameBtn.TabIndex = 18;
        addGameBtn.UseVisualStyleBackColor = false;
        addGameBtn.Click += addGameBtn_Click;
        // 
        // removeGame
        // 
        removeGame.BackColor = SystemColors.ControlDark;
        removeGame.FlatAppearance.BorderSize = 0;
        removeGame.FlatStyle = FlatStyle.Flat;
        removeGame.IconChar = FontAwesome.Sharp.IconChar.SquareMinus;
        removeGame.IconColor = Color.Black;
        removeGame.IconFont = FontAwesome.Sharp.IconFont.Auto;
        removeGame.IconSize = 30;
        removeGame.Location = new Point(536, 96);
        removeGame.Name = "removeGame";
        removeGame.Size = new Size(31, 31);
        removeGame.TabIndex = 19;
        removeGame.UseVisualStyleBackColor = false;
        removeGame.Click += removeGameBtn_Click;
        // 
        // editLocationBtn
        // 
        editLocationBtn.BackColor = SystemColors.ControlDark;
        editLocationBtn.FlatAppearance.BorderSize = 0;
        editLocationBtn.FlatStyle = FlatStyle.Flat;
        editLocationBtn.IconChar = FontAwesome.Sharp.IconChar.Pencil;
        editLocationBtn.IconColor = Color.Black;
        editLocationBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        editLocationBtn.IconSize = 30;
        editLocationBtn.Location = new Point(499, 133);
        editLocationBtn.Name = "editLocationBtn";
        editLocationBtn.Size = new Size(31, 31);
        editLocationBtn.TabIndex = 22;
        editLocationBtn.UseVisualStyleBackColor = false;
        editLocationBtn.Click += editLocationBtn_Click;
        // 
        // resetBtn
        // 
        resetBtn.BackColor = SystemColors.ControlDark;
        resetBtn.FlatAppearance.BorderSize = 0;
        resetBtn.FlatStyle = FlatStyle.Flat;
        resetBtn.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateLeft;
        resetBtn.IconColor = Color.Black;
        resetBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        resetBtn.IconSize = 30;
        resetBtn.Location = new Point(499, 170);
        resetBtn.Name = "resetBtn";
        resetBtn.Size = new Size(31, 31);
        resetBtn.TabIndex = 24;
        resetBtn.UseVisualStyleBackColor = false;
        resetBtn.Click += resetBtn_Click;
        // 
        // increaseBtn
        // 
        increaseBtn.BackColor = SystemColors.ControlDark;
        increaseBtn.FlatAppearance.BorderSize = 0;
        increaseBtn.FlatStyle = FlatStyle.Flat;
        increaseBtn.IconChar = FontAwesome.Sharp.IconChar.ArrowUp;
        increaseBtn.IconColor = Color.Black;
        increaseBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        increaseBtn.IconSize = 30;
        increaseBtn.Location = new Point(462, 170);
        increaseBtn.Name = "increaseBtn";
        increaseBtn.Size = new Size(31, 31);
        increaseBtn.TabIndex = 26;
        increaseBtn.UseVisualStyleBackColor = false;
        increaseBtn.Click += increaseBtn_Click;
        // 
        // addLocationBtn
        // 
        addLocationBtn.BackColor = SystemColors.ControlDark;
        addLocationBtn.FlatAppearance.BorderSize = 0;
        addLocationBtn.FlatStyle = FlatStyle.Flat;
        addLocationBtn.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
        addLocationBtn.IconColor = Color.Black;
        addLocationBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        addLocationBtn.IconSize = 30;
        addLocationBtn.Location = new Point(462, 133);
        addLocationBtn.Name = "addLocationBtn";
        addLocationBtn.Size = new Size(31, 31);
        addLocationBtn.TabIndex = 27;
        addLocationBtn.UseVisualStyleBackColor = false;
        addLocationBtn.Click += addLocationBtn_Click;
        // 
        // decreaseBtn
        // 
        decreaseBtn.BackColor = SystemColors.ControlDark;
        decreaseBtn.FlatAppearance.BorderSize = 0;
        decreaseBtn.FlatStyle = FlatStyle.Flat;
        decreaseBtn.IconChar = FontAwesome.Sharp.IconChar.ArrowDown;
        decreaseBtn.IconColor = Color.Black;
        decreaseBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        decreaseBtn.IconSize = 30;
        decreaseBtn.Location = new Point(536, 170);
        decreaseBtn.Name = "decreaseBtn";
        decreaseBtn.Size = new Size(31, 31);
        decreaseBtn.TabIndex = 28;
        decreaseBtn.UseVisualStyleBackColor = false;
        decreaseBtn.Click += decreaseBtn_Click;
        // 
        // removeLocationbtn
        // 
        removeLocationbtn.BackColor = SystemColors.ControlDark;
        removeLocationbtn.FlatAppearance.BorderSize = 0;
        removeLocationbtn.FlatStyle = FlatStyle.Flat;
        removeLocationbtn.IconChar = FontAwesome.Sharp.IconChar.SquareMinus;
        removeLocationbtn.IconColor = Color.Black;
        removeLocationbtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
        removeLocationbtn.IconSize = 30;
        removeLocationbtn.Location = new Point(536, 133);
        removeLocationbtn.Name = "removeLocationbtn";
        removeLocationbtn.Size = new Size(31, 31);
        removeLocationbtn.TabIndex = 30;
        removeLocationbtn.UseVisualStyleBackColor = false;
        removeLocationbtn.Click += removeLocationbtn_Click;
        // 
        // markerRTB
        // 
        markerRTB.BackColor = SystemColors.ControlDark;
        markerRTB.BorderStyle = BorderStyle.None;
        markerRTB.Location = new Point(573, 96);
        markerRTB.Name = "markerRTB";
        markerRTB.ReadOnly = true;
        markerRTB.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
        markerRTB.Size = new Size(274, 106);
        markerRTB.TabIndex = 31;
        markerRTB.TabStop = false;
        markerRTB.Text = "";
        markerRTB.WordWrap = false;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ControlDarkDark;
        ClientSize = new Size(859, 214);
        Controls.Add(markerRTB);
        Controls.Add(removeLocationbtn);
        Controls.Add(decreaseBtn);
        Controls.Add(addLocationBtn);
        Controls.Add(increaseBtn);
        Controls.Add(resetBtn);
        Controls.Add(editLocationBtn);
        Controls.Add(removeGame);
        Controls.Add(addGameBtn);
        Controls.Add(editGameBtn);
        Controls.Add(panel1);
        Controls.Add(locationDeathCountTxtb);
        Controls.Add(locationCombo);
        Controls.Add(gameSelectCombo);
        Controls.Add(deathCountTxtb);
        Controls.Add(pretextTxtb);
        ForeColor = SystemColors.ControlText;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "MainForm";
        Text = "FallenTally";
        FormClosing += MainForm_FormClosing;
        panel1.ResumeLayout(false);
        panel4.ResumeLayout(false);
        panel3.ResumeLayout(false);
        panel3.PerformLayout();
        panel2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private TextBox pretextTxtb;
    private TextBox deathCountTxtb;
    private ComboBox gameSelectCombo;
    private ComboBox locationCombo;
    private TextBox locationDeathCountTxtb;
    private FontAwesome.Sharp.IconButton syncTimerBtn;
    private Panel panel1;
    private Panel panel4;
    private Panel panel3;
    private Panel panel2;
    private Label streamTimeLbl;
    private FontAwesome.Sharp.IconButton optionsBtn;
    private FontAwesome.Sharp.IconButton editGameBtn;
    private FontAwesome.Sharp.IconButton addGameBtn;
    private FontAwesome.Sharp.IconButton removeGame;
    private FontAwesome.Sharp.IconButton editLocationBtn;
    private FontAwesome.Sharp.IconButton resetBtn;
    private FontAwesome.Sharp.IconButton increaseBtn;
    private FontAwesome.Sharp.IconButton addLocationBtn;
    private FontAwesome.Sharp.IconButton decreaseBtn;
    private FontAwesome.Sharp.IconButton removeLocationbtn;
    private Label recordingTimeLbl;
    private RichTextBox markerRTB;
}