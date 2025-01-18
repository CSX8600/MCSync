using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebModels.company;

namespace API_Company.App_Code
{
    public class EmployeeCache
    {
        private static Dictionary<long, Dictionary<long, CachedEmployee>> _employeesByUserIDByCompanyID = new Dictionary<long, Dictionary<long, CachedEmployee>>();
        private static Dictionary<long, Dictionary<string, CachedEmployee>> _employeesByUsernameByCompanyID = new Dictionary<long, Dictionary<string, CachedEmployee>>();

        public async static Task<CachedEmployee> GetCachedEmployee(long companyID, long userID)
        {
            if (!_employeesByUserIDByCompanyID.ContainsKey(companyID))
            {
                _employeesByUserIDByCompanyID[companyID] = new Dictionary<long, CachedEmployee>();
            }

            if (!_employeesByUserIDByCompanyID[companyID].ContainsKey(userID) || _employeesByUserIDByCompanyID[companyID][userID].CacheExpiration <= DateTime.Now)
            {
                Search<Employee> employeeSearch = new Search<Employee>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<Employee>()
                    {
                        Field = "UserID",
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = userID
                    },
                    new LongSearchCondition<Employee>()
                    {
                        Field = "CompanyID",
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = companyID
                    }));
                await RefreshCacheForEmployee(employeeSearch);
            }

            return _employeesByUserIDByCompanyID[companyID].GetOrDefault(userID);
        }

        public async static Task<CachedEmployee> GetCachedEmployee(long companyID, string username)
        {
            if (!_employeesByUsernameByCompanyID.ContainsKey(companyID))
            {
                _employeesByUsernameByCompanyID.Add(companyID, new Dictionary<string, CachedEmployee>());
            }

            if (_employeesByUsernameByCompanyID[companyID].ContainsKey(username) || _employeesByUsernameByCompanyID[companyID][username].CacheExpiration <= DateTime.Now)
            {
                Search<Employee> employeeSearch = new Search<Employee>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new StringSearchCondition<Employee>()
                    {
                        Field = $"{nameof(Employee.User)}.{nameof(WebModels.security.User.Username)}",
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = username
                    },
                    new LongSearchCondition<Employee>()
                    {
                        Field = "CompanyID",
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = companyID
                    }));
                await RefreshCacheForEmployee(employeeSearch);
            }

            return _employeesByUsernameByCompanyID[companyID].GetOrDefault(username);
        }

        private static async Task RefreshCacheForEmployee(Search<Employee> employeeSearch)
        {
            List<string> fields = FieldPathUtility.CreateFieldPathsAsList<Employee>(e => new List<object>()
            {
                e.EmployeeID,
                e.UserID,
                e.CompanyID,
                e.LocationEmployees.First().LocationID,
                e.User.Username
            });
            fields.AddRange(Employee.GetPermissionFieldNames());

            Employee employee = await Task.Run(() => employeeSearch.GetReadOnly(null, fields));

            if (employee == null)
            {
                return;
            }

            CachedEmployee cachedEmployee = new CachedEmployee()
            {
                CacheExpiration = DateTime.Now.AddSeconds(30),
                UserID = employee?.UserID.Value ?? -1,
                CompanyID = employee?.CompanyID.Value ?? -1,
                Username = employee?.User.Username
            };

            cachedEmployee.EmployeeID = employee.EmployeeID.Value;

            SchemaObject employeeSchemaObject = Schema.GetSchemaObject<Employee>();
            foreach (string permissionField in Employee.GetPermissionFieldNames())
            {
                Field field = employeeSchemaObject.GetField(permissionField);

                if (permissionField.StartsWith(nameof(Employee.LocationEmployees)))
                {
                    string permission = permissionField.Replace($"{nameof(Employee.LocationEmployees)}.", "");
                    foreach (LocationEmployee locationEmployee in employee.LocationEmployees)
                    {
                        if ((bool)field.GetValue(locationEmployee))
                        {
                            if (!cachedEmployee.PermissionsByLocationID.ContainsKey(locationEmployee.LocationID.Value))
                            {
                                cachedEmployee.PermissionsByLocationID[locationEmployee.LocationID.Value] = new HashSet<string>();
                            }

                            cachedEmployee.PermissionsByLocationID[locationEmployee.LocationID.Value].Add(permission);
                        }
                    }
                }
                else
                {
                    DataObject objectToEvaluate = employee;
                    if (permissionField.StartsWith(nameof(Employee.FleetSecurity)))
                    {
                        objectToEvaluate = employee.FleetSecurity;
                    }

                    if ((bool)field.GetValue(objectToEvaluate))
                    {
                        cachedEmployee.Permissions.Add(permissionField);
                    }
                }
            }

            if (!_employeesByUserIDByCompanyID.ContainsKey(employee.CompanyID.Value))
            {
                _employeesByUserIDByCompanyID[employee.CompanyID.Value] = new Dictionary<long, CachedEmployee>();
            }

            if (!_employeesByUsernameByCompanyID.ContainsKey(employee.CompanyID.Value))
            {
                _employeesByUsernameByCompanyID[employee.CompanyID.Value] = new Dictionary<string, CachedEmployee>();
            }

            _employeesByUserIDByCompanyID[employee.CompanyID.Value][employee.UserID.Value] = cachedEmployee;
            _employeesByUsernameByCompanyID[employee.CompanyID.Value][employee.User.Username] = cachedEmployee;
        }

        public class CachedEmployee
        {
            public long CompanyID { get; set; }
            public long EmployeeID { get; set; }
            public long UserID { get; set; }
            public string Username { get; set; }
            public DateTime CacheExpiration { get; set; }
            public HashSet<string> Permissions { get; set; } = new HashSet<string>();
            public Dictionary<long, HashSet<string>> PermissionsByLocationID { get; set; } = new Dictionary<long, HashSet<string>>();
        }
    }
}