using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace MesaSuite.Common
{
    public partial class ImageDisposer : Component
    {
        public List<Image> Images { get; set; } = new List<Image>();
        public ImageDisposer()
        {
            InitializeComponent();
            this.Disposed += ImageDisposer_Disposed;
        }

        private void ImageDisposer_Disposed(object sender, EventArgs e)
        {
            DisposeAllImages();
        }

        public ImageDisposer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void DisposeAllImages()
        {
            foreach (Image image in Images.ToList())
            {
                try
                {
                    image.Dispose();
                    Images.Remove(image);
                }
                catch { }
            }
        }
    }
}
