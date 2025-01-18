namespace CompanyStudio
{
    partial class ItemSelectorInput
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmdDropDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(424, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // cmdDropDown
            // 
            this.cmdDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDropDown.BackColor = System.Drawing.Color.White;
            this.cmdDropDown.BackgroundImage = global::CompanyStudio.Properties.Resources.arrow_down;
            this.cmdDropDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDropDown.Location = new System.Drawing.Point(424, 0);
            this.cmdDropDown.Name = "cmdDropDown";
            this.cmdDropDown.Size = new System.Drawing.Size(20, 20);
            this.cmdDropDown.TabIndex = 1;
            this.cmdDropDown.UseVisualStyleBackColor = false;
            this.cmdDropDown.Click += new System.EventHandler(this.cmdDropDown_Click);
            // 
            // ItemSelectorInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdDropDown);
            this.Controls.Add(this.textBox1);
            this.Name = "ItemSelectorInput";
            this.Size = new System.Drawing.Size(444, 20);
            this.Load += new System.EventHandler(this.ItemSelectorInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button cmdDropDown;
    }
}
