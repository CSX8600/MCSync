namespace CompanyStudio.Purchasing.Templates
{
    partial class frmTemplateDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplateDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBack = new System.Windows.Forms.ToolStripButton();
            this.toolForward = new System.Windows.Forms.ToolStripButton();
            this.toolUp = new System.Windows.Forms.ToolStripButton();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtPath = new MesaSuite.Common.ToolStripSpringTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolNewFolder = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treFolders = new System.Windows.Forms.TreeView();
            this.ctxTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTreeCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTreeCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTreePaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTreeDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTreeRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTreeNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.lstItems = new System.Windows.Forms.ListView();
            this.ctxListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiListCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiListCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiListPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiListDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiListRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiListNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdActionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.studioFormExtender = new CompanyStudio.StudioFormExtender(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelectedTemplateID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentFolderID = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctxTreeView.SuspendLayout();
            this.ctxListView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBack,
            this.toolForward,
            this.toolUp,
            this.toolRefresh,
            this.toolStripSeparator1,
            this.toolHome,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.txtPath,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.txtSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(727, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolBack
            // 
            this.toolBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBack.Enabled = false;
            this.toolBack.Image = global::CompanyStudio.Properties.Resources.arrow_left;
            this.toolBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBack.Name = "toolBack";
            this.toolBack.Size = new System.Drawing.Size(23, 22);
            this.toolBack.Text = "Back";
            this.toolBack.Click += new System.EventHandler(this.toolBack_Click);
            // 
            // toolForward
            // 
            this.toolForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolForward.Enabled = false;
            this.toolForward.Image = global::CompanyStudio.Properties.Resources.arrow_right;
            this.toolForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolForward.Name = "toolForward";
            this.toolForward.Size = new System.Drawing.Size(23, 22);
            this.toolForward.Text = "Forward";
            this.toolForward.Click += new System.EventHandler(this.toolForward_Click);
            // 
            // toolUp
            // 
            this.toolUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolUp.Enabled = false;
            this.toolUp.Image = global::CompanyStudio.Properties.Resources.arrow_up;
            this.toolUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUp.Name = "toolUp";
            this.toolUp.Size = new System.Drawing.Size(23, 22);
            this.toolUp.Text = "Up Level";
            this.toolUp.Click += new System.EventHandler(this.toolUp_Click);
            // 
            // toolRefresh
            // 
            this.toolRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRefresh.Image = global::CompanyStudio.Properties.Resources.arrow_refresh;
            this.toolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolRefresh.Text = "Refresh";
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolHome
            // 
            this.toolHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolHome.Image = global::CompanyStudio.Properties.Resources.house;
            this.toolHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHome.Name = "toolHome";
            this.toolHome.Size = new System.Drawing.Size(23, 22);
            this.toolHome.Text = "Home";
            this.toolHome.Click += new System.EventHandler(this.toolHome_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel1.Text = "Path:";
            // 
            // txtPath
            // 
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(370, 25);
            this.txtPath.Text = "/";
            this.txtPath.Leave += new System.EventHandler(this.txtPath_Leave);
            this.txtPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyDown);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel2.Text = "Search:";
            this.toolStripLabel2.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 25);
            this.txtSearch.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewFolder});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(727, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolNewFolder
            // 
            this.toolNewFolder.Image = global::CompanyStudio.Properties.Resources.folder_add;
            this.toolNewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewFolder.Name = "toolNewFolder";
            this.toolNewFolder.Size = new System.Drawing.Size(87, 22);
            this.toolNewFolder.Text = "New Folder";
            this.toolNewFolder.Click += new System.EventHandler(this.toolNewFolder_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treFolders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstItems);
            this.splitContainer1.Size = new System.Drawing.Size(727, 317);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 2;
            // 
            // treFolders
            // 
            this.treFolders.AllowDrop = true;
            this.treFolders.ContextMenuStrip = this.ctxTreeView;
            this.treFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treFolders.ImageIndex = 0;
            this.treFolders.ImageList = this.imageList;
            this.treFolders.LabelEdit = true;
            this.treFolders.Location = new System.Drawing.Point(0, 0);
            this.treFolders.Name = "treFolders";
            this.treFolders.SelectedImageIndex = 0;
            this.treFolders.Size = new System.Drawing.Size(168, 317);
            this.treFolders.TabIndex = 0;
            this.treFolders.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treFolders_BeforeLabelEdit);
            this.treFolders.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treFolders_AfterLabelEdit);
            this.treFolders.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treFolders_ItemDrag);
            this.treFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treFolders_AfterSelect);
            this.treFolders.DragDrop += new System.Windows.Forms.DragEventHandler(this.treFolders_DragDrop);
            this.treFolders.DragEnter += new System.Windows.Forms.DragEventHandler(this.treFolders_DragEnter);
            this.treFolders.DragOver += new System.Windows.Forms.DragEventHandler(this.treFolders_DragOver);
            this.treFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treFolders_KeyDown);
            // 
            // ctxTreeView
            // 
            this.ctxTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTreeCut,
            this.tsmiTreeCopy,
            this.tsmiTreePaste,
            this.toolStripSeparator4,
            this.tsmiTreeDelete,
            this.tsmiTreeRename,
            this.toolStripMenuItem3,
            this.tsmiTreeNewFolder});
            this.ctxTreeView.Name = "ctxListView";
            this.ctxTreeView.Size = new System.Drawing.Size(145, 148);
            this.ctxTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.ctxTreeView_Opening);
            // 
            // tsmiTreeCut
            // 
            this.tsmiTreeCut.Name = "tsmiTreeCut";
            this.tsmiTreeCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiTreeCut.Size = new System.Drawing.Size(144, 22);
            this.tsmiTreeCut.Text = "Cut";
            this.tsmiTreeCut.Click += new System.EventHandler(this.tsmiTreeCut_Click);
            // 
            // tsmiTreeCopy
            // 
            this.tsmiTreeCopy.Name = "tsmiTreeCopy";
            this.tsmiTreeCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiTreeCopy.Size = new System.Drawing.Size(144, 22);
            this.tsmiTreeCopy.Text = "Copy";
            this.tsmiTreeCopy.Click += new System.EventHandler(this.tsmiTreeCopy_Click);
            // 
            // tsmiTreePaste
            // 
            this.tsmiTreePaste.Name = "tsmiTreePaste";
            this.tsmiTreePaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmiTreePaste.Size = new System.Drawing.Size(144, 22);
            this.tsmiTreePaste.Text = "Paste";
            this.tsmiTreePaste.Click += new System.EventHandler(this.tsmiTreePaste_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // tsmiTreeDelete
            // 
            this.tsmiTreeDelete.Name = "tsmiTreeDelete";
            this.tsmiTreeDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiTreeDelete.Size = new System.Drawing.Size(144, 22);
            this.tsmiTreeDelete.Text = "Delete";
            this.tsmiTreeDelete.Click += new System.EventHandler(this.tsmiTreeDelete_Click);
            // 
            // tsmiTreeRename
            // 
            this.tsmiTreeRename.Name = "tsmiTreeRename";
            this.tsmiTreeRename.Size = new System.Drawing.Size(144, 22);
            this.tsmiTreeRename.Text = "Rename";
            this.tsmiTreeRename.Click += new System.EventHandler(this.tsmiTreeRename_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(141, 6);
            // 
            // tsmiTreeNewFolder
            // 
            this.tsmiTreeNewFolder.Name = "tsmiTreeNewFolder";
            this.tsmiTreeNewFolder.Size = new System.Drawing.Size(144, 22);
            this.tsmiTreeNewFolder.Text = "New Folder";
            this.tsmiTreeNewFolder.Click += new System.EventHandler(this.tsmiTreeNewFolder_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lstItems
            // 
            this.lstItems.AllowDrop = true;
            this.lstItems.ContextMenuStrip = this.ctxListView;
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.HideSelection = false;
            this.lstItems.LabelEdit = true;
            this.lstItems.LargeImageList = this.imageList;
            this.lstItems.Location = new System.Drawing.Point(0, 0);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(555, 317);
            this.lstItems.SmallImageList = this.imageList;
            this.lstItems.TabIndex = 0;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstItems_AfterLabelEdit);
            this.lstItems.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstItems_ItemDrag);
            this.lstItems.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstItems_ItemSelectionChanged);
            this.lstItems.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstItems_DragDrop);
            this.lstItems.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstItems_DragEnter);
            this.lstItems.DragOver += new System.Windows.Forms.DragEventHandler(this.lstItems_DragOver);
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyDown);
            this.lstItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDoubleClick);
            // 
            // ctxListView
            // 
            this.ctxListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiListCut,
            this.tsmiListCopy,
            this.tsmiListPaste,
            this.toolStripMenuItem1,
            this.tsmiListDelete,
            this.tsmiListRename,
            this.toolStripMenuItem2,
            this.tsmiListNewFolder});
            this.ctxListView.Name = "ctxListView";
            this.ctxListView.Size = new System.Drawing.Size(145, 148);
            this.ctxListView.Opening += new System.ComponentModel.CancelEventHandler(this.ctxListView_Opening);
            // 
            // tsmiListCut
            // 
            this.tsmiListCut.Name = "tsmiListCut";
            this.tsmiListCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiListCut.Size = new System.Drawing.Size(144, 22);
            this.tsmiListCut.Text = "Cut";
            this.tsmiListCut.Click += new System.EventHandler(this.tsmiListCut_Click);
            // 
            // tsmiListCopy
            // 
            this.tsmiListCopy.Name = "tsmiListCopy";
            this.tsmiListCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiListCopy.Size = new System.Drawing.Size(144, 22);
            this.tsmiListCopy.Text = "Copy";
            this.tsmiListCopy.Click += new System.EventHandler(this.tsmiListCopy_Click);
            // 
            // tsmiListPaste
            // 
            this.tsmiListPaste.Name = "tsmiListPaste";
            this.tsmiListPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmiListPaste.Size = new System.Drawing.Size(144, 22);
            this.tsmiListPaste.Text = "Paste";
            this.tsmiListPaste.Click += new System.EventHandler(this.tsmiListPaste_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // tsmiListDelete
            // 
            this.tsmiListDelete.Name = "tsmiListDelete";
            this.tsmiListDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiListDelete.Size = new System.Drawing.Size(144, 22);
            this.tsmiListDelete.Text = "Delete";
            this.tsmiListDelete.Click += new System.EventHandler(this.tsmiListDelete_Click);
            // 
            // tsmiListRename
            // 
            this.tsmiListRename.Name = "tsmiListRename";
            this.tsmiListRename.Size = new System.Drawing.Size(144, 22);
            this.tsmiListRename.Text = "Rename";
            this.tsmiListRename.Click += new System.EventHandler(this.tsmiListRename_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
            // 
            // tsmiListNewFolder
            // 
            this.tsmiListNewFolder.Name = "tsmiListNewFolder";
            this.tsmiListNewFolder.Size = new System.Drawing.Size(144, 22);
            this.tsmiListNewFolder.Text = "New Folder";
            this.tsmiListNewFolder.Click += new System.EventHandler(this.tsmiListNewFolder_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdActionButton);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblCurrentFolderID);
            this.panel1.Controls.Add(this.lblSelectedTemplateID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTemplateName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 367);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 66);
            this.panel1.TabIndex = 3;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(559, 31);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdActionButton
            // 
            this.cmdActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdActionButton.Location = new System.Drawing.Point(640, 31);
            this.cmdActionButton.Name = "cmdActionButton";
            this.cmdActionButton.Size = new System.Drawing.Size(75, 23);
            this.cmdActionButton.TabIndex = 2;
            this.cmdActionButton.UseVisualStyleBackColor = true;
            this.cmdActionButton.Click += new System.EventHandler(this.cmdActionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Template Name:";
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplateName.Location = new System.Drawing.Point(94, 6);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(621, 20);
            this.txtTemplateName.TabIndex = 0;
            this.txtTemplateName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTemplateName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selected Template ID:";
            // 
            // lblSelectedTemplateID
            // 
            this.lblSelectedTemplateID.AutoSize = true;
            this.lblSelectedTemplateID.Location = new System.Drawing.Point(122, 36);
            this.lblSelectedTemplateID.Name = "lblSelectedTemplateID";
            this.lblSelectedTemplateID.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedTemplateID.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Current Folder ID:";
            // 
            // lblCurrentFolderID
            // 
            this.lblCurrentFolderID.AutoSize = true;
            this.lblCurrentFolderID.Location = new System.Drawing.Point(337, 36);
            this.lblCurrentFolderID.Name = "lblCurrentFolderID";
            this.lblCurrentFolderID.Size = new System.Drawing.Size(31, 13);
            this.lblCurrentFolderID.TabIndex = 1;
            this.lblCurrentFolderID.Text = "[Null]";
            // 
            // frmTemplateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(727, 433);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTemplateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save Template";
            this.Load += new System.EventHandler(this.frmSaveTemplateDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctxTreeView.ResumeLayout(false);
            this.ctxListView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBack;
        private System.Windows.Forms.ToolStripButton toolForward;
        private System.Windows.Forms.ToolStripButton toolUp;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolHome;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private MesaSuite.Common.ToolStripSpringTextBox txtPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolNewFolder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treFolders;
        private System.Windows.Forms.ListView lstItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdActionButton;
        private StudioFormExtender studioFormExtender;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip ctxListView;
        private System.Windows.Forms.ToolStripMenuItem tsmiListCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiListCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiListPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiListDelete;
        private System.Windows.Forms.ContextMenuStrip ctxTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsmiTreeCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiTreeCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiTreePaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiTreeDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiTreeRename;
        private System.Windows.Forms.ToolStripMenuItem tsmiListRename;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiListNewFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiTreeNewFolder;
        private System.Windows.Forms.Label lblSelectedTemplateID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCurrentFolderID;
    }
}