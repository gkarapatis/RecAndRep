using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.Common.Business.HookManager
{
    class KeyboardHookManager : HookManagerBase<KeyboardHookStruct>
    {
        protected override int HookId => WindowsConstants.WH_KEYBOARD_LL;

        public KeyboardHookManager() : base()
        {

        }

        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);

        private event KeyPressEventHandler s_KeyPress;

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <remarks>
        /// Key events occur in the following order: 
        /// <list type="number">
        /// <item>KeyDown</item>
        /// <item>KeyPress</item>
        /// <item>KeyUp</item>
        /// </list>
        ///The KeyPress event is not raised by noncharacter keys; however, the noncharacter keys do raise the KeyDown and KeyUp events. 
        ///Use the KeyChar property to sample keystrokes at run time and to consume or modify a subset of common keystrokes. 
        ///To handle keyboard events only in your application and not enable other applications to receive keyboard events, 
        /// set the KeyPressEventArgs.Handled property in your form's KeyPress event-handling method to <b>true</b>. 
        /// </remarks>
        public event KeyPressEventHandler KeyPress
        {
            add
            {
                //EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyPress += value;
            }
            remove
            {
                s_KeyPress -= value;
                //TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        protected override bool CustomHookProc(int nCode, int wParam, KeyboardHookStruct MyKeyboardHookStruct)
        {
            //indicates if any of underlaing events set e.Handled flag
            bool handled = false;

            // raise KeyPress 
            if (s_KeyPress != null && wParam == WindowsConstants.WM_KEYDOWN)
            {
                bool isDownShift = ((GetKeyState(WindowsConstants.VK_SHIFT) & 0x80) == 0x80 ? true : false);
                bool isDownCapslock = (GetKeyState(WindowsConstants.VK_CAPITAL) != 0 ? true : false);

                byte[] keyState = new byte[256];
                GetKeyboardState(keyState);
                byte[] inBuffer = new byte[2];
                if (ToAscii(MyKeyboardHookStruct.VirtualKeyCode,
                          MyKeyboardHookStruct.ScanCode,
                          keyState,
                          inBuffer,
                          MyKeyboardHookStruct.Flags) == 1)
                {
                    char key = (char)inBuffer[0];
                    if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
                    KeyPressEventArgs e = new KeyPressEventArgs(key);
                    s_KeyPress.Invoke(null, e);
                    handled = handled || e.Handled;
                }
            }

            return handled;
        }

        /// <summary>
        /// The ToAscii function translates the specified virtual-key code and keyboard 
        /// state to the corresponding character or characters. The function translates the code 
        /// using the input language and physical keyboard layout identified by the keyboard layout handle.
        /// </summary>
        /// <param name="uVirtKey">
        /// [in] Specifies the virtual-key code to be translated. 
        /// </param>
        /// <param name="uScanCode">
        /// [in] Specifies the hardware scan code of the key to be translated. 
        /// The high-order bit of this value is set if the key is up (not pressed). 
        /// </param>
        /// <param name="lpbKeyState">
        /// [in] Pointer to a 256-byte array that contains the current keyboard state. 
        /// Each element (byte) in the array contains the state of one key. 
        /// If the high-order bit of a byte is set, the key is down (pressed). 
        /// The low bit, if set, indicates that the key is toggled on. In this function, 
        /// only the toggle bit of the CAPS LOCK key is relevant. The toggle state 
        /// of the NUM LOCK and SCROLL LOCK keys is ignored.
        /// </param>
        /// <param name="lpwTransKey">
        /// [out] Pointer to the buffer that receives the translated character or characters. 
        /// </param>
        /// <param name="fuState">
        /// [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 
        /// </param>
        /// <returns>
        /// If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values. 
        /// Value Meaning 
        /// 0 The specified virtual key has no translation for the current state of the keyboard. 
        /// 1 One character was copied to the buffer. 
        /// 2 Two characters were copied to the buffer. This usually happens when a dead-key character 
        /// (accent or diacritic) stored in the keyboard layout cannot be composed with the specified 
        /// virtual key to form a single character. 
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32")]
        private static extern int ToAscii(
            int uVirtKey,
            int uScanCode,
            byte[] lpbKeyState,
            byte[] lpwTransKey,
            int fuState);

        /// <summary>
        /// The GetKeyboardState function copies the status of the 256 virtual keys to the 
        /// specified buffer. 
        /// </summary>
        /// <param name="pbKeyState">
        /// [in] Pointer to a 256-byte array that contains keyboard key states. 
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. 
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// The GetKeyState function retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled 
        /// (on, off—alternating each time the key is pressed). 
        /// </summary>
        /// <param name="vKey">
        /// [in] Specifies a virtual key. If the desired virtual key is a letter or digit (A through Z, a through z, or 0 through 9), nVirtKey must be set to the ASCII value of that character. For other keys, it must be a virtual-key code. 
        /// </param>
        /// <returns>
        /// The return value specifies the status of the specified virtual key, as follows: 
        ///If the high-order bit is 1, the key is down; otherwise, it is up.
        ///If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, and off when the key is untoggled.
        /// </returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/ms646301.aspx</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

    }
}
