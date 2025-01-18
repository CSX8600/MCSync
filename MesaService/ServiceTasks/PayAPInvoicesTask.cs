using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.invoicing;

namespace MesaService.ServiceTasks
{
    internal class PayAPInvoicesTask : IServiceTask
    {
        public string Name => "Pay AP Invoices";

        private DateTime _nextRunTime = DateTime.Now;
        public DateTime NextRunTime => _nextRunTime;

        public bool Run()
        {
            if (DateTime.Now > _nextRunTime)
            {
                _nextRunTime = DateTime.Today.AddDays(1).AddHours(-1);
            }

            Errors saveErrors = new Errors();
            Search<AutomaticInvoicePaymentConfiguration> configurationSearch = new Search<AutomaticInvoicePaymentConfiguration>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new DecimalSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.RemainingAmount),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                    Value = 0
                },
                new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.AccountID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotNull
                }));

            List<string> fields = FieldPathUtility.CreateFieldPathsAsList<AutomaticInvoicePaymentConfiguration>(aipc => new object[]
            {
                aipc.Account.Balance
            });

            foreach (AutomaticInvoicePaymentConfiguration configuration in configurationSearch.GetEditableReader(readOnlyFields: fields))
            {
                SearchConditionGroup invoiceConditionGroup = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new IntSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.Status),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = (int)Invoice.Statuses.Sent
                    });

                if (configuration.LocationIDConfiguredFor != null)
                {
                    invoiceConditionGroup.SearchConditions.Add(new LongSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.LocationIDTo),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = configuration.LocationIDConfiguredFor
                    });
                }

                if (configuration.GovernmentIDConfiguredFor != null)
                {
                    invoiceConditionGroup.SearchConditions.Add(new LongSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.GovernmentIDTo),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = configuration.GovernmentIDConfiguredFor
                    });
                }

                if (configuration.GovernmentIDPayee != null)
                {
                    invoiceConditionGroup.SearchConditions.Add(new LongSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.GovernmentIDFrom),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = configuration.GovernmentIDPayee
                    });
                }

                if (configuration.LocationIDPayee != null)
                {
                    invoiceConditionGroup.SearchConditions.Add(new LongSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.LocationIDFrom),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = configuration.LocationIDPayee
                    });
                }

                if (configuration.Schedule == AutomaticInvoicePaymentConfiguration.Schedules.OnDueDate)
                {
                    invoiceConditionGroup.SearchConditions.Add(new DateTimeSearchCondition<Invoice>()
                    {
                        Field = nameof(Invoice.DueDate),
                        SearchConditionType = SearchCondition.SearchConditionTypes.LessEquals,
                        Value = DateTime.Today.AddDays(1).AddSeconds(-1)
                    });
                }

                Search<Invoice> payableInvoiceSearch = new Search<Invoice>(invoiceConditionGroup);
                decimal runningPaidTotal = 0;
                foreach (Invoice invoiceToPay in payableInvoiceSearch.GetEditableReader(readOnlyFields: new[] { nameof(Invoice.Amount) }))
                {
                    if (invoiceToPay.Amount > (configuration.MaxAmount - configuration.PaidAmount) || 
                        invoiceToPay.Amount > (configuration.Account.Balance - runningPaidTotal))
                    {
                        continue;
                    }

                    invoiceToPay.AccountIDFrom = configuration.AccountID;
                    invoiceToPay.Status = Invoice.Statuses.ReadyForReceipt;

                    if (!invoiceToPay.Save())
                    {
                        saveErrors.AddRange(invoiceToPay.Errors.ToArray());
                    }

                    configuration.PaidAmount += invoiceToPay.Amount;
                }

                if (!configuration.Save())
                {
                    saveErrors.AddRange(configuration.Errors.ToArray());
                }
            }

            if (saveErrors.Any())
            {
                throw new InvalidOperationException(saveErrors.ToString());
            }

            return true;
        }
    }
}
