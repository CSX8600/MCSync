using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetTracking.Attributes;
using FleetTracking.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace FleetTracking.Interop
{
    public class FleetTrackingApplication
    {
        private Thread _permissionsThread;
        private CancellationTokenSource _permissionsTokenSource;

        public static class CallbackDelegates
        {
            public delegate Form OpenForm(IFleetTrackingControl primaryControl, OpenFormOptions formOptions, IWin32Window parentWindow);
            public delegate TAccess GetAccess<out TAccess>() where TAccess : DataAccess;
            public delegate bool IsCurrentEntity(long? companyID, long? governmentID);
            public delegate (long?, long?) GetCurrentCompanyIDGovernmentID();
            public delegate Task<List<User>> GetUsersForEntity();
            public delegate bool SecurityPermissionCheck(string permission);
        }

        public FleetTrackingApplication(CallbackDelegates.SecurityPermissionCheck securityPermissionCheckCallback)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            RegisterCallback(securityPermissionCheckCallback);

            _permissionsThread = new Thread(new ParameterizedThreadStart(PermissionsChecker));
            _permissionsThread.IsBackground = true;
            _permissionsTokenSource = new CancellationTokenSource();
            _permissionsThread.Start(_permissionsTokenSource.Token);
        }

        private Dictionary<Type, Delegate> callbacks = new Dictionary<Type, Delegate>();

        public void RegisterCallback<TDelegate>(TDelegate callback) where TDelegate : Delegate
        {
            callbacks[typeof(TDelegate)] = callback;
        }

        private TDelegate GetCallback<TDelegate>() where TDelegate : Delegate
        {
            return (TDelegate)callbacks.GetOrDefault(typeof(TDelegate));
        }

        internal Form OpenForm(IFleetTrackingControl control, OpenFormOptions formOptions, IWin32Window parent)
        {
            if (control.GetType().GetCustomAttribute<SecuredControlAttribute>() != null)
            {
                SecuredControlAttribute securedControl = control.GetType().GetCustomAttribute<SecuredControlAttribute>();
                if (!securedControl.OptionalPermissions.Any(p => SecurityPermissionCheck(p.ToString())))
                {
                    MessageBox.Show("You do not have permission to open this", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return new Form();
                }
            }

            return GetCallback<CallbackDelegates.OpenForm>().Invoke(control, formOptions, parent);
        }

        internal Form OpenForm(IFleetTrackingControl control, OpenFormOptions formOptions)
        {
            return OpenForm(control, formOptions, null);
        }

        internal Form OpenForm(IFleetTrackingControl control)
        {
            return OpenForm(control, OpenFormOptions.None);
        }

        internal bool SecurityPermissionCheck(string permission)
        {
            return GetCallback<CallbackDelegates.SecurityPermissionCheck>().Invoke(permission);
        }

        internal TAccess GetAccess<TAccess>() where TAccess : DataAccess
        {
            return GetCallback<CallbackDelegates.GetAccess<TAccess>>().Invoke();
        }

        public void BrowseLocomotiveModels()
        {
            Form parentForm = OpenForm(new LocomotiveModel.BrowseLocomotiveModels() { Application = this });
            parentForm.Text = "Browse Locomotive Models";
        }

        public void BrowseRailcarModels()
        {
            Form parentForm = OpenForm(new RailcarModel.BrowseRailcarModels() { Application = this });
            parentForm.Text = "Browse Railcar Models";
        }

        public void BrowseEquipmentRoster()
        {
            Form parentForm = OpenForm(new Roster.BrowseRoster() { Application = this });
            parentForm.Text = "Browse Equipment Roster";
        }

        public void OpenRailcarDetail(long? railcarID)
        {
            OpenForm(new Roster.RailcarDetail() { Application = this, RailcarID = railcarID });
        }

        public void ManageLeasing()
        {
            Form parentForm = OpenForm(new Leasing.LeaseManagement() { Application = this });
            parentForm.Text = "Manage Leasing";
        }

        public long? CreateNewLease()
        {
            Leasing.LeaseRequestDetail leaseRequest = new Leasing.LeaseRequestDetail() { Application = this, CloseOnSave = true };
            OpenForm(leaseRequest, OpenFormOptions.Dialog);
            return leaseRequest.LeaseRequestID;
        }

        public void OpenLeaseRequestDetail(long? leaseRequestID)
        {
            OpenForm(new Leasing.LeaseRequestDetail() { Application = this, LeaseRequestID = leaseRequestID }, OpenFormOptions.Popout);
        }

        public void BrowseTrainSymbols()
        {
            Form parentForm = OpenForm(new TrainSymbols.TrainSymbolList()
            {
                Application = this
            });
            parentForm.Text = "Train Symbols";
        }

        public void BrowseTrains()
        {
            Train.TrainList trainList = new Train.TrainList()
            {
                Application = this
            };
            Form form = OpenForm(trainList);
            form.Text = "Train List";
        }

        public void OpenTrackViewer()
        {
            Tracks.TrackViewer viewer = new Tracks.TrackViewer()
            {
                Application = this
            };
            Form form = OpenForm(viewer);
            form.Text = "Track Viewer";
        }

        public void BrowseRailDistricts()
        {
            Tracks.RailDistrictList districtList = new Tracks.RailDistrictList()
            {
                Application = this
            };
            Form form = OpenForm(districtList);
            form.Text = "Rail Districts";
        }

        public void MassReleaseStock()
        {
            MassUpdateRailcars(true);
        }

        public void ManageMiscSetup()
        {
            Misc.MiscellaneousSettings settings = new Misc.MiscellaneousSettings()
            {
                Application = this
            };
            Form settingsForm = OpenForm(settings);
            settingsForm.Text = "Miscellaneous Setup";
        }

        public void StartLiveLoading()
        {
            InputBox input = new InputBox()
            {
                Application = this
            };
            input.Text = "Live Load Code";
            input.lblPrompt.Text = "Enter the Live Load Code provided by the train crew";

            Form inputForm = OpenForm(input, OpenFormOptions.Dialog);
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }

            CarLoading.LiveLoadClient liveLoadClient = new CarLoading.LiveLoadClient()
            {
                Application = this,
                LiveLoadCode = input.InputValue
            };
            Form clientForm = OpenForm(liveLoadClient, OpenFormOptions.Popout);
            clientForm.Text = "Live Load";
        }

        public void PrintTracks()
        {
            Reports.TrackListing.SelectTracks select = new Reports.TrackListing.SelectTracks()
            {
                Application = this
            };
            OpenForm(select, OpenFormOptions.Popout);
        }

        public void PrintTrainManifests()
        {
            Reports.TrainManifest.SelectTrains select = new Reports.TrainManifest.SelectTrains()
            {
                Application = this
            };
            OpenForm(select, OpenFormOptions.Popout);
        }

        public void PrintRailActivity()
        {
            Reports.RailActivity.RailActivityWizard entry = new Reports.RailActivity.RailActivityWizard()
            {
                Application = this
            };
            OpenForm(entry, OpenFormOptions.Popout | OpenFormOptions.ResizeToControl);
        }

        private ToolStripMenuItem toolStrip = null;
        private List<MainNavigationItem> items = null;
        public ToolStripMenuItem GetNavigation()
        {
            if (items == null)
            {
                items = GetFullNavigationTree().ToList();
            }

            if (toolStrip == null)
            {
                toolStrip = new ToolStripMenuItem("Fleet Tracking", Properties.Resources.lorry);
                toolStrip.ImageScaling = ToolStripItemImageScaling.None;
                toolStrip.DropDownOpened += Navigation_DropDownOpening;
                toolStrip.Name = "mnuFleetTracking";
                foreach(MainNavigationItem item in items)
                {
                    AddItemToToolStrip(item, toolStrip);
                }
            }

            return toolStrip;
        }

        public void VerifyNavigationVisibility()
        {
            bool anyVisible = false;
            foreach (ToolStripMenuItem item in toolStrip.DropDownItems.OfType<ToolStripMenuItem>())
            {
                anyVisible |= SetToolStripItemVisibility(item);
            }

            if (toolStrip.Visible != anyVisible)
            {
                toolStrip.Visible = anyVisible;
            }
        }

        private void AddItemToToolStrip(MainNavigationItem item, ToolStripMenuItem toolStripMenuItem)
        {
            ToolStripMenuItem newStripItem = new ToolStripMenuItem(item.Text, null, (s, e) => { if (item.SelectedAction != null) item.SelectedAction(); });
            newStripItem.DropDownOpened += Navigation_DropDownOpening;
            newStripItem.Tag = item;
            newStripItem.Image = item.Image;
            newStripItem.ImageScaling = ToolStripItemImageScaling.None;
            newStripItem.Name = "mnu" + item.Text.Replace(" ", "");
            toolStripMenuItem.DropDownItems.Add(newStripItem);

            if (item.SubItems != null)
            {
                foreach (MainNavigationItem subItem in item.SubItems)
                {
                    AddItemToToolStrip(subItem, newStripItem);
                }
            }
        }

        private void Navigation_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            SetToolStripItemVisibility(item);
        }

        private bool SetToolStripItemVisibility(ToolStripMenuItem item)
        {
            ToolStrip parent = null;
            ToolStripItem searchItem = item;
            while(searchItem.OwnerItem != null)
            {
                searchItem = searchItem.OwnerItem;
            }

            parent = searchItem.Owner;
            if (parent == null)
            {
                return false;
            }

            if (item.Tag is MainNavigationItem navItem && navItem.OptionalPermissions != null)
            {
                bool newVisible = false;
                foreach(string permission in navItem.OptionalPermissions)
                {
                    newVisible |= SecurityPermissionCheck(permission);
                }

                if (newVisible != item.Visible)
                {
                    if (parent.InvokeRequired)
                    {
                        parent.Invoke(new MethodInvoker(() => item.Visible = newVisible));
                    }
                    else
                    {
                        item.Visible = newVisible;
                    }
                }

                if (!newVisible)
                {
                    return false;
                }
            }

            if (item.DropDownItems.Count > 0)
            {
                bool anyVisible = false;
                foreach (ToolStripMenuItem subItem in item.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    anyVisible |= SetToolStripItemVisibility(subItem);
                }

                if (item.Visible != anyVisible)
                {
                    if (parent.InvokeRequired)
                    {
                        parent.Invoke(new MethodInvoker(() => item.Visible = anyVisible));
                    }
                    else
                    {
                        item.Visible = anyVisible;
                    }
                }

                return anyVisible;
            }

            return true;
        }

        private IEnumerable<MainNavigationItem> GetFullNavigationTree()
        {
            yield return new MainNavigationItem("Rail", Properties.Resources.freight)
            {
                SubItems = new List<MainNavigationItem>()
                {
                    new MainNavigationItem("Setup", Properties.Resources.bricks)
                    {
                        SubItems = new List<MainNavigationItem>()
                        {
                            new MainNavigationItem("Locomotive Models", BrowseLocomotiveModels, Properties.Resources.train, nameof(FleetSecurity.AllowSetup)),
                            new MainNavigationItem("Railcar Models", BrowseRailcarModels, Properties.Resources.train_car, nameof(FleetSecurity.AllowSetup)),
                            new MainNavigationItem("Train Symbols", BrowseTrainSymbols, Properties.Resources.paste_plain, nameof(FleetSecurity.AllowSetup), nameof(FleetSecurity.IsYardmaster)),
                            new MainNavigationItem("Track Districts", BrowseRailDistricts, Properties.Resources.sitemap, nameof(FleetSecurity.AllowSetup), nameof(FleetSecurity.IsYardmaster)),
                            new MainNavigationItem("Misc Setup", ManageMiscSetup, Properties.Resources.cog, nameof(FleetSecurity.AllowSetup))
                        }
                    },
                    new MainNavigationItem("Leasing", ManageLeasing, Properties.Resources.basket, nameof(FleetSecurity.AllowLeasingManagement)),
                    new MainNavigationItem("Equipment Roster", BrowseEquipmentRoster, Properties.Resources.application_view_detail, nameof(FleetSecurity.AllowSetup), nameof(FleetSecurity.IsTrainCrew), nameof(FleetSecurity.IsYardmaster)),
                    new MainNavigationItem("Train Manager", BrowseTrains, Properties.Resources.train, nameof(FleetSecurity.IsYardmaster), nameof(FleetSecurity.IsTrainCrew)),
                    new MainNavigationItem("Track Viewer", OpenTrackViewer, Properties.Resources.tracks, nameof(FleetSecurity.IsYardmaster), nameof(FleetSecurity.IsTrainCrew), nameof(FleetSecurity.AllowLoadUnload)),
                    new MainNavigationItem("Release Equipment", MassReleaseStock, Properties.Resources.key_go, nameof(FleetSecurity.IsYardmaster), nameof(FleetSecurity.IsTrainCrew), nameof(FleetSecurity.AllowLoadUnload)),
                    new MainNavigationItem("Load/Unload Cars", Properties.Resources.box)
                    {
                        SubItems = new List<MainNavigationItem>()
                        {
                            new MainNavigationItem("Live Load", StartLiveLoading, Properties.Resources.lightning, nameof(FleetSecurity.AllowLoadUnload)),
                            new MainNavigationItem("Load On Track", () => OpenForm(new CarLoading.LoadOnTrack() { Application = this, Text = "Load On Track" }), Properties.Resources.package, nameof(FleetSecurity.AllowLoadUnload))
                        }
                    },
                    new MainNavigationItem("Utilities", Properties.Resources.lightbulb)
                    {
                        SubItems = new List<MainNavigationItem>()
                        {
                            new MainNavigationItem("Mass Update Railcars", () => MassUpdateRailcars(), Properties.Resources.table_multiple, nameof(FleetSecurity.AllowLoadUnload), nameof(FleetSecurity.IsTrainCrew), nameof(FleetSecurity.IsYardmaster))
                        }
                    },
                    new MainNavigationItem("Reports", Properties.Resources.report)
                    {
                        SubItems = new List<MainNavigationItem>()
                        {
                            new MainNavigationItem("Track Listing", PrintTracks, Properties.Resources.tracks, nameof(FleetSecurity.IsYardmaster), nameof(FleetSecurity.IsTrainCrew), nameof(FleetSecurity.AllowLoadUnload)),
                            new MainNavigationItem("Train Manifest", PrintTrainManifests, Properties.Resources.train, nameof(FleetSecurity.IsYardmaster), nameof(FleetSecurity.IsTrainCrew)),
                            new MainNavigationItem("Rail Activity", PrintRailActivity, Properties.Resources.moving, nameof(FleetSecurity.IsYardmaster))
                        }
                    }
                }
            };
        }

        private void MassUpdateRailcars(bool defaultSetRelease = false)
        {
            Form form = OpenForm(new Utilities.MassUpdateEquipment() { Application = this, DefaultSetRelease = defaultSetRelease }, OpenFormOptions.Popout);
            form.Text = "Mass Update Railcars";
        }

        internal bool IsCurrentEntity(long? companyID, long? governmentID)
        {
            return GetCallback<CallbackDelegates.IsCurrentEntity>()?.Invoke(companyID, governmentID) ?? false;
        }

        internal (long?, long?) GetCurrentCompanyIDGovernmentID()
        {
            return GetCallback<CallbackDelegates.GetCurrentCompanyIDGovernmentID>()?.Invoke() ?? (null, null);
        }

        internal async Task<List<User>> GetUsersForCurrentEntity()
        {
            return (await GetCallback<CallbackDelegates.GetUsersForEntity>()?.Invoke()) ?? new List<User>();
        }

        public enum OpenFormOptions
        {
            None = 0,
            Popout = 1,
            Dialog = 2,
            ResizeToControl = 4
        }

        public class MainNavigationItem
        {
            public MainNavigationItem(string text, Action selectedAction, Image image, params string[] optionalPermissions)
            {
                Text = text;
                SelectedAction = selectedAction;
                Image = image;
                OptionalPermissions = optionalPermissions;
            }

            public MainNavigationItem(string text, Action selectedAction, params string[] optionalPermissions) : this(text, selectedAction, null, optionalPermissions) { }

            public MainNavigationItem(string text, Image image) : this(text, null, image, null) { }

            public MainNavigationItem(string text) : this(text, null, null, optionalPermissions: null) { }

            public MainNavigationItem() : this(null, null, null, optionalPermissions: null) { }

            public string Text { get; set; }
            public Action SelectedAction { get; set; }
            public List<MainNavigationItem> SubItems { get; set; }
            public Image Image { get; set; }
            public string[] OptionalPermissions { get; set; }
        }

        private void PermissionsChecker(object cancelTokenObj)
        {
            if (!(cancelTokenObj is CancellationToken cancellationToken))
            {
                throw new ArgumentException("Parameter must be a CancellationToken", nameof(cancelTokenObj));
            }

            while(!cancellationToken.IsCancellationRequested)
            {
                if (toolStrip != null)
                {
                    VerifyNavigationVisibility();
                }

                foreach(Form form in Application.OpenForms.OfType<Form>().ToList())
                {
                    foreach (IFleetTrackingControl control in form.Controls.OfType<IFleetTrackingControl>().Where(c => c.GetType().GetCustomAttribute<SecuredControlAttribute>() != null))
                    {
                        SecuredControlAttribute securedControl = control.GetType().GetCustomAttribute<SecuredControlAttribute>();
                        if (!securedControl.OptionalPermissions.Any(p => SecurityPermissionCheck(p.ToString())))
                        {
                            Action action = () =>
                            {
                                form.ShowError("You do not have permission to use " + form.Text);
                                form.Close();
                            };

                            if (form.InvokeRequired)
                            {
                                form.Invoke(new MethodInvoker(action));
                            }
                            else
                            {
                                action();
                            }
                        }
                    }
                }

                Thread.Sleep(5000);
            }
        }

        public void Shutdown()
        {
            _permissionsTokenSource.Cancel();
        }
    }
}
