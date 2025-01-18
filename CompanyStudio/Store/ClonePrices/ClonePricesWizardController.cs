using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Wizard;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Store.ClonePrices
{
    public class ClonePricesWizardController : WizardController<ClonePricesWizardData>
    {
        private long? _locationID;
        public ClonePricesWizardController(long? locationID)
        {
            _locationID = locationID;
            WizardStarting += ClonePricesWizardController_WizardStarting;
        }

        private void ClonePricesWizardController_WizardStarting(object sender, EventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == _locationID && !e.Value)
            {
                if (!PermissionsManager.HasPermission(_locationID.Value, PermissionsManager.LocationWidePermissions.ManagePrices) &&
                    !PermissionsManager.HasPermission(_locationID.Value, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders))
                {
                    ForceCloseWizard();
                    PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
                }
            }
        }

        protected override string WindowTitle => "Import/Export Price Wizard";

        protected override Image Logo => Properties.Resources.money_add;

        protected override string ScreenTitle => "Import/Export Prices";

        protected override bool UseNavigation => false;

        protected override string RunButtonCaption => "Run";

        protected override MultiMap<IWizardStep<ClonePricesWizardData>, StepConnection> GetConnections()
        {
            MultiMap<IWizardStep<ClonePricesWizardData>, StepConnection> connections = new MultiMap<IWizardStep<ClonePricesWizardData>, StepConnection>();

            WelcomeStep welcome = new WelcomeStep();
            OriginStep originStep = new OriginStep(_locationID);
            connections.Add(welcome, new StepConnection(originStep));

            DestinationsStep destinationsStep = new DestinationsStep();
            connections.Add(originStep, new StepConnection(destinationsStep));

            Func<ClonePricesWizardData, bool> hasAddItemsCondition = (data) =>
            {
                foreach ((long?, long?) companyIDLocationID in data.CompanyIDLocationIDDestinations)
                {
                    if (GetVerifyAddPopulateFunction()(companyIDLocationID, data).Any())
                    {
                        return true;
                    }
                }

                return false;
            };

            Func<ClonePricesWizardData, bool> hasUpdateItemsCondition = (data) =>
            {
                foreach ((long?, long?) companyIDLocationID in data.CompanyIDLocationIDDestinations)
                {
                    if (GetVerifyUpdatePopulateFunction()(companyIDLocationID, data).Any())
                    {
                        return true;
                    }
                }

                return false;
            };

            Func<ClonePricesWizardData, bool> hasDeleteItemsCondition = (data) =>
            {
                foreach ((long?, long?) companyIDLocationID in data.CompanyIDLocationIDDestinations)
                {
                    if (GetVerifyDeletePopulateFunction()(companyIDLocationID, data).Any())
                    {
                        return true;
                    }
                }

                return false;
            };

            VerifyItems verifyAdd = new VerifyItems(GetVerifyAddPopulateFunction(), "Add", (data) => data.AddedItemsByLocationID);
            VerifyItems verifyUpdate = new VerifyItems(GetVerifyUpdatePopulateFunction(), "Update", (data) => data.UpdatedItemsByLocationID);
            VerifyItems verifyDelete = new VerifyItems(GetVerifyDeletePopulateFunction(), "Delete", (data) => data.DeletedItemsByLocationID);
            ConfirmationStep confirmation = new ConfirmationStep();

            // destination
            connections.Add(destinationsStep, new StepConnection(verifyAdd)
            {
                Condition = hasAddItemsCondition
            });
            connections.Add(destinationsStep, new StepConnection(verifyUpdate)
            {
                Condition = hasUpdateItemsCondition 
            });
            connections.Add(destinationsStep, new StepConnection(verifyDelete)
            {
                Condition = hasDeleteItemsCondition
            });
            connections.Add(destinationsStep, new StepConnection(confirmation));

            // verify add
            connections.Add(verifyAdd, new StepConnection(verifyUpdate)
            {
                Condition = hasUpdateItemsCondition
            });
            connections.Add(verifyAdd, new StepConnection(verifyDelete)
            {
                Condition = hasDeleteItemsCondition
            });
            connections.Add(verifyAdd, new StepConnection(confirmation));

            // verify update
            connections.Add(verifyUpdate, new StepConnection(verifyDelete)
            {
                Condition = hasDeleteItemsCondition
            });
            connections.Add(verifyUpdate, new StepConnection(confirmation));

            // verify delete
            connections.Add(verifyDelete, new StepConnection(confirmation));

            connections.Add(confirmation, new StepConnection(new EndStep<ClonePricesWizardData>()));

            return connections;
        }

        private Func<(long?, long?), ClonePricesWizardData, List<LocationItem>> GetVerifyAddPopulateFunction() => (companyIDLocationID, data) =>
        {
            List<LocationItem> originItems = data.LocationItemsByLocationID[data.LocationIDOrigin];
            List<LocationItem> destinationItems = data.LocationItemsByLocationID[companyIDLocationID.Item2];
            return originItems.Except(originItems.Where(oi => destinationItems.Any(di => di.ItemID == oi.ItemID && di.Quantity == oi.Quantity))).ToList();
        };
        
        private Func<(long?, long?), ClonePricesWizardData, List<LocationItem>> GetVerifyUpdatePopulateFunction() => (companyIDLocationID, data) =>
        {
            List<LocationItem> originItems = data.LocationItemsByLocationID[data.LocationIDOrigin];
            List<LocationItem> destinationItems = data.LocationItemsByLocationID[companyIDLocationID.Item2];
            return originItems.Where(oi => destinationItems.Any(di => di.ItemID == oi.ItemID && di.Quantity == oi.Quantity && di.BasePrice != oi.BasePrice)).ToList();
        };

        private Func<(long?, long?), ClonePricesWizardData, List<LocationItem>> GetVerifyDeletePopulateFunction() => (companyIDLocationID, data) =>
        {
            List<LocationItem> originItems = data.LocationItemsByLocationID[data.LocationIDOrigin];
            List<LocationItem> destinationItems = data.LocationItemsByLocationID[companyIDLocationID.Item2];
            return destinationItems.Except(destinationItems.Where(di => originItems.Any(oi => oi.ItemID == di.ItemID && oi.Quantity == di.Quantity))).ToList();
        };

        protected override async Task WizardComplete(ClonePricesWizardData data)
        {
            foreach((long?, long?) companyIDLocationID in data.CompanyIDLocationIDDestinations)
            {
                List<LocationItem> originItems = data.LocationItemsByLocationID[data.LocationIDOrigin];
                List<LocationItem> destinationItems = data.LocationItemsByLocationID[companyIDLocationID.Item2];

                if (data.AddedItemsByLocationID.ContainsKey(companyIDLocationID.Item2))
                {
                    foreach (long? locationItemIDToAdd in data.AddedItemsByLocationID[companyIDLocationID.Item2])
                    {
                        LocationItem originItem = originItems.Single(oi => oi.LocationItemID == locationItemIDToAdd);
                        LocationItem newItem = new LocationItem()
                        {
                            LocationID = companyIDLocationID.Item2,
                            ItemID = originItem.ItemID,
                            Quantity = originItem.Quantity,
                            BasePrice = originItem.BasePrice
                        };
                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "LocationItem/Post", newItem);
                        post.AddLocationHeader(companyIDLocationID.Item1, companyIDLocationID.Item2);
                        await post.ExecuteNoResult();
                    }
                }

                if (data.UpdatedItemsByLocationID.ContainsKey(companyIDLocationID.Item2))
                {
                    foreach (long? locationItemIDToUpdate in data.UpdatedItemsByLocationID[companyIDLocationID.Item2])
                    {
                        LocationItem originItem = originItems.Single(oi => oi.LocationItemID == locationItemIDToUpdate);
                        LocationItem destinationItem = destinationItems.Single(di => di.ItemID == originItem.ItemID && di.Quantity == originItem.Quantity);

                        destinationItem.BasePrice = originItem.BasePrice;
                        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "LocationItem/Put", destinationItem);
                        put.AddLocationHeader(companyIDLocationID.Item1, companyIDLocationID.Item2);
                        await put.ExecuteNoResult();
                    }
                }

                if (data.DeletedItemsByLocationID.ContainsKey(companyIDLocationID.Item2))
                {
                    foreach (long? locationItemIDToDelete in data.DeletedItemsByLocationID[companyIDLocationID.Item2])
                    {
                        DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "LocationItem/Delete/" + locationItemIDToDelete);
                        delete.AddLocationHeader(companyIDLocationID.Item1, companyIDLocationID.Item2);
                        await delete.Execute();
                    }
                }
            }
        }

        public static async Task PerformDataLoading(ClonePricesWizardData data)
        {
            data.LocationItemsByLocationID.Clear();
            data.AddedItemsByLocationID.Clear();
            data.UpdatedItemsByLocationID.Clear();
            data.DeletedItemsByLocationID.Clear();  

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "LocationItem/GetAll");
            get.AddLocationHeader(data.CompanyIDOrigin, data.LocationIDOrigin);
            List<LocationItem> originItems = await get.GetObject<List<LocationItem>>() ?? new List<LocationItem>();
            data.LocationItemsByLocationID.Add(data.LocationIDOrigin, originItems);

            foreach((long?, long?) companyIDLocationID in data.CompanyIDLocationIDDestinations)
            {
                get.Headers["CompanyID"] = companyIDLocationID.Item1.ToString();
                get.Headers["LocationID"] = companyIDLocationID.Item2.ToString();
                List<LocationItem> destinationItems = await get.GetObject<List<LocationItem>>() ?? new List<LocationItem>();
                data.LocationItemsByLocationID.Add(companyIDLocationID.Item2, destinationItems);

                // Check adds
                if (data.DefaultAdd)
                {
                    foreach (LocationItem itemToAdd in originItems.Except(originItems.Where(oi => destinationItems.Any(di => di.ItemID == oi.ItemID && di.Quantity == oi.Quantity))))
                    {
                        data.AddedItemsByLocationID.GetOrSetDefault(companyIDLocationID.Item2, () => new List<long?>()).Add(itemToAdd.LocationItemID);
                    }
                }

                // Check updates
                if (data.DefaultUpdate)
                {
                    foreach (LocationItem itemToUpdate in originItems.Where(oi => destinationItems.Any(di => di.ItemID == oi.ItemID && di.Quantity == oi.Quantity && di.BasePrice != oi.BasePrice)))
                    {
                        data.UpdatedItemsByLocationID.GetOrSetDefault(companyIDLocationID.Item2, () => new List<long?>()).Add(itemToUpdate.LocationItemID);
                    }
                }

                // Check deletes
                if (data.DefaultDelete)
                {
                    foreach (LocationItem itemToDelete in destinationItems.Except(destinationItems.Where(di => originItems.Any(oi => oi.ItemID == di.ItemID && oi.Quantity == di.Quantity))))
                    {
                        data.DeletedItemsByLocationID.GetOrSetDefault(companyIDLocationID.Item2, () => new List<long?>()).Add(itemToDelete.LocationItemID);
                    }
                }
            }
        }
    }
}
