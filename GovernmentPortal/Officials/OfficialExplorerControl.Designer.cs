
namespace GovernmentPortal.Officials
{
    partial class OfficialExplorerControl
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

            if (disposing)
            {
                PermissionsManager.OnPermissionChange -= PermissionsManager_OnPermissionChange;
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
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEmails = new System.Windows.Forms.CheckBox();
            this.chkOfficials = new System.Windows.Forms.CheckBox();
            this.chkManageAccounts = new System.Windows.Forms.CheckBox();
            this.chkMintCurrency = new System.Windows.Forms.CheckBox();
            this.chkManageTaxes = new System.Windows.Forms.CheckBox();
            this.chkManageInvoices = new System.Windows.Forms.CheckBox();
            this.chkIssueWireTransfers = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkManageLaws = new System.Windows.Forms.CheckBox();
            this.chkInterest = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkFleetLoadUnload = new System.Windows.Forms.CheckBox();
            this.chkFleetTrainCrew = new System.Windows.Forms.CheckBox();
            this.chkFleetYardmaster = new System.Windows.Forms.CheckBox();
            this.chkFleetLeasing = new System.Windows.Forms.CheckBox();
            this.chkFleetSetup = new System.Windows.Forms.CheckBox();
            this.loader = new GovernmentPortal.Loader();
            this.chkPurchaseOrders = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboUsers
            // 
            this.cboUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsers.FormattingEnabled = true;
            this.cboUsers.Location = new System.Drawing.Point(41, 3);
            this.cboUsers.Name = "cboUsers";
            this.cboUsers.Size = new System.Drawing.Size(404, 21);
            this.cboUsers.TabIndex = 0;
            this.cboUsers.SelectedIndexChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "What permissions does this Official have?";
            // 
            // chkEmails
            // 
            this.chkEmails.AutoSize = true;
            this.chkEmails.Location = new System.Drawing.Point(6, 19);
            this.chkEmails.Name = "chkEmails";
            this.chkEmails.Size = new System.Drawing.Size(98, 17);
            this.chkEmails.TabIndex = 0;
            this.chkEmails.Text = "Manage Emails";
            this.chkEmails.UseVisualStyleBackColor = true;
            this.chkEmails.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkOfficials
            // 
            this.chkOfficials.AutoSize = true;
            this.chkOfficials.Location = new System.Drawing.Point(6, 42);
            this.chkOfficials.Name = "chkOfficials";
            this.chkOfficials.Size = new System.Drawing.Size(105, 17);
            this.chkOfficials.TabIndex = 1;
            this.chkOfficials.Text = "Manage Officials";
            this.chkOfficials.UseVisualStyleBackColor = true;
            this.chkOfficials.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkManageAccounts
            // 
            this.chkManageAccounts.AutoSize = true;
            this.chkManageAccounts.Location = new System.Drawing.Point(6, 65);
            this.chkManageAccounts.Name = "chkManageAccounts";
            this.chkManageAccounts.Size = new System.Drawing.Size(113, 17);
            this.chkManageAccounts.TabIndex = 2;
            this.chkManageAccounts.Text = "Manage Accounts";
            this.chkManageAccounts.UseVisualStyleBackColor = true;
            this.chkManageAccounts.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkMintCurrency
            // 
            this.chkMintCurrency.AutoSize = true;
            this.chkMintCurrency.Location = new System.Drawing.Point(6, 88);
            this.chkMintCurrency.Name = "chkMintCurrency";
            this.chkMintCurrency.Size = new System.Drawing.Size(91, 17);
            this.chkMintCurrency.TabIndex = 3;
            this.chkMintCurrency.Text = "Mint Currency";
            this.chkMintCurrency.UseVisualStyleBackColor = true;
            this.chkMintCurrency.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkManageTaxes
            // 
            this.chkManageTaxes.AutoSize = true;
            this.chkManageTaxes.Location = new System.Drawing.Point(6, 134);
            this.chkManageTaxes.Name = "chkManageTaxes";
            this.chkManageTaxes.Size = new System.Drawing.Size(97, 17);
            this.chkManageTaxes.TabIndex = 5;
            this.chkManageTaxes.Text = "Manage Taxes";
            this.chkManageTaxes.UseVisualStyleBackColor = true;
            this.chkManageTaxes.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkManageInvoices
            // 
            this.chkManageInvoices.AutoSize = true;
            this.chkManageInvoices.Location = new System.Drawing.Point(6, 157);
            this.chkManageInvoices.Name = "chkManageInvoices";
            this.chkManageInvoices.Size = new System.Drawing.Size(108, 17);
            this.chkManageInvoices.TabIndex = 6;
            this.chkManageInvoices.Text = "Manage Invoices";
            this.chkManageInvoices.UseVisualStyleBackColor = true;
            this.chkManageInvoices.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkIssueWireTransfers
            // 
            this.chkIssueWireTransfers.AutoSize = true;
            this.chkIssueWireTransfers.Location = new System.Drawing.Point(6, 180);
            this.chkIssueWireTransfers.Name = "chkIssueWireTransfers";
            this.chkIssueWireTransfers.Size = new System.Drawing.Size(123, 17);
            this.chkIssueWireTransfers.TabIndex = 7;
            this.chkIssueWireTransfers.Text = "Issue Wire Transfers";
            this.chkIssueWireTransfers.UseVisualStyleBackColor = true;
            this.chkIssueWireTransfers.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEmails);
            this.groupBox1.Controls.Add(this.chkPurchaseOrders);
            this.groupBox1.Controls.Add(this.chkManageLaws);
            this.groupBox1.Controls.Add(this.chkIssueWireTransfers);
            this.groupBox1.Controls.Add(this.chkOfficials);
            this.groupBox1.Controls.Add(this.chkManageInvoices);
            this.groupBox1.Controls.Add(this.chkManageAccounts);
            this.groupBox1.Controls.Add(this.chkManageTaxes);
            this.groupBox1.Controls.Add(this.chkInterest);
            this.groupBox1.Controls.Add(this.chkMintCurrency);
            this.groupBox1.Location = new System.Drawing.Point(3, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 254);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // chkManageLaws
            // 
            this.chkManageLaws.AutoSize = true;
            this.chkManageLaws.Location = new System.Drawing.Point(6, 203);
            this.chkManageLaws.Name = "chkManageLaws";
            this.chkManageLaws.Size = new System.Drawing.Size(93, 17);
            this.chkManageLaws.TabIndex = 8;
            this.chkManageLaws.Text = "Manage Laws";
            this.chkManageLaws.UseVisualStyleBackColor = true;
            this.chkManageLaws.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkInterest
            // 
            this.chkInterest.AutoSize = true;
            this.chkInterest.Location = new System.Drawing.Point(6, 111);
            this.chkInterest.Name = "chkInterest";
            this.chkInterest.Size = new System.Drawing.Size(109, 17);
            this.chkInterest.TabIndex = 4;
            this.chkInterest.Text = "Configure Interest";
            this.chkInterest.UseVisualStyleBackColor = true;
            this.chkInterest.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkFleetLoadUnload);
            this.groupBox2.Controls.Add(this.chkFleetTrainCrew);
            this.groupBox2.Controls.Add(this.chkFleetYardmaster);
            this.groupBox2.Controls.Add(this.chkFleetLeasing);
            this.groupBox2.Controls.Add(this.chkFleetSetup);
            this.groupBox2.Location = new System.Drawing.Point(212, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 254);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fleet Tracking";
            // 
            // chkFleetLoadUnload
            // 
            this.chkFleetLoadUnload.AutoSize = true;
            this.chkFleetLoadUnload.Location = new System.Drawing.Point(6, 111);
            this.chkFleetLoadUnload.Name = "chkFleetLoadUnload";
            this.chkFleetLoadUnload.Size = new System.Drawing.Size(125, 17);
            this.chkFleetLoadUnload.TabIndex = 4;
            this.chkFleetLoadUnload.Text = "Railcar Load/Unload";
            this.chkFleetLoadUnload.UseVisualStyleBackColor = true;
            this.chkFleetLoadUnload.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkFleetTrainCrew
            // 
            this.chkFleetTrainCrew.AutoSize = true;
            this.chkFleetTrainCrew.Location = new System.Drawing.Point(6, 88);
            this.chkFleetTrainCrew.Name = "chkFleetTrainCrew";
            this.chkFleetTrainCrew.Size = new System.Drawing.Size(131, 17);
            this.chkFleetTrainCrew.TabIndex = 3;
            this.chkFleetTrainCrew.Text = "Train Crew Operations";
            this.chkFleetTrainCrew.UseVisualStyleBackColor = true;
            this.chkFleetTrainCrew.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkFleetYardmaster
            // 
            this.chkFleetYardmaster.AutoSize = true;
            this.chkFleetYardmaster.Location = new System.Drawing.Point(6, 65);
            this.chkFleetYardmaster.Name = "chkFleetYardmaster";
            this.chkFleetYardmaster.Size = new System.Drawing.Size(133, 17);
            this.chkFleetYardmaster.TabIndex = 2;
            this.chkFleetYardmaster.Text = "Yardmaster Operations";
            this.chkFleetYardmaster.UseVisualStyleBackColor = true;
            this.chkFleetYardmaster.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkFleetLeasing
            // 
            this.chkFleetLeasing.AutoSize = true;
            this.chkFleetLeasing.Location = new System.Drawing.Point(6, 42);
            this.chkFleetLeasing.Name = "chkFleetLeasing";
            this.chkFleetLeasing.Size = new System.Drawing.Size(120, 17);
            this.chkFleetLeasing.TabIndex = 1;
            this.chkFleetLeasing.Text = "Lease Management";
            this.chkFleetLeasing.UseVisualStyleBackColor = true;
            this.chkFleetLeasing.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkFleetSetup
            // 
            this.chkFleetSetup.AutoSize = true;
            this.chkFleetSetup.Location = new System.Drawing.Point(6, 19);
            this.chkFleetSetup.Name = "chkFleetSetup";
            this.chkFleetSetup.Size = new System.Drawing.Size(91, 17);
            this.chkFleetSetup.TabIndex = 0;
            this.chkFleetSetup.Text = "System Setup";
            this.chkFleetSetup.UseVisualStyleBackColor = true;
            this.chkFleetSetup.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(0, 0);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(448, 300);
            this.loader.TabIndex = 0;
            this.loader.Visible = false;
            // 
            // chkPurchaseOrders
            // 
            this.chkPurchaseOrders.AutoSize = true;
            this.chkPurchaseOrders.Location = new System.Drawing.Point(6, 226);
            this.chkPurchaseOrders.Name = "chkPurchaseOrders";
            this.chkPurchaseOrders.Size = new System.Drawing.Size(147, 17);
            this.chkPurchaseOrders.TabIndex = 8;
            this.chkPurchaseOrders.Text = "Manage Purchase Orders";
            this.chkPurchaseOrders.UseVisualStyleBackColor = true;
            this.chkPurchaseOrders.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // OfficialExplorerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboUsers);
            this.Controls.Add(this.loader);
            this.Name = "OfficialExplorerControl";
            this.Size = new System.Drawing.Size(448, 300);
            this.Load += new System.EventHandler(this.OfficialExplorerControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Loader loader;
        private System.Windows.Forms.ComboBox cboUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEmails;
        private System.Windows.Forms.CheckBox chkOfficials;
        private System.Windows.Forms.CheckBox chkManageAccounts;
        private System.Windows.Forms.CheckBox chkMintCurrency;
        private System.Windows.Forms.CheckBox chkManageTaxes;
        private System.Windows.Forms.CheckBox chkManageInvoices;
        private System.Windows.Forms.CheckBox chkIssueWireTransfers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkFleetLoadUnload;
        private System.Windows.Forms.CheckBox chkFleetTrainCrew;
        private System.Windows.Forms.CheckBox chkFleetYardmaster;
        private System.Windows.Forms.CheckBox chkFleetLeasing;
        private System.Windows.Forms.CheckBox chkFleetSetup;
        private System.Windows.Forms.CheckBox chkInterest;
        private System.Windows.Forms.CheckBox chkManageLaws;
        private System.Windows.Forms.CheckBox chkPurchaseOrders;
    }
}
