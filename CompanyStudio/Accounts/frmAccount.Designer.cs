
namespace CompanyStudio.Accounts
{
    partial class frmAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccount));
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.tctrlInfo = new System.Windows.Forms.TabControl();
            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.cmdTransPrev = new System.Windows.Forms.Button();
            this.cmdTransFirst = new System.Windows.Forms.Button();
            this.cmdTransNext = new System.Windows.Forms.Button();
            this.cmdTransLast = new System.Windows.Forms.Button();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaderTransactions = new CompanyStudio.Loader();
            this.tabFiscalQuarters = new System.Windows.Forms.TabPage();
            this.dgvFiscalQuarters = new System.Windows.Forms.DataGridView();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuarter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartBal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaderFQ = new CompanyStudio.Loader();
            this.tabDebitCards = new System.Windows.Forms.TabPage();
            this.dgvDebitCards = new System.Windows.Forms.DataGridView();
            this.colCardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIssuedTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIssuedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbDeleteDebitCards = new System.Windows.Forms.ToolStripButton();
            this.loaderDebitCards = new CompanyStudio.Loader();
            this.tabAccess = new System.Windows.Forms.TabPage();
            this.dgvAccess = new System.Windows.Forms.DataGridView();
            this.colAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaderAccess = new CompanyStudio.Loader();
            this.loader = new CompanyStudio.Loader();
            this.cmdTransfer = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tctrlInfo.SuspendLayout();
            this.tabTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.tabFiscalQuarters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiscalQuarters)).BeginInit();
            this.tabDebitCards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebitCards)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabAccess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccess)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account Number:";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccountNumber.Location = new System.Drawing.Point(99, 12);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.ReadOnly = true;
            this.txtAccountNumber.Size = new System.Drawing.Size(689, 20);
            this.txtAccountNumber.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(99, 38);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(689, 20);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.TextChanged += new System.EventHandler(this.MarkFormDirty);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Category:";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(99, 64);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(689, 21);
            this.cboCategory.TabIndex = 2;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.MarkFormDirty);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Current Balance:";
            // 
            // txtBalance
            // 
            this.txtBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBalance.Location = new System.Drawing.Point(99, 91);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(689, 20);
            this.txtBalance.TabIndex = 3;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(713, 117);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(632, 117);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // tctrlInfo
            // 
            this.tctrlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tctrlInfo.Controls.Add(this.tabTransactions);
            this.tctrlInfo.Controls.Add(this.tabFiscalQuarters);
            this.tctrlInfo.Controls.Add(this.tabDebitCards);
            this.tctrlInfo.Controls.Add(this.tabAccess);
            this.tctrlInfo.Location = new System.Drawing.Point(6, 146);
            this.tctrlInfo.Name = "tctrlInfo";
            this.tctrlInfo.SelectedIndex = 0;
            this.tctrlInfo.Size = new System.Drawing.Size(782, 292);
            this.tctrlInfo.TabIndex = 6;
            this.tctrlInfo.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabTransactions
            // 
            this.tabTransactions.Controls.Add(this.cmdTransPrev);
            this.tabTransactions.Controls.Add(this.cmdTransFirst);
            this.tabTransactions.Controls.Add(this.cmdTransNext);
            this.tabTransactions.Controls.Add(this.cmdTransLast);
            this.tabTransactions.Controls.Add(this.dgvTransactions);
            this.tabTransactions.Controls.Add(this.loaderTransactions);
            this.tabTransactions.Location = new System.Drawing.Point(4, 22);
            this.tabTransactions.Name = "tabTransactions";
            this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactions.Size = new System.Drawing.Size(774, 266);
            this.tabTransactions.TabIndex = 0;
            this.tabTransactions.Text = "Transactions";
            this.tabTransactions.UseVisualStyleBackColor = true;
            // 
            // cmdTransPrev
            // 
            this.cmdTransPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdTransPrev.Enabled = false;
            this.cmdTransPrev.Location = new System.Drawing.Point(41, 237);
            this.cmdTransPrev.Name = "cmdTransPrev";
            this.cmdTransPrev.Size = new System.Drawing.Size(29, 23);
            this.cmdTransPrev.TabIndex = 1;
            this.cmdTransPrev.Text = "<";
            this.cmdTransPrev.UseVisualStyleBackColor = true;
            this.cmdTransPrev.Click += new System.EventHandler(this.cmdTransPrev_Click);
            // 
            // cmdTransFirst
            // 
            this.cmdTransFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdTransFirst.Enabled = false;
            this.cmdTransFirst.Location = new System.Drawing.Point(6, 237);
            this.cmdTransFirst.Name = "cmdTransFirst";
            this.cmdTransFirst.Size = new System.Drawing.Size(29, 23);
            this.cmdTransFirst.TabIndex = 1;
            this.cmdTransFirst.Text = "<<";
            this.cmdTransFirst.UseVisualStyleBackColor = true;
            this.cmdTransFirst.Click += new System.EventHandler(this.cmdTransFirst_Click);
            // 
            // cmdTransNext
            // 
            this.cmdTransNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTransNext.Enabled = false;
            this.cmdTransNext.Location = new System.Drawing.Point(704, 237);
            this.cmdTransNext.Name = "cmdTransNext";
            this.cmdTransNext.Size = new System.Drawing.Size(29, 23);
            this.cmdTransNext.TabIndex = 1;
            this.cmdTransNext.Text = ">";
            this.cmdTransNext.UseVisualStyleBackColor = true;
            this.cmdTransNext.Click += new System.EventHandler(this.cmdTransNext_Click);
            // 
            // cmdTransLast
            // 
            this.cmdTransLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTransLast.Enabled = false;
            this.cmdTransLast.Location = new System.Drawing.Point(739, 237);
            this.cmdTransLast.Name = "cmdTransLast";
            this.cmdTransLast.Size = new System.Drawing.Size(29, 23);
            this.cmdTransLast.TabIndex = 1;
            this.cmdTransLast.Text = ">>";
            this.cmdTransLast.UseVisualStyleBackColor = true;
            this.cmdTransLast.Click += new System.EventHandler(this.cmdTransLast_Click);
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colAmount,
            this.colDescription});
            this.dgvTransactions.Location = new System.Drawing.Point(3, 3);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(768, 231);
            this.dgvTransactions.TabIndex = 0;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Date/Time";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 400;
            // 
            // loaderTransactions
            // 
            this.loaderTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderTransactions.BackColor = System.Drawing.Color.Transparent;
            this.loaderTransactions.Location = new System.Drawing.Point(0, 0);
            this.loaderTransactions.Name = "loaderTransactions";
            this.loaderTransactions.Size = new System.Drawing.Size(774, 266);
            this.loaderTransactions.TabIndex = 2;
            this.loaderTransactions.Visible = false;
            // 
            // tabFiscalQuarters
            // 
            this.tabFiscalQuarters.Controls.Add(this.dgvFiscalQuarters);
            this.tabFiscalQuarters.Controls.Add(this.loaderFQ);
            this.tabFiscalQuarters.Location = new System.Drawing.Point(4, 22);
            this.tabFiscalQuarters.Name = "tabFiscalQuarters";
            this.tabFiscalQuarters.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiscalQuarters.Size = new System.Drawing.Size(774, 266);
            this.tabFiscalQuarters.TabIndex = 1;
            this.tabFiscalQuarters.Text = "Fiscal Quarters";
            this.tabFiscalQuarters.UseVisualStyleBackColor = true;
            // 
            // dgvFiscalQuarters
            // 
            this.dgvFiscalQuarters.AllowUserToAddRows = false;
            this.dgvFiscalQuarters.AllowUserToDeleteRows = false;
            this.dgvFiscalQuarters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiscalQuarters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiscalQuarters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colYear,
            this.colQuarter,
            this.colStartDate,
            this.colEndDate,
            this.colStartBal,
            this.colEndBalance,
            this.colNetChange,
            this.colNetPercent});
            this.dgvFiscalQuarters.Location = new System.Drawing.Point(6, 15);
            this.dgvFiscalQuarters.Name = "dgvFiscalQuarters";
            this.dgvFiscalQuarters.ReadOnly = true;
            this.dgvFiscalQuarters.RowHeadersVisible = false;
            this.dgvFiscalQuarters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiscalQuarters.Size = new System.Drawing.Size(768, 251);
            this.dgvFiscalQuarters.TabIndex = 2;
            // 
            // colYear
            // 
            this.colYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            this.colYear.Width = 54;
            // 
            // colQuarter
            // 
            this.colQuarter.HeaderText = "Quarter";
            this.colQuarter.Name = "colQuarter";
            this.colQuarter.ReadOnly = true;
            this.colQuarter.Width = 30;
            // 
            // colStartDate
            // 
            this.colStartDate.HeaderText = "Start Date";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.ReadOnly = true;
            // 
            // colEndDate
            // 
            this.colEndDate.HeaderText = "End Date";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.ReadOnly = true;
            // 
            // colStartBal
            // 
            this.colStartBal.HeaderText = "Starting Balance";
            this.colStartBal.Name = "colStartBal";
            this.colStartBal.ReadOnly = true;
            // 
            // colEndBalance
            // 
            this.colEndBalance.HeaderText = "Ending Balance";
            this.colEndBalance.Name = "colEndBalance";
            this.colEndBalance.ReadOnly = true;
            // 
            // colNetChange
            // 
            this.colNetChange.HeaderText = "Net Change";
            this.colNetChange.Name = "colNetChange";
            this.colNetChange.ReadOnly = true;
            // 
            // colNetPercent
            // 
            this.colNetPercent.HeaderText = "Net Percent";
            this.colNetPercent.Name = "colNetPercent";
            this.colNetPercent.ReadOnly = true;
            this.colNetPercent.Width = 90;
            // 
            // loaderFQ
            // 
            this.loaderFQ.BackColor = System.Drawing.Color.Transparent;
            this.loaderFQ.Location = new System.Drawing.Point(0, 0);
            this.loaderFQ.Name = "loaderFQ";
            this.loaderFQ.Size = new System.Drawing.Size(774, 266);
            this.loaderFQ.TabIndex = 7;
            this.loaderFQ.Visible = false;
            // 
            // tabDebitCards
            // 
            this.tabDebitCards.Controls.Add(this.dgvDebitCards);
            this.tabDebitCards.Controls.Add(this.toolStrip1);
            this.tabDebitCards.Controls.Add(this.loaderDebitCards);
            this.tabDebitCards.Location = new System.Drawing.Point(4, 22);
            this.tabDebitCards.Name = "tabDebitCards";
            this.tabDebitCards.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebitCards.Size = new System.Drawing.Size(774, 266);
            this.tabDebitCards.TabIndex = 3;
            this.tabDebitCards.Text = "Debit Cards";
            this.tabDebitCards.UseVisualStyleBackColor = true;
            // 
            // dgvDebitCards
            // 
            this.dgvDebitCards.AllowUserToAddRows = false;
            this.dgvDebitCards.AllowUserToDeleteRows = false;
            this.dgvDebitCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDebitCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardNumber,
            this.colIssuedTo,
            this.colIssuedTime});
            this.dgvDebitCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDebitCards.Location = new System.Drawing.Point(3, 34);
            this.dgvDebitCards.Name = "dgvDebitCards";
            this.dgvDebitCards.RowHeadersVisible = false;
            this.dgvDebitCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDebitCards.Size = new System.Drawing.Size(768, 229);
            this.dgvDebitCards.TabIndex = 2;
            // 
            // colCardNumber
            // 
            this.colCardNumber.HeaderText = "Card Number";
            this.colCardNumber.Name = "colCardNumber";
            this.colCardNumber.Width = 200;
            // 
            // colIssuedTo
            // 
            this.colIssuedTo.HeaderText = "Issued To";
            this.colIssuedTo.Name = "colIssuedTo";
            this.colIssuedTo.Width = 200;
            // 
            // colIssuedTime
            // 
            this.colIssuedTime.HeaderText = "Issued Time";
            this.colIssuedTime.Name = "colIssuedTime";
            this.colIssuedTime.Width = 200;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDeleteDebitCards});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(768, 31);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbDeleteDebitCards
            // 
            this.tsbDeleteDebitCards.Image = global::CompanyStudio.Properties.Resources.vcard_delete;
            this.tsbDeleteDebitCards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteDebitCards.Name = "tsbDeleteDebitCards";
            this.tsbDeleteDebitCards.Size = new System.Drawing.Size(127, 28);
            this.tsbDeleteDebitCards.Text = "Delete Debit Card";
            this.tsbDeleteDebitCards.Click += new System.EventHandler(this.tsbDeleteDebitCards_Click);
            // 
            // loaderDebitCards
            // 
            this.loaderDebitCards.BackColor = System.Drawing.Color.Transparent;
            this.loaderDebitCards.Location = new System.Drawing.Point(0, 0);
            this.loaderDebitCards.Name = "loaderDebitCards";
            this.loaderDebitCards.Size = new System.Drawing.Size(774, 266);
            this.loaderDebitCards.TabIndex = 4;
            this.loaderDebitCards.Visible = false;
            // 
            // tabAccess
            // 
            this.tabAccess.Controls.Add(this.dgvAccess);
            this.tabAccess.Controls.Add(this.loaderAccess);
            this.tabAccess.Location = new System.Drawing.Point(4, 22);
            this.tabAccess.Name = "tabAccess";
            this.tabAccess.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccess.Size = new System.Drawing.Size(774, 266);
            this.tabAccess.TabIndex = 2;
            this.tabAccess.Text = "Access";
            this.tabAccess.UseVisualStyleBackColor = true;
            // 
            // dgvAccess
            // 
            this.dgvAccess.AllowUserToAddRows = false;
            this.dgvAccess.AllowUserToDeleteRows = false;
            this.dgvAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAccess,
            this.colEmployee});
            this.dgvAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccess.Location = new System.Drawing.Point(3, 3);
            this.dgvAccess.Name = "dgvAccess";
            this.dgvAccess.RowHeadersVisible = false;
            this.dgvAccess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccess.Size = new System.Drawing.Size(768, 260);
            this.dgvAccess.TabIndex = 1;
            // 
            // colAccess
            // 
            this.colAccess.HeaderText = "Access?";
            this.colAccess.Name = "colAccess";
            this.colAccess.Width = 60;
            // 
            // colEmployee
            // 
            this.colEmployee.HeaderText = "Employee";
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.ReadOnly = true;
            this.colEmployee.Width = 300;
            // 
            // loaderAccess
            // 
            this.loaderAccess.BackColor = System.Drawing.Color.Transparent;
            this.loaderAccess.Location = new System.Drawing.Point(2, 3);
            this.loaderAccess.Name = "loaderAccess";
            this.loaderAccess.Size = new System.Drawing.Size(769, 260);
            this.loaderAccess.TabIndex = 2;
            this.loaderAccess.Visible = false;
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(0, 0);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(802, 449);
            this.loader.TabIndex = 7;
            // 
            // cmdTransfer
            // 
            this.cmdTransfer.Location = new System.Drawing.Point(6, 117);
            this.cmdTransfer.Name = "cmdTransfer";
            this.cmdTransfer.Size = new System.Drawing.Size(87, 23);
            this.cmdTransfer.TabIndex = 8;
            this.cmdTransfer.Text = "Transfer Funds";
            this.cmdTransfer.UseVisualStyleBackColor = true;
            this.cmdTransfer.Click += new System.EventHandler(this.cmdTransfer_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(99, 117);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(87, 23);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close Account";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmAccount
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdTransfer);
            this.Controls.Add(this.tctrlInfo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAccountNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAccount_FormClosed);
            this.Load += new System.EventHandler(this.frmAccount_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmAccount_KeyUp);
            this.tctrlInfo.ResumeLayout(false);
            this.tabTransactions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.tabFiscalQuarters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiscalQuarters)).EndInit();
            this.tabDebitCards.ResumeLayout(false);
            this.tabDebitCards.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebitCards)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabAccess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TabControl tctrlInfo;
        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.TabPage tabFiscalQuarters;
        private Loader loader;
        private System.Windows.Forms.Button cmdTransfer;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.Button cmdTransPrev;
        private System.Windows.Forms.Button cmdTransFirst;
        private System.Windows.Forms.Button cmdTransNext;
        private System.Windows.Forms.Button cmdTransLast;
        private Loader loaderTransactions;
        private System.Windows.Forms.DataGridView dgvFiscalQuarters;
        private Loader loaderFQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuarter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartBal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetPercent;
        private System.Windows.Forms.TabPage tabAccess;
        private System.Windows.Forms.DataGridView dgvAccess;
        private Loader loaderAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployee;
        private System.Windows.Forms.TabPage tabDebitCards;
        private System.Windows.Forms.DataGridView dgvDebitCards;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIssuedTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIssuedTime;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbDeleteDebitCards;
        private Loader loaderDebitCards;
    }
}