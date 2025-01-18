using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.Fulfillment
{
    [ToolboxItem(false)]
    public partial class FulfillmentEntryPOLine : UserControl
    {
        public event EventHandler SplitToAnotherCarClicked;
        public event CancelEventHandler RailcarChanged;
        public event EventHandler QuantityLoadedChanged;

        public PurchaseOrderLine PurchaseOrderLine { get; set; }
        public Models.Fulfillment Fulfillment { get; set; }
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }

        public FulfillmentEntryPOLine()
        {
            InitializeComponent();
        }

        private void FulfillmentEntryPOLine_Load(object sender, EventArgs e)
        {
            Fulfillment = Fulfillment ?? new Models.Fulfillment();

            lblReportingMark.Text = Fulfillment.Railcar?.ReportingID ?? "[Select A Railcar]";
            if (Fulfillment?.RailcarID == null)
            {
                lblReportingMark.BackColor = Color.Red;
            }

            txtPurchaseOrderLine.Text = PurchaseOrderLine?.DisplayString;
            txtQtyRemaining.Text = PurchaseOrderLine?.UnfulfilledQuantity?.ToString() ?? "0";
            txtQtyLoaded.Text = Fulfillment.Quantity?.ToString();
        }

        private async void lnkChangeRailcar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GenericInputBox inputBox = new GenericInputBox()
            {
                Prompt = "Enter the reporting mark for the Railcar:",
                ResultType = typeof(string),
                AcceptText = "Submit",
                Text = "Railcar Lookup"
            };

            if (inputBox.ShowDialog() == DialogResult.OK)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetByReportingMark/" + inputBox.Result);
                get.AddLocationHeader(CompanyID, LocationID);
                Railcar railcar = await get.GetObject<Railcar>();
                if (railcar == null)
                {
                    this.ShowError("Railcar " + inputBox.Result + " was not found");
                    return;
                }

                CancelEventArgs cancel = new CancelEventArgs();
                RailcarChanged?.Invoke(this, cancel);
                if (cancel.Cancel)
                {
                    this.ShowError("Railcar has already been selected in this Purchase Order");
                    return;
                }

                Fulfillment.RailcarID = railcar.RailcarID;
                Fulfillment.Railcar = railcar;

                lblReportingMark.Text = railcar.ReportingID;
                lblReportingMark.BackColor = BackColor;
            }
        }

        private void txtQtyLoaded_TextChanged(object sender, EventArgs e)
        {
            Fulfillment.Quantity = decimal.TryParse(txtQtyLoaded.Text, out decimal qtyLoaded) ? qtyLoaded : (decimal?)null;
            QuantityLoadedChanged?.Invoke(this, EventArgs.Empty);
        }

        private void lnkSplitToAnotherCar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SplitToAnotherCarClicked?.Invoke(this, EventArgs.Empty);
        }

        public void SetEntryEnabled(bool enabled)
        {
            txtQtyLoaded.Enabled = enabled;
            lnkSplitToAnotherCar.Enabled = enabled;
            lnkChangeRailcar.Enabled = enabled;
            Fulfillment.Quantity = enabled && decimal.TryParse(txtQtyLoaded.Text, out decimal fulfilledQty) ? fulfilledQty : (decimal?)null;
        }
    }
}
