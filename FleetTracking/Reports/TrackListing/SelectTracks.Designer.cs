namespace FleetTracking.Reports.TrackListing
{
    partial class SelectTracks
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
            this.dgvTracks = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTrack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistrict = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmdRunReport = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolCheckAll = new System.Windows.Forms.ToolStripButton();
            this.toolUncheckAll = new System.Windows.Forms.ToolStripButton();
            this.loader = new FleetTracking.Loader();
            this.dataGridViewStylizer = new FleetTracking.DataGridViewStylizer(this.components);
            this.chkPrintBOLs = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracks)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTracks
            // 
            this.dgvTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colTrack,
            this.colDistrict});
            this.dgvTracks.Location = new System.Drawing.Point(0, 28);
            this.dgvTracks.Name = "dgvTracks";
            this.dgvTracks.Size = new System.Drawing.Size(743, 286);
            this.dgvTracks.TabIndex = 1;
            this.dgvTracks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTracks_CellClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 25;
            // 
            // colTrack
            // 
            this.colTrack.HeaderText = "Track";
            this.colTrack.Name = "colTrack";
            // 
            // colDistrict
            // 
            this.colDistrict.HeaderText = "District";
            this.colDistrict.Name = "colDistrict";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearch.Location = new System.Drawing.Point(56, 320);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(174, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cmdRunReport
            // 
            this.cmdRunReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRunReport.Location = new System.Drawing.Point(665, 318);
            this.cmdRunReport.Name = "cmdRunReport";
            this.cmdRunReport.Size = new System.Drawing.Size(75, 23);
            this.cmdRunReport.TabIndex = 3;
            this.cmdRunReport.Text = "Run Report";
            this.cmdRunReport.UseVisualStyleBackColor = true;
            this.cmdRunReport.Click += new System.EventHandler(this.cmdRunReport_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(584, 318);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCheckAll,
            this.toolUncheckAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(743, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolCheckAll
            // 
            this.toolCheckAll.Image = global::FleetTracking.Properties.Resources.checkall;
            this.toolCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCheckAll.Name = "toolCheckAll";
            this.toolCheckAll.Size = new System.Drawing.Size(77, 22);
            this.toolCheckAll.Text = "Check All";
            this.toolCheckAll.Click += new System.EventHandler(this.toolCheckAll_Click);
            // 
            // toolUncheckAll
            // 
            this.toolUncheckAll.Image = global::FleetTracking.Properties.Resources.uncheckall;
            this.toolUncheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUncheckAll.Name = "toolUncheckAll";
            this.toolUncheckAll.Size = new System.Drawing.Size(90, 22);
            this.toolUncheckAll.Text = "Uncheck All";
            this.toolUncheckAll.Click += new System.EventHandler(this.toolUncheckAll_Click);
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(0, 0);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(743, 343);
            this.loader.TabIndex = 5;
            this.loader.Visible = false;
            // 
            // chkPrintBOLs
            // 
            this.chkPrintBOLs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPrintBOLs.AutoSize = true;
            this.chkPrintBOLs.Location = new System.Drawing.Point(236, 322);
            this.chkPrintBOLs.Name = "chkPrintBOLs";
            this.chkPrintBOLs.Size = new System.Drawing.Size(177, 17);
            this.chkPrintBOLs.TabIndex = 6;
            this.chkPrintBOLs.Text = "Network Print BOLs on selected";
            this.chkPrintBOLs.UseVisualStyleBackColor = true;
            // 
            // SelectTracks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkPrintBOLs);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdRunReport);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTracks);
            this.Controls.Add(this.loader);
            this.Name = "SelectTracks";
            this.Size = new System.Drawing.Size(743, 343);
            this.Load += new System.EventHandler(this.SelectTracks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracks)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTracks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button cmdRunReport;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrack;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistrict;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolCheckAll;
        private System.Windows.Forms.ToolStripButton toolUncheckAll;
        private DataGridViewStylizer dataGridViewStylizer;
        private Loader loader;
        private System.Windows.Forms.CheckBox chkPrintBOLs;
    }
}
