namespace CompanyStudio.Purchasing
{
    partial class frmPurchaseOrderExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrderExplorer));
            this.trePurchaseOrders = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.loader = new CompanyStudio.Loader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAddPurchaseOrder = new System.Windows.Forms.ToolStripSplitButton();
            this.toolNewFromTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trePurchaseOrders
            // 
            this.trePurchaseOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trePurchaseOrders.ImageIndex = 0;
            this.trePurchaseOrders.ImageList = this.imageList;
            this.trePurchaseOrders.Location = new System.Drawing.Point(0, 25);
            this.trePurchaseOrders.Name = "trePurchaseOrders";
            this.trePurchaseOrders.SelectedImageIndex = 0;
            this.trePurchaseOrders.Size = new System.Drawing.Size(498, 425);
            this.trePurchaseOrders.TabIndex = 0;
            this.trePurchaseOrders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trePurchaseOrders_AfterSelect);
            this.trePurchaseOrders.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trePurchaseOrders_NodeMouseDoubleClick);
            this.trePurchaseOrders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trePurchaseOrders_KeyDown);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "completed");
            this.imageList.Images.SetKeyName(1, "rejected");
            this.imageList.Images.SetKeyName(2, "draft");
            this.imageList.Images.SetKeyName(3, "pending");
            this.imageList.Images.SetKeyName(4, "inprogress");
            this.imageList.Images.SetKeyName(5, "accepted");
            this.imageList.Images.SetKeyName(6, "received");
            this.imageList.Images.SetKeyName(7, "sent");
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(155, 172);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 2;
            this.loader.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddPurchaseOrder,
            this.toolDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(498, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolAddPurchaseOrder
            // 
            this.toolAddPurchaseOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewFromTemplate});
            this.toolAddPurchaseOrder.Image = global::CompanyStudio.Properties.Resources.cart_add;
            this.toolAddPurchaseOrder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAddPurchaseOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddPurchaseOrder.Name = "toolAddPurchaseOrder";
            this.toolAddPurchaseOrder.Size = new System.Drawing.Size(138, 22);
            this.toolAddPurchaseOrder.Text = "New Purchase Order";
            this.toolAddPurchaseOrder.ButtonClick += new System.EventHandler(this.toolAddPurchaseOrder_Click);
            // 
            // toolNewFromTemplate
            // 
            this.toolNewFromTemplate.Image = global::CompanyStudio.Properties.Resources.script_go;
            this.toolNewFromTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolNewFromTemplate.Name = "toolNewFromTemplate";
            this.toolNewFromTemplate.Size = new System.Drawing.Size(157, 22);
            this.toolNewFromTemplate.Text = "From Template...";
            this.toolNewFromTemplate.Click += new System.EventHandler(this.toolNewFromTemplate_Click);
            // 
            // toolDelete
            // 
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::CompanyStudio.Properties.Resources.delete;
            this.toolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(136, 22);
            this.toolDelete.Text = "Delete Purchase Order";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // frmPurchaseOrderExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 450);
            this.Controls.Add(this.trePurchaseOrders);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPurchaseOrderExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Explorer";
            this.Load += new System.EventHandler(this.frmPurchaseOrderExplorer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPurchaseOrderExplorer_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trePurchaseOrders;
        private Loader loader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.ToolStripSplitButton toolAddPurchaseOrder;
        private System.Windows.Forms.ToolStripMenuItem toolNewFromTemplate;
    }
}