using System.Drawing;

namespace CompanyStudio.Purchasing.Quotes
{
    partial class frmQuoteRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuoteRequest));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdoGovernment = new System.Windows.Forms.RadioButton();
            this.rdoCompany = new System.Windows.Forms.RadioButton();
            this.cboGovernment = new System.Windows.Forms.ComboBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboItem = new CompanyStudio.ItemSelectorInput();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAdd = new System.Windows.Forms.ToolStripButton();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdIssueQuote = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.cmdIssueQuote);
            this.groupBox1.Controls.Add(this.txtNotes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rdoGovernment);
            this.groupBox1.Controls.Add(this.rdoCompany);
            this.groupBox1.Controls.Add(this.cboGovernment);
            this.groupBox1.Controls.Add(this.cboCompany);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Request For";
            // 
            // txtNotes
            // 
            this.txtNotes.AcceptsReturn = true;
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.Location = new System.Drawing.Point(6, 86);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(764, 41);
            this.txtNotes.TabIndex = 4;
            this.txtNotes.Validated += new System.EventHandler(this.CreateOrUpdateQuoteRequest);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Notes:";
            // 
            // rdoGovernment
            // 
            this.rdoGovernment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdoGovernment.AutoSize = true;
            this.rdoGovernment.Location = new System.Drawing.Point(237, 47);
            this.rdoGovernment.Name = "rdoGovernment";
            this.rdoGovernment.Size = new System.Drawing.Size(83, 17);
            this.rdoGovernment.TabIndex = 2;
            this.rdoGovernment.TabStop = true;
            this.rdoGovernment.Text = "Government";
            this.rdoGovernment.UseVisualStyleBackColor = true;
            this.rdoGovernment.CheckedChanged += new System.EventHandler(this.RadioCheckedChanged);
            // 
            // rdoCompany
            // 
            this.rdoCompany.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdoCompany.AutoSize = true;
            this.rdoCompany.Location = new System.Drawing.Point(237, 20);
            this.rdoCompany.Name = "rdoCompany";
            this.rdoCompany.Size = new System.Drawing.Size(69, 17);
            this.rdoCompany.TabIndex = 0;
            this.rdoCompany.TabStop = true;
            this.rdoCompany.Text = "Company";
            this.rdoCompany.UseVisualStyleBackColor = true;
            this.rdoCompany.CheckedChanged += new System.EventHandler(this.RadioCheckedChanged);
            // 
            // cboGovernment
            // 
            this.cboGovernment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboGovernment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboGovernment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboGovernment.Enabled = false;
            this.cboGovernment.FormattingEnabled = true;
            this.cboGovernment.Location = new System.Drawing.Point(326, 46);
            this.cboGovernment.Name = "cboGovernment";
            this.cboGovernment.Size = new System.Drawing.Size(198, 21);
            this.cboGovernment.TabIndex = 3;
            this.cboGovernment.SelectedIndexChanged += new System.EventHandler(this.CreateOrUpdateQuoteRequest);
            // 
            // cboCompany
            // 
            this.cboCompany.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCompany.Enabled = false;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(326, 19);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(198, 21);
            this.cboCompany.TabIndex = 1;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.CreateOrUpdateQuoteRequest);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quote Request";
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
            this.groupBox2.Location = new System.Drawing.Point(12, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 267);
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
            this.colQuantity});
            this.dgvItems.Location = new System.Drawing.Point(6, 70);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(764, 191);
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
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAdd,
            this.toolDelete});
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
            this.toolAdd.Size = new System.Drawing.Size(71, 22);
            this.toolAdd.Text = "Add Item";
            this.toolAdd.Click += new System.EventHandler(this.toolAdd_Click);
            // 
            // toolDelete
            // 
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::CompanyStudio.Properties.Resources.delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(96, 22);
            this.toolDelete.Text = "Delete Item(s)";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
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
            // cmdIssueQuote
            // 
            this.cmdIssueQuote.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdIssueQuote.Image = global::CompanyStudio.Properties.Resources.comment_edit;
            this.cmdIssueQuote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdIssueQuote.Location = new System.Drawing.Point(530, 18);
            this.cmdIssueQuote.Name = "cmdIssueQuote";
            this.cmdIssueQuote.Size = new System.Drawing.Size(88, 23);
            this.cmdIssueQuote.TabIndex = 5;
            this.cmdIssueQuote.Text = "Issue Quote";
            this.cmdIssueQuote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdIssueQuote.UseVisualStyleBackColor = true;
            this.cmdIssueQuote.Visible = false;
            this.cmdIssueQuote.Click += new System.EventHandler(this.cmdIssueQuote_Click);
            // 
            // frmQuoteRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQuoteRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quote Request";
            this.Load += new System.EventHandler(this.frmQuoteRequest_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGovernment;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.RadioButton rdoGovernment;
        private System.Windows.Forms.RadioButton rdoCompany;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private ItemSelectorInput cboItem;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolAdd;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.Button cmdIssueQuote;
    }
}