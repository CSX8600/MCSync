namespace GovernmentPortal.Invoicing
{
    partial class frmAutomaticPaymentsCloneToPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomaticPaymentsCloneToPicker));
            this.label1 = new System.Windows.Forms.Label();
            this.lstConfigs = new System.Windows.Forms.ListBox();
            this.cmdClone = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Payees To Clone To";
            // 
            // lstConfigs
            // 
            this.lstConfigs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConfigs.FormattingEnabled = true;
            this.lstConfigs.IntegralHeight = false;
            this.lstConfigs.Location = new System.Drawing.Point(12, 32);
            this.lstConfigs.Name = "lstConfigs";
            this.lstConfigs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstConfigs.Size = new System.Drawing.Size(195, 226);
            this.lstConfigs.Sorted = true;
            this.lstConfigs.TabIndex = 1;
            // 
            // cmdClone
            // 
            this.cmdClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClone.Location = new System.Drawing.Point(132, 264);
            this.cmdClone.Name = "cmdClone";
            this.cmdClone.Size = new System.Drawing.Size(75, 23);
            this.cmdClone.TabIndex = 2;
            this.cmdClone.Text = "Clone";
            this.cmdClone.UseVisualStyleBackColor = true;
            this.cmdClone.Click += new System.EventHandler(this.cmdClone_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(51, 264);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmAutomaticPaymentsCloneToPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 294);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdClone);
            this.Controls.Add(this.lstConfigs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutomaticPaymentsCloneToPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Payees";
            this.Load += new System.EventHandler(this.frmAutomaticPaymentsCloneToPicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstConfigs;
        private System.Windows.Forms.Button cmdClone;
        private System.Windows.Forms.Button cmdCancel;
    }
}