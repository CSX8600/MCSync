namespace CompanyStudio.Purchasing.OpenMaintenance
{
    partial class PurchaseOrderLineReadOnly
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblQtyOrdered = new System.Windows.Forms.Label();
            this.txtQtyOrdered = new System.Windows.Forms.TextBox();
            this.lblQtyFulfilled = new System.Windows.Forms.Label();
            this.txtQtyFulfilled = new System.Windows.Forms.TextBox();
            this.lblQtyInTransit = new System.Windows.Forms.Label();
            this.txtQtyInTransit = new System.Windows.Forms.TextBox();
            this.lblQtyRemaining = new System.Windows.Forms.Label();
            this.txtQtyRemaining = new System.Windows.Forms.TextBox();
            this.lnkFulfillmentPlans = new System.Windows.Forms.LinkLabel();
            this.lnkFulfillments = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(3, 16);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(633, 48);
            this.txtDescription.TabIndex = 0;
            // 
            // lblQtyOrdered
            // 
            this.lblQtyOrdered.AutoSize = true;
            this.lblQtyOrdered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyOrdered.Location = new System.Drawing.Point(0, 67);
            this.lblQtyOrdered.Name = "lblQtyOrdered";
            this.lblQtyOrdered.Size = new System.Drawing.Size(75, 13);
            this.lblQtyOrdered.TabIndex = 2;
            this.lblQtyOrdered.Text = "Qty Ordered";
            // 
            // txtQtyOrdered
            // 
            this.txtQtyOrdered.Location = new System.Drawing.Point(3, 83);
            this.txtQtyOrdered.Name = "txtQtyOrdered";
            this.txtQtyOrdered.ReadOnly = true;
            this.txtQtyOrdered.Size = new System.Drawing.Size(154, 20);
            this.txtQtyOrdered.TabIndex = 1;
            // 
            // lblQtyFulfilled
            // 
            this.lblQtyFulfilled.AutoSize = true;
            this.lblQtyFulfilled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyFulfilled.Location = new System.Drawing.Point(160, 67);
            this.lblQtyFulfilled.Name = "lblQtyFulfilled";
            this.lblQtyFulfilled.Size = new System.Drawing.Size(74, 13);
            this.lblQtyFulfilled.TabIndex = 2;
            this.lblQtyFulfilled.Text = "Qty Fulfilled";
            // 
            // txtQtyFulfilled
            // 
            this.txtQtyFulfilled.Location = new System.Drawing.Point(163, 83);
            this.txtQtyFulfilled.Name = "txtQtyFulfilled";
            this.txtQtyFulfilled.ReadOnly = true;
            this.txtQtyFulfilled.Size = new System.Drawing.Size(154, 20);
            this.txtQtyFulfilled.TabIndex = 2;
            // 
            // lblQtyInTransit
            // 
            this.lblQtyInTransit.AutoSize = true;
            this.lblQtyInTransit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyInTransit.Location = new System.Drawing.Point(320, 67);
            this.lblQtyInTransit.Name = "lblQtyInTransit";
            this.lblQtyInTransit.Size = new System.Drawing.Size(84, 13);
            this.lblQtyInTransit.TabIndex = 2;
            this.lblQtyInTransit.Text = "Qty In Transit";
            // 
            // txtQtyInTransit
            // 
            this.txtQtyInTransit.Location = new System.Drawing.Point(323, 83);
            this.txtQtyInTransit.Name = "txtQtyInTransit";
            this.txtQtyInTransit.ReadOnly = true;
            this.txtQtyInTransit.Size = new System.Drawing.Size(154, 20);
            this.txtQtyInTransit.TabIndex = 3;
            // 
            // lblQtyRemaining
            // 
            this.lblQtyRemaining.AutoSize = true;
            this.lblQtyRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyRemaining.Location = new System.Drawing.Point(479, 67);
            this.lblQtyRemaining.Name = "lblQtyRemaining";
            this.lblQtyRemaining.Size = new System.Drawing.Size(89, 13);
            this.lblQtyRemaining.TabIndex = 2;
            this.lblQtyRemaining.Text = "Qty Remaining";
            // 
            // txtQtyRemaining
            // 
            this.txtQtyRemaining.Location = new System.Drawing.Point(482, 83);
            this.txtQtyRemaining.Name = "txtQtyRemaining";
            this.txtQtyRemaining.ReadOnly = true;
            this.txtQtyRemaining.Size = new System.Drawing.Size(154, 20);
            this.txtQtyRemaining.TabIndex = 4;
            // 
            // lnkFulfillmentPlans
            // 
            this.lnkFulfillmentPlans.AutoSize = true;
            this.lnkFulfillmentPlans.Location = new System.Drawing.Point(3, 106);
            this.lnkFulfillmentPlans.Name = "lnkFulfillmentPlans";
            this.lnkFulfillmentPlans.Size = new System.Drawing.Size(97, 13);
            this.lnkFulfillmentPlans.TabIndex = 5;
            this.lnkFulfillmentPlans.TabStop = true;
            this.lnkFulfillmentPlans.Text = "0 Fulfillment Plan(s)";
            this.lnkFulfillmentPlans.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFulfillmentPlans_LinkClicked);
            // 
            // lnkFulfillments
            // 
            this.lnkFulfillments.AutoSize = true;
            this.lnkFulfillments.Location = new System.Drawing.Point(3, 122);
            this.lnkFulfillments.Name = "lnkFulfillments";
            this.lnkFulfillments.Size = new System.Drawing.Size(84, 13);
            this.lnkFulfillments.TabIndex = 6;
            this.lnkFulfillments.TabStop = true;
            this.lnkFulfillments.Text = "View Fulfillments";
            this.lnkFulfillments.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFulfillments_LinkClicked);
            // 
            // PurchaseOrderLineReadOnly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lnkFulfillments);
            this.Controls.Add(this.lnkFulfillmentPlans);
            this.Controls.Add(this.txtQtyRemaining);
            this.Controls.Add(this.txtQtyInTransit);
            this.Controls.Add(this.lblQtyRemaining);
            this.Controls.Add(this.txtQtyFulfilled);
            this.Controls.Add(this.lblQtyInTransit);
            this.Controls.Add(this.txtQtyOrdered);
            this.Controls.Add(this.lblQtyFulfilled);
            this.Controls.Add(this.lblQtyOrdered);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Name = "PurchaseOrderLineReadOnly";
            this.Size = new System.Drawing.Size(637, 141);
            this.Load += new System.EventHandler(this.PurchaseOrderLineReadOnly_Load);
            this.SizeChanged += new System.EventHandler(this.PurchaseOrderLineReadOnly_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblQtyOrdered;
        private System.Windows.Forms.TextBox txtQtyOrdered;
        private System.Windows.Forms.Label lblQtyFulfilled;
        private System.Windows.Forms.TextBox txtQtyFulfilled;
        private System.Windows.Forms.Label lblQtyInTransit;
        private System.Windows.Forms.TextBox txtQtyInTransit;
        private System.Windows.Forms.Label lblQtyRemaining;
        private System.Windows.Forms.TextBox txtQtyRemaining;
        private System.Windows.Forms.LinkLabel lnkFulfillmentPlans;
        private System.Windows.Forms.LinkLabel lnkFulfillments;
    }
}
