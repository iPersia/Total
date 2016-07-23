namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class LoadingForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public LoadingForm()
        {
            InitializeComponent();
            this.Size = new Size(Nzl.Web.Forms.Properties.Resources.LoadingGif.Size.Width + 100,
                                 Nzl.Web.Forms.Properties.Resources.LoadingGif.Size.Height + 100);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Bitmap animatedGif = Nzl.Web.Forms.Properties.Resources.LoadingGif;
            Graphics g = this.panel.CreateGraphics();

            // A Gif image's frame delays are contained in a byte array
            // in the image's PropertyTagFrameDelay Property Item's
            // value property.
            // Retrieve the byte array...
            int PropertyTagFrameDelay = 0x5100;
            PropertyItem propItem = animatedGif.GetPropertyItem(PropertyTagFrameDelay);
            byte[] bytes = propItem.Value;
            // Get the frame count for the Gif...
            FrameDimension frameDimension = new FrameDimension(animatedGif.FrameDimensionsList[0]);
            int frameCount = animatedGif.GetFrameCount(FrameDimension.Time);

            // Create an array of integers to contain the delays,
            // in hundredths of a second, between each frame in the Gif image.
            int[] delays = new int[frameCount + 1];
            int i = 0;
            for (i = 0; i <= frameCount - 1; i++)
            {
                delays[i] = BitConverter.ToInt32(bytes, i * 4);
            }

            // Play the Gif one time...
            while (true)
            {
                for (i = 0; i <= animatedGif.GetFrameCount(frameDimension) - 1; i++)
                {
                    animatedGif.SelectActiveFrame(frameDimension, i);
                    g.DrawImage(animatedGif, new Point(0, 0));
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(delays[i] * 10);
                }
            }
        }
    }
}
