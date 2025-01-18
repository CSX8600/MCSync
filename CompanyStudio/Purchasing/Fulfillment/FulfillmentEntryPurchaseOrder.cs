using CompanyStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.Fulfillment
{
    [ToolboxItem(false)]
    public partial class FulfillmentEntryPurchaseOrder : UserControl
    {
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }

        public event EventHandler<Models.Fulfillment> FulfillmentUpdated;

        public PurchaseOrder PurchaseOrder { get; set; }

        public FulfillmentEntryPurchaseOrder()
        {
            InitializeComponent();
        }

        private List<Models.Fulfillment> _initialFulfillments = new List<Models.Fulfillment>();
        public void SetInitialFulfillments(List<Models.Fulfillment> fulfillments)
        {
            _initialFulfillments = fulfillments ?? new List<Models.Fulfillment>();
        }

        private void FulfillmentEntryPurchaseOrder_Load(object sender, EventArgs e)
        {
            lblPOID.Text = $"PO {PurchaseOrder.PurchaseOrderID}";

            foreach(PurchaseOrderLine line in PurchaseOrder.PurchaseOrderLines)
            {
                List<FulfillmentPlan> fulfillmentPlans = line.FulfillmentPlanPurchaseOrderLines.Select(fppol => fppol.FulfillmentPlan).ToList();
                List<Models.Fulfillment> fulfillments = _initialFulfillments.Where(f => f.PurchaseOrderLineID == line.PurchaseOrderLineID).ToList();
                
                if (fulfillmentPlans.Any())
                {
                    HashSet<Models.Fulfillment> fulfillmentsAssociatedWithPlan = new HashSet<Models.Fulfillment>();
                    foreach(FulfillmentPlan plan in fulfillmentPlans)
                    {
                        Models.Fulfillment fulfillment = fulfillments.FirstOrDefault(f => f.RailcarID == plan.RailcarID);
                        if (fulfillment == null)
                        {
                            fulfillment = new Models.Fulfillment()
                            {
                                PurchaseOrderLineID = line.PurchaseOrderLineID,
                                PurchaseOrderLine = line,
                                RailcarID = plan.RailcarID,
                                Railcar = plan.Railcar
                            };
                        }
                        else
                        {
                            fulfillmentsAssociatedWithPlan.Add(fulfillment);
                        }

                        AddFulfillment(line, fulfillment);
                    }

                    foreach(Models.Fulfillment nonPlanFulfillment in fulfillments.Where(f => !fulfillmentsAssociatedWithPlan.Contains(f)))
                    {
                        AddFulfillment(line, nonPlanFulfillment);
                    }
                }
                else
                {
                    if (fulfillments.Any())
                    {
                        foreach(Models.Fulfillment fulfillment in fulfillments)
                        {
                            AddFulfillment(line, fulfillment);
                        }
                    }
                    else
                    {
                        AddFulfillment(line, null);
                    }
                }
            }
        }

        private void AddFulfillment(PurchaseOrderLine purchaseOrderLine, Models.Fulfillment fulfillment)
        {
            FulfillmentEntryPOLine line = new FulfillmentEntryPOLine()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                PurchaseOrderLine = purchaseOrderLine,
                Fulfillment = fulfillment,
                CompanyID = CompanyID,
                LocationID = LocationID
            };
            int top = 0;
            if (pnlUnfulfilledLines.Controls.OfType<FulfillmentEntryPOLine>().Any())
            {
                top = pnlUnfulfilledLines.Controls.OfType<FulfillmentEntryPOLine>().Max(f => f.Bottom);
            }
            line.Top = top;
            line.Width = pnlUnfulfilledLines.Width;
            line.SizeChanged += FEPOL_SizeChanged;
            line.SplitToAnotherCarClicked += FEPOL_SplitToAnotherCarClicked;
            line.RailcarChanged += FEPOL_RailcarChanged;
            line.QuantityLoadedChanged += (s, e) => FulfillmentUpdated?.Invoke(this, line.Fulfillment);
            pnlUnfulfilledLines.Controls.Add(line);
            Height = pnlUnfulfilledLines.Top + line.Bottom + 3;
        }

        private void FEPOL_RailcarChanged(object sender, CancelEventArgs e)
        {
            FulfillmentEntryPOLine fepol = sender as FulfillmentEntryPOLine;
            long? railcarID = fepol.Fulfillment?.RailcarID;
            if (railcarID == null)
            {
                return;
            }

            e.Cancel = pnlUnfulfilledLines.Controls.OfType<FulfillmentEntryPOLine>().Where(ctrl => ctrl.Fulfillment?.RailcarID == railcarID).Any();

            if (!e.Cancel)
            {
                FulfillmentUpdated?.Invoke(this, fepol.Fulfillment);
            }
        }

        private void FEPOL_SplitToAnotherCarClicked(object sender, EventArgs e)
        {
            FulfillmentEntryPOLine fepol = sender as FulfillmentEntryPOLine;
            AddFulfillment(fepol.PurchaseOrderLine, new Models.Fulfillment() { PurchaseOrderLineID = fepol.PurchaseOrderLine.PurchaseOrderLineID, PurchaseOrderLine = fepol.PurchaseOrderLine });
        }

        private void FEPOL_SizeChanged(object sender, EventArgs e)
        {
            int lastBottom = 0;
            foreach (FulfillmentEntryPOLine fepo in pnlUnfulfilledLines.Controls.OfType<FulfillmentEntryPOLine>().OrderBy(fepo => fepo.Top))
            {
                fepo.Top = lastBottom;
                lastBottom = fepo.Bottom;
            }
        }

        public List<Models.Fulfillment> GetFulfillments()
        {
            List<Models.Fulfillment> fulfillments = new List<Models.Fulfillment>();
            foreach(FulfillmentEntryPOLine fepo in pnlUnfulfilledLines.Controls.OfType<FulfillmentEntryPOLine>())
            {
                fulfillments.Add(fepo.Fulfillment);
            }

            return fulfillments;
        }

        public void SetRailcarEnabled(long? railcarID, bool enabled)
        {
            foreach(FulfillmentEntryPOLine fepol in pnlUnfulfilledLines.Controls.OfType<FulfillmentEntryPOLine>().Where(c => c.Fulfillment?.RailcarID == railcarID))
            {
                fepol.SetEntryEnabled(enabled);
            }
        }
    }
}
