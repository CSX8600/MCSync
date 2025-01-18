namespace CompanyStudio.CompanyForms
{
    partial class LocationEmployees
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
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.loader = new CompanyStudio.Loader();
            this.colLocationEmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmployee = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colManageInvoices = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colManagePrices = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colManageRegisters = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colManageInventory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colManagePurchaseOrders = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLocationEmployeeID,
            this.colEmployee,
            this.colManageInvoices,
            this.colManagePrices,
            this.colManageRegisters,
            this.colManageInventory,
            this.colManagePurchaseOrders});
            this.dgvEmployees.Location = new System.Drawing.Point(0, 0);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.RowHeadersWidth = 62;
            this.dgvEmployees.Size = new System.Drawing.Size(649, 248);
            this.dgvEmployees.TabIndex = 0;
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
            this.loader.Size = new System.Drawing.Size(649, 248);
            this.loader.TabIndex = 1;
            // 
            // colLocationEmployeeID
            // 
            this.colLocationEmployeeID.HeaderText = "";
            this.colLocationEmployeeID.MinimumWidth = 8;
            this.colLocationEmployeeID.Name = "colLocationEmployeeID";
            this.colLocationEmployeeID.Visible = false;
            this.colLocationEmployeeID.Width = 150;
            // 
            // colEmployee
            // 
            this.colEmployee.HeaderText = "Employee";
            this.colEmployee.MinimumWidth = 8;
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.Width = 150;
            // 
            // colManageInvoices
            // 
            this.colManageInvoices.HeaderText = "Manage Invoices";
            this.colManageInvoices.MinimumWidth = 8;
            this.colManageInvoices.Name = "colManageInvoices";
            // 
            // colManagePrices
            // 
            this.colManagePrices.HeaderText = "Manage Prices";
            this.colManagePrices.Name = "colManagePrices";
            // 
            // colManageRegisters
            // 
            this.colManageRegisters.HeaderText = "Manage Registers";
            this.colManageRegisters.Name = "colManageRegisters";
            // 
            // colManageInventory
            // 
            this.colManageInventory.HeaderText = "ManageInventory";
            this.colManageInventory.Name = "colManageInventory";
            // 
            // colManagePurchaseOrders
            // 
            this.colManagePurchaseOrders.HeaderText = "Manage Purchase Orders";
            this.colManagePurchaseOrders.Name = "colManagePurchaseOrders";
            this.colManagePurchaseOrders.Width = 150;
            // 
            // LocationEmployees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.loader);
            this.Name = "LocationEmployees";
            this.Size = new System.Drawing.Size(649, 248);
            this.Load += new System.EventHandler(this.LocationEmployees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmployees;
        private Loader loader;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocationEmployeeID;
        private System.Windows.Forms.DataGridViewComboBoxColumn colEmployee;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colManageInvoices;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colManagePrices;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colManageRegisters;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colManageInventory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colManagePurchaseOrders;
    }
}
