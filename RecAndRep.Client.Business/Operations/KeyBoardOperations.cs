using System;
using System.Runtime.InteropServices;

namespace RecAndRep.Client.Business.Operations
{
    public class KeyBoardOperations
    {

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_LCONTROL = 0xA2; //Left Control key code
        public const int A = 0x41; //A key code
        public const int C = 0x43; //C key code

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        public static void keys_event_send(string message)
        {            
            SendMessage(WindowOperations.GetForegroundWindow(), 0x000C, 0, message);
        }

        [DllImport("user32.dll")]
        static extern int MapVirtualKey(int uCode, uint uMapType);

        public static void keybd_event()
        {
            keybd_event(C, 0, 0, 0);
        }
    }
}
