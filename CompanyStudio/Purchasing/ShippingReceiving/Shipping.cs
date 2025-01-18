using CompanyStudio.Extensions;
using CompanyStudio.Models;
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

namespace CompanyStudio.Purchasing.ShippingReceiving
{
    [ToolboxItem(false)]
    public partial class Shipping : UserControl
    {
        public event EventHandler CarReleased;
        public event EventHandler<CarLoadedEventArgs> CarLoaded;

        private List<Image> images = new List<Image>();

        public long? RailcarID { get; set; }
        private decimal? RailcarCapacity { get; set; }
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }

        public Shipping()
        {
            InitializeComponent();
        }

        private async void Shipping_Load(object sender, EventArgs e)
        {
            Railcar railcar = await LoadRailcarInformation();
            RailcarCapacity = railcar.RailcarModel.CargoCapacity;

            if (cmdAddLoad.Enabled)
            {
                await ReloadSuggestedPOLine();
            }
        }

        private async Task ReloadSuggestedPOLine()
        {
            cboPOLine.SelectedItem = null;
            cboPOLine.Items.Clear();

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetAllRelatedToLocation");
            get.AddLocationHeader(CompanyID, LocationID);
            List<PurchaseOrder> purchaseOrders = await get.GetObject<List<PurchaseOrder>>() ?? new List<PurchaseOrder>();

            PurchaseOrderLine suggestedPOLine = null;
            foreach (PurchaseOrder po in purchaseOrders.Where(po => po.LocationIDDestination == LocationID).OrderBy(po => po.PurchaseOrderDate))
            {
                foreach (PurchaseOrderLine poLine in po.PurchaseOrderLines)
                {
                    DropDownItem<PurchaseOrderLine> ddi = new DropDownItem<PurchaseOrderLine>(poLine, string.Format("{0} (PO {1})", poLine.DisplayString, poLine.PurchaseOrderID));
                    cboPOLine.Items.Add(ddi);

                    decimal? loadQuantityWithoutFulfillment = 0;
                    if (poLine.Fulfillments.Any(f => !f.IsComplete) || poLine.RailcarLoads.Any())
                    {
                        loadQuantityWithoutFulfillment = Math.Max((poLine.RailcarLoads.Sum(rl => rl.Quantity) - poLine.Fulfillments.Where(f => !f.IsComplete).Sum(f => f.Quantity)) ?? 0, 0);
                    }

                    if (suggestedPOLine == null &&
                        poLine.UnfulfilledQuantity - loadQuantityWithoutFulfillment > 0 &&
                        poLine.FulfillmentPlanPurchaseOrderLines != null &&
                        poLine.FulfillmentPlanPurchaseOrderLines.Any(fppol => fppol.FulfillmentPlan?.RailcarID == RailcarID))
                    {
                        suggestedPOLine = poLine;
                    }
                }
            }

            if (suggestedPOLine != null)
            {
                decimal alreadyFulfilledAmount = 0;
                if (suggestedPOLine.Fulfillments.Any(f => !f.IsComplete) || suggestedPOLine.RailcarLoads.Any())
                {
                    alreadyFulfilledAmount = Math.Max((suggestedPOLine.RailcarLoads.Sum(rl => rl.Quantity) - suggestedPOLine.Fulfillments.Where(f => !f.IsComplete).Sum(f => f.Quantity)) ?? 0, 0);
                }

                decimal alreadyLoadedAmount = 0;
                foreach(RailcarLoad load in dgvLoads.Rows.OfType<DataGridViewRow>().Select(row => row.Tag).OfType<RailcarLoad>())
                {
                    alreadyLoadedAmount += load.Quantity ?? 0;
                }

                cboPOLine.SelectedItem = cboPOLine.Items.OfType<DropDownItem<PurchaseOrderLine>>().FirstOrDefault(ddi => ddi.Object.PurchaseOrderLineID == suggestedPOLine.PurchaseOrderLineID);
                cboItem.SelectedID = suggestedPOLine.ItemID;
                decimal suggestedQuantity = (suggestedPOLine.UnfulfilledQuantity ?? 0) - alreadyFulfilledAmount;
                if (suggestedQuantity > RailcarCapacity - alreadyLoadedAmount)
                {
                    suggestedQuantity = (RailcarCapacity ?? 0) - alreadyLoadedAmount;
                }

                if (suggestedQuantity < 0)
                {
                    suggestedQuantity = 0;
                }

                txtQuantity.Text = suggestedQuantity.ToString();
            }
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
            foreach(Image image in images)
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

            get = new GetData(DataAccess.APIs.CompanyStudio, "Fulfillment/GetCurrentByRailcar/" + RailcarID);
            get.AddLocationHeader(CompanyID, LocationID);
            List<Models.Fulfillment> fulfillments = await get.GetObject<List<Models.Fulfillment>>() ?? new List<Models.Fulfillment>();

            if (fulfillments.Any())
            {
                cboPOLine.Enabled = false;
                cboItem.Enabled = false;
                txtQuantity.Enabled = false;

                cmdAddLoad.Visible = false;
                cmdFinalizeLoading.Visible = false;
                cmdRelease.Visible = true;

                string releaseTo = "";
                RailcarRoute route = railcar.RailcarRoutes.OrderBy(rr => rr.SortOrder).FirstOrDefault();
                if (route != null)
                {
                    releaseTo = route.To;
                }
                else // If the route was fulfilled, but returned to a company, then the route will be gone. We can fallback on the fulfillment plan route
                {
                    get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/GetByRailcar/" + RailcarID);
                    get.AddLocationHeader(CompanyID, LocationID);
                    FulfillmentPlan plan = await get.GetObject<FulfillmentPlan>();
                    FulfillmentPlanRoute planRoute = plan.FulfillmentPlanRoutes?.OrderBy(fpr => fpr.SortOrder).FirstOrDefault();

                    if (planRoute != null)
                    {
                        releaseTo = planRoute.To;
                        route = new RailcarRoute()
                        {
                            RailcarID = RailcarID,
                            SortOrder = 1,
                            CompanyIDFrom = planRoute.CompanyIDFrom,
                            GovernmentIDFrom = planRoute.GovernmentIDFrom,
                            CompanyIDTo = planRoute.CompanyIDTo,
                            GovernmentIDTo = planRoute.GovernmentIDTo
                        };
                    }
                }
                cmdRelease.Text = "Release to " + releaseTo;
                cmdRelease.Tag = route;                
            }

            return railcar;
        }

