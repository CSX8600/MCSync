using System;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class RouteControlEnd : UserControl
    {
        public event EventHandler InsertPressed;
        public long? LocationID { get; set; }
        public string EntityName
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        public RouteControlEnd()
        {
            InitializeComponent();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
