using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReaLTaiizor.Controls;
using WeifenLuo.WinFormsUI.Docking;
using static CompanyStudio.frmStartPage;
using static ReaLTaiizor.Helper.CrownHelper;

namespace CompanyStudio
{
    public partial class frmStartPage : DockContent
    {
        public frmStudio Studio { get; set; }
        private List<ToDoItem> toDoItems = new List<ToDoItem>();

        public frmStartPage()
        {
            InitializeComponent();
        }

        private async void frmStartPage_Load(object sender, EventArgs e)
        {
            ThemeProvider.Theme = new LightTheme();
            ctxQuickAccessMenu.BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            ctxQuickAccessMenu.Items.OfType<ToolStripItem>().ForEach(tsi => tsi.BackColor = ThemeProvider.Theme.Colors.GreyBackground);
            chkAlwaysShowStart.Checked = UserPreferences.Get().GetPreferencesForSection("company").GetOrDefault("showStartPage", true) as bool? ?? true;

            await RefreshData();
            ReloadQuickAccess();

        }

        private void ReloadQuickAccess()
        {
            pnlQuickLinks.Controls.OfType<Control>().Where(ctrl => ctrl != lnkQuickAccess).ToList().ForEach(ctrl => pnlQuickLinks.Controls.Remove(ctrl));

            UserPreferences userPreferences = UserPreferences.Get();
            JArray quickAccess = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            if (quickAccess != null)
            {
                ToolStrip bannerStrip = Studio.Controls["mnuBanner"] as ToolStrip;

                foreach (JToken jItem in quickAccess)
                {
                    QuickAccessItem quickAccessItem;
                    try
                    {
                        quickAccessItem = jItem.ToObject<QuickAccessItem>();
                    }
                    catch { continue; }

                    string[] controlNameParts = quickAccessItem.ToolName.Split('/');
                    if (!bannerStrip.Items.ContainsKey(controlNameParts[0]))
                    {
                        continue;
                    }

                    ToolStripMenuItem toolStripMenuItem = bannerStrip.Items[controlNameParts[0]] as ToolStripMenuItem;
                    for (int i = 1; i < controlNameParts.Length; i++)
                    {
                        if (!toolStripMenuItem.DropDownItems.ContainsKey(controlNameParts[i]))
                        {
                            break;
                        }

                        toolStripMenuItem = toolStripMenuItem.DropDownItems[controlNameParts[i]] as ToolStripMenuItem;
                    }

                    if (!toolStripMenuItem.Name.Equals(controlNameParts.Last(), StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    quickAccessItem.ToolStripMenuItem = toolStripMenuItem;

                    DungeonLinkLabel quickAccessLink = new DungeonLinkLabel()
                    {
                        Text = quickAccessItem.FriendlyName,
                        Image = toolStripMenuItem.Image,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        Tag = quickAccessItem,
                        AutoSize = true,
                        Padding = new Padding(16, 0, 0, 0),
                        Margin = new Padding(0, 6, 0, 0)
                    };
                    quickAccessLink.LinkClicked += QuickAccessLink_LinkClicked;
                    pnlQuickLinks.Controls.Add(quickAccessLink);
                }
            }
        }

        private void QuickAccessLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuickAccessItem quickAccessItem = (sender as DungeonLinkLabel).Tag as QuickAccessItem;
            if (e.Button == MouseButtons.Left)
            {
                Studio.ActiveCompany = Studio.Companies.FirstOrDefault(c => c.CompanyID == quickAccessItem.CompanyID);
                Studio.ActiveLocation = Studio.ActiveCompany.Locations.FirstOrDefault(l => l.LocationID == quickAccessItem.LocationID);
                
                List<ToolStripMenuItem> menusToClick = GetMenusToClickWithVisibleParent(quickAccessItem.ToolStripMenuItem);

                for(int i = 0; i < menusToClick.Count; i++)
                {
                    ToolStripMenuItem menu = menusToClick[i];
                    if (!menu.Visible)
                    {
                        menusToClick[0].HideDropDown();
                        return;
                    }

                    if (i == menusToClick.Count - 1)
                    {
                        menu.PerformClick();
                    }
                    else
                    {
                        menu.ShowDropDown();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                ctxQuickAccessMenu.Tag = quickAccessItem;
                ctxQuickAccessMenu.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// A menu may exist multiple times with the same name. Find the one that has a visible parent and return the click
        /// list to click it.
        /// </summary>
        /// <param name="possiblyHiddenMenuItem">The menu that may or may not have a visible parent</param>
        /// <returns>A list of menu items to click starting with the visible parent</returns>
        private List<ToolStripMenuItem> GetMenusToClickWithVisibleParent(ToolStripMenuItem possiblyHiddenMenuItem)
        {
            ToolStrip banner = Studio.Controls["mnuBanner"] as ToolStrip;

            // Find current parent
            List<ToolStripMenuItem> possiblyHiddenMenusToClick = new List<ToolStripMenuItem>();
            ToolStripMenuItem possiblyHiddenParent = possiblyHiddenMenuItem;
            possiblyHiddenMenusToClick.Add(possiblyHiddenParent);
            while(possiblyHiddenParent.OwnerItem != null)
            {
                possiblyHiddenParent = possiblyHiddenParent.OwnerItem as ToolStripMenuItem;
                possiblyHiddenMenusToClick.Add(possiblyHiddenParent);
            }
            possiblyHiddenMenusToClick.Reverse();

            // The parent is visible, we can just click it
            if (possiblyHiddenParent.Visible)
            {
                return possiblyHiddenMenusToClick;
            }

            // The parent is not visible. Try to find the visible parent and get the new list
            // based on the old one
            List<ToolStripMenuItem> menusToClick = new List<ToolStripMenuItem>();
            ToolStripMenuItem parent = banner.Items.Find(possiblyHiddenParent.Name, false).FirstOrDefault(tsmi => tsmi.Visible) as ToolStripMenuItem;
            if (parent == null) // No parent with this name is visible. Return empty list
            {
                return menusToClick;
            }

            menusToClick.Add(parent);
            foreach(ToolStripMenuItem oldMenuToClick in possiblyHiddenMenusToClick.Skip(1))
            {
                parent = parent.DropDownItems.Find(oldMenuToClick.Name, false).First() as ToolStripMenuItem;
                if (parent == null) // Doesn't exist, return what we have
                {
                    return menusToClick;
                }
                menusToClick.Add(parent);
            }

            return menusToClick;
        }

        private async Task RefreshData()
        {
            loader.BringToFront();
            loader.Visible = true;

            while(tbpToDoList.TabPages.Count > 1)
            {
                tbpToDoList.TabPages.RemoveAt(1);
            }

            tabAll.Controls.Clear();

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Employee/GetToDoItems");
            toDoItems = await get.GetObject<List<ToDoItem>>() ?? new List<ToDoItem>();
            toDoItems.ForEach(tdi => tdi.CompanyName = Studio.Companies.FirstOrDefault(c => c.CompanyID == tdi.CompanyID)?.Name);
            toDoItems.Sort(new ToDoItem.ToDoItemComparer());
            foreach (ToDoItem item in toDoItems)
            {
                System.Windows.Forms.TabPage companyPage = tbpToDoList.TabPages.OfType<System.Windows.Forms.TabPage>().FirstOrDefault(tp => tp.Tag is Company company && company.CompanyID == item.CompanyID);
                if (companyPage == null)
                {
                    Company company = Studio.Companies.FirstOrDefault(c => c.CompanyID == item.CompanyID);
                    if (company == null)
                    {
                        continue;
                    }

                    companyPage = new System.Windows.Forms.TabPage();
                    companyPage.Tag = company;
                    companyPage.Text = company.Name;

                    int insertIndex = 1;
                    System.Windows.Forms.TabPage desiredIndex = tbpToDoList.TabPages.OfType<System.Windows.Forms.TabPage>().OrderBy(tp => tp.Text).FirstOrDefault(tb => tb.Text.CompareTo(companyPage.Text) >= 1);
                    if (desiredIndex != null)
                    {
                        insertIndex = tbpToDoList.TabPages.IndexOf(desiredIndex);
                    }

                    tbpToDoList.TabPages.Insert(insertIndex, companyPage);
                }

                lblAllCaughtUp.Visible = false;
                // Insert into All Businesses tab
                ReaLTaiizor.Controls.HopeNotify.AlertType type = item.Severity == ToDoItem.Severities.Urgent ? HopeNotify.AlertType.Error :
                                                                 item.Severity == ToDoItem.Severities.Important ? HopeNotify.AlertType.Warning :
                                                                 HopeNotify.AlertType.Info;

                ReaLTaiizor.Controls.HopeNotify mainTabControl = new ReaLTaiizor.Controls.HopeNotify()
                {
                    Text = $"{(companyPage.Tag as Company).Name}: {item.Message}",
                    Tag = item,
                    Close = false,
                    Width = tabAll.Width,
                    Type = type,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                };
                mainTabControl.Click += NotificationClick;

                int top = 0;
                if (tabAll.Controls.OfType<ReaLTaiizor.Controls.HopeNotify>().Any())
                {
                    top = tabAll.Controls.OfType<ReaLTaiizor.Controls.HopeNotify>().Max(n => n.Bottom) + 6;
                }
                mainTabControl.Top = top;
                tabAll.Controls.Add(mainTabControl);

                // Insert onto company specific tab
                ReaLTaiizor.Controls.HopeNotify companyTabControl = new ReaLTaiizor.Controls.HopeNotify()
                {
                    Text = item.Message,
                    Tag = item,
                    Close = false,
                    Width = companyPage.Width,
                    Type = type,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                };
                companyTabControl.Click += NotificationClick;
                top = 0;
                if (companyPage.Controls.OfType<ReaLTaiizor.Controls.HopeNotify>().Any())
                {
                    top = companyPage.Controls.OfType<ReaLTaiizor.Controls.HopeNotify>().Max(n => n.Bottom) + 6;
                }
                companyTabControl.Top = top;
                companyPage.Controls.Add(companyTabControl);
            }
            lblAllCaughtUp.Visible = !toDoItems.Any();
            lblAllCaughtUp.BringToFront();
            loader.Visible = false;
        }

        private async void NotificationClick(object sender, EventArgs e)
        {
            if (!(sender is ReaLTaiizor.Controls.HopeNotify notify) || !(notify.Tag is ToDoItem toDoItem))
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, null);
            get.AddLocationHeader(toDoItem.CompanyID, toDoItem.LocationID);

            Company company = Studio.Companies.FirstOrDefault(c => c.CompanyID == toDoItem.CompanyID);
            Location location = company.Locations.FirstOrDefault(l => l.LocationID == toDoItem.LocationID);

            switch (toDoItem.Type)
            {
                case ToDoItem.Types.PayableInvoiceWaiting:
                case ToDoItem.Types.PayablePastDueInvoice:
                    get.Resource = "Invoice/Get/" + toDoItem.SourceID;
                    Invoice payableInvoice = await get.GetObject<Invoice>();

                    if (payableInvoice == null)
                    {
                        return;
                    }

                    Invoicing.frmPayableInvoice payableInvoiceForm = new Invoicing.frmPayableInvoice()
                    {
                        InvoiceID = payableInvoice.InvoiceID
                    };
                    Studio.DecorateStudioContent(payableInvoiceForm);
                    payableInvoiceForm.Company = company;
                    payableInvoiceForm.LocationModel = location;
                    payableInvoiceForm.FormClosed += async (_, __) => await RefreshData();
                    payableInvoiceForm.Show(Studio.dockPanel, DockState.Document);
                    break;
                case ToDoItem.Types.ReceivableInvoiceWaiting:
                case ToDoItem.Types.ReceivablePastDueInvoice:
                    get.Resource = "Invoice/Get/" + toDoItem.SourceID;
                    Invoice receivableInvoice = await get.GetObject<Invoice>();

                    if (receivableInvoice == null)
                    {
                        return;
                    }

                    Invoicing.frmReceivableInvoice receivableInvoiceForm = new Invoicing.frmReceivableInvoice()
                    {
                        InvoiceID = receivableInvoice.InvoiceID
                    };
                    Studio.DecorateStudioContent(receivableInvoiceForm);
                    receivableInvoiceForm.Company = company;
                    receivableInvoiceForm.LocationModel = location;
                    receivableInvoiceForm.FormClosed += async (_, __) => await RefreshData();
                    receivableInvoiceForm.Show(Studio.dockPanel, DockState.Document);
                    break;
                case ToDoItem.Types.QuotationRequestWaiting:
                    Purchasing.Quotes.frmQuoteRequest quoteRequest = new Purchasing.Quotes.frmQuoteRequest()
                    {
                        Company = company
                    };
                    Studio.DecorateStudioContent(quoteRequest);
                    quoteRequest.Company = company;
                    quoteRequest.LocationModel = location;
                    quoteRequest.FormClosed += async (_, __) => await RefreshData();
                    quoteRequest.Show(Studio.dockPanel, DockState.Document);
                    break;
                case ToDoItem.Types.PurchaseOrderWaitingApproval:
                    get.Resource = "PurchaseOrder/Get/" + toDoItem.SourceID;
                    PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();

                    if (purchaseOrder == null) { return; }

                    Purchasing.ApprovalViewer.frmApprovalViewerApprover approvalViewer = new Purchasing.ApprovalViewer.frmApprovalViewerApprover()
                    {
                        PurchaseOrderApprovalID = purchaseOrder.PurchaseOrderApprovals.FirstOrDefault(poa => poa.CompanyIDApprover == company.CompanyID)?.PurchaseOrderApprovalID,
                        PurchaseOrderID = toDoItem.SourceID
                    };
                    Studio.DecorateStudioContent(approvalViewer);
                    approvalViewer.Company = company;
                    approvalViewer.LocationModel = location;
                    approvalViewer.FormClosed += async (_, __) => await RefreshData();
                    approvalViewer.Show(Studio.dockPanel, DockState.Document);
                    break;
                case ToDoItem.Types.RailcarAwaitingAction:
                    Purchasing.ShippingReceiving.frmShippingReceiving shippingReceiving = new Purchasing.ShippingReceiving.frmShippingReceiving();
                    Studio.DecorateStudioContent(shippingReceiving);
                    shippingReceiving.Company = company;
                    shippingReceiving.LocationModel = location;
                    shippingReceiving.FormClosed += async (_, __) => await RefreshData();
                    shippingReceiving.Show(Studio.dockPanel, DockState.Document);
                    break;
                case ToDoItem.Types.RegisterOffline:
                    Store.frmRegister register = new Store.frmRegister()
                    {
                        Company = company,
                        RegisterID = toDoItem.SourceID
                    };
                    Studio.DecorateStudioContent(register);
                    register.Company = company;
                    register.LocationModel = location;
                    register.FormClosed += async (_, __) => await RefreshData();
                    register.Show(Studio.dockPanel, DockState.Document);
                    break;
                case ToDoItem.Types.OpenPurchaseOrders:
                    Purchasing.frmPurchaseOrderExplorer purchaseOrderExplorer = new Purchasing.frmPurchaseOrderExplorer();
                    Studio.DecorateStudioContent(purchaseOrderExplorer);
                    purchaseOrderExplorer.Company = company;
                    purchaseOrderExplorer.LocationModel = location;
                    purchaseOrderExplorer.FormClosed += async (_, __) => await RefreshData();
                    purchaseOrderExplorer.Show(Studio.dockPanel, DockState.DockRight);
                    break;
                case ToDoItem.Types.AutomaticPaymentsAlmostComplete:
                    Invoicing.frmAutomaticPaymentConfiguration automaticPaymentConfiguration = new Invoicing.frmAutomaticPaymentConfiguration();
                    Studio.DecorateStudioContent(automaticPaymentConfiguration);
                    automaticPaymentConfiguration.Company = company;
                    automaticPaymentConfiguration.LocationModel = location;
                    automaticPaymentConfiguration.InitialConfigurationID = toDoItem.SourceID;
                    automaticPaymentConfiguration.FormClosed += async (_, __) => await RefreshData();
                    automaticPaymentConfiguration.Show(Studio.dockPanel, DockState.Document);
                    break;
            }
        }

        private class ToDoItem
        {
            public enum Types
            {
                PayableInvoiceWaiting = 0,
                ReceivableInvoiceWaiting,
                PayablePastDueInvoice,
                ReceivablePastDueInvoice,
                PurchaseOrderWaitingApproval,
                QuotationRequestWaiting,
                RailcarAwaitingAction,
                RegisterOffline,
                OpenPurchaseOrders,
                AutomaticPaymentsAlmostComplete
            }

            public enum Severities
            {
                Informational = 0,
                Important,
                Urgent
            }

            public Severities Severity { get; set; }
            public Types Type { get; set; }
            public string Message { get; set; }
            public long? CompanyID { get; set; }
            public long? LocationID { get; set; }
            public long? SourceID { get; set; }

            public string CompanyName { get; set; }

            public class ToDoItemComparer : Comparer<ToDoItem>
            {
                public override int Compare(ToDoItem x, ToDoItem y)
                {
                    int result = ((int)x.Severity).CompareTo((int)y.Severity) * -1;
                    if (result == 0)
                    {
                        result = x.CompanyName.CompareTo(y.CompanyName);
                    }

                    if (result == 0)
                    {
                        result = x.Message.CompareTo(y.Message);
                    }

                    return result;
                }
            }
        }

        private void chkAlwaysShowStart_CheckedChanged(object sender)
        {
            UserPreferences userPreferences = UserPreferences.Get();
            userPreferences.GetPreferencesForSection("company")["showStartPage"] = chkAlwaysShowStart.Checked;
            userPreferences.Save();
        }

        private void lnkQuickAccess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmStartPageEditQuickAccess editQuickAccess = new frmStartPageEditQuickAccess()
            {
                Studio = Studio,
                QuickAccessItem = new QuickAccessItem()
                {
                    CompanyID = Studio.ActiveCompany?.CompanyID,
                    LocationID = Studio.ActiveLocation?.LocationID
                }
            };
            DialogResult result = editQuickAccess.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            UserPreferences userPreferences = UserPreferences.Get();
            JArray array = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            array.Add(JObject.FromObject(editQuickAccess.QuickAccessItem));
            userPreferences.Save();

            ReloadQuickAccess();
        }

        public class QuickAccessItem
        {
            public string ToolName { get; set; } = "";
            public string FriendlyName { get; set; } = "";
            public long? CompanyID { get; set; }
            public long? LocationID { get; set; }

            [JsonIgnore]
            public ToolStripMenuItem ToolStripMenuItem { get; set; }
        }

        private void tsmiEditQuickAccess_Click(object sender, EventArgs e)
        {
            QuickAccessItem quickAccessItem = ctxQuickAccessMenu.Tag as QuickAccessItem;

            UserPreferences userPreferences = UserPreferences.Get();
            JArray array = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            JToken existing = null;
            foreach (JToken token in array)
            {
                try
                {
                    QuickAccessItem item = token.ToObject<QuickAccessItem>();
                    if (quickAccessItem.FriendlyName.Equals(item.FriendlyName, StringComparison.OrdinalIgnoreCase) &&
                        item.CompanyID == quickAccessItem.CompanyID &&
                        item.LocationID == quickAccessItem.LocationID &&
                        quickAccessItem.ToolName.Equals(item.ToolName, StringComparison.OrdinalIgnoreCase))
                    {
                        existing = token;
                        break;
                    }
                }
                catch { continue; }
            }

            if (existing == null)
            {
                CrownMessageBox.ShowError("Unable to locate quick access item in user preferences.", "Error");
                return;
            }

            frmStartPageEditQuickAccess editQuickAccess = new frmStartPageEditQuickAccess()
            {
                Studio = Studio,
                QuickAccessItem = quickAccessItem
            };
            DialogResult result = editQuickAccess.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            existing.Replace(JToken.FromObject(quickAccessItem));
            userPreferences.GetPreferencesForSection("company")["startQuickAccess"] = array;
            userPreferences.Save();

            ReloadQuickAccess();
        }

        private void tsmiDeleteQuickAccess_Click(object sender, EventArgs e)
        {
            QuickAccessItem quickAccessItem = ctxQuickAccessMenu.Tag as QuickAccessItem;

            UserPreferences userPreferences = UserPreferences.Get();
            JArray array = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            JToken existing = null;
            foreach (JToken token in array)
            {
                try
                {
                    QuickAccessItem item = token.ToObject<QuickAccessItem>();
                    if (quickAccessItem.FriendlyName.Equals(item.FriendlyName, StringComparison.OrdinalIgnoreCase) &&
                        item.CompanyID == quickAccessItem.CompanyID &&
                        item.LocationID == quickAccessItem.LocationID &&
                        quickAccessItem.ToolName.Equals(item.ToolName, StringComparison.OrdinalIgnoreCase))
                    {
                        existing = token;
                        break;
                    }
                }
                catch { continue; }
            }

            if (existing == null)
            {
                CrownMessageBox.ShowError("Unable to locate quick access item in user preferences.", "Error");
                return;
            }

            array.Remove(existing);
            userPreferences.GetPreferencesForSection("company")["startQuickAccess"] = array;
            userPreferences.Save();

            ReloadQuickAccess();
        }

        private void tsmiQuickAccessMoveUp_Click(object sender, EventArgs e)
        {
            QuickAccessItem quickAccessItem = ctxQuickAccessMenu.Tag as QuickAccessItem;

            UserPreferences userPreferences = UserPreferences.Get();
            JArray array = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            JToken existing = null;
            foreach (JToken token in array)
            {
                try
                {
                    QuickAccessItem item = token.ToObject<QuickAccessItem>();
                    if (quickAccessItem.FriendlyName.Equals(item.FriendlyName, StringComparison.OrdinalIgnoreCase) &&
                        item.CompanyID == quickAccessItem.CompanyID &&
                        item.LocationID == quickAccessItem.LocationID &&
                        quickAccessItem.ToolName.Equals(item.ToolName, StringComparison.OrdinalIgnoreCase))
                    {
                        existing = token;
                        break;
                    }
                }
                catch { continue; }
            }

            if (existing == null)
            {
                CrownMessageBox.ShowError("Unable to locate quick access item in user preferences.", "Error");
                return;
            }
            int currentIndex = array.IndexOf(existing);

            if (currentIndex == 0)
            {
                return;
            }

            array.Remove(existing);
            array.Insert(currentIndex - 1, existing);
            userPreferences.GetPreferencesForSection("company")["startQuickAccess"] = array;
            userPreferences.Save();

            ReloadQuickAccess();
        }

        private void tsmiQuickAccessMoveDown_Click(object sender, EventArgs e)
        {
            QuickAccessItem quickAccessItem = ctxQuickAccessMenu.Tag as QuickAccessItem;

            UserPreferences userPreferences = UserPreferences.Get();
            JArray array = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            JToken existing = null;
            foreach (JToken token in array)
            {
                try
                {
                    QuickAccessItem item = token.ToObject<QuickAccessItem>();
                    if (quickAccessItem.FriendlyName.Equals(item.FriendlyName, StringComparison.OrdinalIgnoreCase) &&
                        item.CompanyID == quickAccessItem.CompanyID &&
                        item.LocationID == quickAccessItem.LocationID &&
                        quickAccessItem.ToolName.Equals(item.ToolName, StringComparison.OrdinalIgnoreCase))
                    {
                        existing = token;
                        break;
                    }
                }
                catch { continue; }
            }

            if (existing == null)
            {
                CrownMessageBox.ShowError("Unable to locate quick access item in user preferences.", "Error");
                return;
            }
            int currentIndex = array.IndexOf(existing);

            if (currentIndex == array.Count - 1)
            {
                return;
            }

            array.Remove(existing);
            array.Insert(currentIndex + 1, existing);
            userPreferences.GetPreferencesForSection("company")["startQuickAccess"] = array;
            userPreferences.Save();

            ReloadQuickAccess();
        }

        private void ctxQuickAccessMenu_Opening(object sender, CancelEventArgs e)
        {
            QuickAccessItem quickAccessItem = ctxQuickAccessMenu.Tag as QuickAccessItem;

            UserPreferences userPreferences = UserPreferences.Get();
            JArray array = userPreferences.GetPreferencesForSection("company").GetOrSetDefault("startQuickAccess", new JArray()) as JArray;
            JToken existing = null;
            foreach (JToken token in array)
            {
                try
                {
                    QuickAccessItem item = token.ToObject<QuickAccessItem>();
                    if (quickAccessItem.FriendlyName.Equals(item.FriendlyName, StringComparison.OrdinalIgnoreCase) &&
                        item.CompanyID == quickAccessItem.CompanyID &&
                        item.LocationID == quickAccessItem.LocationID &&
                        quickAccessItem.ToolName.Equals(item.ToolName, StringComparison.OrdinalIgnoreCase))
                    {
                        existing = token;
                        break;
                    }
                }
                catch { continue; }
            }

            if (existing == null)
            {
                CrownMessageBox.ShowError("Unable to locate quick access item in user preferences.", "Error");
                return;
            }
            int currentIndex = array.IndexOf(existing);

            tsmiQuickAccessMoveUp.Enabled = currentIndex > 0;
            tsmiQuickAccessMoveDown.Enabled = currentIndex < array.Count - 1;
        }
    }
}
