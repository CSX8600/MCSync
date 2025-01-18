using System.Collections.Generic;

namespace MesaSuite.Common.NetworkReporting
{
    public class PrintPage
    {
        public long? PrintPageID { get; set; }
        public long? PrintJobID { get; set; }
        public PrintJob PrintJob { get; set; }
        public byte? DisplayOrder { get; set; }

        public List<PrintLine> PrintLines { get; set; }
    }
}
