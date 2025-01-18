using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.ShippingReceiving
{
    [ToolboxItem(false)]
    public partial class Receiving : UserControl
    {
        public frmStudio Studio { get; set; }
        public Company Company { get; set; }
        public event EventHandler CarReleased;

        private List<Image> images = new List<Image>();

        public long? RailcarID { get; set; }
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }

        public Receiving()
        {
            InitializeComponent();
        }

        private async void Receiving_Load(object sender, EventArgs e)
        {
            await LoadRailcarInformation();
        }

        /// <summary>
        /// Loads railcar information at the top of the screen as well as railcar load information
        /// </summary>
        /// <param name="get"></param>
        /// <param name="railcar"></param>
        /// <returns>The loaded railcar object</returns>
        private async Task<Railcar> LoadRailcarInformation()
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/Get/" + RailcarID);
            get.AddLocationHeader(CompanyID, LocationID);
            Railcar railcar = await get.GetObject<Railcar>();
            if (railcar == null)
            {
                return null;
            }

            lblRailcar.Text = railcar.ReportingID;
            lblTrack.Text = railcar.RailLocation?.Track?.Name;
            lblPosition.Text = $"Position: {railcar.RailLocation?.Position}";

            dgvLoads.Rows.Clear();
            foreach (Image image in images)
            {
                try
                {
                    image.Dispose();
                }
                catch { }
            }

            foreach (RailcarLoad load in railcar.RailcarLoads ?? new List<RailcarLoad>())
            {
                DataGridViewRow row = dgvLoads.Rows[dgvLoads.Rows.Add()];
                row.Cells[colItem.Name].Value = load.Item?.Name;
                row.Cells[colQuantity.Name].Value = load.Quantity;

                string purchaseOrderLine = "";
                if (load.PurchaseOrderLineID != null)
                {
                    purchaseOrderLine = string.Format("{0} (PO {1})", load.PurchaseOrderLine.DisplayString, load.PurchaseOrderLine.PurchaseOrderID);
                }

                row.Cells[colPOLine.Name].Value = purchaseOrderLine;
                Image loadImage = load.Item?.GetImage();
                if (loadImage != null)
                {
                    row.Cells[colImage.Name].Value = loadImage;
                    images.Add(loadImage);
                }

                row.Tag = load;
            }

            get = new GetData(DataAccess.APIs.CompanyStudio, "BillOfLading/GetByRailcar/" + RailcarID);
            get.AddLocationHeader(CompanyID, LocationID);
            List<BillOfLading> billsOfLading = (await get.GetObject<List<BillOfLading>>() ?? new List<BillOfLading>()).Where(bol => bol.CompanyIDConsignee == CompanyID).ToList();

            List<string> bolIDs = new List<string>();
            foreach(BillOfLading billOfLading in billsOfLading)
            {
                bolIDs.Add(billOfLading.BillOfLadingID.ToString());
            }

            lnkBOL.Text = string.Join(", ", bolIDs);
            lnkBOL.Tag = billsOfLading;
            lblAcceptBOL.Visible = billsOfLading.Any(bol => bol.DeliveredDate == null);

            get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/GetByRailcar/" + RailcarID);
            get.AddLocationHeader(CompanyID, LocationID);
            FulfillmentPlan fulfillmentPlan = await get.GetObject<FulfillmentPlan>();
            FulfillmentPlanRoute lastRoute = fulfillmentPlan?.FulfillmentPlanRoutes?.OrderByDescending(fpr => fpr.SortOrder).FirstOrDefault();
            if (lastRoute == null)
            {
                lnkRelease.Visible = false;
            }
            else
            {
                lnkRelease.Text = $"Release to {lastRoute.From}";
                lnkRelease.Tag = lastRoute;
            }

            bool notCompleted = (railcar.RailcarLoads?.Any(r => r.PurchaseOrderLineID != null) ?? false) ||
                                railcar.TrackDestination.CompanyIDOwner == CompanyID;

            if (!notCompleted)
            {
                lnkClearAllLoads.Enabled = false;
                lnkCompleteReceiving.Enabled = false;
            }

            lblUnloadCompletionWarning.Visible = fulfillmentPlan == null;

            return railcar;
        }

        private void lnkBOL_SizeChanged(object sender, EventArgs e)
        {
            lblAcceptBOL.Left = lnkBOL.Location.X + lnkBOL.Width + 6;
        }

        private void picCompleteReceivingInfo_Click(object sender, EventArgs e)
        {
            this.ShowInformation(toolTip.GetToolTip(picCompleteReceivingInfo));
        }

        private async void lblAcceptBOL_Click(object sender, EventArgs e)
        {
            List<BillOfLading> billsOfLading = lnkBOL.Tag as List<BillOfLading>;
            if (billsOfLading == null)
            {
                return;
            }

            bool canHideLabel = true;
            foreach (BillOfLading billOfLading in billsOfLading.Where(bol => bol.DeliveredDate == null))
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "BillOfLading/AcceptBOL", new { billOfLading.BillOfLadingID });
                post.AddLocationHeader(CompanyID, LocationID);
                await post.ExecuteNoResult();
                if (post.RequestSuccessful)
                {
                    billOfLading.DeliveredDate = DateTime.Now;
                }
                else
                {
                    canHideLabel = false;
                }
            }

            lblAcceptBOL.Visible = !canHideLabel;
        }

        private async void lnkClearAllLoads_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblAcceptBOL.Visible)
            {
                this.ShowError("The Bill of Lading must be accepted before railcar loads can be marked unloaded");
                return;
            }

            foreach(DataGridViewRow row in dgvLoads.Rows)
            {
                RailcarLoad load = row.Tag as RailcarLoad;
                if (load == null)
                {
                    continue;
                }

                DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "Railcar/DeleteRailcarLoad/" + load.RailcarLoadID);
                delete.AddLocationHeader(CompanyID, LocationID);
                await delete.Execute();

                if (delete.RequestSuccessful && lblUnloadCompletionWarning.Visible)
                {
                    CarReleased?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

            await LoadRailcarInformation();
        }

        private async void lnkCompleteReceiving_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Railcar/CompleteReceiving", new { RailcarID });
            post.AddLocationHeader(CompanyID, LocationID);
            await post.ExecuteNoResult();

            await LoadRailcarInformation();
        }

        private async void lnkRelease_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FulfillmentPlanRoute route = lnkRelease.Tag as FulfillmentPlanRoute;

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Railcar/Release", new { RailcarID, CompanyIDReleaseTo = route.CompanyIDFrom, GovernmentIDReleaseTo = route.GovernmentIDFrom });
            post.AddLocationHeader(CompanyID, LocationID);
            await post.ExecuteNoResult();

            CarReleased?.Invoke(this, EventArgs.Empty);
        }

        private async void dgvLoads_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colIndClear.Index || e.RowIndex < 0 || e.RowIndex >= dgvLoads.Rows.Count)
            {
                return;
            }

            if (lblAcceptBOL.Visible)
            {
                this.ShowError("The Bill of Lading must be accepted before railcar loads can be marked unloaded");
                return;
            }

            RailcarLoad railcarLoad = dgvLoads.Rows[e.RowIndex].Tag as RailcarLoad;
            if (railcarLoad == null)
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "Railcar/DeleteRailcarLoad/" + railcarLoad.RailcarLoadID);
            delete.AddLocationHeader(CompanyID, LocationID);
            await delete.Execute();
            if (delete.RequestSuccessful && 
                !dgvLoads.Rows.OfType<DataGridViewRow>().Select(dgvr => dgvr.Tag).OfType<RailcarLoad>().Where(rl => rl.RailcarLoadID != railcarLoad.RailcarLoadID && rl.PurchaseOrderLineID != null).Any() &&
                lblUnloadCompletionWarning.Visible)
            {
                CarReleased?.Invoke(this, EventArgs.Empty);
                return;
            }

            await LoadRailcarInformation();
        }

        private void lnkBOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<BillOfLading> billsOfLading = lnkBOL.Tag as List<BillOfLading>;
            if (billsOfLading == null)
            {
                return;
            }

            billsOfLading.ForEach(bol => bol.DisplayReport(Studio, Company));
        }
    }
}
