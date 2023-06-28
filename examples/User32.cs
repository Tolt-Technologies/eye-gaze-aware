namespace ToltTech.Integration.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class User32
    {
        private const string DllName = "user32.dll";

        [DllImport(DllName)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(DllName)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport(DllName, SetLastError = true)]
        public static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport(DllName, SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(DllName, SetLastError = true)]
        public static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr RemoveProp(IntPtr hWnd, string lpString);

        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetProp(IntPtr hWnd, string lpString);
    }
}
