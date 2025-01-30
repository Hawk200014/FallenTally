namespace DeathCounterHotkey.Forms
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            filterDeathGameCombo = new ComboBox();
            groupBox1 = new GroupBox();
            label8 = new Label();
            DeathsExportBtn = new Button();
            deathExportFormatCombo = new ComboBox();
            deathEntriesTB = new TextBox();
            label6 = new Label();
            groupBox2 = new GroupBox();
            label1 = new Label();
            label3 = new Label();
            filterDeathDateCombo = new ComboBox();
            filterDeathLocationCombo = new ComboBox();
            label2 = new Label();
            groupBox3 = new GroupBox();
            markerExportBtn = new Button();
            MarkerEntriesTB = new TextBox();
            label7 = new Label();
            groupBox4 = new GroupBox();
            label9 = new Label();
            filterMarkerSessionCombo = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            filterMarkerGameCombo = new ComboBox();
            filterMarkerDateCombo = new ComboBox();
            label10 = new Label();
            markerExportFormatCombo = new ComboBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // filterDeathGameCombo
            // 
            filterDeathGameCombo.BackColor = SystemColors.ControlDark;
            filterDeathGameCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterDeathGameCombo.FlatStyle = FlatStyle.Flat;
            filterDeathGameCombo.FormattingEnabled = true;
            filterDeathGameCombo.Location = new Point(178, 15);
            filterDeathGameCombo.Margin = new Padding(3, 2, 3, 2);
            filterDeathGameCombo.Name = "filterDeathGameCombo";
            filterDeathGameCombo.Size = new Size(214, 23);
            filterDeathGameCombo.TabIndex = 0;
            filterDeathGameCombo.SelectedIndexChanged += filterDeathGameCombo_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(DeathsExportBtn);
            groupBox1.Controls.Add(deathExportFormatCombo);
            groupBox1.Controls.Add(deathEntriesTB);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(10, 9);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(410, 202);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Export Deaths";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 147);
            label8.Name = "label8";
            label8.Size = new Size(82, 15);
            label8.TabIndex = 11;
            label8.Text = "Export Format";
            // 
            // DeathsExportBtn
            // 
            DeathsExportBtn.BackColor = SystemColors.ControlDark;
            DeathsExportBtn.FlatStyle = FlatStyle.Flat;
            DeathsExportBtn.Location = new Point(315, 170);
            DeathsExportBtn.Margin = new Padding(3, 2, 3, 2);
            DeathsExportBtn.Name = "DeathsExportBtn";
            DeathsExportBtn.Size = new Size(82, 22);
            DeathsExportBtn.TabIndex = 10;
            DeathsExportBtn.Text = "Export";
            DeathsExportBtn.UseVisualStyleBackColor = false;
            DeathsExportBtn.Click += DeathsExportBtn_Click;
            // 
            // deathExportFormatCombo
            // 
            deathExportFormatCombo.BackColor = SystemColors.ControlDark;
            deathExportFormatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            deathExportFormatCombo.FlatStyle = FlatStyle.Flat;
            deathExportFormatCombo.FormattingEnabled = true;
            deathExportFormatCombo.Location = new Point(184, 145);
            deathExportFormatCombo.Margin = new Padding(3, 2, 3, 2);
            deathExportFormatCombo.Name = "deathExportFormatCombo";
            deathExportFormatCombo.Size = new Size(214, 23);
            deathExportFormatCombo.TabIndex = 6;
            // 
            // deathEntriesTB
            // 
            deathEntriesTB.BackColor = SystemColors.ControlDark;
            deathEntriesTB.Location = new Point(184, 120);
            deathEntriesTB.Margin = new Padding(3, 2, 3, 2);
            deathEntriesTB.Name = "deathEntriesTB";
            deathEntriesTB.ReadOnly = true;
            deathEntriesTB.Size = new Size(214, 23);
            deathEntriesTB.TabIndex = 9;
            deathEntriesTB.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 122);
            label6.Name = "label6";
            label6.Size = new Size(82, 15);
            label6.TabIndex = 7;
            label6.Text = "Found Entries:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(filterDeathGameCombo);
            groupBox2.Controls.Add(filterDeathDateCombo);
            groupBox2.Controls.Add(filterDeathLocationCombo);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(5, 20);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(399, 96);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Filter";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 17);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "Game";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 68);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 5;
            label3.Text = "Date";
            // 
            // filterDeathDateCombo
            // 
            filterDeathDateCombo.BackColor = SystemColors.ControlDark;
            filterDeathDateCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterDeathDateCombo.FlatStyle = FlatStyle.Flat;
            filterDeathDateCombo.FormattingEnabled = true;
            filterDeathDateCombo.Location = new Point(178, 66);
            filterDeathDateCombo.Margin = new Padding(3, 2, 3, 2);
            filterDeathDateCombo.Name = "filterDeathDateCombo";
            filterDeathDateCombo.Size = new Size(214, 23);
            filterDeathDateCombo.TabIndex = 4;
            filterDeathDateCombo.SelectedIndexChanged += filterDeathDateCombo_SelectedIndexChanged;
            // 
            // filterDeathLocationCombo
            // 
            filterDeathLocationCombo.BackColor = SystemColors.ControlDark;
            filterDeathLocationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterDeathLocationCombo.FlatStyle = FlatStyle.Flat;
            filterDeathLocationCombo.FormattingEnabled = true;
            filterDeathLocationCombo.Location = new Point(178, 40);
            filterDeathLocationCombo.Margin = new Padding(3, 2, 3, 2);
            filterDeathLocationCombo.Name = "filterDeathLocationCombo";
            filterDeathLocationCombo.Size = new Size(214, 23);
            filterDeathLocationCombo.TabIndex = 2;
            filterDeathLocationCombo.SelectedIndexChanged += filterDeathLocationCombo_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 43);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 3;
            label2.Text = "Location";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(markerExportFormatCombo);
            groupBox3.Controls.Add(markerExportBtn);
            groupBox3.Controls.Add(MarkerEntriesTB);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(groupBox4);
            groupBox3.Location = new Point(426, 9);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(410, 202);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Export Marker";
            // 
            // markerExportBtn
            // 
            markerExportBtn.BackColor = SystemColors.ControlDark;
            markerExportBtn.FlatStyle = FlatStyle.Flat;
            markerExportBtn.Location = new Point(315, 170);
            markerExportBtn.Margin = new Padding(3, 2, 3, 2);
            markerExportBtn.Name = "markerExportBtn";
            markerExportBtn.Size = new Size(82, 22);
            markerExportBtn.TabIndex = 12;
            markerExportBtn.Text = "Export";
            markerExportBtn.UseVisualStyleBackColor = false;
            markerExportBtn.Click += markerExportBtn_Click;
            // 
            // MarkerEntriesTB
            // 
            MarkerEntriesTB.BackColor = SystemColors.ControlDark;
            MarkerEntriesTB.Location = new Point(184, 120);
            MarkerEntriesTB.Margin = new Padding(3, 2, 3, 2);
            MarkerEntriesTB.Name = "MarkerEntriesTB";
            MarkerEntriesTB.ReadOnly = true;
            MarkerEntriesTB.Size = new Size(214, 23);
            MarkerEntriesTB.TabIndex = 10;
            MarkerEntriesTB.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 122);
            label7.Name = "label7";
            label7.Size = new Size(82, 15);
            label7.TabIndex = 8;
            label7.Text = "Found Entries:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(filterMarkerSessionCombo);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(filterMarkerGameCombo);
            groupBox4.Controls.Add(filterMarkerDateCombo);
            groupBox4.Location = new Point(5, 20);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(399, 96);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Filter";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(5, 68);
            label9.Name = "label9";
            label9.Size = new Size(103, 15);
            label9.TabIndex = 7;
            label9.Text = "Recording Session";
            // 
            // filterMarkerSessionCombo
            // 
            filterMarkerSessionCombo.BackColor = SystemColors.ControlDark;
            filterMarkerSessionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterMarkerSessionCombo.FlatStyle = FlatStyle.Flat;
            filterMarkerSessionCombo.FormattingEnabled = true;
            filterMarkerSessionCombo.Location = new Point(178, 66);
            filterMarkerSessionCombo.Margin = new Padding(3, 2, 3, 2);
            filterMarkerSessionCombo.Name = "filterMarkerSessionCombo";
            filterMarkerSessionCombo.Size = new Size(214, 23);
            filterMarkerSessionCombo.TabIndex = 6;
            filterMarkerSessionCombo.SelectedIndexChanged += filterMarkerSessionCombo_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 17);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 1;
            label4.Text = "Game";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 43);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 5;
            label5.Text = "Date";
            // 
            // filterMarkerGameCombo
            // 
            filterMarkerGameCombo.BackColor = SystemColors.ControlDark;
            filterMarkerGameCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterMarkerGameCombo.FlatStyle = FlatStyle.Flat;
            filterMarkerGameCombo.FormattingEnabled = true;
            filterMarkerGameCombo.Location = new Point(178, 15);
            filterMarkerGameCombo.Margin = new Padding(3, 2, 3, 2);
            filterMarkerGameCombo.Name = "filterMarkerGameCombo";
            filterMarkerGameCombo.Size = new Size(214, 23);
            filterMarkerGameCombo.TabIndex = 0;
            filterMarkerGameCombo.SelectedIndexChanged += filterMarkerGameCombo_SelectedIndexChanged;
            // 
            // filterMarkerDateCombo
            // 
            filterMarkerDateCombo.BackColor = SystemColors.ControlDark;
            filterMarkerDateCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterMarkerDateCombo.FlatStyle = FlatStyle.Flat;
            filterMarkerDateCombo.FormattingEnabled = true;
            filterMarkerDateCombo.Location = new Point(178, 40);
            filterMarkerDateCombo.Margin = new Padding(3, 2, 3, 2);
            filterMarkerDateCombo.Name = "filterMarkerDateCombo";
            filterMarkerDateCombo.Size = new Size(214, 23);
            filterMarkerDateCombo.TabIndex = 4;
            filterMarkerDateCombo.SelectedIndexChanged += filterMarkerDateCombo_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 148);
            label10.Name = "label10";
            label10.Size = new Size(82, 15);
            label10.TabIndex = 14;
            label10.Text = "Export Format";
            // 
            // markerExportFormatCombo
            // 
            markerExportFormatCombo.BackColor = SystemColors.ControlDark;
            markerExportFormatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            markerExportFormatCombo.FlatStyle = FlatStyle.Flat;
            markerExportFormatCombo.FormattingEnabled = true;
            markerExportFormatCombo.Location = new Point(183, 145);
            markerExportFormatCombo.Margin = new Padding(3, 2, 3, 2);
            markerExportFormatCombo.Name = "markerExportFormatCombo";
            markerExportFormatCombo.Size = new Size(214, 23);
            markerExportFormatCombo.TabIndex = 13;
            // 
            // ExportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(848, 217);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "ExportForm";
            Text = "Export - DeathHotkey";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox filterDeathGameCombo;
        private GroupBox groupBox1;
        private Label label3;
        private ComboBox filterDeathDateCombo;
        private Label label2;
        private ComboBox filterDeathLocationCombo;
        private Label label1;
        private GroupBox groupBox2;
        private Label label6;
        private GroupBox groupBox3;
        private Label label7;
        private GroupBox groupBox4;
        private Label label4;
        private Label label5;
        private ComboBox filterMarkerGameCombo;
        private ComboBox filterMarkerDateCombo;
        private TextBox deathEntriesTB;
        private Label label8;
        private Button DeathsExportBtn;
        private ComboBox deathExportFormatCombo;
        private Button markerExportBtn;
        private TextBox MarkerEntriesTB;
        private Label label9;
        private ComboBox filterMarkerSessionCombo;
        private Label label10;
        private ComboBox markerExportFormatCombo;
    }
}