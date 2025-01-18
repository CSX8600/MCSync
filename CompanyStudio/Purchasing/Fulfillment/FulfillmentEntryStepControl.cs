using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Wizard;
using MesaSuite.Common.Data;
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

namespace CompanyStudio.Purchasing.Fulfillment
{
    [ToolboxItem(false)]
    public partial class FulfillmentEntryStepControl : UserControl, IWizardStep<FulfillmentWizardData>
    {
        private long? _companyID;
        private long? _locationID;

        public FulfillmentEntryStepControl()
        {
            InitializeComponent();
        }

        public string NavigationName => "Fulfillment Entry";

        public Control Control => this;

        public Task Commit(FulfillmentWizardData data)
        {
            data.Fulfillments.Clear();
            foreach(FulfillmentEntryPurchaseOrder fepo in pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>())
            {
                data.Fulfillments.AddRange(fepo.GetFulfillments());
            }

            return Task.CompletedTask;
        }

        async Task IWizardStep<FulfillmentWizardData>.Load(FulfillmentWizardData data)
        {
            _companyID = data.CompanyID;
            _locationID = data.LocationID;

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetAllRelatedToLocation");
            get.AddLocationHeader(data.CompanyID, data.LocationID);
            List<PurchaseOrder> purchaseOrders = (await get.GetObject<List<PurchaseOrder>>() ?? new List<PurchaseOrder>()).Where(po => PurchaseOrder.OPEN_STATUSES.Contains(po.Status)).ToList();

            pnlUnfulledPOLines.Controls.Clear();
            cboPurchaseOrders.Items.Clear();

            foreach (PurchaseOrder purchaseOrder in purchaseOrders)
            {
                cboPurchaseOrders.Items.Add(new DropDownItem<PurchaseOrder>(purchaseOrder, purchaseOrder.PurchaseOrderID?.ToString()));

                if ((purchaseOrder.PurchaseOrderLines.Any(pol => data.Fulfillments.Any(f => f.PurchaseOrderLineID == pol.PurchaseOrderLineID)) ||
                    purchaseOrder.PurchaseOrderLines.Any(pol => pol.FulfillmentPlanPurchaseOrderLines.Any(fppol => data.SelectedRailcars.Any(r => r.RailcarID == fppol.FulfillmentPlan.RailcarID)))) &&
                    !pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>().Any(fepo => fepo.PurchaseOrder.PurchaseOrderID == purchaseOrder.PurchaseOrderID))
                {
                    AddPurchaseOrder(purchaseOrder, data.Fulfillments.Where(f => purchaseOrder.PurchaseOrderLines.Any(pol => f.PurchaseOrderLineID == pol.PurchaseOrderLineID)).ToList());
                }
            }
        }

        private void AddPurchaseOrder(PurchaseOrder purchaseOrder, List<Models.Fulfillment> fulfillments = null)
        {
            fulfillments = fulfillments ?? new List<Models.Fulfillment>();

            FulfillmentEntryPurchaseOrder fepo = new FulfillmentEntryPurchaseOrder()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                PurchaseOrder = purchaseOrder,
                CompanyID = _companyID,
                LocationID = _locationID
            };
            fepo.SetInitialFulfillments(fulfillments);
            fepo.Width = pnlUnfulledPOLines.Width;
            int top = 0;
            if (pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>().Any())
            {
                top = pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>().Max(f => f.Bottom);
            }
            fepo.Top = top;
            fepo.SizeChanged += FEPO_SizeChanged;
            fepo.FulfillmentUpdated += FEPO_FulfillmentUpdated;
            pnlUnfulledPOLines.Controls.Add(fepo);
        }

        private void FEPO_FulfillmentUpdated(object sender, Models.Fulfillment e)
        {
            foreach(FulfillmentEntryPurchaseOrder fepo in pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>().Where(c => c != (FulfillmentEntryPurchaseOrder)sender))
            {
                fepo.SetRailcarEnabled(e.RailcarID, e.Quantity == null || e.Quantity == 0);
            }
        }

        private void FEPO_SizeChanged(object sender, EventArgs e)
        {
            int lastBottom = 0;
            foreach(FulfillmentEntryPurchaseOrder fepo in pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>().OrderBy(fepo => fepo.Top))
            {
                fepo.Top = lastBottom;
                lastBottom = fepo.Bottom;
            }
        }

        Task<List<string>> IWizardStep<FulfillmentWizardData>.Validate()
        {
            return Task.FromResult(new List<string>());
        }

        private void cmdAddPO_Click(object sender, EventArgs e)
        {
            DropDownItem<PurchaseOrder> selectedPurchaseOrder = cboPurchaseOrders.SelectedItem as DropDownItem<PurchaseOrder>;
            if (selectedPurchaseOrder == null)
            {
                return;
            }

            if (pnlUnfulledPOLines.Controls.OfType<FulfillmentEntryPurchaseOrder>().Any(fepo => fepo.PurchaseOrder.PurchaseOrderID == selectedPurchaseOrder.Object.PurchaseOrderID))
            {
                return;
            }

            AddPurchaseOrder(selectedPurchaseOrder.Object);
        }
    }
}
