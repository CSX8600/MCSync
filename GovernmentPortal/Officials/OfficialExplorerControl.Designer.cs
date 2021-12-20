﻿
namespace GovernmentPortal.Officials
{
    partial class OfficialExplorerControl
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

            if (disposing)
            {
                PermissionsManager.OnPermissionChange -= PermissionsManager_OnPermissionChange;
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
            this.loader = new GovernmentPortal.Loader();
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEmails = new System.Windows.Forms.CheckBox();
            this.chkOfficials = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Location = new System.Drawing.Point(0, 0);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(448, 230);
            this.loader.TabIndex = 0;
            this.loader.Visible = false;
            // 
            // cboUsers
            // 
            this.cboUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsers.FormattingEnabled = true;
            this.cboUsers.Location = new System.Drawing.Point(41, 3);
            this.cboUsers.Name = "cboUsers";
            this.cboUsers.Size = new System.Drawing.Size(404, 21);
            this.cboUsers.TabIndex = 0;
            this.cboUsers.SelectedIndexChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "What permissions does this Official have?";
            // 
            // chkEmails
            // 
            this.chkEmails.AutoSize = true;
            this.chkEmails.Location = new System.Drawing.Point(6, 43);
            this.chkEmails.Name = "chkEmails";
            this.chkEmails.Size = new System.Drawing.Size(98, 17);
            this.chkEmails.TabIndex = 1;
            this.chkEmails.Text = "Manage Emails";
            this.chkEmails.UseVisualStyleBackColor = true;
            this.chkEmails.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // chkOfficials
            // 
            this.chkOfficials.AutoSize = true;
            this.chkOfficials.Location = new System.Drawing.Point(6, 66);
            this.chkOfficials.Name = "chkOfficials";
            this.chkOfficials.Size = new System.Drawing.Size(105, 17);
            this.chkOfficials.TabIndex = 2;
            this.chkOfficials.Text = "Manage Officials";
            this.chkOfficials.UseVisualStyleBackColor = true;
            this.chkOfficials.CheckedChanged += new System.EventHandler(this.FormValueChanged);
            // 
            // OfficialExplorerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkOfficials);
            this.Controls.Add(this.chkEmails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboUsers);
            this.Controls.Add(this.loader);
            this.Name = "OfficialExplorerControl";
            this.Size = new System.Drawing.Size(448, 230);
            this.Load += new System.EventHandler(this.OfficialExplorerControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Loader loader;
        private System.Windows.Forms.ComboBox cboUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEmails;
        private System.Windows.Forms.CheckBox chkOfficials;
    }
}