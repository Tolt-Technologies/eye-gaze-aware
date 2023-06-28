using System;
using System.Text;
using System.Threading;
using ToltTech.Integration.Win32;

namespace FindGazeAware
{
    internal static class Program
    {
        static void Main()
        {
            while (true)
            {
                var foregroundWindowHwnd = User32.GetForegroundWindow();

                Console.WriteLine($"{GetWindowTitleForHwnd(foregroundWindowHwnd)} - IsGazeAware: {ToltTech.Integration.GazeAware.IsGazeAware()}");

                Thread.Sleep(5000);
            }
        }

        private static string GetWindowTitleForHwnd(IntPtr hwnd)
        {
            int size = User32.GetWindowTextLength(hwnd);
            if (size++ > 0 && User32.IsWindowVisible(hwnd))
            {
                StringBuilder sb = new StringBuilder(size);
                User32.GetWindowText(hwnd, sb, size);

                return sb.ToString();
            }

            return "";
        }
    }
}