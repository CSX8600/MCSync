using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesaSuite.Common.NetworkReporting
{
    public partial class frmPrinterSelector : Form
    {
        public long? PrinterID { get; set; }
        public string DocumentName { get; set; }

        public frmPrinterSelector()
        {
            InitializeComponent();
        }

        private async void frmPrinterSelector_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.SystemManagement, "Printer/GetAll");
            List<Printer> printers = await get.GetObject<List<Printer>>() ?? new List<Printer>();
            foreach (Printer printer in printers.OrderBy(p => p.Name))
            {
                DropDownItem<Printer> printerItem = new DropDownItem<Printer>(printer, printer.Name);
                cboPrinters.Items.Add(printerItem);

                if (PrinterID == printer.PrinterID)
                {
                    cboPrinters.SelectedItem = printerItem;
                }
            }

            txtDocumentName.Text = DocumentName;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ("Printer", cboPrinters),
                ("Document Name", txtDocumentName)
            }))
            {
                return;
            }

            PrinterID = (cboPrinters.SelectedItem as DropDownItem<Printer>).Object.PrinterID;
            DocumentName = txtDocumentName.Text;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
