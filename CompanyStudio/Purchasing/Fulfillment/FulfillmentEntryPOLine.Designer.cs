namespace CompanyStudio.Purchasing.Fulfillment
{
    partial class FulfillmentEntryPOLine
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
            this.lblReportingMark = new System.Windows.Forms.Label();
            this.lnkChangeRailcar = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQtyRemaining = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQtyLoaded = new System.Windows.Forms.TextBox();
            this.txtPurchaseOrderLine = new System.Windows.Forms.TextBox();
            this.lnkSplitToAnotherCar = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblReportingMark
            // 
            this.lblReportingMark.AutoSize = true;
            this.lblReportingMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportingMark.Location = new System.Drawing.Point(3, 0);
            this.lblReportingMark.Name = "lblReportingMark";
            this.lblReportingMark.Size = new System.Drawing.Size(86, 16);
            this.lblReportingMark.TabIndex = 0;
            this.lblReportingMark.Text = "[Railcar ID]";
            // 
            // lnkChangeRailcar
            // 
            this.lnkChangeRailcar.AutoSize = true;
            this.lnkChangeRailcar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkChangeRailcar.Location = new System.Drawing.Point(4, 16);
            this.lnkChangeRailcar.Name = "lnkChangeRailcar";
            this.lnkChangeRailcar.Size = new System.Drawing.Size(58, 9);
            this.lnkChangeRailcar.TabIndex = 0;
            this.lnkChangeRailcar.TabStop = true;
            this.lnkChangeRailcar.Text = "Change Railcar";
            this.lnkChangeRailcar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChangeRailcar_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Purchase Order Line:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Qty Remaining:";
            // 
            // txtQtyRemaining
            // 
            this.txtQtyRemaining.Location = new System.Drawing.Point(88, 63);
            this.txtQtyRemaining.Name = "txtQtyRemaining";
            this.txtQtyRemaining.ReadOnly = true;
            this.txtQtyRemaining.Size = new System.Drawing.Size(57, 20);
            this.txtQtyRemaining.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Qty Loaded:";
            // 
            // txtQtyLoaded
            // 
            this.txtQtyLoaded.Location = new System.Drawing.Point(222, 63);
            this.txtQtyLoaded.Name = "txtQtyLoaded";
            this.txtQtyLoaded.Size = new System.Drawing.Size(57, 20);
            this.txtQtyLoaded.TabIndex = 4;
            this.txtQtyLoaded.TextChanged += new System.EventHandler(this.txtQtyLoaded_TextChanged);
            // 
            // txtPurchaseOrderLine
            // 
            this.txtPurchaseOrderLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurchaseOrderLine.Location = new System.Drawing.Point(116, 22);
            this.txtPurchaseOrderLine.Name = "txtPurchaseOrderLine";
            this.txtPurchaseOrderLine.ReadOnly = true;
            this.txtPurchaseOrderLine.Size = new System.Drawing.Size(359, 20);
            this.txtPurchaseOrderLine.TabIndex = 1;
            // 
            // lnkSplitToAnotherCar
            // 
            this.lnkSplitToAnotherCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lnkSplitToAnotherCar.AutoSize = true;
            this.lnkSplitToAnotherCar.Location = new System.Drawing.Point(210, 45);
            this.lnkSplitToAnotherCar.Name = "lnkSplitToAnotherCar";
            this.lnkSplitToAnotherCar.Size = new System.Drawing.Size(171, 13);
            this.lnkSplitToAnotherCar.TabIndex = 2;
            this.lnkSplitToAnotherCar.TabStop = true;
            this.lnkSplitToAnotherCar.Text = "Split Fulfillment into another Railcar";
            this.lnkSplitToAnotherCar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSplitToAnotherCar_LinkClicked);
            // 
            // FulfillmentEntryPOLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkSplitToAnotherCar);
            this.Controls.Add(this.txtPurchaseOrderLine);
            this.Controls.Add(this.txtQtyLoaded);
            this.Controls.Add(this.txtQtyRemaining);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lnkChangeRailcar);
            this.Controls.Add(this.lblReportingMark);
            this.Name = "FulfillmentEntryPOLine";
            this.Size = new System.Drawing.Size(478, 88);
            this.Load += new System.EventHandler(this.FulfillmentEntryPOLine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReportingMark;
        private System.Windows.Forms.LinkLabel lnkChangeRailcar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQtyRemaining;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQtyLoaded;
        private System.Windows.Forms.TextBox txtPurchaseOrderLine;
        private System.Windows.Forms.LinkLabel lnkSplitToAnotherCar;
    }
}
