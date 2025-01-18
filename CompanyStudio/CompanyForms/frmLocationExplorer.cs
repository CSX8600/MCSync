using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace CompanyStudio.CompanyForms
{
    public partial class frmLocationExplorer : BaseCompanyStudioContent
    {
        public static readonly List<string> LOCATION_FIELDS = new List<string>()
        {
            nameof(Models.Location.LocationID),
            nameof(Models.Location.CompanyID),
            nameof(Models.Location.Name),
            nameof(Models.Location.InvoiceNumberPrefix),
            nameof(Models.Location.NextInvoiceNumber),
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.LocationEmployeeID)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.LocationID)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.EmployeeID)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.ManageInvoices)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.ManageRegisters)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.ManageInventory)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.ManagePrices)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.ManagePurchaseOrders)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.Employee)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.Employee)}.{nameof(Employee.EmployeeID)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.Employee)}.{nameof(Employee.EmployeeName)}",
            $"{nameof(Models.Location.LocationEmployees)}.{nameof(LocationEmployee.Employee)}.{nameof(Employee.EmployeeName)}",
            $"{nameof(Models.Location.LocationGovernments)}.{nameof(LocationGovernment.LocationGovernmentID)}",
            $"{nameof(Models.Location.LocationGovernments)}.{nameof(LocationGovernment.GovernmentID)}",
            $"{nameof(Models.Location.LocationGovernments)}.{nameof(LocationGovernment.LocationID)}",
            $"{nameof(Models.Location.LocationGovernments)}.{nameof(LocationGovernment.PaySalesTax)}",
            $"{nameof(Models.Location.LocationGovernments)}.{nameof(LocationGovernment.Government)}.{nameof(Government.GovernmentID)}",
            $"{nameof(Models.Location.LocationGovernments)}.{nameof(LocationGovernment.Government)}.{nameof(Government.Name)}"
        };

        public frmLocationExplorer()
        {
            InitializeComponent();
        }

        private void frmLocationExplorer_Load(object sender, EventArgs e)
        {
            Text += " - " + Company.Name;
            PermissionsManager.OnCompanyPermissionChange += PermissionsManager_OnCompanyPermissionChange;

            RefreshTreeView();
        }

        private void PermissionsManager_OnCompanyPermissionChange(object sender, PermissionsManager.CompanyWidePermissionChangeEventArgs e)
        {
            if (e.CompanyID != Company.CompanyID || e.Permission != PermissionsManager.CompanyWidePermissions.ManageLocations)
            {
                return;
            }

            if (!e.Value)
            {
                MessageBox.Show($"You do not have access to Location Explorer for {Company.Name}", "No Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
        }

        private async void RefreshTreeView()
        {
            loader.BringToFront();
            loader.Visible = true;

            treLocations.Nodes.Clear();

            GetData getData = new GetData(DataAccess.APIs.CompanyStudio, "Location/GetAll");
            getData.AddCompanyHeader(Company.CompanyID);
            getData.RequestFields = LOCATION_FIELDS;
            List<Location> locations = await getData.GetObject<List<Location>>() ?? new List<Location>();

            foreach(Location location in locations)
            {
                TreeNode parentNode = new TreeNode(location.Name);
                parentNode.Tag = location;

                TreeNode employeeParentNode = new TreeNode("Employee Assignments");

                if (location.LocationEmployees != null)
                {
                    foreach (LocationEmployee locationEmployee in location.LocationEmployees)
                    {
                        TreeNode employeeNode = new TreeNode(locationEmployee.Employee.EmployeeName);
                        employeeNode.Tag = locationEmployee;

                        Type locationEmployeeType = typeof(LocationEmployee);
                        foreach (object locationPermissionObj in Enum.GetValues(typeof(PermissionsManager.LocationWidePermissions)))
                        {
                            PermissionsManager.LocationWidePermissions permission = (PermissionsManager.LocationWidePermissions)locationPermissionObj;
                            PropertyInfo permissionProperty = locationEmployeeType.GetProperty(permission.ToString());

                            TreeNode permissionNode = new TreeNode(permission.ToString().ToDisplayName() + " - " + permissionProperty.GetValue(locationEmployee).ToString());
                            permissionNode.Tag = permission;
                            employeeNode.Nodes.Add(permissionNode);
                        }

                        employeeParentNode.Nodes.Add(employeeNode);
                    }
                }

                parentNode.Nodes.Add(employeeParentNode);

                TreeNode governmentsParentNode = new TreeNode("Governmental Jurisdictions");
                if (location.LocationGovernments != null)
                {
                    foreach (LocationGovernment locationGovernment in location.LocationGovernments)
                    {
                        TreeNode governmentNode = new TreeNode(locationGovernment.Government.Name);
                        governmentNode.Tag = locationGovernment;

                        TreeNode paySalesTax = new TreeNode("Pay Sales Tax? " + locationGovernment.PaySalesTax.ToString());
                        paySalesTax.Tag = nameof(LocationGovernment.PaySalesTax);
                        governmentNode.Nodes.Add(paySalesTax);

                        governmentsParentNode.Nodes.Add(governmentNode);
                    }
                }

                parentNode.Nodes.Add(governmentsParentNode);

                treLocations.Nodes.Add(parentNode);
            }

            treLocations_AfterSelect(null, new TreeViewEventArgs(null));

            loader.Visible = false;
        }

        private void frmLocationExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange -= PermissionsManager_OnCompanyPermissionChange;
        }

        private void treLocations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treLocations.SelectedNode == null)
            {
                toolDeleteLocation.Enabled = false;
                toolUpdateEmployee.Enabled = false;
                toolUpdateGovernment.Enabled = false;
                return;
            }

            toolDeleteLocation.Enabled = true;
            toolUpdateEmployee.Enabled = true;
            toolUpdateGovernment.Enabled = true;
        }

        private void treLocations_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode locationNode = e.Node;
            while(locationNode.Parent != null)
            {
                locationNode = locationNode.Parent;
            }

            Location location = (Location)locationNode.Tag;

            frmLocation openLocation = Studio.dockPanel.Documents.OfType<frmLocation>().FirstOrDefault(l => l.LocationModel.LocationID == location.LocationID);
            if (openLocation != null)
            {
                openLocation.BringToFront();
                return;
            }

            openLocation = new frmLocation();
            openLocation.LocationModel = location;
            Studio.DecorateStudioContent(openLocation);
            openLocation.Company = Company;
            openLocation.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            openLocation.OnSave += Location_OnSave;
            openLocation.FormClosed += Location_FormClosed;
        }

        private void Location_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= Location_FormClosed;
            ((ISaveable)sender).OnSave -= Location_OnSave;
        }

        private void Location_OnSave(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void toolAddLocation_Click(object sender, EventArgs e)
        {
            frmLocation location = new frmLocation();
            Studio.DecorateStudioContent(location);
            location.Company = Company;
            location.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            location.OnSave += Location_OnSave;
            location.FormClosed += Location_FormClosed;
        }

        private async void toolDeleteLocation_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete this Location?"))
            {
                return;
            }

            TreeNode parentNode = treLocations.SelectedNode;
            while(parentNode.Parent != null)
            {
                parentNode = parentNode.Parent;
            }

            Location location = (Location)parentNode.Tag;

            loader.BringToFront();
            loader.Visible = true;

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "Location/Delete/" + location.LocationID);
            delete.AddCompanyHeader(Company.CompanyID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                RefreshTreeView();
            }

            loader.Visible = false;
        }

        private void toolUpdateEmployee_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = treLocations.SelectedNode;

            while(parentNode.Parent != null)
            {
                parentNode = parentNode.Parent;
            }

            Location location = (Location)parentNode.Tag;

            LocationEmployees locationEmployees = new LocationEmployees()
            {
                Company = Company,
                LocationModel = location
            };
            frmLocationChildManager childManager = new frmLocationChildManager(Theme, locationEmployees, locationEmployees.Save);
            childManager.Text = "Manage Employees";
            if (childManager.ShowDialog() == DialogResult.OK)
            {
                RefreshTreeView();
            }
        }

        private void toolUpdateGovernment_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = treLocations.SelectedNode;

            while (parentNode.Parent != null)
            {
                parentNode = parentNode.Parent;
            }

            Location location = (Location)parentNode.Tag;

            LocationGovernments locationGovernment = new LocationGovernments(Company, location);

            frmLocationChildManager childManager = new frmLocationChildManager(Theme, locationGovernment, locationGovernment.Save);
            childManager.Text = "Manage Jurisdictions";
            if (childManager.ShowDialog() == DialogResult.OK)
            {
                RefreshTreeView();
            }
        }
    }
}
