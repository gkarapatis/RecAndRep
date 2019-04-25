using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.Common.Business.HookManager
{
    class MouseHookManager : HookManagerBase<MouseLLHookStruct>
    {
        protected override int HookId => WindowsConstants.WH_MOUSE_LL;

        public MouseHookManager() : base()
        {

        }

        //
        // Summary:
        //     Represents the method that will handle the MouseDown, MouseUp, or MouseMove event
        //     of a form, control, or other component.
        //
        // Parameters:
        //   sender:
        //     The source of the event.
        //
        //   e:
        //     A System.Windows.Forms.MouseEventArgs that contains the event data.
        public delegate void MouseEventHandler(object sender, MouseEventArgs e);

        private static event MouseEventHandler s_MouseClick;
        /// <summary>
        /// Occurs when a click was performed by the mouse. 
        /// </summary>
        public event MouseEventHandler MouseClick
        {
            add
            {
                //EnsureSubscribedToGlobalMouseEvents();
                s_MouseClick += value;
            }
            remove
            {
                s_MouseClick -= value;
                //TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        /// A callback function which will be called every Time a mouse activity detected.
        /// </summary>
        /// <param name="nCode">
        /// [in] Specifies whether the hook procedure must process the message. 
        /// If nCode is HC_ACTION, the hook procedure must process the message. 
        /// If nCode is less than zero, the hook procedure must pass the message to the 
        /// CallNextHookEx function without further processing and must return the 
        /// value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies whether the message was sent by the current thread. 
        /// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
        /// </param>
        /// <param name="lParam">
        /// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
        /// </param>
        /// <returns>
        /// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
        /// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
        /// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
        /// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
        /// procedure does not call CallNextHookEx, the return value should be zero. 
        /// </returns>
        protected override bool CustomHookProc(int nCode, int wParam, MouseLLHookStruct mouseHookStruct)
        {
            //detect button clicked 
            MouseButtons button = MouseButtons.None;
            short mouseDelta = 0;
            int clickCount = 0;
           // bool mouseDown = false;
           // bool mouseUp = false;

            switch (wParam)
            {
                case WindowsConstants.WM_LBUTTONDOWN:
             //       mouseDown = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case WindowsConstants.WM_LBUTTONUP:
             //       mouseUp = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
            }

            //generate event 
            MouseEventExtArgs e = new MouseEventExtArgs(
                                               button,
                                               clickCount,
                                               mouseHookStruct.Point.X,
                                               mouseHookStruct.Point.Y,
                                               mouseDelta);

            if (s_MouseClick != null && clickCount > 0)
            {
                s_MouseClick.Invoke(null, e);
            }

            if (e.Handled)
            {
                return false;
            }
            return true;
        }

    }
}
