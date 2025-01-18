using System.Drawing;

namespace CompanyStudio.Purchasing.ShippingReceiving
{
    partial class Shipping
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
                    try
                    {
                        image.Dispose();
                    }
                    catch { }
                }
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
            this.lblRailcar = new System.Windows.Forms.Label();
            this.lblTrack = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvLoads = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPOLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndClear = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.cmdAddLoad = new System.Windows.Forms.Button();
            this.cboItem = new CompanyStudio.ItemSelectorInput();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboPOLine = new System.Windows.Forms.ComboBox();
            this.cmdFinalizeLoading = new System.Windows.Forms.Button();
            this.cmdRelease = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRailcar
            // 
            this.lblRailcar.AutoSize = true;
            this.lblRailcar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRailcar.Location = new System.Drawing.Point(0, 0);
            this.lblRailcar.Name = "lblRailcar";
            this.lblRailcar.Size = new System.Drawing.Size(99, 20);
            this.lblRailcar.TabIndex = 0;
            this.lblRailcar.Text = "[Railcar ID]";
            // 
            // lblTrack
            // 
            this.lblTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrack.Location = new System.Drawing.Point(0, 0);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(570, 13);
            this.lblTrack.TabIndex = 1;
            this.lblTrack.Text = "[Track Name]";
            this.lblTrack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPosition
            // 
            this.lblPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPosition.Location = new System.Drawing.Point(0, 13);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(570, 13);
            this.lblPosition.TabIndex = 1;
            this.lblPosition.Text = "Position: [Position]";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Shipping";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Current Car Loads:";
            // 
            // dgvLoads
            // 
            this.dgvLoads.AllowUserToAddRows = false;
            this.dgvLoads.AllowUserToDeleteRows = false;
            this.dgvLoads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colItem,
            this.colQuantity,
            this.colPOLine,
            this.colIndClear});
            this.dgvLoads.Location = new System.Drawing.Point(4, 62);
            this.dgvLoads.Name = "dgvLoads";
            this.dgvLoads.ReadOnly = true;
            this.dgvLoads.RowHeadersVisible = false;
            this.dgvLoads.Size = new System.Drawing.Size(565, 77);
            this.dgvLoads.TabIndex = 0;
            this.dgvLoads.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoads_CellContentClick);
            // 
            // colImage
            // 
            this.colImage.HeaderText = "Img";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            this.colImage.Width = 34;
            // 
            // colItem
            // 
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Qty";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 40;
            // 
            // colPOLine
            // 
            this.colPOLine.HeaderText = "PO Line";
            this.colPOLine.Name = "colPOLine";
            this.colPOLine.ReadOnly = true;
            // 
            // colIndClear
            // 
            this.colIndClear.HeaderText = "Clear";
            this.colIndClear.Name = "colIndClear";
            this.colIndClear.ReadOnly = true;
            this.colIndClear.Text = "Clear";
            this.colIndClear.UseColumnTextForButtonValue = true;
            this.colIndClear.Width = 70;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Item:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuantity.Location = new System.Drawing.Point(57, 211);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(511, 20);
            this.txtQuantity.TabIndex = 3;
            // 
            // cmdAddLoad
            // 
            this.cmdAddLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddLoad.Location = new System.Drawing.Point(488, 237);
            this.cmdAddLoad.Name = "cmdAddLoad";
            this.cmdAddLoad.Size = new System.Drawing.Size(80, 23);
            this.cmdAddLoad.TabIndex = 4;
            this.cmdAddLoad.Text = "Add Load";
            this.cmdAddLoad.UseVisualStyleBackColor = true;
            this.cmdAddLoad.Click += new System.EventHandler(this.cmdAddLoad_Click);
            // 
            // cboItem
            // 
            this.cboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboItem.Location = new System.Drawing.Point(57, 185);
            this.cboItem.Name = "cboItem";
            this.cboItem.SelectedID = null;
            this.cboItem.Size = new System.Drawing.Size(511, 20);
            this.cboItem.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Add Load:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "PO Line:";
            // 
            // cboPOLine
            // 
            this.cboPOLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPOLine.FormattingEnabled = true;
            this.cboPOLine.Location = new System.Drawing.Point(57, 158);
            this.cboPOLine.Name = "cboPOLine";
            this.cboPOLine.Size = new System.Drawing.Size(511, 21);
            this.cboPOLine.TabIndex = 1;
            // 
            // cmdFinalizeLoading
            // 
            this.cmdFinalizeLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFinalizeLoading.Location = new System.Drawing.Point(389, 237);
            this.cmdFinalizeLoading.Name = "cmdFinalizeLoading";
            this.cmdFinalizeLoading.Size = new System.Drawing.Size(93, 23);
            this.cmdFinalizeLoading.TabIndex = 5;
            this.cmdFinalizeLoading.Text = "Finalize Loading";
            this.cmdFinalizeLoading.UseVisualStyleBackColor = true;
            this.cmdFinalizeLoading.Click += new System.EventHandler(this.cmdFinalizeLoading_Click);
            // 
            // cmdRelease
            // 
            this.cmdRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRelease.AutoEllipsis = true;
            this.cmdRelease.Location = new System.Drawing.Point(389, 237);
            this.cmdRelease.Name = "cmdRelease";
            this.cmdRelease.Size = new System.Drawing.Size(179, 23);
            this.cmdRelease.TabIndex = 6;
            this.cmdRelease.Text = "Release";
            this.cmdRelease.UseVisualStyleBackColor = true;
            this.cmdRelease.Visible = false;
            this.cmdRelease.Click += new System.EventHandler(this.cmdRelease_Click);
            // 
            // Shipping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboPOLine);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmdFinalizeLoading);
            this.Controls.Add(this.cmdAddLoad);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboItem);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvLoads);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblRailcar);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblTrack);
            this.Controls.Add(this.cmdRelease);
            this.Name = "Shipping";
            this.Size = new System.Drawing.Size(573, 265);
            this.Load += new System.EventHandler(this.Shipping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRailcar;
        private System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvLoads;
        private System.Windows.Forms.Label label7;
        private ItemSelectorInput cboItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button cmdAddLoad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboPOLine;
        private System.Windows.Forms.Button cmdFinalizeLoading;
        private System.Windows.Forms.Button cmdRelease;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPOLine;
        private System.Windows.Forms.DataGridViewButtonColumn colIndClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
    }
}
