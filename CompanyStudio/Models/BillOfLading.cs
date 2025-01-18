using CompanyStudio.Extensions;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.NetworkReporting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Models
{
    public class BillOfLading
    {
        public long? BillOfLadingID { get; set; }
        public long? PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
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
        public string RailcarReportingID => Railcar?.ReportingID;
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

        public static async Task Accept(long? billOfLadingID, long? companyID, long? locationID)
        {
            try
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "BillOfLading/AcceptBOL", new { billOfLadingID });
                post.AddLocationHeader(companyID, locationID);
                await post.ExecuteNoResult();
            }
            catch { }
        }

        public static async Task AcceptMultiple(long? companyID, long? locationID)
        {
            GenericInputBox inputBox = new GenericInputBox()
            {
                Prompt = "Enter Bill of Lading Numbers, separated by commas:",
                Text = "Accept Bills of Lading",
                AcceptText = "Accept",
                ResultType = typeof(string)
            };

            if (inputBox.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] billOfLadingNumbers = inputBox.Result.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string billOfLadingNumber in billOfLadingNumbers)
            {
                billOfLadingNumber.Trim();

                if (!string.IsNullOrEmpty(billOfLadingNumber) && long.TryParse(billOfLadingNumber, out long billOfLadingID))
                {
                    await Accept(billOfLadingID, companyID, locationID);
                }
            }
        }

        public void DisplayReport(frmStudio studio, Company company)
        {
            NetworkReportBuilder networkReportBuilder = new NetworkReportBuilder()
            {
                InitialDocumentName = "BOL " + RailcarReportingID
            };

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
            foreach(BillOfLadingItem billOfLadingItem in BillOfLadingItems ?? new List<BillOfLadingItem>())
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

            frmReportViewer reportViewer = new frmReportViewer();
            studio.DecorateStudioContent(reportViewer);
            reportViewer.Company = company;
            reportViewer.ReportName = "CompanyStudio.Purchasing.BillOfLadingReport.BillOfLading.rdlc";
            reportViewer.AddDataSet("BillOfLadingDataSet", new List<BillOfLading>() { this });
            reportViewer.AddDataSet("BillOfLadingItems.BillOfLadingItemDataSet", BillOfLadingItems ?? new List<BillOfLadingItem>());
            reportViewer.NetworkReportBuilder = networkReportBuilder;
            reportViewer.Show(studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
    }
}
