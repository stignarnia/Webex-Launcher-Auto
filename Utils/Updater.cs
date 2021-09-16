using System.Diagnostics;
using System.Net;
using System.Reflection;

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

            /*try
            {*/
            string latest = webClient.DownloadString("https://github.com/stignarnia/webex-launcher-auto/releases/latest/");
            /*if (!latest.Contains(version))
            {*/
                DoUpdate();
            /*}*/
            /*}
            catch
            {
                ;
            }*/
        }

        public static void DoUpdate()
        {
            WebClient webClient = new WebClient();

            /*try
            {
            webClient.DownloadFile("https://github.com/stignarnia/webex-launcher-auto/releases/latest/download/Webex-Auto-Launcher-Win64.zip", "Webex-Auto-Launcher-Win64.zip");
            }
            catch
            {
                ;
            }*/
        }
    }
}