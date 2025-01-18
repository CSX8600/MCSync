namespace CompanyStudio.Purchasing.ApprovalViewer
{
    partial class frmApprovalViewerApprover
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApprovalViewerApprover));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtApprovalReason = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSellerSetup = new System.Windows.Forms.TabPage();
            this.cboReceivingAccount = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cboInvoiceSchedule = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPOInfo = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvLines = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHasFulfillmentPlan = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.label17 = new System.Windows.Forms.Label();
            this.dgvFulfillmentPlans = new System.Windows.Forms.DataGridView();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPOLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdApprove = new System.Windows.Forms.Button();
            this.cmdDeny = new System.Windows.Forms.Button();
            this.loader = new CompanyStudio.Loader();
            this.chkFutureAutoApprove = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabSellerSetup.SuspendLayout();
            this.tabPOInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).BeginInit();
            this.tabFulfillmentPlans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Purchase Order Requires Your Approval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Approval Reason:";
            // 
            // txtApprovalReason
            // 
            this.txtApprovalReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApprovalReason.Location = new System.Drawing.Point(110, 32);
            this.txtApprovalReason.Name = "txtApprovalReason";
            this.txtApprovalReason.ReadOnly = true;
            this.txtApprovalReason.Size = new System.Drawing.Size(554, 20);
            this.txtApprovalReason.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSellerSetup);
            this.tabControl1.Controls.Add(this.tabPOInfo);
            this.tabControl1.Controls.Add(this.tabFulfillmentPlans);
            this.tabControl1.Location = new System.Drawing.Point(12, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(648, 351);
            this.tabControl1.TabIndex = 1;
            // 
            // tabSellerSetup
            // 
            this.tabSellerSetup.Controls.Add(this.cboReceivingAccount);
            this.tabSellerSetup.Controls.Add(this.label21);
            this.tabSellerSetup.Controls.Add(this.cboInvoiceSchedule);
            this.tabSellerSetup.Controls.Add(this.label20);
            this.tabSellerSetup.Controls.Add(this.label19);
            this.tabSellerSetup.Controls.Add(this.label18);
            this.tabSellerSetup.Location = new System.Drawing.Point(4, 22);
            this.tabSellerSetup.Name = "tabSellerSetup";
            this.tabSellerSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabSellerSetup.Size = new System.Drawing.Size(640, 325);
            this.tabSellerSetup.TabIndex = 2;
            this.tabSellerSetup.Text = "Seller Setup";
            this.tabSellerSetup.UseVisualStyleBackColor = true;
            // 
            // cboReceivingAccount
            // 
            this.cboReceivingAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReceivingAccount.FormattingEnabled = true;
            this.cboReceivingAccount.Location = new System.Drawing.Point(269, 62);
            this.cboReceivingAccount.Name = "cboReceivingAccount";
            this.cboReceivingAccount.Size = new System.Drawing.Size(365, 21);
            this.cboReceivingAccount.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(252, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "Use this Account to receive on automated Invoices:";
            // 
            // cboInvoiceSchedule
            // 
            this.cboInvoiceSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboInvoiceSchedule.FormattingEnabled = true;
            this.cboInvoiceSchedule.Location = new System.Drawing.Point(269, 35);
            this.cboInvoiceSchedule.Name = "cboInvoiceSchedule";
            this.cboInvoiceSchedule.Size = new System.Drawing.Size(365, 21);
            this.cboInvoiceSchedule.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 38);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(260, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Automatically create Invoices based on this schedule:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(290, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "The following settings will be applied after you click Approve";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "Seller Setup";
            // 
            // tabPOInfo
            // 
            this.tabPOInfo.Controls.Add(this.splitContainer1);
            this.tabPOInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPOInfo.Name = "tabPOInfo";
            this.tabPOInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPOInfo.Size = new System.Drawing.Size(640, 325);
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
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtDate);
            this.splitContainer1.Panel1.Controls.Add(this.txtFrom);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtTotal);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.dgvLines);
            this.splitContainer1.Size = new System.Drawing.Size(634, 319);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 1;
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
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(6, 84);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(625, 61);
            this.txtDescription.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Order From:";
            // 
            // txtDate
            // 
            this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDate.Location = new System.Drawing.Point(71, 45);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(560, 20);
            this.txtDate.TabIndex = 1;
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.Location = new System.Drawing.Point(71, 19);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(560, 20);
            this.txtFrom.TabIndex = 0;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Order Date:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Purchase Order Total:";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotal.Location = new System.Drawing.Point(120, 132);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(81, 20);
            this.txtTotal.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Purchase Order Lines";
            // 
            // dgvLines
            // 
            this.dgvLines.AllowUserToAddRows = false;
            this.dgvLines.AllowUserToDeleteRows = false;
            this.dgvLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDescription,
            this.colQuantity,
            this.colUnitCost,
            this.colLineTotal,
            this.colHasFulfillmentPlan});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLines.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLines.Location = new System.Drawing.Point(0, 19);
            this.dgvLines.Name = "dgvLines";
            this.dgvLines.ReadOnly = true;
            this.dgvLines.RowHeadersVisible = false;
            this.dgvLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLines.Size = new System.Drawing.Size(634, 107);
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
            // tabFulfillmentPlans
            // 
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
            this.tabFulfillmentPlans.Controls.Add(this.label17);
            this.tabFulfillmentPlans.Controls.Add(this.dgvFulfillmentPlans);
            this.tabFulfillmentPlans.Location = new System.Drawing.Point(4, 22);
            this.tabFulfillmentPlans.Name = "tabFulfillmentPlans";
            this.tabFulfillmentPlans.Padding = new System.Windows.Forms.Padding(3);
            this.tabFulfillmentPlans.Size = new System.Drawing.Size(640, 325);
            this.tabFulfillmentPlans.TabIndex = 1;
            this.tabFulfillmentPlans.Text = "Fulfillment Plans";
            this.tabFulfillmentPlans.UseVisualStyleBackColor = true;
            // 
            // lnkRailcar
            // 
            this.lnkRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkRailcar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkRailcar.Location = new System.Drawing.Point(154, 104);
            this.lnkRailcar.Name = "lnkRailcar";
            this.lnkRailcar.Size = new System.Drawing.Size(480, 20);
            this.lnkRailcar.TabIndex = 1;
            this.lnkRailcar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkRailcar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRailcar_LinkClicked);
            // 
            // txtRailcarRouting
            // 
            this.txtRailcarRouting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRailcarRouting.Location = new System.Drawing.Point(154, 286);
            this.txtRailcarRouting.Name = "txtRailcarRouting";
            this.txtRailcarRouting.ReadOnly = true;
            this.txtRailcarRouting.Size = new System.Drawing.Size(480, 20);
            this.txtRailcarRouting.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Railcar Routing:";
            // 
            // txtDestinationAfterFulfillment
            // 
            this.txtDestinationAfterFulfillment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestinationAfterFulfillment.Location = new System.Drawing.Point(154, 260);
            this.txtDestinationAfterFulfillment.Name = "txtDestinationAfterFulfillment";
            this.txtDestinationAfterFulfillment.ReadOnly = true;
            this.txtDestinationAfterFulfillment.Size = new System.Drawing.Size(480, 20);
            this.txtDestinationAfterFulfillment.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 263);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Destination after fulfillment:";
            // 
            // txtStrategicAfterDropOff
            // 
            this.txtStrategicAfterDropOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStrategicAfterDropOff.Location = new System.Drawing.Point(154, 234);
            this.txtStrategicAfterDropOff.Name = "txtStrategicAfterDropOff";
            this.txtStrategicAfterDropOff.ReadOnly = true;
            this.txtStrategicAfterDropOff.Size = new System.Drawing.Size(480, 20);
            this.txtStrategicAfterDropOff.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Strategic Track after drop off:";
            // 
            // txtStrategicAfterPickup
            // 
            this.txtStrategicAfterPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStrategicAfterPickup.Location = new System.Drawing.Point(154, 182);
            this.txtStrategicAfterPickup.Name = "txtStrategicAfterPickup";
            this.txtStrategicAfterPickup.ReadOnly = true;
            this.txtStrategicAfterPickup.Size = new System.Drawing.Size(480, 20);
            this.txtStrategicAfterPickup.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Strategic Track after pickup:";
            // 
            // txtDropOffTrack
            // 
            this.txtDropOffTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDropOffTrack.Location = new System.Drawing.Point(154, 208);
            this.txtDropOffTrack.Name = "txtDropOffTrack";
            this.txtDropOffTrack.ReadOnly = true;
            this.txtDropOffTrack.Size = new System.Drawing.Size(480, 20);
            this.txtDropOffTrack.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Drop Off Track:";
            // 
            // txtPurchaseOrderLines
            // 
            this.txtPurchaseOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurchaseOrderLines.Location = new System.Drawing.Point(154, 130);
            this.txtPurchaseOrderLines.Name = "txtPurchaseOrderLines";
            this.txtPurchaseOrderLines.ReadOnly = true;
            this.txtPurchaseOrderLines.Size = new System.Drawing.Size(480, 20);
            this.txtPurchaseOrderLines.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(2, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Purchase Order Lines:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(2, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Railcar:";
            // 
            // txtPickupTrack
            // 
            this.txtPickupTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPickupTrack.Location = new System.Drawing.Point(154, 156);
            this.txtPickupTrack.Name = "txtPickupTrack";
            this.txtPickupTrack.ReadOnly = true;
            this.txtPickupTrack.Size = new System.Drawing.Size(480, 20);
            this.txtPickupTrack.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Pickup Track:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(2, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(167, 16);
            this.label17.TabIndex = 6;
            this.label17.Text = "Fuilfillment Plan Information";
            // 
            // dgvFulfillmentPlans
            // 
            this.dgvFulfillmentPlans.AllowUserToAddRows = false;
            this.dgvFulfillmentPlans.AllowUserToDeleteRows = false;
            this.dgvFulfillmentPlans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFulfillmentPlans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFulfillmentPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFulfillmentPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRailcar,
            this.colPOLines,
            this.colRoute});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFulfillmentPlans.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFulfillmentPlans.Location = new System.Drawing.Point(-1, 0);
            this.dgvFulfillmentPlans.Name = "dgvFulfillmentPlans";
            this.dgvFulfillmentPlans.ReadOnly = true;
            this.dgvFulfillmentPlans.RowHeadersVisible = false;
            this.dgvFulfillmentPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillmentPlans.Size = new System.Drawing.Size(638, 82);
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
            // cmdApprove
            // 
            this.cmdApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApprove.Location = new System.Drawing.Point(589, 415);
            this.cmdApprove.Name = "cmdApprove";
            this.cmdApprove.Size = new System.Drawing.Size(75, 23);
            this.cmdApprove.TabIndex = 2;
            this.cmdApprove.Text = "Approve";
            this.cmdApprove.UseVisualStyleBackColor = true;
            this.cmdApprove.Click += new System.EventHandler(this.cmdApprove_Click);
            // 
            // cmdDeny
            // 
            this.cmdDeny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeny.Location = new System.Drawing.Point(508, 415);
            this.cmdDeny.Name = "cmdDeny";
            this.cmdDeny.Size = new System.Drawing.Size(75, 23);
            this.cmdDeny.TabIndex = 3;
            this.cmdDeny.Text = "Reject";
            this.cmdDeny.UseVisualStyleBackColor = true;
            this.cmdDeny.Click += new System.EventHandler(this.cmdDeny_Click);
            // 
            // loader
            // 
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(243, 192);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 4;
            this.loader.Visible = false;
            // 
            // chkFutureAutoApprove
            // 
            this.chkFutureAutoApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFutureAutoApprove.AutoSize = true;
            this.chkFutureAutoApprove.Checked = true;
            this.chkFutureAutoApprove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFutureAutoApprove.Location = new System.Drawing.Point(119, 419);
            this.chkFutureAutoApprove.Name = "chkFutureAutoApprove";
            this.chkFutureAutoApprove.Size = new System.Drawing.Size(383, 17);
            this.chkFutureAutoApprove.TabIndex = 4;
            this.chkFutureAutoApprove.Text = "Auto-approve for future Purchase Orders created from a template of this one";
            this.chkFutureAutoApprove.UseVisualStyleBackColor = true;
            // 
            // frmApprovalViewerApprover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 450);
            this.Controls.Add(this.chkFutureAutoApprove);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.cmdDeny);
            this.Controls.Add(this.cmdApprove);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtApprovalReason);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmApprovalViewerApprover";
            this.Text = "Pending Purchase Order";
            this.Load += new System.EventHandler(this.frmApprovalViewerApprover_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSellerSetup.ResumeLayout(false);
            this.tabSellerSetup.PerformLayout();
            this.tabPOInfo.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).EndInit();
            this.tabFulfillmentPlans.ResumeLayout(false);
            this.tabFulfillmentPlans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtApprovalReason;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPOInfo;
        private System.Windows.Forms.TabPage tabFulfillmentPlans;
        private System.Windows.Forms.Button cmdApprove;
        private System.Windows.Forms.Button cmdDeny;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineTotal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHasFulfillmentPlan;
        private System.Windows.Forms.LinkLabel lnkRailcar;
        private System.Windows.Forms.TextBox txtRailcarRouting;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDestinationAfterFulfillment;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtStrategicAfterDropOff;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtStrategicAfterPickup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDropOffTrack;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPurchaseOrderLines;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPickupTrack;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dgvFulfillmentPlans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPOLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoute;
        private Loader loader;
        private System.Windows.Forms.TabPage tabSellerSetup;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cboInvoiceSchedule;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox chkFutureAutoApprove;
        private System.Windows.Forms.ComboBox cboReceivingAccount;
        private System.Windows.Forms.Label label21;
    }
}