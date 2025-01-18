namespace FleetTracking.Tracks
{
    partial class TrackViewer
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAddTrack = new System.Windows.Forms.ToolStripButton();
            this.toolDeleteTrack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolModify = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPrint = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTrack = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStrategicDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaderStock = new FleetTracking.Loader();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboOwner = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.loaderMain = new FleetTracking.Loader();
            this.dataGridViewStylizer = new FleetTracking.DataGridViewStylizer(this.components);
            this.tsmiPrintBOLs = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddTrack,
            this.toolDeleteTrack,
            this.toolStripSeparator1,
            this.toolModify,
            this.toolStripSeparator2,
            this.toolPrint,
            this.tsmiPrintBOLs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(641, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolAddTrack
            // 
            this.toolAddTrack.Image = global::FleetTracking.Properties.Resources.add;
            this.toolAddTrack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddTrack.Name = "toolAddTrack";
            this.toolAddTrack.Size = new System.Drawing.Size(64, 35);
            this.toolAddTrack.Text = "Add Track";
            this.toolAddTrack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolAddTrack.Click += new System.EventHandler(this.toolAddTrack_Click);
            // 
            // toolDeleteTrack
            // 
            this.toolDeleteTrack.Enabled = false;
            this.toolDeleteTrack.Image = global::FleetTracking.Properties.Resources.delete;
            this.toolDeleteTrack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteTrack.Name = "toolDeleteTrack";
            this.toolDeleteTrack.Size = new System.Drawing.Size(75, 35);
            this.toolDeleteTrack.Text = "Delete Track";
            this.toolDeleteTrack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDeleteTrack.Click += new System.EventHandler(this.toolDeleteTrack_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolModify
            // 
            this.toolModify.Image = global::FleetTracking.Properties.Resources.application_edit;
            this.toolModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolModify.Name = "toolModify";
            this.toolModify.Size = new System.Drawing.Size(49, 35);
            this.toolModify.Text = "Modify";
            this.toolModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolModify.Click += new System.EventHandler(this.toolModify_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolPrint
            // 
            this.toolPrint.Enabled = false;
            this.toolPrint.Image = global::FleetTracking.Properties.Resources.printer;
            this.toolPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrint.Name = "toolPrint";
            this.toolPrint.Size = new System.Drawing.Size(67, 35);
            this.toolPrint.Text = "Print Track";
            this.toolPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolPrint.Click += new System.EventHandler(this.toolPrint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Track:";
            // 
            // cboTrack
            // 
            this.cboTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTrack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTrack.FormattingEnabled = true;
            this.cboTrack.Location = new System.Drawing.Point(60, 41);
            this.cboTrack.Name = "cboTrack";
            this.cboTrack.Size = new System.Drawing.Size(578, 21);
            this.cboTrack.TabIndex = 1;
            this.cboTrack.SelectedIndexChanged += new System.EventHandler(this.cboTrack_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvStock);
            this.groupBox1.Controls.Add(this.loaderStock);
            this.groupBox1.Location = new System.Drawing.Point(6, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 194);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock";
            // 
            // dgvStock
            // 
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colReportingMark,
            this.colType,
            this.colPos,
            this.colDest,
            this.colStrategicDest,
            this.colContents});
            this.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStock.Location = new System.Drawing.Point(3, 16);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.Size = new System.Drawing.Size(626, 175);
            this.dgvStock.TabIndex = 0;
            this.dgvStock.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStock_CellDoubleClick);
            // 
            // colImage
            // 
            this.colImage.HeaderText = "Image";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            // 
            // colReportingMark
            // 
            this.colReportingMark.HeaderText = "Reporting Mark";
            this.colReportingMark.Name = "colReportingMark";
            this.colReportingMark.Width = 140;
            // 
            // colType
            // 
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            // 
            // colPos
            // 
            this.colPos.HeaderText = "Pos";
            this.colPos.Name = "colPos";
            this.colPos.Width = 40;
            // 
            // colDest
            // 
            this.colDest.HeaderText = "Destination";
            this.colDest.Name = "colDest";
            // 
            // colStrategicDest
            // 
            this.colStrategicDest.HeaderText = "Strategic Dest";
            this.colStrategicDest.Name = "colStrategicDest";
            // 
            // colContents
            // 
            this.colContents.HeaderText = "Contents";
            this.colContents.Name = "colContents";
            // 
            // loaderStock
            // 
            this.loaderStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderStock.BackColor = System.Drawing.Color.Transparent;
            this.loaderStock.Location = new System.Drawing.Point(3, 16);
            this.loaderStock.Name = "loaderStock";
            this.loaderStock.Size = new System.Drawing.Size(626, 178);
            this.loaderStock.TabIndex = 1;
            this.loaderStock.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "District:";
            // 
            // txtDistrict
            // 
            this.txtDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDistrict.Location = new System.Drawing.Point(54, 13);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.ReadOnly = true;
            this.txtDistrict.Size = new System.Drawing.Size(569, 20);
            this.txtDistrict.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cboOwner);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmdReset);
            this.groupBox2.Controls.Add(this.cmdSave);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLength);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.txtDistrict);
            this.groupBox2.Location = new System.Drawing.Point(6, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(629, 119);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Track Details";
            // 
            // cboOwner
            // 
            this.cboOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOwner.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboOwner.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOwner.FormattingEnabled = true;
            this.cboOwner.Location = new System.Drawing.Point(54, 39);
            this.cboOwner.Name = "cboOwner";
            this.cboOwner.Size = new System.Drawing.Size(569, 21);
            this.cboOwner.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Owner:";
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReset.Location = new System.Drawing.Point(467, 89);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 5;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(548, 89);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name:";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(54, 91);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(53, 20);
            this.txtLength.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(54, 65);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(569, 20);
            this.txtName.TabIndex = 2;
            // 
            // loaderMain
            // 
            this.loaderMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderMain.BackColor = System.Drawing.Color.Transparent;
            this.loaderMain.Location = new System.Drawing.Point(0, 0);
            this.loaderMain.Name = "loaderMain";
            this.loaderMain.Size = new System.Drawing.Size(641, 392);
            this.loaderMain.TabIndex = 1;
            this.loaderMain.Visible = false;
            // 
            // tsmiPrintBOLs
            // 
            this.tsmiPrintBOLs.Enabled = false;
            this.tsmiPrintBOLs.Image = global::FleetTracking.Properties.Resources.printer;
            this.tsmiPrintBOLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiPrintBOLs.Name = "tsmiPrintBOLs";
            this.tsmiPrintBOLs.Size = new System.Drawing.Size(88, 35);
            this.tsmiPrintBOLs.Text = "Net Print BOLs";
            this.tsmiPrintBOLs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiPrintBOLs.Click += new System.EventHandler(this.tsmiPrintBOLs_Click);
            // 
            // TrackViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboTrack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loaderMain);
            this.Name = "TrackViewer";
            this.Size = new System.Drawing.Size(641, 392);
            this.Load += new System.EventHandler(this.TrackViewer_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolAddTrack;
        private System.Windows.Forms.ToolStripButton toolDeleteTrack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolModify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTrack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDistrict;
        private DataGridViewStylizer dataGridViewStylizer;
        private Loader loaderStock;
        private Loader loaderMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolPrint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStrategicDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContents;
        private System.Windows.Forms.ToolStripButton tsmiPrintBOLs;
    }
}
