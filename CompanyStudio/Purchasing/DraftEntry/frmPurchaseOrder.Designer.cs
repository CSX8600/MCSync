namespace CompanyStudio.Purchasing.DraftEntry
{
    partial class frmPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrder));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkTaxBreakdown = new System.Windows.Forms.LinkLabel();
            this.txtGrossTotal = new System.Windows.Forms.TextBox();
            this.txtEstTax = new System.Windows.Forms.TextBox();
            this.txtNetTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGovernment = new System.Windows.Forms.ComboBox();
            this.rdoGovernment = new System.Windows.Forms.RadioButton();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.rdoLocation = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlPurchaseOrderLines = new System.Windows.Forms.Panel();
            this.lblLinePlaceholder = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAddNewLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolLoadTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolSaveTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmdSubmit = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.loader = new CompanyStudio.Loader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpFulfillmentPlanInformation = new System.Windows.Forms.GroupBox();
            this.lblPlanPlaceholder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvFulfillmentPlans = new System.Windows.Forms.DataGridView();
            this.colRailcar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFPPOLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolAddPlan = new System.Windows.Forms.ToolStripButton();
            this.toolDeletePlan = new System.Windows.Forms.ToolStripButton();
            this.cloneSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolClonePlan = new System.Windows.Forms.ToolStripButton();
            this.lblSaveToAddPlans = new System.Windows.Forms.ToolStripLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlPurchaseOrderLines.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpFulfillmentPlanInformation.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Purchase Order Drafting";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lnkTaxBreakdown);
            this.groupBox1.Controls.Add(this.txtGrossTotal);
            this.groupBox1.Controls.Add(this.txtEstTax);
            this.groupBox1.Controls.Add(this.txtNetTotal);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtOrderDate);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboGovernment);
            this.groupBox1.Controls.Add(this.rdoGovernment);
            this.groupBox1.Controls.Add(this.cboLocation);
            this.groupBox1.Controls.Add(this.rdoLocation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Purchase Order Information";
            // 
            // lnkTaxBreakdown
            // 
            this.lnkTaxBreakdown.AutoSize = true;
            this.lnkTaxBreakdown.Location = new System.Drawing.Point(180, 207);
            this.lnkTaxBreakdown.Name = "lnkTaxBreakdown";
            this.lnkTaxBreakdown.Size = new System.Drawing.Size(82, 13);
            this.lnkTaxBreakdown.TabIndex = 9;
            this.lnkTaxBreakdown.TabStop = true;
            this.lnkTaxBreakdown.Text = "Tax Breakdown";
            this.lnkTaxBreakdown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTaxBreakdown_LinkClicked);
            // 
            // txtGrossTotal
            // 
            this.txtGrossTotal.Location = new System.Drawing.Point(346, 185);
            this.txtGrossTotal.Name = "txtGrossTotal";
            this.txtGrossTotal.ReadOnly = true;
            this.txtGrossTotal.Size = new System.Drawing.Size(62, 20);
            this.txtGrossTotal.TabIndex = 8;
            // 
            // txtEstTax
            // 
            this.txtEstTax.Location = new System.Drawing.Point(190, 185);
            this.txtEstTax.Name = "txtEstTax";
            this.txtEstTax.ReadOnly = true;
            this.txtEstTax.Size = new System.Drawing.Size(62, 20);
            this.txtEstTax.TabIndex = 7;
            // 
            // txtNetTotal
            // 
            this.txtNetTotal.Location = new System.Drawing.Point(70, 185);
            this.txtNetTotal.Name = "txtNetTotal";
            this.txtNetTotal.ReadOnly = true;
            this.txtNetTotal.Size = new System.Drawing.Size(62, 20);
            this.txtNetTotal.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(258, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Est Gross Total:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Est Tax:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Net Total:";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderDate.Location = new System.Drawing.Point(74, 71);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.ReadOnly = true;
            this.txtOrderDate.Size = new System.Drawing.Size(370, 20);
            this.txtOrderDate.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(9, 114);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(435, 65);
            this.txtDescription.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Order Date:";
            // 
            // cboGovernment
            // 
            this.cboGovernment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGovernment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboGovernment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboGovernment.Enabled = false;
            this.cboGovernment.FormattingEnabled = true;
            this.cboGovernment.Location = new System.Drawing.Point(163, 44);
            this.cboGovernment.Name = "cboGovernment";
            this.cboGovernment.Size = new System.Drawing.Size(281, 21);
            this.cboGovernment.TabIndex = 3;
            this.cboGovernment.SelectedIndexChanged += new System.EventHandler(this.cboGovernment_SelectedIndexChanged);
            // 
            // rdoGovernment
            // 
            this.rdoGovernment.AutoSize = true;
            this.rdoGovernment.Enabled = false;
            this.rdoGovernment.Location = new System.Drawing.Point(74, 46);
            this.rdoGovernment.Name = "rdoGovernment";
            this.rdoGovernment.Size = new System.Drawing.Size(83, 17);
            this.rdoGovernment.TabIndex = 2;
            this.rdoGovernment.Text = "Government";
            this.rdoGovernment.UseVisualStyleBackColor = true;
            this.rdoGovernment.CheckedChanged += new System.EventHandler(this.OrderFromCheckedChanged);
            // 
            // cboLocation
            // 
            this.cboLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(163, 17);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(281, 21);
            this.cboLocation.TabIndex = 1;
            this.cboLocation.SelectedIndexChanged += new System.EventHandler(this.cboLocation_SelectedIndexChanged);
            // 
            // rdoLocation
            // 
            this.rdoLocation.AutoSize = true;
            this.rdoLocation.Checked = true;
            this.rdoLocation.Location = new System.Drawing.Point(74, 19);
            this.rdoLocation.Name = "rdoLocation";
            this.rdoLocation.Size = new System.Drawing.Size(69, 17);
            this.rdoLocation.TabIndex = 0;
            this.rdoLocation.TabStop = true;
            this.rdoLocation.Text = "Company";
            this.rdoLocation.UseVisualStyleBackColor = true;
            this.rdoLocation.CheckedChanged += new System.EventHandler(this.OrderFromCheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Order From:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnlPurchaseOrderLines);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Location = new System.Drawing.Point(3, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 208);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Purchase Order Lines";
            // 
            // pnlPurchaseOrderLines
            // 
            this.pnlPurchaseOrderLines.AutoScroll = true;
            this.pnlPurchaseOrderLines.Controls.Add(this.lblLinePlaceholder);
            this.pnlPurchaseOrderLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPurchaseOrderLines.Location = new System.Drawing.Point(3, 47);
            this.pnlPurchaseOrderLines.Name = "pnlPurchaseOrderLines";
            this.pnlPurchaseOrderLines.Size = new System.Drawing.Size(444, 158);
            this.pnlPurchaseOrderLines.TabIndex = 1;
            // 
            // lblLinePlaceholder
            // 
            this.lblLinePlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLinePlaceholder.AutoSize = true;
            this.lblLinePlaceholder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinePlaceholder.Location = new System.Drawing.Point(124, 73);
            this.lblLinePlaceholder.Name = "lblLinePlaceholder";
            this.lblLinePlaceholder.Size = new System.Drawing.Size(196, 13);
            this.lblLinePlaceholder.TabIndex = 0;
            this.lblLinePlaceholder.Text = "Click New Line to order items or services";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddNewLine});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(444, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolAddNewLine
            // 
            this.toolAddNewLine.Image = global::CompanyStudio.Properties.Resources.cart_add;
            this.toolAddNewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddNewLine.Name = "toolAddNewLine";
            this.toolAddNewLine.Size = new System.Drawing.Size(84, 28);
            this.toolAddNewLine.Text = "New Line";
            this.toolAddNewLine.Click += new System.EventHandler(this.toolAddNewLine_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLoadTemplate,
            this.toolSaveTemplate,
            this.toolStripSeparator1,
            this.toolDelete});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(793, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip2";
            // 
            // toolLoadTemplate
            // 
            this.toolLoadTemplate.Image = global::CompanyStudio.Properties.Resources.script_go;
            this.toolLoadTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolLoadTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLoadTemplate.Name = "toolLoadTemplate";
            this.toolLoadTemplate.Size = new System.Drawing.Size(105, 22);
            this.toolLoadTemplate.Text = "Load Template";
            this.toolLoadTemplate.Click += new System.EventHandler(this.toolLoadTemplate_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolDelete
            // 
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::CompanyStudio.Properties.Resources.delete;
            this.toolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(144, 22);
            this.toolDelete.Text = "Delete Purchase Order";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblStatus,
            this.toolWarnings});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(793, 20);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 15);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 0);
            // 
            // toolWarnings
            // 
            this.toolWarnings.BackColor = System.Drawing.Color.Yellow;
            this.toolWarnings.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolWarnings.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolWarnings.ForeColor = System.Drawing.Color.Black;
            this.toolWarnings.Image = global::CompanyStudio.Properties.Resources.error;
            this.toolWarnings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolWarnings.Name = "toolWarnings";
            this.toolWarnings.Size = new System.Drawing.Size(94, 20);
            this.toolWarnings.Text = "0 Warning(s)";
            this.toolWarnings.Visible = false;
            this.toolWarnings.Click += new System.EventHandler(this.toolWarnings_Click);
            // 
            // cmdSubmit
            // 
            this.cmdSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSubmit.Location = new System.Drawing.Point(216, 443);
            this.cmdSubmit.Name = "cmdSubmit";
            this.cmdSubmit.Size = new System.Drawing.Size(75, 23);
            this.cmdSubmit.TabIndex = 4;
            this.cmdSubmit.Text = "Submit";
            this.cmdSubmit.UseVisualStyleBackColor = true;
            this.cmdSubmit.Visible = false;
            this.cmdSubmit.Click += new System.EventHandler(this.cmdSubmit_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(297, 443);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(378, 443);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(298, 221);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 6;
            this.loader.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdCancel);
            this.splitContainer1.Panel1.Controls.Add(this.cmdSubmit);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.cmdSave);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(793, 469);
            this.splitContainer1.SplitterDistance = 456;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.grpFulfillmentPlanInformation);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(327, 466);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fulfillment Planning";
            // 
            // grpFulfillmentPlanInformation
            // 
            this.grpFulfillmentPlanInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFulfillmentPlanInformation.Controls.Add(this.lblPlanPlaceholder);
            this.grpFulfillmentPlanInformation.Location = new System.Drawing.Point(6, 167);
            this.grpFulfillmentPlanInformation.Name = "grpFulfillmentPlanInformation";
            this.grpFulfillmentPlanInformation.Size = new System.Drawing.Size(315, 296);
            this.grpFulfillmentPlanInformation.TabIndex = 0;
            this.grpFulfillmentPlanInformation.TabStop = false;
            this.grpFulfillmentPlanInformation.Text = "Fulfillment Plan Information";
            // 
            // lblPlanPlaceholder
            // 
            this.lblPlanPlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPlanPlaceholder.AutoSize = true;
            this.lblPlanPlaceholder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanPlaceholder.Location = new System.Drawing.Point(47, 142);
            this.lblPlanPlaceholder.Name = "lblPlanPlaceholder";
            this.lblPlanPlaceholder.Size = new System.Drawing.Size(221, 13);
            this.lblPlanPlaceholder.TabIndex = 1;
            this.lblPlanPlaceholder.Text = "Select a plan above or click New Plan to start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(267, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fulfillment Plans For Purchase Order";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvFulfillmentPlans);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Location = new System.Drawing.Point(6, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 122);
            this.panel1.TabIndex = 1;
            // 
            // dgvFulfillmentPlans
            // 
            this.dgvFulfillmentPlans.AllowUserToAddRows = false;
            this.dgvFulfillmentPlans.AllowUserToDeleteRows = false;
            this.dgvFulfillmentPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFulfillmentPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRailcar,
            this.colFPPOLines,
            this.colRoute});
            this.dgvFulfillmentPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFulfillmentPlans.Location = new System.Drawing.Point(0, 25);
            this.dgvFulfillmentPlans.Name = "dgvFulfillmentPlans";
            this.dgvFulfillmentPlans.ReadOnly = true;
            this.dgvFulfillmentPlans.RowHeadersVisible = false;
            this.dgvFulfillmentPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFulfillmentPlans.Size = new System.Drawing.Size(315, 97);
            this.dgvFulfillmentPlans.TabIndex = 1;
            this.dgvFulfillmentPlans.SelectionChanged += new System.EventHandler(this.dgvFulfillmentPlans_SelectionChanged);
            // 
            // colRailcar
            // 
            this.colRailcar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRailcar.HeaderText = "Railcar";
            this.colRailcar.Name = "colRailcar";
            this.colRailcar.ReadOnly = true;
            // 
            // colFPPOLines
            // 
            this.colFPPOLines.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFPPOLines.HeaderText = "PO Lines";
            this.colFPPOLines.Name = "colFPPOLines";
            this.colFPPOLines.ReadOnly = true;
            // 
            // colRoute
            // 
            this.colRoute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRoute.HeaderText = "Route";
            this.colRoute.Name = "colRoute";
            this.colRoute.ReadOnly = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddPlan,
            this.toolDeletePlan,
            this.cloneSeparator,
            this.toolClonePlan,
            this.lblSaveToAddPlans});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(315, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolAddPlan
            // 
            this.toolAddPlan.Image = global::CompanyStudio.Properties.Resources.package_add;
            this.toolAddPlan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAddPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddPlan.Name = "toolAddPlan";
            this.toolAddPlan.Size = new System.Drawing.Size(77, 22);
            this.toolAddPlan.Text = "New Plan";
            this.toolAddPlan.Visible = false;
            this.toolAddPlan.Click += new System.EventHandler(this.toolAddPlan_Click);
            // 
            // toolDeletePlan
            // 
            this.toolDeletePlan.Enabled = false;
            this.toolDeletePlan.Image = global::CompanyStudio.Properties.Resources.package_delete;
            this.toolDeletePlan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDeletePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeletePlan.Name = "toolDeletePlan";
            this.toolDeletePlan.Size = new System.Drawing.Size(86, 22);
            this.toolDeletePlan.Text = "Delete Plan";
            this.toolDeletePlan.Visible = false;
            this.toolDeletePlan.Click += new System.EventHandler(this.toolDeletePlan_Click);
            // 
            // cloneSeparator
            // 
            this.cloneSeparator.Name = "cloneSeparator";
            this.cloneSeparator.Size = new System.Drawing.Size(6, 25);
            this.cloneSeparator.Visible = false;
            // 
            // toolClonePlan
            // 
            this.toolClonePlan.Enabled = false;
            this.toolClonePlan.Image = global::CompanyStudio.Properties.Resources.package_go;
            this.toolClonePlan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolClonePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClonePlan.Name = "toolClonePlan";
            this.toolClonePlan.Size = new System.Drawing.Size(84, 22);
            this.toolClonePlan.Text = "Clone Plan";
            this.toolClonePlan.Visible = false;
            this.toolClonePlan.Click += new System.EventHandler(this.toolClonePlan_Click);
            // 
            // lblSaveToAddPlans
            // 
            this.lblSaveToAddPlans.Name = "lblSaveToAddPlans";
            this.lblSaveToAddPlans.Size = new System.Drawing.Size(103, 22);
            this.lblSaveToAddPlans.Text = "Save To Add Plans";
            // 
            // frmPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 542);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order";
            this.Load += new System.EventHandler(this.frmPurchaseOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlPurchaseOrderLines.ResumeLayout(false);
            this.pnlPurchaseOrderLines.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpFulfillmentPlanInformation.ResumeLayout(false);
            this.grpFulfillmentPlanInformation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFulfillmentPlans)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboGovernment;
        private System.Windows.Forms.RadioButton rdoGovernment;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.RadioButton rdoLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlPurchaseOrderLines;
        private System.Windows.Forms.ToolStripButton toolLoadTemplate;
        private System.Windows.Forms.ToolStripButton toolAddNewLine;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label lblLinePlaceholder;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdSubmit;
        private System.Windows.Forms.TextBox txtOrderDate;
        private Loader loader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvFulfillmentPlans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRailcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFPPOLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoute;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolAddPlan;
        private System.Windows.Forms.ToolStripButton toolDeletePlan;
        private System.Windows.Forms.GroupBox grpFulfillmentPlanInformation;
        private System.Windows.Forms.Label lblPlanPlaceholder;
        private System.Windows.Forms.ToolStripLabel lblSaveToAddPlans;
        private System.Windows.Forms.ToolStripButton toolSaveTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.ToolStripStatusLabel toolWarnings;
        private System.Windows.Forms.ToolStripSeparator cloneSeparator;
        private System.Windows.Forms.ToolStripButton toolClonePlan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkTaxBreakdown;
        private System.Windows.Forms.TextBox txtGrossTotal;
        private System.Windows.Forms.TextBox txtEstTax;
        private System.Windows.Forms.TextBox txtNetTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}