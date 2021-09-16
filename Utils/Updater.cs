using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Updater
{
    public partial class Updater
    {
        public static void CheckForUpdates()
        {
            WebClient webClient = new WebClient();
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion.Substring(0, 3);

            try
            {
                string latest = webClient.DownloadString("https://github.com/stignarnia/webex-launcher-auto/releases/latest/");
                if (!latest.Contains("/tree/v" + version))
                {
                    if (MessageBox.Show("È disponibile un nuovo aggiornamento, vuoi installarlo adesso?",
                        "Aggiornamento disponibile", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        DoUpdate();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        public static void DoUpdate()
        {
            WebClient webClient = new WebClient();

            try
            {
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\webexlauncher\updateinstaller");
                webClient.DownloadFile("https://github.com/stignarnia/webex-launcher-auto/releases/latest/download/Webex-Auto-Launcher-Win64-Setup.msi", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\webexlauncher\updateinstaller\Webex-Auto-Launcher-Win64-Setup.msi");
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\webexlauncher\updateinstaller\Webex-Auto-Launcher-Win64-Setup.msi");
                Environment.Exit(0);
            }
            catch
            {
                ;
            }
        }
    }
}