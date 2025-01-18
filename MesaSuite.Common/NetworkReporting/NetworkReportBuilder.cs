using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MesaSuite.Common.NetworkReporting
{
    public class NetworkReportBuilder
    {
        public string InitialDocumentName { get; set; }
        public long? InitialPrinterID { get; set; }

        public class Grouping
        {
            public List<PrintLine> PrintLines { get; set; } = new List<PrintLine>();

            public static implicit operator Grouping(PrintLine printLine) => new Grouping()
            {
                PrintLines = new List<PrintLine>() { printLine }
            };

            public static implicit operator Grouping(string printLine) => new Grouping()
            {
                PrintLines = new List<PrintLine>() { printLine }
            };

            public static implicit operator Grouping(string[] printLines) => new Grouping()
            {
                PrintLines = printLines.Select(l => (PrintLine)l).ToList()
            };
        }

        public List<Grouping> Groupings { get; set; } = new List<Grouping>();

        public async Task SaveNetworkReport(long? printerID, string documentName)
        {
            PostData post = new PostData(DataAccess.APIs.SystemManagement, "PrintJob/Post");

            PrintJob printJob = new PrintJob()
            {
                PrinterID = printerID,
                DocumentName = documentName
            };
            post.ObjectToPost = printJob;
            printJob = await post.Execute<PrintJob>();
            if (!post.RequestSuccessful)
            {
                return;
            }

            try
            {
                byte pageCounter = 1;
                PrintPage currentPage = new PrintPage()
                {
                    PrintJobID = printJob.PrintJobID,
                    DisplayOrder = pageCounter
                };

                post.Resource = "PrintPage/Post";
                post.ObjectToPost = currentPage;
                currentPage = await post.Execute<PrintPage>();
                if (!post.RequestSuccessful)
                {
                    throw new OperationCanceledException();
                }

                byte lineCounter = 0;
                foreach(Grouping grouping in Groupings)
                {
                    if (grouping.PrintLines.Count + lineCounter > 20)
                    {
                        currentPage = new PrintPage()
                        {
                            PrintJobID = printJob.PrintJobID,
                            DisplayOrder = ++pageCounter
                        };

                        post.Resource = "PrintPage/Post";
                        post.ObjectToPost = currentPage;
                        currentPage = await post.Execute<PrintPage>();
                        if (!post.RequestSuccessful)
                        {
                            throw new OperationCanceledException();
                        }

                        lineCounter = 0;
                    }

                    post.Resource = "PrintLine/Post";
                    foreach(PrintLine line in grouping.PrintLines)
                    {
                        line.PrintPageID = currentPage.PrintPageID;
                        line.DisplayOrder = ++lineCounter;
                        post.ObjectToPost = line;
                        await post.ExecuteNoResult();

                        if (!post.RequestSuccessful)
                        {
                            throw new OperationCanceledException();
                        }
                    }
                }

                printJob.Finalized = true;

                PutData put = new PutData(DataAccess.APIs.SystemManagement, "PrintJob/Put", printJob);
                await put.ExecuteNoResult();
            }
            catch
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.SystemManagement, $"PrintJob/Delete/{printJob.PrintJobID}");
                await delete.Execute();
            }
        }
    }
}
