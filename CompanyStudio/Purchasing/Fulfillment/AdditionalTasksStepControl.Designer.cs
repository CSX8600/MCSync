namespace CompanyStudio.Purchasing.Fulfillment
{
    partial class AdditionalTasksStepControl
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
            this.chkReleaseCars = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Additional Tasks";
            // 
            // chkReleaseCars
            // 
            this.chkReleaseCars.AutoSize = true;
            this.chkReleaseCars.Location = new System.Drawing.Point(3, 36);
            this.chkReleaseCars.Name = "chkReleaseCars";
            this.chkReleaseCars.Size = new System.Drawing.Size(248, 17);
            this.chkReleaseCars.TabIndex = 1;
            this.chkReleaseCars.Text = "Release fulfilled cars to first carrier, if applicable";
            this.chkReleaseCars.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(356, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select any additional tasks below that you would like the wizard to perform";
            // 
            // AdditionalTasksStepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkReleaseCars);
            this.Controls.Add(this.label1);
            this.Name = "AdditionalTasksStepControl";
            this.Size = new System.Drawing.Size(423, 228);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkReleaseCars;
        private System.Windows.Forms.Label label2;
    }
}
