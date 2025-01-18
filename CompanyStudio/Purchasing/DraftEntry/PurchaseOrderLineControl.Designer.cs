namespace CompanyStudio.Purchasing.DraftEntry
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
            this.rdoItem = new System.Windows.Forms.RadioButton();
            this.rdoService = new System.Windows.Forms.RadioButton();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.cboItem = new CompanyStudio.ItemSelectorInput();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlService = new System.Windows.Forms.Panel();
            this.txtServiceDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnitCost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLineCost = new System.Windows.Forms.TextBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.lblFulfillmentPlanWarning = new System.Windows.Forms.Label();
            this.lnkRequestQuote = new System.Windows.Forms.LinkLabel();
            this.pnlItem.SuspendLayout();
            this.pnlService.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line Type:";
            // 
            // rdoItem
            // 
            this.rdoItem.AutoSize = true;
            this.rdoItem.Location = new System.Drawing.Point(63, 3);
            this.rdoItem.Name = "rdoItem";
            this.rdoItem.Size = new System.Drawing.Size(45, 17);
            this.rdoItem.TabIndex = 0;
            this.rdoItem.TabStop = true;
            this.rdoItem.Text = "Item";
            this.rdoItem.UseVisualStyleBackColor = true;
            this.rdoItem.CheckedChanged += new System.EventHandler(this.LineTypeCheckedChange);
            // 
            // rdoService
            // 
            this.rdoService.AutoSize = true;
            this.rdoService.Location = new System.Drawing.Point(114, 3);
            this.rdoService.Name = "rdoService";
            this.rdoService.Size = new System.Drawing.Size(61, 17);
            this.rdoService.TabIndex = 1;
            this.rdoService.TabStop = true;
            this.rdoService.Text = "Service";
            this.rdoService.UseVisualStyleBackColor = true;
            this.rdoService.CheckedChanged += new System.EventHandler(this.LineTypeCheckedChange);
            // 
            // pnlItem
            // 
            this.pnlItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlItem.Controls.Add(this.cboItem);
            this.pnlItem.Controls.Add(this.txtItemDescription);
            this.pnlItem.Controls.Add(this.label3);
            this.pnlItem.Controls.Add(this.label2);
            this.pnlItem.Location = new System.Drawing.Point(0, 21);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(496, 93);
            this.pnlItem.TabIndex = 2;
            this.pnlItem.Visible = false;
            // 
            // cboItem
            // 
            this.cboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboItem.Location = new System.Drawing.Point(39, 5);
            this.cboItem.Name = "cboItem";
            this.cboItem.SelectedID = null;
            this.cboItem.Size = new System.Drawing.Size(454, 20);
            this.cboItem.TabIndex = 0;
            this.cboItem.ItemSelected += new System.EventHandler(this.cboItem_ItemSelected);
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemDescription.Location = new System.Drawing.Point(6, 44);
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtItemDescription.Size = new System.Drawing.Size(487, 46);
            this.txtItemDescription.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Item Description:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Item:";
            // 
            // pnlService
            // 
            this.pnlService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlService.Controls.Add(this.txtServiceDescription);
            this.pnlService.Controls.Add(this.label4);
            this.pnlService.Location = new System.Drawing.Point(0, 21);
            this.pnlService.Name = "pnlService";
            this.pnlService.Size = new System.Drawing.Size(496, 93);
            this.pnlService.TabIndex = 3;
            this.pnlService.Visible = false;
            // 
            // txtServiceDescription
            // 
            this.txtServiceDescription.AcceptsReturn = true;
            this.txtServiceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceDescription.Location = new System.Drawing.Point(3, 21);
            this.txtServiceDescription.Multiline = true;
            this.txtServiceDescription.Name = "txtServiceDescription";
            this.txtServiceDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServiceDescription.Size = new System.Drawing.Size(490, 69);
            this.txtServiceDescription.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Service Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(55, 120);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(67, 20);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Unit Cost:";
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Location = new System.Drawing.Point(185, 120);
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.ReadOnly = true;
            this.txtUnitCost.Size = new System.Drawing.Size(67, 20);
            this.txtUnitCost.TabIndex = 4;
            this.txtUnitCost.TextChanged += new System.EventHandler(this.txtUnitCost_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(258, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Line Cost:";
            // 
            // txtLineCost
            // 
            this.txtLineCost.Location = new System.Drawing.Point(318, 120);
            this.txtLineCost.Name = "txtLineCost";
            this.txtLineCost.ReadOnly = true;
            this.txtLineCost.Size = new System.Drawing.Size(67, 20);
            this.txtLineCost.TabIndex = 5;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(418, 140);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 7;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // lblFulfillmentPlanWarning
            // 
            this.lblFulfillmentPlanWarning.AutoSize = true;
            this.lblFulfillmentPlanWarning.BackColor = System.Drawing.Color.Yellow;
            this.lblFulfillmentPlanWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFulfillmentPlanWarning.ForeColor = System.Drawing.Color.Black;
            this.lblFulfillmentPlanWarning.Location = new System.Drawing.Point(3, 145);
            this.lblFulfillmentPlanWarning.Name = "lblFulfillmentPlanWarning";
            this.lblFulfillmentPlanWarning.Size = new System.Drawing.Size(169, 13);
            this.lblFulfillmentPlanWarning.TabIndex = 9;
            this.lblFulfillmentPlanWarning.Text = "Not on any Fulfillment Plans!";
            // 
            // lnkRequestQuote
            // 
            this.lnkRequestQuote.AutoSize = true;
            this.lnkRequestQuote.Location = new System.Drawing.Point(391, 123);
            this.lnkRequestQuote.Name = "lnkRequestQuote";
            this.lnkRequestQuote.Size = new System.Drawing.Size(79, 13);
            this.lnkRequestQuote.TabIndex = 6;
            this.lnkRequestQuote.TabStop = true;
            this.lnkRequestQuote.Text = "Request Quote";
            this.lnkRequestQuote.Visible = false;
            this.lnkRequestQuote.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRequestQuote_LinkClicked);
            // 
            // PurchaseOrderLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkRequestQuote);
            this.Controls.Add(this.lblFulfillmentPlanWarning);
            this.Controls.Add(this.pnlItem);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLineCost);
            this.Controls.Add(this.txtUnitCost);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlService);
            this.Controls.Add(this.rdoService);
            this.Controls.Add(this.rdoItem);
            this.Controls.Add(this.label1);
            this.Name = "PurchaseOrderLineControl";
            this.Size = new System.Drawing.Size(496, 166);
            this.Load += new System.EventHandler(this.PurchaseOrderLineControl_Load);
            this.pnlItem.ResumeLayout(false);
            this.pnlItem.PerformLayout();
            this.pnlService.ResumeLayout(false);
            this.pnlService.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoItem;
        private System.Windows.Forms.RadioButton rdoService;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.Label label2;
        private ItemSelectorInput cboItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Panel pnlService;
        private System.Windows.Forms.TextBox txtServiceDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUnitCost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLineCost;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Label lblFulfillmentPlanWarning;
        private System.Windows.Forms.LinkLabel lnkRequestQuote;
    }
}
