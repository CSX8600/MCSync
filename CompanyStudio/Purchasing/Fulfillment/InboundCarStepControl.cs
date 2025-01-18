using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Wizard;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.Fulfillment
{
    [ToolboxItem(false)]
    public partial class InboundCarStepControl : UserControl, IWizardStep<FulfillmentWizardData>
    {
        private long? CompanyID { get; set; }
        private long? LocationID { get; set; }
        public InboundCarStepControl()
        {
            InitializeComponent();
        }

        public string NavigationName => "Car Select";

        public Control Control => this;

        public Task Commit(FulfillmentWizardData data)
        {
            data.SelectedRailcars.Clear();

            foreach(ListViewItem lvi in lstCars.Items)
            {
                Railcar railcar = lvi.Tag as Railcar;
                if (railcar == null)
                {
                    continue;
                }

                data.SelectedRailcars.Add(railcar);
            }

            return Task.CompletedTask;
        }

        async Task IWizardStep<FulfillmentWizardData>.Load(FulfillmentWizardData data)
        {
            CompanyID = data.CompanyID;
            LocationID = data.LocationID;

            cboTrack.Items.Clear();
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Track/GetAll");
            get.AddLocationHeader(data.CompanyID, data.LocationID);
            List<Track> tracks = await get.GetObject<List<Track>>() ?? new List<Track>();
            foreach(Track track in tracks.Where(t => t.CompanyIDOwner == data.CompanyID).OrderBy(t => t.Name))
            {
                cboTrack.Items.Add(new DropDownItem<Track>(track, track.Name));
            }
            
            cboPurchaseOrderLines.Items.Clear();
            get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetAllRelatedToLocation");
            get.AddLocationHeader(data.CompanyID, data.LocationID);
            List<PurchaseOrder> purchaseOrders = await get.GetObject<List<PurchaseOrder>>() ?? new List<PurchaseOrder>();
            foreach(PurchaseOrder purchaseOrder in purchaseOrders.Where(po => po.LocationIDDestination == data.LocationID).OrderBy(po => po.PurchaseOrderID))
            {
                foreach(PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines.Where(pol => pol.UnfulfilledQuantity > 0))
                {
                    cboPurchaseOrderLines.Items.Add(new DropDownItem<PurchaseOrderLine>(line, string.Format("PO {0} - {1}", purchaseOrder.PurchaseOrderID, line.DisplayString)));
                }
            }

            lstCars.Items.Clear();
            foreach(Railcar railcar in data.SelectedRailcars)
            {
                AddRailcarToList(railcar);
            }
        }

        private void AddRailcarToList(Railcar railcar)
        {
            ListViewItem lstItem = new ListViewItem();
            lstItem.Text = railcar.ReportingID;
            lstItem.Tag = railcar;
            lstCars.Items.Add(lstItem);
        }

        Task<List<string>> IWizardStep<FulfillmentWizardData>.Validate()
        {
            return Task.FromResult(new List<string>());
        }

        private async void cmdAddEntireTrack_Click(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedTrack = cboTrack.SelectedItem as DropDownItem<Track>;
            if (selectedTrack == null)
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetOnTrack/" + selectedTrack.Object.TrackID);
            get.AddLocationHeader(CompanyID, LocationID);
            List<Railcar> railcars = await get.GetObject<List<Railcar>>() ?? new List<Railcar>();

            HashSet<long?> alreadyAddedRailcarIDs = lstCars.Items.Cast<ListViewItem>().Select(lvi => (lvi.Tag as Railcar)?.RailcarID).Where(x => x != null).ToHashSet();
            foreach(Railcar railcar in railcars)
            {
                if (alreadyAddedRailcarIDs.Contains(railcar.RailcarID))
                {
                    continue;
                }

                AddRailcarToList(railcar);
            }
        }

        private async void cmdAddFromPOLine_Click(object sender, EventArgs e)
        {
            DropDownItem<PurchaseOrderLine> selectedLine = cboPurchaseOrderLines.SelectedItem as DropDownItem<PurchaseOrderLine>;
            if (selectedLine == null)
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetForPurchaseOrderLine/" + selectedLine.Object.PurchaseOrderLineID);
            get.AddLocationHeader(CompanyID, LocationID);
            List<Railcar> railcars = await get.GetObject<List<Railcar>>() ?? new List<Railcar>();
            
            HashSet<long?> alreadyAddedRailcarIDs = lstCars.Items.Cast<ListViewItem>().Select(lvi => (lvi.Tag as Railcar)?.RailcarID).Where(x => x != null).ToHashSet();
            foreach(Railcar railcar in railcars)
            {
                if (alreadyAddedRailcarIDs.Contains(railcar.RailcarID))
                {
                    continue;
                }

                AddRailcarToList(railcar);
            }
        }

        private void txtRailcar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                cmdAddCarByID.PerformClick();
            }
        }

        private async void cmdAddCarByID_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRailcar.Text))
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetByReportingMark/" + txtRailcar.Text);
            get.AddLocationHeader(CompanyID, LocationID);
            Railcar railcar = await get.GetObject<Railcar>();

            if (railcar == null)
            {
                this.ShowError(txtRailcar.Text + " was not found");
            }
            else
            {
                AddRailcarToList(railcar);
            }

            txtRailcar.Clear();
            txtRailcar.Focus();
        }

        private void txtBOLNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                cmdAddCarByBOL.PerformClick();
            }
        }

        private async void cmdAddCarByBOL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBOLNo.Text))
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetByBillOfLading/" + txtBOLNo.Text);
            get.AddLocationHeader(CompanyID, LocationID);
            Railcar railcar = await get.GetObject<Railcar>();

            if (railcar == null)
            {
                this.ShowError("Railcar associated with BOL " + txtBOLNo.Text + ", or the BOL itself, was not found");
            }
            else
            {
                AddRailcarToList(railcar);
            }
        }

        private void lstCars_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }

            foreach(ListViewItem lvi in lstCars.SelectedItems.ToList())
            {
                lstCars.Items.Remove(lvi);
            }
        }
    }
}
