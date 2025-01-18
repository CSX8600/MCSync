using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Wizard;
using FleetTracking.Reports.RailActivity;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyStudio.Purchasing.Fulfillment
{
    public class FulfillmentWizardController : WizardController<FulfillmentWizardData>
    {
        private long? _companyID;
        private long? _locationID;

        public long? PurchaseOrderID { get; set; }

        public FulfillmentWizardController(long? companyID, long? locationID) : base()
        {
            _companyID = companyID;
            _locationID = locationID;
        }

        protected override string WindowTitle => "Fulfillment Wizard";

        protected override Image Logo => Properties.Resources.lorry_go;

        protected override string ScreenTitle => "Fulfillment Entry";

        protected override string RunButtonCaption => "Save";

        protected override async Task<FulfillmentWizardData> CreateData()
        {
            FulfillmentWizardData wizardData = new FulfillmentWizardData()
            {
                CompanyID = _companyID,
                LocationID = _locationID
            };

            if (PurchaseOrderID != null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(_companyID, _locationID);
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
                if (purchaseOrder != null)
                {
                    HashSet<long?> _railcarIDs = new HashSet<long?>();
                    foreach(PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines)
                    {
                        get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetForPurchaseOrderLine/" + line.PurchaseOrderLineID);
                        get.AddLocationHeader(_companyID, _locationID);
                        List<Railcar> railcars = await get.GetObject<List<Railcar>>() ?? new List<Railcar>();
                        foreach(Railcar railcar in railcars)
                        {
                            if (_railcarIDs.Add(railcar.RailcarID))
                            {
                                wizardData.SelectedRailcars.Add(railcar);
                            }
                        }
                    }
                }
            }

            return wizardData;
        }

        protected override MultiMap<IWizardStep<FulfillmentWizardData>, StepConnection> GetConnections()
        {
            MultiMap<IWizardStep<FulfillmentWizardData>, StepConnection> steps = new MultiMap<IWizardStep<FulfillmentWizardData>, StepConnection>();

            InboundCarStepControl inboundStep = new InboundCarStepControl();
            FulfillmentEntryStepControl fulfillmentEntry = new FulfillmentEntryStepControl();
            steps.Add(inboundStep, new StepConnection(fulfillmentEntry));

            AdditionalTasksStepControl additionalTasks = new AdditionalTasksStepControl();
            steps.Add(fulfillmentEntry, new StepConnection(additionalTasks));

            ReviewStepControl review = new ReviewStepControl();
            steps.Add(additionalTasks, new StepConnection(review));

            EndStep<FulfillmentWizardData> endStep = new EndStep<FulfillmentWizardData>();
            steps.Add(review, new StepConnection(endStep));

            return steps;
        }

        protected override async Task WizardComplete(FulfillmentWizardData data)
        {
            List<Models.Fulfillment> savedFulfillments = new List<Models.Fulfillment>();
            foreach(Models.Fulfillment fulfillment in data.Fulfillments.Where(f => f.Quantity != null && f.Quantity > 0))
            {
                fulfillment.FulfillmentTime = DateTime.Now;
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Fulfillment/Post", fulfillment);
                post.AddLocationHeader(data.CompanyID, data.LocationID);
                Models.Fulfillment savedFulfillment = await post.Execute<Models.Fulfillment>();
                if (post.RequestSuccessful)
                {
                    savedFulfillments.Add(savedFulfillment);
                }
            }

            List<long?> fulfillmentIDs = savedFulfillments.Select(f => f.FulfillmentID).ToList();
            PostData postIssueBOLs = new PostData(DataAccess.APIs.CompanyStudio, "Fulfillment/IssueBillsOfLading", new { fulfillmentIDs });
            postIssueBOLs.AddLocationHeader(data.CompanyID, data.LocationID);
            List<BillOfLading> billsOfLading = await postIssueBOLs.Execute<List<BillOfLading>>() ?? new List<BillOfLading>();

            if (data.ReleaseCars)
            {
                foreach(BillOfLading billOfLading in billsOfLading)
                {
                    GetData get = new GetData(DataAccess.APIs.FleetTracking, "Railcar/Get/" + billOfLading.RailcarID);
                    get.AddCompanyHeader(data.CompanyID);
                    FleetTracking.Models.Railcar railcar = await get.GetObject<FleetTracking.Models.Railcar>();
                    if (railcar != null && railcar.CompanyIDPossessor == data.CompanyID)
                    {
                        PatchData patch = new PatchData(DataAccess.APIs.FleetTracking, "Railcar/Patch", PatchData.PatchMethods.Replace, railcar.RailcarID, new Dictionary<string, object>()
                        {
                            { nameof(FleetTracking.Models.Railcar.GovernmentIDPossessor), billOfLading.GovernmentIDCarrier },
                            { nameof(FleetTracking.Models.Railcar.CompanyIDPossessor), billOfLading.CompanyIDCarrier }
                        });
                        patch.AddCompanyHeader(data.CompanyID);
                        await patch.Execute();
                    }
                }
            }
        }
    }
}

