namespace CompanyStudio.Purchasing.DraftEntry
{
    partial class RouteControlEnd
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
            this.lblName = new System.Windows.Forms.Label();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(311, 23);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "[Company End]";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdInsert
            // 
            this.cmdInsert.Image = global::CompanyStudio.Properties.Resources.arrow_left;
            this.cmdInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInsert.Location = new System.Drawing.Point(3, 30);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(57, 23);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // RouteControlEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.lblName);
            this.Name = "RouteControlEnd";
            this.Size = new System.Drawing.Size(315, 54);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Label lblName;
    }
}
