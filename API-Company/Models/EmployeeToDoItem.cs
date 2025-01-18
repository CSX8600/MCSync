using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Common;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using WebModels.company;
using WebModels.fleet;
using WebModels.invoicing;
using WebModels.purchasing;
using static API_Company.Controllers.EmployeeController;

namespace API_Company.Models
{
    public class EmployeeToDoItem
    {
        public enum Types
        {
            PayableInvoiceWaiting = 0,
            ReceivableInvoiceWaiting,
            PayablePastDueInvoice,
            ReceivablePastDueInvoice,
            PurchaseOrderWaitingApproval,
            QuotationRequestWaiting,
            RailcarAwaitingAction,
            RegisterOffline,
            OpenPurchaseOrders,
            AutomaticPaymentsAlmostComplete
        }

        public enum Severities
        {
            Informational,
            Important,
            Urgent
        }

        public Severities Severity { get; set; }
        public Types Type { get; set; }
        public string Message { get; set; }
        public long? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public long? LocationID { get; set; }
        public string LocationName { get; set; }
        public long? SourceID { get; set; }
        public string MesaSuiteURI { get; set; }

        public static async Task<List<EmployeeToDoItem>> GetForUserID(long userID)
        {
            Search<Employee> employeeRecordsForUser = new Search<Employee>(new LongSearchCondition<Employee>()
            {
                Field = nameof(Employee.UserID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = userID
            });
            List<string> fields = Schema.GetSchemaObject<Employee>().GetFields().Select(f => f.FieldName).Concat(
                                  Schema.GetSchemaObject<LocationEmployee>().GetFields().Select(f => nameof(Employee.LocationEmployees) + "." + f.FieldName)).Concat(
                                  Schema.GetSchemaObject<FleetSecurity>().GetFields().Select(f => nameof(Employee.FleetSecurity) + "." + f.FieldName)).ToList();

            fields.Add(FieldPathUtility.CreateFieldPath<Employee>(e => e.Company.Name));
            fields.Add(FieldPathUtility.CreateFieldPath<Employee>(e => e.LocationEmployees.First().Location.Name));
            List<Employee> employeeList = (await Task.Run(() => employeeRecordsForUser.GetReadOnlyReader(null, fields))).ToList();
            List<EmployeeToDoItem> toDoItems = new List<EmployeeToDoItem>();

            // Check Invoices
            if (employeeList.SelectMany(e => e.LocationEmployees).Any(le => le.ManageInvoices))
            {
                Search<Invoice> invoiceSearch = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                        new ExistsSearchCondition<Invoice>()
                        {
                            RelationshipName = FieldPathUtility.CreateFieldPath<Invoice>(i => i.LocationFrom.LocationEmployees),
                            ExistsType = ExistsSearchCondition<Invoice>.ExistsTypes.Exists,
                            Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                                new LongSearchCondition<LocationEmployee>()
                                {
                                    Field = FieldPathUtility.CreateFieldPath<LocationEmployee>(le => le.Employee.UserID),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                    Value = userID
                                },
                                new BooleanSearchCondition<LocationEmployee>()
                                {
                                    Field = nameof(LocationEmployee.ManageInvoices),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                    Value = true
                                })
                        },
                        new ExistsSearchCondition<Invoice>()
                        {
                            RelationshipName = FieldPathUtility.CreateFieldPath<Invoice>(i => i.LocationTo.LocationEmployees),
                            ExistsType = ExistsSearchCondition<Invoice>.ExistsTypes.Exists,
                            Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                                new LongSearchCondition<LocationEmployee>()
                                {
                                    Field = FieldPathUtility.CreateFieldPath<LocationEmployee>(le => le.Employee.UserID),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                    Value = userID
                                },
                                new BooleanSearchCondition<LocationEmployee>()
                                {
                                    Field = nameof(LocationEmployee.ManageInvoices),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                    Value = true
                                })
                        }),
                    new IntSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.Status),
                        SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                        Value = (int)Invoice.Statuses.Complete
                    }));

                List<string> invoiceFields = FieldPathUtility.CreateFieldPathsAsList<Invoice>(i => new object[]
                {
                    i.InvoiceID,
                    i.LocationIDFrom,
                    i.LocationFrom.Name,
                    i.LocationFrom.CompanyID,
                    i.LocationFrom.Company.Name,
                    i.LocationFrom.LocationEmployees.First().ManageInvoices,
                    i.LocationFrom.LocationEmployees.First().Employee.UserID,
                    i.LocationIDTo,
                    i.LocationTo.Name,
                    i.LocationTo.CompanyID,
                    i.LocationTo.Company.Name,
                    i.LocationTo.LocationEmployees.First().ManageInvoices,
                    i.LocationTo.LocationEmployees.First().Employee.UserID,
                    i.Status,
                    i.DueDate,
                    i.InvoiceNumber
                });

                foreach(Invoice invoice in invoiceSearch.GetReadOnlyReader(null, invoiceFields))
                {
                    bool withinDueDate = invoice.DueDate > DateTime.Now;
                    switch (invoice.Status)
                    {
                        case Invoice.Statuses.Sent:
                            if (invoice.LocationTo.LocationEmployees.Any(le => le.ManageInvoices && le.Employee.UserID == userID))
                            {
                                toDoItems.Add(new EmployeeToDoItem()
                                {
                                    LocationID = invoice.LocationIDTo,
                                    LocationName = invoice.LocationTo.Name,
                                    CompanyID = invoice.LocationTo.CompanyID,
                                    CompanyName = invoice.LocationTo.Company.Name,
                                    SourceID = invoice.InvoiceID,
                                    Message = withinDueDate ?
                                                $"Payable Invoice {invoice.InvoiceNumber} awaits payment" :
                                                $"Payable Invoice {invoice.InvoiceNumber} is overdue",
                                    Type = withinDueDate ? Types.PayableInvoiceWaiting : Types.PayablePastDueInvoice,
                                    MesaSuiteURI = $"mesasuite://company/apinvoice/{invoice.InvoiceID}?companyid={invoice.LocationTo.CompanyID}&locationid={invoice.LocationIDTo}",
                                    Severity = withinDueDate ? Severities.Important : Severities.Urgent
                                });
                            }
                            else if (invoice.LocationFrom.LocationEmployees.Any(le => le.ManageInvoices && le.Employee.UserID == userID) && !withinDueDate)
                            {
                                toDoItems.Add(new EmployeeToDoItem()
                                {
                                    LocationID = invoice.LocationIDFrom,
                                    LocationName = invoice.LocationFrom.Name,
                                    CompanyID = invoice.LocationFrom.CompanyID,
                                    CompanyName = invoice.LocationFrom.Company.Name,
                                    SourceID = invoice.InvoiceID,
                                    Message = $"Receivable Invoice {invoice.InvoiceNumber} is overdue",
                                    Type = Types.ReceivablePastDueInvoice,
                                    MesaSuiteURI = $"mesasuite://company/arinvoice/{invoice.InvoiceID}?companyid={invoice.LocationFrom.CompanyID}&locationid={invoice.LocationIDFrom}",
                                    Severity = Severities.Urgent
                                });
                            }
                            break;
                        case Invoice.Statuses.ReadyForReceipt:
                            if (invoice.LocationFrom.LocationEmployees.Any(le => le.ManageInvoices && le.Employee.UserID == userID))
                            {
                                toDoItems.Add(new EmployeeToDoItem()
                                {
                                    LocationID = invoice.LocationIDFrom,
                                    LocationName = invoice.LocationFrom.Name,
                                    CompanyID = invoice.LocationFrom.CompanyID,
                                    CompanyName = invoice.LocationFrom.Company.Name,
                                    SourceID = invoice.InvoiceID,
                                    Message = withinDueDate ?
                                                $"Receivable Invoice {invoice.InvoiceNumber} is waiting to be received" :
                                                $"Receivable Invoice {invoice.InvoiceNumber} is overdue and is waiting to be received",
                                    Type = withinDueDate ? Types.ReceivableInvoiceWaiting : Types.ReceivablePastDueInvoice,
                                    MesaSuiteURI = $"mesasuite://company/arinvoice/{invoice.InvoiceID}?companyid={invoice.LocationFrom.CompanyID}&locationid={invoice.LocationIDFrom}",
                                    Severity = withinDueDate ? Severities.Important : Severities.Urgent
                                });
                            }
                            break;
                    }
                }

                Search<AutomaticInvoicePaymentConfiguration> automaticInvoicePaymentConfigurationSearch = new Search<AutomaticInvoicePaymentConfiguration>(new ExistsSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPath<AutomaticInvoicePaymentConfiguration>(aipc => aipc.LocationConfiguredFor.LocationEmployees),
                    ExistsType = ExistsSearchCondition<AutomaticInvoicePaymentConfiguration>.ExistsTypes.Exists,
                    Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<LocationEmployee>()
                        {
                            Field = FieldPathUtility.CreateFieldPath<LocationEmployee>(le => le.Employee.UserID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = userID
                        },
                        new BooleanSearchCondition<LocationEmployee>()
                        {
                            Field = nameof(LocationEmployee.ManageInvoices),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = true
                        })
                });

                List<string> configurationFields = FieldPathUtility.CreateFieldPathsAsList<AutomaticInvoicePaymentConfiguration>(aipc => new object[]
                {
                    aipc.AutomaticInvoicePaymentConfigurationID,
                    aipc.LocationIDConfiguredFor,
                    aipc.LocationConfiguredFor.CompanyID,
                    aipc.LocationConfiguredFor.Company.Name,
                    aipc.LocationConfiguredFor.Name,
                    aipc.PaidAmount,
                    aipc.MaxAmount,
                    aipc.GovernmentIDPayee,
                    aipc.GovernmentPayee.Name,
                    aipc.LocationIDPayee,
                    aipc.LocationPayee.Name,
                    aipc.LocationPayee.Company.Name
                });

                foreach(AutomaticInvoicePaymentConfiguration configuration in automaticInvoicePaymentConfigurationSearch.GetReadOnlyReader(null, configurationFields))
                {
                    if (configuration.MaxAmount == 0)
                    {
                        continue;
                    }

                    if (configuration.PaidAmount / configuration.MaxAmount >= 0.9M)
                    {
                        string payee = "";
                        if (configuration.GovernmentIDPayee != null)
                        {
                            payee = configuration.GovernmentPayee.Name + " (Government)";
                        }
                        else
                        {
                            payee = configuration.LocationPayee?.Company?.Name + " (" + configuration.LocationPayee?.Name + ")";
                        }

                        toDoItems.Add(new EmployeeToDoItem()
                        {
                            CompanyID = configuration.LocationConfiguredFor.CompanyID,
                            CompanyName = configuration.LocationConfiguredFor.Company.Name,
                            LocationID = configuration.LocationIDConfiguredFor,
                            LocationName = configuration.LocationConfiguredFor.Name,
                            SourceID = configuration.AutomaticInvoicePaymentConfigurationID,
                            Message = $"Automatic Invoice Payments for {payee} is near or at the maximum amount (MBD${configuration.PaidAmount.Value.ToString("N2")}/MBD${configuration.MaxAmount.Value.ToString("N2")})",
                            MesaSuiteURI = "mesasuite://company/automaticinvoicepaymentconfiguration/" + configuration.AutomaticInvoicePaymentConfigurationID + "?companyid=" + configuration.LocationConfiguredFor.CompanyID + "&locationid=" + configuration.LocationIDConfiguredFor,
                            Type = Types.AutomaticPaymentsAlmostComplete,
                            Severity = Severities.Important
                        });
                    }
                }
            }

            // Check Purchase Orders
            if (employeeList.SelectMany(e => e.LocationEmployees).Any(le => le.ManagePurchaseOrders))
            {
                SearchConditionGroup employeeExistsForManagePOsCondition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<Employee>()
                            {
                                Field = nameof(Employee.UserID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = userID
                            },
                            new ExistsSearchCondition<Employee>()
                            {
                                RelationshipName = nameof(Employee.LocationEmployees),
                                ExistsType = ExistsSearchCondition<Employee>.ExistsTypes.Exists,
                                Condition = new BooleanSearchCondition<LocationEmployee>()
                                {
                                    Field = nameof(LocationEmployee.ManagePurchaseOrders),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                    Value = true
                                }
                            });

                SearchConditionGroup locationEmployeeExistsForManagePOsCondition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<LocationEmployee>()
                    {
                        Field = FieldPathUtility.CreateFieldPath<LocationEmployee>(le => le.Employee.UserID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = userID
                    },
                    new BooleanSearchCondition<LocationEmployee>()
                    {
                        Field = nameof(LocationEmployee.ManagePurchaseOrders),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = true
                    });
                

                // Purchase Order Approvals
                Search<PurchaseOrderApproval> approvalSearch = new Search<PurchaseOrderApproval>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new ExistsSearchCondition<PurchaseOrderApproval>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPath<PurchaseOrderApproval>(poa => poa.CompanyApprover.Employees),
                        ExistsType = ExistsSearchCondition<PurchaseOrderApproval>.ExistsTypes.Exists,
                        Condition = employeeExistsForManagePOsCondition
                    },
                    new IntSearchCondition<PurchaseOrderApproval>()
                    {
                        Field = nameof(PurchaseOrderApproval.ApprovalStatus),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = (int)PurchaseOrderApproval.ApprovalStatuses.Pending
                    }));

                List<string> approvalFields = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderApproval>(poa => new object[]
                {
                    poa.CompanyIDApprover,
                    poa.CompanyApprover.Name,
                    poa.CompanyApprover.Locations.First().LocationID,
                    poa.CompanyApprover.Locations.First().Name,
                    poa.CompanyApprover.Locations.First().LocationEmployees.First().ManagePurchaseOrders,
                    poa.CompanyApprover.Locations.First().LocationEmployees.First().Employee.UserID,
                    poa.PurchaseOrderID,
                    poa.PurchaseOrderApprovalID
                });

                foreach(PurchaseOrderApproval approval in approvalSearch.GetReadOnlyReader(null, approvalFields))
                {
                    // Find first applicable location for approval
                    Location locationForApproval = approval.CompanyApprover.Locations.FirstOrDefault(l => l.LocationEmployees.Any(le => le.ManagePurchaseOrders && le.Employee.UserID == userID));

                    toDoItems.Add(new EmployeeToDoItem()
                    {
                        CompanyID = approval.CompanyIDApprover,
                        CompanyName = approval.CompanyApprover.Name,
                        LocationID = locationForApproval.LocationID,
                        LocationName = locationForApproval.Name,
                        SourceID = approval.PurchaseOrderID,
                        Type = Types.PurchaseOrderWaitingApproval,
                        Message = $"Purchase Order {approval.PurchaseOrderID} is waiting for your approval",
                        MesaSuiteURI = $"mesasuite://company/poapproval/{approval.PurchaseOrderID}/{approval.PurchaseOrderApprovalID}?companyid={approval.CompanyIDApprover}&locationid={locationForApproval.LocationID}",
                        Severity = Severities.Important
                    });
                }

                // Purchase Orders
                Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new ExistsSearchCondition<PurchaseOrder>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPath<PurchaseOrder>(po => po.LocationDestination.LocationEmployees),
                        ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                        Condition = locationEmployeeExistsForManagePOsCondition
                    },
                    new IntSearchCondition<PurchaseOrder>()
                    {
                        Field = nameof(PurchaseOrder.Status),
                        SearchConditionType = SearchCondition.SearchConditionTypes.List,
                        List = new List<int>() { (int)PurchaseOrder.Statuses.Accepted, (int)PurchaseOrder.Statuses.InProgress }
                    }));

                Dictionary<long?, int> purchaseOrdersByLocation = new Dictionary<long?, int>();
                foreach(PurchaseOrder purchaseOrder in purchaseOrderSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new object[] { po.LocationIDDestination })))
                {
                    purchaseOrdersByLocation.GetOrSet(purchaseOrder.LocationIDDestination, () => 0);
                    purchaseOrdersByLocation[purchaseOrder.LocationIDDestination]++;
                }

                foreach(KeyValuePair<long?, int> countByLocationID in purchaseOrdersByLocation)
                {
                    Employee employee = employeeList.Where(e => e.LocationEmployees.Any(l => l.ManagePurchaseOrders && l.LocationID == countByLocationID.Key)).First();
                    Company company = employee.Company;
                    Location location = employee.LocationEmployees.First(l => l.LocationID == countByLocationID.Key).Location;

                    toDoItems.Add(new EmployeeToDoItem()
                    {
                        CompanyID = company.CompanyID,
                        CompanyName = company.Name,
                        LocationID = countByLocationID.Key,
                        LocationName = location.Name,
                        Type = Types.OpenPurchaseOrders,
                        Message = $"You have {countByLocationID.Value} open Purchase Order(s)",
                        MesaSuiteURI = $"mesasuite://company/purchaseorders?companyid={employee.CompanyID}&locationid={countByLocationID.Key}",
                        Severity = Severities.Informational
                    });
                }

                // Quotation Requests
                Search<QuotationRequest> quotationRequestSearch = new Search<QuotationRequest>(
                    new ExistsSearchCondition<QuotationRequest>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPath<QuotationRequest>(qr => qr.CompanyTo.Employees),
                        ExistsType = ExistsSearchCondition<QuotationRequest>.ExistsTypes.Exists,
                        Condition = employeeExistsForManagePOsCondition
                    });

                List<string> quotationRequestFields = FieldPathUtility.CreateFieldPathsAsList<QuotationRequest>(qr => new object[]
                {
                    qr.QuotationRequestID,
                    qr.CompanyIDTo,
                    qr.CompanyTo.Name,
                    qr.CompanyTo.Locations.First().LocationID,
                    qr.CompanyTo.Locations.First().Name,
                    qr.CompanyTo.Locations.First().LocationEmployees.First().ManagePurchaseOrders,
                    qr.CompanyTo.Locations.First().LocationEmployees.First().Employee.UserID,
                    qr.CompanyFrom.Name,
                    qr.GovernmentFrom.Name,
                    qr.QuotationRequestItems.First().QuotationRequestItemID
                });

                foreach(QuotationRequest quotationRequest in quotationRequestSearch.GetReadOnlyReader(null, quotationRequestFields))
                {
                    Location locationForQuotationRequest = quotationRequest.CompanyTo.Locations.FirstOrDefault(l => l.LocationEmployees.Any(le => le.ManagePurchaseOrders && le.Employee.UserID == userID));
                    toDoItems.Add(new EmployeeToDoItem()
                    {
                        CompanyID = quotationRequest.CompanyIDTo,
                        CompanyName = quotationRequest.CompanyTo.Name,
                        LocationID = locationForQuotationRequest.LocationID,
                        LocationName = locationForQuotationRequest.Name,
                        SourceID = quotationRequest.QuotationRequestID,
                        Type = Types.QuotationRequestWaiting,
                        Message = $"Quotation Request from {quotationRequest.CompanyFrom?.Name}{quotationRequest.GovernmentFrom.Name} for {quotationRequest.QuotationRequestItems?.Count ?? 0} item(s) is awaiting response",
                        MesaSuiteURI = $"mesasuite://company/quotationrequest/{quotationRequest.QuotationRequestID}?companyid={quotationRequest.CompanyIDTo}&locationid={locationForQuotationRequest.LocationID}",
                        Severity = Severities.Important
                    });
                }

                // Railcars that need attention
                Search<Railcar> railcarSearch = new Search<Railcar>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new ExistsSearchCondition<Railcar>()
                    {
                        RelationshipName = nameof(Railcar.FulfillmentPlans),
                        ExistsType = ExistsSearchCondition<Railcar>.ExistsTypes.Exists,
                        Condition = new ExistsSearchCondition<FulfillmentPlan>()
                        {
                            RelationshipName = nameof(FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                            ExistsType = ExistsSearchCondition<FulfillmentPlan>.ExistsTypes.Exists,
                            Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                                new IntSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                                {
                                    Field = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.Status }).First(),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.List,
                                    List = new List<int>() { (int)PurchaseOrder.Statuses.Accepted, (int)PurchaseOrder.Statuses.InProgress }
                                },
                                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                                    new ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                                    {
                                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.LocationOrigin.LocationEmployees }).First(),
                                        ExistsType = ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>.ExistsTypes.Exists,
                                        Condition = locationEmployeeExistsForManagePOsCondition
                                    },
                                    new ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                                    {
                                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.LocationDestination.LocationEmployees }).First(),
                                        ExistsType = ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>.ExistsTypes.Exists,
                                        Condition = locationEmployeeExistsForManagePOsCondition
                                    }))
                        }
                    },
                    new ExistsSearchCondition<Railcar>()
                    {
                        RelationshipName = nameof(Railcar.RailcarLoads),
                        ExistsType = ExistsSearchCondition<Railcar>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                            new ExistsSearchCondition<RailcarLoad>()
                            {
                                RelationshipName = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.PurchaseOrderLine.PurchaseOrder.LocationOrigin.LocationEmployees }).First(),
                                ExistsType = ExistsSearchCondition<RailcarLoad>.ExistsTypes.Exists,
                                Condition = locationEmployeeExistsForManagePOsCondition
                            },
                            new ExistsSearchCondition<RailcarLoad>()
                            {
                                RelationshipName = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.PurchaseOrderLine.PurchaseOrder.LocationDestination.LocationEmployees }).First(),
                                ExistsType = ExistsSearchCondition<RailcarLoad>.ExistsTypes.Exists,
                                Condition = locationEmployeeExistsForManagePOsCondition
                            })
                    }),
                new ExistsSearchCondition<Railcar>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<Railcar>(r => new List<object>() { r.RailLocation.Track.CompanyOwner.Employees }).First(),
                    ExistsType = ExistsSearchCondition<Railcar>.ExistsTypes.Exists,
                    Condition = employeeExistsForManagePOsCondition
                },
                new ExistsSearchCondition<Railcar>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPath<Railcar>(r => r.CompanyPossessor.Employees),
                    ExistsType = ExistsSearchCondition<Railcar>.ExistsTypes.Exists,
                    Condition = employeeExistsForManagePOsCondition
                }));

                List<string> railcarFields = FieldPathUtility.CreateFieldPathsAsList<Railcar>(r => new object[]
                {
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationIDDestination,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationDestination.Name,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationDestination.CompanyID,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationDestination.Company.Name,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationIDOrigin,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.Name,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.CompanyID,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.Company.Name,
                    r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.Status,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationIDDestination,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationDestination.Name,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationDestination.CompanyID,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationDestination.Company.Name,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationIDOrigin,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.Name,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.CompanyID,
                    r.RailcarLoads.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.Company.Name,
                    r.CompanyIDPossessor
                });

                List<Railcar> railcars = railcarSearch.GetReadOnlyReader(null, railcarFields).ToList();
                Dictionary<long?, int> railcarCountByLocationID = new Dictionary<long?, int>();
                foreach(Railcar railcar in railcars)
                {
                    PurchaseOrder openPurchaseOrderForRailcar = railcar.FulfillmentPlans?.Select(fp => fp.FulfillmentPlanPurchaseOrderLines?.FirstOrDefault(fppol => fppol.PurchaseOrderLine?.PurchaseOrder?.Status == PurchaseOrder.Statuses.InProgress || fppol.PurchaseOrderLine?.PurchaseOrder?.Status == PurchaseOrder.Statuses.Accepted)?.PurchaseOrderLine.PurchaseOrder).FirstOrDefault();
                    if (openPurchaseOrderForRailcar == null || !employeeList.Any(e => e.LocationEmployees.Any(le => le.ManagePurchaseOrders && (le.LocationID == openPurchaseOrderForRailcar.LocationIDOrigin || le.LocationID == openPurchaseOrderForRailcar.LocationIDDestination))))
                    {
                        openPurchaseOrderForRailcar = railcar.RailcarLoads?.FirstOrDefault(rl => employeeList.Any(e => e.LocationEmployees.Any(el => el.ManagePurchaseOrders && (el.LocationID == rl.PurchaseOrderLine?.PurchaseOrder?.LocationIDDestination || el.LocationID == rl.PurchaseOrderLine?.PurchaseOrder?.LocationIDOrigin))))?.PurchaseOrderLine?.PurchaseOrder;
                    }

                    if (openPurchaseOrderForRailcar == null || railcar.CompanyIDPossessor == null)
                    {
                        continue;
                    }

                    long? locationID = openPurchaseOrderForRailcar.LocationOrigin.CompanyID == railcar.CompanyIDPossessor ? openPurchaseOrderForRailcar.LocationIDOrigin : openPurchaseOrderForRailcar.LocationIDDestination;
                    if (!employeeList.FirstOrDefault(e => e.CompanyID == railcar.CompanyIDPossessor)?.LocationEmployees.Any(le => le.LocationID == locationID && le.ManagePurchaseOrders) ?? false)
                    {
                        continue;
                    }

                    railcarCountByLocationID.GetOrSet(locationID, () => 0);
                    railcarCountByLocationID[locationID]++;
                }

                foreach (KeyValuePair<long?, int> countByLocationID in railcarCountByLocationID)
                {
                    if (countByLocationID.Value <= 0)
                    {
                        continue;
                    }

                    Employee employee = employeeList.Where(e => e.LocationEmployees.Any(le => le.ManagePurchaseOrders && le.LocationID == countByLocationID.Key)).FirstOrDefault();
                    Company company = employee.Company;
                    Location location = employee.LocationEmployees.First(le => le.LocationID == countByLocationID.Key).Location;
                    toDoItems.Add(new EmployeeToDoItem()
                    {
                        CompanyID = company.CompanyID,
                        CompanyName = company.Name,
                        LocationID = location.LocationID,
                        LocationName = location.Name,
                        Type = Types.RailcarAwaitingAction,
                        Message = $"You have {countByLocationID.Value} railcar(s) on your property awaiting action",
                        MesaSuiteURI = $"mesasuite://company/shippingreceiving?companyid={employee.CompanyID}&locationid={countByLocationID.Key}",
                        Severity = Severities.Informational
                    });
                }
            }

            // Check registers
            if (employeeList.Any(e => e.LocationEmployees.Any(el => el.ManageRegisters)))
            {
                Search<Register> registerSearch = new Search<Register>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new IntSearchCondition<Register>()
                    {
                        Field = FieldPathUtility.CreateFieldPath<Register>(r => r.CurrentStatus.Status),
                        SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                        Value = (int)RegisterStatus.Statuses.Online
                    },
                    new ExistsSearchCondition<Register>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPath<Register>(r => r.Location.LocationEmployees),
                        ExistsType = ExistsSearchCondition<Register>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<LocationEmployee>()
                            {
                                Field = FieldPathUtility.CreateFieldPath<LocationEmployee>(le => le.Employee.UserID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = userID
                            },
                            new BooleanSearchCondition<LocationEmployee>()
                            {
                                Field = nameof(LocationEmployee.ManageRegisters),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = true
                            })
                    }));

                List<string> registerFields = FieldPathUtility.CreateFieldPathsAsList<Register>(r => new object[]
                {
                    r.RegisterID,
                    r.Name,
                    r.LocationID,
                    r.Location.Name,
                    r.Location.CompanyID,
                    r.Location.Company.Name
                });

                foreach(Register register in registerSearch.GetReadOnlyReader(null, registerFields))
                {
                    toDoItems.Add(new EmployeeToDoItem()
                    {
                        CompanyID = register.Location.CompanyID,
                        CompanyName = register.Location.Company.Name,
                        LocationID = register.LocationID,
                        LocationName = register.Location.Name,
                        SourceID = register.RegisterID,
                        Type = Types.RegisterOffline,
                        Message = $"Register {register.Name} is reporting offline",
                        MesaSuiteURI = $"mesasuite://company/register/{register.RegisterID}?companyid={register.Location.CompanyID}&locationid={register.LocationID}",
                        Severity = Severities.Important
                    });
                }
            }

            return toDoItems;
        }
    }
}