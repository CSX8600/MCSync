using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Utility;

namespace GovernmentPortal.Invoicing
{
    public partial class frmAutomaticPaymentsSelectPayees : Form
    {
        public IReadOnlyCollection<Location> Locations { get; private set; }
        public IReadOnlyCollection<Government> Governments { get; private set; }

        public long GovernmentID { get; set; }
        public frmAutomaticPaymentsSelectPayees()
        {
            InitializeComponent();
        }

        private async void frmAutomaticPaymentsSelectPayees_Load(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;

            try
            {
                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/GetAll");
                get.AddGovHeader(GovernmentID);
                List<AutomaticInvoicePaymentConfiguration> existingConfigurations = await get.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>();

                get.Resource = "Government/GetAll";
                List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();

                get.Resource = "Company/GetAll";
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();
                Dictionary<Company, List<Location>> locationsByCompany = companies.ToDictionary(c => c, c => c.Locations);

                governments = governments.Where(g => !existingConfigurations.Any(c => c.GovernmentIDPayee == g.GovernmentID)).ToList();
                foreach (KeyValuePair<Company, List<Location>> locationListByCompany in locationsByCompany.ToList())
                {
                    List<Location> locations = locationListByCompany.Value.Where(l => !existingConfigurations.Any(c => c.LocationIDPayee == l.LocationID)).ToList();
                    locations.ForEach(l => l.Company = locationListByCompany.Key);
                    locationsByCompany[locationListByCompany.Key] = locations;
                }

                foreach (Government government in governments)
                {
                    DropDownItem<Government> ddi = new DropDownItem<Government>(government, government.Name + " (Government)");
                    lstAvailable.Items.Add(ddi);
                }

                foreach (KeyValuePair<Company, List<Location>> kvp in locationsByCompany)
                {
                    foreach (Location location in kvp.Value)
                    {
                        DropDownItem<Location> ddi = new DropDownItem<Location>(location, $"{kvp.Key.Name} ({location.Name})");
                        lstAvailable.Items.Add(ddi);
                    }
                }
            }
            finally { loader.Visible = false; }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            TransferItems(lstAvailable, lstSelected, true);
        }

        private void cmdAddAll_Click(object sender, EventArgs e)
        {
            TransferItems(lstAvailable, lstSelected, false);
        }

        private void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            TransferItems(lstSelected, lstAvailable, false);
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            TransferItems(lstSelected, lstAvailable, true);
        }

        private void TransferItems(ListBox from, ListBox to, bool selectedOnly)
        {
            List<object> objectsToTransfer = selectedOnly ? from.SelectedItems.OfType<object>().ToList() : from.Items.OfType<object>().ToList();

            foreach(object item in objectsToTransfer)
            {
                to.Items.Add(item);
                from.Items.Remove(item);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            List<Location> locations = new List<Location>();
            List<Government> governments = new List<Government>();

            foreach(object item in lstSelected.Items)
            {
                DropDownItem<Location> location = item as DropDownItem<Location>;
                DropDownItem<Government> government = item as DropDownItem<Government>;

                if (location != null)
                {
                    locations.Add(location.Object);
                }

                if (government != null)
                {
                    governments.Add(government.Object);
                }
            }

            Locations = locations;
            Governments = governments;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
