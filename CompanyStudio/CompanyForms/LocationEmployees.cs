using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace CompanyStudio.CompanyForms
{
    public partial class LocationEmployees : UserControl
    {
        private List<Employee> _employees = null;

        public Company Company { get; set; }
        public Location LocationModel { get; set; }

        public LocationEmployees()
        {
            InitializeComponent();
        }

        public LocationEmployees(Company company, Location location) : this()
        {
            Company = company;
            LocationModel = location;
        }

        private void LocationEmployees_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private async void RefreshGrid()
        {
            loader.BringToFront();
            loader.Visible = true;

            dgvEmployees.Rows.Clear();

            GetData getEmployees = new GetData(DataAccess.APIs.CompanyStudio, "Employee/GetAllForCompany");
            getEmployees.AddCompanyHeader(Company.CompanyID);
            _employees = await getEmployees.GetObject<List<Employee>>() ?? new List<Employee>();

            DataGridViewComboBoxColumn employeeColumn = (DataGridViewComboBoxColumn)dgvEmployees.Columns["colEmployee"];
            employeeColumn.DisplayMember = "EmployeeName";
            employeeColumn.ValueMember = "EmployeeID";
            employeeColumn.DataSource = _employees;

            if (LocationModel != null)
            {
                foreach (LocationEmployee locationEmployee in LocationModel.LocationEmployees)
                {
                    int rowIndex = dgvEmployees.Rows.Add();
                    DataGridViewRow row = dgvEmployees.Rows[rowIndex];
                    row.Cells["colLocationEmployeeID"].Value = locationEmployee.LocationEmployeeID;
                    DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)row.Cells["colEmployee"];
                    comboBoxCell.Value = locationEmployee.EmployeeID;
                    row.Cells["colManageInvoices"].Value = locationEmployee.ManageInvoices;
                    row.Cells[colManagePrices.Name].Value = locationEmployee.ManagePrices;
                    row.Cells[colManageRegisters.Name].Value = locationEmployee.ManageRegisters;
                    row.Cells[colManageInventory.Name].Value = locationEmployee.ManageInventory;
                    row.Cells[colManagePurchaseOrders.Name].Value = locationEmployee.ManagePurchaseOrders;
                }
            }

            loader.Visible = false;
        }

        public async Task<bool> Save()
        {
            if (!IsHandleCreated)
            {
                return true;
            }

            if (LocationModel == null)
            {
                throw new ArgumentNullException("LocationModel", "LocationModel is not set. Did you forget to set it while saving Location?");
            }

            bool success = true;
            List<LocationEmployee> locationEmployeesToDelete = new List<LocationEmployee>();
            List<LocationEmployee> locationEmployeesToSave = new List<LocationEmployee>();
            List<LocationEmployee> locationEmployeesToCreate = new List<LocationEmployee>();
            foreach(LocationEmployee locationEmployee in LocationModel.LocationEmployees)
            {
                DataGridViewRow existingRow = dgvEmployees.Rows.OfType<DataGridViewRow>().FirstOrDefault(dgv => dgv.Cells["colLocationEmployeeID"].Value is long id && id == locationEmployee.LocationEmployeeID);
                if (existingRow == null)
                {
                    locationEmployeesToDelete.Add(locationEmployee);
                    continue;
                }

                if (existingRow.Cells["colEmployee"].Value == null || existingRow.Cells["colEmployee"].Value is default(long))
                {
                    this.ShowError($"Employee is required on row {existingRow.Index}");
                    continue;
                }

                LocationEmployee savingLocationEmployee = new LocationEmployee()
                {
                    LocationEmployeeID = locationEmployee.LocationEmployeeID,
                    EmployeeID = (long)existingRow.Cells["colEmployee"].Value,
                    LocationID = locationEmployee.LocationID,
                    ManageInvoices = (bool)(existingRow.Cells["colManageInvoices"].Value ?? false),
                    ManagePrices = (bool)(existingRow.Cells[colManagePrices.Name].Value ?? false),
                    ManageRegisters = (bool)(existingRow.Cells[colManageRegisters.Name].Value ?? false),
                    ManageInventory = (bool)(existingRow.Cells[colManageInventory.Name].Value ?? false),
                    ManagePurchaseOrders = (bool)(existingRow.Cells[colManagePurchaseOrders.Name].Value ?? false)
                };
                locationEmployeesToSave.Add(savingLocationEmployee);
            }

            foreach(DataGridViewRow newRow in dgvEmployees.Rows.OfType<DataGridViewRow>().Where(dgvr => !dgvr.IsNewRow && (dgvr.Cells["colLocationEmployeeID"].Value == null || ((long)dgvr.Cells["colLocationEmployeeID"].Value) == default(long))))
            {
                if (newRow.Cells["colEmployee"].Value == null || newRow.Cells["colEmployee"].Value is default(long))
                {
                    this.ShowError($"Employee is required on row {newRow.Index}");
                    continue;
                }

                LocationEmployee newLocationEmployee = new LocationEmployee()
                {
                    LocationID = LocationModel.LocationID,
                    EmployeeID = (long)newRow.Cells["colEmployee"].Value,
                    ManageInvoices = (bool)(newRow.Cells["colManageInvoices"].Value ?? false),
                    ManagePrices = (bool)(newRow.Cells[colManagePrices.Name].Value ?? false),
                    ManageRegisters = (bool)(newRow.Cells[colManageRegisters.Name].Value ?? false),
                    ManageInventory = (bool)(newRow.Cells[colManageInventory.Name].Value ?? false),
                    ManagePurchaseOrders = (bool)(newRow.Cells[colManagePurchaseOrders.Name].Value ?? false)
                };

                locationEmployeesToCreate.Add(newLocationEmployee);
            }

            HashSet<long?> employeesInGrid = new HashSet<long?>();
            foreach(LocationEmployee locationEmployee in locationEmployeesToSave.Concat(locationEmployeesToCreate))
            {
                if (!employeesInGrid.Add(locationEmployee.EmployeeID) && !locationEmployeesToDelete.Any(le => le.EmployeeID == locationEmployee.EmployeeID))
                {
                    this.ShowError("Employees can only be specified once");
                    return false;
                }
            }

            foreach(LocationEmployee employeeToDelete in locationEmployeesToDelete)
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, $"LocationEmployee/Delete/{employeeToDelete.LocationEmployeeID}");
                delete.AddCompanyHeader(Company.CompanyID);
                await delete.Execute();
                success = success && delete.RequestSuccessful;
            }

            foreach(LocationEmployee employeeToSave in locationEmployeesToSave)
            {
                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "LocationEmployee/Put", employeeToSave);
                put.AddCompanyHeader(Company.CompanyID);
                await put.ExecuteNoResult();
                success = success && put.RequestSuccessful;
            }

            foreach(LocationEmployee employeeToCreate in locationEmployeesToCreate)
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "LocationEmployee/Post", employeeToCreate);
                post.AddCompanyHeader(Company.CompanyID);
                await post.ExecuteNoResult();
                success &= post.RequestSuccessful;
            }

            return success;
        }
    }
}
