using System.Drawing;

namespace CompanyStudio.Purchasing.DraftEntry
{
    partial class frmRailcarSelect
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
                foreach(Image image in _images)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRailcarSelect));
            this.dgvRailcars = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colReportingMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleasedTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeased = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoFilterAll = new System.Windows.Forms.RadioButton();
            this.rdoFilterOwned = new System.Windows.Forms.RadioButton();
            this.rdoFilterLeased = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRailcars)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRailcars
            // 
            this.dgvRailcars.AllowUserToAddRows = false;
            this.dgvRailcars.AllowUserToDeleteRows = false;
            this.dgvRailcars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRailcars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRailcars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colReportingMark,
            this.colModel,
            this.colLocation,
            this.colDestination,
            this.colReleasedTo,
            this.colLeased});
            this.dgvRailcars.Location = new System.Drawing.Point(0, 0);
            this.dgvRailcars.Name = "dgvRailcars";
            this.dgvRailcars.ReadOnly = true;
            this.dgvRailcars.RowHeadersVisible = false;
            this.dgvRailcars.RowHeadersWidth = 62;
            this.dgvRailcars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRailcars.Size = new System.Drawing.Size(676, 164);
            this.dgvRailcars.TabIndex = 0;
            // 
            // colImage
            // 
            this.colImage.HeaderText = "Image";
            this.colImage.MinimumWidth = 8;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            this.colImage.Width = 40;
            // 
            // colReportingMark
            // 
            this.colReportingMark.HeaderText = "Reporting Mark";
            this.colReportingMark.MinimumWidth = 8;
            this.colReportingMark.Name = "colReportingMark";
            this.colReportingMark.ReadOnly = true;
            this.colReportingMark.Width = 150;
            // 
            // colModel
            // 
            this.colModel.HeaderText = "Model";
            this.colModel.MinimumWidth = 8;
            this.colModel.Name = "colModel";
            this.colModel.ReadOnly = true;
            // 
            // colLocation
            // 
            this.colLocation.HeaderText = "Location";
            this.colLocation.MinimumWidth = 8;
            this.colLocation.Name = "colLocation";
            this.colLocation.ReadOnly = true;
            this.colLocation.Width = 135;
            // 
            // colDestination
            // 
            this.colDestination.HeaderText = "Destination";
            this.colDestination.MinimumWidth = 8;
            this.colDestination.Name = "colDestination";
            this.colDestination.ReadOnly = true;
            this.colDestination.Width = 135;
            // 
            // colReleasedTo
            // 
            this.colReleasedTo.HeaderText = "Released To";
            this.colReleasedTo.MinimumWidth = 8;
            this.colReleasedTo.Name = "colReleasedTo";
            this.colReleasedTo.ReadOnly = true;
            this.colReleasedTo.Width = 135;
            // 
            // colLeased
            // 
            this.colLeased.HeaderText = "Leased?";
            this.colLeased.MinimumWidth = 8;
            this.colLeased.Name = "colLeased";
            this.colLeased.ReadOnly = true;
            this.colLeased.Width = 60;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(589, 170);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(508, 170);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter";
            // 
            // rdoFilterAll
            // 
            this.rdoFilterAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoFilterAll.AutoSize = true;
            this.rdoFilterAll.Checked = true;
            this.rdoFilterAll.Location = new System.Drawing.Point(53, 171);
            this.rdoFilterAll.Name = "rdoFilterAll";
            this.rdoFilterAll.Size = new System.Drawing.Size(36, 17);
            this.rdoFilterAll.TabIndex = 3;
            this.rdoFilterAll.TabStop = true;
            this.rdoFilterAll.Text = "All";
            this.rdoFilterAll.UseVisualStyleBackColor = true;
            this.rdoFilterAll.CheckedChanged += new System.EventHandler(this.Filter_CheckedChanged);
            // 
            // rdoFilterOwned
            // 
            this.rdoFilterOwned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoFilterOwned.AutoSize = true;
            this.rdoFilterOwned.Location = new System.Drawing.Point(95, 171);
            this.rdoFilterOwned.Name = "rdoFilterOwned";
            this.rdoFilterOwned.Size = new System.Drawing.Size(59, 17);
            this.rdoFilterOwned.TabIndex = 3;
            this.rdoFilterOwned.Text = "Owned";
            this.rdoFilterOwned.UseVisualStyleBackColor = true;
            this.rdoFilterOwned.CheckedChanged += new System.EventHandler(this.Filter_CheckedChanged);
            // 
            // rdoFilterLeased
            // 
            this.rdoFilterLeased.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoFilterLeased.AutoSize = true;
            this.rdoFilterLeased.Location = new System.Drawing.Point(160, 171);
            this.rdoFilterLeased.Name = "rdoFilterLeased";
            this.rdoFilterLeased.Size = new System.Drawing.Size(60, 17);
            this.rdoFilterLeased.TabIndex = 3;
            this.rdoFilterLeased.Text = "Leased";
            this.rdoFilterLeased.UseVisualStyleBackColor = true;
            this.rdoFilterLeased.CheckedChanged += new System.EventHandler(this.Filter_CheckedChanged);
            // 
            // frmRailcarSelect
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(676, 205);
            this.Controls.Add(this.rdoFilterLeased);
            this.Controls.Add(this.rdoFilterOwned);
            this.Controls.Add(this.rdoFilterAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.dgvRailcars);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRailcarSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Railcar";
            this.Load += new System.EventHandler(this.frmRailcarSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRailcars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRailcars;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoFilterAll;
        private System.Windows.Forms.RadioButton rdoFilterOwned;
        private System.Windows.Forms.RadioButton rdoFilterLeased;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportingMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleasedTo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colLeased;
    }
}