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
        optionsBtn = new Button();
        pretextTxtb = new TextBox();
        deathCountTxtb = new TextBox();
        resetBtn = new Button();
        increaseBtn = new Button();
        decreaseBtn = new Button();
        gameSelectCombo = new ComboBox();
        addGameBtn = new Button();
        removeGameBtn = new Button();
        editGameBtn = new Button();
        locationCombo = new ComboBox();
        addLocationBtn = new Button();
        editLocationBtn = new Button();
        removeLocationbtn = new Button();
        locationDeathCountTxtb = new TextBox();
        SuspendLayout();
        // 
        // optionsBtn
        // 
        optionsBtn.Location = new Point(399, 12);
        optionsBtn.Name = "optionsBtn";
        optionsBtn.Size = new Size(102, 29);
        optionsBtn.TabIndex = 0;
        optionsBtn.Text = "Options";
        optionsBtn.UseVisualStyleBackColor = true;
        optionsBtn.Click += optionsBtn_Click;
        // 
        // pretextTxtb
        // 
        pretextTxtb.Location = new Point(12, 148);
        pretextTxtb.Name = "pretextTxtb";
        pretextTxtb.Size = new Size(355, 27);
        pretextTxtb.TabIndex = 1;
        // 
        // deathCountTxtb
        // 
        deathCountTxtb.Location = new Point(373, 148);
        deathCountTxtb.Name = "deathCountTxtb";
        deathCountTxtb.Size = new Size(128, 27);
        deathCountTxtb.TabIndex = 2;
        // 
        // resetBtn
        // 
        resetBtn.Location = new Point(177, 181);
        resetBtn.Name = "resetBtn";
        resetBtn.Size = new Size(159, 29);
        resetBtn.TabIndex = 3;
        resetBtn.Text = "Reset";
        resetBtn.UseVisualStyleBackColor = true;
        resetBtn.Click += resetBtn_Click;
        // 
        // increaseBtn
        // 
        increaseBtn.Location = new Point(342, 182);
        increaseBtn.Name = "increaseBtn";
        increaseBtn.Size = new Size(159, 28);
        increaseBtn.TabIndex = 4;
        increaseBtn.Text = "Increase";
        increaseBtn.UseVisualStyleBackColor = true;
        increaseBtn.Click += increaseBtn_Click;
        // 
        // decreaseBtn
        // 
        decreaseBtn.Location = new Point(12, 181);
        decreaseBtn.Name = "decreaseBtn";
        decreaseBtn.Size = new Size(159, 29);
        decreaseBtn.TabIndex = 5;
        decreaseBtn.Text = "Decrease";
        decreaseBtn.UseVisualStyleBackColor = true;
        decreaseBtn.Click += decreaseBtn_Click;
        // 
        // gameSelectCombo
        // 
        gameSelectCombo.FormattingEnabled = true;
        gameSelectCombo.Location = new Point(12, 12);
        gameSelectCombo.Name = "gameSelectCombo";
        gameSelectCombo.Size = new Size(381, 28);
        gameSelectCombo.TabIndex = 6;
        gameSelectCombo.SelectedIndexChanged += gameSelectCombo_SelectedIndexChanged;
        // 
        // addGameBtn
        // 
        addGameBtn.Location = new Point(12, 46);
        addGameBtn.Name = "addGameBtn";
        addGameBtn.Size = new Size(159, 28);
        addGameBtn.TabIndex = 7;
        addGameBtn.Text = "Add Game";
        addGameBtn.UseVisualStyleBackColor = true;
        addGameBtn.Click += addGameBtn_Click;
        // 
        // removeGameBtn
        // 
        removeGameBtn.Location = new Point(342, 46);
        removeGameBtn.Name = "removeGameBtn";
        removeGameBtn.Size = new Size(159, 28);
        removeGameBtn.TabIndex = 8;
        removeGameBtn.Text = "Remove Game";
        removeGameBtn.UseVisualStyleBackColor = true;
        removeGameBtn.Click += removeGameBtn_Click;
        // 
        // editGameBtn
        // 
        editGameBtn.Location = new Point(177, 46);
        editGameBtn.Name = "editGameBtn";
        editGameBtn.Size = new Size(159, 28);
        editGameBtn.TabIndex = 9;
        editGameBtn.Text = "Edit Game";
        editGameBtn.UseVisualStyleBackColor = true;
        editGameBtn.Click += editGameBtn_Click;
        // 
        // locationCombo
        // 
        locationCombo.FormattingEnabled = true;
        locationCombo.Location = new Point(12, 80);
        locationCombo.Name = "locationCombo";
        locationCombo.Size = new Size(355, 28);
        locationCombo.TabIndex = 10;
        locationCombo.SelectedIndexChanged += locationCombo_SelectedIndexChanged;
        // 
        // addLocationBtn
        // 
        addLocationBtn.Location = new Point(12, 114);
        addLocationBtn.Name = "addLocationBtn";
        addLocationBtn.Size = new Size(159, 28);
        addLocationBtn.TabIndex = 11;
        addLocationBtn.Text = "Add Location";
        addLocationBtn.UseVisualStyleBackColor = true;
        addLocationBtn.Click += addLocationBtn_Click;
        // 
        // editLocationBtn
        // 
        editLocationBtn.Location = new Point(177, 114);
        editLocationBtn.Name = "editLocationBtn";
        editLocationBtn.Size = new Size(159, 28);
        editLocationBtn.TabIndex = 12;
        editLocationBtn.Text = "Edit Location";
        editLocationBtn.UseVisualStyleBackColor = true;
        editLocationBtn.Click += editLocationBtn_Click;
        // 
        // removeLocationbtn
        // 
        removeLocationbtn.Location = new Point(342, 114);
        removeLocationbtn.Name = "removeLocationbtn";
        removeLocationbtn.Size = new Size(159, 28);
        removeLocationbtn.TabIndex = 13;
        removeLocationbtn.Text = "Remove Location";
        removeLocationbtn.UseVisualStyleBackColor = true;
        removeLocationbtn.Click += removeLocationbtn_Click;
        // 
        // locationDeathCountTxtb
        // 
        locationDeathCountTxtb.Location = new Point(373, 80);
        locationDeathCountTxtb.Name = "locationDeathCountTxtb";
        locationDeathCountTxtb.Size = new Size(128, 27);
        locationDeathCountTxtb.TabIndex = 14;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(511, 217);
        Controls.Add(locationDeathCountTxtb);
        Controls.Add(removeLocationbtn);
        Controls.Add(editLocationBtn);
        Controls.Add(addLocationBtn);
        Controls.Add(locationCombo);
        Controls.Add(editGameBtn);
        Controls.Add(removeGameBtn);
        Controls.Add(addGameBtn);
        Controls.Add(gameSelectCombo);
        Controls.Add(decreaseBtn);
        Controls.Add(increaseBtn);
        Controls.Add(resetBtn);
        Controls.Add(deathCountTxtb);
        Controls.Add(pretextTxtb);
        Controls.Add(optionsBtn);
        Name = "MainForm";
        Text = "DeathHotkey";
        FormClosing += MainForm_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button optionsBtn;
    private TextBox pretextTxtb;
    private TextBox deathCountTxtb;
    private Button resetBtn;
    private Button increaseBtn;
    private Button decreaseBtn;
    private ComboBox gameSelectCombo;
    private Button addGameBtn;
    private Button removeGameBtn;
    private Button editGameBtn;
    private ComboBox locationCombo;
    private Button addLocationBtn;
    private Button editLocationBtn;
    private Button removeLocationbtn;
    private TextBox locationDeathCountTxtb;
}