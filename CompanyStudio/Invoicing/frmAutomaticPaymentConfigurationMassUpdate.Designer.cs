namespace CompanyStudio.Invoicing
{
    partial class frmAutomaticPaymentConfigurationMassUpdate
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomaticPaymentConfigurationMassUpdate));
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaxAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPaymentAccount = new System.Windows.Forms.ComboBox();
            this.rdoImmediately = new System.Windows.Forms.RadioButton();
            this.rdoOnDueDate = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.studioFormExtender = new CompanyStudio.StudioFormExtender(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max Amount:";
            // 
            // txtMaxAmount
            // 
            this.txtMaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxAmount.Location = new System.Drawing.Point(117, 12);
            this.txtMaxAmount.Name = "txtMaxAmount";
            this.txtMaxAmount.Size = new System.Drawing.Size(225, 20);
            this.txtMaxAmount.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Payment Account:";
            // 
            // cboPaymentAccount
            // 
            this.cboPaymentAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPaymentAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPaymentAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPaymentAccount.FormattingEnabled = true;
            this.cboPaymentAccount.Location = new System.Drawing.Point(117, 61);
            this.cboPaymentAccount.Name = "cboPaymentAccount";
            this.cboPaymentAccount.Size = new System.Drawing.Size(225, 21);
            this.cboPaymentAccount.TabIndex = 3;
            // 
            // rdoImmediately
            // 
            this.rdoImmediately.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdoImmediately.AutoSize = true;
            this.rdoImmediately.Location = new System.Drawing.Point(211, 38);
            this.rdoImmediately.Name = "rdoImmediately";
            this.rdoImmediately.Size = new System.Drawing.Size(80, 17);
            this.rdoImmediately.TabIndex = 2;
            this.rdoImmediately.Text = "Immediately";
            this.rdoImmediately.UseVisualStyleBackColor = true;
            // 
            // rdoOnDueDate
            // 
            this.rdoOnDueDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdoOnDueDate.AutoSize = true;
            this.rdoOnDueDate.Checked = true;
            this.rdoOnDueDate.Location = new System.Drawing.Point(117, 38);
            this.rdoOnDueDate.Name = "rdoOnDueDate";
            this.rdoOnDueDate.Size = new System.Drawing.Size(88, 17);
            this.rdoOnDueDate.TabIndex = 1;
            this.rdoOnDueDate.TabStop = true;
            this.rdoOnDueDate.Text = "On Due Date";
            this.rdoOnDueDate.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Payment Schedule:";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdSave.Location = new System.Drawing.Point(267, 88);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdCancel.Location = new System.Drawing.Point(186, 88);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmAutomaticPaymentConfigurationMassUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 122);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.rdoImmediately);
            this.Controls.Add(this.rdoOnDueDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboPaymentAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaxAmount);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutomaticPaymentConfigurationMassUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Selected";
            this.Load += new System.EventHandler(this.frmAutomaticPaymentConfigurationMassUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaxAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboPaymentAccount;
        private System.Windows.Forms.RadioButton rdoImmediately;
        private System.Windows.Forms.RadioButton rdoOnDueDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private StudioFormExtender studioFormExtender;
    }
}