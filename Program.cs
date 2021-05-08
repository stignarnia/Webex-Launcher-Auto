using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;


namespace Webex_Launcher_Auto
{
    public partial class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }

        public static bool IsBrowserInstalled()
        {
            string exePath;
            RegistryKey key;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                exePath = subkey.GetValue("InstallLocation") as string;
                if (File.Exists(exePath + @"\" + Properties.Settings.Default["browser"].ToString().ToLower() + ".exe"))
                {
                    return true;
                }
            }
            // NOT FOUND
            return false;
        }

        public static bool Is_Webex_Open()
        {
            Process[] procs = Process.GetProcessesByName("atmgr");

            if (procs.Length == 0)
            {
                return false;
            }
            else
            {
                foreach (Process p in procs)
                {
                    if (p.MainWindowTitle.Length > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void Materia_Button_Common(string nome, string cognome)
        {
            int i, tries;
            string name, surname;
            IntPtr hWnd = IntPtr.Zero;

            tries = Int32.Parse(Properties.Settings.Default["attesa"].ToString()) * 4;

            name = Properties.Settings.Default[nome].ToString();
            surname = Properties.Settings.Default[cognome].ToString();
            Process.Start(Properties.Settings.Default["browser"].ToString() + ".exe", "https://politecnicomilano.webex.com/meet/" + name.ToLower() + "." + surname.ToLower());

            for (i = 0; i < tries; i++)
            {
                if (Is_Webex_Open())
                {
                    SendKeys.Send("%{Tab}");
                    Thread.Sleep(500);
                    SendKeys.Send("^(w)");
                    Thread.Sleep(500);
                    if (Process.GetProcessesByName(Properties.Settings.Default["browser"].ToString()).Length != 0)
                    {
                        SendKeys.Send("%{Tab}");
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

        public static void Sync()
        {
            int i, j;
            string[] subs, subs2, materie = new string[4], nomi = new string[4*4], cognomi = new string[4*4];
            string pattern, materia;
            SceltaMateria SelezioneMateria;

            Process.Start(Properties.Settings.Default["browser"].ToString() + ".exe", "https://servizionline.polimi.it/portaleservizi/");
            Thread.Sleep(500);
            SendKeys.Send("%{Tab}");

            if (MessageBox.Show("Premi ok quando sei sulla pagina giusta",
                    "Sono pronto", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Thread.Sleep(500);
                SendKeys.Send("%{Tab}");
                Thread.Sleep(500);
                SendKeys.Send("^(u)");
                Thread.Sleep(500);
                SendKeys.Send("^(a)");
                Thread.Sleep(500);
                SendKeys.Send("^(c)");
                Thread.Sleep(500);
                SendKeys.Send("^(w)");
                Thread.Sleep(500);
                SendKeys.Send("^(w)");
                Thread.Sleep(500);
                SendKeys.Send("%{Tab}");
            }

            string source = Clipboard.GetText();
            pattern = "[0-9][0-9][0-9][0-9][0-9][0-9]( - )+";
            subs = Regex.Split(source, pattern);
            Array.Reverse(subs);
            j = 0;
            pattern = @"\s";
            for (i = 0; i < subs.Length && j < 4; i++)
            {
                subs2 = Regex.Split(subs[i], pattern);
                if (!subs2[0].Contains("-") && subs2[0] != "" && subs2[0] != " " && !materie.Contains(subs2[0].Replace("</b>", "")))
                {
                    materie[j] = subs2[0];
                    materia = "materia_" + (j + 1).ToString();
                    Properties.Settings.Default[materia] = materie[j];
                    j++;
                }
            }

            while (j < 4)
            {
                materia = "materia_" + (j + 1).ToString();
                Properties.Settings.Default[materia] = "Materia Non Impostata";
                j++;
            }

            pattern = "https://politecnicomilano.webex.com/meet/";
            subs = Regex.Split(source, pattern);
            for (i = (subs.Length / 2 + 1), j = 0; i < subs.Length; i++, j++)
            {
                string[] nameSurname = subs[i].Split('.');
                nomi[j] = nameSurname[0];
                cognomi[j] = nameSurname[1].Split('?')[0];
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

            for (i = 0; i < cognomi.Length; i++)
            {
                if (cognomi[i] != null)
                {
                    SelezioneMateria = new SceltaMateria(nomi[i], cognomi[i]);
                    SelezioneMateria.ShowDialog();
                }
            }

            Properties.Settings.Default.Save();
        }
    }
}
