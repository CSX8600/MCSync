using FleetTracking.Reports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetTracking.Models
{
    public class BillOfLading
    {
        public long? BillOfLadingID { get; set; }
        public long? CompanyIDShipper { get; set; }
        public Company CompanyShipper { get; set; }
        public long? GovernmentIDShipper { get; set; }
        public Government GovernmentShipper { get; set; }
        public long? CompanyIDConsignee { get; set; }
        public Company CompanyConsignee { get; set; }
        public long? GovernmentIDConsignee { get; set; }
        public Government GovernmentConsignee { get; set; }
        public long? CompanyIDCarrier { get; set; }
        public Company CompanyCarrier { get; set; }
        public long? GovernmentIDCarrier { get; set; }
        public Government GovernmentCarrier { get; set; }
        public long? RailcarID { get; set; }
        public Railcar Railcar { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        [Flags]
        public enum Types
        {
            FirstMile,
            Interchange,
            LastMile
        }
        public Types Type { get; set; }

        public List<BillOfLadingItem> BillOfLadingItems { get; set; } = new List<BillOfLadingItem>();

        public string From => GovernmentIDShipper == null ? CompanyShipper.Name : GovernmentShipper.Name + " (Government)";
        public string To => GovernmentIDConsignee == null ? CompanyConsignee.Name : GovernmentConsignee.Name + " (Government)";
        public string Via => GovernmentIDCarrier == null ? CompanyCarrier.Name : GovernmentCarrier.Name + " (Government)";
        public string RailcarReportingID => Railcar?.FormattedReportingMark;
        public string TypeString
        {
            get
            {
                List<string> types = new List<string>();
                if (Type.HasFlag(Types.FirstMile))
                {
                    types.Add("First Mile");
                }
                if (Type.HasFlag(Types.Interchange))
                {
                    types.Add("Interchange");
                }
                if (Type.HasFlag(Types.LastMile))
                {
                    types.Add("Last Mile");
                }

                return string.Join(", ", types);
            }
        }

        public async Task NetworkPrint(long? printerID, string documentName)
        {
            NetworkReportBuilder networkReportBuilder = new NetworkReportBuilder();

            networkReportBuilder.Groupings = new List<NetworkReportBuilder.Grouping>()
            {
                "BOL #: " + BillOfLadingID.ToString(),
                new PrintLine() { Text = "§lBILL OF LADING", Alignment = PrintLine.Alignments.Center },
                $"§l{RailcarReportingID}",
                "",
                new[] { $"§lFrom: §r{From}", $"§lTo: §r{To}", $"§lVia: §r{Via}" },
                $"§Issued: {IssuedDate?.ToString("MM/dd/yyyy")}",
                new PrintLine() { Text = "§lCOMMODITIES", Alignment = PrintLine.Alignments.Center },
            };

            decimal quantityTotal = 0;
            decimal valueTotal = 0;
            foreach (BillOfLadingItem billOfLadingItem in BillOfLadingItems ?? new List<BillOfLadingItem>())
            {
                quantityTotal += billOfLadingItem.Quantity ?? 0;
                valueTotal += billOfLadingItem.Value ?? 0;

                networkReportBuilder.Groupings.Add(new[]
                {
                    $"{billOfLadingItem.Quantity}x {billOfLadingItem.ItemString}",
                    $"Value: {billOfLadingItem.Value?.ToString("N2")}",
                    ""
                });
            }

            if (quantityTotal > 0 || valueTotal > 0)
            {
                networkReportBuilder.Groupings.Add(new NetworkReportBuilder.Grouping()
                {
                    PrintLines =
                    {
                        new PrintLine() { Text = "§lTOTALS", Alignment = PrintLine.Alignments.Center },
                        $"Quantity: {quantityTotal}",
                        $"Value: {valueTotal.ToString("N2")}"
                    }
                });
            }

            await networkReportBuilder.SaveNetworkReport(printerID, documentName);
        }
    }
}
