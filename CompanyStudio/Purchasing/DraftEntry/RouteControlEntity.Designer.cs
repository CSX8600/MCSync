namespace CompanyStudio.Purchasing.DraftEntry
{
    partial class RouteControlEntity
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
            this.arrow1 = new CompanyStudio.Purchasing.DraftEntry.Arrow();
            this.cboEntities = new System.Windows.Forms.ComboBox();
            this.cmdMoveRight = new System.Windows.Forms.Button();
            this.cmdMoveLeft = new System.Windows.Forms.Button();
            this.cmdInsertLeft = new System.Windows.Forms.Button();
            this.cmdInsertRight = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // arrow1
            // 
            this.arrow1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.arrow1.ArrowDirection = CompanyStudio.Purchasing.DraftEntry.Arrow.ArrowDirections.Right;
            this.arrow1.ArrowHeadLength = 15;
            this.arrow1.Location = new System.Drawing.Point(237, 4);
            this.arrow1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrow1.Name = "arrow1";
            this.arrow1.Size = new System.Drawing.Size(75, 20);
            this.arrow1.TabIndex = 3;
            // 
            // cboEntities
            // 
            this.cboEntities.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboEntities.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboEntities.FormattingEnabled = true;
            this.cboEntities.Location = new System.Drawing.Point(3, 3);
            this.cboEntities.Name = "cboEntities";
            this.cboEntities.Size = new System.Drawing.Size(170, 21);
            this.cboEntities.TabIndex = 0;
            this.cboEntities.SelectedIndexChanged += new System.EventHandler(this.cboEntities_SelectedIndexChanged);
            // 
            // cmdMoveRight
            // 
            this.cmdMoveRight.Image = global::CompanyStudio.Properties.Resources.arrow_right;
            this.cmdMoveRight.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdMoveRight.Location = new System.Drawing.Point(161, 30);
            this.cmdMoveRight.Name = "cmdMoveRight";
            this.cmdMoveRight.Size = new System.Drawing.Size(55, 23);
            this.cmdMoveRight.TabIndex = 4;
            this.cmdMoveRight.Text = "Move";
            this.cmdMoveRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdMoveRight.UseVisualStyleBackColor = true;
            this.cmdMoveRight.Click += new System.EventHandler(this.cmdMoveRight_Click);
            // 
            // cmdMoveLeft
            // 
            this.cmdMoveLeft.Image = global::CompanyStudio.Properties.Resources.arrow_left;
            this.cmdMoveLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdMoveLeft.Location = new System.Drawing.Point(100, 30);
            this.cmdMoveLeft.Name = "cmdMoveLeft";
            this.cmdMoveLeft.Size = new System.Drawing.Size(55, 23);
            this.cmdMoveLeft.TabIndex = 3;
            this.cmdMoveLeft.Text = "Move";
            this.cmdMoveLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdMoveLeft.UseVisualStyleBackColor = true;
            this.cmdMoveLeft.Click += new System.EventHandler(this.cmdMoveLeft_Click);
            // 
            // cmdInsertLeft
            // 
            this.cmdInsertLeft.Image = global::CompanyStudio.Properties.Resources.arrow_left;
            this.cmdInsertLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInsertLeft.Location = new System.Drawing.Point(3, 30);
            this.cmdInsertLeft.Name = "cmdInsertLeft";
            this.cmdInsertLeft.Size = new System.Drawing.Size(57, 23);
            this.cmdInsertLeft.TabIndex = 2;
            this.cmdInsertLeft.Text = "Insert";
            this.cmdInsertLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdInsertLeft.UseVisualStyleBackColor = true;
            this.cmdInsertLeft.Click += new System.EventHandler(this.cmdInsertLeft_Click);
            // 
            // cmdInsertRight
            // 
            this.cmdInsertRight.Image = global::CompanyStudio.Properties.Resources.arrow_right;
            this.cmdInsertRight.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdInsertRight.Location = new System.Drawing.Point(257, 30);
            this.cmdInsertRight.Name = "cmdInsertRight";
            this.cmdInsertRight.Size = new System.Drawing.Size(57, 23);
            this.cmdInsertRight.TabIndex = 5;
            this.cmdInsertRight.Text = "Insert";
            this.cmdInsertRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInsertRight.UseVisualStyleBackColor = true;
            this.cmdInsertRight.Click += new System.EventHandler(this.cmdInsertRight_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = global::CompanyStudio.Properties.Resources.delete;
            this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDelete.Location = new System.Drawing.Point(176, 2);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(60, 22);
            this.cmdDelete.TabIndex = 1;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // RouteControlEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cboEntities);
            this.Controls.Add(this.cmdMoveRight);
            this.Controls.Add(this.cmdMoveLeft);
            this.Controls.Add(this.cmdInsertLeft);
            this.Controls.Add(this.cmdInsertRight);
            this.Controls.Add(this.arrow1);
            this.Name = "RouteControlEntity";
            this.Size = new System.Drawing.Size(315, 54);
            this.Load += new System.EventHandler(this.RouteControlCompany_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInsertRight;
        private Arrow arrow1;
        private System.Windows.Forms.Button cmdMoveLeft;
        private System.Windows.Forms.Button cmdMoveRight;
        private System.Windows.Forms.Button cmdInsertLeft;
        private System.Windows.Forms.ComboBox cboEntities;
        private System.Windows.Forms.Button cmdDelete;
    }
}
