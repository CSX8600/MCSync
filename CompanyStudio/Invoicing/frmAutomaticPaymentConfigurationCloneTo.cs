using CompanyStudio.Extensions;
using CompanyStudio.Models;
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
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Invoicing
{
    public partial class frmAutomaticPaymentConfigurationCloneTo : Form
    {
        private object selectAllObject = new object();

        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public ThemeBase Theme { get; set; }

        public IReadOnlyCollection<long?> SelectedLocationIDs { get; private set; }
        public frmAutomaticPaymentConfigurationCloneTo()
        {
            InitializeComponent();
        }

        private async void frmAutomaticPaymentConfigurationCloneTo_Load(object sender, EventArgs e)
        {
            studioFormExtender.ApplyStyle(this, Theme);

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Location/GetAll");
            get.AddCompanyHeader(CompanyID);
            List<Location> locations = await get.GetObject<List<Location>>() ?? new List<Location>();

            chkLocations.Items.Add(new DropDownItem<object>(selectAllObject, "(Select All)"));

            foreach(Location location in locations.Where(l => l.LocationID != LocationID).OrderBy(l => l.Name))
            {
                chkLocations.Items.Add(new DropDownItem<Location>(location, location.Name));
            }
        }

        private bool _suppressItemCheck;
        private void chkLocations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_suppressItemCheck) return;

            _suppressItemCheck = true;
            try
            {
                if (chkLocations.Items[e.Index] is DropDownItem<object> objectItem && objectItem.Object == selectAllObject)
                {
                    for (int i = 1; i < chkLocations.Items.Count; i++)
                    {
                        chkLocations.SetItemChecked(i, e.NewValue == CheckState.Checked);
                    }
                }
                else if (chkLocations.Items[e.Index] is DropDownItem<Location> locationItem)
                {
                    bool allChecked = true;
                    for(int i = 1; i < chkLocations.Items.Count; i++)
                    {
                        if (i == e.Index)
                        {
                            allChecked &= e.NewValue == CheckState.Checked;
                        }
                        else
                        {
                            allChecked &= chkLocations.GetItemChecked(i);
                        }

                        if (!allChecked) break;
                    }

                    chkLocations.SetItemChecked(0, allChecked);
                }
            }
            finally { _suppressItemCheck = false; }
        }

        private void cmdClone_Click(object sender, EventArgs e)
        {
            List<long?> selectedLocationIDs = new List<long?>();
            for(int i = 1; i < chkLocations.Items.Count; i++)
            {
                if (chkLocations.GetItemChecked(i) && chkLocations.Items[i] is DropDownItem<Location> locationItem)
                {
                    selectedLocationIDs.Add(locationItem.Object.LocationID);
                }
            }

            SelectedLocationIDs = selectedLocationIDs;

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
