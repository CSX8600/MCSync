using System.Drawing;

namespace CompanyStudio.Purchasing.ShippingReceiving
{
    partial class Receiving
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

                foreach (Image image in images)
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receiving));
            this.dgvLoads = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPOLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndClear = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRailcar = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblTrack = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lnkBOL = new System.Windows.Forms.LinkLabel();
            this.lblAcceptBOL = new System.Windows.Forms.Label();
            this.lnkClearAllLoads = new System.Windows.Forms.LinkLabel();
            this.lnkCompleteReceiving = new System.Windows.Forms.LinkLabel();
            this.lnkRelease = new System.Windows.Forms.LinkLabel();
            this.picCompleteReceivingInfo = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblUnloadCompletionWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompleteReceivingInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLoads
            // 
            this.dgvLoads.AllowUserToAddRows = false;
            this.dgvLoads.AllowUserToDeleteRows = false;
            this.dgvLoads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLoads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colItem,
            this.colQuantity,
            this.colPOLine,
            this.colIndClear});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLoads.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoads.Location = new System.Drawing.Point(5, 64);
            this.dgvLoads.Name = "dgvLoads";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoads.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
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
            this.colQuantity.Width = 40;
            // 
            // colPOLine
            // 
            this.colPOLine.HeaderText = "PO Line";
            this.colPOLine.Name = "colPOLine";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Current Car Loads:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Receiving";
            // 
            // lblRailcar
            // 
            this.lblRailcar.AutoSize = true;
            this.lblRailcar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRailcar.Location = new System.Drawing.Point(1, 2);
            this.lblRailcar.Name = "lblRailcar";
            this.lblRailcar.Size = new System.Drawing.Size(99, 20);
            this.lblRailcar.TabIndex = 17;
            this.lblRailcar.Text = "[Railcar ID]";
            // 
            // lblPosition
            // 
            this.lblPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPosition.Location = new System.Drawing.Point(1, 15);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(570, 13);
            this.lblPosition.TabIndex = 18;
            this.lblPosition.Text = "Position: [Position]";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTrack
            // 
            this.lblTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrack.Location = new System.Drawing.Point(1, 2);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(570, 13);
            this.lblTrack.TabIndex = 19;
            this.lblTrack.Text = "[Track Name]";
            this.lblTrack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Receiving Operations:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Bill of Lading:";
            // 
            // lnkBOL
            // 
            this.lnkBOL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkBOL.AutoSize = true;
            this.lnkBOL.Location = new System.Drawing.Point(83, 162);
            this.lnkBOL.Name = "lnkBOL";
            this.lnkBOL.Size = new System.Drawing.Size(48, 13);
            this.lnkBOL.TabIndex = 1;
            this.lnkBOL.TabStop = true;
            this.lnkBOL.Text = "[BOL ID]";
            this.lnkBOL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBOL_LinkClicked);
            this.lnkBOL.SizeChanged += new System.EventHandler(this.lnkBOL_SizeChanged);
            // 
            // lblAcceptBOL
            // 
            this.lblAcceptBOL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAcceptBOL.AutoSize = true;
            this.lblAcceptBOL.BackColor = System.Drawing.Color.Yellow;
            this.lblAcceptBOL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAcceptBOL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcceptBOL.ForeColor = System.Drawing.Color.Black;
            this.lblAcceptBOL.Location = new System.Drawing.Point(137, 162);
            this.lblAcceptBOL.Name = "lblAcceptBOL";
            this.lblAcceptBOL.Size = new System.Drawing.Size(183, 13);
            this.lblAcceptBOL.TabIndex = 26;
            this.lblAcceptBOL.Text = "Not Accepted! Click to accept.";
            this.lblAcceptBOL.Visible = false;
            this.lblAcceptBOL.Click += new System.EventHandler(this.lblAcceptBOL_Click);
            // 
            // lnkClearAllLoads
            // 
            this.lnkClearAllLoads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkClearAllLoads.AutoSize = true;
            this.lnkClearAllLoads.Image = global::CompanyStudio.Properties.Resources.lorry_flatbed;
            this.lnkClearAllLoads.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkClearAllLoads.Location = new System.Drawing.Point(7, 175);
            this.lnkClearAllLoads.Name = "lnkClearAllLoads";
            this.lnkClearAllLoads.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lnkClearAllLoads.Size = new System.Drawing.Size(129, 13);
            this.lnkClearAllLoads.TabIndex = 2;
            this.lnkClearAllLoads.TabStop = true;
            this.lnkClearAllLoads.Text = "Clear All Railcar Loads";
            this.lnkClearAllLoads.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClearAllLoads_LinkClicked);
            // 
            // lnkCompleteReceiving
            // 
            this.lnkCompleteReceiving.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkCompleteReceiving.AutoSize = true;
            this.lnkCompleteReceiving.Image = global::CompanyStudio.Properties.Resources.accept;
            this.lnkCompleteReceiving.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkCompleteReceiving.Location = new System.Drawing.Point(7, 188);
            this.lnkCompleteReceiving.Name = "lnkCompleteReceiving";
            this.lnkCompleteReceiving.Padding = new System.Windows.Forms.Padding(16, 2, 0, 1);
            this.lnkCompleteReceiving.Size = new System.Drawing.Size(153, 16);
            this.lnkCompleteReceiving.TabIndex = 3;
            this.lnkCompleteReceiving.TabStop = true;
            this.lnkCompleteReceiving.Text = "Complete receiving process";
            this.lnkCompleteReceiving.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCompleteReceiving_LinkClicked);
            // 
            // lnkRelease
            // 
            this.lnkRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkRelease.AutoSize = true;
            this.lnkRelease.Image = global::CompanyStudio.Properties.Resources.key_go;
            this.lnkRelease.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkRelease.Location = new System.Drawing.Point(7, 204);
            this.lnkRelease.Name = "lnkRelease";
            this.lnkRelease.Padding = new System.Windows.Forms.Padding(16, 2, 0, 1);
            this.lnkRelease.Size = new System.Drawing.Size(98, 16);
            this.lnkRelease.TabIndex = 4;
            this.lnkRelease.TabStop = true;
            this.lnkRelease.Text = "Release to [xyz]";
            this.lnkRelease.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRelease_LinkClicked);
            // 
            // picCompleteReceivingInfo
            // 
            this.picCompleteReceivingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picCompleteReceivingInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCompleteReceivingInfo.Image = global::CompanyStudio.Properties.Resources.information;
            this.picCompleteReceivingInfo.Location = new System.Drawing.Point(159, 188);
            this.picCompleteReceivingInfo.Name = "picCompleteReceivingInfo";
            this.picCompleteReceivingInfo.Size = new System.Drawing.Size(16, 16);
            this.picCompleteReceivingInfo.TabIndex = 29;
            this.picCompleteReceivingInfo.TabStop = false;
            this.toolTip.SetToolTip(this.picCompleteReceivingInfo, resources.GetString("picCompleteReceivingInfo.ToolTip"));
            this.picCompleteReceivingInfo.Click += new System.EventHandler(this.picCompleteReceivingInfo_Click);
            // 
            // lblUnloadCompletionWarning
            // 
            this.lblUnloadCompletionWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnloadCompletionWarning.AutoEllipsis = true;
            this.lblUnloadCompletionWarning.BackColor = System.Drawing.Color.Yellow;
            this.lblUnloadCompletionWarning.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblUnloadCompletionWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnloadCompletionWarning.ForeColor = System.Drawing.Color.Black;
            this.lblUnloadCompletionWarning.Location = new System.Drawing.Point(126, 48);
            this.lblUnloadCompletionWarning.Name = "lblUnloadCompletionWarning";
            this.lblUnloadCompletionWarning.Size = new System.Drawing.Size(442, 13);
            this.lblUnloadCompletionWarning.TabIndex = 26;
            this.lblUnloadCompletionWarning.Text = "Marking this car as unloaded will automatically send it to its final destination " +
    "per the Fulfillment Plan and release it to the carrier.";
            this.toolTip.SetToolTip(this.lblUnloadCompletionWarning, "Marking this car as unloaded will automatically send it to its final destination " +
        "per the Fulfillment Plan and release it to the carrier.");
            this.lblUnloadCompletionWarning.Visible = false;
            // 
            // Receiving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.picCompleteReceivingInfo);
            this.Controls.Add(this.lnkRelease);
            this.Controls.Add(this.lnkCompleteReceiving);
            this.Controls.Add(this.lnkClearAllLoads);
            this.Controls.Add(this.lblUnloadCompletionWarning);
            this.Controls.Add(this.lblAcceptBOL);
            this.Controls.Add(this.lnkBOL);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvLoads);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblRailcar);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblTrack);
            this.Name = "Receiving";
            this.Size = new System.Drawing.Size(571, 222);
            this.Load += new System.EventHandler(this.Receiving_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompleteReceivingInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvLoads;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRailcar;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lnkBOL;
        private System.Windows.Forms.Label lblAcceptBOL;
        private System.Windows.Forms.LinkLabel lnkClearAllLoads;
        private System.Windows.Forms.LinkLabel lnkCompleteReceiving;
        private System.Windows.Forms.LinkLabel lnkRelease;
        private System.Windows.Forms.PictureBox picCompleteReceivingInfo;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPOLine;
        private System.Windows.Forms.DataGridViewButtonColumn colIndClear;
        private System.Windows.Forms.Label lblUnloadCompletionWarning;
    }
}
