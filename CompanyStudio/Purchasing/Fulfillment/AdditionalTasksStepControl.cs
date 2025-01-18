using CompanyStudio.Wizard;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.Fulfillment
{
    public partial class AdditionalTasksStepControl : UserControl, IWizardStep<FulfillmentWizardData>
    {
        public AdditionalTasksStepControl()
        {
            InitializeComponent();
        }

        public string NavigationName => "Additional Tasks";

        public Control Control => this;

        public Task Commit(FulfillmentWizardData data)
        {
            data.ReleaseCars = chkReleaseCars.Checked;
            return Task.CompletedTask;
        }

        Task IWizardStep<FulfillmentWizardData>.Load(FulfillmentWizardData data)
        {
            chkReleaseCars.Checked = data.ReleaseCars;
            return Task.CompletedTask;
        }

        Task<List<string>> IWizardStep<FulfillmentWizardData>.Validate()
        {
            return Task.FromResult(new List<string>());
        }
    }
}
