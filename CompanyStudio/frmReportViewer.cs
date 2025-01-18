using MesaSuite.Common.NetworkReporting;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio
{
    public partial class frmReportViewer : BaseCompanyStudioContent
    {
        private Dictionary<string, object> dataSets = new Dictionary<string, object>();
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public string ReportName { get; set; }
        public NetworkReportBuilder NetworkReportBuilder { get; set; }

        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            cmdNetworkPrint.Visible = NetworkReportBuilder != null;

            reportViewer.LocalReport.ReportEmbeddedResource = ReportName;
            KeyValuePair<string, object> dataSetKVP = dataSets.FirstOrDefault(kvp => !kvp.Key.Contains('.'));
            if (!dataSetKVP.Equals(default(KeyValuePair<string, object>)))
            {
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSetKVP.Key, dataSetKVP.Value));
            }
            foreach(KeyValuePair<string, string> parameter in parameters)
            {
                reportViewer.LocalReport.SetParameters(new ReportParameter(parameter.Key, parameter.Value));
            }
            reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            this.reportViewer.RefreshReport();
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string reportPrefix = e.ReportPath.Substring(0, e.ReportPath.LastIndexOf("."));
            reportPrefix = reportPrefix.Substring(reportPrefix.LastIndexOf(".") + 1);

            foreach(KeyValuePair<string, object> kvp in dataSets.Where(k => k.Key.StartsWith(reportPrefix + ".")))
            {
                string dataSetName = kvp.Key.Substring(kvp.Key.LastIndexOf('.') + 1);
                e.DataSources.Add(new ReportDataSource(dataSetName, kvp.Value));
            }
        }

        public void AddDataSet(string key, object value)
        {
            dataSets.Add(key, value);
        }

        public void AddParameter(string key, string value)
        {
            parameters.Add(key, value);
        }

        private async void cmdNetworkPrint_Click(object sender, EventArgs e)
        {
            frmPrinterSelector printerSelector = new frmPrinterSelector()
            {
                PrinterID = NetworkReportBuilder.InitialPrinterID,
                DocumentName = NetworkReportBuilder.InitialDocumentName
            };

            if (printerSelector.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            await NetworkReportBuilder.SaveNetworkReport(printerSelector.PrinterID, printerSelector.DocumentName);
        }
    }
}
