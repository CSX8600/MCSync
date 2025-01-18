using ClussPro.ObjectBasedFramework.DataSearch;
using System;
using WebModels.purchasing;

namespace MesaService.ServiceTasks
{
    internal class CleanupDatabaseTask : IServiceTask
    {
        public string Name => "Cleanup Database";

        private DateTime _nextRunTime = DateTime.Now;
        public DateTime NextRunTime => _nextRunTime;

        public bool Run()
        {
            _nextRunTime = DateTime.Today.AddDays(1).AddMinutes(-1);

            CleanupOrphanedFulfillmentPlans();

            return true;
        }

        private void CleanupOrphanedFulfillmentPlans()
        {
            Search<FulfillmentPlan> orphanedPlanSearch = new Search<FulfillmentPlan>(new ExistsSearchCondition<FulfillmentPlan>()
            {
                RelationshipName = nameof(FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                ExistsType = ExistsSearchCondition<FulfillmentPlan>.ExistsTypes.NotExists
            });

            foreach (FulfillmentPlan orphanedPlan in orphanedPlanSearch.GetEditableReader())
            {
                orphanedPlan.Delete();
            }
        }
    }
}
