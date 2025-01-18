using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernmentPortal.Models
{
    public class Account
    {
        public long? AccountID { get; set; }
        public long? GovernmentID { get; set; }
        public long? CategoryID { get; set; }
        public string Description { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
