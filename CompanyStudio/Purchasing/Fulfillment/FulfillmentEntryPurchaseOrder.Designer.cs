namespace CompanyStudio.Purchasing.Fulfillment
{
    partial class FulfillmentEntryPurchaseOrder
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
            this.lblPOID = new System.Windows.Forms.Label();
            this.pnlUnfulfilledLines = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblPOID
            // 
            this.lblPOID.AutoSize = true;
            this.lblPOID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOID.Location = new System.Drawing.Point(0, 0);
            this.lblPOID.Name = "lblPOID";
            this.lblPOID.Size = new System.Drawing.Size(144, 16);
            this.lblPOID.TabIndex = 0;
            this.lblPOID.Text = "[Purchase Order ID]";
            // 
            // pnlUnfulfilledLines
            // 
            this.pnlUnfulfilledLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUnfulfilledLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUnfulfilledLines.Location = new System.Drawing.Point(3, 19);
            this.pnlUnfulfilledLines.Name = "pnlUnfulfilledLines";
            this.pnlUnfulfilledLines.Size = new System.Drawing.Size(499, 9);
            this.pnlUnfulfilledLines.TabIndex = 1;
            // 
            // FulfillmentEntryPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnlUnfulfilledLines);
            this.Controls.Add(this.lblPOID);
            this.Name = "FulfillmentEntryPurchaseOrder";
            this.Size = new System.Drawing.Size(505, 32);
            this.Load += new System.EventHandler(this.FulfillmentEntryPurchaseOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPOID;
        private System.Windows.Forms.Panel pnlUnfulfilledLines;
    }
}
