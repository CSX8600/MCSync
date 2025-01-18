using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.Migrations
{
    public class Migration000029 : IMigration
    {
        public int MigrationNumber => 29;

        public void Execute(ITransaction transaction)
        {
            CreatePurchasingTables(transaction);
            CreateFleetTables(transaction);
            CreateInvoiceTables(transaction);
            UpdateTables(transaction);
            UpdateDefaultSecurity(transaction);
        }

        private void CreatePurchasingTables(ITransaction transaction)
        {
            ICreateSchema createSchema = SQLProviderFactory.GetCreateSchemaQuery();
            createSchema.SchemaName = "purchasing";
            createSchema.Execute(transaction);

            ICreateTable createTable = SQLProviderFactory.GetCreateTableQuery();
            createTable.SchemaName = "purchasing";
            createTable.TableName = "PurchaseOrder";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PurchaseOrderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationIDOrigin", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDOrigin", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "LocationIDDestination", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDDestination", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PurchaseOrderIDClonedFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PurchaseOrderDate", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "Status", new FieldSpecification(FieldSpecification.FieldTypes.Int) },
                { "Description", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 250) },
                { "InvoiceSchedule", new FieldSpecification(FieldSpecification.FieldTypes.Int) },
                { "AccountIDReceiving", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);
            IAlterTable alter = SQLProviderFactory.GetAlterTableQuery();
            alter.Schema = "purchasing";
            alter.Table = "PurchaseOrder";
            alter.AddForeignKey("FKPurchaseOrder_Location_LocationIDOrigin", "LocationIDOrigin", "company", "Location", "LocationID", transaction);
            alter.AddForeignKey("FKPurchaseOrder_Location_LocationIDDestination", "LocationIDDestination", "company", "Location", "LocationID", transaction);
            alter.AddForeignKey("FKPurchaseOrder_Government_GovernmentIDOrigin", "GovernmentIDOrigin", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKPurchaseOrder_Government_GovernmentIDDestination", "GovernmentIDDestination", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKPurchaseOrder_PurchaseOrder_PurchaseOrderIDClonedFrom", "PurchaseOrderIDClonedFrom", "purchasing", "PurchaseOrder", "PurchaseOrderID", transaction);
            alter.AddForeignKey("FKPurchaseOrder_Account_AccountIDReceiving", "AccountIDReceiving", "account", "Account", "AccountID", transaction);

            createTable.TableName = "PurchaseOrderApproval";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PurchaseOrderApprovalID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "PurchaseOrderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDApprover", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDApprover", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ApprovalStatus", new FieldSpecification(FieldSpecification.FieldTypes.Int) },
                { "ApprovalPurpose", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 200) },
                { "RejectionReason", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 200) },
                { "FutureAutoApprove", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false } }
            };
            createTable.Execute(transaction);
            alter.Table = "PurchaseOrderApproval";
            alter.AddForeignKey("FKPurchaseOrderApproval_PurchaseOrder_PurchaseOrderID", "PurchaseOrderID", "purchasing", "PurchaseOrder", "PurchaseOrderID", transaction);
            alter.AddForeignKey("FKPurchaseOrderApproval_Company_CompanyIDApprover", "CompanyIDApprover", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKPurchaseOrderApproval_Government_GovernmentIDApprover", "GovernmentIDApprover", "gov", "Government", "GovernmentID", transaction);

            createTable.TableName = "PurchaseOrderLine";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PurchaseOrderLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "PurchaseOrderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "IsService", new FieldSpecification(FieldSpecification.FieldTypes.Bit) },
                { "ServiceDescription", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 1000) },
                { "ItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ItemDescription", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 100) },
                { "Quantity", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "UnitCost", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) }
            };
            createTable.Execute(transaction);
            alter.Table = "PurchaseOrderLine";
            alter.AddForeignKey("FKPurchaseOrderLine_PurchaseOrder_PurchaseOrderID", "PurchaseOrderID", "purchasing", "PurchaseOrder", "PurchaseOrderID", transaction);
            alter.AddForeignKey("FKPurchaseOrderLine_Item_ItemID", "ItemID", "mesasys", "Item", "ItemID", transaction);

            createTable.TableName = "FulfillmentPlan";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "FulfillmentPlanID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "RailcarID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "LeaseRequestID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "TrackIDLoading", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "TrackIDDestination", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "TrackIDStrategicAfterLoad", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "TrackIDStrategicAfterDestination", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "TrackIDPostFulfillment", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);

            alter.Table = "FulfillmentPlan";
            alter.AddForeignKey("FKFulfillmentPlan_Railcar_RailcarID", "RailcarID", "fleet", "Railcar", "RailcarID", transaction);
            alter.AddForeignKey("FKFulfillmentPlan_LeaseRequest_LeaseRequestID", "LeaseRequestID", "fleet", "LeaseRequest", "LeaseRequestID", transaction);
            alter.AddForeignKey("FKFulfillmentPlan_Track_TrackIDLoading", "TrackIDLoading", "fleet", "Track", "TrackID", transaction);
            alter.AddForeignKey("FKFulfillmentPlan_Track_TrackIDDestination", "TrackIDDestination", "fleet", "Track", "TrackID", transaction);
            alter.AddForeignKey("FKFulfillmentPlan_Track_TrackIDStrategicDestination", "TrackIDStrategicAfterLoad", "fleet", "Track", "TrackID", transaction);
            alter.AddForeignKey("FKFulfillmentPlan_Track_TrackIDPostFulfillment", "TrackIDPostFulfillment", "fleet", "Track", "TrackID", transaction);

            createTable.TableName = "FulfillmentPlanPurchaseOrderLine";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "FulfillmentPlanPurchaseOrderLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "FulfillmentPlanID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PurchaseOrderLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);
            alter.Table = "FulfillmentPlanPurchaseOrderLine";
            alter.AddForeignKey("FKFulfillmentPlanPurchaseOrderLine_FulfillmentPlan_FulfillmentPlanID", "FulfillmentPlanID", "purchasing", "FulfillmentPlan", "FulfillmentPlanID", transaction);
            alter.AddForeignKey("FKFulfillmentPlanPurchaseOrderLine_PurchaseOrderLine_PurchaseOrderLineID", "PurchaseOrderLineID", "purchasing", "PurchaseOrderLine", "PurchaseOrderLineID", transaction);

            createTable.TableName = "FulfillmentPlanRoute";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "FulfillmentPlanRouteID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "FulfillmentPlanID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "SortOrder", new FieldSpecification(FieldSpecification.FieldTypes.TinyInt) },
                { "CompanyIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);

            alter.Table = "FulfillmentPlanRoute";
            alter.AddForeignKey("FKFulfillmentPlanRoute_FulfillmentPlan_FulfillmentPlanID", "FulfillmentPlanID", "purchasing", "FulfillmentPlan", "FulfillmentPlanID", transaction);
            alter.AddForeignKey("FKFulfillmentPlanRoute_Company_CompanyIDFrom", "CompanyIDFrom", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKFulfillmentPlanRoute_Company_CompanyIDTo", "CompanyIDTo", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKFulfillmentPlanRoute_Government_GovernmentIDFrom", "GovernmentIDFrom", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKFulfillmentPlanRoute_Government_GovernmentIDTo", "GovernmentIDTo", "gov", "Government", "GovernmentID", transaction);

            createTable.TableName = "Fulfillment";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "FulfillmentID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "PurchaseOrderLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "RailcarID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "FulfillmentTime", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "Quantity", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "IsComplete", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false } },
                { "InvoiceLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);
            alter.Table = "Fulfillment";
            alter.AddForeignKey("FKFulfillment_Railcar_RailcarID", "RailcarID", "fleet", "Railcar", "RailcarID", transaction);
            alter.AddForeignKey("FKFulfillment_PurchaseOrderLine_PurchaseOrderLineID", "PurchaseOrderLineID", "purchasing", "PurchaseOrderLine", "PurchaseOrderLineID", transaction);
            alter.AddForeignKey("FKFulfillment_InvoiceLine_InvoiceLineID", "InvoiceLineID", "invoicing", "InvoiceLine", "InvoiceLineID", transaction);

            createTable.TableName = "BillOfLading";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "BillOfLadingID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "PurchaseOrderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDShipper", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDShipper", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDConsignee", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDConsignee", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDCarrier", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDCarrier", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "RailcarID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "IssuedDate", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "DeliveredDate", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "Type", new FieldSpecification(FieldSpecification.FieldTypes.Int) }
            };
            createTable.Execute(transaction);
            alter.Table = "BillOfLading";
            alter.AddForeignKey("FKBillOfLading_PurchaseOrder_PurchaseOrderID", "PurchaseOrderID", "purchasing", "PurchaseOrder", "PurchaseOrderID", transaction);
            alter.AddForeignKey("FKBillOfLading_Company_CompanyIDShipper", "CompanyIDShipper", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKBillOfLading_Government_GovernmentIDShipper", "GovernmentIDShipper", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKBillOfLading_Company_CompanyIDConsignee", "CompanyIDConsignee", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKBillOfLading_Government_GovernmentIDConsignee", "GovernmentIDConsignee", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKBillOfLading_Company_CompanyIDCarrier", "CompanyIDCarrier", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKBillOfLading_Government_GovernmentIDCarrier", "GovernmentIDCarrier", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKBillOfLading_Railcar_RailcarID", "RailcarID", "fleet", "Railcar", "RailcarID", transaction);

            createTable.TableName = "BillOfLadingItem";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "BillOfLadingItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "BillOfLadingID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ItemDescription", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 100) },
                { "Quantity", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "UnitCost", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) }
            };
            createTable.Execute(transaction);
            alter.Table = "BillOfLadingItem";
            alter.AddForeignKey("FKBillOfLadingItem_BillOfLading_BillOfLadingID", "BillOfLadingID", "purchasing", "BillOfLading", "BillOfLadingID", transaction);
            alter.AddForeignKey("FKBillOfLadingItem_Item_ItemID", "ItemID", "mesasys", "Item", "ItemID", transaction);

            createTable.TableName = "QuotationRequest";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "QuotationRequestID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "CompanyIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Notes", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 4000) }
            };
            createTable.Execute(transaction);
            alter.Table = "QuotationRequest";
            alter.AddForeignKey("FKQuotationRequest_Company_CompanyIDFrom", "CompanyIDFrom", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKQuotationRequest_Company_CompanyIDTo", "CompanyIDTo", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKQuotationRequest_Government_GovernmentIDFrom", "GovernmentIDFrom", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKQuotationRequest_Government_GovernmentIDTo", "GovernmentIDTo", "gov", "Government", "GovernmentID", transaction);

            createTable.TableName = "QuotationRequestItem";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "QuotationRequestItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "QuotationRequestID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Quantity", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) }
            };
            createTable.Execute(transaction);
            alter.Table = "QuotationRequestItem";
            alter.AddForeignKey("FKQuotationRequestItem_QuotationRequest_QuotationRequestID", "QuotationRequestID", "purchasing", "QuotationRequest", "QuotationRequestID", transaction);
            alter.AddForeignKey("FKQuotationRequestItem_Item_ItemID", "ItemID", "mesasys", "Item", "ItemID", transaction);

            createTable.TableName = "Quotation";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "QuotationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "CompanyIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "IsRepeatable", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false } },
                { "ExpirationTime", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) }
            };
            createTable.Execute(transaction);
            alter.Table = "Quotation";
            alter.AddForeignKey("FKQuotation_Company_CompanyIDFrom", "CompanyIDFrom", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKQuotation_Company_CompanyIDTo", "CompanyIDTo", "company", "Company", "CompanyID", transaction);
            alter.AddForeignKey("FKQuotation_Government_GovernmentIDFrom", "GovernmentIDFrom", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKQuotation_Government_GovernmentIDTo", "GovernmentIDTo", "gov", "Government", "GovernmentID", transaction);

            createTable.TableName = "QuotationItem";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "QuotationItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "QuotationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "UnitCost", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "MinimumQuantity", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) }
            };
            createTable.Execute(transaction);
            alter.Table = "QuotationItem";
            alter.AddForeignKey("FKQuotationItem_Quotation_QuotationID", "QuotationID", "purchasing", "Quotation", "QuotationID", transaction);
            alter.AddForeignKey("FKQuotationItem_Item_ItemID", "ItemID", "mesasys", "Item", "ItemID", transaction);

            createTable.TableName = "PurchaseOrderTemplateFolder";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PurchaseOrderTemplateFolderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PurchaseOrderTemplateFolderIDParent", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Name", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 255) }
            };
            createTable.Execute(transaction);
            alter.Table = "PurchaseOrderTemplateFolder";
            alter.AddForeignKey("FKPurchaseOrderTemplateFolder_Location_LocationID", "LocationID", "company", "Location", "LocationID", transaction);
            alter.AddForeignKey("FKPurchaseOrderTemplateFolder_Government_GovernmentID", "GovernmentID", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKPurchaseOrderTemplateFolder_PurchaseOrderTemplateFolder_PurchaseOrderTemplateFolderIDParent", "PurchaseOrderTemplateFolderIDParent", "purchasing", "PurchaseOrderTemplateFolder", "PurchaseOrderTemplateFolderID", transaction);

            createTable.TableName = "PurchaseOrderTemplate";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PurchaseOrderTemplateID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PurchaseOrderTemplateFolderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PurchaseOrderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Name", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 255) }
            };
            createTable.Execute(transaction);
            alter.Table = "PurchaseOrderTemplate";
            alter.AddForeignKey("FKPurchaseOrderTemplate_Location_LocationID", "LocationID", "company", "Location", "LocationID", transaction);
            alter.AddForeignKey("FKPurchaseOrderTemplate_Government_GovernmentID", "GovernmentID", "gov", "Government", "GovernmentID", transaction);
            alter.AddForeignKey("FKPurchaseOrderTemplate_PurchaseOrderTemplateFolder_PurchaseOrderTemplateFolderID", "PurchaseOrderTemplateFolderID", "purchasing", "PurchaseOrderTemplateFolder", "PurchaseOrderTemplateFolderID", transaction);
            alter.AddForeignKey("FKPurchaseOrderTemplate_PurchaseOrder_PurchaseOrderID", "PurchaseOrderID", "purchasing", "PurchaseOrder", "PurchaseOrderID", transaction);
        }

        private void CreateFleetTables(ITransaction transaction)
        {
            ICreateTable createTable = SQLProviderFactory.GetCreateTableQuery();
            createTable.SchemaName = "fleet";
            createTable.TableName = "RailcarRoute";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "RailcarRouteID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "RailcarID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "SortOrder", new FieldSpecification(FieldSpecification.FieldTypes.TinyInt) },
                { "CompanyIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "CompanyIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDFrom", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDTo", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);

            IAlterTable alterTable = SQLProviderFactory.GetAlterTableQuery();
            alterTable.Schema = "fleet";
            alterTable.Table = "RailcarRoute";
            alterTable.AddForeignKey("FKRailcarRoute_Railcar_RailcarID", "RailcarID", "fleet", "Railcar", "RailcarID", transaction);
            alterTable.AddForeignKey("FKRailcarRoute_Company_CompanyIDFrom", "CompanyIDFrom", "company", "Company", "CompanyID", transaction);
            alterTable.AddForeignKey("FKRailcarRoute_Company_CompanyIDTo", "CompanyIDTo", "company", "Company", "CompanyID", transaction);
            alterTable.AddForeignKey("FKRailcarRoute_Government_GovernmentIDFrom", "GovernmentIDFrom", "gov", "Government", "GovernmentID", transaction);
            alterTable.AddForeignKey("FKRailcarRoute_Government_GovernmentIDTo", "GovernmentIDTo", "gov", "Government", "GovernmentID", transaction);
        }

        private void CreateInvoiceTables(ITransaction transaction)
        {
            ICreateTable createTable = SQLProviderFactory.GetCreateTableQuery();
            createTable.SchemaName = "invoicing";
            createTable.TableName = "AutomaticInvoicePaymentConfiguration";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "AutomaticInvoicePaymentConfigurationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationIDConfiguredFor", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDConfiguredFor", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "LocationIDPayee", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "GovernmentIDPayee", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PaidAmount", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "MaxAmount", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "Schedule", new FieldSpecification(FieldSpecification.FieldTypes.Int) },
                { "AccountID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) }
            };
            createTable.Execute(transaction);

            IAlterTable alterTable = SQLProviderFactory.GetAlterTableQuery();
            alterTable.Schema = "invoicing";
            alterTable.Table = "AutomaticInvoicePaymentConfiguration";
            alterTable.AddForeignKey("FKAutomaticInvoicePaymentConfiguration_Location_LocationIDConfiguredFor", "LocationIDConfiguredFor", "company", "Location", "LocationID", transaction);
            alterTable.AddForeignKey("FKAutomaticInvoicePaymentConfiguration_Government_GovernmentIDConfiguredFor", "GovernmentIDConfiguredFor", "gov", "Government", "GovernmentID", transaction);
            alterTable.AddForeignKey("FKAutomaticInvoicePaymentConfiguration_Location_LocationIDPayee", "LocationIDPayee", "company", "Location", "LocationID", transaction);
            alterTable.AddForeignKey("FKAutomaticInvoicePaymentConfiguration_Government_GovernmentIDPayee", "GovernmentIDPayee", "gov", "Government", "GovernmentID", transaction);
            alterTable.AddForeignKey("FKAutomaticInvoicePaymentConfiguration_Account_AccountID", "AccountID", "account", "Account", "AccountID", transaction);
        }

        private void UpdateTables(ITransaction transaction)
        {
            IAlterTable alterTable = SQLProviderFactory.GetAlterTableQuery();
            alterTable.Schema = "invoicing";
            alterTable.Table = "Invoice";
            alterTable.AddColumn("PurchaseOrderID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt), transaction);
            alterTable.AddColumn("AutoReceive", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false });
            alterTable.AddForeignKey("FKInvoice_PurchaseOrder_PurchaseOrderID", "PurchaseOrderID", "purchasing", "PurchaseOrder", "PurchaseOrderID", transaction);

            alterTable.Table = "InvoiceLine";
            alterTable.AddColumn("PurchaseOrderLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt), transaction);
            alterTable.AddForeignKey("FKInvoiceLine_PurchaseOrderLine_PurchaseOrderLineID", "PurchaseOrderLineID", "purchasing", "PurchaseOrderLine", "PurchaseOrderLineID", transaction);

            alterTable.Schema = "company";
            alterTable.Table = "LocationEmployee";
            alterTable.AddColumn("ManagePurchaseOrders", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false }, transaction);

            alterTable.Schema = "gov";
            alterTable.Table = "Official";
            alterTable.AddColumn("ManagePurchaseOrders", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false }, transaction);

            alterTable.Schema = "fleet";
            alterTable.Table = "RailcarLoad";
            alterTable.AddColumn("PurchaseOrderLineID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt));
            alterTable.AddForeignKey("FKRailcarLoad_PurchaseOrderLine_PurchaseOrderLineID", "PurchaseOrderLineID", "purchasing", "PurchaseOrderLine", "PurchaseOrderLineID", transaction);

            alterTable.Schema = "company";
            alterTable.Table = "LocationItem";
            alterTable.AlterColumn("Quantity", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2), transaction);
            alterTable.AddColumn("GovernmentID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt), transaction);
            alterTable.AddForeignKey("FKLocationItem_Government_GovernmentID", "GovernmentID", "gov", "Government", "GovernmentID", transaction);
        }

        private void UpdateDefaultSecurity(ITransaction transaction)
        {
            IUpdateQuery update = SQLProviderFactory.GetUpdateQuery();
            update.Table = new Table("company", "LocationEmployee");
            update.FieldValueList = new List<FieldValue>()
            {
                new FieldValue() { FieldName = "ManagePurchaseOrders", Value = true, FieldType = FieldSpecification.FieldTypes.Bit }
            };
            update.Condition = new Condition()
            {
                Left = (Field)"ManageInvoices",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new Literal(true)
            };
            update.Execute(transaction);

            update.Table = new Table("gov", "Official");
            update.Execute(transaction);
        }
    }
}
