using System;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class RouteControlStart : UserControl
    {
        public event EventHandler InsertPressed;

        public long? GovernmentID { get; set; }
        public long? LocationID { get; set; }
        public string EntityName
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        public RouteControlStart()
        {
            InitializeComponent();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
