namespace CompanyStudio.Purchasing.ApprovalViewer
{
    partial class ApprovalControl
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
            this.lblApprover = new System.Windows.Forms.Label();
            this.txtApprover = new System.Windows.Forms.TextBox();
            this.lblReasonAssigned = new System.Windows.Forms.Label();
            this.txtReasonAssigned = new System.Windows.Forms.TextBox();
            this.lblRejectReason = new System.Windows.Forms.Label();
            this.txtRejectReason = new System.Windows.Forms.TextBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblApprover
            // 
            this.lblApprover.AutoSize = true;
            this.lblApprover.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApprover.Location = new System.Drawing.Point(19, 0);
            this.lblApprover.Name = "lblApprover";
            this.lblApprover.Size = new System.Drawing.Size(58, 13);
            this.lblApprover.TabIndex = 1;
            this.lblApprover.Text = "Approver";
            // 
            // txtApprover
            // 
            this.txtApprover.Location = new System.Drawing.Point(22, 16);
            this.txtApprover.Name = "txtApprover";
            this.txtApprover.ReadOnly = true;
            this.txtApprover.Size = new System.Drawing.Size(169, 20);
            this.txtApprover.TabIndex = 0;
            // 
            // lblReasonAssigned
            // 
            this.lblReasonAssigned.AutoSize = true;
            this.lblReasonAssigned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReasonAssigned.Location = new System.Drawing.Point(194, 0);
            this.lblReasonAssigned.Name = "lblReasonAssigned";
            this.lblReasonAssigned.Size = new System.Drawing.Size(105, 13);
            this.lblReasonAssigned.TabIndex = 1;
            this.lblReasonAssigned.Text = "Reason Assigned";
            // 
            // txtReasonAssigned
            // 
            this.txtReasonAssigned.Location = new System.Drawing.Point(197, 16);
            this.txtReasonAssigned.Name = "txtReasonAssigned";
            this.txtReasonAssigned.ReadOnly = true;
            this.txtReasonAssigned.Size = new System.Drawing.Size(169, 20);
            this.txtReasonAssigned.TabIndex = 1;
            // 
            // lblRejectReason
            // 
            this.lblRejectReason.AutoSize = true;
            this.lblRejectReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRejectReason.Location = new System.Drawing.Point(369, 0);
            this.lblRejectReason.Name = "lblRejectReason";
            this.lblRejectReason.Size = new System.Drawing.Size(91, 13);
            this.lblRejectReason.TabIndex = 1;
            this.lblRejectReason.Text = "Reject Reason";
            // 
            // txtRejectReason
            // 
            this.txtRejectReason.Location = new System.Drawing.Point(372, 16);
            this.txtRejectReason.Name = "txtRejectReason";
            this.txtRejectReason.ReadOnly = true;
            this.txtRejectReason.Size = new System.Drawing.Size(169, 20);
            this.txtRejectReason.TabIndex = 2;
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(0, 17);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(16, 16);
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // ApprovalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtRejectReason);
            this.Controls.Add(this.lblRejectReason);
            this.Controls.Add(this.txtReasonAssigned);
            this.Controls.Add(this.lblReasonAssigned);
            this.Controls.Add(this.txtApprover);
            this.Controls.Add(this.lblApprover);
            this.Controls.Add(this.picIcon);
            this.Name = "ApprovalControl";
            this.Size = new System.Drawing.Size(547, 40);
            this.Load += new System.EventHandler(this.ApprovalControl_Load);
            this.SizeChanged += new System.EventHandler(this.ApprovalControl_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblApprover;
        private System.Windows.Forms.TextBox txtApprover;
        private System.Windows.Forms.Label lblReasonAssigned;
        private System.Windows.Forms.TextBox txtReasonAssigned;
        private System.Windows.Forms.Label lblRejectReason;
        private System.Windows.Forms.TextBox txtRejectReason;
    }
}
