namespace CompanyStudio.Invoicing
{
    partial class frmAutomaticPaymentConfigurationCloneTo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomaticPaymentConfigurationCloneTo));
            this.label1 = new System.Windows.Forms.Label();
            this.chkLocations = new System.Windows.Forms.CheckedListBox();
            this.cmdClone = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.studioFormExtender = new CompanyStudio.StudioFormExtender(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Which Locations would you like to clone these configurations to?";
            // 
            // chkLocations
            // 
            this.chkLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLocations.CheckOnClick = true;
            this.chkLocations.FormattingEnabled = true;
            this.chkLocations.IntegralHeight = false;
            this.chkLocations.Location = new System.Drawing.Point(12, 28);
            this.chkLocations.Name = "chkLocations";
            this.chkLocations.Size = new System.Drawing.Size(385, 199);
            this.chkLocations.TabIndex = 1;
            this.chkLocations.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLocations_ItemCheck);
            // 
            // cmdClone
            // 
            this.cmdClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClone.Location = new System.Drawing.Point(322, 233);
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
            this.cmdCancel.Location = new System.Drawing.Point(241, 233);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmAutomaticPaymentConfigurationCloneTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 264);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdClone);
            this.Controls.Add(this.chkLocations);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutomaticPaymentConfigurationCloneTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Locations";
            this.Load += new System.EventHandler(this.frmAutomaticPaymentConfigurationCloneTo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkLocations;
        private System.Windows.Forms.Button cmdClone;
        private System.Windows.Forms.Button cmdCancel;
        private StudioFormExtender studioFormExtender;
    }
}