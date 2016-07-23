namespace Nzl.Hook
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    
    /// <summary>
    /// Sealed class for hooking the user activity like mouse movement and keyboard pressing.
    /// </summary>
    public sealed class UserActivityHook
    {
        #region Hook thread pointer.
        /// <summary>
        /// The mouse hook thread pointer.
        /// </summary>
        private IntPtr hMouseHook = IntPtr.Zero;

        /// <summary>
        /// The keyboard hook thread pointer.
        /// </summary>
        private IntPtr hKeyboardHook = IntPtr.Zero;
        #endregion

        #region Event keys.
        /// <summary>
        /// Mouse activity event key.
        /// </summary>
        private static readonly EventKey EventMouseActivity = new EventKey();

        /// <summary>
        /// Key down event key.
        /// </summary>
        private static readonly EventKey EventKeyDown = new EventKey();

        /// <summary>
        /// Key press event key.
        /// </summary>
        private static readonly EventKey EventKeyPress = new EventKey();

        /// <summary>
        /// Key up event key.
        /// </summary>
        private static readonly EventKey EventKeyUp = new EventKey();
        #endregion

        #region Static hook processs.
        /// <summary>
        /// 
        /// </summary>
        private static HookProc MouseHookProcedure;

        /// <summary>
        /// 
        /// </summary>
        private static HookProc KeyboardHookProcedure;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private readonly EventSet Events = new EventSet();

        #region Windows structure definitions
        /// <summary>
        /// POINT struct.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class POINT
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Mouse hook struct.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            public POINT pt;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        /// <summary>
        /// Mouse LL hook struct.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        
        /// <summary>
        /// Keyboard hook struct.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;

            public int flags;
            public int time;

            public int dwExtraInfo;
        }
        #endregion        

        #region Windows constants
        private const int WH_MOUSE_LL = 14;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE = 7;
        private const int WH_KEYBOARD = 2;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        private const byte VK_SHIFT = 0x10;
        private const byte VK_CAPITAL = 0x14;
        private const byte VK_NUMLOCK = 0x90;
        #endregion

        #region Ctors & Dtor.
        /// <summary>
        /// Ctor.
        /// </summary>
        public UserActivityHook()
        {
            Start();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="InstallMouseHook">The flag indicates whether the mouse hook will be installed.</param>
        /// <param name="InstallKeyboardHook">The flag indicates whether the keyboard hook will be installed.</param>
        public UserActivityHook(bool InstallMouseHook, bool InstallKeyboardHook)
        {
            Start(InstallMouseHook, InstallKeyboardHook);
        }

        /// <summary>
        /// Dtor.
        /// </summary>
        ~UserActivityHook()
        {
            //uninstall hooks and do not throw exceptions
            Stop(true, true, false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Mouse activity event handlers.
        /// </summary>
        public event EventHandler<MouseExEventArgs> MouseActivity
        {
            add
            {
                this.Events.Add(EventMouseActivity, value);
            }

            remove
            {
                this.Events.Remove(EventMouseActivity, value);
            }
        }

        /// <summary>
        /// Key down activity event handlers.
        /// </summary>
        public event EventHandler<KeyExEventArgs> KeyDown
        {
            add
            {
                this.Events.Add(EventKeyDown, value);
            }

            remove
            {
                this.Events.Remove(EventKeyDown, value);
            }
        }

        /// <summary>
        /// Key press activity event handlers.
        /// </summary>
        public event EventHandler<KeyPressExEventArgs> KeyPress
        {
            add
            {
                this.Events.Add(EventKeyPress, value);
            }

            remove
            {
                this.Events.Remove(EventKeyPress, value);
            }

        }

        /// <summary>
        /// Key activity event handlers.
        /// </summary>
        public event EventHandler<KeyExEventArgs> KeyUp
        {
            add
            {
                this.Events.Add(EventKeyUp, value);
            }

            remove
            {
                this.Events.Remove(EventKeyUp, value);
            }
        }
        #endregion

        /// <summary>
        /// Start hooking.
        /// </summary>
        public void Start()
        {
            this.Start(true, true);
        }

        /// <summary>
        /// Start hooking.
        /// </summary>
        /// <param name="installMouseHook">The flag indicates whether the mouse hook will be installed.</param>
        /// <param name="installKeyboardHook">The flag indicates whether the keyboard hook will be installed.</param>
        public void Start(bool installMouseHook, bool installKeyboardHook)
        {
            if (hMouseHook == IntPtr.Zero && installMouseHook)
            {
                MouseHookProcedure = new HookProc(MouseHookProc);
                hMouseHook = Win32API.SetWindowsHookEx(
                    WH_MOUSE_LL,
                    MouseHookProcedure,
                    Win32API.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName),//Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                    0//Thread.CurrentThread.ManagedThreadId//0
               );

                if (hMouseHook == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    Stop(true, false, false);

                    throw new Win32Exception(errorCode);
                }
            }


            if (hKeyboardHook == IntPtr.Zero && installKeyboardHook)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                //install hook
                hKeyboardHook = Win32API.SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    KeyboardHookProcedure,
                    Win32API.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName),//Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);

                //If SetWindowsHookEx fails.
                if (hKeyboardHook == IntPtr.Zero)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //do cleanup
                    Stop(false, true, false);
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        /// <summary>
        /// Stop hooking.
        /// </summary>
        public void Stop()
        {
            this.Stop(true, true, true);
        }

        /// <summary>
        /// Stop hooking.
        /// </summary>
        /// <param name="uninstallMouseHook">The flag indicates whether the mouse hook will be uninstalled.</param>
        /// <param name="uninstallKeyboardHook">The flag indicates whether the keyboard hook will be uninstalled.</param>
        /// <param name="throwExceptions">The flag indicates whether exceptions can be throwed.</param>
        public void Stop(bool uninstallMouseHook, bool uninstallKeyboardHook, bool throwExceptions)
        {
            //if mouse hook set and must be uninstalled
            if (hMouseHook != IntPtr.Zero && uninstallMouseHook)
            {
                //uninstall hook
                bool retMouse = Win32API.UnhookWindowsHookEx(hMouseHook);
                //reset invalid handle
                hMouseHook = IntPtr.Zero;
                //if failed and exception must be thrown
                if (retMouse == false && throwExceptions)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }

            //if keyboard hook set and must be uninstalled
            if (hKeyboardHook != IntPtr.Zero && uninstallKeyboardHook)
            {
                //uninstall hook
                bool retKeyboard = Win32API.UnhookWindowsHookEx(hKeyboardHook);
                //reset invalid handle
                hKeyboardHook = IntPtr.Zero;
                //if failed and exception must be thrown
                if (retKeyboard == false && throwExceptions)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        /// <summary>
        /// Override the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Hook Information:\n"
                  + "Mouse Hook Ptr:\t" + this.hMouseHook + "\n"
                  + "Keyboard Hook Ptr:\t" + this.hKeyboardHook + "\n";
        }

        /// <summary>
        /// The mouse hook process.
        /// </summary>
        /// <param name="nCode">The code.</param>
        /// <param name="wParam">The param w.</param>
        /// <param name="lParam">The param l.</param>
        /// <returns>The pointer of mouse hook process.</returns>
        private IntPtr MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // if ok and someone listens to our events
            EventHandler<MouseExEventArgs> handler = this.Events[EventMouseActivity] as EventHandler<MouseExEventArgs>;
            if ((nCode >= 0) && (handler != null))
            {
                //Marshall the data from callback.
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                //detect button clicked
                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                switch ((int)wParam)
                {
                    case WM_LBUTTONDOWN:
                        // case WM_LBUTTONUP: 
                        // case WM_LBUTTONDBLCLK: 
                        button = MouseButtons.Left;
                        break;
                    case WM_RBUTTONDOWN:
                        // case WM_RBUTTONUP: 
                        // case WM_RBUTTONDBLCLK: 
                        button = MouseButtons.Right;
                        break;
                    case WM_MOUSEWHEEL:
                        // If the message is WM_MOUSEWHEEL, the high-order word of mouseData member is the wheel delta. 
                        // One wheel click is defined as WHEEL_DELTA, which is 120. 
                        // (value >> 16) & 0xffff; retrieves the high-order word from the given 32-bit value
                        mouseDelta = unchecked((short)((mouseHookStruct.mouseData >> 16) & 0xffff));
                        //TODO: X BUTTONS (I havent them so was unable to test)
                        //If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, 
                        //or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
                        //and the low-order word is reserved. This value can be one or more of the following values. 
                        //Otherwise, mouseData is not used. 
                        break;
                }

                //double clicks
                int clickCount = 0;
                if (button != MouseButtons.None)
                    if ((int)wParam == WM_LBUTTONDBLCLK || (int)wParam == WM_RBUTTONDBLCLK)
                        clickCount = 2;
                    else
                        clickCount = 1;

                //generate event 
                MouseExEventArgs e = new MouseExEventArgs(
                   button,
                   clickCount,
                   mouseHookStruct.pt.x,
                   mouseHookStruct.pt.y,
                   mouseDelta,
                   mouseHookStruct.flags
                );

                //raise it
                handler(this, e);
            }
            //call next hook
            return Win32API.CallNextHookEx(hMouseHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// The keyboard hook process.
        /// </summary>
        /// <param name="nCode">The code.</param>
        /// <param name="wParam">The param w.</param>
        /// <param name="lParam">The param l.</param>
        /// <returns>The pointer of keyboard hook process.</returns>
        private IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //indicates if any of underlaing events set e.Handled flag
            bool handled = false;

            EventHandler<KeyExEventArgs> handlerKeyDown = this.Events[EventKeyDown] as EventHandler<KeyExEventArgs>;
            EventHandler<KeyExEventArgs> handlerKeyUp = this.Events[EventKeyUp] as EventHandler<KeyExEventArgs>;
            EventHandler<KeyPressExEventArgs> handlerKeyPress = this.Events[EventKeyPress] as EventHandler<KeyPressExEventArgs>;

            //it was ok and someone listens to events
            if ((nCode >= 0) && (handlerKeyDown != null || handlerKeyUp != null || handlerKeyPress != null))
            {
                //read structure KeyboardHookStruct at lParam
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                //raise KeyDown
                if (handlerKeyDown != null && ((int)wParam == WM_KEYDOWN || (int)wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyExEventArgs e = new KeyExEventArgs(keyData, MyKeyboardHookStruct.flags);
                    handlerKeyDown(this, e);
                    handled = handled || e.Handled;
                }

                // raise KeyPress
                if (handlerKeyPress != null && (int)wParam == WM_KEYDOWN)
                {
                    bool isDownShift = ((Win32API.GetKeyState(VK_SHIFT) & 0x80) == 0x80 ? true : false);
                    bool isDownCapslock = (Win32API.GetKeyState(VK_CAPITAL) != 0 ? true : false);

                    byte[] keyState = new byte[256];
                    Win32API.GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (Win32API.ToAscii(MyKeyboardHookStruct.vkCode,
                          MyKeyboardHookStruct.scanCode,
                          keyState,
                          inBuffer,
                          MyKeyboardHookStruct.flags) == 1)
                    {
                        char key = (char)inBuffer[0];
                        if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);
                        KeyPressExEventArgs e = new KeyPressExEventArgs(key, MyKeyboardHookStruct.flags);
                        handlerKeyPress(this, e);
                        handled = handled || e.Handled;
                    }
                }

                // raise KeyUp
                if (handlerKeyUp != null && ((int)wParam == WM_KEYUP || (int)wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyExEventArgs e = new KeyExEventArgs(keyData, MyKeyboardHookStruct.flags);

                    handlerKeyUp(this, e);
                    handled = handled || e.Handled;
                }

            }

            //if event handled in application do not handoff to other listeners
            if (handled)
                return (IntPtr)1;
            else
                return Win32API.CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }
    }
}
