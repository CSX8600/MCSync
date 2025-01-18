using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio
{
    public partial class Loader : UserControl
    {
        public Loader()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get => lblLoadingText.Text;
            set => lblLoadingText.Text = value;
        }
    }
}
