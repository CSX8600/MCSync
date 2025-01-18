using FleetTracking.Interop;
using FleetTracking.Properties;
using FleetTracking.Reports;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetTracking
{
    public partial class PrintableReport : UserControl, IFleetTrackingControl
    {
        private FleetTrackingApplication _application;
        public FleetTrackingApplication Application { set => _application = value; }

        public IReportContext ReportContext { get; set; }

        public PrintableReport()
        {
            InitializeComponent();
        }

        private async void PrintableReport_Load(object sender, EventArgs e)
        {
            ParentForm.Text = ReportContext.WindowTitle;
            reportViewer.BackColor = Color.White;
            reportViewer.Reset();
            reportViewer.LocalReport.ReportEmbeddedResource = ReportContext.ReportPath;
            reportViewer.LocalReport.SubreportProcessing += SubreportProcessingAsync;
            Dictionary<string, object> dataSources = await ReportContext.GetReportDataSources();
            foreach(KeyValuePair<string, object> dataSource in dataSources)
            {
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSource.Key, dataSource.Value));
            }
            if (ReportContext is IHasParameters hasParameters)
            {
                foreach(KeyValuePair<string, object> parameter in hasParameters.GetReportParameters())
                {
                    reportViewer.LocalReport.SetParameters(new ReportParameter(parameter.Key, parameter.Value.ToString()));
                }
            }
            reportViewer.LocalReport.Refresh();
            reportViewer.RefreshReport();

            if (ReportContext is INetworkPrintable)
            {
                cmdNetworkPrint.Visible = true;
            }

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
        }

        private async void cmdNetworkPrint_Click(object sender, EventArgs e)
        {
            PrintableReportSelectPrinter selectPrinter = new PrintableReportSelectPrinter()
            {
                Application = _application
            };

            Form dialog = _application.OpenForm(selectPrinter, FleetTrackingApplication.OpenFormOptions.Dialog | FleetTrackingApplication.OpenFormOptions.ResizeToControl);
            if (dialog.DialogResult != DialogResult.OK)
            {
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                await ((INetworkPrintable)ReportContext).NetworkPrint(selectPrinter.PrinterID, selectPrinter.DocumentName);
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void SubreportProcessingAsync(object sender, SubreportProcessingEventArgs e)
        {
            Dictionary<string, object> dataSources = ReportContext.GetReportDataSources().Result;
            foreach (KeyValuePair<string, object> dataSource in dataSources.Where(kvp => kvp.Key.StartsWith(e.ReportPath)))
            {
                e.DataSources.Add(new ReportDataSource(dataSource.Key.Replace(e.ReportPath + ".", ""), dataSource.Value));
            }
        }
    }
}
