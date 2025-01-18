namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    partial class Draft
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboGovernment = new System.Windows.Forms.ComboBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.rdoGovernment = new System.Windows.Forms.RadioButton();
            this.rdoCompany = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalCost = new System.Windows.Forms.TextBox();
            this.grpLines = new System.Windows.Forms.GroupBox();
            this.pnlLines = new System.Windows.Forms.Panel();
            this.lblLinePlaceholder = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewLine = new System.Windows.Forms.ToolStripButton();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpLines.SuspendLayout();
            this.pnlLines.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboGovernment);
            this.groupBox1.Controls.Add(this.cboCompany);
            this.groupBox1.Controls.Add(this.rdoGovernment);
            this.groupBox1.Controls.Add(this.rdoCompany);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order From";
            // 
            // cboGovernment
            // 
            this.cboGovernment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGovernment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboGovernment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboGovernment.Enabled = false;
            this.cboGovernment.FormattingEnabled = true;
            this.cboGovernment.Location = new System.Drawing.Point(95, 46);
            this.cboGovernment.Name = "cboGovernment";
            this.cboGovernment.Size = new System.Drawing.Size(530, 21);
            this.cboGovernment.TabIndex = 1;
            this.cboGovernment.SelectedIndexChanged += new System.EventHandler(this.EntityComboBoxSelectedIndexChanged);
            // 
            // cboCompany
            // 
            this.cboCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCompany.Enabled = false;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(95, 19);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(530, 21);
            this.cboCompany.TabIndex = 1;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.EntityComboBoxSelectedIndexChanged);
            // 
            // rdoGovernment
            // 
            this.rdoGovernment.AutoSize = true;
            this.rdoGovernment.Location = new System.Drawing.Point(6, 47);
            this.rdoGovernment.Name = "rdoGovernment";
            this.rdoGovernment.Size = new System.Drawing.Size(83, 17);
            this.rdoGovernment.TabIndex = 0;
            this.rdoGovernment.TabStop = true;
            this.rdoGovernment.Text = "Government";
            this.rdoGovernment.UseVisualStyleBackColor = true;
            this.rdoGovernment.CheckedChanged += new System.EventHandler(this.EntityRadioCheckedChanged);
            // 
            // rdoCompany
            // 
            this.rdoCompany.AutoSize = true;
            this.rdoCompany.Location = new System.Drawing.Point(6, 20);
            this.rdoCompany.Name = "rdoCompany";
            this.rdoCompany.Size = new System.Drawing.Size(69, 17);
            this.rdoCompany.TabIndex = 0;
            this.rdoCompany.TabStop = true;
            this.rdoCompany.Text = "Company";
            this.rdoCompany.UseVisualStyleBackColor = true;
            this.rdoCompany.CheckedChanged += new System.EventHandler(this.EntityRadioCheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(3, 95);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(631, 53);
            this.txtDescription.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Cost:";
            // 
            // txtTotalCost
            // 
            this.txtTotalCost.Location = new System.Drawing.Point(67, 154);
            this.txtTotalCost.Name = "txtTotalCost";
            this.txtTotalCost.ReadOnly = true;
            this.txtTotalCost.Size = new System.Drawing.Size(58, 20);
            this.txtTotalCost.TabIndex = 4;
            // 
            // grpLines
            // 
            this.grpLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLines.Controls.Add(this.pnlLines);
            this.grpLines.Controls.Add(this.toolStrip1);
            this.grpLines.Location = new System.Drawing.Point(3, 180);
            this.grpLines.Name = "grpLines";
            this.grpLines.Size = new System.Drawing.Size(631, 141);
            this.grpLines.TabIndex = 5;
            this.grpLines.TabStop = false;
            this.grpLines.Text = "Purchase Order Lines";
            // 
            // pnlLines
            // 
            this.pnlLines.AutoScroll = true;
            this.pnlLines.Controls.Add(this.lblLinePlaceholder);
            this.pnlLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLines.Location = new System.Drawing.Point(3, 41);
            this.pnlLines.Name = "pnlLines";
            this.pnlLines.Size = new System.Drawing.Size(625, 97);
            this.pnlLines.TabIndex = 2;
            // 
            // lblLinePlaceholder
            // 
            this.lblLinePlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLinePlaceholder.AutoSize = true;
            this.lblLinePlaceholder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinePlaceholder.Location = new System.Drawing.Point(214, 42);
            this.lblLinePlaceholder.Name = "lblLinePlaceholder";
            this.lblLinePlaceholder.Size = new System.Drawing.Size(196, 13);
            this.lblLinePlaceholder.TabIndex = 1;
            this.lblLinePlaceholder.Text = "Click New Line to order items or services";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewLine});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(625, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNewLine
            // 
            this.tsbNewLine.Image = global::GovernmentPortal.Properties.Resources.cart_add;
            this.tsbNewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewLine.Name = "tsbNewLine";
            this.tsbNewLine.Size = new System.Drawing.Size(70, 22);
            this.tsbNewLine.Text = "New Line";
            this.tsbNewLine.Click += new System.EventHandler(this.tsbNewLine_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(553, 327);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdSubmit
            // 
            this.cmdSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSubmit.Location = new System.Drawing.Point(472, 327);
            this.cmdSubmit.Name = "cmdSubmit";
            this.cmdSubmit.Size = new System.Drawing.Size(75, 23);
            this.cmdSubmit.TabIndex = 6;
            this.cmdSubmit.Text = "Submit";
            this.cmdSubmit.UseVisualStyleBackColor = true;
            this.cmdSubmit.Click += new System.EventHandler(this.cmdSubmit_Click);
            // 
            // Draft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdSubmit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpLines);
            this.Controls.Add(this.txtTotalCost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Draft";
            this.Size = new System.Drawing.Size(637, 353);
            this.Load += new System.EventHandler(this.Draft_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpLines.ResumeLayout(false);
            this.grpLines.PerformLayout();
            this.pnlLines.ResumeLayout(false);
            this.pnlLines.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoGovernment;
        private System.Windows.Forms.RadioButton rdoCompany;
        private System.Windows.Forms.ComboBox cboGovernment;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalCost;
        private System.Windows.Forms.GroupBox grpLines;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNewLine;
        private System.Windows.Forms.Label lblLinePlaceholder;
        private System.Windows.Forms.Panel pnlLines;
        private System.Windows.Forms.Button cmdSubmit;
    }
}
