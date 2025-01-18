namespace GovernmentPortal.Purchasing
{
    partial class frmPriceManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPriceManager));
            this.grpCurrentItems = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuAddItem = new System.Windows.Forms.ToolStripButton();
            this.mnuDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMarkupSelected = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cboItem = new GovernmentPortal.ItemSelectorInput();
            this.imageDisposer = new MesaSuite.Common.ImageDisposer(this.components);
            this.grpCurrentItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCurrentItems
            // 
            this.grpCurrentItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCurrentItems.Controls.Add(this.dgvItems);
            this.grpCurrentItems.Controls.Add(this.toolStrip1);
            this.grpCurrentItems.Location = new System.Drawing.Point(12, 38);
            this.grpCurrentItems.Name = "grpCurrentItems";
            this.grpCurrentItems.Size = new System.Drawing.Size(776, 385);
            this.grpCurrentItems.TabIndex = 4;
            this.grpCurrentItems.TabStop = false;
            this.grpCurrentItems.Text = "Current Store Items";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colItem,
            this.colQty,
            this.colCost});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 41);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(770, 341);
            this.dgvItems.TabIndex = 1;
            this.dgvItems.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellValidated);
            this.dgvItems.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvItems_CellValidating);
            // 
            // colImage
            // 
            this.colImage.HeaderText = "";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            this.colImage.Width = 40;
            // 
            // colItem
            // 
            this.colItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            // 
            // colQty
            // 
            this.colQty.HeaderText = "Quantity";
            this.colQty.Name = "colQty";
            this.colQty.Width = 85;
            // 
            // colCost
            // 
            this.colCost.HeaderText = "Base Price";
            this.colCost.Name = "colCost";
            this.colCost.Width = 85;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddItem,
            this.mnuDeleteItem,
            this.toolStripSeparator1,
            this.mnuMarkupSelected});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuAddItem
            // 
            this.mnuAddItem.Image = global::GovernmentPortal.Properties.Resources.add;
            this.mnuAddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAddItem.Name = "mnuAddItem";
            this.mnuAddItem.Size = new System.Drawing.Size(71, 22);
            this.mnuAddItem.Text = "Add Item";
            this.mnuAddItem.Click += new System.EventHandler(this.mnuAddItem_Click);
            // 
            // mnuDeleteItem
            // 
            this.mnuDeleteItem.Image = global::GovernmentPortal.Properties.Resources.delete;
            this.mnuDeleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDeleteItem.Name = "mnuDeleteItem";
            this.mnuDeleteItem.Size = new System.Drawing.Size(132, 22);
            this.mnuDeleteItem.Text = "Delete Selected Items";
            this.mnuDeleteItem.Click += new System.EventHandler(this.mnuDeleteItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuMarkupSelected
            // 
            this.mnuMarkupSelected.Image = global::GovernmentPortal.Properties.Resources.chart_line;
            this.mnuMarkupSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuMarkupSelected.Name = "mnuMarkupSelected";
            this.mnuMarkupSelected.Size = new System.Drawing.Size(106, 22);
            this.mnuMarkupSelected.Text = "Markup Selected";
            this.mnuMarkupSelected.Click += new System.EventHandler(this.mnuMarkupSelected_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Item:";
            // 
            // cboItem
            // 
            this.cboItem.Location = new System.Drawing.Point(48, 12);
            this.cboItem.Name = "cboItem";
            this.cboItem.SelectedID = null;
            this.cboItem.Size = new System.Drawing.Size(740, 20);
            this.cboItem.TabIndex = 5;
            // 
            // imageDisposer
            // 
            this.imageDisposer.Images = ((System.Collections.Generic.List<System.Drawing.Image>)(resources.GetObject("imageDisposer.Images")));
            // 
            // frmPriceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 435);
            this.Controls.Add(this.cboItem);
            this.Controls.Add(this.grpCurrentItems);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPriceManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price Manager";
            this.Load += new System.EventHandler(this.frmPriceManager_Load);
            this.grpCurrentItems.ResumeLayout(false);
            this.grpCurrentItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCurrentItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnuAddItem;
        private System.Windows.Forms.ToolStripButton mnuDeleteItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnuMarkupSelected;
        private System.Windows.Forms.Label label1;
        private ItemSelectorInput cboItem;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private MesaSuite.Common.ImageDisposer imageDisposer;
    }
}