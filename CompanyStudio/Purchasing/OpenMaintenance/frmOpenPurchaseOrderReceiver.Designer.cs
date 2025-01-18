namespace CompanyStudio.Purchasing.OpenMaintenance
{
    partial class frmOpenPurchaseOrderReceiver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenPurchaseOrderReceiver));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlUnfulfilledLines = new System.Windows.Forms.Panel();
            this.lblPONumber = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOrderFrom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOrderDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDescription = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvFulfillments = new System.Windows.Forms.DataGridView();
            this.colFulfillmentTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFulfillmentItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFulfillmentReceived = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsInvoiced = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuAddFulfilllmentWizard = new System.Windows.Forms.ToolStripSplitButton();
            this.toolManualFulfillmentEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDeleteFulfillment = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.colInvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loader = new CompanyStudio.Loader();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoApproving = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbEnableAutoApproval = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDisableAutoApproval = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillments)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlUnfulfilledLines);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unfulfilled Purchase Order Lines";
            // 
            // pnlUnfulfilledLines
            // 
            this.pnlUnfulfilledLines.AutoScroll = true;
            this.pnlUnfulfilledLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUnfulfilledLines.Location = new System.Drawing.Point(3, 16);
            this.pnlUnfulfilledLines.Name = "pnlUnfulfilledLines";
            this.pnlUnfulfilledLines.Size = new System.Drawing.Size(438, 174);
            this.pnlUnfulfilledLines.TabIndex = 0;
            // 
            // lblPONumber
            // 
            this.lblPONumber.AutoSize = true;
            this.lblPONumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPONumber.Location = new System.Drawing.Point(12, 29);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(163, 20);
            this.lblPONumber.TabIndex = 1;
            this.lblPONumber.Text = "Open Purchase Order";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblOrderFrom,
            this.toolStripStatusLabel2,
            this.lblOrderDate,
            this.toolStripStatusLabel3,
            this.lblDescription});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(71, 19);
            this.toolStripStatusLabel1.Text = "Order From:";
            // 
            // lblOrderFrom
            // 
            this.lblOrderFrom.Name = "lblOrderFrom";
            this.lblOrderFrom.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(71, 19);
            this.toolStripStatusLabel2.Text = "Order Date:";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(74, 19);
            this.toolStripStatusLabel3.Text = "Description:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = false;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(200, 19);
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(776, 375);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(444, 375);
            this.splitContainer2.SplitterDistance = 193;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvFulfillments);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 178);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fulfillments";
            // 
            // dgvFulfillments
            // 
            this.dgvFulfillments.AllowUserToAddRows = false;
            this.dgvFulfillments.AllowUserToDeleteRows = false;
            this.dgvFulfillments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFulfillments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFulfillmentTime,
            this.colRailcar,
            this.colFulfillmentItems,
            this.colFulfillmentReceived,
            this.colIsInvoiced});
            this.dgvFulfillments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFulfillments.Location = new System.Drawing.Point(3, 41);
            this.dgvFulfillments.Name = "dgvFulfillments";
            this.dgvFulfillments.ReadOnly = true;
            this.dgvFulfillments.RowHeadersVisible = false;
            this.dgvFulfillments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillments.Size = new System.Drawing.Size(438, 134);
            this.dgvFulfillments.TabIndex = 1;
            this.dgvFulfillments.SelectionChanged += new System.EventHandler(this.dgvFulfillments_SelectionChanged);
            // 
            // colFulfillmentTime
            // 
            this.colFulfillmentTime.HeaderText = "Fulfillment Time";
            this.colFulfillmentTime.Name = "colFulfillmentTime";
            this.colFulfillmentTime.ReadOnly = true;
            this.colFulfillmentTime.Width = 120;
            // 
            // colRailcar
            // 
            this.colRailcar.HeaderText = "Railcar";
            this.colRailcar.Name = "colRailcar";
            this.colRailcar.ReadOnly = true;
            // 
            // colFulfillmentItems
            // 
            this.colFulfillmentItems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFulfillmentItems.HeaderText = "Item";
            this.colFulfillmentItems.Name = "colFulfillmentItems";
            this.colFulfillmentItems.ReadOnly = true;
            // 
            // colFulfillmentReceived
            // 
            this.colFulfillmentReceived.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colFulfillmentReceived.HeaderText = "Is Received";
            this.colFulfillmentReceived.Name = "colFulfillmentReceived";
            this.colFulfillmentReceived.ReadOnly = true;
            this.colFulfillmentReceived.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFulfillmentReceived.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colFulfillmentReceived.Width = 82;
            // 
            // colIsInvoiced
            // 
            this.colIsInvoiced.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colIsInvoiced.HeaderText = "Is Invoiced";
            this.colIsInvoiced.Name = "colIsInvoiced";
            this.colIsInvoiced.ReadOnly = true;
            this.colIsInvoiced.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colIsInvoiced.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colIsInvoiced.Width = 78;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFulfilllmentWizard,
            this.toolDeleteFulfillment});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(438, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuAddFulfilllmentWizard
            // 
            this.mnuAddFulfilllmentWizard.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolManualFulfillmentEntry});
            this.mnuAddFulfilllmentWizard.Image = global::CompanyStudio.Properties.Resources.wand;
            this.mnuAddFulfilllmentWizard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuAddFulfilllmentWizard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAddFulfilllmentWizard.Name = "mnuAddFulfilllmentWizard";
            this.mnuAddFulfilllmentWizard.Size = new System.Drawing.Size(160, 22);
            this.mnuAddFulfilllmentWizard.Text = "Add Fulfillment Wizard";
            this.mnuAddFulfilllmentWizard.ButtonClick += new System.EventHandler(this.mnuAddFulfilllmentWizard_ButtonClick);
            // 
            // toolManualFulfillmentEntry
            // 
            this.toolManualFulfillmentEntry.Image = global::CompanyStudio.Properties.Resources.package_add;
            this.toolManualFulfillmentEntry.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolManualFulfillmentEntry.Name = "toolManualFulfillmentEntry";
            this.toolManualFulfillmentEntry.Size = new System.Drawing.Size(144, 22);
            this.toolManualFulfillmentEntry.Text = "Manual Entry";
            this.toolManualFulfillmentEntry.Click += new System.EventHandler(this.toolManualFulfillmentEntry_Click);
            // 
            // toolDeleteFulfillment
            // 
            this.toolDeleteFulfillment.Enabled = false;
            this.toolDeleteFulfillment.Image = global::CompanyStudio.Properties.Resources.package_delete;
            this.toolDeleteFulfillment.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDeleteFulfillment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteFulfillment.Name = "toolDeleteFulfillment";
            this.toolDeleteFulfillment.Size = new System.Drawing.Size(120, 22);
            this.toolDeleteFulfillment.Text = "Delete Fulfillment";
            this.toolDeleteFulfillment.Click += new System.EventHandler(this.toolDeleteFulfillment_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvInvoices);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 375);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Invoices";
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInvoiceNumber,
            this.colInvoiceDate,
            this.colDueDate,
            this.colAmount,
            this.colStatus});
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.Location = new System.Drawing.Point(3, 16);
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersVisible = false;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(322, 356);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInvoices_CellMouseDoubleClickAsync);
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.HeaderText = "Invoice #";
            this.colInvoiceNumber.Name = "colInvoiceNumber";
            this.colInvoiceNumber.ReadOnly = true;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.HeaderText = "Inv Date";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.ReadOnly = true;
            // 
            // colDueDate
            // 
            this.colDueDate.HeaderText = "Due Date";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // loader
            // 
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(302, 175);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 4;
            this.loader.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tsbAutoApproving});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(800, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = global::CompanyStudio.Properties.Resources.cart_delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(140, 22);
            this.tsbClose.Text = "Close Purchase Order";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tsbAutoApproving
            // 
            this.tsbAutoApproving.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEnableAutoApproval,
            this.tsbDisableAutoApproval});
            this.tsbAutoApproving.Image = global::CompanyStudio.Properties.Resources.accept;
            this.tsbAutoApproving.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoApproving.Name = "tsbAutoApproving";
            this.tsbAutoApproving.Size = new System.Drawing.Size(123, 22);
            this.tsbAutoApproving.Text = "Auto-Approving";
            // 
            // tsbEnableAutoApproval
            // 
            this.tsbEnableAutoApproval.Image = global::CompanyStudio.Properties.Resources.accept;
            this.tsbEnableAutoApproval.Name = "tsbEnableAutoApproval";
            this.tsbEnableAutoApproval.Size = new System.Drawing.Size(236, 22);
            this.tsbEnableAutoApproval.Text = "Enable Future Auto-Approvals";
            this.tsbEnableAutoApproval.Click += new System.EventHandler(this.tsbEnableAutoApproval_Click);
            // 
            // tsbDisableAutoApproval
            // 
            this.tsbDisableAutoApproval.Image = global::CompanyStudio.Properties.Resources.cancel;
            this.tsbDisableAutoApproval.Name = "tsbDisableAutoApproval";
            this.tsbDisableAutoApproval.Size = new System.Drawing.Size(236, 22);
            this.tsbDisableAutoApproval.Text = "Disable Future Auto-Approvals";
            this.tsbDisableAutoApproval.Click += new System.EventHandler(this.tsbDisableAutoApproval_Click);
            // 
            // frmOpenPurchaseOrderReceiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 452);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblPONumber);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.toolStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOpenPurchaseOrderReceiver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Purchase Order";
            this.Load += new System.EventHandler(this.frmOpenPurchaseOrderReceiver_Load);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillments)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPONumber;
        private System.Windows.Forms.Panel pnlUnfulfilledLines;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblOrderFrom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblOrderDate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblDescription;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Loader loader;
        private System.Windows.Forms.DataGridView dgvFulfillments;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton mnuAddFulfilllmentWizard;
        private System.Windows.Forms.ToolStripMenuItem toolManualFulfillmentEntry;
        private System.Windows.Forms.ToolStripButton toolDeleteFulfillment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFulfillmentTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFulfillmentItems;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFulfillmentReceived;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsInvoiced;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripDropDownButton tsbAutoApproving;
        private System.Windows.Forms.ToolStripMenuItem tsbEnableAutoApproval;
        private System.Windows.Forms.ToolStripMenuItem tsbDisableAutoApproval;
    }
}