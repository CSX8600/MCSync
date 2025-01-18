using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using Pluralize.NET;

namespace GovernmentPortal
{
    public partial class frmGenericExplorer<TModel> : Form where TModel : class
    {
        private ExplorerContext<TModel> explorerContext;
        private IExplorerControl<TModel> shownControl;
        private List<DropDownItem<TModel>> dropDownItems;

        public frmGenericExplorer()
        {
            InitializeComponent();
        }

        internal frmGenericExplorer(ExplorerContext<TModel> explorerContext) : this()
        {
            this.explorerContext = explorerContext;
        }

        private int priorIndex = -1;
        bool suppressItemListChange = false;
        private void listItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressItemListChange)
            {
                suppressItemListChange = false;
                return;
            }

            if (shownControl != null && shownControl.IsDirty && WarnDirty() == DialogResult.Cancel)
            {
                suppressItemListChange = true;
                lstItems.SelectedIndex = priorIndex;
                return;
            }

            DropDownItem<TModel> dropDownItem = lstItems.SelectedItem as DropDownItem<TModel>;
            if (dropDownItem == null)
            {
                grpContent.Controls.Clear();
                shownControl = null;
                return;
            }

            ShowFromModel(dropDownItem.Object);
            priorIndex = lstItems.SelectedIndex;
        }

        private void ShowFromModel(TModel model)
        {
            grpContent.Controls.Clear();
            shownControl = explorerContext.GetControlForModel(model);
            shownControl.Explorer = this;
            Control control = (Control)shownControl;
            grpContent.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }

        private async void frmGenericExplorer_Load(object sender, EventArgs e)
        {
            if (explorerContext == null)
            {
                throw new ArgumentNullException("Explorer Content");
            }

            Icon = explorerContext.ExplorerIcon;

            Pluralizer pluralizer = new Pluralizer();
            string pluralizedName = pluralizer.Pluralize(explorerContext.ObjectDisplayName);

            lblTitle.Text = pluralizedName;
            Text = $"{explorerContext.ObjectDisplayName} Explorer";
            cmdNew.Text = $"New {explorerContext.ObjectDisplayName}";
            cmdDelete.Text = $"Delete {explorerContext.ObjectDisplayName}";
            cmdDelete.Visible = explorerContext.DeleteButtonVisible;
            grpContent.Text = explorerContext.ObjectDisplayName;
            IEnumerable<ToolStripMenuItem> extraTools = explorerContext.GetExtraToolItems();
            if (extraTools.Any())
            {
                foreach(ToolStripMenuItem item in extraTools) 
                {
                    toolStrip.Items.Add(item);
                }

                lstItems.Top = toolStrip.Bottom;
                toolStrip.Visible = true;
            }
            await LoadAllItems(true);
            DropDownItem<TModel> initiallySelected = explorerContext.GetInitiallySelectedItem(dropDownItems);
            if (initiallySelected != null)
            {
                lstItems.SelectedItem = initiallySelected;
            }
        }
        
        public async Task LoadAllItems(bool doFill = false, string selectedTextOverride = null)
        {
            if (shownControl != null && shownControl.IsDirty && WarnDirty() == DialogResult.Cancel)
            {
                return;
            }

            dropDownItems = new List<DropDownItem<TModel>>();
            try
            {
                dropDownItems = await explorerContext.GetInitialListItems();
            }
            catch(Exception ex)
            {
                this.ShowError($"An error occurred while loading data:\r\n{ex.Message}");
            }

            if (doFill)
            {
                FillItems(selectedTextOverride);
            }
        }

        private string _priorFillSearchText;
        public void FillItems(string selectedTextOverride = null)
        {
            DropDownItem<TModel> currentlySelectedItem = lstItems.SelectedItem as DropDownItem<TModel>;
            if (currentlySelectedItem != null && !currentlySelectedItem.Text.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) && shownControl != null && shownControl.IsDirty && WarnDirty() == DialogResult.Cancel)
            {
                suppressTextChangeEvent = true;
                txtSearch.Text = _priorFillSearchText;
                return;
            }

            _priorFillSearchText = txtSearch.Text;

            lstItems.Items.Clear();

            foreach(DropDownItem<TModel> item in dropDownItems.Where(ddi => string.IsNullOrEmpty(txtSearch.Text) ? true : ddi.Text.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)))
            {
                lstItems.Items.Add(item);
            }

            if (!string.IsNullOrEmpty(selectedTextOverride))
            {
                currentlySelectedItem = lstItems.Items.OfType<DropDownItem<TModel>>().FirstOrDefault(ddi => ddi.Text == selectedTextOverride);
            }

            if (currentlySelectedItem != null && lstItems.Items.Contains(currentlySelectedItem))
            {
                if (string.IsNullOrEmpty(selectedTextOverride)) // We want to trigger the update if we're picking a specific item
                {
                    suppressItemListChange = true;
                }
                lstItems.SelectedItem = currentlySelectedItem;
            }
        }

        private DialogResult WarnDirty()
        {
            DialogResult result = MessageBox.Show("You have unsaved changes.\r\n\r\nWould you like to save these changes before continuing?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                shownControl.OnSaveClicked();
                shownControl.IsDirty = false;
            }
            else if (result == DialogResult.No)
            {
                shownControl.IsDirty = false;
            }

            return result;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Control)shownControl).IsDisposed) return;

            shownControl?.OnSaveClicked();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (((Control)shownControl).IsDisposed) return;

            shownControl?.OnDeleteClicked();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            if (shownControl != null && shownControl.IsDirty && WarnDirty() == DialogResult.Cancel)
            {
                lstItems.SelectedIndex = priorIndex;
                return;
            }

            ShowFromModel(null);
        }

        bool suppressTextChangeEvent = false;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (suppressTextChangeEvent)
            {
                suppressTextChangeEvent = false;
                return;
            }
            FillItems();
        }

        public void ForceClose()
        {
            suppressCloseEvent = true;
            Close();
        }

        bool suppressCloseEvent = false;
        private void frmGenericExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!suppressCloseEvent && shownControl != null && shownControl.IsDirty && WarnDirty() == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void frmGenericExplorer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadAllItems(true);
                grpContent.Controls.Clear();
            }
        }

        private void lstItems_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawDropDownItems<TModel>(lstItems);
        }
    }
}
