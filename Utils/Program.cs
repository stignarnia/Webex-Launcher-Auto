using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Webex_Launcher_Auto.Forms;

namespace Webex_Launcher_Auto
{
    public partial class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    SetProcessDPIAware();
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Updater.Updater.CheckForUpdates();
                Application.Run(new Home());
            }
        }

        public static string IsBrowserInstalled()
        {
            string exePath;
            RegistryKey key;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            exePath = IsPathAvailable(key);
            if(exePath == "")
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                exePath = IsPathAvailable(key);
            }
            
            return exePath;
        }

        public static string IsPathAvailable(RegistryKey key)
        {
            string exePath;

            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                exePath = subkey.GetValue("InstallLocation") as string;
                if (File.Exists(exePath + @"\" + Properties.Settings.Default["browser"].ToString().ToLower() + ".exe"))
                {
                    string path = exePath + @"\" + Properties.Settings.Default["browser"].ToString().ToLower() + ".exe";
                    return path;
                }
            }

            // NOT FOUND
            return "";
        }

        public static int Is_Webex_Open()
        {
            Process[] procs = Process.GetProcessesByName("atmgr");

            if (procs.Length == 0)
            {
                return 0;
            }
            else
            {
                foreach (Process p in procs)
                {
                    if (p.MainWindowTitle.Length > 0)
                    {
                        return 2;
                    }
                }
                return 1;
            }
        }

        public static void Terminate (string program)
        {
            try
            {
                Process[] procs = Process.GetProcessesByName(program);
                foreach (Process p in procs)
                {
                    p.Kill();
                    Thread.Sleep(250);
                }
            }
            catch
            {
                ;
            }
        }

        public static void Subject_Button_Common(string nome, string cognome)
        {
            int i, tries;
            string name, surname;
            tries = Int32.Parse(Properties.Settings.Default["attesa"].ToString()) * 4;

            name = Properties.Settings.Default[nome].ToString();
            surname = Properties.Settings.Default[cognome].ToString();
            Process.Start(Properties.Settings.Default["browser"].ToString() + ".exe", "https://politecnicomilano.webex.com/meet/" + name.ToLower() + "." + surname.ToLower());

            for (i = 0; i < tries; i++)
            {
                if (Is_Webex_Open() == 2)
                {
                    SendKeys.SendWait("%{Tab}");
                    Thread.Sleep(500);
                    SendKeys.SendWait("^(w)");
                    Thread.Sleep(500);
                    if (Process.GetProcessesByName(Properties.Settings.Default["browser"].ToString()).Length != 0)
                    {
                        SendKeys.SendWait("%{Tab}");
                    }
                    break;
                }
                Thread.Sleep(250);
            }

            if (i == tries)
            {
                SendKeys.Send("%{Tab}");
                if (MessageBox.Show("Webex non si è aperto correttamente. Se mi sono sbagliato, prova ad aumentare il tempo di attesa nelle impostazioni," +
                    " altrimenti segui le istruzioni riportate nella wiki, vuoi aprirla?",
                    "Errore", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    Process.Start("https://github.com/stignarnia/Webex-Launcher-Auto/wiki/Come-iniziare");
                }
            }
            Application.Exit();
        }

        public static bool Sync()
        {
            int i, j;
            string[] subs, subs2, subjects = new string[4], names = new string[4 * 4], surnames = new string[4 * 4];
            string pattern, subject, source, path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\schedule.html";
            SubjectChoice SubjectSelection;

            if (Properties.Settings.Default["browser"].ToString() == "Firefox")
            {
                WLA_selenium.WLA_selenium.DownloadScheduleGecko();
            }
            else
            {
                Task task = Task.Run(async () => await WLA_puppeteer.WLA_puppeteer.DownloadScheduleChromium());
                task.Wait();
            }
            

            if (File.Exists(path))
            {
                source = File.ReadAllText(path);
                File.Delete(path);
            }
            else
            {
                return false;
            }
            pattern = "[0-9][0-9][0-9][0-9][0-9][0-9]( - )+";
            subs = Regex.Split(source, pattern);
            Array.Reverse(subs);
            j = 0;
            pattern = @"\s";
            for (i = 0; i < subs.Length && j < 4; i++)
            {
                subs2 = Regex.Split(subs[i], pattern);
                if (!subs2[0].Contains("-") && subs2[0] != "" && subs2[0] != " " && subs2[0] != "<!DOCTYPE" && !subjects.Contains(subs2[0].Replace("</b>", "")))
                {
                    subjects[j] = subs2[0];
                    subject = "materia_" + (j + 1).ToString();
                    Properties.Settings.Default[subject] = subjects[j];
                    j++;
                }
            }

            while (j < 4)
            {
                subject = "materia_" + (j + 1).ToString();
                Properties.Settings.Default[subject] = "Materia Non Impostata";
                j++;
            }

            pattern = "https://politecnicomilano.webex.com/meet/";
            subs = Regex.Split(source, pattern);
            for (i = (subs.Length / 2 + 1), j = 0; i < subs.Length; i++, j++)
            {
                string[] nameSurname = subs[i].Split('.');
                names[j] = nameSurname[0];
                surnames[j] = nameSurname[1].Split('?')[0];
            }

            for (j = 1; j <= 4; j++)
            {
                for (i = 1; i <= 4; i++)
                {
                    source = "prof_" + j.ToString() + "_" + i.ToString() + "_nome";
                    Properties.Settings.Default[source] = "Nome Prof Non Impostato";
                    source = "prof_" + j.ToString() + "_" + i.ToString() + "_cognome";
                    Properties.Settings.Default[source] = "";
                }
            }

            for (i = 0; i < surnames.Length; i++)
            {
                if (surnames[i] != null)
                {
                    SubjectSelection = new SubjectChoice(names[i], surnames[i]);
                    SubjectSelection.ShowDialog();
                }
            }

            Properties.Settings.Default.Save();
            return true;
        }
    }
}