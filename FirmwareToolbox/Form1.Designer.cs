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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkbtn = new System.Windows.Forms.Button();
            this.dwlbtn = new System.Windows.Forms.Button();
            this.versionslist = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.typelist = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.extractbtn = new System.Windows.Forms.Button();
            this.formatbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.devicelist = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 171);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(378, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(122, 17);
            this.status.Text = "Waiting for user input";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkbtn);
            this.groupBox1.Controls.Add(this.dwlbtn);
            this.groupBox1.Controls.Add(this.versionslist);
            this.groupBox1.Location = new System.Drawing.Point(12, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // chkbtn
            // 
            this.chkbtn.Enabled = false;
            this.chkbtn.Location = new System.Drawing.Point(6, 75);
            this.chkbtn.Name = "chkbtn";
            this.chkbtn.Size = new System.Drawing.Size(154, 23);
            this.chkbtn.TabIndex = 2;
            this.chkbtn.Text = "Check It";
            this.chkbtn.UseVisualStyleBackColor = true;
            this.chkbtn.Click += new System.EventHandler(this.ChkbtnClick);
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
            this.dwlbtn.Click += new System.EventHandler(this.DwlbtnClick);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.typelist);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 46);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type/Console";
            // 
            // typelist
            // 
            this.typelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typelist.FormattingEnabled = true;
            this.typelist.Location = new System.Drawing.Point(6, 19);
            this.typelist.Name = "typelist";
            this.typelist.Size = new System.Drawing.Size(154, 21);
            this.typelist.TabIndex = 0;
            this.typelist.SelectedIndexChanged += new System.EventHandler(this.TypelistSelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.extractbtn);
            this.groupBox3.Controls.Add(this.formatbtn);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.devicelist);
            this.groupBox3.Location = new System.Drawing.Point(184, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 156);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Prepare Memory";
            // 
            // extractbtn
            // 
            this.extractbtn.Enabled = false;
            this.extractbtn.Location = new System.Drawing.Point(6, 46);
            this.extractbtn.Name = "extractbtn";
            this.extractbtn.Size = new System.Drawing.Size(170, 23);
            this.extractbtn.TabIndex = 2;
            this.extractbtn.Text = "Extract Update";
            this.extractbtn.UseVisualStyleBackColor = true;
            // 
            // formatbtn
            // 
            this.formatbtn.Enabled = false;
            this.formatbtn.Location = new System.Drawing.Point(6, 127);
            this.formatbtn.Name = "formatbtn";
            this.formatbtn.Size = new System.Drawing.Size(170, 23);
            this.formatbtn.TabIndex = 2;
            this.formatbtn.Text = "Format Device";
            this.formatbtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device:";
            // 
            // devicelist
            // 
            this.devicelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devicelist.FormattingEnabled = true;
            this.devicelist.Location = new System.Drawing.Point(56, 19);
            this.devicelist.Name = "devicelist";
            this.devicelist.Size = new System.Drawing.Size(120, 21);
            this.devicelist.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 193);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox versionslist;
        private System.Windows.Forms.Button dwlbtn;
        private System.Windows.Forms.Button chkbtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox typelist;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox devicelist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button formatbtn;
        private System.Windows.Forms.Button extractbtn;

    }
}

