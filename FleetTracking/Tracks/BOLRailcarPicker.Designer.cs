namespace FleetTracking.Tracks
{
    partial class BOLRailcarPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BOLRailcarPicker));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdRemoveAll = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdAddAll = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvAvailable = new System.Windows.Forms.DataGridView();
            this.colAImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colAReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colACurrentLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colADestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAStrategicDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.colSImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colSReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSCurrentLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSStrategicDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageDisposer = new MesaSuite.Common.ImageDisposer(this.components);
            this.loader = new FleetTracking.Loader();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPrinter = new System.Windows.Forms.ComboBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.cmdAddAll);
            this.splitContainer1.Panel2.Controls.Add(this.cmdAdd);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRemove);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRemoveAll);
            this.splitContainer1.Size = new System.Drawing.Size(747, 295);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvAvailable);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 295);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Cars";
            // 
            // cmdRemoveAll
            // 
            this.cmdRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdRemoveAll.Image = global::FleetTracking.Properties.Resources.arrow_left;
            this.cmdRemoveAll.Location = new System.Drawing.Point(3, 78);
            this.cmdRemoveAll.Name = "cmdRemoveAll";
            this.cmdRemoveAll.Size = new System.Drawing.Size(87, 23);
            this.cmdRemoveAll.TabIndex = 0;
            this.cmdRemoveAll.Text = "Remove All";
            this.cmdRemoveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRemoveAll.UseVisualStyleBackColor = true;
            this.cmdRemoveAll.Click += new System.EventHandler(this.cmdRemoveAll_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdRemove.Image = global::FleetTracking.Properties.Resources.arrow_left;
            this.cmdRemove.Location = new System.Drawing.Point(3, 107);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(87, 23);
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
            this.cmdAdd.Location = new System.Drawing.Point(3, 136);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(87, 23);
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
            this.cmdAddAll.Location = new System.Drawing.Point(3, 165);
            this.cmdAddAll.Name = "cmdAddAll";
            this.cmdAddAll.Size = new System.Drawing.Size(87, 23);
            this.cmdAddAll.TabIndex = 3;
            this.cmdAddAll.Text = "Add All";
            this.cmdAddAll.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdAddAll.UseVisualStyleBackColor = true;
            this.cmdAddAll.Click += new System.EventHandler(this.cmdAddAll_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvSelected);
            this.groupBox2.Location = new System.Drawing.Point(96, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 268);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Cars";
            // 
            // dgvAvailable
            // 
            this.dgvAvailable.AllowUserToAddRows = false;
            this.dgvAvailable.AllowUserToDeleteRows = false;
            this.dgvAvailable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAImage,
            this.colAReportingMark,
            this.colACurrentLocation,
            this.colADestination,
            this.colAStrategicDest,
            this.colAPosition});
            this.dgvAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAvailable.Location = new System.Drawing.Point(3, 16);
            this.dgvAvailable.Name = "dgvAvailable";
            this.dgvAvailable.ReadOnly = true;
            this.dgvAvailable.RowHeadersVisible = false;
            this.dgvAvailable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailable.Size = new System.Drawing.Size(284, 276);
            this.dgvAvailable.TabIndex = 0;
            // 
            // colAImage
            // 
            this.colAImage.HeaderText = "Image";
            this.colAImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colAImage.Name = "colAImage";
            this.colAImage.ReadOnly = true;
            this.colAImage.Width = 40;
            // 
            // colAReportingMark
            // 
            this.colAReportingMark.HeaderText = "Reporting Mark";
            this.colAReportingMark.Name = "colAReportingMark";
            this.colAReportingMark.ReadOnly = true;
            // 
            // colACurrentLocation
            // 
            this.colACurrentLocation.HeaderText = "Current Location";
            this.colACurrentLocation.Name = "colACurrentLocation";
            this.colACurrentLocation.ReadOnly = true;
            // 
            // colADestination
            // 
            this.colADestination.HeaderText = "Destination";
            this.colADestination.Name = "colADestination";
            this.colADestination.ReadOnly = true;
            // 
            // colAStrategicDest
            // 
            this.colAStrategicDest.HeaderText = "Strategic Dest";
            this.colAStrategicDest.Name = "colAStrategicDest";
            this.colAStrategicDest.ReadOnly = true;
            // 
            // colAPosition
            // 
            this.colAPosition.HeaderText = "Position";
            this.colAPosition.Name = "colAPosition";
            this.colAPosition.ReadOnly = true;
            this.colAPosition.Width = 35;
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToDeleteRows = false;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSImage,
            this.colSReportingMark,
            this.colSCurrentLocation,
            this.colSDestination,
            this.colSStrategicDest,
            this.colSPosition});
            this.dgvSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelected.Location = new System.Drawing.Point(3, 16);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.ReadOnly = true;
            this.dgvSelected.RowHeadersVisible = false;
            this.dgvSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelected.Size = new System.Drawing.Size(351, 249);
            this.dgvSelected.TabIndex = 0;
            // 
            // colSImage
            // 
            this.colSImage.HeaderText = "Image";
            this.colSImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colSImage.Name = "colSImage";
            this.colSImage.ReadOnly = true;
            this.colSImage.Width = 40;
            // 
            // colSReportingMark
            // 
            this.colSReportingMark.HeaderText = "Reporting Mark";
            this.colSReportingMark.Name = "colSReportingMark";
            this.colSReportingMark.ReadOnly = true;
            // 
            // colSCurrentLocation
            // 
            this.colSCurrentLocation.HeaderText = "Current Location";
            this.colSCurrentLocation.Name = "colSCurrentLocation";
            this.colSCurrentLocation.ReadOnly = true;
            // 
            // colSDestination
            // 
            this.colSDestination.HeaderText = "Destination";
            this.colSDestination.Name = "colSDestination";
            this.colSDestination.ReadOnly = true;
            // 
            // colSStrategicDest
            // 
            this.colSStrategicDest.HeaderText = "Strategic Dest";
            this.colSStrategicDest.Name = "colSStrategicDest";
            this.colSStrategicDest.ReadOnly = true;
            // 
            // colSPosition
            // 
            this.colSPosition.HeaderText = "Position";
            this.colSPosition.Name = "colSPosition";
            this.colSPosition.ReadOnly = true;
            this.colSPosition.Width = 35;
            // 
            // imageDisposer
            // 
            this.imageDisposer.Images = ((System.Collections.Generic.List<System.Drawing.Image>)(resources.GetObject("imageDisposer.Images")));
            // 
            // loader
            // 
            this.loader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(275, 128);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(196, 101);
            this.loader.TabIndex = 1;
            this.loader.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Printer:";
            // 
            // cboPrinter
            // 
            this.cboPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPrinter.FormattingEnabled = true;
            this.cboPrinter.Location = new System.Drawing.Point(49, 301);
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.Size = new System.Drawing.Size(695, 21);
            this.cboPrinter.Sorted = true;
            this.cboPrinter.TabIndex = 3;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Location = new System.Drawing.Point(669, 328);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(75, 23);
            this.cmdPrint.TabIndex = 4;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(588, 328);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // BOLRailcarPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cboPrinter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.loader);
            this.Name = "BOLRailcarPicker";
            this.Size = new System.Drawing.Size(747, 357);
            this.Load += new System.EventHandler(this.BOLRailcarPicker_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdAddAll;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdRemoveAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvAvailable;
        private System.Windows.Forms.DataGridViewImageColumn colAImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colACurrentLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colADestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAStrategicDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAPosition;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.DataGridViewImageColumn colSImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSCurrentLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSStrategicDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSPosition;
        private MesaSuite.Common.ImageDisposer imageDisposer;
        private Loader loader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPrinter;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdCancel;
    }
}
