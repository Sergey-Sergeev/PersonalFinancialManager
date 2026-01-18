

using PersonalFinancialManager.source.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Application = System.Windows.Forms.Application;

namespace PersonalFinancialManager.source
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;

        static void HideConsole()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }


        [STAThread]
        static void Main()
        {
#if !DEBUG
            HideConsole();
#endif

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}