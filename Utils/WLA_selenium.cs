using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using Webex_Launcher_Auto;

namespace WLA_selenium
{
    public partial class WLA_selenium
    {
        public static void DownloadScheduleGecko()
        {
            int i;
            string curPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            try
            {
                Process.Start(curPath + @"\geckodriver.exe");
                FirefoxDriverService serviceCR = FirefoxDriverService.CreateDefaultService(curPath);
                serviceCR.Port = 4444;
                IWebDriver browser = new FirefoxDriver(serviceCR);
                browser.Navigate().GoToUrl("https://aunicalogin.polimi.it/aunicalogin/aunicalogin.jsp?id_servizio=376&profile=0&lang=IT&polij_device_category=DESKTOP&__pj0=0&__pj1=b015f2f9bdbbc23774f309454d1cb027");
                for (i = 0; i < 240; i++)
                {
                    try
                    {
                        IWebElement l = browser.FindElement(By.Id("matricolaELinkcambio"));
                        break;
                    }
                    catch
                    {
                        string l = browser.Title;
                    }
                    Thread.Sleep(500);
                }
                if (i != 240)
                {
                    browser.Navigate().GoToUrl("https://servizionline.polimi.it/portaleservizi/portaleservizi/controller/servizi/Servizi.do?evn_srv=evento&idServizio=398");
                    for (i = 0; i < 240; i++)
                    {
                        try
                        {
                            IWebElement l = browser.FindElement(By.Id("testuale"));
                            string html = browser.PageSource;
                            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\schedule.html", html);
                            break;
                        }
                        catch
                        {
                            string l = browser.Title;
                        }
                        Thread.Sleep(500);
                    }
                }
                browser.Quit();
            }
            catch
            {
                ;
            }
            Program.Terminate("geckodriver");
        }
    }
}