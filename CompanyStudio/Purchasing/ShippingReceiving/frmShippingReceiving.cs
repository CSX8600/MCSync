using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.ShippingReceiving
{
    [UriReachable("shippingreceiving")]
    public partial class frmShippingReceiving : BaseCompanyStudioContent, ILocationScoped
    {
        public frmShippingReceiving()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        private object selectAll = new object();
        private HashSet<Railcar> ShippingCars = new HashSet<Railcar>();
        private HashSet<Railcar> ReceivingCars = new HashSet<Railcar>();

        private async void frmShippingReceiving_Load(object sender, EventArgs e)
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                suppressCarsCheckedEvent = true;
                suppressTracksCheckedEvent = true;

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetForShippingReceiving");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<Railcar> railcars = await get.GetObject<List<Railcar>>() ?? new List<Railcar>();

                get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetAllRelatedToLocation");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<PurchaseOrder> purchaseOrders = await get.GetObject<List<PurchaseOrder>>() ?? new List<PurchaseOrder>();

                Dictionary<long?, string> trackNamesByID = new Dictionary<long?, string>();
                foreach (Railcar railcar in railcars)
                {
                    trackNamesByID[railcar.RailLocation.Track.TrackID] = railcar.RailLocation.Track.Name;
                }

                foreach (IGrouping<long?, Railcar> railcarsByTrack in railcars.GroupBy(r => r.RailLocation.Track.TrackID).OrderBy(g => g.Key))
                {
                    chkTracks.Items.Add(new DropDownItem<long?>(railcarsByTrack.Key, trackNamesByID[railcarsByTrack.Key]), true);

                    foreach (Railcar railcar in railcarsByTrack)
                    {
                        chkCars.Items.Add(new DropDownItem<Railcar>(railcar, railcar.ReportingID + " (" + trackNamesByID[railcarsByTrack.Key] + ")"), true);

                        PurchaseOrder relatedPurchaseOrder = purchaseOrders.Where(po =>
                        {
                            if (po.PurchaseOrderLines?.Any(
                                pol => pol.FulfillmentPlanPurchaseOrderLines?.Any(fppol =>
                                    fppol.FulfillmentPlan?.RailcarID == railcar.RailcarID) ?? false) ?? false)
                            {
                                return true;
                            }

                            if (railcar.RailcarLoads?.Any(rl => rl.PurchaseOrderLine?.PurchaseOrderID == po.PurchaseOrderID) ?? false)
                            {
                                return true;
                            }

                            return false;
                        }).FirstOrDefault();

                        if (relatedPurchaseOrder != null)
                        {
                            if (relatedPurchaseOrder.LocationIDDestination == LocationModel.LocationID)
                            {
                                ShippingCars.Add(railcar);
                                AddShippingCar(railcar);
                            }
                            else if (relatedPurchaseOrder.LocationIDOrigin == LocationModel.LocationID)
                            {
                                ReceivingCars.Add(railcar);
                                AddReceivingCar(railcar);
                            }
                        }
                    }
                }

                chkTracks.Items.Insert(0, new DropDownItem<object>(selectAll, "(Select All)"));
                chkTracks.SetItemChecked(0, true);
                chkCars.Items.Insert(0, new DropDownItem<object>(selectAll, "(Select All)"));
                chkCars.SetItemChecked(0, true);

                suppressCarsCheckedEvent = false;
                suppressTracksCheckedEvent = false;
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void AddShippingCar(Railcar railcar)
        {
            Shipping shipping = new Shipping()
            {
                RailcarID = railcar.RailcarID,
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                Width = splitContainer2.Panel2.Width
            };
            shipping.CarLoaded += Shipping_CarLoaded;
            shipping.CarReleased += Shipping_CarReleased;

            int top = 0;
            if (splitContainer2.Panel2.Controls.Count > 0)
            {
                top = splitContainer2.Panel2.Controls.OfType<Control>().Max(c => c.Bottom);
            }
            shipping.Top = top;
            splitContainer2.Panel2.Controls.Add(shipping);
        }

        private void AddReceivingCar(Railcar railcar)
        {
            Receiving receiving = new Receiving()
            {
                Studio = Studio,
                RailcarID = railcar.RailcarID,
                Company = Company,
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                Width = splitContainer2.Panel2.Width
            };

            receiving.CarReleased += Receiving_CarReleased;
            int top = 0;
            if (splitContainer2.Panel2.Controls.Count > 0)
            {
                top = splitContainer2.Panel2.Controls.OfType<Control>().Max(c => c.Bottom);
            }
            receiving.Top = top;
            splitContainer2.Panel2.Controls.Add(receiving);
        }

        private void Receiving_CarReleased(object sender, EventArgs e)
        {
            Receiving receivingCtrl = (Receiving)sender;
            splitContainer2.Panel2.Controls.Remove(receivingCtrl);
            RestackControls();
            UpdateCheckLists(receivingCtrl.RailcarID, ReceivingCars);
        }

        private void Shipping_CarReleased(object sender, EventArgs e)
        {
            Shipping shippingCtrl = (Shipping)sender;
            splitContainer2.Panel2.Controls.Remove(shippingCtrl);
            RestackControls();
            UpdateCheckLists(shippingCtrl.RailcarID, ShippingCars);
        }

        private void UpdateCheckLists(long? railcarID, HashSet<Railcar> listToRemoveItemFrom)
        {
            DropDownItem<Railcar> railcarItem = chkCars.Items.OfType<DropDownItem<Railcar>>().Where(ddi => ddi.Object.RailcarID == railcarID).FirstOrDefault();
            if (railcarItem != null)
            {
                chkCars.Items.Remove(railcarItem);
                listToRemoveItemFrom.Remove(railcarItem.Object);
            }

            bool anyOnTrackExist = chkCars.Items.OfType<DropDownItem<Railcar>>().Any(ddi => ddi.Object.RailLocation.Track.TrackID == railcarItem.Object.RailLocation.Track.TrackID);
            if (!anyOnTrackExist)
            {
                DropDownItem<long?> trackItem = chkTracks.Items.OfType<DropDownItem<long?>>().Where(ddi => ddi.Object == railcarItem.Object.RailLocation.Track.TrackID).FirstOrDefault();
                if (trackItem != null)
                {
                    chkTracks.Items.Remove(trackItem);
                }
            }
        }

        private void RestackControls()
        {
            int top = 0;
            foreach(Control control in splitContainer2.Panel2.Controls.OfType<Control>().OrderBy(c => c.Top))
            {
                control.Top = top;
                top = control.Bottom;
            }
        }

        private void Shipping_CarLoaded(object sender, Shipping.CarLoadedEventArgs e)
        {
            Shipping shippingCtrl = (Shipping)sender;
            foreach(Shipping shipping in splitContainer2.Panel2.Controls.OfType<Shipping>().Where(s => s != shippingCtrl))
            {
                shipping.NotifyOtherCarLoaded(e.PurchaseOrderLineID);
            }
        }

        private bool suppressCarsCheckedEvent = false;
        private void chkCars_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (suppressCarsCheckedEvent) return;

            if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
            {
                DropDownItem<object> selectAllItem = chkCars.Items[e.Index] as DropDownItem<object>;
                if (selectAllItem != null && selectAllItem.Object == selectAll)
                {
                    for(int i = 0; i < chkCars.Items.Count; i++)
                    {
                        DropDownItem<Railcar> railcarItemToCheck = chkCars.Items[i] as DropDownItem<Railcar>;
                        if (railcarItemToCheck == null || i == e.Index)
                        {
                            continue;
                        }

                        chkCars.SetItemChecked(i, true);
                    }

                    return;
                }

                DropDownItem<Railcar> railcarItem = chkCars.Items[e.Index] as DropDownItem<Railcar>;
                if (ShippingCars.Contains(railcarItem.Object))
                {
                    AddShippingCar(railcarItem.Object);
                }
                else if (ReceivingCars.Contains(railcarItem.Object))
                {
                    AddReceivingCar(railcarItem.Object);
                }
            }
            else if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
            {
                DropDownItem<object> selectAllItem = chkCars.Items[e.Index] as DropDownItem<object>;
                if (selectAllItem != null && selectAllItem.Object == selectAll)
                {
                    bool uncheckAll = true;
                    for(int i = 1; i < chkCars.Items.Count; i++)
                    {
                        uncheckAll &= chkCars.GetItemChecked(i);
                    }

                    if (uncheckAll)
                    {
                        for (int i = 1; i < chkCars.Items.Count; i++)
                        {
                            DropDownItem<Railcar> railcarItemToCheck = chkCars.Items[i] as DropDownItem<Railcar>;
                            if (railcarItemToCheck == null)
                            {
                                continue;
                            }

                            chkCars.SetItemChecked(i, false);
                        }
                    }

                    return;
                }

                DropDownItem<Railcar> railcarItem = chkCars.Items[e.Index] as DropDownItem<Railcar>;
                foreach(Control ctrl in splitContainer2.Panel2.Controls)
                {
                    switch(ctrl)
                    {
                        case Shipping shipping:
                            if (shipping.RailcarID == railcarItem.Object.RailcarID)
                            {
                                splitContainer2.Panel2.Controls.Remove(shipping);
                                RestackControls();
                            }
                            break;
                        case Receiving receiving:
                            if (receiving.RailcarID == railcarItem.Object.RailcarID)
                            {
                                splitContainer2.Panel2.Controls.Remove(receiving);
                                RestackControls();
                            }
                            break;
                    }
                }
            }

            bool allChecked = true;
            for (int i = 0; i < chkCars.Items.Count; i++)
            {
                DropDownItem<Railcar> railcarItemToCheck = chkCars.Items[i] as DropDownItem<Railcar>;
                if (railcarItemToCheck == null)
                {
                    continue;
                }

                allChecked &= i == e.Index ? e.NewValue == CheckState.Checked : chkCars.GetItemChecked(i);
            }

            suppressCarsCheckedEvent = true;
            if (allChecked)
            {
                chkCars.SetItemChecked(0, true);
            }
            else
            {
                chkCars.SetItemChecked(0, false);
            }
            suppressCarsCheckedEvent = false;
        }

        private bool suppressTracksCheckedEvent = false;
        private void chkTracks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (suppressTracksCheckedEvent) return;

            DropDownItem<object> selectAllObject = chkTracks.Items[e.Index] as DropDownItem<object>;
            bool isSelectAll = selectAllObject?.Object == selectAll;
            DropDownItem<long?> trackDDI = chkTracks.Items[e.Index] as DropDownItem<long?>;

            if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
            {
                if (isSelectAll)
                {
                    for (int i = 1; i < chkTracks.Items.Count; i++)
                    {
                        if (chkTracks.Items[i] is DropDownItem<long?>)
                        {
                            chkTracks.SetItemChecked(i, true);
                        }
                    }

                    return;
                }

                foreach(Railcar railcar in ShippingCars.Concat(ReceivingCars).Where(r => r.RailLocation.Track.TrackID == trackDDI.Object))
                {
                    DropDownItem<Railcar> railcarDDI = new DropDownItem<Railcar>(railcar, railcar.ReportingID + " (" + railcar.RailLocation.Track.Name + ")");
                    chkCars.SetItemChecked(chkCars.Items.Add(railcarDDI), true);
                }
            }
            else if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
            {
                if (isSelectAll)
                {
                    bool allChecked = true;
                    for(int i = 1; i < chkTracks.Items.Count; i++)
                    {
                        allChecked &= chkTracks.GetItemChecked(i);
                    }

                    if (allChecked)
                    {
                        for(int i = 1; i < chkTracks.Items.Count; i++)
                        {
                            if (chkTracks.Items[i] is DropDownItem<long?>)
                            {
                                chkTracks.SetItemChecked(i, false);
                            }
                        }
                    }

                    return;
                }

                foreach(DropDownItem<Railcar> railcarObj in chkCars.Items.OfType<DropDownItem<Railcar>>().Where(ddi => ddi.Object.RailLocation.Track.TrackID == trackDDI.Object).ToList())
                {
                    chkCars.Items.Remove(railcarObj);

                    Shipping shippingControl = splitContainer2.Panel2.Controls.OfType<Shipping>().FirstOrDefault(s => s.RailcarID == railcarObj.Object.RailcarID);
                    if (shippingControl != null)
                    {
                        splitContainer2.Panel2.Controls.Remove(shippingControl);
                        RestackControls();
                    }

                    Receiving receivingControl = splitContainer2.Panel2.Controls.OfType<Receiving>().FirstOrDefault(r => r.RailcarID == railcarObj.Object.RailcarID);
                    if (receivingControl != null)
                    {
                        splitContainer2.Panel2.Controls.Remove(receivingControl);
                        RestackControls();
                    }
                }
            }

            bool areAllChecked = true;
            for (int i = 0; i < chkTracks.Items.Count; i++)
            {
                if (chkTracks.Items[i] is DropDownItem<long?>)
                {
                    areAllChecked &= i == e.Index ? e.NewValue == CheckState.Checked : chkTracks.GetItemChecked(i);
                }
            }

            suppressTracksCheckedEvent = true;
            if (areAllChecked)
            {
                chkTracks.SetItemChecked(0, true);
            }
            else
            {
                chkTracks.SetItemChecked(0, false);
            }
            suppressTracksCheckedEvent = false;
        }

        private void chkCars_Click(object sender, EventArgs e)
        {
            
        }
    }
}
