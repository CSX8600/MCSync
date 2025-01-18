namespace CompanyStudio.Purchasing
{
    partial class frmBillOfLadingExplorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillOfLadingExplorer));
            this.treBOLs = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.loader = new CompanyStudio.Loader();
            this.toolAcceptBOL = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolAcceptMultiple = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treBOLs
            // 
            this.treBOLs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treBOLs.ImageIndex = 0;
            this.treBOLs.ImageList = this.imageList;
            this.treBOLs.Location = new System.Drawing.Point(0, 25);
            this.treBOLs.Name = "treBOLs";
            this.treBOLs.SelectedImageIndex = 0;
            this.treBOLs.ShowNodeToolTips = true;
            this.treBOLs.Size = new System.Drawing.Size(514, 476);
            this.treBOLs.TabIndex = 0;
            this.treBOLs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treBOLs_NodeMouseClick);
            this.treBOLs.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treBOLs_NodeMouseDoubleClick);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(159, 200);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 1;
            this.loader.Visible = false;
            // 
            // toolAcceptBOL
            // 
            this.toolAcceptBOL.Enabled = false;
            this.toolAcceptBOL.Image = global::CompanyStudio.Properties.Resources.script_lightning;
            this.toolAcceptBOL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAcceptBOL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAcceptBOL.Name = "toolAcceptBOL";
            this.toolAcceptBOL.Size = new System.Drawing.Size(89, 22);
            this.toolAcceptBOL.Text = "Accept BOL";
            this.toolAcceptBOL.Click += new System.EventHandler(this.toolAcceptBOL_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolAcceptMultiple
            // 
            this.toolAcceptMultiple.Image = global::CompanyStudio.Properties.Resources.script_lightning;
            this.toolAcceptMultiple.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAcceptMultiple.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAcceptMultiple.Name = "toolAcceptMultiple";
            this.toolAcceptMultiple.Size = new System.Drawing.Size(111, 22);
            this.toolAcceptMultiple.Text = "Accept Multiple";
            this.toolAcceptMultiple.Click += new System.EventHandler(this.toolAcceptMultiple_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAcceptBOL,
            this.toolStripSeparator1,
            this.toolAcceptMultiple});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(514, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // frmBillOfLadingExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 501);
            this.Controls.Add(this.treBOLs);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmBillOfLadingExplorer";
            this.Text = "Bill Of Lading Explorer";
            this.Load += new System.EventHandler(this.frmBillOfLadingExplorer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBillOfLadingExplorer_KeyUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treBOLs;
        private System.Windows.Forms.ImageList imageList;
        private Loader loader;
        private System.Windows.Forms.ToolStripButton toolAcceptBOL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolAcceptMultiple;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}