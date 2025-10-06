

using Application = System.Windows.Forms.Application;

namespace PersonalFinancialManager.source
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}