namespace CompanyStudio
{
    partial class frmStartPageEditQuickAccess
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
            this.spaceForm1 = new ReaLTaiizor.Forms.SpaceForm();
            this.cmdCancel = new ReaLTaiizor.Controls.CrownButton();
            this.cmdSave = new ReaLTaiizor.Controls.CrownButton();
            this.txtQuickAccessName = new ReaLTaiizor.Controls.CrownTextBox();
            this.cboLocations = new ReaLTaiizor.Controls.CrownComboBox();
            this.treMenus = new ReaLTaiizor.Controls.CrownTreeView();
            this.crownLabel2 = new ReaLTaiizor.Controls.CrownLabel();
            this.crownLabel3 = new ReaLTaiizor.Controls.CrownLabel();
            this.crownLabel1 = new ReaLTaiizor.Controls.CrownLabel();
            this.spaceClose1 = new ReaLTaiizor.Controls.SpaceClose();
            this.spaceForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spaceForm1
            // 
            this.spaceForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.spaceForm1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.spaceForm1.Controls.Add(this.cmdCancel);
            this.spaceForm1.Controls.Add(this.cmdSave);
            this.spaceForm1.Controls.Add(this.txtQuickAccessName);
            this.spaceForm1.Controls.Add(this.cboLocations);
            this.spaceForm1.Controls.Add(this.treMenus);
            this.spaceForm1.Controls.Add(this.crownLabel2);
            this.spaceForm1.Controls.Add(this.crownLabel3);
            this.spaceForm1.Controls.Add(this.crownLabel1);
            this.spaceForm1.Controls.Add(this.spaceClose1);
            this.spaceForm1.Customization = "/////+Dg4P//////Kioq/xwcHP9QUFD/Kysr/xkZGf8=";
            this.spaceForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spaceForm1.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceForm1.Image = null;
            this.spaceForm1.Location = new System.Drawing.Point(0, 0);
            this.spaceForm1.MinimumSize = new System.Drawing.Size(200, 25);
            this.spaceForm1.Movable = true;
            this.spaceForm1.Name = "spaceForm1";
            this.spaceForm1.NoRounding = false;
            this.spaceForm1.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            this.spaceForm1.Sizable = true;
            this.spaceForm1.Size = new System.Drawing.Size(667, 325);
            this.spaceForm1.SmartBounds = true;
            this.spaceForm1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.spaceForm1.TabIndex = 0;
            this.spaceForm1.Text = "Quick Access Editor";
            this.spaceForm1.TransparencyKey = System.Drawing.Color.Purple;
            this.spaceForm1.Transparent = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(499, 294);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Padding = new System.Windows.Forms.Padding(5);
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(580, 294);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(5);
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtQuickAccessName
            // 
            this.txtQuickAccessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuickAccessName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtQuickAccessName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuickAccessName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtQuickAccessName.Location = new System.Drawing.Point(172, 268);
            this.txtQuickAccessName.Name = "txtQuickAccessName";
            this.txtQuickAccessName.Size = new System.Drawing.Size(483, 20);
            this.txtQuickAccessName.TabIndex = 2;
            // 
            // cboLocations
            // 
            this.cboLocations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLocations.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboLocations.FormattingEnabled = true;
            this.cboLocations.Location = new System.Drawing.Point(123, 32);
            this.cboLocations.Name = "cboLocations";
            this.cboLocations.Size = new System.Drawing.Size(532, 21);
            this.cboLocations.TabIndex = 0;
            this.cboLocations.SelectionChangeCommitted += new System.EventHandler(this.treMenus_SelectedNodesChanged);
            // 
            // treMenus
            // 
            this.treMenus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treMenus.Location = new System.Drawing.Point(12, 72);
            this.treMenus.MaxDragChange = 20;
            this.treMenus.Name = "treMenus";
            this.treMenus.ShowIcons = true;
            this.treMenus.Size = new System.Drawing.Size(643, 188);
            this.treMenus.TabIndex = 1;
            this.treMenus.Text = "crownTreeView1";
            this.treMenus.SelectedNodesChanged += new System.EventHandler(this.treMenus_SelectedNodesChanged);
            // 
            // crownLabel2
            // 
            this.crownLabel2.AutoSize = true;
            this.crownLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.crownLabel2.Location = new System.Drawing.Point(8, 56);
            this.crownLabel2.Name = "crownLabel2";
            this.crownLabel2.Size = new System.Drawing.Size(94, 13);
            this.crownLabel2.TabIndex = 1;
            this.crownLabel2.Text = "Select a menu:";
            // 
            // crownLabel3
            // 
            this.crownLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.crownLabel3.AutoSize = true;
            this.crownLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.crownLabel3.Location = new System.Drawing.Point(8, 271);
            this.crownLabel3.Name = "crownLabel3";
            this.crownLabel3.Size = new System.Drawing.Size(158, 13);
            this.crownLabel3.TabIndex = 1;
            this.crownLabel3.Text = "Enter Quick Access Name:";
            // 
            // crownLabel1
            // 
            this.crownLabel1.AutoSize = true;
            this.crownLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.crownLabel1.Location = new System.Drawing.Point(8, 35);
            this.crownLabel1.Name = "crownLabel1";
            this.crownLabel1.Size = new System.Drawing.Size(109, 13);
            this.crownLabel1.TabIndex = 1;
            this.crownLabel1.Text = "Select a Location:";
            // 
            // spaceClose1
            // 
            this.spaceClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spaceClose1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.spaceClose1.Customization = "DQ/S/xhh8///////4ODg/x5/9/8ND9L/UFBQ/+Dg4P/AwMD/";
            this.spaceClose1.DefaultAnchor = true;
            this.spaceClose1.DefaultLocation = true;
            this.spaceClose1.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceClose1.Image = null;
            this.spaceClose1.Location = new System.Drawing.Point(641, 3);
            this.spaceClose1.Name = "spaceClose1";
            this.spaceClose1.NoRounding = false;
            this.spaceClose1.Size = new System.Drawing.Size(23, 21);
            this.spaceClose1.TabIndex = 0;
            this.spaceClose1.Text = "x";
            this.spaceClose1.Transparent = false;
            // 
            // frmStartPageEditQuickAccess
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(667, 325);
            this.Controls.Add(this.spaceForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(200, 25);
            this.Name = "frmStartPageEditQuickAccess";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Access Editor";
            this.TransparencyKey = System.Drawing.Color.Purple;
            this.Load += new System.EventHandler(this.frmStartPageEditQuickAccess_Load);
            this.spaceForm1.ResumeLayout(false);
            this.spaceForm1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Forms.SpaceForm spaceForm1;
        private ReaLTaiizor.Controls.SpaceClose spaceClose1;
        private ReaLTaiizor.Controls.CrownLabel crownLabel1;
        private ReaLTaiizor.Controls.CrownTreeView treMenus;
        private ReaLTaiizor.Controls.CrownComboBox cboLocations;
        private ReaLTaiizor.Controls.CrownButton cmdCancel;
        private ReaLTaiizor.Controls.CrownButton cmdSave;
        private ReaLTaiizor.Controls.CrownTextBox txtQuickAccessName;
        private ReaLTaiizor.Controls.CrownLabel crownLabel2;
        private ReaLTaiizor.Controls.CrownLabel crownLabel3;
    }
}