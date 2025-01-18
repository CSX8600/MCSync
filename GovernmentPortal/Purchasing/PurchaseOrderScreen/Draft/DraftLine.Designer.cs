namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    partial class DraftLine
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
            this.rdoItem = new System.Windows.Forms.RadioButton();
            this.rdoService = new System.Windows.Forms.RadioButton();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpService = new System.Windows.Forms.GroupBox();
            this.txtServiceDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnitCost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLineTotal = new System.Windows.Forms.TextBox();
            this.lblFulfillmentPlanWarning = new System.Windows.Forms.Label();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdFulfillmentPlan = new System.Windows.Forms.Button();
            this.cboItem = new GovernmentPortal.ItemSelectorInput();
            this.grpItem.SuspendLayout();
            this.grpService.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line Type:";
            // 
            // rdoItem
            // 
            this.rdoItem.AutoSize = true;
            this.rdoItem.Checked = true;
            this.rdoItem.Location = new System.Drawing.Point(66, 3);
            this.rdoItem.Name = "rdoItem";
            this.rdoItem.Size = new System.Drawing.Size(45, 17);
            this.rdoItem.TabIndex = 1;
            this.rdoItem.TabStop = true;
            this.rdoItem.Text = "Item";
            this.rdoItem.UseVisualStyleBackColor = true;
            this.rdoItem.CheckedChanged += new System.EventHandler(this.LineTypeRadioCheckedChanged);
            // 
            // rdoService
            // 
            this.rdoService.AutoSize = true;
            this.rdoService.Location = new System.Drawing.Point(117, 3);
            this.rdoService.Name = "rdoService";
            this.rdoService.Size = new System.Drawing.Size(61, 17);
            this.rdoService.TabIndex = 1;
            this.rdoService.Text = "Service";
            this.rdoService.UseVisualStyleBackColor = true;
            // 
            // grpItem
            // 
            this.grpItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpItem.Controls.Add(this.cboItem);
            this.grpItem.Controls.Add(this.txtItemDescription);
            this.grpItem.Controls.Add(this.label3);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Location = new System.Drawing.Point(3, 26);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(448, 107);
            this.grpItem.TabIndex = 2;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "Item Details";
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemDescription.Location = new System.Drawing.Point(6, 53);
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtItemDescription.Size = new System.Drawing.Size(436, 46);
            this.txtItemDescription.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Item Description:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Item:";
            // 
            // grpService
            // 
            this.grpService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpService.Controls.Add(this.txtServiceDescription);
            this.grpService.Controls.Add(this.label4);
            this.grpService.Location = new System.Drawing.Point(3, 26);
            this.grpService.Name = "grpService";
            this.grpService.Size = new System.Drawing.Size(448, 107);
            this.grpService.TabIndex = 3;
            this.grpService.TabStop = false;
            this.grpService.Text = "Service Details";
            this.grpService.Visible = false;
            // 
            // txtServiceDescription
            // 
            this.txtServiceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceDescription.Location = new System.Drawing.Point(6, 30);
            this.txtServiceDescription.Multiline = true;
            this.txtServiceDescription.Name = "txtServiceDescription";
            this.txtServiceDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServiceDescription.Size = new System.Drawing.Size(436, 69);
            this.txtServiceDescription.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Service Description:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtQuantity.Location = new System.Drawing.Point(55, 139);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(53, 20);
            this.txtQuantity.TabIndex = 5;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Unit Cost:";
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtUnitCost.Location = new System.Drawing.Point(173, 139);
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.ReadOnly = true;
            this.txtUnitCost.Size = new System.Drawing.Size(53, 20);
            this.txtUnitCost.TabIndex = 5;
            this.txtUnitCost.Text = "Invalid";
            this.txtUnitCost.TextChanged += new System.EventHandler(this.txtUnitCost_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Line Total:";
            // 
            // txtLineTotal
            // 
            this.txtLineTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLineTotal.Location = new System.Drawing.Point(295, 139);
            this.txtLineTotal.Name = "txtLineTotal";
            this.txtLineTotal.ReadOnly = true;
            this.txtLineTotal.Size = new System.Drawing.Size(53, 20);
            this.txtLineTotal.TabIndex = 5;
            this.txtLineTotal.TextChanged += new System.EventHandler(this.txtLineTotal_TextChanged);
            // 
            // lblFulfillmentPlanWarning
            // 
            this.lblFulfillmentPlanWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFulfillmentPlanWarning.AutoSize = true;
            this.lblFulfillmentPlanWarning.BackColor = System.Drawing.Color.Yellow;
            this.lblFulfillmentPlanWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFulfillmentPlanWarning.ForeColor = System.Drawing.Color.Black;
            this.lblFulfillmentPlanWarning.Location = new System.Drawing.Point(3, 172);
            this.lblFulfillmentPlanWarning.Name = "lblFulfillmentPlanWarning";
            this.lblFulfillmentPlanWarning.Size = new System.Drawing.Size(169, 13);
            this.lblFulfillmentPlanWarning.TabIndex = 6;
            this.lblFulfillmentPlanWarning.Text = "Not on any Fulfillment Plans!";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(376, 167);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 7;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdFulfillmentPlan
            // 
            this.cmdFulfillmentPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFulfillmentPlan.Enabled = false;
            this.cmdFulfillmentPlan.Location = new System.Drawing.Point(281, 167);
            this.cmdFulfillmentPlan.Name = "cmdFulfillmentPlan";
            this.cmdFulfillmentPlan.Size = new System.Drawing.Size(89, 23);
            this.cmdFulfillmentPlan.TabIndex = 7;
            this.cmdFulfillmentPlan.Text = "Fulfillment Plan";
            this.cmdFulfillmentPlan.UseVisualStyleBackColor = true;
            this.cmdFulfillmentPlan.Click += new System.EventHandler(this.cmdFulfillmentPlan_Click);
            // 
            // cboItem
            // 
            this.cboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboItem.Location = new System.Drawing.Point(42, 14);
            this.cboItem.Name = "cboItem";
            this.cboItem.SelectedID = null;
            this.cboItem.Size = new System.Drawing.Size(400, 20);
            this.cboItem.TabIndex = 0;
            this.cboItem.ItemSelected += new System.EventHandler(this.cboItem_ItemSelected);
            // 
            // DraftLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdFulfillmentPlan);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.lblFulfillmentPlanWarning);
            this.Controls.Add(this.txtLineTotal);
            this.Controls.Add(this.txtUnitCost);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.rdoService);
            this.Controls.Add(this.rdoItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpService);
            this.Name = "DraftLine";
            this.Size = new System.Drawing.Size(454, 193);
            this.Load += new System.EventHandler(this.DraftLine_Load);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.grpService.ResumeLayout(false);
            this.grpService.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoItem;
        private System.Windows.Forms.RadioButton rdoService;
        private System.Windows.Forms.GroupBox grpItem;
        private ItemSelectorInput cboItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpService;
        private System.Windows.Forms.TextBox txtServiceDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUnitCost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLineTotal;
        private System.Windows.Forms.Label lblFulfillmentPlanWarning;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdFulfillmentPlan;
    }
}
