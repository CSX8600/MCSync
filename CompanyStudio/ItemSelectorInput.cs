using CompanyStudio.Models;
using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio
{
    public partial class ItemSelectorInput : UserControl
    {
        ItemSelector selector;

        public event EventHandler ItemSelected;
        public string SelectedText { get; private set; }
        private bool _itemSelectorSelected = false;
        private long? _selectedID;
        public long? SelectedID
        {
            get => _selectedID;
            set
            {
                _selectedID = value;
                if (!_itemSelectorSelected)
                {
                    LoadInitialData();
                }
            }
        }

        private Size OriginalSize;
        public ItemSelectorInput()
        {
            InitializeComponent();
            OriginalSize = Size;
        }

        private void ItemSelectorInput_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadInitialData();
            }
        }

        private async void LoadInitialData()
        {
            if (SelectedID != null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, $"Item/Get/{SelectedID}");
                Item item = await get.GetObject<Item>();
                if (item != null)
                {
                    SelectedText = item.Name;
                    textBox1.Text = item.Name;
                }
            }
        }

        private void cmdDropDown_Click(object sender, EventArgs e)
        {
            if (selector != null)
            {
                if (this.Controls.Contains(selector))
                {
                    this.Controls.Remove(selector);
                }
                if (!selector.IsDisposed)
                {
                    selector.Dispose();
                }
            }
            else
            {
                OriginalSize = Size;
            }

            selector = new ItemSelector();
            selector.Width = Width;
            selector.SelectedItemID = SelectedID;
            selector.Leave += (s, ea) => { Controls.Remove(selector); SetControlBounds(false); };
            selector.ItemSelected += (s, ea) =>
            {
                _itemSelectorSelected = true;
                SelectedText = selector.SelectedItemText;
                textBox1.Text = SelectedText;
                SelectedID = selector.SelectedItemID;
                textBox1.SelectAll();
                textBox1.Focus();
                _itemSelectorSelected = false;
                ItemSelected?.Invoke(this, EventArgs.Empty);
            };
            SetControlBounds(true);
            this.Controls.Add(selector);
            selector.BringToFront();
            selector.Focus();
        }

        private void SetControlBounds(bool showingSelector)
        {
            if (showingSelector)
            {
                Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, selector.Height);
            }
            else
            {
                Bounds = new Rectangle(Location, OriginalSize);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Escape:
                    return;
            }

            if (e.Control || e.Alt)
            {
                return;
            }

            cmdDropDown.PerformClick();
        }
    }
}
