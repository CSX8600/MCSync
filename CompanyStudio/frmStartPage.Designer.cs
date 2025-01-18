namespace CompanyStudio
{
    partial class frmStartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStartPage));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.airForm1 = new ReaLTaiizor.Forms.AirForm();
            this.lblAllCaughtUp = new ReaLTaiizor.Controls.MaterialLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.headerLabel1 = new ReaLTaiizor.Controls.HeaderLabel();
            this.tbpToDoList = new ReaLTaiizor.Controls.AirTabPage();
            this.tabAll = new System.Windows.Forms.TabPage();
            this.pnlQuickLinks = new System.Windows.Forms.FlowLayoutPanel();
            this.lnkQuickAccess = new ReaLTaiizor.Controls.DungeonLinkLabel();
            this.headerLabel2 = new ReaLTaiizor.Controls.HeaderLabel();
            this.chkAlwaysShowStart = new ReaLTaiizor.Controls.AirCheckBox();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.smallLabel1 = new ReaLTaiizor.Controls.SmallLabel();
            this.loader = new CompanyStudio.Loader();
            this.ctxQuickAccessMenu = new ReaLTaiizor.Controls.CrownContextMenuStrip();
            this.tsmiEditQuickAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteQuickAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiQuickAccessMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQuickAccessMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.airForm1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tbpToDoList.SuspendLayout();
            this.pnlQuickLinks.SuspendLayout();
            this.ctxQuickAccessMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(0, 0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(200, 100);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            // 
            // airForm1
            // 
            this.airForm1.BackColor = System.Drawing.Color.White;
            this.airForm1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.airForm1.Controls.Add(this.loader);
            this.airForm1.Controls.Add(this.lblAllCaughtUp);
            this.airForm1.Controls.Add(this.splitContainer1);
            this.airForm1.Controls.Add(this.chkAlwaysShowStart);
            this.airForm1.Controls.Add(this.bigLabel1);
            this.airForm1.Controls.Add(this.smallLabel1);
            this.airForm1.Customization = "AAAA/1paWv9ycnL/";
            this.airForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.airForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.airForm1.Image = null;
            this.airForm1.Location = new System.Drawing.Point(0, 0);
            this.airForm1.MinimumSize = new System.Drawing.Size(112, 35);
            this.airForm1.Movable = true;
            this.airForm1.Name = "airForm1";
            this.airForm1.NoRounding = true;
            this.airForm1.Sizable = true;
            this.airForm1.Size = new System.Drawing.Size(800, 416);
            this.airForm1.SmartBounds = true;
            this.airForm1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.airForm1.TabIndex = 0;
            this.airForm1.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.airForm1.Transparent = false;
            // 
            // lblAllCaughtUp
            // 
            this.lblAllCaughtUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAllCaughtUp.AutoSize = true;
            this.lblAllCaughtUp.Depth = 0;
            this.lblAllCaughtUp.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAllCaughtUp.Image = global::CompanyStudio.Properties.Resources.emoticon_happy;
            this.lblAllCaughtUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAllCaughtUp.Location = new System.Drawing.Point(261, 236);
            this.lblAllCaughtUp.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.lblAllCaughtUp.Name = "lblAllCaughtUp";
            this.lblAllCaughtUp.Size = new System.Drawing.Size(164, 19);
            this.lblAllCaughtUp.TabIndex = 0;
            this.lblAllCaughtUp.Text = "🎉 You\'re all caught up!";
            this.lblAllCaughtUp.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 90);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.headerLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.tbpToDoList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlQuickLinks);
            this.splitContainer1.Panel2.Controls.Add(this.headerLabel2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 300);
            this.splitContainer1.SplitterDistance = 561;
            this.splitContainer1.TabIndex = 4;
            // 
            // headerLabel1
            // 
            this.headerLabel1.AutoSize = true;
            this.headerLabel1.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.headerLabel1.Location = new System.Drawing.Point(3, 2);
            this.headerLabel1.Name = "headerLabel1";
            this.headerLabel1.Size = new System.Drawing.Size(133, 18);
            this.headerLabel1.TabIndex = 0;
            this.headerLabel1.Text = "Your To-Do List:";
            // 
            // tbpToDoList
            // 
            this.tbpToDoList.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tbpToDoList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbpToDoList.BaseColor = System.Drawing.Color.White;
            this.tbpToDoList.Controls.Add(this.tabAll);
            this.tbpToDoList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbpToDoList.ItemSize = new System.Drawing.Size(30, 115);
            this.tbpToDoList.Location = new System.Drawing.Point(6, 21);
            this.tbpToDoList.Multiline = true;
            this.tbpToDoList.Name = "tbpToDoList";
            this.tbpToDoList.NormalTextColor = System.Drawing.Color.DimGray;
            this.tbpToDoList.SelectedIndex = 0;
            this.tbpToDoList.SelectedTabBackColor = System.Drawing.Color.White;
            this.tbpToDoList.SelectedTextColor = System.Drawing.Color.Black;
            this.tbpToDoList.ShowOuterBorders = true;
            this.tbpToDoList.Size = new System.Drawing.Size(550, 274);
            this.tbpToDoList.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbpToDoList.SquareColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(87)))), ((int)(((byte)(100)))));
            this.tbpToDoList.TabCursor = System.Windows.Forms.Cursors.Hand;
            this.tbpToDoList.TabIndex = 0;
            // 
            // tabAll
            // 
            this.tabAll.AutoScroll = true;
            this.tabAll.BackColor = System.Drawing.Color.White;
            this.tabAll.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabAll.Location = new System.Drawing.Point(119, 4);
            this.tabAll.Name = "tabAll";
            this.tabAll.Padding = new System.Windows.Forms.Padding(3);
            this.tabAll.Size = new System.Drawing.Size(427, 266);
            this.tabAll.TabIndex = 0;
            this.tabAll.Text = "All Businesses";
            // 
            // pnlQuickLinks
            // 
            this.pnlQuickLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQuickLinks.AutoScroll = true;
            this.pnlQuickLinks.Controls.Add(this.lnkQuickAccess);
            this.pnlQuickLinks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlQuickLinks.Location = new System.Drawing.Point(3, 25);
            this.pnlQuickLinks.Name = "pnlQuickLinks";
            this.pnlQuickLinks.Size = new System.Drawing.Size(227, 270);
            this.pnlQuickLinks.TabIndex = 2;
            this.pnlQuickLinks.WrapContents = false;
            // 
            // lnkQuickAccess
            // 
            this.lnkQuickAccess.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(72)))), ((int)(((byte)(20)))));
            this.lnkQuickAccess.AutoSize = true;
            this.lnkQuickAccess.BackColor = System.Drawing.Color.Transparent;
            this.lnkQuickAccess.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lnkQuickAccess.Image = global::CompanyStudio.Properties.Resources.add;
            this.lnkQuickAccess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkQuickAccess.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkQuickAccess.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(119)))), ((int)(((byte)(70)))));
            this.lnkQuickAccess.Location = new System.Drawing.Point(3, 0);
            this.lnkQuickAccess.Name = "lnkQuickAccess";
            this.lnkQuickAccess.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lnkQuickAccess.Size = new System.Drawing.Size(142, 20);
            this.lnkQuickAccess.TabIndex = 0;
            this.lnkQuickAccess.TabStop = true;
            this.lnkQuickAccess.Text = "Add Quick Access";
            this.lnkQuickAccess.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(119)))), ((int)(((byte)(70)))));
            this.lnkQuickAccess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQuickAccess_LinkClicked);
            // 
            // headerLabel2
            // 
            this.headerLabel2.AutoSize = true;
            this.headerLabel2.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel2.ForeColor = System.Drawing.Color.DimGray;
            this.headerLabel2.Location = new System.Drawing.Point(3, 2);
            this.headerLabel2.Name = "headerLabel2";
            this.headerLabel2.Size = new System.Drawing.Size(117, 18);
            this.headerLabel2.TabIndex = 1;
            this.headerLabel2.Text = "Quick Access:";
            // 
            // chkAlwaysShowStart
            // 
            this.chkAlwaysShowStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAlwaysShowStart.Checked = true;
            this.chkAlwaysShowStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAlwaysShowStart.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8=";
            this.chkAlwaysShowStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAlwaysShowStart.Image = null;
            this.chkAlwaysShowStart.Location = new System.Drawing.Point(3, 396);
            this.chkAlwaysShowStart.Name = "chkAlwaysShowStart";
            this.chkAlwaysShowStart.NoRounding = false;
            this.chkAlwaysShowStart.Size = new System.Drawing.Size(326, 17);
            this.chkAlwaysShowStart.TabIndex = 3;
            this.chkAlwaysShowStart.Text = "Always show this Start Page when Company Studio starts";
            this.chkAlwaysShowStart.Transparent = false;
            this.chkAlwaysShowStart.CheckedChanged += new ReaLTaiizor.Controls.AirCheckBox.CheckedChangedEventHandler(this.chkAlwaysShowStart_CheckedChanged);
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.bigLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bigLabel1.Location = new System.Drawing.Point(3, -5);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(467, 46);
            this.bigLabel1.TabIndex = 2;
            this.bigLabel1.Text = "Welcome to Company Studio!";
            // 
            // smallLabel1
            // 
            this.smallLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smallLabel1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.smallLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.smallLabel1.Location = new System.Drawing.Point(3, 41);
            this.smallLabel1.Name = "smallLabel1";
            this.smallLabel1.Size = new System.Drawing.Size(785, 29);
            this.smallLabel1.TabIndex = 1;
            this.smallLabel1.Text = resources.GetString("smallLabel1.Text");
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(130, 115);
            this.loader.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(418, 266);
            this.loader.TabIndex = 1;
            // 
            // ctxQuickAccessMenu
            // 
            this.ctxQuickAccessMenu.BackColor = System.Drawing.Color.White;
            this.ctxQuickAccessMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ctxQuickAccessMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxQuickAccessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditQuickAccess,
            this.toolStripMenuItem1,
            this.tsmiDeleteQuickAccess,
            this.toolStripMenuItem2,
            this.tsmiQuickAccessMoveUp,
            this.tsmiQuickAccessMoveDown});
            this.ctxQuickAccessMenu.Name = "ctxQuickAccessMenu";
            this.ctxQuickAccessMenu.Size = new System.Drawing.Size(131, 106);
            this.ctxQuickAccessMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxQuickAccessMenu_Opening);
            // 
            // tsmiEditQuickAccess
            // 
            this.tsmiEditQuickAccess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tsmiEditQuickAccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tsmiEditQuickAccess.Name = "tsmiEditQuickAccess";
            this.tsmiEditQuickAccess.Size = new System.Drawing.Size(130, 22);
            this.tsmiEditQuickAccess.Text = "Edit";
            this.tsmiEditQuickAccess.Click += new System.EventHandler(this.tsmiEditQuickAccess_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(127, 6);
            // 
            // tsmiDeleteQuickAccess
            // 
            this.tsmiDeleteQuickAccess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tsmiDeleteQuickAccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tsmiDeleteQuickAccess.Name = "tsmiDeleteQuickAccess";
            this.tsmiDeleteQuickAccess.Size = new System.Drawing.Size(130, 22);
            this.tsmiDeleteQuickAccess.Text = "Delete";
            this.tsmiDeleteQuickAccess.Click += new System.EventHandler(this.tsmiDeleteQuickAccess_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(127, 6);
            // 
            // tsmiQuickAccessMoveUp
            // 
            this.tsmiQuickAccessMoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tsmiQuickAccessMoveUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tsmiQuickAccessMoveUp.Name = "tsmiQuickAccessMoveUp";
            this.tsmiQuickAccessMoveUp.Size = new System.Drawing.Size(130, 22);
            this.tsmiQuickAccessMoveUp.Text = "Move Up";
            this.tsmiQuickAccessMoveUp.Click += new System.EventHandler(this.tsmiQuickAccessMoveUp_Click);
            // 
            // tsmiQuickAccessMoveDown
            // 
            this.tsmiQuickAccessMoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tsmiQuickAccessMoveDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tsmiQuickAccessMoveDown.Name = "tsmiQuickAccessMoveDown";
            this.tsmiQuickAccessMoveDown.Size = new System.Drawing.Size(130, 22);
            this.tsmiQuickAccessMoveDown.Text = "Move Down";
            this.tsmiQuickAccessMoveDown.Click += new System.EventHandler(this.tsmiQuickAccessMoveDown_Click);
            // 
            // frmStartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 416);
            this.CloseButtonVisible = false;
            this.Controls.Add(this.airForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(112, 35);
            this.Name = "frmStartPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.frmStartPage_Load);
            this.airForm1.ResumeLayout(false);
            this.airForm1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tbpToDoList.ResumeLayout(false);
            this.pnlQuickLinks.ResumeLayout(false);
            this.pnlQuickLinks.PerformLayout();
            this.ctxQuickAccessMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private ReaLTaiizor.Forms.AirForm airForm1;
        private ReaLTaiizor.Controls.AirTabPage tbpToDoList;
        private System.Windows.Forms.TabPage tabAll;
        private ReaLTaiizor.Controls.HeaderLabel headerLabel1;
        private ReaLTaiizor.Controls.SmallLabel smallLabel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.AirCheckBox chkAlwaysShowStart;
        private ReaLTaiizor.Controls.MaterialLabel lblAllCaughtUp;
        private Loader loader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ReaLTaiizor.Controls.HeaderLabel headerLabel2;
        private System.Windows.Forms.FlowLayoutPanel pnlQuickLinks;
        private ReaLTaiizor.Controls.DungeonLinkLabel lnkQuickAccess;
        private ReaLTaiizor.Controls.CrownContextMenuStrip ctxQuickAccessMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditQuickAccess;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteQuickAccess;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuickAccessMoveUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuickAccessMoveDown;
    }
}