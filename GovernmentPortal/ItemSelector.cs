using GovernmentPortal.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal
{
    [ToolboxItem(false)]
    public partial class ItemSelector : UserControl
    {
        private CancellationTokenSource _loadDataSource;
        private const int TAKE = 15;

        public event EventHandler ItemSelected;
        
        public long? SelectedItemID
        {
            get;
            set;
        }

        public string SelectedItemText
        {
            get;
            private set;
        }

        public bool ReadOnlyMode { get; set; }

        public ItemSelector()
        {
            InitializeComponent();
        }

        #region Fancy UI Stuff
        private void panel1_Click(object sender, EventArgs e)
        {
            int index = txtSearch.GetCharIndexFromPosition(new Point(txtSearch.PointToClient(Cursor.Position).X, txtSearch.Top));
            txtSearch.Select(index, 0);
            txtSearch.Focus();
        }

        private void dgvList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            row.Selected = true;
        }

        private void dgvList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            row.Selected = false;
        }

        private void dgvList_MouseLeave(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvList.Rows)
            {
                row.Selected = false;
            }
        }

        private void dgvList_SizeChanged(object sender, EventArgs e)
        {
            colName.Width = dgvList.Width - 35;
        }
        #endregion

        private void ItemSelector_Load(object sender, EventArgs e)
        {
            LoadInitialData();

            if (ReadOnlyMode)
            {
                this.Size = new Size(panel1.Right, panel1.Height);
                txtSearch.ReadOnly = true;
                panel1.BackColor = SystemColors.Control;
                pictureBox1.BackColor = SystemColors.Control;
            }
            else
            {
                LoadData();
            }
        }

        private async void LoadInitialData()
        {
            if (SelectedItemID != null)
            {
                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, $"Item/Get/{SelectedItemID}");
                Item item = await get.GetObject<Item>();
                if (item != null)
                {
                    SelectedItemText = item.Name;
                    txtSearch.Text = item.Name;
                    using (MemoryStream stream = new MemoryStream(item.Image))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
            }
        }

        private async void LoadData()
        {
            if (_loadDataSource != null)
            {
                _loadDataSource.Cancel();
            }

            loader.BringToFront();
            loader.Visible = true;

            _loadDataSource = new CancellationTokenSource();
            CancellationToken token = _loadDataSource.Token;

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "Item/GetByQuery");
            get.QueryString.Add("q", txtSearch.Text);
            get.QueryString.Add("t", TAKE.ToString());
            List<Item> items = await get.GetObject<List<Item>>() ?? new List<Item>();

            if (token.IsCancellationRequested) return;

            dgvList.Rows.Clear();

            foreach(Item item in items)
            {
                if (token.IsCancellationRequested) return;

                int rowIndex = dgvList.Rows.Add();
                DataGridViewRow row = dgvList.Rows[rowIndex];

                Image image;
                using (MemoryStream stream = new MemoryStream(item.Image))
                {
                    image = Image.FromStream(stream);
                }

                row.Cells[colImage.Name].Value = image;
                row.Cells[colName.Name].Value = string.Format("{0}\r\n({1})", item.Name, item.ItemNamespace.FriendlyName);
                row.Tag = item;
            }

            loader.Visible = false;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (ReadOnlyMode) return;
            pictureBox1.Image = null;
            SelectedItemID = null;
            SelectedItemText = null;
            LoadData();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvList.Rows[e.RowIndex];

            SelectedItemID = row.Tag.Cast<Item>().ItemID;
            SelectedItemText = row.Tag.Cast<Item>().Name;
            ItemSelected?.Invoke(this, EventArgs.Empty);

            pictureBox1.Image = (Image)row.Cells[colImage.Name].Value;
            txtSearch.Text = (string)row.Cells[colName.Name].Value;
            txtSearch.SelectAll();
            txtSearch.Focus();
        }
    }
}
