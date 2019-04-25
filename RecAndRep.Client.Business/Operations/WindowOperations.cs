using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace RecAndRep.Client.Business.Operations
{
    public class WindowOperations
    {

        public static bool SetWindowByProcessName(string name)
        {
            var proc = Process.GetProcessesByName(name).FirstOrDefault();
            if (proc != null && proc.MainWindowHandle != IntPtr.Zero)
            {
                SetForegroundWindow(proc.MainWindowHandle);
                return true;
            }
            return false;
        }

        public static Tuple<int, int> GetActiveWindowCoordinates()
        {
            GetWindowRect(GetForegroundWindow(), out RECT lpRect);
            return Tuple.Create(lpRect.Left, lpRect.Top);
        }


        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner  
            public int Top;         // y position of upper-left corner  
            public int Right;       // x position of lower-right corner  
            public int Bottom;      // y position of lower-right corner  
        }







    }
}
