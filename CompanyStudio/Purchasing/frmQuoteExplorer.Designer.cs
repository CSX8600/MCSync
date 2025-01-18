namespace CompanyStudio.Purchasing
{
    partial class frmQuoteExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuoteExplorer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolRequestQuote = new System.Windows.Forms.ToolStripButton();
            this.toolDeleteRequest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolIssueQuote = new System.Windows.Forms.ToolStripButton();
            this.toolDeleteQuote = new System.Windows.Forms.ToolStripButton();
            this.toolCloneQuote = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolIssueFromRequest = new System.Windows.Forms.ToolStripButton();
            this.treQuotes = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRequestQuote,
            this.toolDeleteRequest,
            this.toolStripSeparator1,
            this.toolIssueQuote,
            this.toolDeleteQuote,
            this.toolCloneQuote,
            this.toolStripSeparator2,
            this.toolIssueFromRequest});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(499, 46);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolRequestQuote
            // 
            this.toolRequestQuote.Image = global::CompanyStudio.Properties.Resources.comment_add;
            this.toolRequestQuote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRequestQuote.Name = "toolRequestQuote";
            this.toolRequestQuote.Size = new System.Drawing.Size(105, 20);
            this.toolRequestQuote.Text = "Request Quote";
            this.toolRequestQuote.Click += new System.EventHandler(this.toolRequestQuote_Click);
            // 
            // toolDeleteRequest
            // 
            this.toolDeleteRequest.Enabled = false;
            this.toolDeleteRequest.Image = global::CompanyStudio.Properties.Resources.comment_delete;
            this.toolDeleteRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteRequest.Name = "toolDeleteRequest";
            this.toolDeleteRequest.Size = new System.Drawing.Size(105, 20);
            this.toolDeleteRequest.Text = "Delete Request";
            this.toolDeleteRequest.Click += new System.EventHandler(this.toolDeleteRequest_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolIssueQuote
            // 
            this.toolIssueQuote.Image = global::CompanyStudio.Properties.Resources.comments_add;
            this.toolIssueQuote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolIssueQuote.Name = "toolIssueQuote";
            this.toolIssueQuote.Size = new System.Drawing.Size(89, 20);
            this.toolIssueQuote.Text = "Issue Quote";
            this.toolIssueQuote.Click += new System.EventHandler(this.toolIssueQuote_Click);
            // 
            // toolDeleteQuote
            // 
            this.toolDeleteQuote.Enabled = false;
            this.toolDeleteQuote.Image = global::CompanyStudio.Properties.Resources.comments_delete;
            this.toolDeleteQuote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteQuote.Name = "toolDeleteQuote";
            this.toolDeleteQuote.Size = new System.Drawing.Size(96, 20);
            this.toolDeleteQuote.Text = "Delete Quote";
            this.toolDeleteQuote.Click += new System.EventHandler(this.toolDeleteQuote_Click);
            // 
            // toolCloneQuote
            // 
            this.toolCloneQuote.Enabled = false;
            this.toolCloneQuote.Image = global::CompanyStudio.Properties.Resources.page_copy;
            this.toolCloneQuote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCloneQuote.Name = "toolCloneQuote";
            this.toolCloneQuote.Size = new System.Drawing.Size(94, 20);
            this.toolCloneQuote.Text = "Clone Quote";
            this.toolCloneQuote.Click += new System.EventHandler(this.toolCloneQuote_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // toolIssueFromRequest
            // 
            this.toolIssueFromRequest.Enabled = false;
            this.toolIssueFromRequest.Image = global::CompanyStudio.Properties.Resources.comment_edit;
            this.toolIssueFromRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolIssueFromRequest.Name = "toolIssueFromRequest";
            this.toolIssueFromRequest.Size = new System.Drawing.Size(165, 20);
            this.toolIssueFromRequest.Text = "Issue Quote From Request";
            this.toolIssueFromRequest.Click += new System.EventHandler(this.toolIssueFromRequest_Click);
            // 
            // treQuotes
            // 
            this.treQuotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treQuotes.ImageIndex = 0;
            this.treQuotes.ImageList = this.imgList;
            this.treQuotes.Location = new System.Drawing.Point(0, 46);
            this.treQuotes.Name = "treQuotes";
            this.treQuotes.SelectedImageIndex = 0;
            this.treQuotes.Size = new System.Drawing.Size(499, 404);
            this.treQuotes.TabIndex = 1;
            this.treQuotes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treQuotes_AfterSelect);
            this.treQuotes.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treQuotes_NodeMouseDoubleClick);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgList.ImageSize = new System.Drawing.Size(16, 16);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmQuoteExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 450);
            this.Controls.Add(this.treQuotes);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmQuoteExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quote Explorer";
            this.Load += new System.EventHandler(this.frmQuoteExplorer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmQuoteExplorer_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRequestQuote;
        private System.Windows.Forms.ToolStripButton toolDeleteRequest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolIssueQuote;
        private System.Windows.Forms.ToolStripButton toolDeleteQuote;
        private System.Windows.Forms.TreeView treQuotes;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolIssueFromRequest;
        private System.Windows.Forms.ToolStripButton toolCloneQuote;
    }
}