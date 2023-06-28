using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using ToltTech.Integration.Win32;

namespace ToltTech.Integration
{
    public static class GazeAware
    {
        private const uint WS_TOBIIGAZEAWARE = 0x2;
        private const string EyeGazeAware = "eyegaze:aware";
        private static readonly IntPtr pStr = Marshal.StringToHGlobalUni(EyeGazeAware);

        public static void SetGazeAware(Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;

            // Tobii Computer Control gaze aware indicator
            var windowStyle = User32.GetWindowLong(hwnd, User32.GWL_EXSTYLE) | WS_TOBIIGAZEAWARE;
            if (User32.SetWindowLong(hwnd, User32.GWL_EXSTYLE, windowStyle) == 0)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            // MSFT Windows Input team proposal
            if (!User32.SetProp(hwnd, EyeGazeAware, pStr))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            window.Unloaded += (object sender, RoutedEventArgs e) => User32.RemoveProp(hwnd, EyeGazeAware);
        }

        public static bool IsGazeAware()
        {
            var foregroundWindowHwnd = User32.GetForegroundWindow();
            if (foregroundWindowHwnd == IntPtr.Zero)
            {
                // The foreground window can be NULL in certain circumstances, such as when a window is losing activation.
                return false;
            }

            var tobiiGazeAware = (User32.GetWindowLong(foregroundWindowHwnd, User32.GWL_EXSTYLE) & WS_TOBIIGAZEAWARE) == WS_TOBIIGAZEAWARE;
            var eyeGazeAware = User32.GetProp(foregroundWindowHwnd, EyeGazeAware) != IntPtr.Zero;

            return tobiiGazeAware || eyeGazeAware;
        }
    }
}