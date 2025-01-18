using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
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

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    public partial class frmDraftFulfillmentPlans : Form
    {
        public long GovernmentIDOrigin { get; set; }
        public long? GovernmentIDDestination { get; set; }
        public long? CompanyIDDestination { get; set; }
        public long? PurchaseOrderID { get; set; }
        public long? PurchaseOrderLineIDFor { get; set; }
        public frmPortal Shell { get; set; }
        public IEnumerable<long?> FulfillmentPlanIDs { get; set; } = Enumerable.Empty<long?>();
        public frmDraftFulfillmentPlans()
        {
            InitializeComponent();
        }

        private async void frmDraftFulfillmentPlan_Load(object sender, EventArgs e)
        {
            await ReloadData();
        }

        private async Task ReloadData()
        {
            dgvFulfillmentPlans.Rows.Clear();

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlan/GetByPurchaseOrderID/" + PurchaseOrderID);
            get.AddGovHeader(GovernmentIDOrigin);
            List<FulfillmentPlan> fulfillmentPlans = await get.GetObject<List<FulfillmentPlan>>() ?? new List<FulfillmentPlan>();

            foreach (FulfillmentPlan plan in fulfillmentPlans)
            {
                DataGridViewRow row = dgvFulfillmentPlans.Rows[dgvFulfillmentPlans.Rows.Add()];
                string railcarValue = "[None]";
                if (plan.RailcarID != null)
                {
                    railcarValue = plan.Railcar?.FormattedReportingMark;
                }
                else if (plan.LeaseRequestID != null)
                {
                    railcarValue = $"Lease Request {plan.LeaseRequestID} ({plan.LeaseRequest?.LeaseBids?.Count ?? 0} Bids)";
                }

                row.Cells[colRailcar.Name].Value = railcarValue;
                row.Cells[colPOLines.Name].Value = (plan.FulfillmentPlanPurchaseOrderLines?.Count ?? 0).ToString() + " Line(s)";
                row.Cells[colRoute.Name].Value = plan.RouteDisplayString;
                row.Tag = plan;
            }

            foreach (DataGridViewRow row in dgvFulfillmentPlans.Rows)
            {
                if (!(row.Tag is FulfillmentPlan fulfillmentPlan))
                {
                    row.Selected = false;
                    continue;
                }

                row.Selected = FulfillmentPlanIDs.Contains(fulfillmentPlan.FulfillmentPlanID);
            }
        }

        private async void tsbNewFulfillmentPlan_Click(object sender, EventArgs e)
        {
            DisplayFulfillmentPlanEditor();

            await ReloadData();
        }

        private void DisplayFulfillmentPlanEditor(long? fulfillmentPlanID = null)
        {
            frmDraftFulfillmentPlan fulfillmentPlan = new frmDraftFulfillmentPlan()
            {
                GovernmentIDOrigin = GovernmentIDOrigin,
                GovernmentIDDestination = GovernmentIDDestination,
                CompanyIDDestination = CompanyIDDestination,
                PurchaseOrderLineIDFor = PurchaseOrderLineIDFor,
                FulfillmentPlanID = fulfillmentPlanID,
                Shell = Shell
            };

            fulfillmentPlan.ShowDialog();
        }

        private async void dgvFulfillmentPlans_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvFulfillmentPlans.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dgvFulfillmentPlans.Rows[e.RowIndex];
            if (!(row.Tag is FulfillmentPlan fulfillmentPlan))
            {
                return;
            }

            DisplayFulfillmentPlanEditor(fulfillmentPlan.FulfillmentPlanID);

            await ReloadData();
        }

        private void tsbApplySelected_Click(object sender, EventArgs e)
        {
            if (!CheckUnselectedPlans() && !this.Confirm("Unselecting a Fulfillment Plan where this Purchase Order Line is the only selected line will delete the Fulfillment Plan. Continue?"))
            {
                return;
            }

            List<long?> fulfillmentPlanIDs = new List<long?>();
            foreach(DataGridViewRow selectedRow in dgvFulfillmentPlans.SelectedRows)
            {
                FulfillmentPlan plan = selectedRow.Tag as FulfillmentPlan;
                if (plan == null)
                {
                    continue;
                }

                fulfillmentPlanIDs.Add(plan.FulfillmentPlanID);
            }

            FulfillmentPlanIDs = fulfillmentPlanIDs;

            Close();
        }

        private void tsbSelectNone_Click(object sender, EventArgs e)
        {
            dgvFulfillmentPlans.Rows.OfType<DataGridViewRow>().ForEach(dgvr => dgvr.Selected = false);

            if (!CheckUnselectedPlans() && !this.Confirm("Unselecting a Fulfillment Plan where this Purchase Order Line is the only selected line will delete the Fulfillment Plan. Continue?"))
            {
                return;
            }

            FulfillmentPlanIDs = Enumerable.Empty<long?>();

            Close();
        }

        private bool CheckUnselectedPlans()
        {
            foreach(DataGridViewRow row in dgvFulfillmentPlans.Rows)
            {
                if (row.Selected)
                {
                    continue;
                }

                FulfillmentPlan plan = row.Tag as FulfillmentPlan;
                if (plan == null)
                {
                    continue;
                }

                if (plan.FulfillmentPlanPurchaseOrderLines != null &&
                    plan.FulfillmentPlanPurchaseOrderLines.Count == 1 &&
                    plan.FulfillmentPlanPurchaseOrderLines.Single().PurchaseOrderLineID == PurchaseOrderLineIDFor)
                {
                    return false;
                }
            }

            return true;
        }

        private async void tsbClone_Click(object sender, EventArgs e)
        {
            if (dgvFulfillmentPlans.SelectedRows.Count == 0)
            {
                return;
            }

            PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlan/Clone");
            post.AddGovHeader(GovernmentIDOrigin);

            foreach(DataGridViewRow row in dgvFulfillmentPlans.SelectedRows)
            {
                FulfillmentPlan plan = row.Tag as FulfillmentPlan;
                if (plan == null)
                {
                    continue;
                }

                post.ObjectToPost = new { plan.FulfillmentPlanID };
                FulfillmentPlan clonedPlan = await post.Execute<FulfillmentPlan>();

                if (post.RequestSuccessful && clonedPlan != null && clonedPlan.FulfillmentPlanPurchaseOrderLines.Any(fppol => fppol.PurchaseOrderLineID == PurchaseOrderLineIDFor))
                {
                    FulfillmentPlanIDs = FulfillmentPlanIDs.Concat(new[] { clonedPlan.FulfillmentPlanID });
                }
            }

            await ReloadData();
        }
    }
}
