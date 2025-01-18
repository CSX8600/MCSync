using System;
using System.Collections.Generic;
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
    internal class AutomaticPaymentsContext : ExplorerContext<AutomaticInvoicePaymentConfiguration>
    {
        private long _governmentID;
        private AutomaticPaymentControl _lastCreatedControl = null;
        public AutomaticPaymentsContext(long governmentID)
        {
            _governmentID = governmentID;
        }

        internal override Icon ExplorerIcon => Properties.Resources.icn_money; // TODO: Make proper icon file

        internal override IExplorerControl<AutomaticInvoicePaymentConfiguration> GetControlForModel(AutomaticInvoicePaymentConfiguration model)
        {
            AutomaticPaymentControl control = new AutomaticPaymentControl()
            {
                GovernmentID = _governmentID,
                Model = model
            };
            _lastCreatedControl = control;
            return control;
        }

        internal override async Task<List<DropDownItem<AutomaticInvoicePaymentConfiguration>>> GetInitialListItems()
        {
            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/GetAll");
            get.AddGovHeader(_governmentID);
            List<AutomaticInvoicePaymentConfiguration> configurations = await get.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>();

            return configurations.OrderBy(c => c.DisplayName).Select(c => new DropDownItem<AutomaticInvoicePaymentConfiguration>(c, GetDisplayName(c))).ToList();
        }

        internal static string GetDisplayName(AutomaticInvoicePaymentConfiguration configuration)
        {
            return $"{configuration.DisplayName} ({configuration.PaidAmount?.ToString("N2")}/{configuration.MaxAmount?.ToString("N2")})";
        }

        internal override string ObjectDisplayName => "Automatic Pay";

        internal override IEnumerable<ToolStripMenuItem> GetExtraToolItems()
        {
            yield return new ToolStripMenuItem("Clone Selected To...", Properties.Resources.paste_plain, CloneSelectedTo_Clicked, "tsmiClone");
        }

        internal void CloneSelectedTo_Clicked(object sender, EventArgs e) 
        {
            if (_lastCreatedControl == null || !_lastCreatedControl.IsHandleCreated)
            {
                return;
            }

            _lastCreatedControl.CloneThisConfiguration();
        }
    }
}
