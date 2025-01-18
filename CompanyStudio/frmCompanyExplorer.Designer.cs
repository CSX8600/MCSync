
namespace CompanyStudio
{
    partial class frmCompanyExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyExplorer));
            this.lstCompanies = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colManageEmails = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colManageEmployees = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colManageAccounts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loader = new CompanyStudio.Loader();
            this.imlSmall = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lstCompanies
            // 
            this.lstCompanies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCompanies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colManageEmails,
            this.colManageEmployees,
            this.colManageAccounts});
            this.lstCompanies.FullRowSelect = true;
            this.lstCompanies.HideSelection = false;
            this.lstCompanies.Location = new System.Drawing.Point(0, 0);
            this.lstCompanies.Name = "lstCompanies";
            this.lstCompanies.Size = new System.Drawing.Size(632, 462);
            this.lstCompanies.TabIndex = 0;
            this.lstCompanies.UseCompatibleStateImageBehavior = false;
            this.lstCompanies.View = System.Windows.Forms.View.Details;
            this.lstCompanies.SelectedIndexChanged += new System.EventHandler(this.lstCompanies_SelectedIndexChanged);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 120;
            // 
            // colManageEmails
            // 
            this.colManageEmails.Text = "Manage Emails";
            this.colManageEmails.Width = 120;
            // 
            // colManageEmployees
            // 
            this.colManageEmployees.Text = "Manage Employees";
            this.colManageEmployees.Width = 120;
            // 
            // colManageAccounts
            // 
            this.colManageAccounts.Text = "Manage  Accounts";
            this.colManageAccounts.Width = 120;
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
            this.loader.Size = new System.Drawing.Size(632, 462);
            this.loader.TabIndex = 2;
            // 
            // imlSmall
            // 
            this.imlSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imlSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imlSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmCompanyExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 462);
            this.Controls.Add(this.lstCompanies);
            this.Controls.Add(this.loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCompanyExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Explorer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCompanyExplorer_FormClosed);
            this.Load += new System.EventHandler(this.frmCompanyExplorer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstCompanies;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colManageEmails;
        private System.Windows.Forms.ColumnHeader colManageEmployees;
        private Loader loader;
        private System.Windows.Forms.ImageList imlSmall;
        private System.Windows.Forms.ColumnHeader colManageAccounts;
    }
}