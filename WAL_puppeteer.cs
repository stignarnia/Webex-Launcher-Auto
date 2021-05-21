using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace WAL_puppeteer
{
    public partial class WAL_puppeteer
    {
        public static async Task DownloadSchedule()
        {
            int i;
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var browserFetcherOptions = new BrowserFetcherOptions { Path = downloadPath };
            var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            await browserFetcher.DownloadAsync();

            var list = Directory.GetDirectories(downloadPath);
            foreach (string isThisChrome in list)
            {
                if (isThisChrome.Contains("Win64-"))
                {
                    downloadPath = isThisChrome + @"\chrome-win\chrome.exe";
                    break;
                }
            }

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = false, ExecutablePath = downloadPath });
            Page page = await browser.NewPageAsync();
            await page.GoToAsync("https://aunicalogin.polimi.it/aunicalogin/aunicalogin.jsp?id_servizio=376&profile=0&lang=IT&polij_device_category=DESKTOP&__pj0=0&__pj1=b015f2f9bdbbc23774f309454d1cb027");
            for (i = 0; i < 4; i++)
            {
                try
                {
                    await page.WaitForSelectorAsync("#matricolaELinkcambio");
                    break;
                }
                catch
                {
                    ;
                }
            }
            if (i != 4)
            {
                await page.GoToAsync("https://servizionline.polimi.it/portaleservizi/portaleservizi/controller/servizi/Servizi.do?evn_srv=evento&idServizio=398");
                try
                {
                    await page.WaitForSelectorAsync("#testuale");
                    string html = await page.GetContentAsync();
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\schedule.html", html);
                } catch
                {
                    ;
                }
            }
            await browser.CloseAsync();
            return;
        }
    }
}