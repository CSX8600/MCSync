using CompanyStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Purchasing.Fulfillment
{
    public class FulfillmentWizardData
    {
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public bool ReleaseCars { get; set; } = true;
        public List<Railcar> SelectedRailcars { get; set; } = new List<Railcar>();
        public List<Models.Fulfillment> Fulfillments { get; set; } = new List<Models.Fulfillment>();
    }
}