        private async void cmdAddLoad_Click(object sender, EventArgs e)
        {
            if (cboItem.SelectedID == null ||
                string.IsNullOrEmpty(txtQuantity.Text) ||
                !decimal.TryParse(txtQuantity.Text, out decimal quantity) ||
                quantity <= 0)
            {
                this.ShowError("Item and Quantity is required, and Quantity must be greater than 0");
                return;
            }

            if (cboPOLine.SelectedItem != null)
            {
                PurchaseOrderLine selectedLine = (cboPOLine.SelectedItem as DropDownItem<PurchaseOrderLine>).Object;
                if (!dgvLoads.Rows.OfType<DataGridViewRow>().Select(dgvr => dgvr.Tag as RailcarLoad).All(rl => rl.PurchaseOrderLineID == null || rl.PurchaseOrderLineID == selectedLine.PurchaseOrderLineID))
                {
                    this.ShowError("Only Purchase Order Lines from the same Purchase Order may be loaded at one time");
                    return;
                }
            }

            RailcarLoad newLoad = new RailcarLoad()
            {
                RailcarID = RailcarID,
                ItemID = cboItem.SelectedID,
                PurchaseOrderLineID = (cboPOLine.SelectedItem as DropDownItem<PurchaseOrderLine>)?.Object.PurchaseOrderLineID,
                Quantity = quantity
            };

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Railcar/AddRailcarLoad", newLoad);
            post.AddLocationHeader(CompanyID, LocationID);
            await post.ExecuteNoResult();

            if (post.RequestSuccessful)
            {
                CarLoaded?.Invoke(this, new CarLoadedEventArgs()
                {
                    PurchaseOrderLineID = newLoad.PurchaseOrderLineID,
                    Quantity = quantity
                });
            }

            await LoadRailcarInformation();
            await ReloadSuggestedPOLine();
        }

