using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Purchasing.OpenMaintenance
{
    public partial class frmManualFulfillmentEntry : Form
    {
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public ThemeBase Theme { get; set; }
        public long? PurchaseOrderID { get; set; }
        public frmManualFulfillmentEntry()
        {
            InitializeComponent();
        }

        private async void frmManualFulfillmentEntry_Load(object sender, EventArgs e)
        {
            studioFormExtender.ApplyStyle(this, Theme);

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
            get.AddLocationHeader(CompanyID, LocationID);
            PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
            if (get.RequestSuccessful)
            {
                HashSet<string> reportingMarks = new HashSet<string>();
                foreach(PurchaseOrderLine purchaseOrderLine in purchaseOrder.PurchaseOrderLines ?? new List<PurchaseOrderLine>())
                {
                    DropDownItem<PurchaseOrderLine> ddi = new DropDownItem<PurchaseOrderLine>(purchaseOrderLine, purchaseOrderLine.DisplayString);
                    cboPOLine.Items.Add(ddi);

                    foreach(Railcar railcar in purchaseOrderLine.FulfillmentPlanPurchaseOrderLines.Select(fppol => fppol.FulfillmentPlan.Railcar))
                    {
                        reportingMarks.Add(railcar.ReportingID);
                    }
                }

                txtRailcar.AutoCompleteCustomSource.AddRange(reportingMarks.ToArray());
            }
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ("Purchase Order Line", cboPOLine),
                ("Quantity", txtQuantity)
            }))
            {
                return;
            }

            if (!decimal.TryParse(txtQuantity.Text, out decimal quantity))
            {
                this.ShowError("Quantity must be a number.");
                return;
            }

            long? railcarID = null;
            if (!string.IsNullOrEmpty(txtRailcar.Text))
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetByReportingMark/" + txtRailcar.Text);
                get.AddLocationHeader(CompanyID, LocationID);
                Railcar railcar = await get.GetObject<Railcar>();
                if (!get.RequestSuccessful || railcar == null)
                {
                    this.ShowError("Entered railcar was not found");
                    return;
                }

                railcarID = railcar.RailcarID;
            }

            Models.Fulfillment newFulfillment = new Models.Fulfillment()
            {
                PurchaseOrderLineID = ((DropDownItem<PurchaseOrderLine>)cboPOLine.SelectedItem).Object.PurchaseOrderLineID,
                RailcarID = railcarID,
                Quantity = quantity,
                FulfillmentTime = dtpFulfillmentTime.Value
            };

            PostData postData = new PostData(DataAccess.APIs.CompanyStudio, "Fulfillment/Post", newFulfillment);
            postData.AddLocationHeader(CompanyID, LocationID);
            await postData.ExecuteNoResult();

            if (postData.RequestSuccessful)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cboPOLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownItem<PurchaseOrderLine> purchaseOrderLine = cboPOLine.SelectedItem as DropDownItem<PurchaseOrderLine>;
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                txtQuantity.Text = purchaseOrderLine.Object.UnfulfilledQuantity?.ToString();
            }
        }
    }
}
