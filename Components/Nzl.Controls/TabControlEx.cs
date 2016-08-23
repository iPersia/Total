namespace Nzl.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Description of TabControlEx.
    /// </summary>
    [ToolboxBitmap(typeof(TabControl))]
    public class TabControlEx : TabControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TabControlEx() : base()
        {
            if (this._DisplayManager.Equals(TabControlDisplayManager.Custom))
            {
                this.SetStyle(ControlStyles.UserPaint, true);
                this.ItemSize = new Size(0, 15);
                this.Padding = new Point(9, 0);
            }

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.ResizeRedraw = true;
        }

        /// <summary>
        /// 
        /// </summary>
        TabControlDisplayManager _DisplayManager = TabControlDisplayManager.Custom;

        /// <summary>
        /// 
        /// </summary>
        [System.ComponentModel.DefaultValue(typeof(TabControlDisplayManager), "Custom")]
        public TabControlDisplayManager DisplayManager
        {
            get
            {
                return this._DisplayManager;
            }
            set
            {
                if (this._DisplayManager != value)
                {
                    if (this._DisplayManager.Equals(TabControlDisplayManager.Custom))
                    {
                        this.SetStyle(ControlStyles.UserPaint, true);
                        this.ItemSize = new Size(0, 15);
                        this.Padding = new Point(9, 0);
                    }
                    else
                    {
                        this.ItemSize = new Size(0, 0);
                        this.Padding = new Point(6, 3);
                        this.SetStyle(ControlStyles.UserPaint, false);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (this.DesignMode == true)
            {
                LinearGradientBrush backBrush = new LinearGradientBrush(
                            this.Bounds,
                            SystemColors.ControlLightLight,
                            SystemColors.ControlLight,
                            LinearGradientMode.Vertical);
                pevent.Graphics.FillRectangle(backBrush, this.Bounds);
                backBrush.Dispose();
            }
            else
            {
                this.PaintTransparentBackground(pevent.Graphics, this.ClientRectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clipRect"></param>
        protected void PaintTransparentBackground(Graphics g, Rectangle clipRect)
        {
            if ((this.Parent != null))
            {
                clipRect.Offset(this.Location);
                PaintEventArgs e = new PaintEventArgs(g, clipRect);
                GraphicsState state = g.Save();
                g.SmoothingMode = SmoothingMode.HighSpeed;
                try
                {
                    g.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);
                }

                finally
                {
                    g.Restore(state);
                    clipRect.Offset(-this.Location.X, -this.Location.Y);
                }
            }
            else
            {
                System.Drawing.Drawing2D.LinearGradientBrush backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(this.Bounds, SystemColors.ControlLightLight, SystemColors.ControlLight, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                g.FillRectangle(backBrush, this.Bounds);
                backBrush.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //   Paint the Background
            this.PaintTransparentBackground(e.Graphics, this.ClientRectangle);

            this.PaintAllTheTabs(e);
            this.PaintTheTabPageBorder(e);
            this.PaintTheSelectedTab(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void PaintAllTheTabs(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                for (int index = 0; index < this.TabCount; index++)
                {
                    this.PaintTab(e, index);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="index"></param>
        private void PaintTab(System.Windows.Forms.PaintEventArgs e, int index)
        {
            GraphicsPath path = this.GetPath(index);
            this.PaintTabBackground(e.Graphics, index, path);
            this.PaintTabBorder(e.Graphics, index, path);
            this.PaintTabText(e.Graphics, index);
            this.PaintTabImage(e.Graphics, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="index"></param>
        /// <param name="path"></param>
        private void PaintTabBackground(System.Drawing.Graphics graph, int index, System.Drawing.Drawing2D.GraphicsPath path)
        {
            Rectangle rect = this.GetTabRect(index);
            System.Drawing.Brush buttonBrush =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    rect,
                    SystemColors.ControlLightLight,
                    SystemColors.ControlLight,
                    LinearGradientMode.Vertical);

            if (index == this.SelectedIndex)
            {
                buttonBrush = new System.Drawing.SolidBrush(SystemColors.ControlLightLight);
            }

            graph.FillPath(buttonBrush, path);
            buttonBrush.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="index"></param>
        /// <param name="path"></param>
        private void PaintTabBorder(System.Drawing.Graphics graph, int index, System.Drawing.Drawing2D.GraphicsPath path)
        {
            Pen borderPen = new Pen(SystemColors.ControlDark);

            if (index == this.SelectedIndex)
            {
                borderPen = new Pen(ThemedColors.ToolBorder);
            }
            graph.DrawPath(borderPen, path);
            borderPen.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="index"></param>
        private void PaintTabImage(System.Drawing.Graphics graph, int index)
        {
            Image tabImage = null;
            if (this.TabPages[index].ImageIndex > -1 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageIndex];
            }
            else if (this.TabPages[index].ImageKey.Trim().Length > 0 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageKey];
            }
            if (tabImage != null)
            {
                Rectangle rect = this.GetTabRect(index);
                graph.DrawImage(tabImage, rect.Right - rect.Height - 4, 4, rect.Height - 2, rect.Height - 2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="index"></param>
        private void PaintTabText(System.Drawing.Graphics graph, int index)
        {
            Rectangle rect = this.GetTabRect(index);
            Rectangle rect2 = new Rectangle(rect.Left + 8, rect.Top + 1, rect.Width - 6, rect.Height);
            if (index == 0) rect2 = new Rectangle(rect.Left + rect.Height, rect.Top + 1, rect.Width - rect.Height, rect.Height);

            string tabtext = this.TabPages[index].Text;

            System.Drawing.StringFormat format = new System.Drawing.StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            Brush forebrush = null;

            if (this.TabPages[index].Enabled == false)
            {
                forebrush = SystemBrushes.ControlDark;
            }
            else
            {
                forebrush = SystemBrushes.ControlText;
            }

            Font tabFont = this.Font;
            if (index == this.SelectedIndex)
            {
                tabFont = new Font(this.Font, FontStyle.Bold);
                if (index == 0)
                {
                    rect2 = new Rectangle(rect.Left + rect.Height, rect.Top + 1, rect.Width - rect.Height + 5, rect.Height);
                }
            }

            graph.DrawString(tabtext, tabFont, forebrush, rect2, format);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void PaintTheTabPageBorder(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                Rectangle borderRect = this.TabPages[0].Bounds;
                borderRect.Inflate(1, 1);
                ControlPaint.DrawBorder(e.Graphics, borderRect, ThemedColors.ToolBorder, ButtonBorderStyle.Solid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void PaintTheSelectedTab(System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle selrect;
            int selrectRight = 0;

            switch (this.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    selrect = this.GetTabRect(this.SelectedIndex);
                    selrectRight = selrect.Right;
                    e.Graphics.DrawLine(SystemPens.ControlLightLight, selrect.Left + 2, selrect.Bottom + 1, selrectRight - 2, selrect.Bottom + 1);
                    break;
                default:
                    selrect = this.GetTabRect(this.SelectedIndex);
                    selrectRight = selrect.Right;
                    e.Graphics.DrawLine(SystemPens.ControlLightLight, selrect.Left + 6 - selrect.Height, selrect.Bottom + 1, selrectRight - 2, selrect.Bottom + 1);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private System.Drawing.Drawing2D.GraphicsPath GetPath(int index)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.Reset();

            Rectangle rect = this.GetTabRect(index);

            if (index == 0)
            {
                path.AddLine(rect.Left + 1, rect.Bottom + 1, rect.Left + rect.Height, rect.Top + 2);
                path.AddLine(rect.Left + rect.Height + 4, rect.Top, rect.Right - 3, rect.Top);
                path.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom + 1);
            }
            else
            {
                if (index == this.SelectedIndex)
                {
                    path.AddLine(rect.Left + 1, rect.Top + 5, rect.Left + 4, rect.Top + 2);
                    path.AddLine(rect.Left + 8, rect.Top, rect.Right - 3, rect.Top);
                    path.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom + 1);
                    path.AddLine(rect.Right - 1, rect.Bottom + 1, rect.Left + 1, rect.Bottom + 1);
                }
                else
                {
                    path.AddLine(rect.Left, rect.Top + 6, rect.Left + 4, rect.Top + 2);
                    path.AddLine(rect.Left + 8, rect.Top, rect.Right - 3, rect.Top);
                    path.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom + 1);
                    path.AddLine(rect.Right - 1, rect.Bottom + 1, rect.Left, rect.Bottom + 1);
                }

            }
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum TabControlDisplayManager
        {
            Default,
            Custom
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1d;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr hFont = this.Font.ToHfont();
            SendMessage(this.Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
            this.ItemSize = new Size(0, this.Font.Height + 2);
        }
    }
}