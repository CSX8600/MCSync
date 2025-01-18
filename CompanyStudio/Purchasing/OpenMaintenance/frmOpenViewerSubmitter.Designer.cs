namespace CompanyStudio.Purchasing.OpenMaintenance
{
    partial class frmOpenViewerSubmitter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenViewerSubmitter));
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPOInfo = new System.Windows.Forms.TabPage();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlPOLines = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabFulfillmentPlans = new System.Windows.Forms.TabPage();
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
            this.tabFulfillments = new System.Windows.Forms.TabPage();
            this.dgvFulfillments = new System.Windows.Forms.DataGridView();
            this.colPurchaseOrderLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFulfillmentTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsComplete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabInvoices = new System.Windows.Forms.TabPage();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.colInvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loader = new CompanyStudio.Loader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolSaveTemplate = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPOInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabFulfillmentPlans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).BeginInit();
            this.tabFulfillments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillments)).BeginInit();
            this.tabInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open Purchase Order";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPOInfo);
            this.tabControl1.Controls.Add(this.tabFulfillmentPlans);
            this.tabControl1.Controls.Add(this.tabFulfillments);
            this.tabControl1.Controls.Add(this.tabInvoices);
            this.tabControl1.Location = new System.Drawing.Point(16, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(924, 501);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPOInfo
            // 
            this.tabPOInfo.Controls.Add(this.cmdClose);
            this.tabPOInfo.Controls.Add(this.groupBox1);
            this.tabPOInfo.Controls.Add(this.label5);
            this.tabPOInfo.Controls.Add(this.txtDescription);
            this.tabPOInfo.Controls.Add(this.label2);
            this.tabPOInfo.Controls.Add(this.txtDate);
            this.tabPOInfo.Controls.Add(this.txtFrom);
            this.tabPOInfo.Controls.Add(this.label4);
            this.tabPOInfo.Controls.Add(this.label3);
            this.tabPOInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPOInfo.Name = "tabPOInfo";
            this.tabPOInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPOInfo.Size = new System.Drawing.Size(916, 475);
            this.tabPOInfo.TabIndex = 0;
            this.tabPOInfo.Text = "Purchase Order Information";
            this.tabPOInfo.UseVisualStyleBackColor = true;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.BackColor = System.Drawing.Color.Red;
            this.cmdClose.ForeColor = System.Drawing.Color.Black;
            this.cmdClose.Location = new System.Drawing.Point(785, 154);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(125, 23);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close Purchase Order";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnlPOLines);
            this.groupBox1.Location = new System.Drawing.Point(6, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 286);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Purchase Order Lines";
            // 
            // pnlPOLines
            // 
            this.pnlPOLines.AutoScroll = true;
            this.pnlPOLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPOLines.Location = new System.Drawing.Point(3, 16);
            this.pnlPOLines.Name = "pnlPOLines";
            this.pnlPOLines.Size = new System.Drawing.Size(898, 267);
            this.pnlPOLines.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Purchase Order Information";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(8, 87);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(902, 61);
            this.txtDescription.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Order From:";
            // 
            // txtDate
            // 
            this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDate.Location = new System.Drawing.Point(73, 48);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(837, 20);
            this.txtDate.TabIndex = 1;
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.Location = new System.Drawing.Point(73, 22);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(837, 20);
            this.txtFrom.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Order Date:";
            // 
            // tabFulfillmentPlans
            // 
            this.tabFulfillmentPlans.Controls.Add(this.lnkFPRailcar);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPRailcarRouting);
            this.tabFulfillmentPlans.Controls.Add(this.label14);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPDestinationAfterFulfillment);
            this.tabFulfillmentPlans.Controls.Add(this.label13);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPStrategicAfterDropOff);
            this.tabFulfillmentPlans.Controls.Add(this.label12);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPStrategicAfterPickup);
            this.tabFulfillmentPlans.Controls.Add(this.label10);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPDropOffTrack);
            this.tabFulfillmentPlans.Controls.Add(this.label11);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPPurchaseOrderLines);
            this.tabFulfillmentPlans.Controls.Add(this.label16);
            this.tabFulfillmentPlans.Controls.Add(this.label15);
            this.tabFulfillmentPlans.Controls.Add(this.txtFPPickupTrack);
            this.tabFulfillmentPlans.Controls.Add(this.label9);
            this.tabFulfillmentPlans.Controls.Add(this.label8);
            this.tabFulfillmentPlans.Controls.Add(this.dgvFulfillmentPlans);
            this.tabFulfillmentPlans.Location = new System.Drawing.Point(4, 22);
            this.tabFulfillmentPlans.Name = "tabFulfillmentPlans";
            this.tabFulfillmentPlans.Size = new System.Drawing.Size(916, 475);
            this.tabFulfillmentPlans.TabIndex = 3;
            this.tabFulfillmentPlans.Text = "Fulfillment Plans";
            this.tabFulfillmentPlans.UseVisualStyleBackColor = true;
            // 
            // lnkFPRailcar
            // 
            this.lnkFPRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkFPRailcar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkFPRailcar.Location = new System.Drawing.Point(155, 104);
            this.lnkFPRailcar.Name = "lnkFPRailcar";
            this.lnkFPRailcar.Size = new System.Drawing.Size(755, 20);
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
            this.txtFPRailcarRouting.Size = new System.Drawing.Size(755, 20);
            this.txtFPRailcarRouting.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Railcar Routing:";
            // 
            // txtFPDestinationAfterFulfillment
            // 
            this.txtFPDestinationAfterFulfillment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPDestinationAfterFulfillment.Location = new System.Drawing.Point(155, 260);
            this.txtFPDestinationAfterFulfillment.Name = "txtFPDestinationAfterFulfillment";
            this.txtFPDestinationAfterFulfillment.ReadOnly = true;
            this.txtFPDestinationAfterFulfillment.Size = new System.Drawing.Size(755, 20);
            this.txtFPDestinationAfterFulfillment.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 263);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Destination after fulfillment:";
            // 
            // txtFPStrategicAfterDropOff
            // 
            this.txtFPStrategicAfterDropOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPStrategicAfterDropOff.Location = new System.Drawing.Point(155, 234);
            this.txtFPStrategicAfterDropOff.Name = "txtFPStrategicAfterDropOff";
            this.txtFPStrategicAfterDropOff.ReadOnly = true;
            this.txtFPStrategicAfterDropOff.Size = new System.Drawing.Size(755, 20);
            this.txtFPStrategicAfterDropOff.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Strategic Track after drop off:";
            // 
            // txtFPStrategicAfterPickup
            // 
            this.txtFPStrategicAfterPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPStrategicAfterPickup.Location = new System.Drawing.Point(155, 182);
            this.txtFPStrategicAfterPickup.Name = "txtFPStrategicAfterPickup";
            this.txtFPStrategicAfterPickup.ReadOnly = true;
            this.txtFPStrategicAfterPickup.Size = new System.Drawing.Size(755, 20);
            this.txtFPStrategicAfterPickup.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Strategic Track after pickup:";
            // 
            // txtFPDropOffTrack
            // 
            this.txtFPDropOffTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPDropOffTrack.Location = new System.Drawing.Point(155, 208);
            this.txtFPDropOffTrack.Name = "txtFPDropOffTrack";
            this.txtFPDropOffTrack.ReadOnly = true;
            this.txtFPDropOffTrack.Size = new System.Drawing.Size(755, 20);
            this.txtFPDropOffTrack.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Drop Off Track:";
            // 
            // txtFPPurchaseOrderLines
            // 
            this.txtFPPurchaseOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPPurchaseOrderLines.Location = new System.Drawing.Point(155, 130);
            this.txtFPPurchaseOrderLines.Name = "txtFPPurchaseOrderLines";
            this.txtFPPurchaseOrderLines.ReadOnly = true;
            this.txtFPPurchaseOrderLines.Size = new System.Drawing.Size(755, 20);
            this.txtFPPurchaseOrderLines.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Purchase Order Lines:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Railcar:";
            // 
            // txtFPPickupTrack
            // 
            this.txtFPPickupTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFPPickupTrack.Location = new System.Drawing.Point(155, 156);
            this.txtFPPickupTrack.Name = "txtFPPickupTrack";
            this.txtFPPickupTrack.ReadOnly = true;
            this.txtFPPickupTrack.Size = new System.Drawing.Size(755, 20);
            this.txtFPPickupTrack.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Pickup Track:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 16);
            this.label8.TabIndex = 6;
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
            this.dgvFulfillmentPlans.Size = new System.Drawing.Size(916, 82);
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
            // tabFulfillments
            // 
            this.tabFulfillments.Controls.Add(this.dgvFulfillments);
            this.tabFulfillments.Location = new System.Drawing.Point(4, 22);
            this.tabFulfillments.Name = "tabFulfillments";
            this.tabFulfillments.Padding = new System.Windows.Forms.Padding(3);
            this.tabFulfillments.Size = new System.Drawing.Size(916, 475);
            this.tabFulfillments.TabIndex = 1;
            this.tabFulfillments.Text = "Fulfillments";
            this.tabFulfillments.UseVisualStyleBackColor = true;
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
            this.dgvFulfillments.Location = new System.Drawing.Point(3, 3);
            this.dgvFulfillments.Name = "dgvFulfillments";
            this.dgvFulfillments.ReadOnly = true;
            this.dgvFulfillments.RowHeadersVisible = false;
            this.dgvFulfillments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillments.Size = new System.Drawing.Size(910, 469);
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
            // tabInvoices
            // 
            this.tabInvoices.Controls.Add(this.dgvInvoices);
            this.tabInvoices.Location = new System.Drawing.Point(4, 22);
            this.tabInvoices.Name = "tabInvoices";
            this.tabInvoices.Size = new System.Drawing.Size(916, 475);
            this.tabInvoices.TabIndex = 2;
            this.tabInvoices.Text = "Invoices";
            this.tabInvoices.UseVisualStyleBackColor = true;
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
            this.dgvInvoices.Size = new System.Drawing.Size(916, 475);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellDoubleClick);
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
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(378, 231);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 2;
            this.loader.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSaveTemplate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(952, 25);
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
            // frmOpenViewerSubmitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 562);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOpenViewerSubmitter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Purchase Order";
            this.Load += new System.EventHandler(this.frmOpenViewerSubmitter_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPOInfo.ResumeLayout(false);
            this.tabPOInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabFulfillmentPlans.ResumeLayout(false);
            this.tabFulfillmentPlans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).EndInit();
            this.tabFulfillments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillments)).EndInit();
            this.tabInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPOInfo;
        private System.Windows.Forms.TabPage tabFulfillments;
        private System.Windows.Forms.TabPage tabFulfillmentPlans;
        private System.Windows.Forms.TabPage tabInvoices;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlPOLines;
        private Loader loader;
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
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSaveTemplate;
    }
}