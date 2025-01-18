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
    public partial class frmAutomaticPaymentsCloneToPicker : Form
    {
        public long GovernmentID { get; set; }
        public long CurrentAutomaticInvoicePaymentConfigurationID { get; set; }

        public IReadOnlyCollection<AutomaticInvoicePaymentConfiguration> SelectedAutomaticInvoicePaymentConfigurations { get; private set; }

        public frmAutomaticPaymentsCloneToPicker()
        {
            InitializeComponent();
        }

        private async void frmAutomaticPaymentsCloneToPicker_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/GetAll");
            get.AddGovHeader(GovernmentID);
            List<AutomaticInvoicePaymentConfiguration> existingConfigs = await get.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>();
            foreach(AutomaticInvoicePaymentConfiguration existingConfig in existingConfigs.Where(c => c.AutomaticInvoicePaymentConfigurationID != CurrentAutomaticInvoicePaymentConfigurationID))
            {
                DropDownItem<AutomaticInvoicePaymentConfiguration> ddi = new DropDownItem<AutomaticInvoicePaymentConfiguration>(existingConfig, existingConfig.DisplayName);
                lstConfigs.Items.Add(ddi);
            }
        }

        private void cmdClone_Click(object sender, EventArgs e)
        {
            List<AutomaticInvoicePaymentConfiguration> selectedConfigs = new List<AutomaticInvoicePaymentConfiguration>();
            foreach(DropDownItem<AutomaticInvoicePaymentConfiguration> selectedConfig in lstConfigs.SelectedItems.OfType<DropDownItem<AutomaticInvoicePaymentConfiguration>>())
            {
                selectedConfigs.Add(selectedConfig.Object);
            }

            SelectedAutomaticInvoicePaymentConfigurations = selectedConfigs;

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
