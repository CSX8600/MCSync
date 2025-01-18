using CompanyStudio.Wizard;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.Fulfillment
{
    public partial class ReviewStepControl : UserControl, IWizardStep<FulfillmentWizardData>
    {
        public ReviewStepControl()
        {
            InitializeComponent();
        }

        public string NavigationName => "Review";

        public Control Control => this;

        public Task Commit(FulfillmentWizardData data)
        {
            return Task.CompletedTask;
        }

        Task IWizardStep<FulfillmentWizardData>.Load(FulfillmentWizardData data)
        {
            StringBuilder reviewBuilder = new StringBuilder("Railcars Loaded:");
            reviewBuilder.AppendLine();
            foreach(IGrouping<string, Models.Fulfillment> fulfillmentsByCar in data.Fulfillments.Where(f => f.Quantity != null && f.Quantity > 0).GroupBy(f => f.Railcar?.ReportingID))
            {
                string productsLoaded = string.Join(", ", fulfillmentsByCar.Select(f => $"{f.Quantity}x {f.PurchaseOrderLine.DisplayStringNoQuantity}").ToArray());

                reviewBuilder.AppendLine($"{fulfillmentsByCar.Key ?? "[No Railcar Assigned]"} - {productsLoaded}");
            }

            reviewBuilder.AppendLine();
            reviewBuilder.AppendLine("Additional Tasks:");
            reviewBuilder.Append(data.ReleaseCars ? "- Will " : "- Will not ");
            reviewBuilder.AppendLine("release cars, if applicable.");

            txtReview.Text = reviewBuilder.ToString();

            return Task.CompletedTask;
        }

        Task<List<string>> IWizardStep<FulfillmentWizardData>.Validate()
        {
            return Task.FromResult(new List<string>());
        }
    }
}
