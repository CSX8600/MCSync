using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Loader;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using WebModels.account;
using WebModels.company;
using WebModels.fleet;
using WebModels.mesasys;

namespace WebModels.Loaders.mesasys
{
    public class EmailTemplateLoader : ILoader
    {
        public IEnumerable<LoaderObject> GetLoaderObjects()
        {
            List<LoaderObject> loaderObjects = new List<LoaderObject>();
            loaderObjects.AddRange(GetInvoicingEmails());
            loaderObjects.AddRange(GetFleetTrackingEmails());
            loaderObjects.AddRange(GetStoreEmails());

            return loaderObjects;
        }

        private IEnumerable<LoaderObject> GetInvoicingEmails()
        {
            yield return new EmailTemplateLoaderObject<WireTransferHistory>(EmailTemplate.EmailTemplates.WireTransferReceived,
                                                                            "Wire Transfer Received",
                                                                            "A Wire Transfer has been sent to you from {GovernmentFrom.Name}{CompanyFrom.Name} at {TransferTime}. The amount of MBD${Amount:N2} has been deposited into your account of {AccountToHistorical}.\r\n\r\nMemo:\r\n{Memo}",
                                                                            EmailTemplate.SecurityCheckTypes.WireTransferHistory,
                                                                            wth => new List<object>()
                                                                            {
                                                                                wth.GovernmentFrom.Name,
                                                                                wth.CompanyFrom.Name,
                                                                                wth.GovernmentTo.Name,
                                                                                wth.CompanyTo.Name,
                                                                                wth.TransferTime,
                                                                                wth.AccountFromMasked,
                                                                                wth.AccountToHistorical,
                                                                                wth.AccountToMasked,
                                                                                wth.Amount,
                                                                                wth.Memo
                                                                            });

            yield return new EmailTemplateLoaderObject<invoicing.Invoice>(EmailTemplate.EmailTemplates.AccountsPayableInvoiceReceived,
                                                                          "Payable Invoice Received",
                                                                          "Invoice Number {InvoiceNumber} has been issued to you by {GovernmentFrom.Name}{LocationFrom.Company.Name} for the amount of MBD${Amount:N2}.\r\n\r\n" +
                                                                            "Invoice Date: {InvoiceDate:MM/dd/yyyy}\r\n" +
                                                                            "Description: {Description}\r\n" +
                                                                            "Due Date: {DueDate:MM/dd/yyyy}",
                                                                          EmailTemplate.SecurityCheckTypes.Invoicing,
                                                                          invoice => new List<object>()
                                                                          {
                                                                              invoice.GovernmentFrom.Name,
                                                                              invoice.LocationFrom.Name,
                                                                              invoice.LocationFrom.Company.Name,
                                                                              invoice.GovernmentTo.Name,
                                                                              invoice.LocationTo.Name,
                                                                              invoice.LocationTo.Company.Name,
                                                                              invoice.InvoiceNumber,
                                                                              invoice.Description,
                                                                              invoice.InvoiceDate,
                                                                              invoice.DueDate,
                                                                              invoice.Status,
                                                                              invoice.Amount
                                                                          });

            yield return new EmailTemplateLoaderObject<invoicing.Invoice>(EmailTemplate.EmailTemplates.AccountsReceivableInvoiceReadyForReceipt,
                                                                          "Receivable Invoice Ready For Receipt",
                                                                          "Your Invoice {InvoiceNumber} to {GovernmentTo.Name}{LocationTo.Company.Name} has been authorized for payment and is ready for receipt.",
                                                                          EmailTemplate.SecurityCheckTypes.Invoicing,
                                                                          invoice => new List<object>()
                                                                          {
                                                                              invoice.GovernmentFrom.Name,
                                                                              invoice.LocationFrom.Name,
                                                                              invoice.LocationFrom.Company.Name,
                                                                              invoice.GovernmentTo.Name,
                                                                              invoice.LocationTo.Name,
                                                                              invoice.LocationTo.Company.Name,
                                                                              invoice.InvoiceNumber,
                                                                              invoice.Description,
                                                                              invoice.InvoiceDate,
                                                                              invoice.DueDate,
                                                                              invoice.Status,
                                                                              invoice.AccountTo.AccountNumber,
                                                                              invoice.AccountTo.Description,
                                                                              invoice.Amount
                                                                          });
        }

