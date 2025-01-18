using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyStudio.Models
{
    public class PurchaseOrder
    {
        public long? PurchaseOrderID { get; set; }
        public long? LocationIDOrigin { get; set; }
        public Location LocationOrigin { get; set; }
        public long? GovernmentIDOrigin { get; set; }
        public Government GovernmentOrigin { get; set; }
        public long? LocationIDDestination { get; set; }
        public Location LocationDestination { get; set; }
        public long? GovernmentIDDestination { get; set; }
        public Government GovernmentDestination { get; set; }

        public long? PurchaseOrderIDClonedFrom { get; set; }

        public string OriginString
        {
            get
            {
                if (GovernmentIDOrigin != null)
                {
                    return GovernmentOrigin?.Name;
                }
                else
                {
                    return $"{LocationOrigin?.Company?.Name} - ({LocationOrigin?.Name})";
                }
            }
        }

        public string DestinationString
        {
            get
            {
                if (GovernmentIDDestination != null)
                {
                    return GovernmentDestination?.Name;
                }
                else
                {
                    return $"{LocationDestination?.Company?.Name} - ({LocationDestination?.Name})";
                }
            }
        }

        public DateTime? PurchaseOrderDate { get; set; }
        public enum Statuses
        {
            Draft,
            Pending,
            Accepted,
            Rejected,
            InProgress,
            Completed
        }
        public Statuses Status { get; set; }
        public string Description { get; set; }
        public enum InvoiceSchedules
        {
            UponShipment,
            UponDelivery,
            Manual
        }
        public InvoiceSchedules InvoiceSchedule { get; set; }
        public long? AccountIDReceiving { get; set; }

        public List<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public List<PurchaseOrderApproval> PurchaseOrderApprovals { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<PurchaseOrder> PurchaseOrderClones { get; set; }
        public List<PurchaseOrderTemplate> PurchaseOrderTemplates { get; set; }

        public static readonly IReadOnlyCollection<Statuses> RECEIVED_STATUSES = new List<Statuses>()
        {
            Statuses.Pending,
            Statuses.Accepted,
            Statuses.InProgress,
            Statuses.Completed
        };

        public static readonly IReadOnlyCollection<Statuses> OPEN_STATUSES = new List<Statuses>()
        {
            Statuses.Accepted,
            Statuses.InProgress
        };

        public static BaseCompanyStudioContent OpenPurchaseOrderForm<T>(T parent, PurchaseOrder purchaseOrder = null) where T : BaseCompanyStudioContent, ILocationScoped
        {
            Statuses status = purchaseOrder?.Status ?? Statuses.Draft;

            switch (status)
            {
                case Statuses.Draft:
                    Purchasing.DraftEntry.frmPurchaseOrder draftEntry = new Purchasing.DraftEntry.frmPurchaseOrder();
                    parent.Studio.DecorateStudioContent(draftEntry);
                    draftEntry.Company = parent.Company;
                    draftEntry.LocationModel = parent.LocationModel;
                    draftEntry.PurchaseOrderID = purchaseOrder?.PurchaseOrderID;
                    draftEntry.Show(parent.Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                    return draftEntry;
                case Statuses.Pending:
                case Statuses.Rejected:
                    if (purchaseOrder.LocationIDOrigin == parent.LocationModel.LocationID)
                    {
                        Purchasing.ApprovalViewer.frmApprovalViewerSubmitter approvalViewerSubmitter = new Purchasing.ApprovalViewer.frmApprovalViewerSubmitter()
                        {
                            PurchaseOrderID = purchaseOrder.PurchaseOrderID
                        };
                        parent.Studio.DecorateStudioContent(approvalViewerSubmitter);
                        approvalViewerSubmitter.Company = parent.Company;
                        approvalViewerSubmitter.LocationModel = parent.LocationModel;
                        approvalViewerSubmitter.Show(parent.Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                        return approvalViewerSubmitter;
                    }
                    else if (purchaseOrder.LocationIDDestination == parent.LocationModel.LocationID || purchaseOrder.PurchaseOrderApprovals.Any(poa => poa.CompanyIDApprover == parent.Company.CompanyID))
                    {
                        Purchasing.ApprovalViewer.frmApprovalViewerApprover approvalViewerApprover = new Purchasing.ApprovalViewer.frmApprovalViewerApprover()
                        {
                            PurchaseOrderID = purchaseOrder.PurchaseOrderID
                        };
                        parent.Studio.DecorateStudioContent(approvalViewerApprover);
                        approvalViewerApprover.Company = parent.Company;
                        approvalViewerApprover.LocationModel = parent.LocationModel;
                        approvalViewerApprover.Show(parent.Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                        return approvalViewerApprover;
                    }
                    break;
                case Statuses.Accepted:
                case Statuses.InProgress:
                    if (purchaseOrder.LocationIDOrigin == parent.LocationModel.LocationID)
                    {
                        Purchasing.OpenMaintenance.frmOpenViewerSubmitter openMaintenanceSubmitter = new Purchasing.OpenMaintenance.frmOpenViewerSubmitter()
                        {
                            PurchaseOrderID = purchaseOrder.PurchaseOrderID
                        };
                        parent.Studio.DecorateStudioContent(openMaintenanceSubmitter);
                        openMaintenanceSubmitter.Company = parent.Company;
                        openMaintenanceSubmitter.LocationModel = parent.LocationModel;
                        openMaintenanceSubmitter.Show(parent.Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                        return openMaintenanceSubmitter;
                    }
                    else if (purchaseOrder.LocationIDDestination == parent.LocationModel.LocationID)
                    {
                        Purchasing.OpenMaintenance.frmOpenPurchaseOrderReceiver openMaintenanceReceiver = new Purchasing.OpenMaintenance.frmOpenPurchaseOrderReceiver()
                        {
                            PurchaseOrderID = purchaseOrder.PurchaseOrderID
                        };
                        parent.Studio.DecorateStudioContent(openMaintenanceReceiver);
                        openMaintenanceReceiver.Company = parent.Company;
                        openMaintenanceReceiver.LocationModel = parent.LocationModel;
                        openMaintenanceReceiver.Show(parent.Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                        return openMaintenanceReceiver;
                    }
                    break;
                case Statuses.Completed:
                    Purchasing.History.frmHistoricalPurchaseOrder historicalViewer = new Purchasing.History.frmHistoricalPurchaseOrder()
                    {
                        PurchaseOrderID = purchaseOrder.PurchaseOrderID
                    };
                    parent.Studio.DecorateStudioContent(historicalViewer);
                    historicalViewer.Company = parent.Company;
                    historicalViewer.LocationModel = parent.LocationModel;
                    historicalViewer.Show(parent.Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                    return historicalViewer;
            }

            return null;
        }
    }
}
