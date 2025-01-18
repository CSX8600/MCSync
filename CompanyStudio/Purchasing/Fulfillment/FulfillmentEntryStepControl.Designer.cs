namespace CompanyStudio.Purchasing.Fulfillment
{
    partial class FulfillmentEntryStepControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FulfillmentEntryStepControl));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cboPurchaseOrders = new System.Windows.Forms.ComboBox();
            this.cmdAddPO = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlUnfulledPOLines = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fulfillment Entry";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.MaximumSize = new System.Drawing.Size(600, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(584, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Add Purchase Order:";
            // 
            // cboPurchaseOrders
            // 
            this.cboPurchaseOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPurchaseOrders.FormattingEnabled = true;
            this.cboPurchaseOrders.Location = new System.Drawing.Point(115, 54);
            this.cboPurchaseOrders.Name = "cboPurchaseOrders";
            this.cboPurchaseOrders.Size = new System.Drawing.Size(391, 21);
            this.cboPurchaseOrders.TabIndex = 0;
            // 
            // cmdAddPO
            // 
            this.cmdAddPO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddPO.Location = new System.Drawing.Point(512, 52);
            this.cmdAddPO.Name = "cmdAddPO";
            this.cmdAddPO.Size = new System.Drawing.Size(75, 23);
            this.cmdAddPO.TabIndex = 1;
            this.cmdAddPO.Text = "Add";
            this.cmdAddPO.UseVisualStyleBackColor = true;
            this.cmdAddPO.Click += new System.EventHandler(this.cmdAddPO_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Unfulfilled Purchase Order Lines";
            // 
            // pnlUnfulledPOLines
            // 
            this.pnlUnfulledPOLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUnfulledPOLines.AutoScroll = true;
            this.pnlUnfulledPOLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUnfulledPOLines.Location = new System.Drawing.Point(3, 97);
            this.pnlUnfulledPOLines.Name = "pnlUnfulledPOLines";
            this.pnlUnfulledPOLines.Size = new System.Drawing.Size(584, 233);
            this.pnlUnfulledPOLines.TabIndex = 2;
            // 
            // FulfillmentEntryStepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlUnfulledPOLines);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdAddPO);
            this.Controls.Add(this.cboPurchaseOrders);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FulfillmentEntryStepControl";
            this.Size = new System.Drawing.Size(590, 333);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox cboPurchaseOrders;
        private System.Windows.Forms.Button cmdAddPO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlUnfulledPOLines;
    }
}
