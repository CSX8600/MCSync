using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;

namespace CompanyStudio
{
    public static class PermissionsManager
    {
        private static bool _requestStop;
        private static bool _isRunning;
        private static ConcurrentDictionary<long?, Dictionary<CompanyWidePermissions, bool>> CompanyWidePermissionsByCompany = new ConcurrentDictionary<long?, Dictionary<CompanyWidePermissions, bool>>();
        private static ConcurrentDictionary<long?, Dictionary<LocationWidePermissions, bool>> LocationWidePermissionsByLocation = new ConcurrentDictionary<long?, Dictionary<LocationWidePermissions, bool>>();
        private static ConcurrentDictionary<long?, FleetTracking.Models.FleetSecurity> FleetSecuritiesByCompany = new ConcurrentDictionary<long?, FleetTracking.Models.FleetSecurity>();

        public static event EventHandler<CompanyWidePermissionChangeEventArgs> OnCompanyPermissionChange;
        public static event EventHandler<LocationWidePermissionChangeEventArgs> OnLocationPermissionChange;

        public static void StartCheckThread(Action<Action> threadSafeCallback)
        {
            new Thread(new ParameterizedThreadStart(CheckThread)).Start(threadSafeCallback);
        }

        public static void StopCheckThread()
        {
            _requestStop = true;
        }

        private static void CheckThread(object threadSafeCallback)
        {
            Action<Action> callback = (Action<Action>)threadSafeCallback;
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;
            _requestStop = false;
            CompanyWidePermissionsByCompany = new ConcurrentDictionary<long?, Dictionary<CompanyWidePermissions, bool>>();
            Dictionary<string, PropertyInfo> companyPermissionPropertiesByName = new Dictionary<string, PropertyInfo>();
            Type employeeType = typeof(Employee);
            foreach (var item in Enum.GetValues(typeof(CompanyWidePermissions)))
            {
                PropertyInfo permissionProperty = employeeType.GetProperty(item.ToString());
                companyPermissionPropertiesByName.Add(item.ToString(), permissionProperty);
            }

            LocationWidePermissionsByLocation = new ConcurrentDictionary<long?, Dictionary<LocationWidePermissions, bool>>();
            Dictionary<string, PropertyInfo> locationPermissionPropertiesByName = new Dictionary<string, PropertyInfo>();
            Type locationEmployeeType = typeof(LocationEmployee);
            foreach (var item in Enum.GetValues(typeof(LocationWidePermissions)))
            {
                PropertyInfo permissionProperty = locationEmployeeType.GetProperty(item.ToString());
                locationPermissionPropertiesByName.Add(item.ToString(), permissionProperty);
            }

            while (!_requestStop)
            {
                GetData getData = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetForEmployee");
                List<Company> companies = getData.GetObject<List<Company>>().Result ?? new List<Company>();

                foreach (Company company in companies)
                {
                    getData = new GetData(DataAccess.APIs.CompanyStudio, "Employee/GetForCompany");
                    getData.Headers.Add("CompanyID", company.CompanyID.ToString());
                    getData.QueryString.Add("id", company.CompanyID.ToString());

                    Employee employee = getData.GetObject<Employee>().Result;
                    if (employee == null)
                    {
                        continue;
                    }

                    FleetSecuritiesByCompany[employee.CompanyID] = employee.FleetSecurity;

                    foreach (var item in Enum.GetValues(typeof(CompanyWidePermissions)))
                    {
                        PropertyInfo propertyInfo = companyPermissionPropertiesByName[item.ToString()];

                        bool isNew = !CompanyWidePermissionsByCompany.ContainsKey(company.CompanyID) || !CompanyWidePermissionsByCompany[company.CompanyID].ContainsKey((CompanyWidePermissions)item);
                        bool previousValue = false;
                        if (!isNew)
                        {
                            previousValue = CompanyWidePermissionsByCompany[company.CompanyID][(CompanyWidePermissions)item];
                        }

                        if (!CompanyWidePermissionsByCompany.ContainsKey(company.CompanyID))
                        {
                            CompanyWidePermissionsByCompany[company.CompanyID] = new Dictionary<CompanyWidePermissions, bool>();
                        }

                        CompanyWidePermissionsByCompany[company.CompanyID][(CompanyWidePermissions)item] = (bool)propertyInfo.GetValue(employee);

                        if (!isNew && previousValue != CompanyWidePermissionsByCompany[company.CompanyID][(CompanyWidePermissions)item])
                        {
                            callback(() => OnCompanyPermissionChange?.Invoke(null, new CompanyWidePermissionChangeEventArgs(company.CompanyID, (CompanyWidePermissions)item, CompanyWidePermissionsByCompany[company.CompanyID][(CompanyWidePermissions)item])));
                        }
                    }

                    foreach(Location location in company.Locations)
                    {
                        getData = new GetData(DataAccess.APIs.CompanyStudio, "LocationEmployee/GetForCurrentUser");
                        getData.AddCompanyHeader(company.CompanyID);
                        getData.QueryString.Add("locationID", location.LocationID.ToString());

                        LocationEmployee locationEmployee = getData.GetObject<LocationEmployee>().Result ?? new LocationEmployee();

                        foreach(var item in Enum.GetValues(typeof(LocationWidePermissions)))
                        {
                            PropertyInfo propertyInfo = locationPermissionPropertiesByName[item.ToString()];

                            bool isNew = !LocationWidePermissionsByLocation.ContainsKey(location.LocationID) || !LocationWidePermissionsByLocation[location.LocationID].ContainsKey((LocationWidePermissions)item);
                            bool previousValue = false;

                            if (!isNew)
                            {
                                previousValue = LocationWidePermissionsByLocation[location.LocationID][(LocationWidePermissions)item];
                            }

                            if (!LocationWidePermissionsByLocation.ContainsKey(location.LocationID))
                            {
                                LocationWidePermissionsByLocation[location.LocationID] = new Dictionary<LocationWidePermissions, bool>();
                            }

                            LocationWidePermissionsByLocation[location.LocationID][(LocationWidePermissions)item] = (bool)propertyInfo.GetValue(locationEmployee);

                            if (!isNew && previousValue != LocationWidePermissionsByLocation[location.LocationID][(LocationWidePermissions)item])
                            {
                                callback(() => OnLocationPermissionChange?.Invoke(null, new LocationWidePermissionChangeEventArgs(location.LocationID, (LocationWidePermissions)item, LocationWidePermissionsByLocation[location.LocationID][(LocationWidePermissions)item])));
                            }
                        }
                    }
                }

                foreach(long? missingCompanyID in CompanyWidePermissionsByCompany.Keys.Except(companies.Select(c => c.CompanyID)).ToList())
                {
                    foreach(var item in Enum.GetValues(typeof(CompanyWidePermissions)))
                    {
                        CompanyWidePermissions permission = (CompanyWidePermissions)item;
                        if (!CompanyWidePermissionsByCompany[missingCompanyID].ContainsKey(permission))
                        {
                            continue;
                        }

                        bool previousValue = CompanyWidePermissionsByCompany[missingCompanyID][permission];
                        if (previousValue)
                        {
                            CompanyWidePermissionsByCompany[missingCompanyID][permission] = false;
                            callback(() => OnCompanyPermissionChange?.Invoke(null, new CompanyWidePermissionChangeEventArgs(missingCompanyID, permission, false)));
                        }
                    }

                    CompanyWidePermissionsByCompany.TryRemove(missingCompanyID, out _);
                }

                foreach (long missingLocationID in LocationWidePermissionsByLocation.Keys.Except(companies.SelectMany(c => c.Locations).Select(c => c.LocationID)).ToList())
                {
                    foreach (var item in Enum.GetValues(typeof(LocationWidePermissions)))
                    {
                        LocationWidePermissions permission = (LocationWidePermissions)item;
                        if (!LocationWidePermissionsByLocation[missingLocationID].ContainsKey(permission))
                        {
                            continue;
                        }

                        bool previousValue = LocationWidePermissionsByLocation[missingLocationID][permission];
                        if (previousValue)
                        {
                            LocationWidePermissionsByLocation[missingLocationID][permission] = false;
                            callback(() => OnLocationPermissionChange?.Invoke(null, new LocationWidePermissionChangeEventArgs(missingLocationID, permission, false)));
                        }
                    }

                    LocationWidePermissionsByLocation.TryRemove(missingLocationID, out _);
                }

                Thread.Sleep(3000);
            }

            _requestStop = false;
            _isRunning = false;
        }

