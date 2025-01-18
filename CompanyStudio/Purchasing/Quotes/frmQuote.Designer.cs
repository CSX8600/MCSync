using System.Drawing;

namespace CompanyStudio.Purchasing.Quotes
{
    partial class frmQuote
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
                foreach(Image image in images)
                {
                    image.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuote));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblExpired = new System.Windows.Forms.Label();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRepeatable = new System.Windows.Forms.CheckBox();
            this.rdoGovernment = new System.Windows.Forms.RadioButton();
            this.rdoCompany = new System.Windows.Forms.RadioButton();
            this.cboGovernment = new System.Windows.Forms.ComboBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboItem = new CompanyStudio.ItemSelectorInput();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinimumQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAdd = new System.Windows.Forms.ToolStripButton();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSetUnitByBulk = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblExpired);
            this.groupBox1.Controls.Add(this.dtpExpiration);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkRepeatable);
            this.groupBox1.Controls.Add(this.rdoGovernment);
            this.groupBox1.Controls.Add(this.rdoCompany);
            this.groupBox1.Controls.Add(this.cboGovernment);
            this.groupBox1.Controls.Add(this.cboCompany);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Issued To";
            // 
            // lblExpired
            // 
            this.lblExpired.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblExpired.AutoSize = true;
            this.lblExpired.BackColor = System.Drawing.Color.Yellow;
            this.lblExpired.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpired.ForeColor = System.Drawing.Color.Black;
            this.lblExpired.Location = new System.Drawing.Point(573, 99);
            this.lblExpired.Name = "lblExpired";
            this.lblExpired.Size = new System.Drawing.Size(49, 13);
            this.lblExpired.TabIndex = 11;
            this.lblExpired.Text = "Expired";
            this.lblExpired.Visible = false;
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpExpiration.CustomFormat = "dddd MMMM dd, yyyy hh:mm tt";
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiration.Location = new System.Drawing.Point(301, 96);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(266, 20);
            this.dtpExpiration.TabIndex = 5;
            this.dtpExpiration.ValueChanged += new System.EventHandler(this.CreateOrUpdateQuote);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Expiration Time:";
            // 
            // chkRepeatable
            // 
            this.chkRepeatable.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkRepeatable.AutoSize = true;
            this.chkRepeatable.Location = new System.Drawing.Point(278, 73);
            this.chkRepeatable.Name = "chkRepeatable";
            this.chkRepeatable.Size = new System.Drawing.Size(221, 17);
            this.chkRepeatable.TabIndex = 4;
            this.chkRepeatable.Text = "Allow recipient to use quote multiple times";
            this.chkRepeatable.UseVisualStyleBackColor = true;
            this.chkRepeatable.CheckedChanged += new System.EventHandler(this.CreateOrUpdateQuote);
            // 
            // rdoGovernment
            // 
            this.rdoGovernment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdoGovernment.AutoSize = true;
            this.rdoGovernment.Location = new System.Drawing.Point(212, 47);
            this.rdoGovernment.Name = "rdoGovernment";
            this.rdoGovernment.Size = new System.Drawing.Size(83, 17);
            this.rdoGovernment.TabIndex = 2;
            this.rdoGovernment.TabStop = true;
            this.rdoGovernment.Text = "Government";
            this.rdoGovernment.UseVisualStyleBackColor = true;
            this.rdoGovernment.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // rdoCompany
            // 
            this.rdoCompany.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdoCompany.AutoSize = true;
            this.rdoCompany.Location = new System.Drawing.Point(212, 20);
            this.rdoCompany.Name = "rdoCompany";
            this.rdoCompany.Size = new System.Drawing.Size(69, 17);
            this.rdoCompany.TabIndex = 0;
            this.rdoCompany.TabStop = true;
            this.rdoCompany.Text = "Company";
            this.rdoCompany.UseVisualStyleBackColor = true;
            this.rdoCompany.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // cboGovernment
            // 
            this.cboGovernment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboGovernment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboGovernment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboGovernment.Enabled = false;
            this.cboGovernment.FormattingEnabled = true;
            this.cboGovernment.Location = new System.Drawing.Point(301, 46);
            this.cboGovernment.Name = "cboGovernment";
            this.cboGovernment.Size = new System.Drawing.Size(266, 21);
            this.cboGovernment.TabIndex = 3;
            this.cboGovernment.SelectedIndexChanged += new System.EventHandler(this.CreateOrUpdateQuote);
            // 
            // cboCompany
            // 
            this.cboCompany.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCompany.Enabled = false;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(301, 19);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(266, 21);
            this.cboCompany.TabIndex = 1;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.CreateOrUpdateQuote);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cboItem);
            this.groupBox2.Controls.Add(this.dgvItems);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 269);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Items";
            // 
            // cboItem
            // 
            this.cboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboItem.Location = new System.Drawing.Point(42, 44);
            this.cboItem.Name = "cboItem";
            this.cboItem.SelectedID = null;
            this.cboItem.Size = new System.Drawing.Size(728, 20);
            this.cboItem.TabIndex = 1;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colItem,
            this.colUnitCost,
            this.colMinimumQuantity});
            this.dgvItems.Location = new System.Drawing.Point(6, 70);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(764, 193);
            this.dgvItems.TabIndex = 2;
            this.dgvItems.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvItems_CellValidating);
            this.dgvItems.SelectionChanged += new System.EventHandler(this.dgvItems_SelectionChanged);
            // 
            // colImage
            // 
            this.colImage.HeaderText = "Image";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            this.colImage.Width = 50;
            // 
            // colItem
            // 
            this.colItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            // 
            // colUnitCost
            // 
            this.colUnitCost.HeaderText = "Unit Cost";
            this.colUnitCost.Name = "colUnitCost";
            // 
            // colMinimumQuantity
            // 
            this.colMinimumQuantity.HeaderText = "Minimum Qty";
            this.colMinimumQuantity.Name = "colMinimumQuantity";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAdd,
            this.toolDelete,
            this.toolStripSeparator1,
            this.toolSetUnitByBulk});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolAdd
            // 
            this.toolAdd.Image = global::CompanyStudio.Properties.Resources.add;
            this.toolAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.Size = new System.Drawing.Size(76, 22);
            this.toolAdd.Text = "Add Item";
            this.toolAdd.Click += new System.EventHandler(this.toolAdd_Click);
            // 
            // toolDelete
            // 
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::CompanyStudio.Properties.Resources.delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(100, 22);
            this.toolDelete.Text = "Delete Item(s)";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSetUnitByBulk
            // 
            this.toolSetUnitByBulk.Enabled = false;
            this.toolSetUnitByBulk.Image = global::CompanyStudio.Properties.Resources.calculator;
            this.toolSetUnitByBulk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetUnitByBulk.Name = "toolSetUnitByBulk";
            this.toolSetUnitByBulk.Size = new System.Drawing.Size(137, 22);
            this.toolSetUnitByBulk.Text = "Set Unit Cost by Bulk";
            this.toolSetUnitByBulk.Click += new System.EventHandler(this.toolSetUnitByBulk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Item:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Purchasing Quotation";
            // 
            // frmQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQuote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quote";
            this.Load += new System.EventHandler(this.frmQuote_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoGovernment;
        private System.Windows.Forms.RadioButton rdoCompany;
        private System.Windows.Forms.ComboBox cboGovernment;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.CheckBox chkRepeatable;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ItemSelectorInput cboItem;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolAdd;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolSetUnitByBulk;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinimumQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExpired;
    }
}