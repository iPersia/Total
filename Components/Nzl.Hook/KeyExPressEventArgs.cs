namespace Nzl.Hook
{
    using System;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.ComponentModel;

    /// <summary>
    /// Extension class of key press event args.
    /// </summary>
    public class KeyPressExEventArgs : KeyPressEventArgs
    {
        /// <summary>
        /// The hook struct flags.
        /// </summary>
        private int flags;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="keyChar">The char value pressed.</param>
        /// <param name="flags">The flags.</param>
        public KeyPressExEventArgs(char keyChar, int flags)
            : base(keyChar)
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
