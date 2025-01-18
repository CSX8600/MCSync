namespace CompanyStudio.Invoicing
{
    partial class frmAutomaticPaymentConfigurationAddPayees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomaticPaymentConfigurationAddPayees));
            this.label1 = new System.Windows.Forms.Label();
            this.lstAvailable = new System.Windows.Forms.ListView();
            this.colSourcePayee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCloneable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdRemoveAll = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdAddAll = new System.Windows.Forms.Button();
            this.dgvChosen = new System.Windows.Forms.DataGridView();
            this.colPayee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCloneFrom = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboCloneAllFrom = new System.Windows.Forms.ToolStripComboBox();
            this.cmdApply = new System.Windows.Forms.ToolStripButton();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.loader = new CompanyStudio.Loader();
            this.studioFormExtender = new CompanyStudio.StudioFormExtender(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChosen)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Payees";
            // 
            // lstAvailable
            // 
            this.lstAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSourcePayee,
            this.colCloneable});
            this.lstAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAvailable.FullRowSelect = true;
            this.lstAvailable.HideSelection = false;
            this.lstAvailable.Location = new System.Drawing.Point(0, 0);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.Size = new System.Drawing.Size(317, 232);
            this.lstAvailable.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstAvailable.TabIndex = 0;
            this.lstAvailable.UseCompatibleStateImageBehavior = false;
            this.lstAvailable.View = System.Windows.Forms.View.Details;
            // 
            // colSourcePayee
            // 
            this.colSourcePayee.Text = "Payee";
            this.colSourcePayee.Width = 250;
            // 
            // colCloneable
            // 
            this.colCloneable.Text = "Cloneable";
            // 
            // cmdRemoveAll
            // 
            this.cmdRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdRemoveAll.Image = global::CompanyStudio.Properties.Resources.arrow_left;
            this.cmdRemoveAll.Location = new System.Drawing.Point(3, 50);
            this.cmdRemoveAll.Name = "cmdRemoveAll";
            this.cmdRemoveAll.Size = new System.Drawing.Size(86, 23);
            this.cmdRemoveAll.TabIndex = 0;
            this.cmdRemoveAll.Text = "Remove All";
            this.cmdRemoveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRemoveAll.UseVisualStyleBackColor = true;
            this.cmdRemoveAll.Click += new System.EventHandler(this.cmdRemoveAll_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdRemove.Image = global::CompanyStudio.Properties.Resources.arrow_left;
            this.cmdRemove.Location = new System.Drawing.Point(3, 79);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(86, 23);
            this.cmdRemove.TabIndex = 1;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.Location = new System.Drawing.Point(3, 108);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(86, 23);
            this.cmdAdd.TabIndex = 2;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdAddAll
            // 
            this.cmdAddAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdAddAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddAll.Image")));
            this.cmdAddAll.Location = new System.Drawing.Point(3, 137);
            this.cmdAddAll.Name = "cmdAddAll";
            this.cmdAddAll.Size = new System.Drawing.Size(86, 23);
            this.cmdAddAll.TabIndex = 3;
            this.cmdAddAll.Text = "Add All";
            this.cmdAddAll.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdAddAll.UseVisualStyleBackColor = true;
            this.cmdAddAll.Click += new System.EventHandler(this.cmdAddAll_Click);
            // 
            // dgvChosen
            // 
            this.dgvChosen.AllowUserToAddRows = false;
            this.dgvChosen.AllowUserToDeleteRows = false;
            this.dgvChosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChosen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPayee,
            this.colCloneFrom});
            this.dgvChosen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChosen.Location = new System.Drawing.Point(0, 25);
            this.dgvChosen.Name = "dgvChosen";
            this.dgvChosen.RowHeadersVisible = false;
            this.dgvChosen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChosen.Size = new System.Drawing.Size(460, 207);
            this.dgvChosen.TabIndex = 0;
            // 
            // colPayee
            // 
            this.colPayee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPayee.HeaderText = "Payee";
            this.colPayee.Name = "colPayee";
            this.colPayee.ReadOnly = true;
            // 
            // colCloneFrom
            // 
            this.colCloneFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCloneFrom.HeaderText = "Clone From";
            this.colCloneFrom.Name = "colCloneFrom";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvChosen);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(95, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 232);
            this.panel1.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboCloneAllFrom,
            this.cmdApply});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(89, 22);
            this.toolStripLabel1.Text = "Clone All From:";
            // 
            // cboCloneAllFrom
            // 
            this.cboCloneAllFrom.Name = "cboCloneAllFrom";
            this.cboCloneAllFrom.Size = new System.Drawing.Size(121, 25);
            this.cboCloneAllFrom.Sorted = true;
            // 
            // cmdApply
            // 
            this.cmdApply.Image = global::CompanyStudio.Properties.Resources.accept;
            this.cmdApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(58, 22);
            this.cmdApply.Text = "Apply";
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(813, 270);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(732, 270);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstAvailable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRemoveAll);
            this.splitContainer1.Panel2.Controls.Add(this.cmdAdd);
            this.splitContainer1.Panel2.Controls.Add(this.cmdAddAll);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRemove);
            this.splitContainer1.Size = new System.Drawing.Size(876, 232);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 1;
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(352, 100);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 6;
            this.loader.Visible = false;
            // 
            // frmAutomaticPaymentConfigurationAddPayees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 300);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.loader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutomaticPaymentConfigurationAddPayees";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Payees";
            this.Load += new System.EventHandler(this.frmAutomaticPaymentConfigurationAddPayees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChosen)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstAvailable;
        private System.Windows.Forms.ColumnHeader colSourcePayee;
        private System.Windows.Forms.ColumnHeader colCloneable;
        private System.Windows.Forms.Button cmdRemoveAll;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdAddAll;
        private System.Windows.Forms.DataGridView dgvChosen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboCloneAllFrom;
        private System.Windows.Forms.ToolStripButton cmdApply;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private StudioFormExtender studioFormExtender;
        private Loader loader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayee;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCloneFrom;
    }
}