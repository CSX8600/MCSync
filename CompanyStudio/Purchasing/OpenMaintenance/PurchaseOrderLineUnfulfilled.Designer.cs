namespace CompanyStudio.Purchasing.OpenMaintenance
{
    partial class PurchaseOrderLineUnfulfilled
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtServiceItem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnfulfilled = new System.Windows.Forms.TextBox();
            this.txtExpectedRailcars = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUnitCost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLineTotal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Expected Railcars:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Service/Item:";
            // 
            // txtServiceItem
            // 
            this.txtServiceItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceItem.Location = new System.Drawing.Point(6, 42);
            this.txtServiceItem.Multiline = true;
            this.txtServiceItem.Name = "txtServiceItem";
            this.txtServiceItem.ReadOnly = true;
            this.txtServiceItem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServiceItem.Size = new System.Drawing.Size(530, 47);
            this.txtServiceItem.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Unfulfilled Quantity:";
            // 
            // txtUnfulfilled
            // 
            this.txtUnfulfilled.Location = new System.Drawing.Point(128, 121);
            this.txtUnfulfilled.Name = "txtUnfulfilled";
            this.txtUnfulfilled.ReadOnly = true;
            this.txtUnfulfilled.Size = new System.Drawing.Size(58, 20);
            this.txtUnfulfilled.TabIndex = 5;
            // 
            // txtExpectedRailcars
            // 
            this.txtExpectedRailcars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpectedRailcars.Location = new System.Drawing.Point(105, 3);
            this.txtExpectedRailcars.Name = "txtExpectedRailcars";
            this.txtExpectedRailcars.ReadOnly = true;
            this.txtExpectedRailcars.Size = new System.Drawing.Size(431, 20);
            this.txtExpectedRailcars.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Order Qty:";
            // 
            // txtOrderQty
            // 
            this.txtOrderQty.Location = new System.Drawing.Point(64, 95);
            this.txtOrderQty.Name = "txtOrderQty";
            this.txtOrderQty.ReadOnly = true;
            this.txtOrderQty.Size = new System.Drawing.Size(58, 20);
            this.txtOrderQty.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(128, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Unit Cost:";
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Location = new System.Drawing.Point(187, 95);
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.ReadOnly = true;
            this.txtUnitCost.Size = new System.Drawing.Size(58, 20);
            this.txtUnitCost.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(251, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Line Total:";
            // 
            // txtLineTotal
            // 
            this.txtLineTotal.Location = new System.Drawing.Point(314, 95);
            this.txtLineTotal.Name = "txtLineTotal";
            this.txtLineTotal.ReadOnly = true;
            this.txtLineTotal.Size = new System.Drawing.Size(58, 20);
            this.txtLineTotal.TabIndex = 4;
            // 
            // PurchaseOrderLineUnfulfilled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtExpectedRailcars);
            this.Controls.Add(this.txtLineTotal);
            this.Controls.Add(this.txtUnitCost);
            this.Controls.Add(this.txtOrderQty);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUnfulfilled);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtServiceItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PurchaseOrderLineUnfulfilled";
            this.Size = new System.Drawing.Size(539, 145);
            this.Load += new System.EventHandler(this.PurchaseOrderLineUnfulfilled_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServiceItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUnfulfilled;
        private System.Windows.Forms.TextBox txtExpectedRailcars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrderQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUnitCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLineTotal;
    }
}
