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
        SuspendLayout();
        // 
        // optionsBtn
        // 
        optionsBtn.Location = new Point(538, 11);
        optionsBtn.Name = "optionsBtn";
        optionsBtn.Size = new Size(102, 29);
        optionsBtn.TabIndex = 0;
        optionsBtn.Text = "OPTIONS";
        optionsBtn.UseVisualStyleBackColor = true;
        optionsBtn.Click += optionsBtn_Click;
        // 
        // pretextTxtb
        // 
        pretextTxtb.Location = new Point(12, 120);
        pretextTxtb.Name = "pretextTxtb";
        pretextTxtb.Size = new Size(386, 27);
        pretextTxtb.TabIndex = 1;
        // 
        // deathCountTxtb
        // 
        deathCountTxtb.Location = new Point(404, 120);
        deathCountTxtb.Name = "deathCountTxtb";
        deathCountTxtb.Size = new Size(128, 27);
        deathCountTxtb.TabIndex = 2;
        // 
        // resetBtn
        // 
        resetBtn.Location = new Point(538, 46);
        resetBtn.Name = "resetBtn";
        resetBtn.Size = new Size(102, 29);
        resetBtn.TabIndex = 3;
        resetBtn.Text = "RESET";
        resetBtn.UseVisualStyleBackColor = true;
        resetBtn.Click += resetBtn_Click;
        // 
        // increaseBtn
        // 
        increaseBtn.Location = new Point(538, 118);
        increaseBtn.Name = "increaseBtn";
        increaseBtn.Size = new Size(102, 29);
        increaseBtn.TabIndex = 4;
        increaseBtn.Text = "INCREASE";
        increaseBtn.UseVisualStyleBackColor = true;
        increaseBtn.Click += increaseBtn_Click;
        // 
        // decreaseBtn
        // 
        decreaseBtn.Location = new Point(538, 83);
        decreaseBtn.Name = "decreaseBtn";
        decreaseBtn.Size = new Size(102, 29);
        decreaseBtn.TabIndex = 5;
        decreaseBtn.Text = "DECREASE";
        decreaseBtn.UseVisualStyleBackColor = true;
        decreaseBtn.Click += decreaseBtn_Click;
        // 
        // gameSelectCombo
        // 
        gameSelectCombo.FormattingEnabled = true;
        gameSelectCombo.Location = new Point(12, 12);
        gameSelectCombo.Name = "gameSelectCombo";
        gameSelectCombo.Size = new Size(386, 28);
        gameSelectCombo.TabIndex = 6;
        gameSelectCombo.SelectedIndexChanged += gameSelectCombo_SelectedIndexChanged;
        // 
        // addGameBtn
        // 
        addGameBtn.Location = new Point(12, 46);
        addGameBtn.Name = "addGameBtn";
        addGameBtn.Size = new Size(130, 29);
        addGameBtn.TabIndex = 7;
        addGameBtn.Text = "Add Game";
        addGameBtn.UseVisualStyleBackColor = true;
        addGameBtn.Click += addGameBtn_Click;
        // 
        // removeGameBtn
        // 
        removeGameBtn.Location = new Point(268, 46);
        removeGameBtn.Name = "removeGameBtn";
        removeGameBtn.Size = new Size(130, 29);
        removeGameBtn.TabIndex = 8;
        removeGameBtn.Text = "Remove Game";
        removeGameBtn.UseVisualStyleBackColor = true;
        removeGameBtn.Click += removeGameBtn_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(652, 159);
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
}