namespace Bump_2_Panes
{
    partial class frmBumpedPanes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBumpedPanes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbDesktop = new System.Windows.Forms.RadioButton();
            this.rbPiled = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmeTick = new System.Windows.Forms.Timer();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.ImageButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkAllWindows = new System.Windows.Forms.CheckBox();
            this.tmeDeleteDirectory = new System.Windows.Forms.Timer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbScale = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblScale = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.rbDesktop);
            this.panel1.Controls.Add(this.rbPiled);
            this.panel1.Location = new System.Drawing.Point(70, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 25);
            this.panel1.TabIndex = 0;
            // 
            // rbDesktop
            // 
            this.rbDesktop.AutoSize = true;
            this.rbDesktop.Location = new System.Drawing.Point(73, 5);
            this.rbDesktop.Name = "rbDesktop";
            this.rbDesktop.Size = new System.Drawing.Size(96, 17);
            this.rbDesktop.TabIndex = 1;
            this.rbDesktop.Text = "Desktop Direct";
            this.rbDesktop.UseVisualStyleBackColor = true;
            this.rbDesktop.CheckedChanged += new System.EventHandler(this.rbDesktop_CheckedChanged);
            // 
            // rbPiled
            // 
            this.rbPiled.AutoSize = true;
            this.rbPiled.Checked = true;
            this.rbPiled.Location = new System.Drawing.Point(3, 5);
            this.rbPiled.Name = "rbPiled";
            this.rbPiled.Size = new System.Drawing.Size(64, 17);
            this.rbPiled.TabIndex = 0;
            this.rbPiled.TabStop = true;
            this.rbPiled.Text = "In A Pile";
            this.rbPiled.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txtPileName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(70, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(259, 27);
            this.panel2.TabIndex = 1;
            // 
            // txtPileName
            // 
            this.txtPileName.Location = new System.Drawing.Point(64, 3);
            this.txtPileName.Name = "txtPileName";
            this.txtPileName.Size = new System.Drawing.Size(135, 20);
            this.txtPileName.TabIndex = 1;
            this.txtPileName.Text = "Applications";
            this.txtPileName.TextChanged += new System.EventHandler(this.txtPileName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pile Name";
            // 
            // tmeTick
            // 
            this.tmeTick.Tick += new System.EventHandler(this.tmeTick_Tick);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(311, 192);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.DownImage = null;
            this.btnClose.HoverImage = ((System.Drawing.Image)(resources.GetObject("btnClose.HoverImage")));
            this.btnClose.Location = new System.Drawing.Point(353, -2);
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = global::Bump_2_Panes.Properties.Resources.X;
            this.btnClose.Size = new System.Drawing.Size(43, 41);
            this.btnClose.TabIndex = 5;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.chkAllWindows);
            this.panel4.Location = new System.Drawing.Point(70, 117);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(259, 27);
            this.panel4.TabIndex = 2;
            // 
            // chkAllWindows
            // 
            this.chkAllWindows.AutoSize = true;
            this.chkAllWindows.Location = new System.Drawing.Point(3, 3);
            this.chkAllWindows.Name = "chkAllWindows";
            this.chkAllWindows.Size = new System.Drawing.Size(173, 17);
            this.chkAllWindows.TabIndex = 1;
            this.chkAllWindows.Text = "Bump All Windows To Desktop";
            this.chkAllWindows.UseVisualStyleBackColor = true;
            this.chkAllWindows.CheckedChanged += new System.EventHandler(this.chkAllWindows_CheckedChanged);
            // 
            // tmeDeleteDirectory
            // 
            this.tmeDeleteDirectory.Interval = 1000;
            this.tmeDeleteDirectory.Tick += new System.EventHandler(this.tmeDeleteDirectory_Tick);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.tbScale);
            this.panel5.Location = new System.Drawing.Point(67, 163);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(250, 23);
            this.panel5.TabIndex = 7;
            // 
            // tbScale
            // 
            this.tbScale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScale.Location = new System.Drawing.Point(0, 0);
            this.tbScale.Maximum = 60;
            this.tbScale.Minimum = 10;
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(250, 23);
            this.tbScale.TabIndex = 0;
            this.tbScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbScale.Value = 10;
            this.tbScale.Scroll += new System.EventHandler(this.tbScale_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(70, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Bumped Windows Size";
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.BackColor = System.Drawing.Color.Transparent;
            this.lblScale.Location = new System.Drawing.Point(208, 147);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(48, 13);
            this.lblScale.TabIndex = 9;
            this.lblScale.Text = "1x Scale";
            // 
            // frmBumpedPanes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(398, 225);
            this.ControlBox = false;
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBumpedPanes";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBumpedPanes_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmBumpedPanes_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPiled;
        private System.Windows.Forms.RadioButton rbDesktop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmeTick;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ImageButton btnClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkAllWindows;
        private System.Windows.Forms.Timer tmeDeleteDirectory;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TrackBar tbScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblScale;

    }
}

