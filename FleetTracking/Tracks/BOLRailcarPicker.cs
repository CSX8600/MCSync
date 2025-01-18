using FleetTracking.Interop;
using FleetTracking.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetTracking.Tracks
{
    public partial class BOLRailcarPicker : UserControl, IFleetTrackingControl
    {
        public List<long?> TrackIDs { get; set; } = new List<long?>();
        public long? TrainID { get; set; }

        public BOLRailcarPicker()
        {
            InitializeComponent();
        }

        private FleetTrackingApplication _application;
        public FleetTrackingApplication Application { set => _application = value; }

        private async void BOLRailcarPicker_Load(object sender, EventArgs e)
        {
            ParentForm.Text = "Print Bills of Lading";

            loader.BringToFront();
            loader.Visible = true;

            try
            {
                GetData get;
                if (TrackIDs.Any())
                {
                    get = new GetData(DataAccess.APIs.FleetTracking, "BillOfLading/GetByTracks");
                    TrackIDs.ForEach(trackID => get.QueryString.Add("trackIDs", trackID.ToString()));
                }
                else if (TrainID != null)
                {
                    get = new GetData(DataAccess.APIs.FleetTracking, "BillOfLading/GetByTrain");
                    get.QueryString.Add("trainID", TrainID.ToString());
                }
                else
                {
                    return;
                }

                (long?, long?) companyIDGovernmentID = _application.GetCurrentCompanyIDGovernmentID();
                get.QueryString.Add("companyID", companyIDGovernmentID.Item1?.ToString());
                get.QueryString.Add("governmentID", companyIDGovernmentID.Item2?.ToString());
                List<BillOfLading> billsOfLading = await get.GetObject<List<BillOfLading>>() ?? new List<BillOfLading>();

                List<RailLocation> railLocations = new List<RailLocation>();
                if (TrackIDs.Any())
                {
                    foreach (long? trackID in TrackIDs)
                    {
                        get = new GetData(DataAccess.APIs.FleetTracking, "RailLocation/GetByTrack/" + trackID);
                        railLocations.AddRange(await get.GetObject<List<RailLocation>>() ?? new List<RailLocation>());
                    }
                }
                else
                {
                    get = new GetData(DataAccess.APIs.FleetTracking, "RailLocation/GetByTrain/" + TrainID);
                    railLocations.AddRange(await get.GetObject<List<RailLocation>>() ?? new List<RailLocation>());
                }

                foreach(RailLocation railLocation in railLocations.Where(rl => billsOfLading.Any(bol => bol.RailcarID == rl.RailcarID)).OrderBy(rl => rl.Track.Name).ThenBy(rl => rl.Position))
                {
                    BillOfLading billOfLading = billsOfLading.First(bol => bol.RailcarID == railLocation.RailcarID);

                    DataGridViewRow row = dgvAvailable.Rows[dgvAvailable.Rows.Add()];
                    row.Cells[colAReportingMark.Name].Value = railLocation.Railcar.FormattedReportingMark;
                    row.Cells[colACurrentLocation.Name].Value = railLocation.Track.Name ?? railLocation.Train.TrainSymbol.Name + " (" + railLocation.Train.TimeOnDuty?.ToString("MM/dd/yyyy HH:mm") + ")";
                    row.Cells[colADestination.Name].Value = railLocation.Railcar.TrackDestination?.Name;
                    row.Cells[colAStrategicDest.Name].Value = railLocation.Railcar.TrackStrategic?.Name;
                    row.Cells[colAPosition.Name].Value = railLocation.Position;
                    row.Tag = billsOfLading.First(bol => bol.RailcarID == railLocation.RailcarID); 
                    
                    get = new GetData(DataAccess.APIs.FleetTracking, "Railcar/GetImageThumbnail/" + billOfLading.RailcarID);
                    byte[] image = await get.GetObject<byte[]>();
                    if (image == null)
                    {
                        continue;
                    }

                    using (MemoryStream stream = new MemoryStream(image))
                    {
                        Image img = Image.FromStream(stream);
                        imageDisposer.Images.Add(img);
                        row.Cells[colAImage.Name].Value = img;
                    }
                }

                get = new GetData(DataAccess.APIs.SystemManagement, "Printer/GetAll");
                List<Printer> printers = await get.GetObject<List<Printer>>() ?? new List<Printer>();
                printers.ForEach(p => cboPrinter.Items.Add(new DropDownItem<Printer>(p, p.Name)));
            }
            finally { loader.Visible = false; }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            MoveRows(dgvAvailable, dgvSelected, true);
        }

        private void cmdAddAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvAvailable, dgvSelected, false);
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            MoveRows(dgvSelected, dgvAvailable, true);
        }

        private void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvSelected, dgvAvailable, false);
        }

        private void MoveRows(DataGridView source, DataGridView destination, bool selectedOnly)
        {
            List<DataGridViewRow> rowsToMove = selectedOnly ? source.SelectedRows.OfType<DataGridViewRow>().ToList() : source.Rows.OfType<DataGridViewRow>().ToList();

            foreach (DataGridViewRow row in rowsToMove)
            {
                DataGridViewRow selectedRow = row.Clone() as DataGridViewRow;
                for (int index = 0; index < row.Cells.Count; index++)
                {
                    selectedRow.Cells[index].Value = row.Cells[index].Value;
                }
                destination.Rows.Add(selectedRow);
                source.Rows.Remove(row);
            }
            destination.Sort(new RailLocationGridSorter());
        }

        private class RailLocationGridSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                DataGridViewRow rowX = x as DataGridViewRow;
                DataGridViewRow rowY = y as DataGridViewRow;

                DataGridViewColumn locationColumn = rowX.DataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(col => col.Name.Contains("CurrentLocation"));
                DataGridViewColumn positionColumn = rowX.DataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(col => col.Name.Contains("Position"));

                int signum = rowX.Cells[locationColumn.Name].Value.ToString().CompareTo(rowY.Cells[locationColumn.Name].Value.ToString());
                if (signum == 0)
                {
                    return rowX.Cells[positionColumn.Name].Value.ToString().CompareTo(rowY.Cells[positionColumn.Name].Value.ToString());
                }

                return signum;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
            ParentForm.Close();
        }

        private async void cmdPrint_Click(object sender, EventArgs e)
        {
            if (dgvSelected.Rows.Count == 0 || cboPrinter.SelectedItem == null)
            {
                this.ShowError("Please select at least one Bill of Lading and a printer to print to.");
                return;
            }

            long? printerID = (cboPrinter.SelectedItem as DropDownItem<Printer>).Object.PrinterID;
            foreach(DataGridViewRow row in dgvSelected.Rows)
            {
                BillOfLading bol = row.Tag as BillOfLading;
                await bol.NetworkPrint(printerID, $"BOL {bol.RailcarReportingID}");
            }

            ParentForm.DialogResult = DialogResult.OK;
            ParentForm.Close();
        }
    }
}
