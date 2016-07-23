namespace Nzl.Hook
{
    using System;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.ComponentModel;

    /// <summary>
    /// Extension class of key event args.
    /// </summary>
    public class KeyExEventArgs : KeyEventArgs
    {
        /// <summary>
        /// The hook struct flags.
        /// </summary>
        private int flags;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="keyData">Key data.</param>
        /// <param name="flags">The flags.</param>
        public KeyExEventArgs(Keys keyData, int flags)
            : base(keyData)
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
