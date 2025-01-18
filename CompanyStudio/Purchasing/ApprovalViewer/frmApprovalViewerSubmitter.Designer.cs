namespace CompanyStudio.Purchasing.ApprovalViewer
{
    partial class frmApprovalViewerSubmitter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApprovalViewerSubmitter));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlApprovals = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLines = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHasFulfillmentPlan = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tbcDetails = new System.Windows.Forms.TabControl();
            this.tabPOInfo = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.lnkTaxBreakdown = new System.Windows.Forms.LinkLabel();
            this.txtGrossTotal = new System.Windows.Forms.TextBox();
            this.txtEstTax = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabFulfillmentPlans = new System.Windows.Forms.TabPage();
            this.lnkRailcar = new System.Windows.Forms.LinkLabel();
            this.txtRailcarRouting = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDestinationAfterFulfillment = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtStrategicAfterDropOff = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtStrategicAfterPickup = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDropOffTrack = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPurchaseOrderLines = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPickupTrack = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvFulfillmentPlans = new System.Windows.Forms.DataGridView();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPOLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmdWithdrawSubmission = new System.Windows.Forms.Button();
            this.loader = new CompanyStudio.Loader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolSaveTemplate = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).BeginInit();
            this.tbcDetails.SuspendLayout();
            this.tabPOInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabFulfillmentPlans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlApprovals);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(786, 198);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Approvals";
            // 
            // pnlApprovals
            // 
            this.pnlApprovals.AutoScroll = true;
            this.pnlApprovals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlApprovals.Location = new System.Drawing.Point(3, 16);
            this.pnlApprovals.Name = "pnlApprovals";
            this.pnlApprovals.Size = new System.Drawing.Size(780, 179);
            this.pnlApprovals.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Purchase Order Pending Approvals";
            // 
            // dgvLines
            // 
            this.dgvLines.AllowUserToAddRows = false;
            this.dgvLines.AllowUserToDeleteRows = false;
            this.dgvLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDescription,
            this.colQuantity,
            this.colUnitCost,
            this.colLineTotal,
            this.colHasFulfillmentPlan});
            this.dgvLines.Location = new System.Drawing.Point(0, 19);
            this.dgvLines.Name = "dgvLines";
            this.dgvLines.ReadOnly = true;
            this.dgvLines.RowHeadersVisible = false;
            this.dgvLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLines.Size = new System.Drawing.Size(772, 141);
            this.dgvLines.TabIndex = 0;
            // 
            // colType
            // 
            this.colType.HeaderText = "Line Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 80;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 200;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 75;
            // 
            // colUnitCost
            // 
            this.colUnitCost.HeaderText = "Unit Cost";
            this.colUnitCost.Name = "colUnitCost";
            this.colUnitCost.ReadOnly = true;
            this.colUnitCost.Width = 75;
            // 
            // colLineTotal
            // 
            this.colLineTotal.HeaderText = "Line Total";
            this.colLineTotal.Name = "colLineTotal";
            this.colLineTotal.ReadOnly = true;
            this.colLineTotal.Width = 80;
            // 
            // colHasFulfillmentPlan
            // 
            this.colHasFulfillmentPlan.HeaderText = "Has Fulfillment Plan";
            this.colHasFulfillmentPlan.Name = "colHasFulfillmentPlan";
            this.colHasFulfillmentPlan.ReadOnly = true;
            this.colHasFulfillmentPlan.Width = 120;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Order From:";
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.Location = new System.Drawing.Point(71, 19);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(698, 20);
            this.txtFrom.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Order Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description:";
            // 
            // txtDate
            // 
            this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDate.Location = new System.Drawing.Point(71, 45);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(698, 20);
            this.txtDate.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(6, 84);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(763, 61);
            this.txtDescription.TabIndex = 2;
            // 
            // tbcDetails
            // 
            this.tbcDetails.Controls.Add(this.tabPOInfo);
            this.tbcDetails.Controls.Add(this.tabFulfillmentPlans);
            this.tbcDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcDetails.Location = new System.Drawing.Point(0, 0);
            this.tbcDetails.Name = "tbcDetails";
            this.tbcDetails.SelectedIndex = 0;
            this.tbcDetails.Size = new System.Drawing.Size(786, 390);
            this.tbcDetails.TabIndex = 0;
            // 
            // tabPOInfo
            // 
            this.tabPOInfo.Controls.Add(this.splitContainer1);
            this.tabPOInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPOInfo.Name = "tabPOInfo";
            this.tabPOInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPOInfo.Size = new System.Drawing.Size(778, 364);
            this.tabPOInfo.TabIndex = 0;
            this.tabPOInfo.Text = "Purchase Order Information";
            this.tabPOInfo.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txtDescription);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtDate);
            this.splitContainer1.Panel1.Controls.Add(this.txtFrom);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lnkTaxBreakdown);
            this.splitContainer1.Panel2.Controls.Add(this.txtGrossTotal);
            this.splitContainer1.Panel2.Controls.Add(this.txtEstTax);
            this.splitContainer1.Panel2.Controls.Add(this.label17);
            this.splitContainer1.Panel2.Controls.Add(this.label18);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtTotal);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.dgvLines);
            this.splitContainer1.Size = new System.Drawing.Size(772, 358);
            this.splitContainer1.SplitterDistance = 149;
            this.splitContainer1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Purchase Order Information";
            // 
            // lnkTaxBreakdown
            // 
            this.lnkTaxBreakdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkTaxBreakdown.AutoSize = true;
            this.lnkTaxBreakdown.Location = new System.Drawing.Point(251, 188);
            this.lnkTaxBreakdown.Name = "lnkTaxBreakdown";
            this.lnkTaxBreakdown.Size = new System.Drawing.Size(82, 13);
            this.lnkTaxBreakdown.TabIndex = 4;
            this.lnkTaxBreakdown.TabStop = true;
            this.lnkTaxBreakdown.Text = "Tax Breakdown";
            this.lnkTaxBreakdown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTaxBreakdown_LinkClicked);
            // 
            // txtGrossTotal
            // 
            this.txtGrossTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGrossTotal.Location = new System.Drawing.Point(417, 166);
            this.txtGrossTotal.Name = "txtGrossTotal";
            this.txtGrossTotal.ReadOnly = true;
            this.txtGrossTotal.Size = new System.Drawing.Size(62, 20);
            this.txtGrossTotal.TabIndex = 3;
            // 
            // txtEstTax
            // 
            this.txtEstTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtEstTax.Location = new System.Drawing.Point(261, 166);
            this.txtEstTax.Name = "txtEstTax";
            this.txtEstTax.ReadOnly = true;
            this.txtEstTax.Size = new System.Drawing.Size(62, 20);
            this.txtEstTax.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(329, 169);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "Est Gross Total:";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(209, 169);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "Est Tax:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Purchase Order Total:";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotal.Location = new System.Drawing.Point(122, 166);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(81, 20);
            this.txtTotal.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Purchase Order Lines";
            // 
            // tabFulfillmentPlans
            // 
            this.tabFulfillmentPlans.AutoScroll = true;
            this.tabFulfillmentPlans.Controls.Add(this.lnkRailcar);
            this.tabFulfillmentPlans.Controls.Add(this.txtRailcarRouting);
            this.tabFulfillmentPlans.Controls.Add(this.label14);
            this.tabFulfillmentPlans.Controls.Add(this.txtDestinationAfterFulfillment);
            this.tabFulfillmentPlans.Controls.Add(this.label13);
            this.tabFulfillmentPlans.Controls.Add(this.txtStrategicAfterDropOff);
            this.tabFulfillmentPlans.Controls.Add(this.label12);
            this.tabFulfillmentPlans.Controls.Add(this.txtStrategicAfterPickup);
            this.tabFulfillmentPlans.Controls.Add(this.label10);
            this.tabFulfillmentPlans.Controls.Add(this.txtDropOffTrack);
            this.tabFulfillmentPlans.Controls.Add(this.label11);
            this.tabFulfillmentPlans.Controls.Add(this.txtPurchaseOrderLines);
            this.tabFulfillmentPlans.Controls.Add(this.label16);
            this.tabFulfillmentPlans.Controls.Add(this.label15);
            this.tabFulfillmentPlans.Controls.Add(this.txtPickupTrack);
            this.tabFulfillmentPlans.Controls.Add(this.label9);
            this.tabFulfillmentPlans.Controls.Add(this.label8);
            this.tabFulfillmentPlans.Controls.Add(this.dgvFulfillmentPlans);
            this.tabFulfillmentPlans.Location = new System.Drawing.Point(4, 22);
            this.tabFulfillmentPlans.Name = "tabFulfillmentPlans";
            this.tabFulfillmentPlans.Padding = new System.Windows.Forms.Padding(3);
            this.tabFulfillmentPlans.Size = new System.Drawing.Size(778, 364);
            this.tabFulfillmentPlans.TabIndex = 1;
            this.tabFulfillmentPlans.Text = "Fulfillment Plans";
            this.tabFulfillmentPlans.UseVisualStyleBackColor = true;
            // 
            // lnkRailcar
            // 
            this.lnkRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkRailcar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkRailcar.Location = new System.Drawing.Point(155, 104);
            this.lnkRailcar.Name = "lnkRailcar";
            this.lnkRailcar.Size = new System.Drawing.Size(617, 20);
            this.lnkRailcar.TabIndex = 1;
            this.lnkRailcar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkRailcar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRailcar_LinkClicked);
            // 
            // txtRailcarRouting
            // 
            this.txtRailcarRouting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRailcarRouting.Location = new System.Drawing.Point(155, 286);
            this.txtRailcarRouting.Name = "txtRailcarRouting";
            this.txtRailcarRouting.ReadOnly = true;
            this.txtRailcarRouting.Size = new System.Drawing.Size(617, 20);
            this.txtRailcarRouting.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Railcar Routing:";
            // 
            // txtDestinationAfterFulfillment
            // 
            this.txtDestinationAfterFulfillment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestinationAfterFulfillment.Location = new System.Drawing.Point(155, 260);
            this.txtDestinationAfterFulfillment.Name = "txtDestinationAfterFulfillment";
            this.txtDestinationAfterFulfillment.ReadOnly = true;
            this.txtDestinationAfterFulfillment.Size = new System.Drawing.Size(617, 20);
            this.txtDestinationAfterFulfillment.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 263);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Destination after fulfillment:";
            // 
            // txtStrategicAfterDropOff
            // 
            this.txtStrategicAfterDropOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStrategicAfterDropOff.Location = new System.Drawing.Point(155, 234);
            this.txtStrategicAfterDropOff.Name = "txtStrategicAfterDropOff";
            this.txtStrategicAfterDropOff.ReadOnly = true;
            this.txtStrategicAfterDropOff.Size = new System.Drawing.Size(617, 20);
            this.txtStrategicAfterDropOff.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Strategic Track after drop off:";
            // 
            // txtStrategicAfterPickup
            // 
            this.txtStrategicAfterPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStrategicAfterPickup.Location = new System.Drawing.Point(155, 182);
            this.txtStrategicAfterPickup.Name = "txtStrategicAfterPickup";
            this.txtStrategicAfterPickup.ReadOnly = true;
            this.txtStrategicAfterPickup.Size = new System.Drawing.Size(617, 20);
            this.txtStrategicAfterPickup.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Strategic Track after pickup:";
            // 
            // txtDropOffTrack
            // 
            this.txtDropOffTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDropOffTrack.Location = new System.Drawing.Point(155, 208);
            this.txtDropOffTrack.Name = "txtDropOffTrack";
            this.txtDropOffTrack.ReadOnly = true;
            this.txtDropOffTrack.Size = new System.Drawing.Size(617, 20);
            this.txtDropOffTrack.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Drop Off Track:";
            // 
            // txtPurchaseOrderLines
            // 
            this.txtPurchaseOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurchaseOrderLines.Location = new System.Drawing.Point(155, 130);
            this.txtPurchaseOrderLines.Name = "txtPurchaseOrderLines";
            this.txtPurchaseOrderLines.ReadOnly = true;
            this.txtPurchaseOrderLines.Size = new System.Drawing.Size(617, 20);
            this.txtPurchaseOrderLines.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Purchase Order Lines:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Railcar:";
            // 
            // txtPickupTrack
            // 
            this.txtPickupTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPickupTrack.Location = new System.Drawing.Point(155, 156);
            this.txtPickupTrack.Name = "txtPickupTrack";
            this.txtPickupTrack.ReadOnly = true;
            this.txtPickupTrack.Size = new System.Drawing.Size(617, 20);
            this.txtPickupTrack.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Pickup Track:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Fuilfillment Plan Information";
            // 
            // dgvFulfillmentPlans
            // 
            this.dgvFulfillmentPlans.AllowUserToAddRows = false;
            this.dgvFulfillmentPlans.AllowUserToDeleteRows = false;
            this.dgvFulfillmentPlans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFulfillmentPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFulfillmentPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRailcar,
            this.colPOLines,
            this.colRoute});
            this.dgvFulfillmentPlans.Location = new System.Drawing.Point(0, 0);
            this.dgvFulfillmentPlans.Name = "dgvFulfillmentPlans";
            this.dgvFulfillmentPlans.ReadOnly = true;
            this.dgvFulfillmentPlans.RowHeadersVisible = false;
            this.dgvFulfillmentPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillmentPlans.Size = new System.Drawing.Size(778, 82);
            this.dgvFulfillmentPlans.TabIndex = 0;
            this.dgvFulfillmentPlans.SelectionChanged += new System.EventHandler(this.dgvFulfillmentPlans_SelectionChanged);
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
            this.colPOLines.HeaderText = "Purchase Order Lines";
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
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(12, 51);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbcDetails);
            this.splitContainer2.Size = new System.Drawing.Size(786, 592);
            this.splitContainer2.SplitterDistance = 198;
            this.splitContainer2.TabIndex = 1;
            // 
            // cmdWithdrawSubmission
            // 
            this.cmdWithdrawSubmission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWithdrawSubmission.BackColor = System.Drawing.Color.Red;
            this.cmdWithdrawSubmission.ForeColor = System.Drawing.Color.White;
            this.cmdWithdrawSubmission.Location = new System.Drawing.Point(671, 649);
            this.cmdWithdrawSubmission.Name = "cmdWithdrawSubmission";
            this.cmdWithdrawSubmission.Size = new System.Drawing.Size(127, 23);
            this.cmdWithdrawSubmission.TabIndex = 2;
            this.cmdWithdrawSubmission.Text = "Withdraw Submission";
            this.cmdWithdrawSubmission.UseVisualStyleBackColor = false;
            this.cmdWithdrawSubmission.Click += new System.EventHandler(this.cmdWithdrawSubmission_Click);
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(307, 289);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 7;
            this.loader.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSaveTemplate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(810, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolSaveTemplate
            // 
            this.toolSaveTemplate.Image = global::CompanyStudio.Properties.Resources.script_save;
            this.toolSaveTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSaveTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveTemplate.Name = "toolSaveTemplate";
            this.toolSaveTemplate.Size = new System.Drawing.Size(103, 22);
            this.toolSaveTemplate.Text = "Save Template";
            this.toolSaveTemplate.Click += new System.EventHandler(this.toolSaveTemplate_Click);
            // 
            // frmApprovalViewerSubmitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 679);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.cmdWithdrawSubmission);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmApprovalViewerSubmitter";
            this.Text = "Approval Tracker";
            this.Load += new System.EventHandler(this.frmApprovalViewerSubmitter_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).EndInit();
            this.tbcDetails.ResumeLayout(false);
            this.tabPOInfo.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabFulfillmentPlans.ResumeLayout(false);
            this.tabFulfillmentPlans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlApprovals;
        private System.Windows.Forms.DataGridView dgvLines;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TabControl tbcDetails;
        private System.Windows.Forms.TabPage tabPOInfo;
        private System.Windows.Forms.TabPage tabFulfillmentPlans;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button cmdWithdrawSubmission;
        private Loader loader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridView dgvFulfillmentPlans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPOLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoute;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPickupTrack;
        private System.Windows.Forms.TextBox txtStrategicAfterPickup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtStrategicAfterDropOff;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDropOffTrack;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRailcarRouting;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDestinationAfterFulfillment;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPurchaseOrderLines;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.LinkLabel lnkRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineTotal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHasFulfillmentPlan;
        private System.Windows.Forms.LinkLabel lnkTaxBreakdown;
        private System.Windows.Forms.TextBox txtGrossTotal;
        private System.Windows.Forms.TextBox txtEstTax;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSaveTemplate;
    }
}