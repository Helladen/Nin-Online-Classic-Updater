using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Windows.Forms;
using Updater.Forms;

namespace Updater.Core
{
    internal static class Program
    {

        [STAThread]
        private static void Main()
        {
#if !DEBUG
            var identity = WindowsIdentity.GetCurrent();
            if (identity == null) throw new InvalidOperationException("Couldn't get the current user identity!");
            var principal = new WindowsPrincipal(identity);

            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show(@"The user account on this computer is not an Administrator. Please use an Administrator account and try again.");
                return;
            }
#endif

            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());     

        }
    }
}
