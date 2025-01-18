using System.Collections.Generic;

namespace MesaSuite.Common.NetworkReporting
{
    public class PrintJob
    {
        public long? PrintJobID { get; set; }
        public long? PrinterID { get; set; }
        public Printer Printer { get; set; }
        public string DocumentName { get; set; }
        public bool Finalized { get; set; }

        public List<PrintPage> PrintPages { get; set; }
    }
}
