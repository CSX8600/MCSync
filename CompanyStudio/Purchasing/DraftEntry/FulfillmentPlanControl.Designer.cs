namespace CompanyStudio.Purchasing.DraftEntry
{
    partial class FulfillmentPlanControl
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
            this.cboStrategicAfterPickup = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboDestination = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboPickup = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvPurchaseOrderLines = new System.Windows.Forms.DataGridView();
            this.colApplyPOLine = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPOLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSelectRailcar = new System.Windows.Forms.Button();
            this.lnkRailcar = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStrategicAfterDestination = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPostFulfillment = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.routeStart = new CompanyStudio.Purchasing.DraftEntry.RouteControlStart();
            this.routeEnd = new CompanyStudio.Purchasing.DraftEntry.RouteControlEnd();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdReset = new System.Windows.Forms.Button();
            this.loader = new CompanyStudio.Loader();
            this.ctxSelectRailcar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolSelectRailcar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSelectLease = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSelectCreateLeaseRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSelectExistingLeaseRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSelectRemoveRailcar = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrderLines)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.ctxSelectRailcar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboStrategicAfterPickup
            // 
            this.cboStrategicAfterPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStrategicAfterPickup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboStrategicAfterPickup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboStrategicAfterPickup.FormattingEnabled = true;
            this.cboStrategicAfterPickup.Location = new System.Drawing.Point(5, 209);
            this.cboStrategicAfterPickup.Name = "cboStrategicAfterPickup";
            this.cboStrategicAfterPickup.Size = new System.Drawing.Size(373, 21);
            this.cboStrategicAfterPickup.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Strategic Track after pickup:";
            // 
            // cboDestination
            // 
            this.cboDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDestination.FormattingEnabled = true;
            this.cboDestination.Location = new System.Drawing.Point(89, 236);
            this.cboDestination.Name = "cboDestination";
            this.cboDestination.Size = new System.Drawing.Size(289, 21);
            this.cboDestination.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 239);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Drop Off Track:";
            // 
            // cboPickup
            // 
            this.cboPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPickup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPickup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPickup.FormattingEnabled = true;
            this.cboPickup.Location = new System.Drawing.Point(89, 169);
            this.cboPickup.Name = "cboPickup";
            this.cboPickup.Size = new System.Drawing.Size(289, 21);
            this.cboPickup.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Pickup Track:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Applies to Purchase Order Lines:";
            // 
            // dgvPurchaseOrderLines
            // 
            this.dgvPurchaseOrderLines.AllowUserToAddRows = false;
            this.dgvPurchaseOrderLines.AllowUserToDeleteRows = false;
            this.dgvPurchaseOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPurchaseOrderLines.ColumnHeadersHeight = 34;
            this.dgvPurchaseOrderLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPurchaseOrderLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colApplyPOLine,
            this.colPOLine});
            this.dgvPurchaseOrderLines.Location = new System.Drawing.Point(2, 65);
            this.dgvPurchaseOrderLines.Name = "dgvPurchaseOrderLines";
            this.dgvPurchaseOrderLines.RowHeadersVisible = false;
            this.dgvPurchaseOrderLines.RowHeadersWidth = 62;
            this.dgvPurchaseOrderLines.Size = new System.Drawing.Size(376, 98);
            this.dgvPurchaseOrderLines.TabIndex = 2;
            // 
            // colApplyPOLine
            // 
            this.colApplyPOLine.HeaderText = "Select";
            this.colApplyPOLine.MinimumWidth = 8;
            this.colApplyPOLine.Name = "colApplyPOLine";
            this.colApplyPOLine.Width = 50;
            // 
            // colPOLine
            // 
            this.colPOLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPOLine.HeaderText = "Purchase Order Line";
            this.colPOLine.MinimumWidth = 8;
            this.colPOLine.Name = "colPOLine";
            this.colPOLine.ReadOnly = true;
            // 
            // cmdSelectRailcar
            // 
            this.cmdSelectRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectRailcar.Location = new System.Drawing.Point(51, 23);
            this.cmdSelectRailcar.Name = "cmdSelectRailcar";
            this.cmdSelectRailcar.Size = new System.Drawing.Size(327, 23);
            this.cmdSelectRailcar.TabIndex = 1;
            this.cmdSelectRailcar.Text = "Select...";
            this.cmdSelectRailcar.UseVisualStyleBackColor = true;
            this.cmdSelectRailcar.Click += new System.EventHandler(this.cmdSelectRailcar_Click);
            // 
            // lnkRailcar
            // 
            this.lnkRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkRailcar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkRailcar.Enabled = false;
            this.lnkRailcar.Location = new System.Drawing.Point(51, 0);
            this.lnkRailcar.Name = "lnkRailcar";
            this.lnkRailcar.Size = new System.Drawing.Size(327, 20);
            this.lnkRailcar.TabIndex = 0;
            this.lnkRailcar.TabStop = true;
            this.lnkRailcar.Text = "None";
            this.lnkRailcar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkRailcar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRailcar_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Railcar:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Strategic Track after drop off:";
            // 
            // cboStrategicAfterDestination
            // 
            this.cboStrategicAfterDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStrategicAfterDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboStrategicAfterDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboStrategicAfterDestination.FormattingEnabled = true;
            this.cboStrategicAfterDestination.Location = new System.Drawing.Point(5, 276);
            this.cboStrategicAfterDestination.Name = "cboStrategicAfterDestination";
            this.cboStrategicAfterDestination.Size = new System.Drawing.Size(373, 21);
            this.cboStrategicAfterDestination.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Destination after fulfillment:";
            // 
            // cboPostFulfillment
            // 
            this.cboPostFulfillment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPostFulfillment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPostFulfillment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPostFulfillment.FormattingEnabled = true;
            this.cboPostFulfillment.Location = new System.Drawing.Point(5, 316);
            this.cboPostFulfillment.Name = "cboPostFulfillment";
            this.cboPostFulfillment.Size = new System.Drawing.Size(373, 21);
            this.cboPostFulfillment.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.routeStart);
            this.flowLayoutPanel1.Controls.Add(this.routeEnd);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 356);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(2, 184);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(375, 184);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // routeStart
            // 
            this.routeStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.routeStart.EntityName = "[Company Start]";
            this.routeStart.GovernmentID = null;
            this.routeStart.Location = new System.Drawing.Point(4, 5);
            this.routeStart.LocationID = null;
            this.routeStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.routeStart.Name = "routeStart";
            this.routeStart.Size = new System.Drawing.Size(317, 56);
            this.routeStart.TabIndex = 0;
            this.routeStart.InsertPressed += new System.EventHandler(this.routeStart_InsertPressed);
            // 
            // routeEnd
            // 
            this.routeEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.routeEnd.EntityName = "[Company End]";
            this.routeEnd.Location = new System.Drawing.Point(4, 71);
            this.routeEnd.LocationID = null;
            this.routeEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.routeEnd.Name = "routeEnd";
            this.routeEnd.Size = new System.Drawing.Size(317, 56);
            this.routeEnd.TabIndex = 2;
            this.routeEnd.InsertPressed += new System.EventHandler(this.routeEnd_InsertPressed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Railcar Routing:";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(303, 542);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 9;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReset.Location = new System.Drawing.Point(222, 542);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 10;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(92, 234);
            this.loader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 20;
            this.loader.Visible = false;
            // 
            // ctxSelectRailcar
            // 
            this.ctxSelectRailcar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxSelectRailcar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelectRailcar,
            this.toolSelectLease,
            this.toolStripMenuItem1,
            this.toolSelectRemoveRailcar});
            this.ctxSelectRailcar.Name = "ctxSelectRailcar";
            this.ctxSelectRailcar.Size = new System.Drawing.Size(226, 76);
            this.ctxSelectRailcar.Opening += new System.ComponentModel.CancelEventHandler(this.ctxSelectRailcar_Opening);
            // 
            // toolSelectRailcar
            // 
            this.toolSelectRailcar.Name = "toolSelectRailcar";
            this.toolSelectRailcar.Size = new System.Drawing.Size(225, 22);
            this.toolSelectRailcar.Text = "Select Owned/Leased Railcar";
            this.toolSelectRailcar.Click += new System.EventHandler(this.toolSelectRailcar_Click);
            // 
            // toolSelectLease
            // 
            this.toolSelectLease.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelectCreateLeaseRequest,
            this.toolSelectExistingLeaseRequest});
            this.toolSelectLease.Name = "toolSelectLease";
            this.toolSelectLease.Size = new System.Drawing.Size(225, 22);
            this.toolSelectLease.Text = "Request a Lease";
            // 
            // toolSelectCreateLeaseRequest
            // 
            this.toolSelectCreateLeaseRequest.Name = "toolSelectCreateLeaseRequest";
            this.toolSelectCreateLeaseRequest.Size = new System.Drawing.Size(225, 22);
            this.toolSelectCreateLeaseRequest.Text = "Create New Lease Request";
            this.toolSelectCreateLeaseRequest.Click += new System.EventHandler(this.toolSelectCreateLeaseRequest_Click);
            // 
            // toolSelectExistingLeaseRequest
            // 
            this.toolSelectExistingLeaseRequest.Name = "toolSelectExistingLeaseRequest";
            this.toolSelectExistingLeaseRequest.Size = new System.Drawing.Size(225, 22);
            this.toolSelectExistingLeaseRequest.Text = "Select Existing Lease Request";
            this.toolSelectExistingLeaseRequest.Click += new System.EventHandler(this.toolSelectExistingLeaseRequest_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(222, 6);
            // 
            // toolSelectRemoveRailcar
            // 
            this.toolSelectRemoveRailcar.Name = "toolSelectRemoveRailcar";
            this.toolSelectRemoveRailcar.Size = new System.Drawing.Size(225, 22);
            this.toolSelectRemoveRailcar.Text = "Remove Selected";
            this.toolSelectRemoveRailcar.Click += new System.EventHandler(this.toolSelectRemoveRailcar_Click);
            // 
            // FulfillmentPlanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cboPostFulfillment);
            this.Controls.Add(this.cboStrategicAfterDestination);
            this.Controls.Add(this.cboStrategicAfterPickup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboDestination);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboPickup);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvPurchaseOrderLines);
            this.Controls.Add(this.cmdSelectRailcar);
            this.Controls.Add(this.lnkRailcar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.loader);
            this.Name = "FulfillmentPlanControl";
            this.Size = new System.Drawing.Size(381, 568);
            this.Load += new System.EventHandler(this.FulfillmentPlanControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrderLines)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ctxSelectRailcar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboStrategicAfterPickup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboDestination;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboPickup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvPurchaseOrderLines;
        private System.Windows.Forms.Button cmdSelectRailcar;
        private System.Windows.Forms.LinkLabel lnkRailcar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStrategicAfterDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboPostFulfillment;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private RouteControlStart routeStart;
        private RouteControlEnd routeEnd;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdReset;
        private Loader loader;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colApplyPOLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPOLine;
        private System.Windows.Forms.ContextMenuStrip ctxSelectRailcar;
        private System.Windows.Forms.ToolStripMenuItem toolSelectRailcar;
        private System.Windows.Forms.ToolStripMenuItem toolSelectLease;
        private System.Windows.Forms.ToolStripMenuItem toolSelectCreateLeaseRequest;
        private System.Windows.Forms.ToolStripMenuItem toolSelectExistingLeaseRequest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolSelectRemoveRailcar;
    }
}