        private IEnumerable<LoaderObject> GetFleetTrackingEmails()
        {
            yield return new EmailTemplateLoaderObject<Railcar>(EmailTemplate.EmailTemplates.RailcarReleasedReceived,
                                                                "Railcar Release Received",
                                                                "Railcar {ReportingMark}{ReportingNumber} has been released to {CompanyPossessor.Name}{GovernmentPossessor.Name}. It has been set out to {RailLocation.Track.Name}{RailLocation.Train.TrainSymbol.Name}.",
                                                                EmailTemplate.SecurityCheckTypes.FleetTracking,
                                                                r => new List<object>()
                                                                {
                                                                    r.ReportingMark,
                                                                    r.ReportingNumber,
                                                                    r.CompanyPossessor.Name,
                                                                    r.GovernmentPossessor.Name,
                                                                    r.TrackDestination.Name,
                                                                    r.RailLocation.Position,
                                                                    r.RailLocation.Track.Name,
                                                                    r.RailLocation.Train.TimeOnDuty,
                                                                    r.RailLocation.Train.TrainSymbol.Name
                                                                });

            yield return new EmailTemplateLoaderObject<Locomotive>(EmailTemplate.EmailTemplates.LocomotiveReleasedReceived,
                                                                "Locomotive Release Received",
                                                                "Locomotive {ReportingMark}{ReportingNumber} has been released to {CompanyPossessor.Name}{GovernmentPossessor.Name}. It has been set out to {RailLocation.Track.Name}{RailLocation.Train.TrainSymbol.Name}.",
                                                                EmailTemplate.SecurityCheckTypes.FleetTracking,
                                                                l => new List<object>()
                                                                {
                                                                    l.ReportingMark,
                                                                    l.ReportingNumber,
                                                                    l.CompanyPossessor.Name,
                                                                    l.GovernmentPossessor.Name,
                                                                    l.RailLocation.Position,
                                                                    l.RailLocation.Track.Name,
                                                                    l.RailLocation.Train.TimeOnDuty,
                                                                    l.RailLocation.Train.TrainSymbol.Name
                                                                });

            yield return new EmailTemplateLoaderObject<LeaseRequest>(EmailTemplate.EmailTemplates.NewLeaseRequestAvailable,
                                                                    "New Lease Request Available",
                                                                    "A new Lease Request has been made by {CompanyRequester.Name}{GovernmentRequester.Name} for a {LeaseType}.\r\n\r\nPurpose: {Purpose}\r\n\r\nBidding Ends: {BidEndTime}",
                                                                    EmailTemplate.SecurityCheckTypes.FleetTracking,
                                                                    lr => new List<object>()
                                                                    {
                                                                        lr.LeaseRequestID,
                                                                        lr.CompanyRequester.Name,
                                                                        lr.GovernmentRequester.Name,
                                                                        lr.LeaseType,
                                                                        lr.RailcarType,
                                                                        lr.TrackDeliveryLocation.Name,
                                                                        lr.Purpose,
                                                                        lr.BidEndTime
                                                                    });

            yield return new EmailTemplateLoaderObject<LeaseBid>(EmailTemplate.EmailTemplates.LeaseBidReceived,
                                                                "Lease Bid Received",
                                                                "{Railcar.CompanyOwner.Name}{Railcar.GovernmentOwner.Name}{Locomotive.CompanyOwner.Name}{Locomotive.GovernmentOwner.Name} has submitted a bid for your Lease Request number {LeaseRequestID}.\r\n\r\n" +
                                                                    "Bid Amount: {LeaseAmount}\r\n" +
                                                                    "Recurring? {RecurringAmountType}\r\n" +
                                                                    "Recurring Amount: {RecurringAmount}\r\n" +
                                                                    "Terms: {Terms}",
                                                                EmailTemplate.SecurityCheckTypes.FleetTracking,
                                                                lb => new List<object>()
                                                                {
                                                                    lb.LeaseRequestID,
                                                                    lb.LeaseAmount,
                                                                    lb.RecurringAmountType,
                                                                    lb.RecurringAmount,
                                                                    lb.Terms,
                                                                    lb.LeaseRequest.TrackDeliveryLocation.Name,
                                                                    lb.LeaseRequest.LeaseType,
                                                                    lb.LeaseRequest.RailcarType,
                                                                    lb.LeaseRequest.Purpose,
                                                                    lb.LeaseRequest.BidEndTime,
                                                                    lb.Railcar.ReportingMark,
                                                                    lb.Railcar.ReportingNumber,
                                                                    lb.Railcar.RailcarModel.Name,
                                                                    lb.Railcar.RailcarModel.CargoCapacity,
                                                                    lb.Railcar.RailcarModel.Length,
                                                                    lb.Railcar.CompanyOwner.Name,
                                                                    lb.Railcar.GovernmentOwner.Name,
                                                                    lb.Railcar.RailLocation.Track.Name,
                                                                    lb.Railcar.RailLocation.Train.TrainSymbol.Name,
                                                                    lb.Railcar.RailLocation.Train.TimeOnDuty,
                                                                    lb.Locomotive.ReportingMark,
                                                                    lb.Locomotive.ReportingNumber,
                                                                    lb.Locomotive.LocomotiveModel.Name,
                                                                    lb.Locomotive.LocomotiveModel.IsSteamPowered,
                                                                    lb.Locomotive.LocomotiveModel.WaterCapacity,
                                                                    lb.Locomotive.LocomotiveModel.FuelCapacity,
                                                                    lb.Locomotive.LocomotiveModel.Length,
                                                                    lb.Locomotive.CompanyOwner.Name,
                                                                    lb.Locomotive.GovernmentOwner.Name,
                                                                    lb.Locomotive.RailLocation.Track.Name,
                                                                    lb.Locomotive.RailLocation.Train.TrainSymbol.Name,
                                                                    lb.Locomotive.RailLocation.Train.TimeOnDuty
                                                                });
        }

