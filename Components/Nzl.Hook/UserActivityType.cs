using System;
using System.Collections.Generic;
using System.Text;

namespace Nzl.Hook
{
    public enum UserActivityType
    {
        /// <summary>
        /// Mouse Activity.
        /// </summary>
        Mouse = 0,

        /// <summary>
        /// Keyboard activity - key down.
        /// </summary>
        KeyDown = 1,

        /// <summary>
        /// Keyboard activity - key press.
        /// </summary>
        KeyPress = 2,

        /// <summary>
        /// Keyboard activity - key up.
        /// </summary>
        KeyUp = 3,

        /// <summary>
        /// Idle.
        /// </summary>
        Idle = 4,
    }
}
