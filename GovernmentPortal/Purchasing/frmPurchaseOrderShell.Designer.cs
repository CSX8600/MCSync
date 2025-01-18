namespace GovernmentPortal.Purchasing
{
    partial class frmPurchaseOrderShell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrderShell));
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trePurchaseOrders = new System.Windows.Forms.TreeView();
            this.treeImages = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAddPurchaseOrder = new System.Windows.Forms.ToolStripSplitButton();
            this.toolNewFromTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Purchase Orders";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trePurchaseOrders);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(776, 525);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 1;
            // 
            // trePurchaseOrders
            // 
            this.trePurchaseOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trePurchaseOrders.ImageIndex = 0;
            this.trePurchaseOrders.ImageList = this.treeImages;
            this.trePurchaseOrders.Location = new System.Drawing.Point(0, 23);
            this.trePurchaseOrders.Name = "trePurchaseOrders";
            this.trePurchaseOrders.SelectedImageIndex = 0;
            this.trePurchaseOrders.Size = new System.Drawing.Size(178, 502);
            this.trePurchaseOrders.TabIndex = 0;
            this.trePurchaseOrders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trePurchaseOrders_AfterSelect);
            // 
            // treeImages
            // 
            this.treeImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.treeImages.ImageSize = new System.Drawing.Size(16, 16);
            this.treeImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddPurchaseOrder,
            this.toolDelete});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(178, 23);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolAddPurchaseOrder
            // 
            this.toolAddPurchaseOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewFromTemplate});
            this.toolAddPurchaseOrder.Image = global::GovernmentPortal.Properties.Resources.cart_add;
            this.toolAddPurchaseOrder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAddPurchaseOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddPurchaseOrder.Name = "toolAddPurchaseOrder";
            this.toolAddPurchaseOrder.Size = new System.Drawing.Size(82, 20);
            this.toolAddPurchaseOrder.Text = "Add New";
            this.toolAddPurchaseOrder.ButtonClick += new System.EventHandler(this.toolAddPurchaseOrder_ButtonClick);
            // 
            // toolNewFromTemplate
            // 
            this.toolNewFromTemplate.Image = global::GovernmentPortal.Properties.Resources.script_go;
            this.toolNewFromTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolNewFromTemplate.Name = "toolNewFromTemplate";
            this.toolNewFromTemplate.Size = new System.Drawing.Size(157, 22);
            this.toolNewFromTemplate.Text = "From Template...";
            // 
            // toolDelete
            // 
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::GovernmentPortal.Properties.Resources.delete;
            this.toolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(58, 20);
            this.toolDelete.Text = "Delete";
            // 
            // frmPurchaseOrderShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 574);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPurchaseOrderShell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Maintenance";
            this.Load += new System.EventHandler(this.frmPurchaseOrderShell_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trePurchaseOrders;
        private System.Windows.Forms.ImageList treeImages;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolAddPurchaseOrder;
        private System.Windows.Forms.ToolStripMenuItem toolNewFromTemplate;
        private System.Windows.Forms.ToolStripButton toolDelete;
    }
}