using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class RouteControlEntity : UserControl
    {
        public event EventHandler DeletePressed;
        public event EventHandler<Directions> MovePressed;
        public event EventHandler<Directions> InsertPressed;
        public long? ContextCompanyID { get; set; }
        public long? ContextLocationID { get; set; }
        public long? SelectedCompanyID { get; set; }
        public long? SelectedGovernmentID { get; set; }

        public RouteControlEntity()
        {
            InitializeComponent();
        }

        private async void RouteControlCompany_Load(object sender, EventArgs e)
        {
            try
            {
                cboEntities.Enabled = false;

                List<DropDownItem> dropDownItems = new List<DropDownItem>();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetAll");
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();
                foreach(Company company in companies.OrderBy(c => c.Name))
                {
                    dropDownItems.Add(new DropDownItem<Company>(company, company.Name));
                }

#warning Government Disabled
                //get = new GetData(DataAccess.APIs.CompanyStudio, "Government/GetAll");
                //get.AddLocationHeader(ContextCompanyID, ContextLocationID);
                //List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();
                //foreach (Government government in governments.OrderBy(g => g.Name))
                //{
                //    dropDownItems.Add(new DropDownItem<Government>(government, government.Name));
                //}

                cboEntities.Items.AddRange(dropDownItems.OrderBy(ddi => ddi.Text).ToArray());

                if (SelectedCompanyID != null)
                {
                    cboEntities.SelectedItem = cboEntities.Items.OfType<DropDownItem<Company>>().FirstOrDefault(c => c.Object.CompanyID == SelectedCompanyID);
                }
                else if (SelectedGovernmentID != null)
                {
                    cboEntities.SelectedItem = cboEntities.Items.OfType<DropDownItem<Government>>().FirstOrDefault(c => c.Object.GovernmentID == SelectedGovernmentID);
                }
            }
            finally
            {
                cboEntities.Enabled = true;
            }
        }

        public enum Directions
        {
            Left,
            Right
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeletePressed?.Invoke(this, EventArgs.Empty);
        }

        private void cmdInsertLeft_Click(object sender, EventArgs e)
        {
            InsertPressed?.Invoke(this, Directions.Left);
        }

        private void cmdMoveLeft_Click(object sender, EventArgs e)
        {
            MovePressed?.Invoke(this, Directions.Left);
        }

        private void cmdMoveRight_Click(object sender, EventArgs e)
        {
            MovePressed?.Invoke(this, Directions.Right);
        }

        private void cmdInsertRight_Click(object sender, EventArgs e)
        {
            InsertPressed?.Invoke(this, Directions.Right);
        }

        private void cboEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCompanyID = (cboEntities.SelectedItem as DropDownItem<Company>)?.Object.CompanyID;
            SelectedGovernmentID = (cboEntities.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID;
        }
    }
}
