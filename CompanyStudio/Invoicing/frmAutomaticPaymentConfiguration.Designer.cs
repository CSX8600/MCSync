namespace CompanyStudio.Invoicing
{
    partial class frmAutomaticPaymentConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomaticPaymentConfiguration));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstConfigs = new System.Windows.Forms.ListView();
            this.colPayee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPaidAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMaxAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddPayees = new System.Windows.Forms.ToolStripButton();
            this.tsbDeletePayees = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbResetPaidAmounts = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdateSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCloneTo = new System.Windows.Forms.ToolStripButton();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUpcomingInvoices = new System.Windows.Forms.DataGridView();
            this.colInvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPayee = new System.Windows.Forms.TextBox();
            this.cboPaymentAccount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            this.rdoImmediately = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoOnDueDate = new System.Windows.Forms.RadioButton();
            this.txtMaxAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdReset = new System.Windows.Forms.Button();
            this.pnlPlaceholder = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingInvoices)).BeginInit();
            this.pnlPlaceholder.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstConfigs);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlDetails);
            this.splitContainer1.Panel2.Controls.Add(this.pnlPlaceholder);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstConfigs
            // 
            this.lstConfigs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPayee,
            this.colPaidAmount,
            this.colMaxAmount,
            this.colAccount});
            this.lstConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConfigs.FullRowSelect = true;
            this.lstConfigs.HideSelection = false;
            this.lstConfigs.Location = new System.Drawing.Point(0, 69);
            this.lstConfigs.Name = "lstConfigs";
            this.lstConfigs.Size = new System.Drawing.Size(281, 381);
            this.lstConfigs.TabIndex = 1;
            this.lstConfigs.UseCompatibleStateImageBehavior = false;
            this.lstConfigs.View = System.Windows.Forms.View.Details;
            this.lstConfigs.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstConfigs_ItemSelectionChanged);
            this.lstConfigs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstConfigs_KeyDown);
            // 
            // colPayee
            // 
            this.colPayee.Text = "Payee";
            this.colPayee.Width = 150;
            // 
            // colPaidAmount
            // 
            this.colPaidAmount.Text = "Paid Amount";
            this.colPaidAmount.Width = 100;
            // 
            // colMaxAmount
            // 
            this.colMaxAmount.Text = "Max Amount";
            this.colMaxAmount.Width = 100;
            // 
            // colAccount
            // 
            this.colAccount.Text = "Account";
            this.colAccount.Width = 150;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddPayees,
            this.tsbDeletePayees,
            this.toolStripSeparator1,
            this.tsbResetPaidAmounts,
            this.tsbUpdateSelected,
            this.toolStripSeparator2,
            this.tsbCloneTo});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(281, 69);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddPayees
            // 
            this.tsbAddPayees.Image = global::CompanyStudio.Properties.Resources.building_add;
            this.tsbAddPayees.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddPayees.Name = "tsbAddPayees";
            this.tsbAddPayees.Size = new System.Drawing.Size(88, 20);
            this.tsbAddPayees.Text = "Add Payees";
            this.tsbAddPayees.Click += new System.EventHandler(this.tsbAddPayees_Click);
            // 
            // tsbDeletePayees
            // 
            this.tsbDeletePayees.Image = global::CompanyStudio.Properties.Resources.building_delete;
            this.tsbDeletePayees.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeletePayees.Name = "tsbDeletePayees";
            this.tsbDeletePayees.Size = new System.Drawing.Size(99, 20);
            this.tsbDeletePayees.Text = "Delete Payees";
            this.tsbDeletePayees.Click += new System.EventHandler(this.tsbDeletePayees_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // tsbResetPaidAmounts
            // 
            this.tsbResetPaidAmounts.Image = global::CompanyStudio.Properties.Resources.arrow_undo;
            this.tsbResetPaidAmounts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbResetPaidAmounts.Name = "tsbResetPaidAmounts";
            this.tsbResetPaidAmounts.Size = new System.Drawing.Size(133, 20);
            this.tsbResetPaidAmounts.Text = "Reset Paid Amounts";
            this.tsbResetPaidAmounts.Click += new System.EventHandler(this.tsbResetPaidAmounts_Click);
            // 
            // tsbUpdateSelected
            // 
            this.tsbUpdateSelected.Image = global::CompanyStudio.Properties.Resources.page_edit;
            this.tsbUpdateSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdateSelected.Name = "tsbUpdateSelected";
            this.tsbUpdateSelected.Size = new System.Drawing.Size(112, 20);
            this.tsbUpdateSelected.Text = "Update Selected";
            this.tsbUpdateSelected.Click += new System.EventHandler(this.tsbUpdateSelected_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // tsbCloneTo
            // 
            this.tsbCloneTo.Image = global::CompanyStudio.Properties.Resources.paste_plain;
            this.tsbCloneTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloneTo.Name = "tsbCloneTo";
            this.tsbCloneTo.Size = new System.Drawing.Size(130, 20);
            this.tsbCloneTo.Text = "Clone Selected To...";
            this.tsbCloneTo.Click += new System.EventHandler(this.tsbCloneTo_Click);
            // 
            // pnlDetails
            // 
            this.pnlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetails.Controls.Add(this.cmdSave);
            this.pnlDetails.Controls.Add(this.label1);
            this.pnlDetails.Controls.Add(this.dgvUpcomingInvoices);
            this.pnlDetails.Controls.Add(this.label2);
            this.pnlDetails.Controls.Add(this.label7);
            this.pnlDetails.Controls.Add(this.txtPayee);
            this.pnlDetails.Controls.Add(this.cboPaymentAccount);
            this.pnlDetails.Controls.Add(this.label3);
            this.pnlDetails.Controls.Add(this.label6);
            this.pnlDetails.Controls.Add(this.txtAmountPaid);
            this.pnlDetails.Controls.Add(this.rdoImmediately);
            this.pnlDetails.Controls.Add(this.label4);
            this.pnlDetails.Controls.Add(this.rdoOnDueDate);
            this.pnlDetails.Controls.Add(this.txtMaxAmount);
            this.pnlDetails.Controls.Add(this.label5);
            this.pnlDetails.Controls.Add(this.cmdReset);
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(515, 450);
            this.pnlDetails.TabIndex = 0;
            this.pnlDetails.Visible = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(428, 424);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Automatic Payment Configuration";
            // 
            // dgvUpcomingInvoices
            // 
            this.dgvUpcomingInvoices.AllowUserToAddRows = false;
            this.dgvUpcomingInvoices.AllowUserToDeleteRows = false;
            this.dgvUpcomingInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUpcomingInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpcomingInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInvoiceNumber,
            this.colInvoiceDate,
            this.colDueDate,
            this.colDescription,
            this.colAmount});
            this.dgvUpcomingInvoices.Location = new System.Drawing.Point(3, 175);
            this.dgvUpcomingInvoices.Name = "dgvUpcomingInvoices";
            this.dgvUpcomingInvoices.ReadOnly = true;
            this.dgvUpcomingInvoices.RowHeadersVisible = false;
            this.dgvUpcomingInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUpcomingInvoices.Size = new System.Drawing.Size(500, 243);
            this.dgvUpcomingInvoices.TabIndex = 7;
            this.dgvUpcomingInvoices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUpcomingInvoices_CellDoubleClick);
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.HeaderText = "Invoice Number";
            this.colInvoiceNumber.Name = "colInvoiceNumber";
            this.colInvoiceNumber.ReadOnly = true;
            this.colInvoiceNumber.Width = 120;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.HeaderText = "Invoice Date";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.ReadOnly = true;
            // 
            // colDueDate
            // 
            this.colDueDate.HeaderText = "Due Date";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Payee:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Upcoming Invoices:";
            // 
            // txtPayee
            // 
            this.txtPayee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayee.Location = new System.Drawing.Point(49, 32);
            this.txtPayee.Name = "txtPayee";
            this.txtPayee.ReadOnly = true;
            this.txtPayee.Size = new System.Drawing.Size(454, 20);
            this.txtPayee.TabIndex = 0;
            // 
            // cboPaymentAccount
            // 
            this.cboPaymentAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPaymentAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPaymentAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPaymentAccount.FormattingEnabled = true;
            this.cboPaymentAccount.Location = new System.Drawing.Point(109, 135);
            this.cboPaymentAccount.Name = "cboPaymentAccount";
            this.cboPaymentAccount.Size = new System.Drawing.Size(394, 21);
            this.cboPaymentAccount.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Amount Paid:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Payment Account:";
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.Location = new System.Drawing.Point(79, 60);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.ReadOnly = true;
            this.txtAmountPaid.Size = new System.Drawing.Size(100, 20);
            this.txtAmountPaid.TabIndex = 1;
            // 
            // rdoImmediately
            // 
            this.rdoImmediately.AutoSize = true;
            this.rdoImmediately.Location = new System.Drawing.Point(203, 112);
            this.rdoImmediately.Name = "rdoImmediately";
            this.rdoImmediately.Size = new System.Drawing.Size(80, 17);
            this.rdoImmediately.TabIndex = 5;
            this.rdoImmediately.TabStop = true;
            this.rdoImmediately.Text = "Immediately";
            this.rdoImmediately.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Max Amount:";
            // 
            // rdoOnDueDate
            // 
            this.rdoOnDueDate.AutoSize = true;
            this.rdoOnDueDate.Location = new System.Drawing.Point(109, 112);
            this.rdoOnDueDate.Name = "rdoOnDueDate";
            this.rdoOnDueDate.Size = new System.Drawing.Size(88, 17);
            this.rdoOnDueDate.TabIndex = 4;
            this.rdoOnDueDate.TabStop = true;
            this.rdoOnDueDate.Text = "On Due Date";
            this.rdoOnDueDate.UseVisualStyleBackColor = true;
            // 
            // txtMaxAmount
            // 
            this.txtMaxAmount.Location = new System.Drawing.Point(79, 86);
            this.txtMaxAmount.Name = "txtMaxAmount";
            this.txtMaxAmount.Size = new System.Drawing.Size(100, 20);
            this.txtMaxAmount.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Payment Schedule:";
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(185, 58);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 2;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // pnlPlaceholder
            // 
            this.pnlPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaceholder.Controls.Add(this.label8);
            this.pnlPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.pnlPlaceholder.Name = "pnlPlaceholder";
            this.pnlPlaceholder.Size = new System.Drawing.Size(512, 447);
            this.pnlPlaceholder.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(65, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(383, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Select a configuration on the left, or click Add Payees";
            // 
            // frmAutomaticPaymentConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutomaticPaymentConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Automatic Payments Configuration";
            this.Load += new System.EventHandler(this.frmAutomaticPaymentConfiguration_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingInvoices)).EndInit();
            this.pnlPlaceholder.ResumeLayout(false);
            this.pnlPlaceholder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lstConfigs;
        private System.Windows.Forms.ColumnHeader colPayee;
        private System.Windows.Forms.ColumnHeader colPaidAmount;
        private System.Windows.Forms.ColumnHeader colMaxAmount;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddPayees;
        private System.Windows.Forms.ToolStripButton tsbDeletePayees;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPayee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaxAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoOnDueDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdoImmediately;
        private System.Windows.Forms.DataGridView dgvUpcomingInvoices;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPaymentAccount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbResetPaidAmounts;
        private System.Windows.Forms.ToolStripButton tsbCloneTo;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel pnlPlaceholder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColumnHeader colAccount;
        private System.Windows.Forms.ToolStripButton tsbUpdateSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}