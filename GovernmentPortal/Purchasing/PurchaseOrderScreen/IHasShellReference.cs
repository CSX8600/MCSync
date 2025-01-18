using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen
{
    internal interface IHasShellReference
    {
        frmPurchaseOrderShell Shell { get; set; }
    }
}
