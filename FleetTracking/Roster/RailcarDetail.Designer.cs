namespace FleetTracking.Roster
{
    partial class RailcarDetail
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
            this.components = new System.ComponentModel.Container();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pboxImage = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdUpdateImage = new System.Windows.Forms.Button();
            this.lnkLocation = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLessee = new System.Windows.Forms.TextBox();
            this.txtCurrentLocation = new System.Windows.Forms.TextBox();
            this.cboModel = new FleetTracking.ControlSelector();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReportingMark = new System.Windows.Forms.TextBox();
            this.cboStrategicTrack = new System.Windows.Forms.ComboBox();
            this.cboDestination = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboPossessor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboOwner = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReportingNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContents = new System.Windows.Forms.TextBox();
            this.cboCurrentLocation = new System.Windows.Forms.ComboBox();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.loaderGeneral = new FleetTracking.Loader();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdLast = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.cmdFirst = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaderHistory = new FleetTracking.Loader();
            this.dataGridViewStylizer = new FleetTracking.DataGridViewStylizer(this.components);
            this.tabRoute = new System.Windows.Forms.TabPage();
            this.dgvRoute = new System.Windows.Forms.DataGridView();
            this.loaderRoute = new FleetTracking.Loader();
            this.colRouteSort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRouteFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRouteTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.tabRoute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReset.Location = new System.Drawing.Point(286, 257);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 12;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(367, 257);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 11;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Model:";
            // 
            // pboxImage
            // 
            this.pboxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pboxImage.Location = new System.Drawing.Point(-1, 0);
            this.pboxImage.Name = "pboxImage";
            this.pboxImage.Size = new System.Drawing.Size(284, 251);
            this.pboxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboxImage.TabIndex = 0;
            this.pboxImage.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pboxImage);
            this.splitContainer1.Panel1.Controls.Add(this.cmdUpdateImage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.lnkLocation);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.txtLessee);
            this.splitContainer1.Panel2.Controls.Add(this.txtCurrentLocation);
            this.splitContainer1.Panel2.Controls.Add(this.cmdReset);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSave);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.cboModel);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txtReportingMark);
            this.splitContainer1.Panel2.Controls.Add(this.cboStrategicTrack);
            this.splitContainer1.Panel2.Controls.Add(this.cboDestination);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.cboPossessor);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.cboOwner);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.txtReportingNumber);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtContents);
            this.splitContainer1.Panel2.Controls.Add(this.cboCurrentLocation);
            this.splitContainer1.Size = new System.Drawing.Size(735, 283);
            this.splitContainer1.SplitterDistance = 284;
            this.splitContainer1.TabIndex = 0;
            // 
            // cmdUpdateImage
            // 
            this.cmdUpdateImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateImage.Location = new System.Drawing.Point(-1, 257);
            this.cmdUpdateImage.Name = "cmdUpdateImage";
            this.cmdUpdateImage.Size = new System.Drawing.Size(284, 23);
            this.cmdUpdateImage.TabIndex = 0;
            this.cmdUpdateImage.Text = "Update Image";
            this.cmdUpdateImage.UseVisualStyleBackColor = true;
            this.cmdUpdateImage.Click += new System.EventHandler(this.cmdUpdateImage_Click);
            // 
            // lnkLocation
            // 
            this.lnkLocation.AutoSize = true;
            this.lnkLocation.Location = new System.Drawing.Point(3, 139);
            this.lnkLocation.Name = "lnkLocation";
            this.lnkLocation.Size = new System.Drawing.Size(88, 13);
            this.lnkLocation.TabIndex = 6;
            this.lnkLocation.TabStop = true;
            this.lnkLocation.Text = "Current Location:";
            this.lnkLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLocation_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Lessee:";
            // 
            // txtLessee
            // 
            this.txtLessee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLessee.Location = new System.Drawing.Point(96, 83);
            this.txtLessee.Name = "txtLessee";
            this.txtLessee.ReadOnly = true;
            this.txtLessee.Size = new System.Drawing.Size(346, 20);
            this.txtLessee.TabIndex = 4;
            // 
            // txtCurrentLocation
            // 
            this.txtCurrentLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentLocation.Location = new System.Drawing.Point(96, 136);
            this.txtCurrentLocation.Name = "txtCurrentLocation";
            this.txtCurrentLocation.ReadOnly = true;
            this.txtCurrentLocation.Size = new System.Drawing.Size(346, 20);
            this.txtCurrentLocation.TabIndex = 7;
            // 
            // cboModel
            // 
            this.cboModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboModel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModel.Location = new System.Drawing.Point(96, 3);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(346, 21);
            this.cboModel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reporting Mark:";
            // 
            // txtReportingMark
            // 
            this.txtReportingMark.Location = new System.Drawing.Point(96, 30);
            this.txtReportingMark.Name = "txtReportingMark";
            this.txtReportingMark.Size = new System.Drawing.Size(57, 20);
            this.txtReportingMark.TabIndex = 1;
            // 
            // cboStrategicTrack
            // 
            this.cboStrategicTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStrategicTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStrategicTrack.Enabled = false;
            this.cboStrategicTrack.FormattingEnabled = true;
            this.cboStrategicTrack.Location = new System.Drawing.Point(96, 189);
            this.cboStrategicTrack.Name = "cboStrategicTrack";
            this.cboStrategicTrack.Size = new System.Drawing.Size(346, 21);
            this.cboStrategicTrack.Sorted = true;
            this.cboStrategicTrack.TabIndex = 9;
            // 
            // cboDestination
            // 
            this.cboDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestination.Enabled = false;
            this.cboDestination.FormattingEnabled = true;
            this.cboDestination.Location = new System.Drawing.Point(96, 162);
            this.cboDestination.Name = "cboDestination";
            this.cboDestination.Size = new System.Drawing.Size(346, 21);
            this.cboDestination.Sorted = true;
            this.cboDestination.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Strategic Track:";
            // 
            // cboPossessor
            // 
            this.cboPossessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPossessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPossessor.FormattingEnabled = true;
            this.cboPossessor.Location = new System.Drawing.Point(96, 109);
            this.cboPossessor.Name = "cboPossessor";
            this.cboPossessor.Size = new System.Drawing.Size(346, 21);
            this.cboPossessor.Sorted = true;
            this.cboPossessor.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Destination:";
            // 
            // cboOwner
            // 
            this.cboOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOwner.FormattingEnabled = true;
            this.cboOwner.Location = new System.Drawing.Point(96, 56);
            this.cboOwner.Name = "cboOwner";
            this.cboOwner.Size = new System.Drawing.Size(346, 21);
            this.cboOwner.Sorted = true;
            this.cboOwner.TabIndex = 3;
            this.cboOwner.SelectedIndexChanged += new System.EventHandler(this.cboOwner_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Released To:";
            // 
            // txtReportingNumber
            // 
            this.txtReportingNumber.Location = new System.Drawing.Point(159, 30);
            this.txtReportingNumber.Name = "txtReportingNumber";
            this.txtReportingNumber.Size = new System.Drawing.Size(57, 20);
            this.txtReportingNumber.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Owner:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Contents:";
            // 
            // txtContents
            // 
            this.txtContents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContents.Location = new System.Drawing.Point(96, 216);
            this.txtContents.Name = "txtContents";
            this.txtContents.ReadOnly = true;
            this.txtContents.Size = new System.Drawing.Size(346, 20);
            this.txtContents.TabIndex = 10;
            // 
            // cboCurrentLocation
            // 
            this.cboCurrentLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCurrentLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrentLocation.FormattingEnabled = true;
            this.cboCurrentLocation.Location = new System.Drawing.Point(96, 136);
            this.cboCurrentLocation.Name = "cboCurrentLocation";
            this.cboCurrentLocation.Size = new System.Drawing.Size(346, 21);
            this.cboCurrentLocation.Sorted = true;
            this.cboCurrentLocation.TabIndex = 7;
            this.cboCurrentLocation.Visible = false;
            this.cboCurrentLocation.SelectedIndexChanged += new System.EventHandler(this.cboCurrentLocation_SelectedIndexChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.splitContainer1);
            this.tabGeneral.Controls.Add(this.loaderGeneral);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(741, 289);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // loaderGeneral
            // 
            this.loaderGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderGeneral.BackColor = System.Drawing.Color.Transparent;
            this.loaderGeneral.Location = new System.Drawing.Point(0, 0);
            this.loaderGeneral.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loaderGeneral.Name = "loaderGeneral";
            this.loaderGeneral.Size = new System.Drawing.Size(738, 289);
            this.loaderGeneral.TabIndex = 3;
            this.loaderGeneral.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabHistory);
            this.tabControl.Controls.Add(this.tabRoute);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(749, 315);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.cmdNext);
            this.tabHistory.Controls.Add(this.cmdLast);
            this.tabHistory.Controls.Add(this.cmdPrevious);
            this.tabHistory.Controls.Add(this.cmdFirst);
            this.tabHistory.Controls.Add(this.dgvHistory);
            this.tabHistory.Controls.Add(this.loaderHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(741, 289);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "Location History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // cmdNext
            // 
            this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNext.BackgroundImage = global::FleetTracking.Properties.Resources.resultset_next;
            this.cmdNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdNext.Enabled = false;
            this.cmdNext.Location = new System.Drawing.Point(683, 263);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(23, 23);
            this.cmdNext.TabIndex = 3;
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdLast
            // 
            this.cmdLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLast.BackgroundImage = global::FleetTracking.Properties.Resources.resultset_last;
            this.cmdLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdLast.Enabled = false;
            this.cmdLast.Location = new System.Drawing.Point(712, 263);
            this.cmdLast.Name = "cmdLast";
            this.cmdLast.Size = new System.Drawing.Size(23, 23);
            this.cmdLast.TabIndex = 4;
            this.cmdLast.UseVisualStyleBackColor = true;
            this.cmdLast.Click += new System.EventHandler(this.cmdLast_Click);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrevious.BackgroundImage = global::FleetTracking.Properties.Resources.resultset_previous;
            this.cmdPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdPrevious.Enabled = false;
            this.cmdPrevious.Location = new System.Drawing.Point(35, 263);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(23, 23);
            this.cmdPrevious.TabIndex = 2;
            this.cmdPrevious.UseVisualStyleBackColor = true;
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // cmdFirst
            // 
            this.cmdFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdFirst.BackgroundImage = global::FleetTracking.Properties.Resources.resultset_first;
            this.cmdFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdFirst.Enabled = false;
            this.cmdFirst.Location = new System.Drawing.Point(6, 263);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Size = new System.Drawing.Size(23, 23);
            this.cmdFirst.TabIndex = 1;
            this.cmdFirst.UseVisualStyleBackColor = true;
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // dgvHistory
            // 
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colTrain,
            this.colTrack});
            this.dgvHistory.Location = new System.Drawing.Point(3, 3);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 62;
            this.dgvHistory.Size = new System.Drawing.Size(735, 254);
            this.dgvHistory.TabIndex = 0;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 8;
            this.colTime.Name = "colTime";
            this.colTime.Width = 150;
            // 
            // colTrain
            // 
            this.colTrain.HeaderText = "Moved To Train";
            this.colTrain.MinimumWidth = 8;
            this.colTrain.Name = "colTrain";
            this.colTrain.Width = 150;
            // 
            // colTrack
            // 
            this.colTrack.HeaderText = "Moved To Track";
            this.colTrack.MinimumWidth = 8;
            this.colTrack.Name = "colTrack";
            this.colTrack.Width = 150;
            // 
            // loaderHistory
            // 
            this.loaderHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderHistory.BackColor = System.Drawing.Color.Transparent;
            this.loaderHistory.Location = new System.Drawing.Point(0, 0);
            this.loaderHistory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loaderHistory.Name = "loaderHistory";
            this.loaderHistory.Size = new System.Drawing.Size(742, 290);
            this.loaderHistory.TabIndex = 1;
            this.loaderHistory.Visible = false;
            // 
            // tabRoute
            // 
            this.tabRoute.Controls.Add(this.dgvRoute);
            this.tabRoute.Controls.Add(this.loaderRoute);
            this.tabRoute.Location = new System.Drawing.Point(4, 22);
            this.tabRoute.Name = "tabRoute";
            this.tabRoute.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoute.Size = new System.Drawing.Size(741, 289);
            this.tabRoute.TabIndex = 2;
            this.tabRoute.Text = "Route";
            this.tabRoute.UseVisualStyleBackColor = true;
            // 
            // dgvRoute
            // 
            this.dgvRoute.AllowUserToAddRows = false;
            this.dgvRoute.AllowUserToDeleteRows = false;
            this.dgvRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRouteSort,
            this.colRouteFrom,
            this.colRouteTo});
            this.dgvRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoute.Location = new System.Drawing.Point(3, 3);
            this.dgvRoute.Name = "dgvRoute";
            this.dgvRoute.ReadOnly = true;
            this.dgvRoute.RowHeadersVisible = false;
            this.dgvRoute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoute.Size = new System.Drawing.Size(735, 283);
            this.dgvRoute.TabIndex = 0;
            // 
            // loaderRoute
            // 
            this.loaderRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderRoute.BackColor = System.Drawing.Color.Transparent;
            this.loaderRoute.Location = new System.Drawing.Point(3, 3);
            this.loaderRoute.Name = "loaderRoute";
            this.loaderRoute.Size = new System.Drawing.Size(735, 283);
            this.loaderRoute.TabIndex = 1;
            this.loaderRoute.Visible = false;
            // 
            // colRouteSort
            // 
            this.colRouteSort.HeaderText = "Sort";
            this.colRouteSort.Name = "colRouteSort";
            this.colRouteSort.ReadOnly = true;
            this.colRouteSort.Width = 50;
            // 
            // colRouteFrom
            // 
            this.colRouteFrom.HeaderText = "From";
            this.colRouteFrom.Name = "colRouteFrom";
            this.colRouteFrom.ReadOnly = true;
            this.colRouteFrom.Width = 250;
            // 
            // colRouteTo
            // 
            this.colRouteTo.HeaderText = "To";
            this.colRouteTo.Name = "colRouteTo";
            this.colRouteTo.ReadOnly = true;
            this.colRouteTo.Width = 250;
            // 
            // RailcarDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "RailcarDetail";
            this.Size = new System.Drawing.Size(749, 315);
            this.Load += new System.EventHandler(this.RailcarDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.tabRoute.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Loader loaderGeneral;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label4;
        private ControlSelector cboModel;
        private System.Windows.Forms.PictureBox pboxImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button cmdUpdateImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReportingMark;
        private System.Windows.Forms.ComboBox cboOwner;
        private System.Windows.Forms.TextBox txtReportingNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrentLocation;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ComboBox cboPossessor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLessee;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
        private Loader loaderHistory;
        private DataGridViewStylizer dataGridViewStylizer;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdLast;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Button cmdFirst;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtContents;
        private System.Windows.Forms.ComboBox cboStrategicTrack;
        private System.Windows.Forms.ComboBox cboDestination;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboCurrentLocation;
        private System.Windows.Forms.LinkLabel lnkLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrack;
        private System.Windows.Forms.TabPage tabRoute;
        private System.Windows.Forms.DataGridView dgvRoute;
        private Loader loaderRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRouteSort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRouteFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRouteTo;
    }
}
