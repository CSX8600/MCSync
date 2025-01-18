using CompanyStudio.Models;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio
{
    public class BaseCompanyStudioContent : DockContent
    {
        public event EventHandler OnCompanyChange;
        public event EventHandler<ThemeBase> OnThemeChange;
        public event EventHandler OnStudioChange;
        public event EventHandler OnIsDirtyChange;

        protected StudioFormExtender studioFormExtender;

        private Company _company;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Company Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnCompanyChange?.Invoke(this, new EventArgs());

                if (Studio != null)
                {
                    Studio.OnCompanyRemoved -= Studio_OnCompanyRemoved;
                    Studio.OnCompanyRemoved += Studio_OnCompanyRemoved;
                    FormClosed -= BaseCompanyStudioContent_FormClosed;
                    FormClosed += BaseCompanyStudioContent_FormClosed;
                }
            }
        }

        private ThemeBase _theme;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeBase Theme
        {
            protected get { return _theme; }
            set
            {
                _theme = value;
                studioFormExtender.ApplyStyle(this, value);
                OnThemeChange?.Invoke(this, value);
            }
        }

        private frmStudio _frmStudio;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public frmStudio Studio
        {
            get { return _frmStudio; }
            set
            {
                if (_frmStudio != null)
                {
                    _frmStudio.OnCompanyRemoved -= Studio_OnCompanyRemoved;
                }

                _frmStudio = value;
                OnStudioChange?.Invoke(this, new EventArgs());
                if (Company != null)
                {
                    _frmStudio.OnCompanyRemoved -= Studio_OnCompanyRemoved;
                    _frmStudio.OnCompanyRemoved += Studio_OnCompanyRemoved;
                    FormClosed -= BaseCompanyStudioContent_FormClosed;
                    FormClosed += BaseCompanyStudioContent_FormClosed;
                }
            }
        }

        private bool _isDirty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get { return _isDirty; }
            protected set
            {
                _isDirty = value;

                if (IsDirty && !Text.StartsWith("*"))
                {
                    Text = $"*{Text}";
                }
                else if (!IsDirty && Text.StartsWith("*"))
                {
                    Text = Text.Substring(1);
                }

                OnIsDirtyChange?.Invoke(this, new EventArgs());
            }
        }

        public BaseCompanyStudioContent()
        {
            InitializeComponent();
            studioFormExtender = new StudioFormExtender();
        }

        protected sealed override string GetPersistString()
        {
            JObject persistObject = new JObject();
            persistObject.Add("type", GetType().ToString());

            if (Company != null)
            {
                persistObject.Add("company", JObject.FromObject(Company));
            }

            JObject customPersistObject = GetPersistObject();
            if (customPersistObject != null)
            {
                persistObject.Add("custom", customPersistObject);
            }

            return persistObject.ToString();
        }

        public void HandlePersistString(string persistString)
        {
            JObject persistObject = JObject.Parse(persistString);
            Company = persistObject.GetValue("company")?.Value<JObject>().ToObject<Company>();
            JObject customPersistObject = persistObject.GetValue("custom")?.Value<JObject>();

            if (customPersistObject != null)
            {
                HandlePersistObject(persistObject);
            }
        }

        protected virtual JObject GetPersistObject()
        {
            return null;
        }

        protected virtual void HandlePersistObject(JObject value) { }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseCompanyStudioContent
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "BaseCompanyStudioContent";
            this.ResumeLayout(false);

        }

        private void Studio_OnCompanyRemoved(object sender, Company e)
        {
            if (e.CompanyID == (Company?.CompanyID ?? 0))
            {
                Close();
            }
        }

        private void BaseCompanyStudioContent_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            FormClosed -= BaseCompanyStudioContent_FormClosed;
            Studio.OnCompanyRemoved -= Studio_OnCompanyRemoved;
        }

        protected void AppendCompanyLocationNameToWindowText()
        {
            Text += $" - {Company.Name}";
            if (this is ILocationScoped locationScoped)
            {
                Text += $" ({locationScoped.LocationModel.Name})";
            }
        }
    }
}
