namespace FirmwareToolbox
{
    internal sealed partial class Form1
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.TypeLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.typelist = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.versionslist = new System.Windows.Forms.ComboBox();
            this.dwlbtn = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TypeLabel,
            this.typelist});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(449, 27);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "mainmenu";
            // 
            // TypeLabel
            // 
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(48, 23);
            this.TypeLabel.Text = "Type:";
            this.TypeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TypeLabelMouseHandler);
            this.TypeLabel.MouseEnter += new System.EventHandler(this.TypeLabelMouseHandler);
            this.TypeLabel.MouseLeave += new System.EventHandler(this.TypeLabelMouseHandler);
            this.TypeLabel.MouseHover += new System.EventHandler(this.TypeLabelMouseHandler);
            this.TypeLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TypeLabelMouseHandler);
            this.TypeLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TypeLabelMouseHandler);
            // 
            // typelist
            // 
            this.typelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typelist.Name = "typelist";
            this.typelist.Size = new System.Drawing.Size(121, 23);
            this.typelist.SelectedIndexChanged += new System.EventHandler(this.TypelistSelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 268);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(449, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(122, 17);
            this.toolStripStatusLabel2.Text = "Waiting for user input";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dwlbtn);
            this.groupBox1.Controls.Add(this.versionslist);
            this.groupBox1.Location = new System.Drawing.Point(12, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // versionslist
            // 
            this.versionslist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionslist.FormattingEnabled = true;
            this.versionslist.Location = new System.Drawing.Point(6, 19);
            this.versionslist.Name = "versionslist";
            this.versionslist.Size = new System.Drawing.Size(154, 21);
            this.versionslist.TabIndex = 0;
            this.versionslist.SelectedIndexChanged += new System.EventHandler(this.VersionslistSelectedIndexChanged);
            // 
            // dwlbtn
            // 
            this.dwlbtn.Enabled = false;
            this.dwlbtn.Location = new System.Drawing.Point(6, 46);
            this.dwlbtn.Name = "dwlbtn";
            this.dwlbtn.Size = new System.Drawing.Size(154, 23);
            this.dwlbtn.TabIndex = 1;
            this.dwlbtn.Text = "Download It";
            this.dwlbtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 290);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripComboBox typelist;
        private System.Windows.Forms.ToolStripMenuItem TypeLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox versionslist;
        private System.Windows.Forms.Button dwlbtn;

    }
}

