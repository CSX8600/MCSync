using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    [ToolboxItem(false)]
    public partial class Draft : UserControl, IHasShellReference
    {
        public frmPurchaseOrderShell Shell { get; set; }

        public long? PurchaseOrderID { get; set; }

        private List<long?> _deletedPurchaseOrderLineIDs = new List<long?>();

        public Draft()
        {
            InitializeComponent();
        }

        private async void Draft_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "Company/GetAll");
            get.AddGovHeader(Shell.GovernmentID);
            List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();

            foreach (Company company in companies.OrderBy(c => c.Name))
            {
                foreach(Location location in company.Locations.OrderBy(l => l.Name))
                {
                    DropDownItem<Location> ddi = new DropDownItem<Location>(location, $"{company.Name} ({location.Name})");
                    cboCompany.Items.Add(ddi);
                }
            }

            get.Resource = "Government/GetAll";
            List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();

            foreach (Government government in governments.OrderBy(g => g.Name))
            {
                DropDownItem<Government> ddi = new DropDownItem<Government>(government, government.Name);
                cboGovernment.Items.Add(ddi);
            }

            await ReloadData();
        }

        public async Task ReloadData()
        {
            if (PurchaseOrderID == null) return;

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "PurchaseOrder/Get/" + PurchaseOrderID);
            get.AddGovHeader(Shell.GovernmentID);
            PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
            if (purchaseOrder == null) return;

            rdoCompany.Checked = purchaseOrder.LocationIDDestination != null;
            rdoGovernment.Checked = purchaseOrder.GovernmentIDDestination != null;

            cboCompany.SelectedItem = cboCompany.Items.OfType<DropDownItem<Location>>().FirstOrDefault(l => l.Object.LocationID == purchaseOrder.LocationIDDestination);
            cboGovernment.SelectedItem = cboGovernment.Items.OfType<DropDownItem<Government>>().FirstOrDefault(g => g.Object.GovernmentID == purchaseOrder.GovernmentIDDestination);
            txtDescription.Text = purchaseOrder.Description;

            foreach (PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines)
            {
                AddFulfillmentPlan(line);
            }
        }

        private void tsbNewLine_Click(object sender, EventArgs e)
        {
            AddFulfillmentPlan();
        }

        private void AddFulfillmentPlan(PurchaseOrderLine line = null)
        {
            lblLinePlaceholder.Visible = false;

            DraftLine draftLine = new DraftLine();
            draftLine.PurchaseOrderLine = line ?? new PurchaseOrderLine() { PurchaseOrderID = PurchaseOrderID };
            draftLine.GovernmentIDOrigin = Shell.GovernmentID;
            draftLine.LocationIDDestination = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Location>)?.Object.LocationID : null;
            draftLine.GovernmentIDDestination = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null;
            draftLine.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            draftLine.Width = pnlLines.Width;
            draftLine.BorderStyle = BorderStyle.FixedSingle;
            draftLine.FulfillmentPlanClicked += DraftLine_FulfillmentPlanClicked;
            draftLine.LineCostChanged += DraftLine_LineCostChanged;
            draftLine.DeleteClicked += DraftLine_DeleteClicked;

            int top = 0;
            if (pnlLines.Controls.OfType<DraftLine>().Any())
            {
                top = pnlLines.Controls.OfType<DraftLine>().Max(dl => dl.Bottom);
            }

            draftLine.Top = top;
            pnlLines.Controls.Add(draftLine);
        }

        private void DraftLine_DeleteClicked(object sender, EventArgs e)
        {
            if (!(sender is DraftLine draftLine)) return;

            pnlLines.Controls.Remove(draftLine);
            int top = 0;
            foreach(DraftLine line in pnlLines.Controls.OfType<DraftLine>().OrderBy(dl => dl.Top))
            {
                line.Top = top;
                top = line.Bottom;
            }
            draftLine.FulfillmentPlanClicked -= DraftLine_FulfillmentPlanClicked;
            draftLine.LineCostChanged -= DraftLine_LineCostChanged;
            draftLine.DeleteClicked -= DraftLine_DeleteClicked;

            if (draftLine.PurchaseOrderLine.PurchaseOrderLineID != null)
            {
                _deletedPurchaseOrderLineIDs.Add(draftLine.PurchaseOrderLine.PurchaseOrderLineID);
            }
        }

        private void DraftLine_LineCostChanged(object sender, EventArgs e)
        {
            decimal total = pnlLines.Controls.OfType<DraftLine>().Any() ? pnlLines.Controls.OfType<DraftLine>().Sum(dl => dl.GetLineCost()) : 0M;

            txtTotalCost.Text = total.ToString("N2");
        }

        private async void DraftLine_FulfillmentPlanClicked(object sender, EventArgs e)
        {
            if (!(sender is DraftLine draftLine) || PurchaseOrderID == null) return;

            Color originalColor = draftLine.BackColor;
            draftLine.BackColor = SystemColors.Highlight;

            frmDraftFulfillmentPlans fulfillmentPlanList = new frmDraftFulfillmentPlans();
            fulfillmentPlanList.Shell = (frmPortal)Shell.MdiParent;
            fulfillmentPlanList.PurchaseOrderID = PurchaseOrderID;
            fulfillmentPlanList.PurchaseOrderLineIDFor = draftLine.PurchaseOrderLine.PurchaseOrderLineID;
            fulfillmentPlanList.GovernmentIDOrigin = Shell.GovernmentID;
            fulfillmentPlanList.GovernmentIDDestination = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null;
            fulfillmentPlanList.CompanyIDDestination = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Location>)?.Object.CompanyID : null;
            fulfillmentPlanList.FulfillmentPlanIDs = draftLine.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines?.Select(fp => fp.FulfillmentPlanID).ToList() ?? new List<long?>();
            fulfillmentPlanList.StartPosition = FormStartPosition.Manual;
            fulfillmentPlanList.Location = draftLine.PointToScreen(new Point(draftLine.Location.X + draftLine.Width, draftLine.Location.Y));
            if (fulfillmentPlanList.Right > Screen.FromPoint(draftLine.PointToScreen(draftLine.Location)).Bounds.Right)
            {
                fulfillmentPlanList.Location = new Point(Screen.FromPoint(draftLine.PointToScreen(draftLine.Location)).Bounds.Right - fulfillmentPlanList.Width, fulfillmentPlanList.Location.Y);
            }
            fulfillmentPlanList.ShowDialog();

            GetData refreshLineGet = new GetData(DataAccess.APIs.GovernmentPortal, "PurchaseOrder/Get/" + PurchaseOrderID);
            refreshLineGet.AddGovHeader(Shell.GovernmentID);
            PurchaseOrder refreshedPurchaseOrder = await refreshLineGet.GetObject<PurchaseOrder>();
            draftLine.PurchaseOrderLine = refreshedPurchaseOrder.PurchaseOrderLines.First(pol => pol.PurchaseOrderLineID == draftLine.PurchaseOrderLine.PurchaseOrderLineID);

            foreach (long? newlySelectedPlanID in fulfillmentPlanList.FulfillmentPlanIDs.Except(draftLine.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines?.Select(fppol => fppol.FulfillmentPlanID).ToList() ?? new List<long?>()))
            {
                FulfillmentPlanPurchaseOrderLine newJoin = new FulfillmentPlanPurchaseOrderLine()
                {
                    PurchaseOrderLineID = draftLine.PurchaseOrderLine.PurchaseOrderLineID,
                    FulfillmentPlanID = newlySelectedPlanID
                };

                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlanPurchaseOrderLine/Post", newJoin);
                post.AddGovHeader(Shell.GovernmentID);
                await post.Execute<FulfillmentPlanPurchaseOrderLine>();
            }

            foreach(long? deletedPlanID in (draftLine.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines?.Select(fppol => fppol.FulfillmentPlanID).ToList() ?? new List<long?>()).Except(fulfillmentPlanList.FulfillmentPlanIDs))
            {
                FulfillmentPlanPurchaseOrderLine deletedJoin = draftLine.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines.FirstOrDefault(fppol => fppol.FulfillmentPlanID == deletedPlanID);
                if (deletedJoin == null)
                {
                    continue;
                }

                DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlanPurchaseOrderLine/Delete/" + deletedJoin.FulfillmentPlanPurchaseOrderLineID);
                delete.AddGovHeader(Shell.GovernmentID);
                await delete.Execute();
            }

            refreshedPurchaseOrder = await refreshLineGet.GetObject<PurchaseOrder>();
            // Multiple lines may have been affected by a fulfillment plan clone, so we need to refresh all of them

            foreach (DraftLine draftLineToUpdate in pnlLines.Controls.OfType<DraftLine>())
            {
                draftLineToUpdate.PurchaseOrderLine = refreshedPurchaseOrder.PurchaseOrderLines.First(pol => pol.PurchaseOrderLineID == draftLineToUpdate.PurchaseOrderLine.PurchaseOrderLineID);
                draftLineToUpdate.RefreshDisplay();
            }

            draftLine.BackColor = originalColor;
        }

        private void EntityRadioCheckedChanged(object sender, EventArgs e)
        {
            cboCompany.Enabled = rdoCompany.Checked;
            cboGovernment.Enabled = rdoGovernment.Checked;

            if (((RadioButton)sender).Checked)
            {
                foreach(DraftLine draftLine in pnlLines.Controls.OfType<DraftLine>())
                {
                    draftLine.LocationIDDestination = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Location>)?.Object.LocationID : null;
                    draftLine.GovernmentIDDestination = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null;
                }
            }
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            await PerformSave();
        }

        private async Task<bool> PerformSave()
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ( "Description", txtDescription )
            }))
            {
                return false;
            }

            if ((rdoCompany.Checked && cboCompany.SelectedItem == null) || (rdoGovernment.Checked && cboGovernment.SelectedItem == null))
            {
                this.ShowError("Company or Government is required.");
                return false;
            }

            if (PurchaseOrderID == null)
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder()
                {
                    GovernmentIDOrigin = Shell.GovernmentID,
                    GovernmentIDDestination = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null,
                    LocationIDDestination = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Location>)?.Object.LocationID : null,
                    Description = txtDescription.Text
                };
                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "PurchaseOrder/Post", purchaseOrder);
                post.AddGovHeader(Shell.GovernmentID);
                purchaseOrder = await post.Execute<PurchaseOrder>();
                if (!post.RequestSuccessful)
                {
                    return false;
                }

                PurchaseOrderID = purchaseOrder.PurchaseOrderID;
            }
            else
            {
                PatchData patch = new PatchData(DataAccess.APIs.GovernmentPortal, "PurchaseOrder/Patch", PatchData.PatchMethods.Replace, PurchaseOrderID, new Dictionary<string, object>()
                {
                    { nameof(PurchaseOrder.GovernmentIDDestination), rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null },
                    { nameof(PurchaseOrder.LocationIDDestination), rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Location>)?.Object.LocationID : null },
                    { nameof(PurchaseOrder.Description), txtDescription.Text }
                });
                patch.AddGovHeader(Shell.GovernmentID);
                await patch.Execute();
                if (!patch.RequestSuccessful)
                {
                    return false;
                }
            }

            bool allLineSavesSuccessful = true;
            foreach (long? lineIDToDelete in _deletedPurchaseOrderLineIDs)
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, "PurchaseOrderLine/Delete/" + lineIDToDelete);
                delete.AddGovHeader(Shell.GovernmentID);
                await delete.Execute();
                allLineSavesSuccessful &= delete.RequestSuccessful;
            }

            foreach (DraftLine draftLine in pnlLines.Controls.OfType<DraftLine>())
            {
                draftLine.PurchaseOrderLine.PurchaseOrderID = PurchaseOrderID;
                allLineSavesSuccessful &= await draftLine.SaveLine();
            }

            await Shell.RefreshTree(PurchaseOrderID);

            return allLineSavesSuccessful;
        }

        private void EntityComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            long? locationID = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Location>)?.Object.LocationID : null;
            long? governmentID = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null;
            foreach (DraftLine draftLine in pnlLines.Controls.OfType<DraftLine>())
            {
                draftLine.LocationIDDestination = locationID;
                draftLine.GovernmentIDDestination = governmentID;
            }
        }

        private async void cmdSubmit_Click(object sender, EventArgs e)
        {
            if (!await PerformSave())
            {
                return;
            }

            PostData submit = new PostData(DataAccess.APIs.GovernmentPortal, "PurchaseOrder/Submit", new { id = PurchaseOrderID });
            submit.AddGovHeader(Shell.GovernmentID);
            PurchaseOrder submittedPurchaseOrder = await submit.Execute<PurchaseOrder>();

            if (submit.RequestSuccessful)
            {
                await Shell.RefreshTree(PurchaseOrderID);
                UserControl purchaseOrderControl = PurchaseOrder.CreateControlForPurchaseOrder(submittedPurchaseOrder);
                Shell.DisplayControl(purchaseOrderControl);
            }
        }
    }
}
