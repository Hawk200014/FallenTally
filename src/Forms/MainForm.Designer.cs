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
        button1 = new Button();
        locationCombo = new ComboBox();
        button2 = new Button();
        button3 = new Button();
        button4 = new Button();
        textBox1 = new TextBox();
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
        gameSelectCombo.Enabled = false;
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
        // button1
        // 
        button1.Location = new Point(177, 46);
        button1.Name = "button1";
        button1.Size = new Size(159, 28);
        button1.TabIndex = 9;
        button1.Text = "Edit Game";
        button1.UseVisualStyleBackColor = true;
        // 
        // locationCombo
        // 
        locationCombo.Enabled = false;
        locationCombo.FormattingEnabled = true;
        locationCombo.Location = new Point(12, 80);
        locationCombo.Name = "locationCombo";
        locationCombo.Size = new Size(355, 28);
        locationCombo.TabIndex = 10;
        // 
        // button2
        // 
        button2.Location = new Point(12, 114);
        button2.Name = "button2";
        button2.Size = new Size(159, 28);
        button2.TabIndex = 11;
        button2.Text = "Add Location";
        button2.UseVisualStyleBackColor = true;
        // 
        // button3
        // 
        button3.Location = new Point(177, 114);
        button3.Name = "button3";
        button3.Size = new Size(159, 28);
        button3.TabIndex = 12;
        button3.Text = "Edit Location";
        button3.UseVisualStyleBackColor = true;
        // 
        // button4
        // 
        button4.Location = new Point(342, 114);
        button4.Name = "button4";
        button4.Size = new Size(159, 28);
        button4.TabIndex = 13;
        button4.Text = "Remove Location";
        button4.UseVisualStyleBackColor = true;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(373, 80);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(128, 27);
        textBox1.TabIndex = 14;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(511, 217);
        Controls.Add(textBox1);
        Controls.Add(button4);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(locationCombo);
        Controls.Add(button1);
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
    private Button button1;
    private ComboBox locationCombo;
    private Button button2;
    private Button button3;
    private Button button4;
    private TextBox textBox1;
}