using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesaSuite.Common
{
    public partial class frmLogin : Form
    {
        public Guid ClientID { get; set; }
        public Guid State { get; set; }
        public int Port { get; set; }
        public string LoginToProgramName { get; set; }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            browser.Refresh();
            browser.Load($"{ConfigurationManager.AppSettings.Get("MesaSuite.Common.AuthHost")}/Authorize?response_type=code&client_id={ClientID.ToString()}&redirect_uri={WebUtility.UrlEncode("http://localhost:" + Port)}&state={State.ToString()}&logintoprogramname={WebUtility.UrlEncode(LoginToProgramName)}");
        }
    }
}
