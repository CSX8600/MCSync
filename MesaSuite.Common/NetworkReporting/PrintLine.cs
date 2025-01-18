using System.Runtime.CompilerServices;

namespace MesaSuite.Common.NetworkReporting
{
    public class PrintLine
    {
        public long? PrintLineID { get; set; }
        public long? PrintPageID { get; set; }
        public PrintPage PrintPage { get; set; }
        public byte? DisplayOrder { get; set; }
        public enum Alignments
        {
            Unspecified = 0,
            Left,
            Center
        }

        public Alignments Alignment { get; set; }
        public string Text { get; set; }

        public static PrintLine Create(long? printPageID, string text, byte counter, Alignments alignment = Alignments.Left)
        {
            return new PrintLine()
            {
                PrintPageID = printPageID,
                DisplayOrder = counter,
                Alignment = alignment,
                Text = text
            };
        }

        public static implicit operator PrintLine(string text) => new PrintLine() { Text = text };
    }
}