        public static bool HasPermission(long companyID, CompanyWidePermissions permission)
        {
            if (!CompanyWidePermissionsByCompany.ContainsKey(companyID))
            {
                return false;
            }

            if (!CompanyWidePermissionsByCompany[companyID].ContainsKey(permission))
            {
                return false;
            }

            return CompanyWidePermissionsByCompany[companyID][permission];
        }

        public static bool HasPermission(long locationID, LocationWidePermissions permission)
        {
            if (!LocationWidePermissionsByLocation.ContainsKey(locationID))
            {
                return false;
            }

            if (!LocationWidePermissionsByLocation[locationID].ContainsKey(permission))
            {
                return false;
            }

            return LocationWidePermissionsByLocation[locationID][permission];
        }

        public static bool HasPermissionFleetTrack(long companyID, string permission)
        {
            if (!FleetSecuritiesByCompany.ContainsKey(companyID) || typeof(FleetTracking.Models.FleetSecurity).GetProperty(permission) == null)
            {
                return false;
            }

            FleetTracking.Models.FleetSecurity fleetSecurity = FleetSecuritiesByCompany[companyID];
            PropertyInfo permissionProp = typeof(FleetTracking.Models.FleetSecurity).GetProperty(permission);
            return (bool)permissionProp.GetValue(fleetSecurity);
        }

        public static bool HasAccessToLocation(long locationID)
        {
            return LocationWidePermissionsByLocation.ContainsKey(locationID);
        }

        public static IReadOnlyCollection<long?> AccessibleLocationIDs => LocationWidePermissionsByLocation.Keys.ToList();

        public enum CompanyWidePermissions
        {
            ManageEmails,
            ManageEmployees,
            ManageAccounts,
            ManageLocations,
            IssueWireTransfers
        }

        public enum LocationWidePermissions
        {
            ManageInvoices,
            ManagePrices,
            ManageRegisters,
            ManageInventory,
            ManagePurchaseOrders
        }

        public class CompanyWidePermissionChangeEventArgs : EventArgs
        {
            public long? CompanyID { get; }
            public CompanyWidePermissions Permission { get; }
            public bool Value { get; }

            public CompanyWidePermissionChangeEventArgs(long? companyID, CompanyWidePermissions permission, bool value)
            {
                CompanyID = companyID;
                Permission = permission;
                Value = value;
            }
        }

        public class LocationWidePermissionChangeEventArgs : EventArgs
        {
            public long? LocationID { get; }
            public LocationWidePermissions Permission { get; }
            public bool Value { get; }

            public LocationWidePermissionChangeEventArgs(long? locationID, LocationWidePermissions permission, bool value)
            {
                LocationID = locationID;
                Permission = permission;
                Value = value;
            }
        }
    }
}
