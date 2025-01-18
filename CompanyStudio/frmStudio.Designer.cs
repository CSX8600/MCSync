namespace CompanyStudio
{
    partial class frmStudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudio));
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.mnuBanner = new System.Windows.Forms.MenuStrip();
            this.companyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCompanyExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocationExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.storeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRegisters = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStorePriceManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPromotions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStoreConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStoreSalesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.emailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmailExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.employeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmployeeExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.financeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccountExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.purchasingFulfillmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billsOfLadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolQuotes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPurchasingPriceManager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.shippingReceivingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiveBillsOfLadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicePayables = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicingReceivables = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAutomaticPayments = new System.Windows.Forms.ToolStripMenuItem();
            this.emailConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicingEmailPayableReceived = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoicingEmailReceivableReady = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWireTransfers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWireTransferHistoryExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuWireTransferEmailConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.wIndowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStartPage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLightTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDarkTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBlueTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExtender = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.vS2015LightTheme = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.vS2015DarkTheme = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.vS2015BlueTheme = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolCompanyDropDown = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolLocationDropDown = new System.Windows.Forms.ToolStripComboBox();
            this.tmrLocationUpdater = new System.Windows.Forms.Timer(this.components);
            this.loader = new CompanyStudio.Loader();
            this.studioFormExtender = new CompanyStudio.StudioFormExtender(this.components);
            this.tsmiFulfillmentWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanner.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel
            // 
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.DockBottomPortion = 0.15D;
            this.dockPanel.DockLeftPortion = 0.15D;
            this.dockPanel.DockRightPortion = 0.15D;
            this.dockPanel.DockTopPortion = 0.15D;
            this.dockPanel.Location = new System.Drawing.Point(0, 52);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(855, 398);
            this.dockPanel.TabIndex = 0;
            // 
            // mnuBanner
            // 
            this.mnuBanner.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuBanner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companyToolStripMenuItem,
            this.storeToolStripMenuItem,
            this.emailToolStripMenuItem,
            this.employeesToolStripMenuItem,
            this.financeToolStripMenuItem,
            this.wIndowToolStripMenuItem});
            this.mnuBanner.Location = new System.Drawing.Point(0, 0);
            this.mnuBanner.Name = "mnuBanner";
            this.mnuBanner.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.mnuBanner.Size = new System.Drawing.Size(855, 24);
            this.mnuBanner.TabIndex = 2;
            this.mnuBanner.Text = "Banner";
            // 
            // companyToolStripMenuItem
            // 
            this.companyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCompanyExplorer,
            this.mnuLocationExplorer});
            this.companyToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.building;
            this.companyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.companyToolStripMenuItem.Name = "companyToolStripMenuItem";
            this.companyToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.companyToolStripMenuItem.Text = "Company";
            // 
            // mnuCompanyExplorer
            // 
            this.mnuCompanyExplorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnuCompanyExplorer.Image = global::CompanyStudio.Properties.Resources.building;
            this.mnuCompanyExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuCompanyExplorer.Name = "mnuCompanyExplorer";
            this.mnuCompanyExplorer.Size = new System.Drawing.Size(180, 22);
            this.mnuCompanyExplorer.Text = "Company Explorer";
            this.mnuCompanyExplorer.Click += new System.EventHandler(this.mnuCompanyExplorer_Click);
            // 
            // mnuLocationExplorer
            // 
            this.mnuLocationExplorer.Image = global::CompanyStudio.Properties.Resources.world;
            this.mnuLocationExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuLocationExplorer.Name = "mnuLocationExplorer";
            this.mnuLocationExplorer.Size = new System.Drawing.Size(180, 22);
            this.mnuLocationExplorer.Text = "Location Explorer";
            this.mnuLocationExplorer.Click += new System.EventHandler(this.mnuLocationExplorer_Click);
            // 
            // storeToolStripMenuItem
            // 
            this.storeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRegisters,
            this.mnuStorePriceManager,
            this.mnuPromotions,
            this.toolStripMenuItem3,
            this.mnuStoreConfiguration,
            this.mnuStoreSalesReport});
            this.storeToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.basket;
            this.storeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.storeToolStripMenuItem.Name = "storeToolStripMenuItem";
            this.storeToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.storeToolStripMenuItem.Text = "Store";
            this.storeToolStripMenuItem.Visible = false;
            this.storeToolStripMenuItem.DropDownOpening += new System.EventHandler(this.storeToolStripMenuItem_DropDownOpening);
            // 
            // mnuRegisters
            // 
            this.mnuRegisters.Image = global::CompanyStudio.Properties.Resources.cash_register_small;
            this.mnuRegisters.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuRegisters.Name = "mnuRegisters";
            this.mnuRegisters.Size = new System.Drawing.Size(168, 22);
            this.mnuRegisters.Text = "Registers";
            this.mnuRegisters.Click += new System.EventHandler(this.mnuRegisters_Click);
            // 
            // mnuStorePriceManager
            // 
            this.mnuStorePriceManager.Image = global::CompanyStudio.Properties.Resources.money;
            this.mnuStorePriceManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuStorePriceManager.Name = "mnuStorePriceManager";
            this.mnuStorePriceManager.Size = new System.Drawing.Size(168, 22);
            this.mnuStorePriceManager.Text = "Price Manager";
            this.mnuStorePriceManager.Click += new System.EventHandler(this.mnuStorePriceManager_Click);
            // 
            // mnuPromotions
            // 
            this.mnuPromotions.Image = global::CompanyStudio.Properties.Resources.tag_red;
            this.mnuPromotions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuPromotions.Name = "mnuPromotions";
            this.mnuPromotions.Size = new System.Drawing.Size(168, 22);
            this.mnuPromotions.Text = "Promotions";
            this.mnuPromotions.Click += new System.EventHandler(this.mnuPromotions_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 6);
            // 
            // mnuStoreConfiguration
            // 
            this.mnuStoreConfiguration.Image = global::CompanyStudio.Properties.Resources.cog;
            this.mnuStoreConfiguration.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuStoreConfiguration.Name = "mnuStoreConfiguration";
            this.mnuStoreConfiguration.Size = new System.Drawing.Size(168, 22);
            this.mnuStoreConfiguration.Text = "Configuration";
            this.mnuStoreConfiguration.Click += new System.EventHandler(this.mnuStoreConfiguration_Click);
            // 
            // mnuStoreSalesReport
            // 
            this.mnuStoreSalesReport.Image = global::CompanyStudio.Properties.Resources.report;
            this.mnuStoreSalesReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuStoreSalesReport.Name = "mnuStoreSalesReport";
            this.mnuStoreSalesReport.Size = new System.Drawing.Size(168, 22);
            this.mnuStoreSalesReport.Text = "Store Sales Report";
            this.mnuStoreSalesReport.Click += new System.EventHandler(this.mnuStoreSalesReport_Click);
            // 
            // emailToolStripMenuItem
            // 
            this.emailToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEmailExplorer});
            this.emailToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.email;
            this.emailToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.emailToolStripMenuItem.Name = "emailToolStripMenuItem";
            this.emailToolStripMenuItem.Size = new System.Drawing.Size(64, 22);
            this.emailToolStripMenuItem.Text = "Email";
            this.emailToolStripMenuItem.Visible = false;
            // 
            // mnuEmailExplorer
            // 
            this.mnuEmailExplorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnuEmailExplorer.Image = global::CompanyStudio.Properties.Resources.email;
            this.mnuEmailExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuEmailExplorer.Name = "mnuEmailExplorer";
            this.mnuEmailExplorer.Size = new System.Drawing.Size(148, 22);
            this.mnuEmailExplorer.Text = "Email Explorer";
            this.mnuEmailExplorer.Click += new System.EventHandler(this.mnuEmailExplorer_Click);
            // 
            // employeesToolStripMenuItem
            // 
            this.employeesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEmployeeExplorer});
            this.employeesToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.user;
            this.employeesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.employeesToolStripMenuItem.Name = "employeesToolStripMenuItem";
            this.employeesToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.employeesToolStripMenuItem.Text = "Employees";
            this.employeesToolStripMenuItem.Visible = false;
            // 
            // mnuEmployeeExplorer
            // 
            this.mnuEmployeeExplorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnuEmployeeExplorer.Image = global::CompanyStudio.Properties.Resources.user;
            this.mnuEmployeeExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuEmployeeExplorer.Name = "mnuEmployeeExplorer";
            this.mnuEmployeeExplorer.Size = new System.Drawing.Size(171, 22);
            this.mnuEmployeeExplorer.Text = "Employee Explorer";
            this.mnuEmployeeExplorer.Click += new System.EventHandler(this.mnuEmployeeExplorer_Click);
            // 
            // financeToolStripMenuItem
            // 
            this.financeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountsToolStripMenuItem,
            this.purchasingFulfillmentToolStripMenuItem,
            this.invoicingToolStripMenuItem,
            this.mnuWireTransfers});
            this.financeToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.money_dollar;
            this.financeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.financeToolStripMenuItem.Name = "financeToolStripMenuItem";
            this.financeToolStripMenuItem.Size = new System.Drawing.Size(76, 22);
            this.financeToolStripMenuItem.Text = "Finance";
            this.financeToolStripMenuItem.Visible = false;
            this.financeToolStripMenuItem.DropDownOpening += new System.EventHandler(this.financeToolStripMenuItem_DropDownOpening);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAccountExplorer,
            this.mnuCategories});
            this.accountsToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.money;
            this.accountsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // mnuAccountExplorer
            // 
            this.mnuAccountExplorer.Image = global::CompanyStudio.Properties.Resources.book;
            this.mnuAccountExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuAccountExplorer.Name = "mnuAccountExplorer";
            this.mnuAccountExplorer.Size = new System.Drawing.Size(164, 22);
            this.mnuAccountExplorer.Text = "Account Explorer";
            this.mnuAccountExplorer.Click += new System.EventHandler(this.mnuAccountExplorer_Click);
            // 
            // mnuCategories
            // 
            this.mnuCategories.Image = global::CompanyStudio.Properties.Resources.database;
            this.mnuCategories.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuCategories.Name = "mnuCategories";
            this.mnuCategories.Size = new System.Drawing.Size(164, 22);
            this.mnuCategories.Text = "Categories";
            this.mnuCategories.Click += new System.EventHandler(this.mnuCategories_Click);
            // 
            // purchasingFulfillmentToolStripMenuItem
            // 
            this.purchasingFulfillmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchaseOrdersToolStripMenuItem,
            this.billsOfLadingToolStripMenuItem,
            this.toolQuotes,
            this.toolStripMenuItem5,
            this.toolPurchasingPriceManager,
            this.toolStripMenuItem4,
            this.tsmiFulfillmentWizard,
            this.shippingReceivingToolStripMenuItem,
            this.receiveBillsOfLadingToolStripMenuItem});
            this.purchasingFulfillmentToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.cart;
            this.purchasingFulfillmentToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.purchasingFulfillmentToolStripMenuItem.Name = "purchasingFulfillmentToolStripMenuItem";
            this.purchasingFulfillmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.purchasingFulfillmentToolStripMenuItem.Text = "Purchasing && Fulfillment";
            // 
            // purchaseOrdersToolStripMenuItem
            // 
            this.purchaseOrdersToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.cart;
            this.purchaseOrdersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.purchaseOrdersToolStripMenuItem.Name = "purchaseOrdersToolStripMenuItem";
            this.purchaseOrdersToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.purchaseOrdersToolStripMenuItem.Text = "Purchase Orders";
            this.purchaseOrdersToolStripMenuItem.Click += new System.EventHandler(this.purchaseOrdersToolStripMenuItem_Click);
            // 
            // billsOfLadingToolStripMenuItem
            // 
            this.billsOfLadingToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.script;
            this.billsOfLadingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.billsOfLadingToolStripMenuItem.Name = "billsOfLadingToolStripMenuItem";
            this.billsOfLadingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.billsOfLadingToolStripMenuItem.Text = "Bills of Lading";
            this.billsOfLadingToolStripMenuItem.Click += new System.EventHandler(this.billsOfLadingToolStripMenuItem_Click);
            // 
            // toolQuotes
            // 
            this.toolQuotes.Image = global::CompanyStudio.Properties.Resources.comments;
            this.toolQuotes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolQuotes.Name = "toolQuotes";
            this.toolQuotes.Size = new System.Drawing.Size(188, 22);
            this.toolQuotes.Text = "Quotes";
            this.toolQuotes.Click += new System.EventHandler(this.toolQuotes_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(185, 6);
            // 
            // toolPurchasingPriceManager
            // 
            this.toolPurchasingPriceManager.Image = global::CompanyStudio.Properties.Resources.money;
            this.toolPurchasingPriceManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolPurchasingPriceManager.Name = "toolPurchasingPriceManager";
            this.toolPurchasingPriceManager.Size = new System.Drawing.Size(188, 22);
            this.toolPurchasingPriceManager.Text = "Price Manager";
            this.toolPurchasingPriceManager.Click += new System.EventHandler(this.toolPurchasingPriceManager_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(185, 6);
            // 
            // shippingReceivingToolStripMenuItem
            // 
            this.shippingReceivingToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.box;
            this.shippingReceivingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.shippingReceivingToolStripMenuItem.Name = "shippingReceivingToolStripMenuItem";
            this.shippingReceivingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.shippingReceivingToolStripMenuItem.Text = "Shipping && Receiving";
            this.shippingReceivingToolStripMenuItem.Click += new System.EventHandler(this.shippingReceivingToolStripMenuItem_Click);
            // 
            // receiveBillsOfLadingToolStripMenuItem
            // 
            this.receiveBillsOfLadingToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.script_lightning;
            this.receiveBillsOfLadingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.receiveBillsOfLadingToolStripMenuItem.Name = "receiveBillsOfLadingToolStripMenuItem";
            this.receiveBillsOfLadingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.receiveBillsOfLadingToolStripMenuItem.Text = "Accept Bills of Lading";
            this.receiveBillsOfLadingToolStripMenuItem.Click += new System.EventHandler(this.receiveBillsOfLadingToolStripMenuItem_Click);
            // 
            // invoicingToolStripMenuItem
            // 
            this.invoicingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInvoicePayables,
            this.mnuInvoicingReceivables,
            this.toolStripMenuItem1,
            this.tsmiAutomaticPayments,
            this.emailConfigurationToolStripMenuItem});
            this.invoicingToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.page;
            this.invoicingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.invoicingToolStripMenuItem.Name = "invoicingToolStripMenuItem";
            this.invoicingToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.invoicingToolStripMenuItem.Text = "Invoicing";
            // 
            // mnuInvoicePayables
            // 
            this.mnuInvoicePayables.Image = global::CompanyStudio.Properties.Resources.money_delete;
            this.mnuInvoicePayables.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuInvoicePayables.Name = "mnuInvoicePayables";
            this.mnuInvoicePayables.Size = new System.Drawing.Size(185, 22);
            this.mnuInvoicePayables.Text = "Payables";
            this.mnuInvoicePayables.Click += new System.EventHandler(this.mnuInvoicePayables_Click);
            // 
            // mnuInvoicingReceivables
            // 
            this.mnuInvoicingReceivables.Image = global::CompanyStudio.Properties.Resources.money_add;
            this.mnuInvoicingReceivables.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuInvoicingReceivables.Name = "mnuInvoicingReceivables";
            this.mnuInvoicingReceivables.Size = new System.Drawing.Size(185, 22);
            this.mnuInvoicingReceivables.Text = "Receivables";
            this.mnuInvoicingReceivables.Click += new System.EventHandler(this.mnuInvoicingReceivables_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
            // 
            // tsmiAutomaticPayments
            // 
            this.tsmiAutomaticPayments.Image = global::CompanyStudio.Properties.Resources.lightning;
            this.tsmiAutomaticPayments.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAutomaticPayments.Name = "tsmiAutomaticPayments";
            this.tsmiAutomaticPayments.Size = new System.Drawing.Size(185, 22);
            this.tsmiAutomaticPayments.Text = "Automatic Payments";
            this.tsmiAutomaticPayments.Click += new System.EventHandler(this.tsmiAutomaticPayments_Click);
            // 
            // emailConfigurationToolStripMenuItem
            // 
            this.emailConfigurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInvoicingEmailPayableReceived,
            this.mnuInvoicingEmailReceivableReady});
            this.emailConfigurationToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.email;
            this.emailConfigurationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.emailConfigurationToolStripMenuItem.Name = "emailConfigurationToolStripMenuItem";
            this.emailConfigurationToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.emailConfigurationToolStripMenuItem.Text = "Email Configuration";
            // 
            // mnuInvoicingEmailPayableReceived
            // 
            this.mnuInvoicingEmailPayableReceived.Name = "mnuInvoicingEmailPayableReceived";
            this.mnuInvoicingEmailPayableReceived.Size = new System.Drawing.Size(165, 22);
            this.mnuInvoicingEmailPayableReceived.Text = "Payable Received";
            this.mnuInvoicingEmailPayableReceived.Click += new System.EventHandler(this.mnuInvoicingEmailPayableReceived_Click);
            // 
            // mnuInvoicingEmailReceivableReady
            // 
            this.mnuInvoicingEmailReceivableReady.Name = "mnuInvoicingEmailReceivableReady";
            this.mnuInvoicingEmailReceivableReady.Size = new System.Drawing.Size(165, 22);
            this.mnuInvoicingEmailReceivableReady.Text = "Receivable Ready";
            this.mnuInvoicingEmailReceivableReady.Click += new System.EventHandler(this.mnuInvoicingEmailReceivableReady_Click);
            // 
            // mnuWireTransfers
            // 
            this.mnuWireTransfers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWireTransferHistoryExplorer,
            this.toolStripMenuItem2,
            this.mnuWireTransferEmailConfiguration});
            this.mnuWireTransfers.Image = global::CompanyStudio.Properties.Resources.arrow_turn_right;
            this.mnuWireTransfers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuWireTransfers.Name = "mnuWireTransfers";
            this.mnuWireTransfers.Size = new System.Drawing.Size(206, 22);
            this.mnuWireTransfers.Text = "Wire Transfers";
            this.mnuWireTransfers.Visible = false;
            // 
            // mnuWireTransferHistoryExplorer
            // 
            this.mnuWireTransferHistoryExplorer.Image = global::CompanyStudio.Properties.Resources.book_open;
            this.mnuWireTransferHistoryExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuWireTransferHistoryExplorer.Name = "mnuWireTransferHistoryExplorer";
            this.mnuWireTransferHistoryExplorer.Size = new System.Drawing.Size(180, 22);
            this.mnuWireTransferHistoryExplorer.Text = "History Explorer";
            this.mnuWireTransferHistoryExplorer.Click += new System.EventHandler(this.mnuWireTransfersHistoryExplorer_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuWireTransferEmailConfiguration
            // 
            this.mnuWireTransferEmailConfiguration.Image = global::CompanyStudio.Properties.Resources.email;
            this.mnuWireTransferEmailConfiguration.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuWireTransferEmailConfiguration.Name = "mnuWireTransferEmailConfiguration";
            this.mnuWireTransferEmailConfiguration.Size = new System.Drawing.Size(180, 22);
            this.mnuWireTransferEmailConfiguration.Text = "Email Configuration";
            this.mnuWireTransferEmailConfiguration.Click += new System.EventHandler(this.mnuWireTransferEmailConfiguration_Click);
            // 
            // wIndowToolStripMenuItem
            // 
            this.wIndowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStartPage,
            this.mnuThemes});
            this.wIndowToolStripMenuItem.Image = global::CompanyStudio.Properties.Resources.application;
            this.wIndowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.wIndowToolStripMenuItem.Name = "wIndowToolStripMenuItem";
            this.wIndowToolStripMenuItem.Size = new System.Drawing.Size(79, 22);
            this.wIndowToolStripMenuItem.Text = "Window";
            // 
            // tsmiStartPage
            // 
            this.tsmiStartPage.Image = global::CompanyStudio.Properties.Resources.star;
            this.tsmiStartPage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiStartPage.Name = "tsmiStartPage";
            this.tsmiStartPage.Size = new System.Drawing.Size(127, 22);
            this.tsmiStartPage.Text = "Start Page";
            this.tsmiStartPage.Click += new System.EventHandler(this.tsmiStartPage_Click);
            // 
            // mnuThemes
            // 
            this.mnuThemes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLightTheme,
            this.mnuDarkTheme,
            this.mnuBlueTheme});
            this.mnuThemes.Image = global::CompanyStudio.Properties.Resources.paintbrush;
            this.mnuThemes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuThemes.Name = "mnuThemes";
            this.mnuThemes.Size = new System.Drawing.Size(127, 22);
            this.mnuThemes.Text = "Theme";
            // 
            // mnuLightTheme
            // 
            this.mnuLightTheme.Name = "mnuLightTheme";
            this.mnuLightTheme.Size = new System.Drawing.Size(101, 22);
            this.mnuLightTheme.Tag = "light";
            this.mnuLightTheme.Text = "Light";
            this.mnuLightTheme.Click += new System.EventHandler(this.mnuTheme_Click);
            // 
            // mnuDarkTheme
            // 
            this.mnuDarkTheme.Name = "mnuDarkTheme";
            this.mnuDarkTheme.Size = new System.Drawing.Size(101, 22);
            this.mnuDarkTheme.Tag = "dark";
            this.mnuDarkTheme.Text = "Dark";
            this.mnuDarkTheme.Click += new System.EventHandler(this.mnuTheme_Click);
            // 
            // mnuBlueTheme
            // 
            this.mnuBlueTheme.Name = "mnuBlueTheme";
            this.mnuBlueTheme.Size = new System.Drawing.Size(101, 22);
            this.mnuBlueTheme.Tag = "blue";
            this.mnuBlueTheme.Text = "Blue";
            this.mnuBlueTheme.Click += new System.EventHandler(this.mnuTheme_Click);
            // 
            // toolStripExtender
            // 
            this.toolStripExtender.DefaultRenderer = null;
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSave,
            this.toolSaveAll,
            this.toolStripLabel1,
            this.toolCompanyDropDown,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolLocationDropDown});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(855, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolSave
            // 
            this.toolSave.Image = global::CompanyStudio.Properties.Resources.script_save;
            this.toolSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(51, 22);
            this.toolSave.Text = "Save";
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolSaveAll
            // 
            this.toolSaveAll.Image = global::CompanyStudio.Properties.Resources.page_save;
            this.toolSaveAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveAll.Name = "toolSaveAll";
            this.toolSaveAll.Size = new System.Drawing.Size(68, 22);
            this.toolSaveAll.Text = "Save All";
            this.toolSaveAll.Click += new System.EventHandler(this.toolSaveAll_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::CompanyStudio.Properties.Resources.building;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(78, 22);
            this.toolStripLabel1.Text = "Company:";
            // 
            // toolCompanyDropDown
            // 
            this.toolCompanyDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.toolCompanyDropDown.Name = "toolCompanyDropDown";
            this.toolCompanyDropDown.Size = new System.Drawing.Size(121, 25);
            this.toolCompanyDropDown.SelectedIndexChanged += new System.EventHandler(this.toolCompanyDropDown_SelectedIndexChanged);
            this.toolCompanyDropDown.Enter += new System.EventHandler(this.toolCompanyDropDown_Enter);
            this.toolCompanyDropDown.Leave += new System.EventHandler(this.toolCompanyDropDown_Leave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Image = global::CompanyStudio.Properties.Resources.world;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(72, 22);
            this.toolStripLabel2.Text = "Location:";
            // 
            // toolLocationDropDown
            // 
            this.toolLocationDropDown.Name = "toolLocationDropDown";
            this.toolLocationDropDown.Size = new System.Drawing.Size(121, 25);
            this.toolLocationDropDown.SelectedIndexChanged += new System.EventHandler(this.toolLocationDropDown_SelectedIndexChanged);
            this.toolLocationDropDown.Enter += new System.EventHandler(this.toolLocationDropDown_Enter);
            this.toolLocationDropDown.Leave += new System.EventHandler(this.toolLocationDropDown_Leave);
            // 
            // tmrLocationUpdater
            // 
            this.tmrLocationUpdater.Enabled = true;
            this.tmrLocationUpdater.Interval = 1000;
            this.tmrLocationUpdater.Tick += new System.EventHandler(this.tmrLocationUpdater_Tick);
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
            this.loader.Size = new System.Drawing.Size(855, 450);
            this.loader.TabIndex = 7;
            this.loader.Visible = false;
            // 
            // tsmiFulfillmentWizard
            // 
            this.tsmiFulfillmentWizard.Image = global::CompanyStudio.Properties.Resources.wand;
            this.tsmiFulfillmentWizard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiFulfillmentWizard.Name = "tsmiFulfillmentWizard";
            this.tsmiFulfillmentWizard.Size = new System.Drawing.Size(188, 22);
            this.tsmiFulfillmentWizard.Text = "Fulfillment Wizard";
            this.tsmiFulfillmentWizard.Click += new System.EventHandler(this.tsmiFulfillmentWizard_Click);
            // 
            // frmStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 450);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.mnuBanner);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuBanner;
            this.Name = "frmStudio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Studio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStudio_FormClosing);
            this.Load += new System.EventHandler(this.frmStudio_Load);
            this.mnuBanner.ResumeLayout(false);
            this.mnuBanner.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mnuBanner;
        private System.Windows.Forms.ToolStripMenuItem emailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEmailExplorer;
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender toolStripExtender;
        private System.Windows.Forms.ToolStripMenuItem wIndowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuThemes;
        private System.Windows.Forms.ToolStripMenuItem mnuLightTheme;
        private System.Windows.Forms.ToolStripMenuItem mnuDarkTheme;
        private System.Windows.Forms.ToolStripMenuItem mnuBlueTheme;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme;
        private System.Windows.Forms.ToolStripMenuItem companyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCompanyExplorer;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox toolCompanyDropDown;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripButton toolSaveAll;
        private System.Windows.Forms.ToolStripMenuItem employeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEmployeeExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolLocationDropDown;
        private System.Windows.Forms.ToolStripMenuItem mnuLocationExplorer;
        private System.Windows.Forms.Timer tmrLocationUpdater;
        private System.Windows.Forms.ToolStripMenuItem financeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAccountExplorer;
        private System.Windows.Forms.ToolStripMenuItem mnuCategories;
        private System.Windows.Forms.ToolStripMenuItem invoicingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicePayables;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicingReceivables;
        private System.Windows.Forms.ToolStripMenuItem mnuWireTransfers;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem emailConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuWireTransferHistoryExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuWireTransferEmailConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicingEmailPayableReceived;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoicingEmailReceivableReady;
        private Loader loader;
        private StudioFormExtender studioFormExtender;
        private System.Windows.Forms.ToolStripMenuItem storeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRegisters;
        private System.Windows.Forms.ToolStripMenuItem mnuStorePriceManager;
        private System.Windows.Forms.ToolStripMenuItem mnuPromotions;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuStoreConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuStoreSalesReport;
        private System.Windows.Forms.ToolStripMenuItem purchasingFulfillmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem billsOfLadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem receiveBillsOfLadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shippingReceivingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolQuotes;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolPurchasingPriceManager;
        private System.Windows.Forms.ToolStripMenuItem tsmiStartPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutomaticPayments;
        private System.Windows.Forms.ToolStripMenuItem tsmiFulfillmentWizard;
    }
}

