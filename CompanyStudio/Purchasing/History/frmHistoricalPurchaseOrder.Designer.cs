namespace CompanyStudio.Purchasing.History
{
    partial class frmHistoricalPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistoricalPurchaseOrder));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInvoiceSchedule = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSeller = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPurchaser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlLines = new System.Windows.Forms.Panel();
            this.tabFulfillmentPlan = new System.Windows.Forms.TabPage();
            this.lnkFPRailcar = new System.Windows.Forms.LinkLabel();
            this.txtFPRailcarRouting = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFPDestinationAfterFulfillment = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtFPStrategicAfterDropOff = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFPStrategicAfterPickup = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFPDropOffTrack = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFPPurchaseOrderLines = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFPPickupTrack = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvFulfillmentPlans = new System.Windows.Forms.DataGridView();
            this.colFPRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFPPOLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFPRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFulfillment = new System.Windows.Forms.TabPage();
            this.dgvFulfillments = new System.Windows.Forms.DataGridView();
            this.colPurchaseOrderLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFulfillmentTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsComplete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabInvoice = new System.Windows.Forms.TabPage();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.colInvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolSaveTemplate = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoApproving = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbEnableAutoApproval = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDisableAutoApproval = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabFulfillmentPlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).BeginInit();
            this.tabFulfillment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillments)).BeginInit();
            this.tabInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtInvoiceSchedule);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtOrderDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSeller);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPurchaser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Purchase Order Information";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(75, 97);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(675, 43);
            this.txtDescription.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description:";
            // 
            // txtInvoiceSchedule
            // 
            this.txtInvoiceSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInvoiceSchedule.Location = new System.Drawing.Point(75, 146);
            this.txtInvoiceSchedule.Name = "txtInvoiceSchedule";
            this.txtInvoiceSchedule.ReadOnly = true;
            this.txtInvoiceSchedule.Size = new System.Drawing.Size(675, 20);
            this.txtInvoiceSchedule.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Invoice:";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderDate.Location = new System.Drawing.Point(75, 71);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.ReadOnly = true;
            this.txtOrderDate.Size = new System.Drawing.Size(675, 20);
            this.txtOrderDate.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Order Date:";
            // 
            // txtSeller
            // 
            this.txtSeller.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeller.Location = new System.Drawing.Point(75, 45);
            this.txtSeller.Name = "txtSeller";
            this.txtSeller.ReadOnly = true;
            this.txtSeller.Size = new System.Drawing.Size(675, 20);
            this.txtSeller.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Seller:";
            // 
            // txtPurchaser
            // 
            this.txtPurchaser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurchaser.Location = new System.Drawing.Point(75, 19);
            this.txtPurchaser.Name = "txtPurchaser";
            this.txtPurchaser.ReadOnly = true;
            this.txtPurchaser.Size = new System.Drawing.Size(675, 20);
            this.txtPurchaser.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Purchaser:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(192, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Complete Purchase Order";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabFulfillmentPlan);
            this.tabControl1.Controls.Add(this.tabFulfillment);
            this.tabControl1.Controls.Add(this.tabInvoice);
            this.tabControl1.Location = new System.Drawing.Point(12, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 406);
            this.tabControl1.TabIndex = 1;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.groupBox2);
            this.tabInfo.Controls.Add(this.groupBox1);
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(768, 380);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "Purchase Order";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnlLines);
            this.groupBox2.Location = new System.Drawing.Point(6, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 186);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Purchase Order Lines";
            // 
            // pnlLines
            // 
            this.pnlLines.AutoScroll = true;
            this.pnlLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLines.Location = new System.Drawing.Point(3, 16);
            this.pnlLines.Name = "pnlLines";
            this.pnlLines.Size = new System.Drawing.Size(750, 167);
            this.pnlLines.TabIndex = 0;
            // 
            // tabFulfillmentPlan
            // 
            this.tabFulfillmentPlan.Controls.Add(this.lnkFPRailcar);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPRailcarRouting);
            this.tabFulfillmentPlan.Controls.Add(this.label14);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPDestinationAfterFulfillment);
            this.tabFulfillmentPlan.Controls.Add(this.label13);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPStrategicAfterDropOff);
            this.tabFulfillmentPlan.Controls.Add(this.label12);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPStrategicAfterPickup);
            this.tabFulfillmentPlan.Controls.Add(this.label10);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPDropOffTrack);
            this.tabFulfillmentPlan.Controls.Add(this.label11);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPPurchaseOrderLines);
            this.tabFulfillmentPlan.Controls.Add(this.label16);
            this.tabFulfillmentPlan.Controls.Add(this.label15);
            this.tabFulfillmentPlan.Controls.Add(this.txtFPPickupTrack);
            this.tabFulfillmentPlan.Controls.Add(this.label9);
            this.tabFulfillmentPlan.Controls.Add(this.label8);
            this.tabFulfillmentPlan.Controls.Add(this.dgvFulfillmentPlans);
            this.tabFulfillmentPlan.Location = new System.Drawing.Point(4, 22);
            this.tabFulfillmentPlan.Name = "tabFulfillmentPlan";
            this.tabFulfillmentPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabFulfillmentPlan.Size = new System.Drawing.Size(768, 380);
            this.tabFulfillmentPlan.TabIndex = 1;
            this.tabFulfillmentPlan.Text = "Fulfillment Plans";
            this.tabFulfillmentPlan.UseVisualStyleBackColor = true;
            // 
            // lnkFPRailcar
            // 
            this.lnkFPRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkFPRailcar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkFPRailcar.Location = new System.Drawing.Point(155, 104);
            this.lnkFPRailcar.Name = "lnkFPRailcar";
            this.lnkFPRailcar.Size = new System.Drawing.Size(607, 20);
            this.lnkFPRailcar.TabIndex = 1;
            this.lnkFPRailcar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkFPRailcar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFPRailcar_LinkClicked);
            // 
            // txtFPRailcarRouting
            // 
            this.txtFPRailcarRouting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPRailcarRouting.Location = new System.Drawing.Point(155, 286);
            this.txtFPRailcarRouting.Name = "txtFPRailcarRouting";
            this.txtFPRailcarRouting.ReadOnly = true;
            this.txtFPRailcarRouting.Size = new System.Drawing.Size(607, 20);
            this.txtFPRailcarRouting.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "Railcar Routing:";
            // 
            // txtFPDestinationAfterFulfillment
            // 
            this.txtFPDestinationAfterFulfillment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPDestinationAfterFulfillment.Location = new System.Drawing.Point(155, 260);
            this.txtFPDestinationAfterFulfillment.Name = "txtFPDestinationAfterFulfillment";
            this.txtFPDestinationAfterFulfillment.ReadOnly = true;
            this.txtFPDestinationAfterFulfillment.Size = new System.Drawing.Size(607, 20);
            this.txtFPDestinationAfterFulfillment.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 263);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Destination after fulfillment:";
            // 
            // txtFPStrategicAfterDropOff
            // 
            this.txtFPStrategicAfterDropOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPStrategicAfterDropOff.Location = new System.Drawing.Point(155, 234);
            this.txtFPStrategicAfterDropOff.Name = "txtFPStrategicAfterDropOff";
            this.txtFPStrategicAfterDropOff.ReadOnly = true;
            this.txtFPStrategicAfterDropOff.Size = new System.Drawing.Size(607, 20);
            this.txtFPStrategicAfterDropOff.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Strategic Track after drop off:";
            // 
            // txtFPStrategicAfterPickup
            // 
            this.txtFPStrategicAfterPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPStrategicAfterPickup.Location = new System.Drawing.Point(155, 182);
            this.txtFPStrategicAfterPickup.Name = "txtFPStrategicAfterPickup";
            this.txtFPStrategicAfterPickup.ReadOnly = true;
            this.txtFPStrategicAfterPickup.Size = new System.Drawing.Size(607, 20);
            this.txtFPStrategicAfterPickup.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Strategic Track after pickup:";
            // 
            // txtFPDropOffTrack
            // 
            this.txtFPDropOffTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPDropOffTrack.Location = new System.Drawing.Point(155, 208);
            this.txtFPDropOffTrack.Name = "txtFPDropOffTrack";
            this.txtFPDropOffTrack.ReadOnly = true;
            this.txtFPDropOffTrack.Size = new System.Drawing.Size(607, 20);
            this.txtFPDropOffTrack.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Drop Off Track:";
            // 
            // txtFPPurchaseOrderLines
            // 
            this.txtFPPurchaseOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPPurchaseOrderLines.Location = new System.Drawing.Point(155, 130);
            this.txtFPPurchaseOrderLines.Name = "txtFPPurchaseOrderLines";
            this.txtFPPurchaseOrderLines.ReadOnly = true;
            this.txtFPPurchaseOrderLines.Size = new System.Drawing.Size(601, 20);
            this.txtFPPurchaseOrderLines.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "Purchase Order Lines:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Railcar:";
            // 
            // txtFPPickupTrack
            // 
            this.txtFPPickupTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPPickupTrack.Location = new System.Drawing.Point(155, 156);
            this.txtFPPickupTrack.Name = "txtFPPickupTrack";
            this.txtFPPickupTrack.ReadOnly = true;
            this.txtFPPickupTrack.Size = new System.Drawing.Size(607, 20);
            this.txtFPPickupTrack.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Pickup Track:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 16);
            this.label8.TabIndex = 24;
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
            this.colFPRailcar,
            this.colFPPOLines,
            this.colFPRoute});
            this.dgvFulfillmentPlans.Location = new System.Drawing.Point(0, 0);
            this.dgvFulfillmentPlans.Name = "dgvFulfillmentPlans";
            this.dgvFulfillmentPlans.ReadOnly = true;
            this.dgvFulfillmentPlans.RowHeadersVisible = false;
            this.dgvFulfillmentPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillmentPlans.Size = new System.Drawing.Size(768, 82);
            this.dgvFulfillmentPlans.TabIndex = 0;
            this.dgvFulfillmentPlans.SelectionChanged += new System.EventHandler(this.dgvFulfillmentPlans_SelectionChanged);
            // 
            // colFPRailcar
            // 
            this.colFPRailcar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFPRailcar.HeaderText = "Railcar";
            this.colFPRailcar.Name = "colFPRailcar";
            this.colFPRailcar.ReadOnly = true;
            // 
            // colFPPOLines
            // 
            this.colFPPOLines.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFPPOLines.HeaderText = "Purchase Order Lines";
            this.colFPPOLines.Name = "colFPPOLines";
            this.colFPPOLines.ReadOnly = true;
            // 
            // colFPRoute
            // 
            this.colFPRoute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFPRoute.HeaderText = "Route";
            this.colFPRoute.Name = "colFPRoute";
            this.colFPRoute.ReadOnly = true;
            // 
            // tabFulfillment
            // 
            this.tabFulfillment.Controls.Add(this.dgvFulfillments);
            this.tabFulfillment.Location = new System.Drawing.Point(4, 22);
            this.tabFulfillment.Name = "tabFulfillment";
            this.tabFulfillment.Size = new System.Drawing.Size(768, 380);
            this.tabFulfillment.TabIndex = 2;
            this.tabFulfillment.Text = "Fulfillments";
            this.tabFulfillment.UseVisualStyleBackColor = true;
            // 
            // dgvFulfillments
            // 
            this.dgvFulfillments.AllowUserToAddRows = false;
            this.dgvFulfillments.AllowUserToDeleteRows = false;
            this.dgvFulfillments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFulfillments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPurchaseOrderLine,
            this.colRailcar,
            this.colQuantity,
            this.colFulfillmentTime,
            this.colIsComplete});
            this.dgvFulfillments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFulfillments.Location = new System.Drawing.Point(0, 0);
            this.dgvFulfillments.Name = "dgvFulfillments";
            this.dgvFulfillments.ReadOnly = true;
            this.dgvFulfillments.RowHeadersVisible = false;
            this.dgvFulfillments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillments.Size = new System.Drawing.Size(768, 380);
            this.dgvFulfillments.TabIndex = 0;
            // 
            // colPurchaseOrderLine
            // 
            this.colPurchaseOrderLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPurchaseOrderLine.HeaderText = "Purchase Order Line";
            this.colPurchaseOrderLine.Name = "colPurchaseOrderLine";
            this.colPurchaseOrderLine.ReadOnly = true;
            // 
            // colRailcar
            // 
            this.colRailcar.HeaderText = "Railcar";
            this.colRailcar.Name = "colRailcar";
            this.colRailcar.ReadOnly = true;
            this.colRailcar.Width = 150;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colFulfillmentTime
            // 
            this.colFulfillmentTime.HeaderText = "Ship Time";
            this.colFulfillmentTime.Name = "colFulfillmentTime";
            this.colFulfillmentTime.ReadOnly = true;
            this.colFulfillmentTime.Width = 150;
            // 
            // colIsComplete
            // 
            this.colIsComplete.HeaderText = "Arrived";
            this.colIsComplete.Name = "colIsComplete";
            this.colIsComplete.ReadOnly = true;
            this.colIsComplete.Width = 75;
            // 
            // tabInvoice
            // 
            this.tabInvoice.Controls.Add(this.dgvInvoices);
            this.tabInvoice.Location = new System.Drawing.Point(4, 22);
            this.tabInvoice.Name = "tabInvoice";
            this.tabInvoice.Size = new System.Drawing.Size(768, 380);
            this.tabInvoice.TabIndex = 3;
            this.tabInvoice.Text = "Invoices";
            this.tabInvoice.UseVisualStyleBackColor = true;
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
            this.colStatus,
            this.colInvoiceAmount});
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.Location = new System.Drawing.Point(0, 0);
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersVisible = false;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(768, 380);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInvoices_CellMouseDoubleClick);
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.HeaderText = "Invoice Number";
            this.colInvoiceNumber.Name = "colInvoiceNumber";
            this.colInvoiceNumber.ReadOnly = true;
            this.colInvoiceNumber.Width = 150;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.HeaderText = "Invoice Date";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.ReadOnly = true;
            this.colInvoiceDate.Width = 150;
            // 
            // colDueDate
            // 
            this.colDueDate.HeaderText = "Due Date";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.ReadOnly = true;
            this.colDueDate.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 150;
            // 
            // colInvoiceAmount
            // 
            this.colInvoiceAmount.HeaderText = "Amount";
            this.colInvoiceAmount.Name = "colInvoiceAmount";
            this.colInvoiceAmount.ReadOnly = true;
            this.colInvoiceAmount.Width = 150;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSaveTemplate,
            this.tsbAutoApproving});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
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
            this.toolSaveTemplate.Visible = false;
            this.toolSaveTemplate.Click += new System.EventHandler(this.toolSaveTemplate_Click);
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
            this.tsbAutoApproving.Visible = false;
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
            // frmHistoricalPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 466);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHistoricalPurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Complete Purchase Order";
            this.Load += new System.EventHandler(this.frmHistoricalPurchaseOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabFulfillmentPlan.ResumeLayout(false);
            this.tabFulfillmentPlan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).EndInit();
            this.tabFulfillment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillments)).EndInit();
            this.tabInvoice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSeller;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPurchaser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInvoiceSchedule;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabFulfillmentPlan;
        private System.Windows.Forms.TabPage tabFulfillment;
        private System.Windows.Forms.TabPage tabInvoice;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlLines;
        private System.Windows.Forms.LinkLabel lnkFPRailcar;
        private System.Windows.Forms.TextBox txtFPRailcarRouting;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtFPDestinationAfterFulfillment;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtFPStrategicAfterDropOff;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFPStrategicAfterPickup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFPDropOffTrack;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFPPurchaseOrderLines;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFPPickupTrack;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvFulfillmentPlans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFPRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFPPOLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFPRoute;
        private System.Windows.Forms.DataGridView dgvFulfillments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPurchaseOrderLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFulfillmentTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsComplete;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceAmount;
        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSaveTemplate;
        private System.Windows.Forms.ToolStripDropDownButton tsbAutoApproving;
        private System.Windows.Forms.ToolStripMenuItem tsbEnableAutoApproval;
        private System.Windows.Forms.ToolStripMenuItem tsbDisableAutoApproval;
    }
}