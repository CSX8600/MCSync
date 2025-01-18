using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Purchasing.Templates
{
    public partial class frmTemplateDialog : Form
    {
        private const string FOLDER = nameof(FOLDER);
        private const string FILE = nameof(FILE);
        private MultiMap<long?, PurchaseOrderTemplateFolder> foldersByFolder = new MultiMap<long?, PurchaseOrderTemplateFolder>();
        private MultiMap<long?, PurchaseOrderTemplate> templatesByFolder = new MultiMap<long?, PurchaseOrderTemplate>();
        private long? _selectedFolderID = -1L;
        private bool _suppressBreadcrumbUpdate = false;
        private long? SelectedFolderID
        {
            get => _selectedFolderID;
            set
            {
                _selectedFolderID = value;
                if (!_suppressBreadcrumbUpdate)
                {
                    AddBreadcrumb(value);
                }
                if (_selectedFolderID == -1)
                {
                    lblCurrentFolderID.Text = "[Null]";
                }
                else
                {
                    lblCurrentFolderID.Text = _selectedFolderID.ToString();
                }
                _suppressBreadcrumbUpdate = false;
            }
        }
        private List<long?> folderIDBreadcrumbs = new List<long?>() { -1L };
        private int folderIDBreadcrumbsIndex = 0;

        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public ThemeBase Theme { get; set; }
        public enum DialogModes
        {
            Open,
            Save
        }
        public DialogModes DialogMode { get; set; } = DialogModes.Open;
        public PurchaseOrderTemplate SelectedTemplate { get; set; }

        public frmTemplateDialog()
        {
            InitializeComponent();
            imageList.Images.Add(FOLDER, Properties.Resources.folder);
            imageList.Images.Add(FILE, Properties.Resources.page);
        }

        private async void frmSaveTemplateDialog_Load(object sender, EventArgs e)
        {
            studioFormExtender.ApplyStyle(this, Theme);
            cmdActionButton.Text = DialogMode == DialogModes.Open ? "Open" : "Save";
            await ReloadAllData();
        }

        private async Task ReloadAllData()
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/GetAll");
            get.AddLocationHeader(CompanyID, LocationID);
            List<PurchaseOrderTemplateFolder> folders = await get.GetObject<List<PurchaseOrderTemplateFolder>>() ?? new List<PurchaseOrderTemplateFolder>();
            foldersByFolder = new MultiMap<long?, PurchaseOrderTemplateFolder>()
            {
                { -1L, new HashSet<PurchaseOrderTemplateFolder>() }
            };
            templatesByFolder = new MultiMap<long?, PurchaseOrderTemplate>()
            {
                { -1L, new HashSet<PurchaseOrderTemplate>() }
            };

            folders.ForEach(f => foldersByFolder.Add(f.PurchaseOrderTemplateFolderID ?? -1L, new HashSet<PurchaseOrderTemplateFolder>()));
            folders.ForEach(f => foldersByFolder.Add(f.PurchaseOrderTemplateFolderIDParent ?? -1L, f));
            folders.ForEach(f => templatesByFolder.Add(f.PurchaseOrderTemplateFolderID ?? -1L, new HashSet<PurchaseOrderTemplate>()));
            RefreshTree(folders);

            get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/GetAll");
            get.AddLocationHeader(CompanyID, LocationID);
            List<PurchaseOrderTemplate> templates = await get.GetObject<List<PurchaseOrderTemplate>>() ?? new List<PurchaseOrderTemplate>();
            templates.ForEach(t => templatesByFolder.Add(t.PurchaseOrderTemplateFolderID ?? -1L, t));

            LoadListItems();
        }

        private void RefreshTree(List<PurchaseOrderTemplateFolder> folders)
        {
            treFolders.Nodes.Clear();

            TreeNode root = new TreeNode("/");
            root.ImageKey = FOLDER;
            root.SelectedImageKey = FOLDER;
            foreach (PurchaseOrderTemplateFolder folder in folders.Where(f => f.PurchaseOrderTemplateFolderIDParent == null).OrderBy(f => f.Name))
            {
                folder.FolderPath = "/" + folder.Name + "/";

                TreeNode node = new TreeNode(folder.Name);
                node.ImageKey = FOLDER;
                node.SelectedImageKey = FOLDER;
                node.Tag = folder;
                AddChildFolders(node, folder.PurchaseOrderTemplateFolderID, folders, folder.FolderPath);
                root.Nodes.Add(node);
            }
            treFolders.Nodes.Add(root);

            _suppressTreeAfterSelect = true;
            if (SelectedFolderID == -1L)
            {
                treFolders.SelectedNode = root;
                root.Expand();
            }
            else
            {
                treFolders.SelectedNode = root.Nodes.AllNodes().FirstOrDefault(n => (n.Tag as PurchaseOrderTemplateFolder)?.PurchaseOrderTemplateFolderID == SelectedFolderID);
                if (treFolders.SelectedNode != null)
                {
                    treFolders.SelectedNode.Expand();
                    treFolders.SelectedNode.EnsureVisible();
                }
            }
            _suppressTreeAfterSelect = false;

            txtPath.AutoCompleteCustomSource.Clear();
            txtPath.AutoCompleteCustomSource.AddRange(folders.Select(f => f.FolderPath).OrderBy(p => p).ToArray());
        }

        private void AddChildFolders(TreeNode parentFolder, long? parentFolderID, List<PurchaseOrderTemplateFolder> folders, string folderPathPrefix)
        {
            foreach(PurchaseOrderTemplateFolder folder in folders.Where(f => f.PurchaseOrderTemplateFolderIDParent == parentFolderID).OrderBy(f => f.Name))
            {
                folder.FolderPath = folderPathPrefix + folder.Name + "/";

                TreeNode node = new TreeNode(folder.Name);
                node.Tag = folder;
                node.ImageKey = FOLDER;
                node.SelectedImageKey = FOLDER;
                AddChildFolders(node, folder.PurchaseOrderTemplateFolderID, folders, folder.FolderPath);
                parentFolder.Nodes.Add(node);
            }
        }

        private void LoadListItems()
        {
            lstItems.Items.Clear();

            PurchaseOrderTemplateFolder selectedFolder = foldersByFolder.Values.SelectMany(hs => hs).FirstOrDefault(f => f.PurchaseOrderTemplateFolderID == SelectedFolderID);

            txtPath.Text = selectedFolder?.FolderPath ?? "/";
            toolUp.Enabled = selectedFolder != null;

            TreeNode node = treFolders.Nodes.AllNodes().FirstOrDefault(n => n.Tag is PurchaseOrderTemplateFolder folder && folder.PurchaseOrderTemplateFolderID == SelectedFolderID);
            if (node == null)
            {
                node = treFolders.Nodes[0];
            }
            _suppressTreeAfterSelect = true;
            treFolders.SelectedNode = node;
            _suppressTreeAfterSelect = false;

            foreach (PurchaseOrderTemplateFolder folder in foldersByFolder.GetOrDefault(SelectedFolderID, new HashSet<PurchaseOrderTemplateFolder>()).OrderBy(f => f.Name))
            {
                ListViewItem folderItem = new ListViewItem(folder.Name);
                folderItem.ImageKey = FOLDER;
                folderItem.Tag = folder;
                lstItems.Items.Add(folderItem);
            }

            foreach (PurchaseOrderTemplate template in templatesByFolder.GetOrDefault(SelectedFolderID, new HashSet<PurchaseOrderTemplate>()).OrderBy(f => f.Name))
            {
                ListViewItem item = new ListViewItem(template.Name);
                item.ImageKey = FILE;
                item.Tag = template;
                lstItems.Items.Add(item);
            }

            lblSelectedTemplateID.Text = string.Empty;
        }

        private void AddBreadcrumb(long? folderID)
        {
            if (folderIDBreadcrumbsIndex < folderIDBreadcrumbs.Count && folderIDBreadcrumbs.ElementAt(folderIDBreadcrumbsIndex) == folderID)
            {
                return;
            }

            if (folderIDBreadcrumbsIndex < folderIDBreadcrumbs.Count - 1)
            {
                folderIDBreadcrumbs.RemoveRange(folderIDBreadcrumbsIndex + 1, folderIDBreadcrumbs.Count - folderIDBreadcrumbsIndex - 1);

            }
            folderIDBreadcrumbs.Add(folderID);
            folderIDBreadcrumbsIndex = folderIDBreadcrumbs.Count - 1;

            toolForward.Enabled = false;
            toolBack.Enabled = true;
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lstItems.GetItemAt(e.X, e.Y);
            if (item == null || !item.Selected)
            {
                return;
            }

            PurchaseOrderTemplateFolder folder = item.Tag as PurchaseOrderTemplateFolder;
            PurchaseOrderTemplate template = item.Tag as PurchaseOrderTemplate;
            if (template != null)
            {
                cmdActionButton.PerformClick();
                return;
            }

            if (folder == null)
            {
                return;
            }

            SelectedFolderID = folder.PurchaseOrderTemplateFolderID;
            RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
            LoadListItems();
        }

        private void toolUp_Click(object sender, EventArgs e)
        {
            PurchaseOrderTemplateFolder folder = foldersByFolder.Values.SelectMany(hs => hs).FirstOrDefault(f => f.PurchaseOrderTemplateFolderID == SelectedFolderID);

            SelectedFolderID = folder.PurchaseOrderTemplateFolderIDParent ?? -1L;
            LoadListItems();
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Return)
            {
                return;
            }

            long? folderID = foldersByFolder.Values.SelectMany(hs => hs).FirstOrDefault(f => f.FolderPath == txtPath.Text)?.PurchaseOrderTemplateFolderID;
            if (folderID == null && !txtPath.Text.Equals("/"))
            {
                return;
            }
            SelectedFolderID = folderID ?? -1L;
            LoadListItems();
        }

        private void txtPath_Leave(object sender, EventArgs e)
        {
            txtPath.Text = foldersByFolder.Values.SelectMany(hs => hs).FirstOrDefault(f => f.PurchaseOrderTemplateFolderID == SelectedFolderID)?.FolderPath;
        }

        private void lstItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lstItems.SelectedItems.Count > 0)
            {
                PurchaseOrderTemplate selectedTemplate = lstItems.SelectedItems[lstItems.SelectedItems.Count - 1].Tag as PurchaseOrderTemplate;
                lblSelectedTemplateID.Text = selectedTemplate?.PurchaseOrderTemplateID.ToString();
                txtTemplateName.Text = lstItems.SelectedItems[lstItems.SelectedItems.Count - 1].Text;
            }
            else
            {
                lblSelectedTemplateID.Text = string.Empty;
            }
        }

        [Serializable]
        private class DragAndDropLists
        {
            public List<PurchaseOrderTemplateFolder> Folders { get; set; } = new List<PurchaseOrderTemplateFolder>();
            public List<PurchaseOrderTemplate> Templates { get; set; } = new List<PurchaseOrderTemplate>();
            public enum Modes
            {
                Cut,
                Copy
            }
            public Modes Mode { get; set; } = Modes.Cut;
        }

        #region List View D&D
        private void lstItems_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (lstItems.SelectedItems.Count <= 0)
            {
                return;
            }

            DragAndDropLists dragAndDropLists = new DragAndDropLists()
            {
                Folders = lstItems.SelectedItems.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplateFolder).Select(lvi => lvi.Tag as PurchaseOrderTemplateFolder).ToList(),
                Templates = lstItems.SelectedItems.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplate).Select(lvi => lvi.Tag as PurchaseOrderTemplate).ToList()
            };

            lstItems.DoDragDrop(dragAndDropLists, DragDropEffects.Move);
        }

        private void lstItems_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragAndDropLists)))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void lstItems_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragAndDropLists)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            DragAndDropLists lists = e.Data.GetData(typeof(DragAndDropLists)) as DragAndDropLists;
            Point clientPoint = lstItems.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoveredItem = lstItems.GetItemAt(clientPoint.X, clientPoint.Y);
            if (hoveredItem == null || !(hoveredItem.Tag is PurchaseOrderTemplateFolder folder) || lists.Folders.Contains(folder))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private async void lstItems_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragAndDropLists)))
            {
                return;
            }

            DragAndDropLists lists = e.Data.GetData(typeof(DragAndDropLists)) as DragAndDropLists;
            Point clientPoint = lstItems.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoveredItem = lstItems.GetItemAt(clientPoint.X, clientPoint.Y);

            if (hoveredItem == null || !(hoveredItem.Tag is PurchaseOrderTemplateFolder folder) || lists.Folders.Contains(folder))
            {
                return;
            }

            await MoveDragAndDropLists(lists, folder);
        }

        private async Task MoveDragAndDropLists(DragAndDropLists lists, PurchaseOrderTemplateFolder destinationFolder)
        {
            if (lists.Mode == DragAndDropLists.Modes.Cut)
            {
                foreach (PurchaseOrderTemplateFolder folderToMove in lists.Folders)
                {
                    PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Patch", PatchData.PatchMethods.Replace, folderToMove.PurchaseOrderTemplateFolderID, new Dictionary<string, object>()
                    {
                        { nameof(PurchaseOrderTemplateFolder.PurchaseOrderTemplateFolderIDParent), destinationFolder?.PurchaseOrderTemplateFolderID }
                    });
                    patch.AddLocationHeader(CompanyID, LocationID);
                    await patch.Execute();

                    if (patch.RequestSuccessful)
                    {
                        foldersByFolder[folderToMove.PurchaseOrderTemplateFolderIDParent ?? -1L].Remove(folderToMove);
                        folderToMove.PurchaseOrderTemplateFolderIDParent = destinationFolder?.PurchaseOrderTemplateFolderID;
                        foldersByFolder.Add(folderToMove.PurchaseOrderTemplateFolderIDParent ?? -1L, folderToMove);
                    }
                }
            }
            else if (lists.Mode == DragAndDropLists.Modes.Copy)
            {
                Dictionary<long?, long?> newParentFolderMap = new Dictionary<long?, long?>()
                {
                    { destinationFolder?.PurchaseOrderTemplateFolderID ?? -1L, destinationFolder?.PurchaseOrderTemplateFolderID ?? -1L }
                };

                async Task CopyChildContents(long? newFolderID)
                {
                    PurchaseOrderTemplateFolder newParentFolder = foldersByFolder.Values.SelectMany(h => h).First(f => f.PurchaseOrderTemplateFolderID == newFolderID);
                    foreach(PurchaseOrderTemplateFolder folderToMove in foldersByFolder[newParentFolderMap[newFolderID]])
                    {
                        PurchaseOrderTemplateFolder newFolder = new PurchaseOrderTemplateFolder()
                        {
                            LocationID = LocationID,
                            Name = folderToMove.Name,
                            FolderPath = newParentFolder.FolderPath + folderToMove.Name + "/",
                            PurchaseOrderTemplateFolderIDParent = newFolderID
                        };

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Post", newFolder);
                        post.AddLocationHeader(CompanyID, LocationID);
                        newFolder = await post.Execute<PurchaseOrderTemplateFolder>();

                        if (post.RequestSuccessful)
                        {
                            foldersByFolder[newFolderID].Add(newFolder);
                            foldersByFolder.Add(newFolder.PurchaseOrderTemplateFolderID, new HashSet<PurchaseOrderTemplateFolder>());
                            templatesByFolder.Add(newFolder.PurchaseOrderTemplateFolderID, new HashSet<PurchaseOrderTemplate>());
                            newParentFolderMap.Add(newFolder.PurchaseOrderTemplateFolderID, folderToMove.PurchaseOrderTemplateFolderID);

                            await CopyChildContents(newFolder.PurchaseOrderTemplateFolderID);
                        }
                    }

                    foreach(PurchaseOrderTemplate templateToMove in templatesByFolder[newParentFolderMap[newFolderID]])
                    {
                        PurchaseOrderTemplate newTemplate = new PurchaseOrderTemplate()
                        {
                            LocationID = LocationID,
                            Name = templateToMove.Name,
                            PurchaseOrderID = templateToMove.PurchaseOrderID,
                            PurchaseOrderTemplateFolderID = newFolderID
                        };

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Post", newTemplate);
                        post.AddLocationHeader(CompanyID, LocationID);
                        newTemplate = await post.Execute<PurchaseOrderTemplate>();

                        if (post.RequestSuccessful)
                        {
                            templatesByFolder[newFolderID].Add(newTemplate);
                        }
                    }
                };

                foreach (PurchaseOrderTemplateFolder folderToMove in lists.Folders)
                {
                    string newName = folderToMove.Name;
                    int i = 1;
                    while (foldersByFolder.GetOrDefault(destinationFolder?.PurchaseOrderTemplateFolderID ?? -1L, new HashSet<PurchaseOrderTemplateFolder>()).Any(f => f.Name == newName))
                    {
                        newName = $"{folderToMove.Name} ({i++})";
                    }

                    PurchaseOrderTemplateFolder newFolder = new PurchaseOrderTemplateFolder()
                    {
                        LocationID = LocationID,
                        Name = newName,
                        FolderPath = (destinationFolder?.FolderPath ?? "/") + newName + "/",
                        PurchaseOrderTemplateFolderIDParent = destinationFolder?.PurchaseOrderTemplateFolderID
                    };

                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Post", newFolder);
                    post.AddLocationHeader(CompanyID, LocationID);
                    newFolder = await post.Execute<PurchaseOrderTemplateFolder>();

                    if (post.RequestSuccessful)
                    {
                        foldersByFolder[destinationFolder?.PurchaseOrderTemplateFolderID ?? -1L].Add(newFolder);
                        foldersByFolder.Add(newFolder.PurchaseOrderTemplateFolderID, new HashSet<PurchaseOrderTemplateFolder>());
                        templatesByFolder.Add(newFolder.PurchaseOrderTemplateFolderID, new HashSet<PurchaseOrderTemplate>());
                        newParentFolderMap.Add(newFolder.PurchaseOrderTemplateFolderID, folderToMove.PurchaseOrderTemplateFolderID);

                        await CopyChildContents(newFolder.PurchaseOrderTemplateFolderID);
                    }
                }
            }

            RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());

            foreach (PurchaseOrderTemplate template in lists.Templates)
            {
                if (lists.Mode == DragAndDropLists.Modes.Cut)
                {
                    PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Patch", PatchData.PatchMethods.Replace, template.PurchaseOrderTemplateID, new Dictionary<string, object>()
                    {
                        { nameof(PurchaseOrderTemplate.PurchaseOrderTemplateFolderID), destinationFolder?.PurchaseOrderTemplateFolderID }
                    });
                    patch.AddLocationHeader(CompanyID, LocationID);
                    await patch.Execute();

                    if (patch.RequestSuccessful)
                    {
                        templatesByFolder[template.PurchaseOrderTemplateFolderID ?? -1L].Remove(template);
                        template.PurchaseOrderTemplateFolderID = destinationFolder?.PurchaseOrderTemplateFolderID;
                        templatesByFolder.Add(template.PurchaseOrderTemplateFolderID ?? -1L, template);
                    }
                }
                else if (lists.Mode == DragAndDropLists.Modes.Copy)
                {
                    string newName = template.Name;
                    int i = 1;
                    while (templatesByFolder.GetOrDefault(destinationFolder?.PurchaseOrderTemplateFolderID ?? -1L, new HashSet<PurchaseOrderTemplate>()).Any(f => f.Name == newName))
                    {
                        newName = $"{template.Name} ({i++})";
                    }

                    PurchaseOrderTemplate newTemplate = new PurchaseOrderTemplate()
                    {
                        LocationID = LocationID,
                        Name = newName,
                        PurchaseOrderTemplateFolderID = destinationFolder?.PurchaseOrderTemplateFolderID,
                        PurchaseOrderID = template.PurchaseOrderID
                    };

                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Post", newTemplate);
                    post.AddLocationHeader(CompanyID, LocationID);
                    newTemplate = await post.Execute<PurchaseOrderTemplate>();

                    if (post.RequestSuccessful)
                    {
                        templatesByFolder[destinationFolder?.PurchaseOrderTemplateFolderID ?? -1L].Add(newTemplate);
                    }
                }
            }

            LoadListItems();
        }
        #endregion

        #region Tree View D&D
        private void treFolders_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (!(((TreeNode)e.Item).Tag is PurchaseOrderTemplateFolder folder))
            {
                return;
            }

            treFolders.DoDragDrop(folder, DragDropEffects.Move);
        }

        private void treFolders_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(PurchaseOrderTemplateFolder)) || !e.Data.GetDataPresent(typeof(DragAndDropLists)))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void treFolders_DragOver(object sender, DragEventArgs e)
        {
            Point clientPoint = treFolders.PointToClient(new Point(e.X, e.Y));
            TreeNode hoveredNode = treFolders.GetNodeAt(clientPoint.X, clientPoint.Y);
            if (hoveredNode == null)
            {
                treFolders.SelectedNode = null;
                e.Effect = DragDropEffects.None;
                return;
            }

            DragAndDropLists lists = e.Data.GetDataPresent(typeof(DragAndDropLists)) ? e.Data.GetData(typeof(DragAndDropLists)) as DragAndDropLists : null;
            PurchaseOrderTemplateFolder draggingFolder = e.Data.GetDataPresent(typeof(PurchaseOrderTemplateFolder)) ? e.Data.GetData(typeof(PurchaseOrderTemplateFolder)) as PurchaseOrderTemplateFolder : null;

            PurchaseOrderTemplateFolder destinationFolder = hoveredNode.Tag as PurchaseOrderTemplateFolder;
            if (destinationFolder != null && ((lists != null && lists.Folders.Contains(destinationFolder)) || (draggingFolder != null && draggingFolder == destinationFolder)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (lists != null)
            {
                foreach(PurchaseOrderTemplateFolder folder in lists.Folders)
                {
                    TreeNode node = treFolders.Nodes.AllNodes().FirstOrDefault(n => (n.Tag as PurchaseOrderTemplateFolder)?.PurchaseOrderTemplateFolderID == folder.PurchaseOrderTemplateFolderID);
                    TreeNode childNode = hoveredNode;
                    do
                    {
                        if (node == childNode.Parent)
                        {
                            return;
                        }
                        childNode = childNode.Parent;
                    }
                    while (childNode != null);
                }
            }

            if (draggingFolder != null)
            {
                TreeNode node = treFolders.Nodes.AllNodes().FirstOrDefault(n => (n.Tag as PurchaseOrderTemplateFolder)?.PurchaseOrderTemplateFolderID == draggingFolder.PurchaseOrderTemplateFolderID);
                TreeNode childNode = hoveredNode;
                do
                {
                    if (node == childNode.Parent)
                    {
                        return;
                    }
                    childNode = childNode.Parent;
                }
                while (childNode != null);
            }

            e.Effect = DragDropEffects.Move;
        }

        private async void treFolders_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = treFolders.PointToClient(new Point(e.X, e.Y));
            TreeNode hoveredNode = treFolders.GetNodeAt(clientPoint.X, clientPoint.Y);
            if (hoveredNode == null)
            {
                return;
            }

            DragAndDropLists lists = e.Data.GetDataPresent(typeof(DragAndDropLists)) ? e.Data.GetData(typeof(DragAndDropLists)) as DragAndDropLists : null;
            PurchaseOrderTemplateFolder draggingFolder = e.Data.GetDataPresent(typeof(PurchaseOrderTemplateFolder)) ? e.Data.GetData(typeof(PurchaseOrderTemplateFolder)) as PurchaseOrderTemplateFolder : null;

            PurchaseOrderTemplateFolder destinationFolder = hoveredNode.Tag as PurchaseOrderTemplateFolder;
            if (destinationFolder != null && ((lists != null && lists.Folders.Contains(destinationFolder)) || (draggingFolder != null && draggingFolder == destinationFolder)))
            {
                return;
            }

            if (lists != null)
            {
                foreach (PurchaseOrderTemplateFolder folder in lists.Folders)
                {
                    TreeNode node = treFolders.Nodes.AllNodes().FirstOrDefault(n => (n.Tag as PurchaseOrderTemplateFolder)?.PurchaseOrderTemplateFolderID == folder.PurchaseOrderTemplateFolderID);
                    TreeNode childNode = hoveredNode;
                    do
                    {
                        if (node == childNode.Parent)
                        {
                            return;
                        }
                        childNode = childNode.Parent;
                    }
                    while (childNode != null);
                }

                await MoveDragAndDropLists(lists, destinationFolder);
            }
            else
            {
                TreeNode node = treFolders.Nodes.AllNodes().FirstOrDefault(n => (n.Tag as PurchaseOrderTemplateFolder)?.PurchaseOrderTemplateFolderID == draggingFolder.PurchaseOrderTemplateFolderID);
                TreeNode childNode = hoveredNode;
                do
                {
                    if (node == childNode.Parent)
                    {
                        return;
                    }
                    childNode = childNode.Parent;
                }
                while (childNode != null);
                await MoveDragAndDropLists(new DragAndDropLists() { Folders = { draggingFolder } }, destinationFolder);
            }
        }
        #endregion

        private async void lstItems_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }

            ListViewItem item = lstItems.Items[e.Item];

            PurchaseOrderTemplateFolder folder = item.Tag as PurchaseOrderTemplateFolder;
            PurchaseOrderTemplate template = item.Tag as PurchaseOrderTemplate;
            if (folder != null)
            {
                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Patch", PatchData.PatchMethods.Replace, folder.PurchaseOrderTemplateFolderID, new Dictionary<string, object>()
                {
                    { nameof(PurchaseOrderTemplateFolder.Name), e.Label }
                });
                patch.AddLocationHeader(CompanyID, LocationID);
                await patch.Execute();
                if (!patch.RequestSuccessful)
                {
                    e.CancelEdit = true;
                    item.Text = folder.Name;
                    return;
                }

                folder.Name = e.Label;
                RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
            }
            else if (template != null)
            {
                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Patch", PatchData.PatchMethods.Replace, template.PurchaseOrderTemplateID, new Dictionary<string, object>()
                {
                    { nameof(PurchaseOrderTemplate.Name), e.Label }
                });
                patch.AddLocationHeader(CompanyID, LocationID);
                await patch.Execute();
                if (!patch.RequestSuccessful)
                {
                    e.CancelEdit = true;
                    item.Text = template.Name;
                    return;
                }

                template.Name = e.Label;
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private async void treFolders_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }

            PurchaseOrderTemplateFolder folder = e.Node.Tag as PurchaseOrderTemplateFolder;
            if (folder != null)
            {
                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Patch", PatchData.PatchMethods.Replace, folder.PurchaseOrderTemplateFolderID, new Dictionary<string, object>()
                {
                    { nameof(PurchaseOrderTemplateFolder.Name), e.Label }
                });
                patch.AddLocationHeader(CompanyID, LocationID);
                await patch.Execute();
                if (!patch.RequestSuccessful)
                {
                    e.CancelEdit = true;
                    e.Node.Text = folder.Name;
                    return;
                }
                folder.Name = e.Label;
                RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
                LoadListItems();
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private void treFolders_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!(e.Node.Tag is PurchaseOrderTemplateFolder))
            {
                e.CancelEdit = true;
            }
        }

        private bool _suppressTreeAfterSelect = false;
        private void treFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_suppressTreeAfterSelect)
            {
                _suppressTreeAfterSelect = false;
                return;
            }

            PurchaseOrderTemplateFolder folder = e.Node.Tag as PurchaseOrderTemplateFolder;

            SelectedFolderID = folder?.PurchaseOrderTemplateFolderID ?? -1L;
            lblSelectedTemplateID.Text = string.Empty;
            LoadListItems();
        }

        private void toolBack_Click(object sender, EventArgs e)
        {
            folderIDBreadcrumbsIndex--;
            if (folderIDBreadcrumbsIndex < 0) folderIDBreadcrumbsIndex = 0;

            _suppressBreadcrumbUpdate = true;
            SelectedFolderID = folderIDBreadcrumbs[folderIDBreadcrumbsIndex];
            toolBack.Enabled = folderIDBreadcrumbsIndex > 0;
            toolForward.Enabled = folderIDBreadcrumbsIndex < folderIDBreadcrumbs.Count - 1;

            LoadListItems();
        }

        private void toolForward_Click(object sender, EventArgs e)
        {
            folderIDBreadcrumbsIndex++;
            if (folderIDBreadcrumbsIndex >= folderIDBreadcrumbs.Count) folderIDBreadcrumbsIndex = folderIDBreadcrumbs.Count - 1;

            _suppressBreadcrumbUpdate = true;
            SelectedFolderID = folderIDBreadcrumbs[folderIDBreadcrumbsIndex];
            toolBack.Enabled = folderIDBreadcrumbsIndex > 0;
            toolForward.Enabled = folderIDBreadcrumbsIndex < folderIDBreadcrumbs.Count - 1;

            LoadListItems();
        }

        private async void toolRefresh_Click(object sender, EventArgs e)
        {
            await ReloadAllData();
        }

        private void toolHome_Click(object sender, EventArgs e)
        {
            _suppressBreadcrumbUpdate = true;
            SelectedFolderID = -1L;
            folderIDBreadcrumbs.Clear();
            folderIDBreadcrumbs.Add(-1L);
            folderIDBreadcrumbsIndex = 0;
            toolBack.Enabled = false;
            toolForward.Enabled = false;
            LoadListItems();
        }

        private void lstItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                lstItems_CopyToClipboard();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                lstItems_PasteFromClipboard();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                lstItems_CutToClipboard();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                lstItems_DeleteItems();
            }
            else if (e.KeyCode == Keys.Back && toolBack.Enabled)
            {
                toolBack.PerformClick();
            }
            else if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) && lstItems.SelectedItems.Count > 0 && lstItems.SelectedItems[0].Tag is PurchaseOrderTemplateFolder folderToOpen)
            {
                SelectedFolderID = folderToOpen.PurchaseOrderTemplateFolderID;
                RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
                LoadListItems();
            }
        }

        private void lstItems_CopyToClipboard()
        {
            DragAndDropLists dragAndDropLists = new DragAndDropLists()
            {
                Folders = lstItems.SelectedItems.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplateFolder).Select(lvi => lvi.Tag as PurchaseOrderTemplateFolder).ToList() ?? new List<PurchaseOrderTemplateFolder>(),
                Templates = lstItems.SelectedItems.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplate).Select(lvi => lvi.Tag as PurchaseOrderTemplate).ToList() ?? new List<PurchaseOrderTemplate>(),
                Mode = DragAndDropLists.Modes.Copy
            };

            Clipboard.SetData(typeof(DragAndDropLists).FullName, dragAndDropLists);
        }

        private void lstItems_CutToClipboard()
        {
            DragAndDropLists dragAndDropLists = new DragAndDropLists()
            {
                Folders = lstItems.SelectedItems.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplateFolder).Select(lvi => lvi.Tag as PurchaseOrderTemplateFolder).ToList() ?? new List<PurchaseOrderTemplateFolder>(),
                Templates = lstItems.SelectedItems.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplate).Select(lvi => lvi.Tag as PurchaseOrderTemplate).ToList() ?? new List<PurchaseOrderTemplate>()
            };
            Clipboard.SetData(typeof(DragAndDropLists).FullName, dragAndDropLists);
        }

        private async void lstItems_PasteFromClipboard()
        {
            if (Clipboard.ContainsData(typeof(DragAndDropLists).FullName))
            {
                await MoveDragAndDropLists(Clipboard.GetData(typeof(DragAndDropLists).FullName) as DragAndDropLists, foldersByFolder.Values.SelectMany(h => h).FirstOrDefault(f => f.PurchaseOrderTemplateFolderID == SelectedFolderID));
            }
        }

        private async void lstItems_DeleteItems()
        {
            if (lstItems.SelectedItems.Count <= 0 || !this.Confirm("Are you sure you want to delete these items?"))
            {
                return;
            }

            foreach(ListViewItem lvi in lstItems.SelectedItems)
            {
                PurchaseOrderTemplateFolder folder = lvi.Tag as PurchaseOrderTemplateFolder;
                PurchaseOrderTemplate template = lvi.Tag as PurchaseOrderTemplate;

                if (folder != null)
                {
                    DeleteData deleteFolder = new DeleteData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Delete/" + folder.PurchaseOrderTemplateFolderID);
                    deleteFolder.AddLocationHeader(CompanyID, LocationID);
                    await deleteFolder.Execute();

                    foldersByFolder[folder.PurchaseOrderTemplateFolderIDParent ?? -1L].Remove(folder);
                    templatesByFolder.Remove(folder.PurchaseOrderTemplateFolderID);
                }
                else if (template != null)
                {
                    DeleteData deleteTemplate = new DeleteData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Delete/" + template.PurchaseOrderTemplateID);
                    deleteTemplate.AddLocationHeader(CompanyID, LocationID);
                    await deleteTemplate.Execute();

                    templatesByFolder[template.PurchaseOrderTemplateFolderID ?? -1L].Remove(template);
                }
            }

            RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());

            LoadListItems();
        }

        private void ctxListView_Opening(object sender, CancelEventArgs e)
        {
            tsmiListCut.Enabled = lstItems.SelectedItems.Count > 0;
            tsmiListCopy.Enabled = lstItems.SelectedItems.Count > 0;
            tsmiListDelete.Enabled = lstItems.SelectedItems.Count > 0;
            tsmiListPaste.Enabled = Clipboard.ContainsData(typeof(DragAndDropLists).FullName);
            tsmiListRename.Enabled = lstItems.SelectedItems.Count > 0;
        }

        private void tsmiListCut_Click(object sender, EventArgs e)
        {
            lstItems_CutToClipboard();
        }

        private void tsmiListCopy_Click(object sender, EventArgs e)
        {
            lstItems_CopyToClipboard();
        }

        private void tsmiListPaste_Click(object sender, EventArgs e)
        {
            lstItems_PasteFromClipboard();
        }

        private void tsmiListDelete_Click(object sender, EventArgs e)
        {
            lstItems_DeleteItems();
        }

        private void treFolders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch(e.KeyCode)
                {
                    case Keys.C:
                        treFolders_CopyToClipboard();
                        break;
                    case Keys.V:
                        treFolders_PasteFromClipboard();
                        break;
                    case Keys.X:
                        treFolders_CutToClipboard();
                        break;
                }
            }

            switch(e.KeyCode)
            {
                case Keys.Delete:
                    treFolders_DeleteFolder();
                    break;
            }
        }

        private void treFolders_CopyToClipboard()
        {
            if (treFolders.SelectedNode == null || !(treFolders.SelectedNode.Tag is PurchaseOrderTemplateFolder folder))
            {
                return;
            }

            DragAndDropLists dragAndDropLists = new DragAndDropLists()
            {
                Folders = { folder },
                Mode = DragAndDropLists.Modes.Copy
            };

            Clipboard.SetData(typeof(DragAndDropLists).FullName, dragAndDropLists);
        }

        private void treFolders_CutToClipboard()
        {
            if (treFolders.SelectedNode == null || !(treFolders.SelectedNode.Tag is PurchaseOrderTemplateFolder folder))
            {
                return;
            }

            DragAndDropLists dragAndDropLists = new DragAndDropLists()
            {
                Folders = { folder }
            };
            Clipboard.SetData(typeof(DragAndDropLists).FullName, dragAndDropLists);
        }

        private async void treFolders_PasteFromClipboard()
        {
            if (Clipboard.ContainsData(typeof(DragAndDropLists).FullName))
            {
                await MoveDragAndDropLists(Clipboard.GetData(typeof(DragAndDropLists).FullName) as DragAndDropLists, foldersByFolder.Values.SelectMany(h => h).FirstOrDefault(f => f.PurchaseOrderTemplateFolderID == SelectedFolderID));
            }
        }

        private async void treFolders_DeleteFolder()
        {
            if (treFolders.SelectedNode == null || !(treFolders.SelectedNode.Tag is PurchaseOrderTemplateFolder folder) || !this.Confirm($"Are you sure you want to delete '{folder.Name}'?"))
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Delete/" + folder.PurchaseOrderTemplateFolderID);
            delete.AddLocationHeader(CompanyID, LocationID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                foldersByFolder[folder.PurchaseOrderTemplateFolderIDParent ?? -1L].Remove(folder);
                foldersByFolder.Remove(folder.PurchaseOrderTemplateFolderID);
                templatesByFolder.Remove(folder.PurchaseOrderTemplateFolderID);

                RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
                LoadListItems();
            }
        }

        private void tsmiTreeCut_Click(object sender, EventArgs e)
        {
            treFolders_CutToClipboard();
        }

        private void tsmiTreeCopy_Click(object sender, EventArgs e)
        {
            treFolders_CopyToClipboard();
        }

        private void tsmiTreePaste_Click(object sender, EventArgs e)
        {
            treFolders_PasteFromClipboard();
        }

        private void tsmiTreeDelete_Click(object sender, EventArgs e)
        {
            treFolders_DeleteFolder();
        }

        private void ctxTreeView_Opening(object sender, CancelEventArgs e)
        {
            tsmiTreeCopy.Enabled = treFolders.SelectedNode != null;
            tsmiTreeCut.Enabled = treFolders.SelectedNode != null;
            tsmiTreeDelete.Enabled = treFolders.SelectedNode != null;
            tsmiTreeRename.Enabled = treFolders.SelectedNode != null;
            tsmiTreePaste.Enabled = Clipboard.ContainsData(typeof(DragAndDropLists).FullName);
        }

        private void tsmiListRename_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count <= 0 || (!(lstItems.SelectedItems[0].Tag is PurchaseOrderTemplateFolder) && !(lstItems.SelectedItems[0].Tag is PurchaseOrderTemplate)))
            {
                return;
            }

            lstItems.SelectedItems[0].BeginEdit();
        }

        private void tsmiTreeRename_Click(object sender, EventArgs e)
        {
            if (treFolders.SelectedNode == null || !(treFolders.SelectedNode.Tag is PurchaseOrderTemplateFolder))
            {
                return;
            }

            treFolders.SelectedNode.BeginEdit();
        }

        private async void toolNewFolder_Click(object sender, EventArgs e)
        {
            long? newFolderID = await CreateNewFolder(SelectedFolderID);

            if (newFolderID != null)
            {
                ListViewItem listViewItem = lstItems.Items.OfType<ListViewItem>().Where(lvi => lvi.Tag is PurchaseOrderTemplateFolder folder && folder.PurchaseOrderTemplateFolderID == newFolderID).FirstOrDefault();
                if (listViewItem != null)
                {
                    listViewItem.BeginEdit();
                }
            }
        }

        private async Task<long?> CreateNewFolder(long? parentFolderID)
        {
            string newName = "New Folder";
            int i = 1;
            while (foldersByFolder[parentFolderID].Any(f => f.Name == newName))
            {
                newName = $"New Folder ({i})";
                i++;
            }

            PurchaseOrderTemplateFolder currentFolder = foldersByFolder.Values.SelectMany(h => h).FirstOrDefault(f => f.PurchaseOrderTemplateFolderID == parentFolderID);
            PurchaseOrderTemplateFolder newFolder = new PurchaseOrderTemplateFolder()
            {
                LocationID = LocationID,
                FolderPath = (currentFolder?.FolderPath ?? "/") + newName + "/",
                Name = newName,
                PurchaseOrderTemplateFolderIDParent = currentFolder?.PurchaseOrderTemplateFolderID
            };

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplateFolder/Post", newFolder);
            post.AddLocationHeader(CompanyID, LocationID);
            newFolder = await post.Execute<PurchaseOrderTemplateFolder>();

            if (post.RequestSuccessful)
            {
                foldersByFolder[parentFolderID].Add(newFolder);
                foldersByFolder.Add(newFolder.PurchaseOrderTemplateFolderID, new HashSet<PurchaseOrderTemplateFolder>());
                templatesByFolder.Add(newFolder.PurchaseOrderTemplateFolderID, new HashSet<PurchaseOrderTemplate>());

                RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
                LoadListItems();

                return newFolder.PurchaseOrderTemplateFolderID;
            }

            return null;
        }

        private void tsmiListNewFolder_Click(object sender, EventArgs e)
        {
            toolNewFolder.PerformClick();
        }

        private async void tsmiTreeNewFolder_Click(object sender, EventArgs e)
        {
            if (treFolders.SelectedNode == null)
            {
                return;
            }

            PurchaseOrderTemplateFolder parentFolder = treFolders.SelectedNode.Tag as PurchaseOrderTemplateFolder;
            long? newFolderID = await CreateNewFolder(parentFolder?.PurchaseOrderTemplateFolderID ?? -1L);
            if (newFolderID != null)
            {
                TreeNode node = treFolders.Nodes.AllNodes().First(n => n.Tag is PurchaseOrderTemplateFolder folder && folder.PurchaseOrderTemplateFolderID == newFolderID.Value);

                if (node != null)
                {
                    treFolders.SelectedNode = node;
                    node.BeginEdit();
                }
            }
        }

        private void cmdActionButton_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = lstItems.Items.OfType<ListViewItem>().FirstOrDefault(lvi => string.Equals(lvi.Text, txtTemplateName.Text, StringComparison.OrdinalIgnoreCase));
            switch(DialogMode)
            {
                case DialogModes.Open:
                    if (selectedItem == null)
                    {
                        this.ShowError("A template must be selected");
                        return;
                    }

                    if (selectedItem.Tag is PurchaseOrderTemplateFolder folder)
                    {
                        SelectedFolderID = folder.PurchaseOrderTemplateFolderID;
                        RefreshTree(foldersByFolder.Values.SelectMany(h => h).ToList());
                        LoadListItems();
                        return;
                    }

                    if (!(selectedItem.Tag is PurchaseOrderTemplate template))
                    {
                        this.ShowError("An item must be selected");
                        return;
                    }

                    SelectedTemplate = template;
                    DialogResult = DialogResult.OK;
                    Close();
                    break;
                case DialogModes.Save:
                    if (selectedItem != null && selectedItem.Tag is PurchaseOrderTemplate overwriteTemplateForWarning && !this.Confirm($"Are you sure you want to overwrite {overwriteTemplateForWarning.Name}?"))
                    { 
                        return;
                    }

                    PurchaseOrderTemplate templateToSave = selectedItem?.Tag as PurchaseOrderTemplate;
                    if (templateToSave == null)
                    {
                        templateToSave = new PurchaseOrderTemplate()
                        {
                            LocationID = LocationID,
                            Name = txtTemplateName.Text,
                            PurchaseOrderTemplateFolderID = SelectedFolderID
                        };
                    }

                    SelectedTemplate = templateToSave;
                    DialogResult = DialogResult.OK;
                    Close();
                    break;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtTemplateName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                cmdActionButton.PerformClick();
            }
        }
    }
}
