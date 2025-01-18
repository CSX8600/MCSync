namespace CompanyStudio.Purchasing.DraftEntry
{
    partial class frmLeaseRequestSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeaseRequestSelect));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.dgvLeaseRequests = new System.Windows.Forms.DataGridView();
            this.colRequestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeaseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBids = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPurpose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaseRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(508, 170);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(589, 170);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // dgvLeaseRequests
            // 
            this.dgvLeaseRequests.AllowUserToAddRows = false;
            this.dgvLeaseRequests.AllowUserToDeleteRows = false;
            this.dgvLeaseRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLeaseRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeaseRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRequestID,
            this.colLeaseType,
            this.colEndTime,
            this.colBids,
            this.colPurpose});
            this.dgvLeaseRequests.Location = new System.Drawing.Point(0, 0);
            this.dgvLeaseRequests.Name = "dgvLeaseRequests";
            this.dgvLeaseRequests.ReadOnly = true;
            this.dgvLeaseRequests.RowHeadersVisible = false;
            this.dgvLeaseRequests.RowHeadersWidth = 62;
            this.dgvLeaseRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaseRequests.Size = new System.Drawing.Size(676, 164);
            this.dgvLeaseRequests.TabIndex = 4;
            // 
            // colRequestID
            // 
            this.colRequestID.HeaderText = "Request Number";
            this.colRequestID.Name = "colRequestID";
            this.colRequestID.ReadOnly = true;
            this.colRequestID.Width = 50;
            // 
            // colLeaseType
            // 
            this.colLeaseType.HeaderText = "Lease Type";
            this.colLeaseType.Name = "colLeaseType";
            this.colLeaseType.ReadOnly = true;
            // 
            // colEndTime
            // 
            this.colEndTime.HeaderText = "End Time";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.ReadOnly = true;
            // 
            // colBids
            // 
            this.colBids.HeaderText = "Bid Count";
            this.colBids.Name = "colBids";
            this.colBids.ReadOnly = true;
            this.colBids.Width = 75;
            // 
            // colPurpose
            // 
            this.colPurpose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPurpose.HeaderText = "Purpose";
            this.colPurpose.Name = "colPurpose";
            this.colPurpose.ReadOnly = true;
            // 
            // frmLeaseRequestSelect
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(676, 205);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.dgvLeaseRequests);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLeaseRequestSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Lease Request";
            this.Load += new System.EventHandler(this.frmLeaseRequestSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaseRequests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.DataGridView dgvLeaseRequests;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeaseType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBids;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPurpose;
    }
}