using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.Common.Business.HookManager
{
    /// <summary>
    /// Provides data for the MouseClickExt and MouseMoveExt events. It also provides a property Handled.
    /// Set this property to <b>true</b> to prevent further processing of the event in other applications.
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the MouseEventArgs class. 
        /// </summary>
        /// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
        /// <param name="clicks">The number of times a mouse button was pressed.</param>
        /// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
        /// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
        /// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        { }

        /// <summary>
        /// Initializes a new instance of the MouseEventArgs class. 
        /// </summary>
        /// <param name="e">An ordinary <see cref="MouseEventArgs"/> argument to be extended.</param>
        internal MouseEventExtArgs(MouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        { }

        private bool m_Handled;

        /// <summary>
        /// Set this property to <b>true</b> inside your event handler to prevent further processing of the event in other applications.
        /// </summary>
        public bool Handled
        {
            get { return m_Handled; }
            set { m_Handled = value; }
        }
    }


    //
    // Summary:
    //     Provides data for the System.Windows.Forms.Control.MouseUp, System.Windows.Forms.Control.MouseDown,
    //     and System.Windows.Forms.Control.MouseMove events.
    [ComVisible(true)]
    public class MouseEventArgs : EventArgs
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.MouseEventArgs class.
        //
        // Parameters:
        //   button:
        //     One of the System.Windows.Forms.MouseButtons values indicating which mouse button
        //     was pressed.
        //
        //   clicks:
        //     The number of times a mouse button was pressed.
        //
        //   x:
        //     The x-coordinate of a mouse click, in pixels.
        //
        //   y:
        //
        //   delta:
        //     A signed count of the number of detents the wheel has rotated.
        public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        {
            Button = button;
            Clicks = clicks;
            X = x;
            Y = y;
            Delta = delta;
        }

        //
        // Summary:
        //     Gets which mouse button was pressed.
        //
        // Returns:
        //     One of the System.Windows.Forms.MouseButtons values.
        public MouseButtons Button { get; }
        //
        // Summary:
        //     Gets the number of times the mouse button was pressed and released.
        //
        // Returns:
        //     An System.Int32 containing the number of times the mouse button was pressed and
        //     released.
        public int Clicks { get; }
        //
        // Summary:
        //     Gets the x-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The x-coordinate of the mouse, in pixels.
        public int X { get; }
        //
        // Summary:
        //     Gets the y-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The y-coordinate of the mouse, in pixels.
        public int Y { get; }
        //
        // Summary:
        //     Gets a signed count of the number of detents the mouse wheel has rotated. A detent
        //     is one notch of the mouse wheel.
        //
        // Returns:
        //     A signed count of the number of detents the mouse wheel has rotated.
        public int Delta { get; }
        //
        // Summary:
        //     Gets the location of the mouse during the generating mouse event.
        //
        // Returns:
        //     A System.Drawing.Point containing the x- and y- coordinate of the mouse, in pixels.
        // public Point Location { get; }
    }

    //
    // Summary:
    //     Provides data for the System.Windows.Forms.Control.KeyPress event.
    [ComVisible(true)]
    public class KeyPressEventArgs : EventArgs
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.KeyPressEventArgs class.
        //
        // Parameters:
        //   keyChar:
        //     The ASCII character corresponding to the key the user pressed.
        public KeyPressEventArgs(char keyChar)
        {
            KeyChar = keyChar;
        }

        //
        // Summary:
        //     Gets or sets the character corresponding to the key pressed.
        //
        // Returns:
        //     The ASCII character that is composed. For example, if the user presses SHIFT
        //     + K, this property returns an uppercase K.
        public char KeyChar { get; set; }
        //
        // Summary:
        //     Gets or sets a value indicating whether the System.Windows.Forms.Control.KeyPress
        //     event was handled.
        //
        // Returns:
        //     true if the event is handled; otherwise, false.
        public bool Handled { get; set; }
    }
}
