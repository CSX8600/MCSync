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
    public partial class frmAutomaticPaymentConfigurationMassUpdate : Form
    {
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public ThemeBase Theme { get; set; }

        public decimal? MaxAmount { get; private set; }
        internal AutomaticInvoicePaymentConfiguration.Schedules Schedule { get; private set; }
        public long? AccountID { get; private set; }

        public frmAutomaticPaymentConfigurationMassUpdate()
        {
            InitializeComponent();
        }

        private async void frmAutomaticPaymentConfigurationMassUpdate_Load(object sender, EventArgs e)
        {
            studioFormExtender.ApplyStyle(this, Theme);

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Account/GetAllForUser");
            get.AddLocationHeader(CompanyID, LocationID);
            List<Account> accounts = await get.GetObject<List<Account>>() ?? new List<Account>();

            foreach (Account account in accounts)
            {
                cboPaymentAccount.Items.Add(new DropDownItem<Account>(account, $"{account.Description} ({account.AccountNumber})"));
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaxAmount.Text) && decimal.TryParse(txtMaxAmount.Text, out decimal maxAmount))
            {
                MaxAmount = maxAmount;
            }

            Schedule = rdoImmediately.Checked ? AutomaticInvoicePaymentConfiguration.Schedules.Immediately : AutomaticInvoicePaymentConfiguration.Schedules.OnDueDate;

            if (cboPaymentAccount.SelectedItem != null)
            {
                AccountID = (cboPaymentAccount.SelectedItem as DropDownItem<Account>)?.Object.AccountID;
            }

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
