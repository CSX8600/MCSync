using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GovernmentPortal.Models;
using MesaSuite.Common.Data;

namespace GovernmentPortal
{
    public static class PermissionsManager
    {
        private static bool _requestStop;
        private static bool _isRunning;
        private static ConcurrentDictionary<long, Dictionary<Permissions, bool>> PermissionsByGovernment = new ConcurrentDictionary<long, Dictionary<Permissions, bool>>();
        private static ConcurrentDictionary<long, FleetTracking.Models.FleetSecurity> FleetSecuritiesByGovernment = new ConcurrentDictionary<long, FleetTracking.Models.FleetSecurity>();

        public static event EventHandler<PermissionChangeEventArgs> OnPermissionChange;

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
            PermissionsByGovernment = new ConcurrentDictionary<long, Dictionary<Permissions, bool>>();
            FleetSecuritiesByGovernment = new ConcurrentDictionary<long, FleetTracking.Models.FleetSecurity>();
            Dictionary<string, PropertyInfo> permissionPropertiesByName = new Dictionary<string, PropertyInfo>();
            Type officialType = typeof(Official);
            foreach (var item in Enum.GetValues(typeof(Permissions)))
            {
                PropertyInfo permissionProperty = officialType.GetProperty(item.ToString());
                permissionPropertiesByName.Add(item.ToString(), permissionProperty);
            }

            while (!_requestStop)
            {
                GetData getData = new GetData(DataAccess.APIs.GovernmentPortal, "Government/GetAllForUser");
                List<Government> governments = getData.GetObject<List<Government>>().Result;

                if (getData.RequestSuccessful)
                {
                    foreach (Government government in governments)
                    {
                        getData = new GetData(DataAccess.APIs.GovernmentPortal, "Official/GetForGovernment");
                        getData.Headers.Add("GovernmentID", government.GovernmentID.Value.ToString());
                        getData.QueryString.Add("id", government.GovernmentID.Value.ToString());

                        Official official = getData.GetObject<Official>().Result;
                        if (official == null)
                        {
                            continue;
                        }

                        FleetSecuritiesByGovernment[government.GovernmentID.Value] = official.FleetSecurity;

                        foreach (var item in Enum.GetValues(typeof(Permissions)))
                        {
                            PropertyInfo propertyInfo = permissionPropertiesByName[item.ToString()];

                            bool isNew = !PermissionsByGovernment.ContainsKey(government.GovernmentID.Value) || !PermissionsByGovernment[government.GovernmentID.Value].ContainsKey((Permissions)item);
                            bool previousValue = false;
                            if (!isNew)
                            {
                                previousValue = PermissionsByGovernment[government.GovernmentID.Value][(Permissions)item];
                            }

                            if (!PermissionsByGovernment.ContainsKey(government.GovernmentID.Value))
                            {
                                PermissionsByGovernment[government.GovernmentID.Value] = new Dictionary<Permissions, bool>();
                            }

                            PermissionsByGovernment[government.GovernmentID.Value][(Permissions)item] = (bool)propertyInfo.GetValue(official);

                            if (!isNew && previousValue != PermissionsByGovernment[government.GovernmentID.Value][(Permissions)item])
                            {
                                callback(() => OnPermissionChange?.Invoke(null, new PermissionChangeEventArgs(government.GovernmentID.Value, (Permissions)item, PermissionsByGovernment[government.GovernmentID.Value][(Permissions)item])));
                            }
                        }
                    }
                }

                Thread.Sleep(3000);
            }

            _requestStop = false;
            _isRunning = false;
        }

        public static bool HasPermission(long governmentID, Permissions permission)
        {
            if (!PermissionsByGovernment.ContainsKey(governmentID))
            {
                return false;
            }

            if (!PermissionsByGovernment[governmentID].ContainsKey(permission))
            {
                return false;
            }

            return PermissionsByGovernment[governmentID][permission];
        }

        public static bool HasFleetPermission(long governmentID, string permission)
        {
            if (!FleetSecuritiesByGovernment.ContainsKey(governmentID) || typeof(FleetTracking.Models.FleetSecurity).GetProperty(permission) == null)
            {
                return false;
            }

            FleetTracking.Models.FleetSecurity fleetSecurity = FleetSecuritiesByGovernment[governmentID];
            PropertyInfo permissionProp = typeof(FleetTracking.Models.FleetSecurity).GetProperty(permission);
            return (bool)permissionProp.GetValue(fleetSecurity);
        }

        public enum Permissions
        {
            ManageEmails,
            ManageOfficials,
            ManageAccounts,
            ManageTaxes,
            CanMintCurrency,
            ManageInvoices,
            IssueWireTransfers,
            CanConfigureInterest,
            ManageLaws,
            ManagePurchaseOrders
        }

        public class PermissionChangeEventArgs : EventArgs
        {
            public long GovernmentID { get; }
            public Permissions Permission { get; }
            public bool Value { get; }

            public PermissionChangeEventArgs(long governmentID, Permissions permission, bool value)
            {
                GovernmentID = governmentID;
                Permission = permission;
                Value = value;
            }
        }
    }
}