        private async void cmdFinalizeLoading_Click(object sender, EventArgs e)
        {
            bool atLeastOneFulfillment = false;

            List<long?> fulfillmentIDs = new List<long?>();
            foreach(DataGridViewRow row in dgvLoads.Rows)
            {
                RailcarLoad load = row.Tag as RailcarLoad;
                if (load == null || load.PurchaseOrderLineID == null || load.Quantity == null || load.Quantity <= 0)
                {
                    continue;
                }

                Models.Fulfillment fulfillment = new Models.Fulfillment()
                {
                    RailcarID = RailcarID,
                    PurchaseOrderLineID = load.PurchaseOrderLineID,
                    Quantity = load.Quantity,
                    FulfillmentTime = DateTime.Now
                };

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Fulfillment/Post", fulfillment);
                post.AddLocationHeader(CompanyID, LocationID);
                Models.Fulfillment savedFulfillment = await post.Execute<Models.Fulfillment>();
                if (post.RequestSuccessful)
                {
                    atLeastOneFulfillment = true;
                    fulfillmentIDs.Add(savedFulfillment.FulfillmentID);
                }
            }

            if (atLeastOneFulfillment)
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Fulfillment/IssueBillsOfLading", new { fulfillmentIDs });
                post.AddLocationHeader(CompanyID, LocationID);
                await post.ExecuteNoResult();

                await LoadRailcarInformation();
            }
            else
            {
                this.ShowError("Finalizing is not possible without at least one load issued to a Purchase Order");
            }
        }

        private async void cmdRelease_Click(object sender, EventArgs e)
        {
            RailcarRoute route = cmdRelease.Tag as RailcarRoute;
            if (route == null)
            {
                return;
            }

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Railcar/Release", new { RailcarID, CompanyIDReleaseTo = route.CompanyIDTo, GovernmentIDReleaseTo = route.GovernmentIDTo });
            post.AddLocationHeader(CompanyID, LocationID);
            await post.ExecuteNoResult();
            if (post.RequestSuccessful)
            {
                CarReleased?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void dgvLoads_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLoads.Rows.Count || e.ColumnIndex != colIndClear.Index)
            {
                return;
            }

            RailcarLoad load = dgvLoads.Rows[e.RowIndex].Tag as RailcarLoad;
            if (load == null || load.RailcarLoadID == null)
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "Railcar/DeleteRailcarLoad/" + load.RailcarLoadID);
            delete.AddLocationHeader(CompanyID, LocationID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                CarLoaded?.Invoke(this, new CarLoadedEventArgs()
                {
                    PurchaseOrderLineID = load.PurchaseOrderLineID,
                    Quantity = -load.Quantity
                });
            }

            await LoadRailcarInformation();
            await ReloadSuggestedPOLine();
        }

        public async void NotifyOtherCarLoaded(long? purchaseOrderLineID)
        {
            if (cboPOLine.SelectedItem is DropDownItem<PurchaseOrderLine> poLine && poLine.Object.PurchaseOrderLineID == purchaseOrderLineID)
            {
                await ReloadSuggestedPOLine();
            }
        }

        public class CarLoadedEventArgs : EventArgs
        {
            public long? PurchaseOrderLineID { get; set; }
            public decimal? Quantity { get; set; }
        }
    }
}
