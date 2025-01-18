namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    partial class frmDraftFulfillmentPlans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDraftFulfillmentPlans));
            this.dgvFulfillmentPlans = new System.Windows.Forms.DataGridView();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPOLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewFulfillmentPlan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbApplySelected = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectNone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClone = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFulfillmentPlans
            // 
            this.dgvFulfillmentPlans.AllowUserToAddRows = false;
            this.dgvFulfillmentPlans.AllowUserToDeleteRows = false;
            this.dgvFulfillmentPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFulfillmentPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRailcar,
            this.colPOLines,
            this.colRoute});
            this.dgvFulfillmentPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFulfillmentPlans.Location = new System.Drawing.Point(0, 25);
            this.dgvFulfillmentPlans.Name = "dgvFulfillmentPlans";
            this.dgvFulfillmentPlans.ReadOnly = true;
            this.dgvFulfillmentPlans.RowHeadersVisible = false;
            this.dgvFulfillmentPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillmentPlans.Size = new System.Drawing.Size(549, 166);
            this.dgvFulfillmentPlans.TabIndex = 0;
            this.dgvFulfillmentPlans.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFulfillmentPlans_CellMouseDoubleClick);
            // 
            // colRailcar
            // 
            this.colRailcar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRailcar.HeaderText = "Railcar";
            this.colRailcar.Name = "colRailcar";
            this.colRailcar.ReadOnly = true;
            // 
            // colPOLines
            // 
            this.colPOLines.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPOLines.HeaderText = "PO Lines";
            this.colPOLines.Name = "colPOLines";
            this.colPOLines.ReadOnly = true;
            // 
            // colRoute
            // 
            this.colRoute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRoute.HeaderText = "Route";
            this.colRoute.Name = "colRoute";
            this.colRoute.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewFulfillmentPlan,
            this.toolStripSeparator1,
            this.tsbApplySelected,
            this.tsbSelectNone,
            this.toolStripSeparator2,
            this.tsbClone});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(549, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNewFulfillmentPlan
            // 
            this.tsbNewFulfillmentPlan.Image = global::GovernmentPortal.Properties.Resources.package_add;
            this.tsbNewFulfillmentPlan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNewFulfillmentPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewFulfillmentPlan.Name = "tsbNewFulfillmentPlan";
            this.tsbNewFulfillmentPlan.Size = new System.Drawing.Size(122, 22);
            this.tsbNewFulfillmentPlan.Text = "New Fulfillment Plan";
            this.tsbNewFulfillmentPlan.Click += new System.EventHandler(this.tsbNewFulfillmentPlan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbApplySelected
            // 
            this.tsbApplySelected.Image = global::GovernmentPortal.Properties.Resources.accept;
            this.tsbApplySelected.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbApplySelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApplySelected.Name = "tsbApplySelected";
            this.tsbApplySelected.Size = new System.Drawing.Size(98, 22);
            this.tsbApplySelected.Text = "Apply Selected";
            this.tsbApplySelected.Click += new System.EventHandler(this.tsbApplySelected_Click);
            // 
            // tsbSelectNone
            // 
            this.tsbSelectNone.Image = global::GovernmentPortal.Properties.Resources.cancel;
            this.tsbSelectNone.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSelectNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectNone.Name = "tsbSelectNone";
            this.tsbSelectNone.Size = new System.Drawing.Size(84, 22);
            this.tsbSelectNone.Text = "Select None";
            this.tsbSelectNone.Click += new System.EventHandler(this.tsbSelectNone_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbClone
            // 
            this.tsbClone.Image = global::GovernmentPortal.Properties.Resources.package_go;
            this.tsbClone.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClone.Name = "tsbClone";
            this.tsbClone.Size = new System.Drawing.Size(128, 22);
            this.tsbClone.Text = "Clone Fulfillment Plan";
            this.tsbClone.Click += new System.EventHandler(this.tsbClone_Click);
            // 
            // frmDraftFulfillmentPlans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 191);
            this.Controls.Add(this.dgvFulfillmentPlans);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDraftFulfillmentPlans";
            this.Text = "Fulfillment Plans";
            this.Load += new System.EventHandler(this.frmDraftFulfillmentPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFulfillmentPlans;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNewFulfillmentPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPOLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoute;
        private System.Windows.Forms.ToolStripButton tsbSelectNone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbApplySelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbClone;
    }
}