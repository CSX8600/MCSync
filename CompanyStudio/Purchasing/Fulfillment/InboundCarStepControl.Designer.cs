namespace CompanyStudio.Purchasing.Fulfillment
{
    partial class InboundCarStepControl
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
            this.lstCars = new System.Windows.Forms.ListView();
            this.colRailcar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBOL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBOLNo = new System.Windows.Forms.TextBox();
            this.txtRailcar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdAddCarByBOL = new System.Windows.Forms.Button();
            this.cmdAddCarByID = new System.Windows.Forms.Button();
            this.cmdAddFromPOLine = new System.Windows.Forms.Button();
            this.cmdAddEntireTrack = new System.Windows.Forms.Button();
            this.cboPurchaseOrderLines = new System.Windows.Forms.ComboBox();
            this.cboTrack = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCars
            // 
            this.lstCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRailcar,
            this.colBOL});
            this.lstCars.FullRowSelect = true;
            this.lstCars.HideSelection = false;
            this.lstCars.Location = new System.Drawing.Point(0, 70);
            this.lstCars.Name = "lstCars";
            this.lstCars.Size = new System.Drawing.Size(564, 217);
            this.lstCars.TabIndex = 0;
            this.lstCars.UseCompatibleStateImageBehavior = false;
            this.lstCars.View = System.Windows.Forms.View.Details;
            this.lstCars.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstCars_KeyUp);
            // 
            // colRailcar
            // 
            this.colRailcar.Text = "Railcar";
            this.colRailcar.Width = 150;
            // 
            // colBOL
            // 
            this.colBOL.Text = "Inbound BOL #";
            this.colBOL.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Inbound Railcars";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(1, 20);
            this.label2.MaximumSize = new System.Drawing.Size(560, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(560, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "By entering railcar information below, you will be presented with an up-to-date l" +
    "ist of items that has yet to be fulfilled per each railcar.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Railcars To Receive";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtBOLNo);
            this.groupBox1.Controls.Add(this.txtRailcar);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmdAddCarByBOL);
            this.groupBox1.Controls.Add(this.cmdAddCarByID);
            this.groupBox1.Controls.Add(this.cmdAddFromPOLine);
            this.groupBox1.Controls.Add(this.cmdAddEntireTrack);
            this.groupBox1.Controls.Add(this.cboPurchaseOrderLines);
            this.groupBox1.Controls.Add(this.cboTrack);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 293);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entry Methods";
            // 
            // txtBOLNo
            // 
            this.txtBOLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBOLNo.Location = new System.Drawing.Point(92, 106);
            this.txtBOLNo.Name = "txtBOLNo";
            this.txtBOLNo.Size = new System.Drawing.Size(379, 20);
            this.txtBOLNo.TabIndex = 6;
            this.txtBOLNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBOLNo_KeyUp);
            // 
            // txtRailcar
            // 
            this.txtRailcar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRailcar.Location = new System.Drawing.Point(92, 77);
            this.txtRailcar.Name = "txtRailcar";
            this.txtRailcar.Size = new System.Drawing.Size(379, 20);
            this.txtRailcar.TabIndex = 4;
            this.txtRailcar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRailcar_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "From BOL #:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Add Railcar:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Add from Purchase Order Line:";
            // 
            // cmdAddCarByBOL
            // 
            this.cmdAddCarByBOL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddCarByBOL.Location = new System.Drawing.Point(477, 104);
            this.cmdAddCarByBOL.Name = "cmdAddCarByBOL";
            this.cmdAddCarByBOL.Size = new System.Drawing.Size(75, 23);
            this.cmdAddCarByBOL.TabIndex = 7;
            this.cmdAddCarByBOL.Text = "Add Car";
            this.cmdAddCarByBOL.UseVisualStyleBackColor = true;
            this.cmdAddCarByBOL.Click += new System.EventHandler(this.cmdAddCarByBOL_Click);
            // 
            // cmdAddCarByID
            // 
            this.cmdAddCarByID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddCarByID.Location = new System.Drawing.Point(477, 75);
            this.cmdAddCarByID.Name = "cmdAddCarByID";
            this.cmdAddCarByID.Size = new System.Drawing.Size(75, 23);
            this.cmdAddCarByID.TabIndex = 5;
            this.cmdAddCarByID.Text = "Add Car";
            this.cmdAddCarByID.UseVisualStyleBackColor = true;
            this.cmdAddCarByID.Click += new System.EventHandler(this.cmdAddCarByID_Click);
            // 
            // cmdAddFromPOLine
            // 
            this.cmdAddFromPOLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddFromPOLine.Location = new System.Drawing.Point(477, 46);
            this.cmdAddFromPOLine.Name = "cmdAddFromPOLine";
            this.cmdAddFromPOLine.Size = new System.Drawing.Size(75, 23);
            this.cmdAddFromPOLine.TabIndex = 3;
            this.cmdAddFromPOLine.Text = "Add All Cars";
            this.cmdAddFromPOLine.UseVisualStyleBackColor = true;
            this.cmdAddFromPOLine.Click += new System.EventHandler(this.cmdAddFromPOLine_Click);
            // 
            // cmdAddEntireTrack
            // 
            this.cmdAddEntireTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddEntireTrack.Location = new System.Drawing.Point(477, 17);
            this.cmdAddEntireTrack.Name = "cmdAddEntireTrack";
            this.cmdAddEntireTrack.Size = new System.Drawing.Size(75, 23);
            this.cmdAddEntireTrack.TabIndex = 1;
            this.cmdAddEntireTrack.Text = "Add All Cars";
            this.cmdAddEntireTrack.UseVisualStyleBackColor = true;
            this.cmdAddEntireTrack.Click += new System.EventHandler(this.cmdAddEntireTrack_Click);
            // 
            // cboPurchaseOrderLines
            // 
            this.cboPurchaseOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPurchaseOrderLines.FormattingEnabled = true;
            this.cboPurchaseOrderLines.Location = new System.Drawing.Point(164, 48);
            this.cboPurchaseOrderLines.Name = "cboPurchaseOrderLines";
            this.cboPurchaseOrderLines.Size = new System.Drawing.Size(307, 21);
            this.cboPurchaseOrderLines.TabIndex = 2;
            // 
            // cboTrack
            // 
            this.cboTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrack.FormattingEnabled = true;
            this.cboTrack.Location = new System.Drawing.Point(102, 19);
            this.cboTrack.Name = "cboTrack";
            this.cboTrack.Size = new System.Drawing.Size(369, 21);
            this.cboTrack.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Add Entire Track:";
            // 
            // InboundCarStepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCars);
            this.Name = "InboundCarStepControl";
            this.Size = new System.Drawing.Size(564, 431);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstCars;
        private System.Windows.Forms.ColumnHeader colRailcar;
        private System.Windows.Forms.ColumnHeader colBOL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdAddEntireTrack;
        private System.Windows.Forms.ComboBox cboTrack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdAddFromPOLine;
        private System.Windows.Forms.ComboBox cboPurchaseOrderLines;
        private System.Windows.Forms.TextBox txtBOLNo;
        private System.Windows.Forms.TextBox txtRailcar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdAddCarByBOL;
        private System.Windows.Forms.Button cmdAddCarByID;
    }
}
