namespace CompanyStudio.Purchasing.History
{
    partial class PurchaseOrderLineControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnitCost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLineTotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuantityFulfilled = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantityUnfulfilled = new System.Windows.Forms.TextBox();
            this.lnkFulfillments = new System.Windows.Forms.LinkLabel();
            this.lnkFulfillmentPlans = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(3, 0);
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
            this.txtDescription.Size = new System.Drawing.Size(594, 54);
            this.txtDescription.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(58, 76);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.ReadOnly = true;
            this.txtQuantity.Size = new System.Drawing.Size(60, 20);
            this.txtQuantity.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Unit Cost:";
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Location = new System.Drawing.Point(183, 76);
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.ReadOnly = true;
            this.txtUnitCost.Size = new System.Drawing.Size(60, 20);
            this.txtUnitCost.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Line Total:";
            // 
            // txtLineTotal
            // 
            this.txtLineTotal.Location = new System.Drawing.Point(312, 76);
            this.txtLineTotal.Name = "txtLineTotal";
            this.txtLineTotal.ReadOnly = true;
            this.txtLineTotal.Size = new System.Drawing.Size(60, 20);
            this.txtLineTotal.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Quantity Fulfilled:";
            // 
            // txtQuantityFulfilled
            // 
            this.txtQuantityFulfilled.Location = new System.Drawing.Point(96, 102);
            this.txtQuantityFulfilled.Name = "txtQuantityFulfilled";
            this.txtQuantityFulfilled.ReadOnly = true;
            this.txtQuantityFulfilled.Size = new System.Drawing.Size(60, 20);
            this.txtQuantityFulfilled.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Quantity Unfulfilled:";
            // 
            // txtQuantityUnfulfilled
            // 
            this.txtQuantityUnfulfilled.Location = new System.Drawing.Point(266, 102);
            this.txtQuantityUnfulfilled.Name = "txtQuantityUnfulfilled";
            this.txtQuantityUnfulfilled.ReadOnly = true;
            this.txtQuantityUnfulfilled.Size = new System.Drawing.Size(60, 20);
            this.txtQuantityUnfulfilled.TabIndex = 5;
            // 
            // lnkFulfillments
            // 
            this.lnkFulfillments.AutoSize = true;
            this.lnkFulfillments.Location = new System.Drawing.Point(3, 141);
            this.lnkFulfillments.Name = "lnkFulfillments";
            this.lnkFulfillments.Size = new System.Drawing.Size(84, 13);
            this.lnkFulfillments.TabIndex = 7;
            this.lnkFulfillments.TabStop = true;
            this.lnkFulfillments.Text = "View Fulfillments";
            this.lnkFulfillments.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFulfillments_LinkClicked);
            // 
            // lnkFulfillmentPlans
            // 
            this.lnkFulfillmentPlans.AutoSize = true;
            this.lnkFulfillmentPlans.Location = new System.Drawing.Point(3, 125);
            this.lnkFulfillmentPlans.Name = "lnkFulfillmentPlans";
            this.lnkFulfillmentPlans.Size = new System.Drawing.Size(97, 13);
            this.lnkFulfillmentPlans.TabIndex = 6;
            this.lnkFulfillmentPlans.TabStop = true;
            this.lnkFulfillmentPlans.Text = "0 Fulfillment Plan(s)";
            this.lnkFulfillmentPlans.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFulfillmentPlans_LinkClicked);
            // 
            // PurchaseOrderLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lnkFulfillments);
            this.Controls.Add(this.lnkFulfillmentPlans);
            this.Controls.Add(this.txtLineTotal);
            this.Controls.Add(this.txtUnitCost);
            this.Controls.Add(this.txtQuantityUnfulfilled);
            this.Controls.Add(this.txtQuantityFulfilled);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Name = "PurchaseOrderLineControl";
            this.Size = new System.Drawing.Size(600, 156);
            this.Load += new System.EventHandler(this.PurchaseOrderLineControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUnitCost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLineTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuantityFulfilled;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQuantityUnfulfilled;
        private System.Windows.Forms.LinkLabel lnkFulfillments;
        private System.Windows.Forms.LinkLabel lnkFulfillmentPlans;
    }
}
