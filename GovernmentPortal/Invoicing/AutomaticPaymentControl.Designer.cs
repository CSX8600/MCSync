namespace GovernmentPortal.Invoicing
{
    partial class AutomaticPaymentControl
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingInvoices)).BeginInit();
            this.SuspendLayout();
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
            this.dgvUpcomingInvoices.Location = new System.Drawing.Point(4, 146);
            this.dgvUpcomingInvoices.Name = "dgvUpcomingInvoices";
            this.dgvUpcomingInvoices.ReadOnly = true;
            this.dgvUpcomingInvoices.RowHeadersVisible = false;
            this.dgvUpcomingInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUpcomingInvoices.Size = new System.Drawing.Size(583, 256);
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
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Payee:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Upcoming Invoices:";
            // 
            // txtPayee
            // 
            this.txtPayee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayee.Location = new System.Drawing.Point(50, 3);
            this.txtPayee.Name = "txtPayee";
            this.txtPayee.ReadOnly = true;
            this.txtPayee.Size = new System.Drawing.Size(537, 20);
            this.txtPayee.TabIndex = 0;
            // 
            // cboPaymentAccount
            // 
            this.cboPaymentAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPaymentAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPaymentAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPaymentAccount.FormattingEnabled = true;
            this.cboPaymentAccount.Location = new System.Drawing.Point(110, 106);
            this.cboPaymentAccount.Name = "cboPaymentAccount";
            this.cboPaymentAccount.Size = new System.Drawing.Size(477, 21);
            this.cboPaymentAccount.TabIndex = 6;
            this.cboPaymentAccount.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Amount Paid:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Payment Account:";
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.Location = new System.Drawing.Point(80, 31);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.ReadOnly = true;
            this.txtAmountPaid.Size = new System.Drawing.Size(100, 20);
            this.txtAmountPaid.TabIndex = 1;
            // 
            // rdoImmediately
            // 
            this.rdoImmediately.AutoSize = true;
            this.rdoImmediately.Location = new System.Drawing.Point(204, 83);
            this.rdoImmediately.Name = "rdoImmediately";
            this.rdoImmediately.Size = new System.Drawing.Size(80, 17);
            this.rdoImmediately.TabIndex = 5;
            this.rdoImmediately.TabStop = true;
            this.rdoImmediately.Text = "Immediately";
            this.rdoImmediately.UseVisualStyleBackColor = true;
            this.rdoImmediately.CheckedChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Max Amount:";
            // 
            // rdoOnDueDate
            // 
            this.rdoOnDueDate.AutoSize = true;
            this.rdoOnDueDate.Location = new System.Drawing.Point(110, 83);
            this.rdoOnDueDate.Name = "rdoOnDueDate";
            this.rdoOnDueDate.Size = new System.Drawing.Size(88, 17);
            this.rdoOnDueDate.TabIndex = 4;
            this.rdoOnDueDate.TabStop = true;
            this.rdoOnDueDate.Text = "On Due Date";
            this.rdoOnDueDate.UseVisualStyleBackColor = true;
            this.rdoOnDueDate.CheckedChanged += new System.EventHandler(this.ControlChanged);
            // 
            // txtMaxAmount
            // 
            this.txtMaxAmount.Location = new System.Drawing.Point(80, 57);
            this.txtMaxAmount.Name = "txtMaxAmount";
            this.txtMaxAmount.Size = new System.Drawing.Size(100, 20);
            this.txtMaxAmount.TabIndex = 3;
            this.txtMaxAmount.TextChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Payment Schedule:";
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(186, 29);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 2;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // AutomaticPaymentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvUpcomingInvoices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPayee);
            this.Controls.Add(this.cboPaymentAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAmountPaid);
            this.Controls.Add(this.rdoImmediately);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rdoOnDueDate);
            this.Controls.Add(this.txtMaxAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdReset);
            this.Name = "AutomaticPaymentControl";
            this.Size = new System.Drawing.Size(590, 405);
            this.Load += new System.EventHandler(this.AutomaticPaymentControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingInvoices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUpcomingInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPayee;
        private System.Windows.Forms.ComboBox cboPaymentAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.RadioButton rdoImmediately;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoOnDueDate;
        private System.Windows.Forms.TextBox txtMaxAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdReset;
    }
}