        private IEnumerable<LoaderObject> GetStoreEmails()
        {
            yield return new EmailTemplateLoaderObject<RegisterStatus>(EmailTemplate.EmailTemplates.RegisterOffline,
                "Register Offline",
                "The register {Register.Name} at {Register.Location.Company.Name} - {Register.Location.Name} has stopped working unexpectedly.\r\n\r\nReported Status: {Status}\r\nInitiated By: {Initiator}",
                EmailTemplate.SecurityCheckTypes.StoreRegister,
                rs => new List<object>()
                {
                    rs.ChangeTime,
                    rs.Status,
                    rs.Initiator,
                    rs.Register.Name,
                    rs.Register.Location.Name,
                    rs.Register.Location.Company.Name
                });
        }

        private class EmailTemplateLoaderObject<TTemplateObject> : LoaderObject where TTemplateObject : DataObject
        {
            public EmailTemplateLoaderObject(Guid systemID, string name, string template, EmailTemplate.SecurityCheckTypes securityCheckType, Expression<Func<TTemplateObject, object>> allowedFields) : base(typeof(EmailTemplate))
            {
                SystemID = systemID;
                Values = new Dictionary<string, object>()
                {
                    { nameof(EmailTemplate.TemplateSchema), Schema.GetSchemaObject<TTemplateObject>().SchemaName },
                    { nameof(EmailTemplate.TemplateObject), Schema.GetSchemaObject<TTemplateObject>().ObjectName },
                    { nameof(EmailTemplate.Name), name },
                    { nameof(EmailTemplate.Template), template },
                    { nameof(EmailTemplate.SecurityCheckType), securityCheckType },
                    { nameof(EmailTemplate.AllowedFields), FieldPathUtility.CreateFieldPaths(allowedFields) }
                };
            }
        }

        
    }
}
