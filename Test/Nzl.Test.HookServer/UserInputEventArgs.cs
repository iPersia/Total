namespace Nzl.Test.HookServer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.ComponentModel;

    public class UserInputEventArgs : EventArgs
    {
        public string Input
        {
            get;
            set;
        }
    }
}
