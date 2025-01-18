using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace GovernmentPortal.Accounts
{
    public partial class AccountExplorerControl : UserControl, IExplorerControl<Account>
    {
        public event EventHandler IsDirtyChanged;
        private long _governmentID;
        public AccountExplorerControl()
        {
            InitializeComponent();
        }

        public AccountExplorerControl(long governmentID) : this()
        {
            _governmentID = governmentID;
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                IsDirtyChanged?.Invoke(this, new EventArgs());
            }
        }

        public Account Model { get; set; }

        private frmGenericExplorer<Account> _explorer;
        public frmGenericExplorer<Account> Explorer { set => _explorer = value; }


        public void OnDeleteClicked()
        {
            this.ShowError("Accounts must be properly closed. Please use the Close Account button instead.");
        }

        public async void OnSaveClicked()
        {
            loader.BringToFront();
            loader.Visible = true;

            Account accountToSave = Model;
            if (accountToSave == null)
            {
                accountToSave = new Account();
                accountToSave.GovernmentID = _governmentID;
            }

            accountToSave.Description = txtDescription.Text;
            accountToSave.CategoryID = cboCategory.SelectedItem?.Cast<DropDownItem<Category>>().Object?.CategoryID;

            if (Model == null)
            {
                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "Account/Post", accountToSave);
                post.AddGovHeader(_governmentID);

                Account savedAccount = await post.Execute<Account>();
                if (post.RequestSuccessful && await SaveAccesses(savedAccount.AccountID.Value))
                {
                    IsDirty = false;
                    _explorer.LoadAllItems(true, accountToSave.Description);
                    Dispose();
                    return;
                }
            }
            else
            {
                PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "Account/Put", accountToSave);
                put.AddGovHeader(_governmentID);

                await put.ExecuteNoResult();
                if (put.RequestSuccessful && await SaveAccesses(Model.AccountID.Value))
                {
                    IsDirty = false;
                    _explorer.LoadAllItems(true, accountToSave.Description);
                    Dispose();
                    return;
                }
            }

            loader.Visible = false;
        }

        private async Task<bool> SaveAccesses(long accountID)
        {
            bool success = true;

            foreach(DataGridViewRow row in dgvAccess.Rows)
            {
                Official official = (Official)row.Tag;
                bool hasAccess = (bool)(row.Cells["colAccess"].Value ?? false);

                PutData putAccess = new PutData(DataAccess.APIs.GovernmentPortal, $"Account/PutUserIDAccessForAccount/{accountID}", new { userid = official.UserID, hasAccess });
                putAccess.AddGovHeader(_governmentID);
                await putAccess.ExecuteNoResult();

                success &= putAccess.RequestSuccessful;
            }

            return success;
        }

        private async void AccountExplorerControl_Load(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;

            cboCategory.Items.Add(new DropDownItem<Category>(null, ""));

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "Category/GetAll");
            get.AddGovHeader(_governmentID);
            List<Category> categories = await get.GetObject<List<Category>>();
            if (categories != null)
            {
                foreach (Category category in categories)
                {
                    cboCategory.Items.Add(new DropDownItem<Category>(category, category.Name));
                }
            }

            get = new GetData(DataAccess.APIs.GovernmentPortal, "Official/GetAllForGovernment");
            get.AddGovHeader(_governmentID);
            List<Official> allOfficials = await get.GetObject<List<Official>>();
            if (allOfficials != null)
            {
                foreach(Official official in allOfficials)
                {
                    DataGridViewRow row = dgvAccess.Rows[dgvAccess.Rows.Add()];
                    row.Cells["colOfficial"].Value = official.OfficialName;
                    row.Tag = official;
                }
            }

            if (Model != null)
            {
                txtAccountNumber.Text = Model.AccountNumber;
                txtDescription.Text = Model.Description;
                cboCategory.SelectedItem = cboCategory.Items.Cast<DropDownItem<Category>>().FirstOrDefault(c => c.Object?.CategoryID == Model.CategoryID);
                txtBalance.Text = Model.Balance.ToString("N2");

                get = new GetData(DataAccess.APIs.GovernmentPortal, $"Account/GetUserIDAccessForAccount/{Model.AccountID}");
                get.AddGovHeader(_governmentID);
                long[] userIDs = await get.GetObject<long[]>();
                if (get.RequestSuccessful)
                {
                    foreach(DataGridViewRow row in dgvAccess.Rows)
                    {
                        Official official = (Official)row.Tag;
                        row.Cells["colAccess"].Value = userIDs.Contains(official.UserID);
                    }
                }
            }

            loader.Visible = false;
        }

        int transactionPage = 1;
        int totalTransactions = 0;
        private async void LoadTransactions()
        {
            if (Model == null)
            {
                return;
            }

            loader.BringToFront();
            loader.Visible = true;
            dgvTransactions.Rows.Clear();

            GetData getTransactions = new GetData(DataAccess.APIs.GovernmentPortal, "Transaction/GetForAccount");
            getTransactions.AddGovHeader(_governmentID);
            getTransactions.QueryString.Add("accountID", Model.AccountID.ToString());
            getTransactions.QueryString.Add("skip", ((transactionPage - 1) * 50).ToString());
            getTransactions.QueryString.Add("take", "50");
            var responseObject = new
            {
                hasNext = false,
                hasPrevious = false,
                transactions = new List<Transaction>(),
                transactionCount = 0
            };

            responseObject = await getTransactions.GetAnonymousObject(responseObject);
            if (!getTransactions.RequestSuccessful)
            {
                loader.Visible = false;
            }

            cmdTransFirst.Enabled = responseObject.hasPrevious;
            cmdTransPrev.Enabled = responseObject.hasPrevious;
            cmdTransNext.Enabled = responseObject.hasNext;
            cmdTransLast.Enabled = responseObject.hasNext;

            foreach (Transaction transaction in responseObject.transactions)
            {
                int row = dgvTransactions.Rows.Add();
                dgvTransactions[0, row].Value = TimeZoneInfo.ConvertTime(transaction.TransactionTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"), TimeZoneInfo.Local).ToString("MM/dd/yyyy HH:mm");
                dgvTransactions[1, row].Value = transaction.Amount.ToString("N2");
                dgvTransactions[1, row].Style.ForeColor = transaction.Amount >= 0 ? Color.DarkGreen : Color.Red;
                dgvTransactions[2, row].Value = transaction.Description;
            }

            totalTransactions = responseObject.transactionCount;

            loader.Visible = false;
        }

        private void cmdTransNext_Click(object sender, EventArgs e)
        {
            transactionPage++;
            LoadTransactions();
        }

        private void cmdTransPrev_Click(object sender, EventArgs e)
        {
            transactionPage--;
            LoadTransactions();
        }

        private void cmdTransFirst_Click(object sender, EventArgs e)
        {
            transactionPage = 1;
            LoadTransactions();
        }

        private void cmdTransLast_Click(object sender, EventArgs e)
        {
            transactionPage = totalTransactions / 50 + 1;
            LoadTransactions();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            transactionPage = 1;
            totalTransactions = 0;
            if (tabControl.SelectedTab == tabTransactions)
            {
                LoadTransactions();
            }
            else if (tabControl.SelectedTab == tabFiscalQuarters)
            {
                LoadFiscalQuarters();
            }
            else if (tabControl.SelectedTab == tabDebitCards)
            {
                LoadDebitCards();
            }
        }

        private async void LoadFiscalQuarters()
        {
            if (Model == null)
            {
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            dgvFiscalQuarters.Rows.Clear();

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "FiscalQuarter/GetForAccount");
            get.AddGovHeader(_governmentID);
            get.QueryString.Add("AccountID", Model.AccountID.ToString());

            List<FiscalQuarter> fiscalQuarters = await get.GetObject<List<FiscalQuarter>>();
            if (!get.RequestSuccessful)
            {
                loader.Visible = false;
                return;
            }

            foreach (FiscalQuarter fiscalQuarter in fiscalQuarters)
            {
                int row = dgvFiscalQuarters.Rows.Add();
                dgvFiscalQuarters[0, row].Value = fiscalQuarter.Year;
                dgvFiscalQuarters[1, row].Value = fiscalQuarter.Quarter;
                dgvFiscalQuarters[2, row].Value = fiscalQuarter.StartDate.ToString("MM/dd/yyyy");
                dgvFiscalQuarters[3, row].Value = fiscalQuarter.EndDate.ToString("MM/dd/yyyy");
                dgvFiscalQuarters[4, row].Value = fiscalQuarter.StartingBalance.ToString("N2");
                dgvFiscalQuarters[5, row].Value = fiscalQuarter.EndingBalance?.ToString("N2");

                decimal? netChange = fiscalQuarter.EndingBalance - fiscalQuarter.StartingBalance;
                decimal? netPercent = fiscalQuarter.StartingBalance == 0 ? netChange : netChange / fiscalQuarter.StartingBalance;

                dgvFiscalQuarters[6, row].Value = netChange?.ToString("N2");
                if (netChange != null)
                {
                    dgvFiscalQuarters[6, row].Style.ForeColor = netChange > 0 ? Color.DarkGreen : Color.Red;
                }

                dgvFiscalQuarters[7, row].Value = netPercent?.ToString("P");
                if (netPercent != null)
                {
                    dgvFiscalQuarters[7, row].Style.ForeColor = netPercent > 0 ? Color.DarkGreen : Color.Red;
                }
            }

            loader.Visible = false;
        }

        public async void LoadDebitCards()
        {
            if (Model == null)
            {
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            dgvDebitCards.Rows.Clear();

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "DebitCard/GetForAccount");
            get.Headers.Add("GovernmentID", _governmentID.ToString());
            get.QueryString.Add("AccountID", Model.AccountID.ToString());

            List<DebitCard> debitCards = await get.GetObject<List<DebitCard>>() ?? new List<DebitCard>();

            foreach (DebitCard card in debitCards)
            {
                int row = dgvDebitCards.Rows.Add();

                dgvDebitCards[colCardNumber.Index, row].Value = new string('X', 12) + card.CardNumber.Substring(12);
                dgvDebitCards[colIssuedTo.Index, row].Value = card.UserIssuedBy?.Username;
                dgvDebitCards[colIssuedTime.Index, row].Value = card.IssuedTime?.ToString("MM/dd/yyyy HH:mm");
                dgvDebitCards.Rows[row].Tag = card.DebitCardID;
            }

            loader.Visible = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (Model == null)
            {
                this.ShowError("Cannot close an unsaved Account");
                return;
            }

            frmCloseAccount frmCloseAccount = new frmCloseAccount(Model.AccountID.Value, _governmentID);
            if (frmCloseAccount.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            IsDirty = false;
            _explorer.LoadAllItems(true);
            Dispose();
        }

        private void cmdTransfer_Click(object sender, EventArgs e)
        {
            if (Model == null)
            {
                this.ShowError("Cannot transfer from an unsaved Account");
                return;
            }

            frmTransfer frmTransfer = new frmTransfer(Model.AccountID.Value, _governmentID);
            if (frmTransfer.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _explorer.LoadAllItems(true, Model.Description);
        }

        private async void tsbDeleteDebitCard_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete these Debit Cards?"))
            {
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            foreach(DataGridViewRow row in dgvDebitCards.SelectedRows)
            {
                long? debitCardID = (long?)row.Tag;

                DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, $"DebitCard/Delete/{debitCardID}");
                delete.AddGovHeader(_governmentID);
                await delete.Execute();
            }

            LoadDebitCards();
            loader.Visible = false;
        }
    }
}
