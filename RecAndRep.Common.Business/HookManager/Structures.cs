using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.Common.Business.HookManager
{
    /// <summary>
    /// The Point structure defines the X- and Y- coordinates of a point. 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/rectangl_0tiq.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    struct Point
    {
        /// <summary>
        /// Specifies the X-coordinate of the point. 
        /// </summary>
        public int X;
        /// <summary>
        /// Specifies the Y-coordinate of the point. 
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct MouseLLHookStruct
    {
        /// <summary>
        /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
        /// </summary>
        public Point Point;
        /// <summary>
        /// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. 
        /// The low-order word is reserved. A positive value indicates that the wheel was rotated forward, 
        /// away from the user; a negative value indicates that the wheel was rotated backward, toward the user. 
        /// One wheel click is defined as WHEEL_DELTA, which is 120. 
        ///If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP,
        /// or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
        /// and the low-order word is reserved. This value can be one or more of the following values. Otherwise, MouseData is not used. 
        ///XBUTTON1
        ///The first X button was pressed or released.
        ///XBUTTON2
        ///The second X button was pressed or released.
        /// </summary>
        public int MouseData;
        /// <summary>
        /// Specifies the event-injected flag. An application can use the following value to test the mouse Flags. Value Purpose 
        ///LLMHF_INJECTED Test the event-injected flag.  
        ///0
        ///Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
        ///1-15
        ///Reserved.
        /// </summary>
        public int Flags;
        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public int Time;
        /// <summary>
        /// Specifies extra information associated with the message. 
        /// </summary>
        public int ExtraInfo;
    }

    /// <summary>
    /// The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookstructures/cwpstruct.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    struct KeyboardHookStruct
    {
        /// <summary>
        /// Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
        /// </summary>
        public int VirtualKeyCode;
        /// <summary>
        /// Specifies a hardware scan code for the key. 
        /// </summary>
        public int ScanCode;
        /// <summary>
        /// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        public int Flags;
        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public int Time;
        /// <summary>
        /// Specifies extra information associated with the message. 
        /// </summary>
        public int ExtraInfo;
    }



    //
    // Summary:
    //     Specifies constants that define which mouse button was pressed.
    [ComVisible(true)]
    [Flags]
    public enum MouseButtons
    {
        //
        // Summary:
        //     No mouse button was pressed.
        None = 0,
        //
        // Summary:
        //     The left mouse button was pressed.
        Left = 1048576,
        //
        // Summary:
        //     The right mouse button was pressed.
        Right = 2097152,
        //
        // Summary:
        //     The middle mouse button was pressed.
        Middle = 4194304,
        //
        // Summary:
        //     The first XButton was pressed.
        XButton1 = 8388608,
        //
        // Summary:
        //     The second XButton was pressed.
        XButton2 = 16777216
    }
}
