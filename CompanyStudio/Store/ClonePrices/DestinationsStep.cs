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

namespace CompanyStudio.Store.ClonePrices
{
    public partial class DestinationsStep : UserControl, IWizardStep<ClonePricesWizardData>, IUsesWizardLoader
    {
        public DestinationsStep()
        {
            InitializeComponent();
        }

        public string NavigationName => "Destinations";

        public Control Control => this;

        private Action _showLoader;
        public Action ShowLoader { set => _showLoader = value; }
        private Action _hideLoader;
        public Action HideLoader { set => _hideLoader = value; }

        public async Task Commit(ClonePricesWizardData data)
        {
            data.CompanyIDLocationIDDestinations.Clear();

            foreach(DropDownItem<Location> ddi in lstDestinations.SelectedItems.OfType<DropDownItem<Location>>())
            {
                data.CompanyIDLocationIDDestinations.Add((ddi.Object.CompanyID, ddi.Object.LocationID));
            }

            _showLoader();
            await ClonePricesWizardController.PerformDataLoading(data);
            _hideLoader();
        }

        async Task IWizardStep<ClonePricesWizardData>.Load(ClonePricesWizardData data)
        {
            lstDestinations.Items.Clear();

            _showLoader();
            try
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetForEmployee");
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();
                foreach(Company company in companies.OrderBy(c => c.Name))
                {
                    foreach (Location location in company.Locations.OrderBy(l => l.Name))
                    {
                        if (location.LocationID == data.LocationIDOrigin)
                        {
                            continue;
                        }

                        get = new GetData(DataAccess.APIs.CompanyStudio, "LocationEmployee/GetForCurrentUser");
                        get.QueryString.Add("locationID", location.LocationID.ToString());
                        get.AddCompanyHeader(company.CompanyID);
                        LocationEmployee locationEmployee = await get.GetObject<LocationEmployee>();
                        if (locationEmployee == null || (!locationEmployee.ManagePrices && !locationEmployee.ManagePurchaseOrders))
                        {
                            continue;
                        }

                        DropDownItem<Location> ddi = new DropDownItem<Location>(location, $"{location.Name} ({company.Name})");
                        lstDestinations.Items.Add(ddi);
                    }
                }

                lstDestinations.ClearSelected();
                foreach ((long?, long?) selectedLocationID in data.CompanyIDLocationIDDestinations)
                {
                    DropDownItem<Location> ddi = lstDestinations.Items.OfType<DropDownItem<Location>>().FirstOrDefault(x => x.Object.LocationID == selectedLocationID.Item2);
                    if (ddi != null)
                    {
                        lstDestinations.SetSelected(lstDestinations.Items.IndexOf(ddi), true);
                    }
                }
            }
            finally
            {
                _hideLoader();
            }
        }

        async Task<List<string>> IWizardStep<ClonePricesWizardData>.Validate()
        {
            if (lstDestinations.SelectedItems.Count == 0)
            {
                return new List<string>()
                {
                    "At least one destination is required"
                };
            }

            return new List<string>();
        }
    }
}
