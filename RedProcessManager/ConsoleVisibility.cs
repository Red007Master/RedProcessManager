using System;
using System.Runtime.InteropServices;

class ConsoleVisibility
{
    const int SW_HIDE = 0;
    const int SW_Norm = 4;

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [STAThread]

    static public void Show()
    {
        Other.DebugLog("ConsoleVisibility Try set: Show");

        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_Norm);

        Other.DebugLog("ConsoleVisibility Success set: Show");
    }
    static public void Hide()
    {
        Other.DebugLog("ConsoleVisibility Try set: Hide");

        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);

        Other.DebugLog("ConsoleVisibility Success set: Hide");
    }
}
