
namespace GovernmentPortal
{
    partial class frmPortal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPortal));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolFinance = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAccountList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAccountCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPurchasing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPurchaseOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPriceManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoices = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoiceReceivable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicePayable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAutomaticPayments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicesInvoiceConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.emailConfigurationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicePayableReceived = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoiceReceivableReady = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTaxes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSalesTax = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEmploymentTax = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWireTransfers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIssueWireTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWireTransferHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuWireTransfersEmailConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMintCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbInterestRates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolOfficials = new System.Windows.Forms.ToolStripButton();
            this.tsbSwitchGovernment = new System.Windows.Forms.ToolStripButton();
            this.toolEmail = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiAliases = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDistributionLists = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLaws = new System.Windows.Forms.ToolStripButton();
            this.loader = new GovernmentPortal.Loader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFinance,
            this.toolOfficials,
            this.tsbSwitchGovernment,
            this.toolEmail,
            this.toolLaws});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(659, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolFinance
            // 
            this.toolFinance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAccounts,
            this.toolPurchasing,
            this.mnuInvoices,
            this.tsmiTaxes,
            this.mnuWireTransfers,
            this.tsbMintCurrency,
            this.tsbInterestRates});
            this.toolFinance.Image = global::GovernmentPortal.Properties.Resources.money;
            this.toolFinance.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolFinance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFinance.Name = "toolFinance";
            this.toolFinance.Size = new System.Drawing.Size(77, 22);
            this.toolFinance.Text = "Finance";
            this.toolFinance.Visible = false;
            this.toolFinance.DropDownOpening += new System.EventHandler(this.toolFinance_DropDownOpening);
            // 
            // toolAccounts
            // 
            this.toolAccounts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAccountList,
            this.tsmiAccountCategories});
            this.toolAccounts.Image = global::GovernmentPortal.Properties.Resources.group;
            this.toolAccounts.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAccounts.Name = "toolAccounts";
            this.toolAccounts.Size = new System.Drawing.Size(180, 22);
            this.toolAccounts.Text = "Accounts";
            // 
            // tsmiAccountList
            // 
            this.tsmiAccountList.Image = global::GovernmentPortal.Properties.Resources.page;
            this.tsmiAccountList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAccountList.Name = "tsmiAccountList";
            this.tsmiAccountList.Size = new System.Drawing.Size(130, 22);
            this.tsmiAccountList.Text = "List";
            this.tsmiAccountList.Click += new System.EventHandler(this.tsmiAccountList_Click);
            // 
            // tsmiAccountCategories
            // 
            this.tsmiAccountCategories.Image = global::GovernmentPortal.Properties.Resources.database;
            this.tsmiAccountCategories.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAccountCategories.Name = "tsmiAccountCategories";
            this.tsmiAccountCategories.Size = new System.Drawing.Size(130, 22);
            this.tsmiAccountCategories.Text = "Categories";
            this.tsmiAccountCategories.Click += new System.EventHandler(this.tsmiAccountCategories_Click);
            // 
            // toolPurchasing
            // 
            this.toolPurchasing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPurchaseOrders,
            this.toolPriceManager});
            this.toolPurchasing.Image = global::GovernmentPortal.Properties.Resources.cart;
            this.toolPurchasing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolPurchasing.Name = "toolPurchasing";
            this.toolPurchasing.Size = new System.Drawing.Size(180, 22);
            this.toolPurchasing.Text = "Purchasing";
            this.toolPurchasing.Visible = false;
            // 
            // tsbPurchaseOrders
            // 
            this.tsbPurchaseOrders.Image = global::GovernmentPortal.Properties.Resources.cart;
            this.tsbPurchaseOrders.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPurchaseOrders.Name = "tsbPurchaseOrders";
            this.tsbPurchaseOrders.Size = new System.Drawing.Size(160, 22);
            this.tsbPurchaseOrders.Text = "Purchase Orders";
            this.tsbPurchaseOrders.Click += new System.EventHandler(this.tsbPurchaseOrders_Click);
            // 
            // toolPriceManager
            // 
            this.toolPriceManager.Image = global::GovernmentPortal.Properties.Resources.money;
            this.toolPriceManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolPriceManager.Name = "toolPriceManager";
            this.toolPriceManager.Size = new System.Drawing.Size(160, 22);
            this.toolPriceManager.Text = "Price Manager";
            this.toolPriceManager.Click += new System.EventHandler(this.toolPriceManager_Click);
            // 
            // mnuInvoices
            // 
            this.mnuInvoices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInvoiceReceivable,
            this.mnuInvoicePayable,
            this.toolStripMenuItem2,
            this.tsmiAutomaticPayments,
            this.mnuInvoicesInvoiceConfiguration,
            this.emailConfigurationToolStripMenuItem1});
            this.mnuInvoices.Image = global::GovernmentPortal.Properties.Resources.book;
            this.mnuInvoices.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuInvoices.Name = "mnuInvoices";
            this.mnuInvoices.Size = new System.Drawing.Size(180, 22);
            this.mnuInvoices.Text = "Invoices";
            // 
            // mnuInvoiceReceivable
            // 
            this.mnuInvoiceReceivable.Image = global::GovernmentPortal.Properties.Resources.money_add;
            this.mnuInvoiceReceivable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuInvoiceReceivable.Name = "mnuInvoiceReceivable";
            this.mnuInvoiceReceivable.Size = new System.Drawing.Size(189, 22);
            this.mnuInvoiceReceivable.Text = "Receivable";
            this.mnuInvoiceReceivable.Click += new System.EventHandler(this.mnuInvoiceReceivable_Click);
            // 
            // mnuInvoicePayable
            // 
            this.mnuInvoicePayable.Image = global::GovernmentPortal.Properties.Resources.money_delete;
            this.mnuInvoicePayable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuInvoicePayable.Name = "mnuInvoicePayable";
            this.mnuInvoicePayable.Size = new System.Drawing.Size(189, 22);
            this.mnuInvoicePayable.Text = "Payable";
            this.mnuInvoicePayable.Click += new System.EventHandler(this.mnuInvoicePayable_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(186, 6);
            // 
            // tsmiAutomaticPayments
            // 
            this.tsmiAutomaticPayments.Image = global::GovernmentPortal.Properties.Resources.lightning;
            this.tsmiAutomaticPayments.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAutomaticPayments.Name = "tsmiAutomaticPayments";
            this.tsmiAutomaticPayments.Size = new System.Drawing.Size(189, 22);
            this.tsmiAutomaticPayments.Text = "Automatic Payments";
            this.tsmiAutomaticPayments.Click += new System.EventHandler(this.tsmiAutomaticPayments_Click);
            // 
            // mnuInvoicesInvoiceConfiguration
            // 
            this.mnuInvoicesInvoiceConfiguration.Image = global::GovernmentPortal.Properties.Resources.page;
            this.mnuInvoicesInvoiceConfiguration.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuInvoicesInvoiceConfiguration.Name = "mnuInvoicesInvoiceConfiguration";
            this.mnuInvoicesInvoiceConfiguration.Size = new System.Drawing.Size(189, 22);
            this.mnuInvoicesInvoiceConfiguration.Text = "Invoice Configuration";
            this.mnuInvoicesInvoiceConfiguration.Click += new System.EventHandler(this.mnuInvoicesInvoiceConfiguration_Click);
            // 
            // emailConfigurationToolStripMenuItem1
            // 
            this.emailConfigurationToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInvoicePayableReceived,
            this.mnuInvoiceReceivableReady});
            this.emailConfigurationToolStripMenuItem1.Image = global::GovernmentPortal.Properties.Resources.email;
            this.emailConfigurationToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.emailConfigurationToolStripMenuItem1.Name = "emailConfigurationToolStripMenuItem1";
            this.emailConfigurationToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.emailConfigurationToolStripMenuItem1.Text = "Email Configuration";
            // 
            // mnuInvoicePayableReceived
            // 
            this.mnuInvoicePayableReceived.Name = "mnuInvoicePayableReceived";
            this.mnuInvoicePayableReceived.Size = new System.Drawing.Size(165, 22);
            this.mnuInvoicePayableReceived.Text = "Payable Received";
            this.mnuInvoicePayableReceived.Click += new System.EventHandler(this.mnuInvoicePayableReceived_Click);
            // 
            // mnuInvoiceReceivableReady
            // 
            this.mnuInvoiceReceivableReady.Name = "mnuInvoiceReceivableReady";
            this.mnuInvoiceReceivableReady.Size = new System.Drawing.Size(165, 22);
            this.mnuInvoiceReceivableReady.Text = "Receivable Ready";
            this.mnuInvoiceReceivableReady.Click += new System.EventHandler(this.mnuInvoiceReceivableReady_Click);
            // 
            // tsmiTaxes
            // 
            this.tsmiTaxes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSalesTax,
            this.tsmiEmploymentTax});
            this.tsmiTaxes.Image = global::GovernmentPortal.Properties.Resources.money_add;
            this.tsmiTaxes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiTaxes.Name = "tsmiTaxes";
            this.tsmiTaxes.Size = new System.Drawing.Size(180, 22);
            this.tsmiTaxes.Text = "Taxes";
            // 
            // tsmiSalesTax
            // 
            this.tsmiSalesTax.Image = global::GovernmentPortal.Properties.Resources.basket;
            this.tsmiSalesTax.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSalesTax.Name = "tsmiSalesTax";
            this.tsmiSalesTax.Size = new System.Drawing.Size(162, 22);
            this.tsmiSalesTax.Text = "Sales Tax";
            this.tsmiSalesTax.Click += new System.EventHandler(this.tsmiSalesTax_Click);
            // 
            // tsmiEmploymentTax
            // 
            this.tsmiEmploymentTax.Image = global::GovernmentPortal.Properties.Resources.group;
            this.tsmiEmploymentTax.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiEmploymentTax.Name = "tsmiEmploymentTax";
            this.tsmiEmploymentTax.Size = new System.Drawing.Size(162, 22);
            this.tsmiEmploymentTax.Text = "Employment Tax";
            // 
            // mnuWireTransfers
            // 
            this.mnuWireTransfers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuIssueWireTransfer,
            this.mnuWireTransferHistory,
            this.toolStripMenuItem1,
            this.mnuWireTransfersEmailConfiguration});
            this.mnuWireTransfers.Image = global::GovernmentPortal.Properties.Resources.money_delete;
            this.mnuWireTransfers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuWireTransfers.Name = "mnuWireTransfers";
            this.mnuWireTransfers.Size = new System.Drawing.Size(180, 22);
            this.mnuWireTransfers.Text = "Wire Transfers";
            // 
            // mnuIssueWireTransfer
            // 
            this.mnuIssueWireTransfer.Image = global::GovernmentPortal.Properties.Resources.money_delete;
            this.mnuIssueWireTransfer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuIssueWireTransfer.Name = "mnuIssueWireTransfer";
            this.mnuIssueWireTransfer.Size = new System.Drawing.Size(184, 22);
            this.mnuIssueWireTransfer.Text = "Issue Wire Transfer";
            this.mnuIssueWireTransfer.Click += new System.EventHandler(this.mnuIssueWireTransfer_Click);
            // 
            // mnuWireTransferHistory
            // 
            this.mnuWireTransferHistory.Image = global::GovernmentPortal.Properties.Resources.book_open;
            this.mnuWireTransferHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuWireTransferHistory.Name = "mnuWireTransferHistory";
            this.mnuWireTransferHistory.Size = new System.Drawing.Size(184, 22);
            this.mnuWireTransferHistory.Text = "Wire Transfer History";
            this.mnuWireTransferHistory.Click += new System.EventHandler(this.mnuWireTransferHistory_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuWireTransfersEmailConfiguration
            // 
            this.mnuWireTransfersEmailConfiguration.Image = global::GovernmentPortal.Properties.Resources.email;
            this.mnuWireTransfersEmailConfiguration.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuWireTransfersEmailConfiguration.Name = "mnuWireTransfersEmailConfiguration";
            this.mnuWireTransfersEmailConfiguration.Size = new System.Drawing.Size(184, 22);
            this.mnuWireTransfersEmailConfiguration.Text = "Email Configuration";
            this.mnuWireTransfersEmailConfiguration.Click += new System.EventHandler(this.mnuWireTransfersEmailConfiguration_Click);
            // 
            // tsbMintCurrency
            // 
            this.tsbMintCurrency.Image = global::GovernmentPortal.Properties.Resources.coins_add;
            this.tsbMintCurrency.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMintCurrency.Name = "tsbMintCurrency";
            this.tsbMintCurrency.Size = new System.Drawing.Size(180, 22);
            this.tsbMintCurrency.Text = "Mint Currency";
            this.tsbMintCurrency.Click += new System.EventHandler(this.tsbMintCurrency_Click);
            // 
            // tsbInterestRates
            // 
            this.tsbInterestRates.Image = global::GovernmentPortal.Properties.Resources.chart_curve;
            this.tsbInterestRates.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbInterestRates.Name = "tsbInterestRates";
            this.tsbInterestRates.Size = new System.Drawing.Size(180, 22);
            this.tsbInterestRates.Text = "Interest Rates";
            this.tsbInterestRates.Click += new System.EventHandler(this.tsbInterestRates_Click);
            // 
            // toolOfficials
            // 
            this.toolOfficials.Image = global::GovernmentPortal.Properties.Resources.user;
            this.toolOfficials.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolOfficials.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOfficials.Name = "toolOfficials";
            this.toolOfficials.Size = new System.Drawing.Size(70, 22);
            this.toolOfficials.Text = "Officials";
            this.toolOfficials.Visible = false;
            this.toolOfficials.Click += new System.EventHandler(this.toolOfficials_Click);
            // 
            // tsbSwitchGovernment
            // 
            this.tsbSwitchGovernment.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSwitchGovernment.Image = global::GovernmentPortal.Properties.Resources.building_go;
            this.tsbSwitchGovernment.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSwitchGovernment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwitchGovernment.Name = "tsbSwitchGovernment";
            this.tsbSwitchGovernment.Size = new System.Drawing.Size(131, 22);
            this.tsbSwitchGovernment.Text = "Switch Government";
            this.tsbSwitchGovernment.Click += new System.EventHandler(this.tsbSwitchGovernment_Click);
            // 
            // toolEmail
            // 
            this.toolEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAliases,
            this.tsmiDistributionLists});
            this.toolEmail.Image = global::GovernmentPortal.Properties.Resources.email;
            this.toolEmail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEmail.Name = "toolEmail";
            this.toolEmail.Size = new System.Drawing.Size(65, 22);
            this.toolEmail.Text = "Email";
            this.toolEmail.Visible = false;
            // 
            // tsmiAliases
            // 
            this.tsmiAliases.Image = global::GovernmentPortal.Properties.Resources.email_link;
            this.tsmiAliases.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAliases.Name = "tsmiAliases";
            this.tsmiAliases.Size = new System.Drawing.Size(162, 22);
            this.tsmiAliases.Text = "Aliases";
            this.tsmiAliases.Click += new System.EventHandler(this.tsmiAliases_Click);
            // 
            // tsmiDistributionLists
            // 
            this.tsmiDistributionLists.Image = global::GovernmentPortal.Properties.Resources.group_link;
            this.tsmiDistributionLists.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDistributionLists.Name = "tsmiDistributionLists";
            this.tsmiDistributionLists.Size = new System.Drawing.Size(162, 22);
            this.tsmiDistributionLists.Text = "Distribution Lists";
            this.tsmiDistributionLists.Click += new System.EventHandler(this.tsmiDistributionLists_Click);
            // 
            // toolLaws
            // 
            this.toolLaws.Image = global::GovernmentPortal.Properties.Resources.scales;
            this.toolLaws.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolLaws.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLaws.Name = "toolLaws";
            this.toolLaws.Size = new System.Drawing.Size(53, 22);
            this.toolLaws.Text = "Laws";
            this.toolLaws.Visible = false;
            this.toolLaws.Click += new System.EventHandler(this.toolLaws_Click);
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(0, 0);
            this.loader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(659, 372);
            this.loader.TabIndex = 3;
            this.loader.Visible = false;
            // 
            // frmPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 371);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmPortal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Government Portal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPortal_FormClosed);
            this.Load += new System.EventHandler(this.frmPortal_Load);
            this.Shown += new System.EventHandler(this.frmPortal_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolOfficials;
        private System.Windows.Forms.ToolStripDropDownButton toolEmail;
        private System.Windows.Forms.ToolStripMenuItem tsmiAliases;
        private System.Windows.Forms.ToolStripMenuItem tsmiDistributionLists;
        private System.Windows.Forms.ToolStripButton tsbSwitchGovernment;
        private System.Windows.Forms.ToolStripDropDownButton toolFinance;
        private System.Windows.Forms.ToolStripMenuItem toolAccounts;
        private System.Windows.Forms.ToolStripMenuItem tsmiAccountList;
        private System.Windows.Forms.ToolStripMenuItem tsmiAccountCategories;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoices;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoiceReceivable;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicePayable;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicesInvoiceConfiguration;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaxes;
        private System.Windows.Forms.ToolStripMenuItem tsmiSalesTax;
        private System.Windows.Forms.ToolStripMenuItem tsmiEmploymentTax;
        private System.Windows.Forms.ToolStripMenuItem tsbMintCurrency;
        private System.Windows.Forms.ToolStripMenuItem mnuWireTransfers;
        private System.Windows.Forms.ToolStripMenuItem mnuIssueWireTransfer;
        private System.Windows.Forms.ToolStripMenuItem mnuWireTransferHistory;
        private System.Windows.Forms.ToolStripMenuItem emailConfigurationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicePayableReceived;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoiceReceivableReady;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuWireTransfersEmailConfiguration;
        private Loader loader;
        private System.Windows.Forms.ToolStripMenuItem tsbInterestRates;
        private System.Windows.Forms.ToolStripButton toolLaws;
        private System.Windows.Forms.ToolStripMenuItem toolPurchasing;
        private System.Windows.Forms.ToolStripMenuItem toolPriceManager;
        private System.Windows.Forms.ToolStripMenuItem tsbPurchaseOrders;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutomaticPayments;
    }
}

