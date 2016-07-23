namespace Nzl.Hook
{
    using System;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.ComponentModel;

    /// <summary>
    /// Extension class of mouse event args.
    /// </summary>
    public class MouseExEventArgs : MouseEventArgs
    {
        /// <summary>
        /// The hook struct flags.
        /// </summary>
        private int flags;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="button">Button type.</param>
        /// <param name="clicks">Number of clicks.</param>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="delta">Delta value.</param>
        /// <param name="flags">The flags.</param>
        public MouseExEventArgs(MouseButtons button, int clicks, int x, int y, int delta, int flags)
            : base(button, clicks, x, y, delta)
        {
            this.flags = flags;
        }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public int Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }
    }
}