﻿using System;
using System.Windows;
using System.Runtime.InteropServices;

namespace KinectV2MouseControl
{
    class MouseControl
    {
        public static void MouseLeftDown()
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
        }
        public static void MouseLeftUp()
        {
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static void DoMouseClick()
        {
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static void ArrowLeft()
        {
            keybd_event(0x25, 0, 0, UIntPtr.Zero); // FIXME: update to SendInput
            keybd_event(0x25, 0, 2, UIntPtr.Zero); // >:(
        }

        public static void ArrowRight()
        {
            keybd_event(0x27, 0, 0, UIntPtr.Zero);
            keybd_event(0x27, 0, 2, UIntPtr.Zero);
        }

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        // CHANGE TO SENDINPUT
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);
        [DllImport("user32.dll")]
        static extern void keybd_event(uint keycode, uint scancode, uint flags, UIntPtr extraInfo);
        [Flags]
        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

    }
}
