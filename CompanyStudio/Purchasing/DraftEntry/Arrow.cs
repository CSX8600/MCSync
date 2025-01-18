using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class Arrow : UserControl
    {
        private int _arrowHeadLength = 45;
        public int ArrowHeadLength
        {
            get => _arrowHeadLength;
            set
            {
                _arrowHeadLength = value;
                Invalidate();
            }
        }

        public enum ArrowDirections
        {
            Right,
            Left
        }

        private ArrowDirections _arrowDirection;
        public ArrowDirections ArrowDirection
        {
            get => _arrowDirection;
            set
            {
                _arrowDirection = value;
                Invalidate();
            }
        }

        public Arrow()
        {
            InitializeComponent();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            pen = new Pen(ForeColor, 2f);
            base.OnForeColorChanged(e);
        }

        private Pen pen = new Pen(DefaultForeColor, 2F);
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen, new Point(ClientRectangle.Left, ClientRectangle.Bottom / 2), new Point(ClientRectangle.Right, ClientRectangle.Bottom / 2));
            switch(ArrowDirection)
            {
                case ArrowDirections.Left:
                    e.Graphics.DrawLine(pen, new Point(ClientRectangle.Left, ClientRectangle.Bottom / 2), new Point(ClientRectangle.Left + ArrowHeadLength, ClientRectangle.Top));
                    e.Graphics.DrawLine(pen, new Point(ClientRectangle.Left, ClientRectangle.Bottom / 2), new Point(ClientRectangle.Left + ArrowHeadLength, ClientRectangle.Bottom));
                    break;
                case ArrowDirections.Right:
                    e.Graphics.DrawLine(pen, new Point(ClientRectangle.Right, ClientRectangle.Bottom / 2), new Point(ClientRectangle.Right - ArrowHeadLength, ClientRectangle.Top));
                    e.Graphics.DrawLine(pen, new Point(ClientRectangle.Right, ClientRectangle.Bottom / 2), new Point(ClientRectangle.Right - ArrowHeadLength, ClientRectangle.Bottom));
                    break;
            }
            base.OnPaint(e);
        }
    }
}
