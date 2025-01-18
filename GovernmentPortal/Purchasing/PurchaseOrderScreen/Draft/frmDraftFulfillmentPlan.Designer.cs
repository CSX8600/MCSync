namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    partial class frmDraftFulfillmentPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDraftFulfillmentPlan));
            this.label1 = new System.Windows.Forms.Label();
            this.lnkRailcar = new System.Windows.Forms.LinkLabel();
            this.cmdSelectRailcarLeaseRequest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPurchaseOrderLines = new System.Windows.Forms.TextBox();
            this.cboPostFulfillment = new System.Windows.Forms.ComboBox();
            this.cboStrategicAfterDestination = new System.Windows.Forms.ComboBox();
            this.cboStrategicAfterPickup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboDestination = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboPickup = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvRoute = new System.Windows.Forms.DataGridView();
            this.colRoute = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colUp = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDown = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdInsertRoute = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ctxSelectRailcarLeaseRequest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectRailcar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLeaseRequests = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateLeaseRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectLeaseRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClearRailcar = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).BeginInit();
            this.ctxSelectRailcarLeaseRequest.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Railcar:";
            // 
            // lnkRailcar
            // 
            this.lnkRailcar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkRailcar.Enabled = false;
            this.lnkRailcar.Location = new System.Drawing.Point(62, 9);
            this.lnkRailcar.Name = "lnkRailcar";
            this.lnkRailcar.Size = new System.Drawing.Size(363, 23);
            this.lnkRailcar.TabIndex = 1;
            this.lnkRailcar.TabStop = true;
            this.lnkRailcar.Text = "[None]";
            this.lnkRailcar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdSelectRailcarLeaseRequest
            // 
            this.cmdSelectRailcarLeaseRequest.Location = new System.Drawing.Point(62, 35);
            this.cmdSelectRailcarLeaseRequest.Name = "cmdSelectRailcarLeaseRequest";
            this.cmdSelectRailcarLeaseRequest.Size = new System.Drawing.Size(363, 23);
            this.cmdSelectRailcarLeaseRequest.TabIndex = 2;
            this.cmdSelectRailcarLeaseRequest.Text = "Select Railcar/Lease Request";
            this.cmdSelectRailcarLeaseRequest.UseVisualStyleBackColor = true;
            this.cmdSelectRailcarLeaseRequest.Click += new System.EventHandler(this.cmdSelectRailcarLeaseRequest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Associated Purchase Order Lines:";
            // 
            // txtPurchaseOrderLines
            // 
            this.txtPurchaseOrderLines.Location = new System.Drawing.Point(12, 77);
            this.txtPurchaseOrderLines.Multiline = true;
            this.txtPurchaseOrderLines.Name = "txtPurchaseOrderLines";
            this.txtPurchaseOrderLines.ReadOnly = true;
            this.txtPurchaseOrderLines.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPurchaseOrderLines.Size = new System.Drawing.Size(413, 38);
            this.txtPurchaseOrderLines.TabIndex = 4;
            // 
            // cboPostFulfillment
            // 
            this.cboPostFulfillment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPostFulfillment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPostFulfillment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPostFulfillment.FormattingEnabled = true;
            this.cboPostFulfillment.Location = new System.Drawing.Point(159, 229);
            this.cboPostFulfillment.Name = "cboPostFulfillment";
            this.cboPostFulfillment.Size = new System.Drawing.Size(266, 21);
            this.cboPostFulfillment.TabIndex = 23;
            // 
            // cboStrategicAfterDestination
            // 
            this.cboStrategicAfterDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStrategicAfterDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboStrategicAfterDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboStrategicAfterDestination.FormattingEnabled = true;
            this.cboStrategicAfterDestination.Location = new System.Drawing.Point(159, 202);
            this.cboStrategicAfterDestination.Name = "cboStrategicAfterDestination";
            this.cboStrategicAfterDestination.Size = new System.Drawing.Size(266, 21);
            this.cboStrategicAfterDestination.TabIndex = 24;
            // 
            // cboStrategicAfterPickup
            // 
            this.cboStrategicAfterPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStrategicAfterPickup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboStrategicAfterPickup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboStrategicAfterPickup.FormattingEnabled = true;
            this.cboStrategicAfterPickup.Location = new System.Drawing.Point(159, 148);
            this.cboStrategicAfterPickup.Name = "cboStrategicAfterPickup";
            this.cboStrategicAfterPickup.Size = new System.Drawing.Size(266, 21);
            this.cboStrategicAfterPickup.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Destination after fulfillment:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Strategic Track after drop off:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Strategic Track after pickup:";
            // 
            // cboDestination
            // 
            this.cboDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDestination.FormattingEnabled = true;
            this.cboDestination.Location = new System.Drawing.Point(159, 175);
            this.cboDestination.Name = "cboDestination";
            this.cboDestination.Size = new System.Drawing.Size(266, 21);
            this.cboDestination.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 178);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Drop Off Track:";
            // 
            // cboPickup
            // 
            this.cboPickup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPickup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPickup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPickup.FormattingEnabled = true;
            this.cboPickup.Location = new System.Drawing.Point(159, 121);
            this.cboPickup.Name = "cboPickup";
            this.cboPickup.Size = new System.Drawing.Size(266, 21);
            this.cboPickup.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Pickup Track:";
            // 
            // dgvRoute
            // 
            this.dgvRoute.AllowUserToAddRows = false;
            this.dgvRoute.AllowUserToDeleteRows = false;
            this.dgvRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRoute,
            this.colUp,
            this.colDown,
            this.colDelete});
            this.dgvRoute.Location = new System.Drawing.Point(12, 269);
            this.dgvRoute.Name = "dgvRoute";
            this.dgvRoute.RowHeadersVisible = false;
            this.dgvRoute.Size = new System.Drawing.Size(413, 99);
            this.dgvRoute.TabIndex = 28;
            this.dgvRoute.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoute_CellClick);
            this.dgvRoute.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvRoute_CellPainting);
            // 
            // colRoute
            // 
            this.colRoute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRoute.HeaderText = "Route Through";
            this.colRoute.Name = "colRoute";
            // 
            // colUp
            // 
            this.colUp.HeaderText = "";
            this.colUp.Name = "colUp";
            this.colUp.Width = 20;
            // 
            // colDown
            // 
            this.colDown.HeaderText = "";
            this.colDown.Name = "colDown";
            this.colDown.Width = 20;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "";
            this.colDelete.Name = "colDelete";
            this.colDelete.Width = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Route:";
            // 
            // cmdInsertRoute
            // 
            this.cmdInsertRoute.Enabled = false;
            this.cmdInsertRoute.Location = new System.Drawing.Point(350, 374);
            this.cmdInsertRoute.Name = "cmdInsertRoute";
            this.cmdInsertRoute.Size = new System.Drawing.Size(75, 23);
            this.cmdInsertRoute.TabIndex = 29;
            this.cmdInsertRoute.Text = "Insert Route";
            this.cmdInsertRoute.UseVisualStyleBackColor = true;
            this.cmdInsertRoute.Click += new System.EventHandler(this.cmdInsertRoute_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(350, 403);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 30;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(269, 403);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 30;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // ctxSelectRailcarLeaseRequest
            // 
            this.ctxSelectRailcarLeaseRequest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectRailcar,
            this.tsmiLeaseRequests,
            this.toolStripMenuItem1,
            this.tsmiClearRailcar});
            this.ctxSelectRailcarLeaseRequest.Name = "contextMenuStrip1";
            this.ctxSelectRailcarLeaseRequest.Size = new System.Drawing.Size(214, 76);
            this.ctxSelectRailcarLeaseRequest.Opening += new System.ComponentModel.CancelEventHandler(this.ctxSelectRailcarLeaseRequest_Opening);
            // 
            // tsmiSelectRailcar
            // 
            this.tsmiSelectRailcar.Name = "tsmiSelectRailcar";
            this.tsmiSelectRailcar.Size = new System.Drawing.Size(213, 22);
            this.tsmiSelectRailcar.Text = "Select Owned/Leased Railcar";
            this.tsmiSelectRailcar.Click += new System.EventHandler(this.tsmiSelectRailcar_Click);
            // 
            // tsmiLeaseRequests
            // 
            this.tsmiLeaseRequests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateLeaseRequest,
            this.tsmiSelectLeaseRequest});
            this.tsmiLeaseRequests.Name = "tsmiLeaseRequests";
            this.tsmiLeaseRequests.Size = new System.Drawing.Size(213, 22);
            this.tsmiLeaseRequests.Text = "Lease Request";
            // 
            // tsmiCreateLeaseRequest
            // 
            this.tsmiCreateLeaseRequest.Name = "tsmiCreateLeaseRequest";
            this.tsmiCreateLeaseRequest.Size = new System.Drawing.Size(217, 22);
            this.tsmiCreateLeaseRequest.Text = "Create New Lease Request";
            this.tsmiCreateLeaseRequest.Click += new System.EventHandler(this.tsmiCreateLeaseRequest_Click);
            // 
            // tsmiSelectLeaseRequest
            // 
            this.tsmiSelectLeaseRequest.Name = "tsmiSelectLeaseRequest";
            this.tsmiSelectLeaseRequest.Size = new System.Drawing.Size(217, 22);
            this.tsmiSelectLeaseRequest.Text = "Select Existing Lease Request";
            this.tsmiSelectLeaseRequest.Click += new System.EventHandler(this.tsmiSelectLeaseRequest_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 6);
            // 
            // tsmiClearRailcar
            // 
            this.tsmiClearRailcar.Name = "tsmiClearRailcar";
            this.tsmiClearRailcar.Size = new System.Drawing.Size(213, 22);
            this.tsmiClearRailcar.Text = "Clear Selection";
            this.tsmiClearRailcar.Click += new System.EventHandler(this.tsmiClearRailcar_Click);
            // 
            // frmDraftFulfillmentPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 433);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdInsertRoute);
            this.Controls.Add(this.dgvRoute);
            this.Controls.Add(this.cboPostFulfillment);
            this.Controls.Add(this.cboStrategicAfterDestination);
            this.Controls.Add(this.cboStrategicAfterPickup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboDestination);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboPickup);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPurchaseOrderLines);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdSelectRailcarLeaseRequest);
            this.Controls.Add(this.lnkRailcar);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDraftFulfillmentPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fulfillment Plan";
            this.Load += new System.EventHandler(this.frmDraftFulfillmentPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).EndInit();
            this.ctxSelectRailcarLeaseRequest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkRailcar;
        private System.Windows.Forms.Button cmdSelectRailcarLeaseRequest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPurchaseOrderLines;
        private System.Windows.Forms.ComboBox cboPostFulfillment;
        private System.Windows.Forms.ComboBox cboStrategicAfterDestination;
        private System.Windows.Forms.ComboBox cboStrategicAfterPickup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboDestination;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboPickup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvRoute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdInsertRoute;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewComboBoxColumn colRoute;
        private System.Windows.Forms.DataGridViewButtonColumn colUp;
        private System.Windows.Forms.DataGridViewButtonColumn colDown;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.ContextMenuStrip ctxSelectRailcarLeaseRequest;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectRailcar;
        private System.Windows.Forms.ToolStripMenuItem tsmiLeaseRequests;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateLeaseRequest;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectLeaseRequest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearRailcar;
    }
}