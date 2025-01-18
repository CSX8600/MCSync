using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Purchasing.ApprovalViewer;
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

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class frmPurchaseOrder : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public frmPurchaseOrder()
        {
            InitializeComponent();
        }

        public long? PurchaseOrderID { get; set; }
        private PurchaseOrder.Statuses _currentStatus = PurchaseOrder.Statuses.Draft;
        public Location LocationModel { get; set; }
        private List<long?> _deletedLineIDs = new List<long?>();
        private List<PurchaseOrderLine> _linesAtLoad = new List<PurchaseOrderLine>();
        private long? _purchaseOrderIDClonedFromAtLoad = null;

        private bool _clonedWarning = false;
        private bool _templateWarning = false;
        private bool _clonesExistWarning = false;

        public event EventHandler OnSave;

        private async void frmPurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripMain.Enabled = false;
                loader.BringToFront();
                loader.Visible = true;

                Studio.dockPanel.Contents.OfType<frmPurchaseOrderExplorer>().Where(poe => poe.LocationModel.LocationID == LocationModel.LocationID).FirstOrDefault()?.RegisterPurchaseOrderForm(this, () => PurchaseOrderID);

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetAll");
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();
                foreach(Company company in companies.OrderBy(c => c.Name))
                {
                    foreach(Location location in company.Locations.OrderBy(l => l.Name))
                    {
                        cboLocation.Items.Add(new DropDownItem<Location>(location, $"{company.Name} ({location.Name})"));
                    }
                }

                get = new GetData(DataAccess.APIs.CompanyStudio, "Government/GetAll");
                get.AddCompanyHeader(Company.CompanyID);
                List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();
                foreach(Government government in governments.OrderBy(g => g.Name))
                {
                    cboGovernment.Items.Add(new DropDownItem<Government>(government, government.Name));
                }

                if (PurchaseOrderID != null)
                {
                    Text += $" - {PurchaseOrderID}";

                    get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                    get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
                    rdoLocation.Checked = purchaseOrder?.LocationIDDestination != null;
                    cboLocation.SelectedItem = cboLocation.Items.OfType<DropDownItem<Location>>().FirstOrDefault(item => item.Object.LocationID == purchaseOrder?.LocationIDDestination);
                    rdoGovernment.Checked = purchaseOrder?.GovernmentIDDestination != null;
                    cboGovernment.SelectedItem = cboGovernment.Items.OfType<DropDownItem<Government>>().FirstOrDefault(item => item.Object.GovernmentID == purchaseOrder?.GovernmentIDDestination);
                    txtOrderDate.Text = purchaseOrder?.PurchaseOrderDate?.ToString("MM/dd/yyyy");
                    txtDescription.Text = purchaseOrder?.Description;
                    _currentStatus = purchaseOrder?.Status ?? PurchaseOrder.Statuses.Draft;
                    _purchaseOrderIDClonedFromAtLoad = purchaseOrder.PurchaseOrderIDClonedFrom;
                    lblStatus.Text = _currentStatus.ToString().ToDisplayName();
                    SetupFormWarnings(purchaseOrder);

                    foreach (PurchaseOrderLineControl polc in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().ToList())
                    {
                        pnlPurchaseOrderLines.Controls.Remove(polc);
                    }

                    if (purchaseOrder.PurchaseOrderLines != null)
                    {
                        foreach (PurchaseOrderLine purchaseOrderLine in purchaseOrder.PurchaseOrderLines)
                        {
                            AddPurchaseOrderLineControl(purchaseOrderLine, purchaseOrder.Status == PurchaseOrder.Statuses.InProgress || purchaseOrder.Status == PurchaseOrder.Statuses.Completed);
                        }
                    }

                    lblSaveToAddPlans.Visible = false;
                    toolAddPlan.Visible = true;
                    toolDeletePlan.Visible = true;
                    cloneSeparator.Visible = true;
                    toolClonePlan.Visible = true;
                    toolSaveTemplate.Visible = true;
                    toolDelete.Enabled = true;

                    switch (_currentStatus)
                    {
                        case PurchaseOrder.Statuses.Draft:
                            cmdSubmit.Visible = _currentStatus == PurchaseOrder.Statuses.Draft;
                            break;
                        case PurchaseOrder.Statuses.InProgress:
                        case PurchaseOrder.Statuses.Completed:
                            cboGovernment.Enabled = false;
                            cboLocation.Enabled = false;
                            txtDescription.Enabled = false;
                            cmdSave.Enabled = false;
                            toolLoadTemplate.Enabled = false;
                            toolAddNewLine.Enabled = false;
                            break;
                    }

                    _linesAtLoad = purchaseOrder?.PurchaseOrderLines ?? new List<PurchaseOrderLine>();

                    await UpdateTotals();

                    await RefreshFulfillmentPlans();
                }
                else
                {
                    _currentStatus = PurchaseOrder.Statuses.Draft;
                    lblStatus.Text = PurchaseOrder.Statuses.Draft.ToString().ToDisplayName();
                    cmdSubmit.Visible = true;
                }
            }
            finally
            {
                toolStripMain.Enabled = true;
                loader.Visible = false;
            }
        }

        private void SetupFormWarnings(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder != null)
            {
                _clonedWarning = purchaseOrder.PurchaseOrderIDClonedFrom != null;
                _templateWarning = purchaseOrder.PurchaseOrderTemplates?.Any() ?? false;
                _clonesExistWarning = purchaseOrder.PurchaseOrderClones?.Any() ?? false;
            }

            int warningCount = 0;
            warningCount += _clonedWarning ? 1 : 0;
            warningCount += _templateWarning ? 1 : 0;
            warningCount += _clonesExistWarning ? 1 : 0;
            toolWarnings.Text = $"{warningCount} Warning(s)";
            toolWarnings.Visible = warningCount > 0;
        }

        private void toolAddNewLine_Click(object sender, EventArgs e)
        {
            AddPurchaseOrderLineControl();
        }

        private void AddPurchaseOrderLineControl(PurchaseOrderLine purchaseOrderLine = null, bool disabled = false)
        {
            lblLinePlaceholder.Visible = false;

            int lowestHeight = 0;

            if (pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().Any())
            {
                lowestHeight = pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().Max(ctrl => ctrl.Bottom);
            }

            PurchaseOrderLineControl purchaseOrderLineControl = new PurchaseOrderLineControl();
            purchaseOrderLineControl.CompanyID = Company.CompanyID;
            purchaseOrderLineControl.LocationIDOrigin = LocationModel.LocationID;
            if (rdoLocation.Checked)
            {
                purchaseOrderLineControl.CompanyIDDestination = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.CompanyID;
                purchaseOrderLineControl.LocationIDDestination = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.LocationID;
            }
            else if (rdoGovernment.Checked)
            {
                purchaseOrderLineControl.GovernmentIDDestination = (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID;
            }
            purchaseOrderLineControl.PurchaseOrderLine = purchaseOrderLine;
            purchaseOrderLineControl.PurchaseOrderID = PurchaseOrderID;
            purchaseOrderLineControl.Location = new Point(3, lowestHeight + 3);
            purchaseOrderLineControl.Width = pnlPurchaseOrderLines.Width - 6;
            purchaseOrderLineControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            purchaseOrderLineControl.BorderStyle = BorderStyle.FixedSingle;
            purchaseOrderLineControl.Enabled = !disabled;
            purchaseOrderLineControl.DeleteClicked += (_, __) =>
            {
                if (purchaseOrderLineControl.PurchaseOrderLine != null)
                {
                    _deletedLineIDs.Add(purchaseOrderLineControl.PurchaseOrderLine.PurchaseOrderLineID);
                }

                int currentBottom = purchaseOrderLineControl.Top - 3;
                pnlPurchaseOrderLines.Controls.Remove(purchaseOrderLineControl);
                foreach (PurchaseOrderLineControl ctrl in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().Where(ctrl => ctrl.Top > currentBottom).OrderBy(ctrl => ctrl.Top))
                {
                    ctrl.Top = currentBottom + 3;
                    currentBottom = ctrl.Bottom;
                }

                if (!pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().Any())
                {
                    lblLinePlaceholder.Visible = true;
                }

                UpdateTotals();
            };
            purchaseOrderLineControl.TotalChanged += (_, __) => { UpdateTotals(); };
            purchaseOrderLineControl.QuotationRequestClicked += (_, qr) =>
            {
                Quotes.frmQuoteRequest requestForm = new Quotes.frmQuoteRequest()
                {
                    QuotationRequestID = qr.QuotationRequestID
                };
                Studio.DecorateStudioContent(requestForm);
                requestForm.Company = Company;
                requestForm.LocationModel = LocationModel;
                requestForm.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            };
            pnlPurchaseOrderLines.Controls.Add(purchaseOrderLineControl);
        }

        private async Task UpdateTotals()
        {
            List<SalesTax> salesTaxes = new List<SalesTax>();
            if (rdoLocation.Checked && cboLocation.SelectedItem is DropDownItem<Location> location)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "SalesTax/GetEffectiveSalesTaxForLocation/" + location.Object.LocationID);
                get.AddCompanyHeader(Company.CompanyID);
                salesTaxes = await get.GetObject<List<SalesTax>>() ?? new List<SalesTax>();
            }

            decimal runningTotal = 0M;
            foreach(PurchaseOrderLineControl ctrl in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>())
            {
                runningTotal += ctrl.GetCurrentTotal() ?? 0M;
            }

            txtNetTotal.Text = runningTotal.ToString("N2");
            if (!salesTaxes.Any())
            {
                txtEstTax.Text = "0.00";
                txtGrossTotal.Text = txtNetTotal.Text;
            }
            else
            {
                decimal taxRate = salesTaxes.Sum(st => st.Rate / 100M);
                txtEstTax.Text = (runningTotal * taxRate).ToString("N2");
                txtGrossTotal.Text = (runningTotal + (runningTotal * taxRate)).ToString("N2");
            }

            lnkTaxBreakdown.Tag = salesTaxes;
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            await Save();
        }

        private void OrderFromCheckedChanged(object sender, EventArgs e)
        {
            cboLocation.Enabled = rdoLocation.Checked;
            cboGovernment.Enabled = rdoGovernment.Checked;

            foreach(PurchaseOrderLineControl ctrl in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>())
            {
                if (rdoLocation.Checked)
                {
                    ctrl.GovernmentIDDestination = null;
                    ctrl.CompanyIDDestination = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.CompanyID;
                    ctrl.LocationIDDestination = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.LocationID;
                }
                else if (rdoGovernment.Checked)
                {
                    ctrl.CompanyIDDestination = null;
                    ctrl.LocationIDDestination = null;
                    ctrl.GovernmentIDDestination = (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID;
                }
            }

            UpdateTotals();
        }

        private async void cmdSubmit_Click(object sender, EventArgs e)
        {
            if (!await InternalSave())
            {
                return;
            }

            try
            {
                toolStripMain.Enabled = false;
                loader.BringToFront();
                loader.Visible = true;

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Submit/" + PurchaseOrderID, new object());
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                PurchaseOrder purchaseOrder = await post.Execute<PurchaseOrder>();
                if (post.RequestSuccessful)
                {
                    StringBuilder linesWithDiscrepantPrices = new StringBuilder();
                    foreach(PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines ?? new List<PurchaseOrderLine>())
                    {
                        PurchaseOrderLineControl polc = pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().FirstOrDefault(ctrl => ctrl.PurchaseOrderLine?.PurchaseOrderLineID == line.PurchaseOrderLineID);
                        if (line.UnitCost != polc.GetCurrentUnitCost())
                        {
                            linesWithDiscrepantPrices.AppendLine($"* {line.Quantity}x {line.Item?.Name} (was {polc.GetCurrentUnitCost().Value.ToString("N2")} each, now {line.UnitCost.Value.ToString("N2")} each)");
                        }
                    }

                    if (linesWithDiscrepantPrices.Length > 0)
                    {
                        this.ShowWarning("The following prices were updated following submission:\r\n\r\n" + linesWithDiscrepantPrices.ToString());
                    }

                    // Open up next form
                    PurchaseOrder.OpenPurchaseOrderForm(this, purchaseOrder);
                    Close();
                }

            }
            finally
            {
                loader.Visible = false;
                toolStripMain.Enabled = true;
            }
        }

        public async Task Save()
        {
            await InternalSave();
        }

        public async Task<bool> InternalSave()
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ("Description", txtDescription)
            }))
            {
                return false;
            }

            if (rdoLocation.Checked && cboLocation.SelectedItem == null)
            {
                this.ShowError("Location is a required field");
            }
            if (rdoGovernment.Checked && cboGovernment.SelectedItem == null)
            {
                this.ShowError("Government is a required field");
            }

            PurchaseOrderLineControl[] purchaseOrderLines = pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>().ToArray();
            StringBuilder lineErrors = new StringBuilder();
            for (int i = 0; i < purchaseOrderLines.Length; i++)
            {
                foreach (string error in purchaseOrderLines[i].ValidatePresence())
                {
                    lineErrors.AppendLine($"Purchase Order Line {i + 1}: {error}");
                }
            }

            if (lineErrors.Length > 0)
            {
                this.ShowError("The following errors occurred with Purchase Order Lines:\r\n" + lineErrors.ToString());
                return false;
            }

            try
            {
                toolStripMain.Enabled = false;

                loader.BringToFront();
                loader.Visible = true;

                PurchaseOrder purchaseOrderToSave = new PurchaseOrder()
                {
                    PurchaseOrderID = PurchaseOrderID,
                    LocationIDOrigin = LocationModel.LocationID,
                    LocationIDDestination = rdoLocation.Checked ? ((DropDownItem<Location>)cboLocation.SelectedItem).Object.LocationID : null,
                    GovernmentIDDestination = rdoGovernment.Checked ? ((DropDownItem<Government>)cboGovernment.SelectedItem).Object.GovernmentID : null,
                    Description = txtDescription.Text,
                    PurchaseOrderIDClonedFrom = _purchaseOrderIDClonedFromAtLoad
                };

                if (PurchaseOrderID == null)
                {
                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Post", purchaseOrderToSave);
                    post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    purchaseOrderToSave = await post.Execute<PurchaseOrder>();
                    if (post.RequestSuccessful)
                    {
                        PurchaseOrderID = purchaseOrderToSave.PurchaseOrderID;
                        _purchaseOrderIDClonedFromAtLoad = purchaseOrderToSave.PurchaseOrderIDClonedFrom;
                        foreach (PurchaseOrderLineControl line in purchaseOrderLines)
                        {
                            line.PurchaseOrderID = PurchaseOrderID;
                        }

                        Text += $" - {PurchaseOrderID}";
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    PutData put = new PutData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Put", purchaseOrderToSave);
                    put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    purchaseOrderToSave = await put.Execute<PurchaseOrder>();
                    if (!put.RequestSuccessful)
                    {
                        return false;
                    }

                    _purchaseOrderIDClonedFromAtLoad = purchaseOrderToSave.PurchaseOrderIDClonedFrom;
                }

                lblSaveToAddPlans.Visible = false;
                toolAddPlan.Visible = true;
                toolDeletePlan.Visible = true;
                toolSaveTemplate.Visible = true;
                cloneSeparator.Visible = true;
                toolClonePlan.Visible = true;
                toolDelete.Enabled = true;

                SetupFormWarnings(purchaseOrderToSave);

                lineErrors = new StringBuilder();
                for (int i = 0; i < purchaseOrderLines.Length; i++)
                {
                    PurchaseOrderLineControl line = purchaseOrderLines[i];
                    string lineError;
                    if (!string.IsNullOrEmpty(lineError = await line.PerformSave()))
                    {
                        lineErrors.AppendLine($"Purchase Order Line {i + 1}: {lineError}");
                    }
                }

                foreach (long? deletedLineID in _deletedLineIDs)
                {
                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "PurchaseOrderLine/Delete/" + deletedLineID);
                    delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    delete.SuppressErrors = true;
                    await delete.Execute();
                    if (!delete.RequestSuccessful)
                    {
                        lineErrors.AppendLine("Deleted Purchase Order Line: " + delete.LastError);
                    }
                }

                _deletedLineIDs.Clear();

                if (lineErrors.Length > 0)
                {
                    this.ShowError("These Purchase Order Lines encountered errors and were not saved::\r\n" + lineErrors.ToString() +
                        "\r\n\r\nCorrect these errors and try saving again.");
                    return false;
                }

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrderLine/GetByPurchaseOrderID/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                _linesAtLoad = await get.GetObject<List<PurchaseOrderLine>>() ?? new List<PurchaseOrderLine>();

                OnSave?.Invoke(this, EventArgs.Empty);

                await RefreshFulfillmentPlans();

                return true;
            }
            finally
            {
                toolStripMain.Enabled = true;
                loader.Visible = false;
            }
        }

        bool isLoadingFulfillmentPlans = false;
        private async Task RefreshFulfillmentPlans()
        {
            long? previouslySelectedPlanID = null;
            if (dgvFulfillmentPlans.SelectedRows.Count > 0)
            {
                previouslySelectedPlanID = dgvFulfillmentPlans.SelectedRows[0].Tag as long?;
            }

            dgvFulfillmentPlans.Rows.Clear();

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/GetByPurchaseOrderID/" + PurchaseOrderID);
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            List<FulfillmentPlan> plans = await get.GetObject<List<FulfillmentPlan>>() ?? new List<FulfillmentPlan>();

            HashSet<long?> _purchaseOrderLineIDsOnFulfillmentPlans = new HashSet<long?>();
            try
            {
                isLoadingFulfillmentPlans = true;
                foreach (FulfillmentPlan plan in plans)
                {
                    int rowIndex = dgvFulfillmentPlans.Rows.Add();
                    DataGridViewRow row = dgvFulfillmentPlans.Rows[rowIndex];
                    row.Cells[colRailcar.Name].Value = plan.RailcarID != null ?
                                                        plan.Railcar?.ReportingMark + plan.Railcar?.ReportingNumber :
                                                        plan.LeaseRequestID != null ?
                                                            $"Lease Request ID: {plan.LeaseRequestID} (Bids: {plan.LeaseRequest?.LeaseBids?.Count() ?? 0})" :
                                                            "None";
                    row.Cells[colFPPOLines.Name].Value = (plan.FulfillmentPlanPurchaseOrderLines?.Count ?? 0) + " Lines";
                    StringBuilder routeStringBuilder = new StringBuilder();
                    if (!plan.FulfillmentPlanRoutes.Any())
                    {
                        routeStringBuilder.Append("None");
                    }
                    else
                    {
                        FulfillmentPlanRoute firstRoute = plan.FulfillmentPlanRoutes.FirstOrDefault();
                        routeStringBuilder.Append(firstRoute.CompanyFrom?.Name ?? firstRoute.GovernmentFrom?.Name);

                        foreach (FulfillmentPlanRoute route in plan.FulfillmentPlanRoutes)
                        {
                            routeStringBuilder.Append($" -> {route.CompanyTo?.Name ?? route.GovernmentTo?.Name}");
                        }
                    }
                    row.Cells[colRoute.Name].Value = routeStringBuilder.ToString();

                    row.Tag = plan.FulfillmentPlanID;

                    _purchaseOrderLineIDsOnFulfillmentPlans.AddRange(plan.FulfillmentPlanPurchaseOrderLines.Select(fppol => fppol.PurchaseOrderLineID));
                }
            }
            finally
            {
                isLoadingFulfillmentPlans = false;
            }

            if (previouslySelectedPlanID != null)
            {
                foreach(DataGridViewRow row in dgvFulfillmentPlans.Rows)
                {
                    row.Selected = row.Tag as long? == previouslySelectedPlanID;
                }
            }

            if (dgvFulfillmentPlans.SelectedRows.Count > 0)
            {
                dgvFulfillmentPlans_SelectionChanged(this, EventArgs.Empty);
            }

            foreach (PurchaseOrderLineControl line in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>())
            {
                line.SetHasFulfillmentPlan(_purchaseOrderLineIDsOnFulfillmentPlans.Contains(line.PurchaseOrderLine?.PurchaseOrderLineID));
            }
        }

        private void toolAddPlan_Click(object sender, EventArgs e)
        {
            AddFulfillmentPlanControl();
        }

        private void AddFulfillmentPlanControl(long? fulfillmentPlanID = null)
        {
            grpFulfillmentPlanInformation.Controls.OfType<FulfillmentPlanControl>().ToList().ForEach(fpc => grpFulfillmentPlanInformation.Controls.Remove(fpc));
            lblPlanPlaceholder.Visible = false;

            FulfillmentPlanControl control = new FulfillmentPlanControl()
            {
                FulfillmentPlanID = fulfillmentPlanID,
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                Dock = DockStyle.Fill,
                StartIDGovernment = (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID,
                StartIDCompany = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.CompanyID,
                StartIDLocation = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.LocationID,
                StartName = rdoLocation.Checked ? (cboLocation.SelectedItem as DropDownItem<Location>)?.Text : rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Text : null,
                EndIDLocation = LocationModel.LocationID,
                EndName = $"{Company.Name} ({LocationModel.Name})",
                Studio = Studio
            };
            control.OnSave += FulfillmentPlanControl_OnSave;
            control.OnReset += FulfillmentPlanControl_OnReset;
            grpFulfillmentPlanInformation.Controls.Add(control);

            control.SetPurchaseOrderLines(_linesAtLoad);
        }

        private void FulfillmentPlanControl_OnReset(object sender, EventArgs e)
        {
            FulfillmentPlanControl control = sender as FulfillmentPlanControl;
            if (control == null)
            {
                return;
            }

            AddFulfillmentPlanControl(control.FulfillmentPlanID);
        }

        private async void FulfillmentPlanControl_OnSave(object sender, EventArgs e)
        {
            await RefreshFulfillmentPlans();

            GetData getData = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
            getData.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            PurchaseOrder purchaseOrder = await getData.GetObject<PurchaseOrder>();

            if (purchaseOrder != null)
            {
                SetupFormWarnings(purchaseOrder);
            }
        }

        private void dgvFulfillmentPlans_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoadingFulfillmentPlans)
            {
                return;
            }

            DataGridViewRow row = dgvFulfillmentPlans.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            toolDeletePlan.Enabled = row != null;
            toolClonePlan.Enabled = row != null;

            if (row != null)
            {
                AddFulfillmentPlanControl(row.Tag as long?);
            }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(PurchaseOrderLineControl ctrl in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>())
            {
                ctrl.GovernmentIDDestination = null;
                ctrl.CompanyIDDestination = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.CompanyID;
                ctrl.LocationIDDestination = (cboLocation.SelectedItem as DropDownItem<Location>)?.Object.LocationID;
            }

            UpdateTotals();
        }

        private void cboGovernment_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (PurchaseOrderLineControl ctrl in pnlPurchaseOrderLines.Controls.OfType<PurchaseOrderLineControl>())
            {
                ctrl.CompanyIDDestination = null;
                ctrl.LocationIDDestination = null;
                ctrl.GovernmentIDDestination = (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID;
            }

            UpdateTotals();
        }

        private async void toolLoadTemplate_Click(object sender, EventArgs e)
        {
            Templates.frmTemplateDialog loadTemplate = new Templates.frmTemplateDialog()
            {
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                Theme = Theme,
                DialogMode = Templates.frmTemplateDialog.DialogModes.Open
            };
            DialogResult result = loadTemplate.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/CloneFromTemplate", new { loadTemplate.SelectedTemplate.PurchaseOrderTemplateID });
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            PurchaseOrder newPO = await post.Execute<PurchaseOrder>();
            if (post.RequestSuccessful)
            {
                frmPurchaseOrder newPOForm = new frmPurchaseOrder();
                Studio.DecorateStudioContent(newPOForm);
                newPOForm.PurchaseOrderID = newPO.PurchaseOrderID;
                newPOForm.Company = Company;
                newPOForm.LocationModel = LocationModel;
                newPOForm.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                Close();
            }
        }

        private async void toolSaveTemplate_Click(object sender, EventArgs e)
        {
            long? templateID = await PurchaseOrderTemplate.PromptAndSavePurchaseOrderAsTemplate(Company, LocationModel, Theme, PurchaseOrderID);

            if (templateID != null)
            {
                _templateWarning = true;
                SetupFormWarnings(null);
            }
        }

        private async void toolDelete_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete this Purchase Order?"))
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Delete/" + PurchaseOrderID);
            delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await delete.Execute();
            Close();
        }

        private void toolWarnings_Click(object sender, EventArgs e)
        {
            if (!_clonedWarning && !_templateWarning && !_clonesExistWarning)
            {
                return;
            }

            StringBuilder warningBuilder = new StringBuilder();
            if (_clonedWarning)
            {
                warningBuilder.AppendLine("* This Purchase Order was cloned from a Template. Significant changes will skip pre-approvals.");
            }
            if (_templateWarning)
            {
                warningBuilder.AppendLine("* This Purchase Order is saved as a Template. Significant changes will delete the Template.");
            }
            if (_clonesExistWarning)
            {
                warningBuilder.AppendLine("* This Purchase Order has one or more Clones. Significant changes will skip pre-approvals on all Clones.");
            }

            this.ShowWarning(warningBuilder.ToString());
        }

        private async void toolDeletePlan_Click(object sender, EventArgs e)
        {
            if (dgvFulfillmentPlans.SelectedRows.Count <= 0 || !(dgvFulfillmentPlans.SelectedRows[0].Tag is long?) || !this.Confirm("Are you sure you want to delete this Fulfillment Plan?"))
            {
                return;
            }

            long? planID = dgvFulfillmentPlans.SelectedRows[0].Tag as long?;

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/Delete/" + planID);
            delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                await RefreshFulfillmentPlans();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();

                SetupFormWarnings(purchaseOrder);
            }
        }

        private async void toolClonePlan_Click(object sender, EventArgs e)
        {
            if (dgvFulfillmentPlans.SelectedRows.Count <= 0 || !(dgvFulfillmentPlans.SelectedRows[0].Tag is long?))
            {
                return;
            }

            long? fulfillmentPlanID = dgvFulfillmentPlans.SelectedRows[0].Tag as long?;
            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/Clone", new { fulfillmentPlanID });
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await post.ExecuteNoResult();

            if (post.RequestSuccessful)
            {
                await RefreshFulfillmentPlans();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();

                SetupFormWarnings(purchaseOrder);
            }
        }

        private void lnkTaxBreakdown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!(lnkTaxBreakdown.Tag is List<SalesTax> salesTaxes) || !salesTaxes.Any())
            {
                this.ShowInformation("No tax information available.");
                return;
            }

            StringBuilder taxInformation = new StringBuilder();
            foreach(SalesTax salesTax in salesTaxes)
            {
                taxInformation.AppendLine($"{salesTax.Government.Name}: {salesTax.Rate}%");
            }

            this.ShowInformation(taxInformation.ToString());
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
