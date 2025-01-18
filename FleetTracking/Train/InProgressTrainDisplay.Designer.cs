namespace FleetTracking.Train
{
    partial class InProgressTrainDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkSymbol = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblOnDutySince = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdEndTrain = new System.Windows.Forms.Button();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.loaderTrainInfo = new FleetTracking.Loader();
            this.lblTotalLength = new System.Windows.Forms.Label();
            this.lblStockTotal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabConsist = new System.Windows.Forms.TabPage();
            this.dgvConsist = new System.Windows.Forms.DataGridView();
            this.colConsistImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStrategic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConsistType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolModifyConsist = new System.Windows.Forms.ToolStripButton();
            this.toolLiveLoad = new System.Windows.Forms.ToolStripButton();
            this.toolSpotAtStrategic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPrintManifest = new System.Windows.Forms.ToolStripButton();
            this.loaderConsist = new FleetTracking.Loader();
            this.tabDutyTrans = new System.Windows.Forms.TabPage();
            this.dgvDutyTrans = new System.Windows.Forms.DataGridView();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAddTransaction = new System.Windows.Forms.ToolStripButton();
            this.loaderDutyTrans = new FleetTracking.Loader();
            this.tabLocoFuel = new System.Windows.Forms.TabPage();
            this.dgvLocoFuel = new System.Windows.Forms.DataGridView();
            this.colFuelImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colFuelReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartingFuel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndingFuel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolFuelAddLoco = new System.Windows.Forms.ToolStripButton();
            this.toolFuelSetStart = new System.Windows.Forms.ToolStripButton();
            this.toolFuelSetEnd = new System.Windows.Forms.ToolStripButton();
            this.loaderLocoFuel = new FleetTracking.Loader();
            this.tabHandledCars = new System.Windows.Forms.TabPage();
            this.dgvHandledCars = new System.Windows.Forms.DataGridView();
            this.colHandledImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colHandledReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPickedUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSetOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartialTrip = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolTogglePartialTrip = new System.Windows.Forms.ToolStripButton();
            this.loaderHandledCars = new FleetTracking.Loader();
            this.dataGridViewStylizer = new FleetTracking.DataGridViewStylizer(this.components);
            this.tsbPrintBOLs = new System.Windows.Forms.ToolStripButton();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabConsist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsist)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.tabDutyTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDutyTrans)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabLocoFuel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocoFuel)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.tabHandledCars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandledCars)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Symbol:";
            // 
            // lnkSymbol
            // 
            this.lnkSymbol.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkSymbol.AutoSize = true;
            this.lnkSymbol.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSymbol.Location = new System.Drawing.Point(60, 2);
            this.lnkSymbol.Name = "lnkSymbol";
            this.lnkSymbol.Size = new System.Drawing.Size(57, 13);
            this.lnkSymbol.TabIndex = 1;
            this.lnkSymbol.TabStop = true;
            this.lnkSymbol.Text = "IRW Q100";
            this.lnkSymbol.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSymbol_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Train Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(89, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(169, 23);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Not Started";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "On Duty Since:";
            // 
            // lblOnDutySince
            // 
            this.lblOnDutySince.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOnDutySince.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnDutySince.Location = new System.Drawing.Point(93, 43);
            this.lblOnDutySince.Name = "lblOnDutySince";
            this.lblOnDutySince.Size = new System.Drawing.Size(165, 23);
            this.lblOnDutySince.TabIndex = 3;
            this.lblOnDutySince.Text = "--/--/---- --:--";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Controls.Add(this.loaderTrainInfo);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(544, 115);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Train Information";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdEndTrain);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lnkSymbol);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lblOnDutySince);
            this.splitContainer1.Panel1.Controls.Add(this.lblStatus);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtInstructions);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Size = new System.Drawing.Size(538, 94);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 0;
            // 
            // cmdEndTrain
            // 
            this.cmdEndTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEndTrain.Location = new System.Drawing.Point(3, 69);
            this.cmdEndTrain.Name = "cmdEndTrain";
            this.cmdEndTrain.Size = new System.Drawing.Size(255, 23);
            this.cmdEndTrain.TabIndex = 4;
            this.cmdEndTrain.Text = "End Train";
            this.cmdEndTrain.UseVisualStyleBackColor = true;
            this.cmdEndTrain.Click += new System.EventHandler(this.cmdEndTrain_Click);
            // 
            // txtInstructions
            // 
            this.txtInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstructions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstructions.Location = new System.Drawing.Point(3, 16);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.ReadOnly = true;
            this.txtInstructions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInstructions.Size = new System.Drawing.Size(267, 75);
            this.txtInstructions.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Train Instructions:";
            // 
            // loaderTrainInfo
            // 
            this.loaderTrainInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderTrainInfo.BackColor = System.Drawing.Color.Transparent;
            this.loaderTrainInfo.Location = new System.Drawing.Point(3, 16);
            this.loaderTrainInfo.Name = "loaderTrainInfo";
            this.loaderTrainInfo.Size = new System.Drawing.Size(538, 99);
            this.loaderTrainInfo.TabIndex = 1;
            this.loaderTrainInfo.Visible = false;
            // 
            // lblTotalLength
            // 
            this.lblTotalLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalLength.Location = new System.Drawing.Point(447, 341);
            this.lblTotalLength.Name = "lblTotalLength";
            this.lblTotalLength.Size = new System.Drawing.Size(65, 13);
            this.lblTotalLength.TabIndex = 0;
            this.lblTotalLength.Text = "Stock Total:";
            // 
            // lblStockTotal
            // 
            this.lblStockTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStockTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStockTotal.Location = new System.Drawing.Point(83, 341);
            this.lblStockTotal.Name = "lblStockTotal";
            this.lblStockTotal.Size = new System.Drawing.Size(62, 13);
            this.lblStockTotal.TabIndex = 0;
            this.lblStockTotal.Text = "Stock Total:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(358, 341);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Total Length:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Stock Total:";
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabConsist);
            this.tabControl.Controls.Add(this.tabDutyTrans);
            this.tabControl.Controls.Add(this.tabLocoFuel);
            this.tabControl.Controls.Add(this.tabHandledCars);
            this.tabControl.Location = new System.Drawing.Point(0, 121);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(545, 365);
            this.tabControl.TabIndex = 6;
            // 
            // tabConsist
            // 
            this.tabConsist.Controls.Add(this.lblTotalLength);
            this.tabConsist.Controls.Add(this.label8);
            this.tabConsist.Controls.Add(this.dgvConsist);
            this.tabConsist.Controls.Add(this.lblStockTotal);
            this.tabConsist.Controls.Add(this.toolStrip2);
            this.tabConsist.Controls.Add(this.label6);
            this.tabConsist.Controls.Add(this.loaderConsist);
            this.tabConsist.Location = new System.Drawing.Point(23, 4);
            this.tabConsist.Name = "tabConsist";
            this.tabConsist.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsist.Size = new System.Drawing.Size(518, 357);
            this.tabConsist.TabIndex = 0;
            this.tabConsist.Text = "Consist";
            this.tabConsist.UseVisualStyleBackColor = true;
            // 
            // dgvConsist
            // 
            this.dgvConsist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConsist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colConsistImage,
            this.colReportingMark,
            this.colPosition,
            this.colLoad,
            this.colDestination,
            this.colStrategic,
            this.colConsistType});
            this.dgvConsist.Location = new System.Drawing.Point(3, 28);
            this.dgvConsist.Name = "dgvConsist";
            this.dgvConsist.Size = new System.Drawing.Size(512, 310);
            this.dgvConsist.TabIndex = 2;
            this.dgvConsist.SelectionChanged += new System.EventHandler(this.dgvConsist_SelectionChanged);
            // 
            // colConsistImage
            // 
            this.colConsistImage.HeaderText = "Image";
            this.colConsistImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colConsistImage.Name = "colConsistImage";
            // 
            // colReportingMark
            // 
            this.colReportingMark.HeaderText = "Reporting Mark";
            this.colReportingMark.Name = "colReportingMark";
            this.colReportingMark.Width = 120;
            // 
            // colPosition
            // 
            this.colPosition.HeaderText = "Pos";
            this.colPosition.Name = "colPosition";
            this.colPosition.Width = 40;
            // 
            // colLoad
            // 
            this.colLoad.HeaderText = "Current Load";
            this.colLoad.Name = "colLoad";
            this.colLoad.Width = 200;
            // 
            // colDestination
            // 
            this.colDestination.HeaderText = "Destination";
            this.colDestination.Name = "colDestination";
            this.colDestination.Width = 200;
            // 
            // colStrategic
            // 
            this.colStrategic.HeaderText = "Strategic Dest";
            this.colStrategic.Name = "colStrategic";
            this.colStrategic.Width = 200;
            // 
            // colConsistType
            // 
            this.colConsistType.HeaderText = "Type";
            this.colConsistType.Name = "colConsistType";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolModifyConsist,
            this.toolLiveLoad,
            this.toolSpotAtStrategic,
            this.toolStripSeparator1,
            this.toolPrintManifest,
            this.tsbPrintBOLs});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(512, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolModifyConsist
            // 
            this.toolModifyConsist.Image = global::FleetTracking.Properties.Resources.application_edit;
            this.toolModifyConsist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolModifyConsist.Name = "toolModifyConsist";
            this.toolModifyConsist.Size = new System.Drawing.Size(65, 22);
            this.toolModifyConsist.Text = "Modify";
            this.toolModifyConsist.Click += new System.EventHandler(this.toolModifyConsist_Click);
            // 
            // toolLiveLoad
            // 
            this.toolLiveLoad.Image = global::FleetTracking.Properties.Resources.freight1;
            this.toolLiveLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLiveLoad.Name = "toolLiveLoad";
            this.toolLiveLoad.Size = new System.Drawing.Size(77, 22);
            this.toolLiveLoad.Text = "Live Load";
            this.toolLiveLoad.Click += new System.EventHandler(this.toolLiveLoad_Click);
            // 
            // toolSpotAtStrategic
            // 
            this.toolSpotAtStrategic.Enabled = false;
            this.toolSpotAtStrategic.Image = global::FleetTracking.Properties.Resources.eye;
            this.toolSpotAtStrategic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSpotAtStrategic.Name = "toolSpotAtStrategic";
            this.toolSpotAtStrategic.Size = new System.Drawing.Size(114, 22);
            this.toolSpotAtStrategic.Text = "Spot @ Strategic";
            this.toolSpotAtStrategic.Click += new System.EventHandler(this.toolSpotAtStrategic_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolPrintManifest
            // 
            this.toolPrintManifest.Image = global::FleetTracking.Properties.Resources.printer;
            this.toolPrintManifest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrintManifest.Name = "toolPrintManifest";
            this.toolPrintManifest.Size = new System.Drawing.Size(101, 22);
            this.toolPrintManifest.Text = "Print Manifest";
            this.toolPrintManifest.Click += new System.EventHandler(this.toolPrintManifest_Click);
            // 
            // loaderConsist
            // 
            this.loaderConsist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderConsist.BackColor = System.Drawing.Color.Transparent;
            this.loaderConsist.Location = new System.Drawing.Point(0, 0);
            this.loaderConsist.Name = "loaderConsist";
            this.loaderConsist.Size = new System.Drawing.Size(518, 357);
            this.loaderConsist.TabIndex = 3;
            this.loaderConsist.Visible = false;
            // 
            // tabDutyTrans
            // 
            this.tabDutyTrans.Controls.Add(this.dgvDutyTrans);
            this.tabDutyTrans.Controls.Add(this.toolStrip1);
            this.tabDutyTrans.Controls.Add(this.loaderDutyTrans);
            this.tabDutyTrans.Location = new System.Drawing.Point(23, 4);
            this.tabDutyTrans.Name = "tabDutyTrans";
            this.tabDutyTrans.Padding = new System.Windows.Forms.Padding(3);
            this.tabDutyTrans.Size = new System.Drawing.Size(518, 357);
            this.tabDutyTrans.TabIndex = 1;
            this.tabDutyTrans.Text = "Duty Trans";
            this.tabDutyTrans.UseVisualStyleBackColor = true;
            // 
            // dgvDutyTrans
            // 
            this.dgvDutyTrans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDutyTrans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOperator,
            this.colTimeStart,
            this.colTimeEnd});
            this.dgvDutyTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDutyTrans.Location = new System.Drawing.Point(3, 28);
            this.dgvDutyTrans.Name = "dgvDutyTrans";
            this.dgvDutyTrans.RowHeadersVisible = false;
            this.dgvDutyTrans.Size = new System.Drawing.Size(512, 326);
            this.dgvDutyTrans.TabIndex = 2;
            this.dgvDutyTrans.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDutyTrans_CellDoubleClick);
            // 
            // colOperator
            // 
            this.colOperator.HeaderText = "Operator";
            this.colOperator.Name = "colOperator";
            // 
            // colTimeStart
            // 
            this.colTimeStart.HeaderText = "Start Time";
            this.colTimeStart.Name = "colTimeStart";
            // 
            // colTimeEnd
            // 
            this.colTimeEnd.HeaderText = "End Time";
            this.colTimeEnd.Name = "colTimeEnd";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddTransaction});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(512, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolAddTransaction
            // 
            this.toolAddTransaction.Image = global::FleetTracking.Properties.Resources.add;
            this.toolAddTransaction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddTransaction.Name = "toolAddTransaction";
            this.toolAddTransaction.Size = new System.Drawing.Size(141, 22);
            this.toolAddTransaction.Text = "Add Duty Transaction";
            this.toolAddTransaction.Click += new System.EventHandler(this.toolAddTransaction_Click);
            // 
            // loaderDutyTrans
            // 
            this.loaderDutyTrans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderDutyTrans.BackColor = System.Drawing.Color.Transparent;
            this.loaderDutyTrans.Location = new System.Drawing.Point(0, 0);
            this.loaderDutyTrans.Name = "loaderDutyTrans";
            this.loaderDutyTrans.Size = new System.Drawing.Size(518, 358);
            this.loaderDutyTrans.TabIndex = 3;
            this.loaderDutyTrans.Visible = false;
            // 
            // tabLocoFuel
            // 
            this.tabLocoFuel.Controls.Add(this.dgvLocoFuel);
            this.tabLocoFuel.Controls.Add(this.toolStrip3);
            this.tabLocoFuel.Controls.Add(this.loaderLocoFuel);
            this.tabLocoFuel.Location = new System.Drawing.Point(23, 4);
            this.tabLocoFuel.Name = "tabLocoFuel";
            this.tabLocoFuel.Size = new System.Drawing.Size(518, 357);
            this.tabLocoFuel.TabIndex = 2;
            this.tabLocoFuel.Text = "Loco Fuel";
            this.tabLocoFuel.UseVisualStyleBackColor = true;
            // 
            // dgvLocoFuel
            // 
            this.dgvLocoFuel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocoFuel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFuelImage,
            this.colFuelReportingMark,
            this.colStartingFuel,
            this.colEndingFuel});
            this.dgvLocoFuel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocoFuel.Location = new System.Drawing.Point(0, 25);
            this.dgvLocoFuel.Name = "dgvLocoFuel";
            this.dgvLocoFuel.RowHeadersVisible = false;
            this.dgvLocoFuel.Size = new System.Drawing.Size(518, 332);
            this.dgvLocoFuel.TabIndex = 2;
            // 
            // colFuelImage
            // 
            this.colFuelImage.HeaderText = "Image";
            this.colFuelImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colFuelImage.Name = "colFuelImage";
            // 
            // colFuelReportingMark
            // 
            this.colFuelReportingMark.HeaderText = "Reporting Mark";
            this.colFuelReportingMark.Name = "colFuelReportingMark";
            this.colFuelReportingMark.Width = 120;
            // 
            // colStartingFuel
            // 
            this.colStartingFuel.HeaderText = "Start Amount";
            this.colStartingFuel.Name = "colStartingFuel";
            // 
            // colEndingFuel
            // 
            this.colEndingFuel.HeaderText = "End Amount";
            this.colEndingFuel.Name = "colEndingFuel";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFuelAddLoco,
            this.toolFuelSetStart,
            this.toolFuelSetEnd});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(518, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip1";
            // 
            // toolFuelAddLoco
            // 
            this.toolFuelAddLoco.Image = global::FleetTracking.Properties.Resources.add;
            this.toolFuelAddLoco.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFuelAddLoco.Name = "toolFuelAddLoco";
            this.toolFuelAddLoco.Size = new System.Drawing.Size(115, 22);
            this.toolFuelAddLoco.Text = "Add Locomotive";
            this.toolFuelAddLoco.Click += new System.EventHandler(this.toolFuelAddLoco_Click);
            // 
            // toolFuelSetStart
            // 
            this.toolFuelSetStart.Image = global::FleetTracking.Properties.Resources.control_play_blue;
            this.toolFuelSetStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFuelSetStart.Name = "toolFuelSetStart";
            this.toolFuelSetStart.Size = new System.Drawing.Size(70, 22);
            this.toolFuelSetStart.Text = "Set Start";
            this.toolFuelSetStart.Click += new System.EventHandler(this.toolFuelSetStart_Click);
            // 
            // toolFuelSetEnd
            // 
            this.toolFuelSetEnd.Image = global::FleetTracking.Properties.Resources.control_stop_blue;
            this.toolFuelSetEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFuelSetEnd.Name = "toolFuelSetEnd";
            this.toolFuelSetEnd.Size = new System.Drawing.Size(66, 22);
            this.toolFuelSetEnd.Text = "Set End";
            this.toolFuelSetEnd.Click += new System.EventHandler(this.toolFuelSetEnd_Click);
            // 
            // loaderLocoFuel
            // 
            this.loaderLocoFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderLocoFuel.BackColor = System.Drawing.Color.Transparent;
            this.loaderLocoFuel.Location = new System.Drawing.Point(0, 0);
            this.loaderLocoFuel.Name = "loaderLocoFuel";
            this.loaderLocoFuel.Size = new System.Drawing.Size(518, 358);
            this.loaderLocoFuel.TabIndex = 3;
            this.loaderLocoFuel.Visible = false;
            // 
            // tabHandledCars
            // 
            this.tabHandledCars.Controls.Add(this.dgvHandledCars);
            this.tabHandledCars.Controls.Add(this.toolStrip4);
            this.tabHandledCars.Controls.Add(this.loaderHandledCars);
            this.tabHandledCars.Location = new System.Drawing.Point(23, 4);
            this.tabHandledCars.Name = "tabHandledCars";
            this.tabHandledCars.Size = new System.Drawing.Size(518, 357);
            this.tabHandledCars.TabIndex = 3;
            this.tabHandledCars.Text = "Handled Cars";
            this.tabHandledCars.UseVisualStyleBackColor = true;
            // 
            // dgvHandledCars
            // 
            this.dgvHandledCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHandledCars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHandledImage,
            this.colHandledReportingMark,
            this.colPickedUp,
            this.colSetOut,
            this.colPartialTrip});
            this.dgvHandledCars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHandledCars.Location = new System.Drawing.Point(0, 25);
            this.dgvHandledCars.Name = "dgvHandledCars";
            this.dgvHandledCars.Size = new System.Drawing.Size(518, 332);
            this.dgvHandledCars.TabIndex = 1;
            // 
            // colHandledImage
            // 
            this.colHandledImage.HeaderText = "Image";
            this.colHandledImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colHandledImage.Name = "colHandledImage";
            // 
            // colHandledReportingMark
            // 
            this.colHandledReportingMark.HeaderText = "Reporting Mark";
            this.colHandledReportingMark.Name = "colHandledReportingMark";
            this.colHandledReportingMark.Width = 120;
            // 
            // colPickedUp
            // 
            this.colPickedUp.HeaderText = "Picked Up From";
            this.colPickedUp.Name = "colPickedUp";
            this.colPickedUp.Width = 120;
            // 
            // colSetOut
            // 
            this.colSetOut.HeaderText = "Set Out To";
            this.colSetOut.Name = "colSetOut";
            this.colSetOut.Width = 120;
            // 
            // colPartialTrip
            // 
            this.colPartialTrip.HeaderText = "Partial Trip";
            this.colPartialTrip.Name = "colPartialTrip";
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTogglePartialTrip});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(518, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolTogglePartialTrip
            // 
            this.toolTogglePartialTrip.Image = global::FleetTracking.Properties.Resources.arrow_switch;
            this.toolTogglePartialTrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTogglePartialTrip.Name = "toolTogglePartialTrip";
            this.toolTogglePartialTrip.Size = new System.Drawing.Size(122, 22);
            this.toolTogglePartialTrip.Text = "Toggle Partial Trip";
            this.toolTogglePartialTrip.Click += new System.EventHandler(this.toolTogglePartialTrip_Click);
            // 
            // loaderHandledCars
            // 
            this.loaderHandledCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderHandledCars.BackColor = System.Drawing.Color.Transparent;
            this.loaderHandledCars.Location = new System.Drawing.Point(0, 0);
            this.loaderHandledCars.Name = "loaderHandledCars";
            this.loaderHandledCars.Size = new System.Drawing.Size(518, 357);
            this.loaderHandledCars.TabIndex = 2;
            this.loaderHandledCars.Visible = false;
            // 
            // tsbPrintBOLs
            // 
            this.tsbPrintBOLs.Image = global::FleetTracking.Properties.Resources.printer;
            this.tsbPrintBOLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrintBOLs.Name = "tsbPrintBOLs";
            this.tsbPrintBOLs.Size = new System.Drawing.Size(104, 22);
            this.tsbPrintBOLs.Text = "Net Print BOLs";
            this.tsbPrintBOLs.Click += new System.EventHandler(this.tsbPrintBOLs_Click);
            // 
            // InProgressTrainDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox3);
            this.Name = "InProgressTrainDisplay";
            this.Size = new System.Drawing.Size(548, 486);
            this.Load += new System.EventHandler(this.InProgressTrainDisplay_Load);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabConsist.ResumeLayout(false);
            this.tabConsist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsist)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabDutyTrans.ResumeLayout(false);
            this.tabDutyTrans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDutyTrans)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabLocoFuel.ResumeLayout(false);
            this.tabLocoFuel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocoFuel)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabHandledCars.ResumeLayout(false);
            this.tabHandledCars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandledCars)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkSymbol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblOnDutySince;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblTotalLength;
        private System.Windows.Forms.Label lblStockTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabConsist;
        private System.Windows.Forms.TabPage tabDutyTrans;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvConsist;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolModifyConsist;
        private System.Windows.Forms.ToolStripButton toolLiveLoad;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolAddTransaction;
        private System.Windows.Forms.TabPage tabLocoFuel;
        private System.Windows.Forms.DataGridView dgvLocoFuel;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton toolFuelAddLoco;
        private System.Windows.Forms.TabPage tabHandledCars;
        private System.Windows.Forms.ToolStripButton toolFuelSetStart;
        private System.Windows.Forms.ToolStripButton toolFuelSetEnd;
        private DataGridViewStylizer dataGridViewStylizer;
        private System.Windows.Forms.DataGridView dgvDutyTrans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeEnd;
        private System.Windows.Forms.DataGridViewImageColumn colFuelImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFuelReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartingFuel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndingFuel;
        private System.Windows.Forms.DataGridView dgvHandledCars;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton toolTogglePartialTrip;
        private Loader loaderTrainInfo;
        private Loader loaderConsist;
        private Loader loaderDutyTrans;
        private Loader loaderLocoFuel;
        private Loader loaderHandledCars;
        private System.Windows.Forms.DataGridViewImageColumn colHandledImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHandledReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPickedUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetOut;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPartialTrip;
        private System.Windows.Forms.Button cmdEndTrain;
        private System.Windows.Forms.DataGridViewImageColumn colConsistImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStrategic;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConsistType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolPrintManifest;
        private System.Windows.Forms.ToolStripButton toolSpotAtStrategic;
        private System.Windows.Forms.ToolStripButton tsbPrintBOLs;
    }
}
