using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Webex_Launcher_Auto
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            if (bool.Parse(Properties.Settings.Default["auto"].ToString()))
            {
                Sync.Visible = true;
                Sync.Enabled = true;
            } else
            {
                Sync.Visible = false;
                Sync.Enabled = false;
            }

            button1.Text = Properties.Settings.Default["materia_1"].ToString();
            button2.Text = Properties.Settings.Default["materia_2"].ToString();
            button3.Text = Properties.Settings.Default["materia_3"].ToString();
            button4.Text = Properties.Settings.Default["materia_4"].ToString();

            if (button1.Text == "Materia Non Impostata" && button2.Text == "Materia Non Impostata" &&
                button3.Text == "Materia Non Impostata" && button4.Text == "Materia Non Impostata")
            {
                if (MessageBox.Show("Il programma non è ancora configurato, lascia che ti apra le impostazioni",
                    "Benvenuto", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                   Application.Exit();
                } else {
                    Sync.Enabled = false;
                    Impostazioni_Click(null, EventArgs.Empty);
                }
            }

            if (!Program.IsBrowserInstalled())
            {
                if (MessageBox.Show("Il browser selezionato non è installato, cambialo nelle impostazioni",
                                "Errore", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                } else {
                    Impostazioni_Click(null, EventArgs.Empty);
                }
            }

            Process[] procs = Process.GetProcessesByName("atmgr");
            if (procs.Length != 0)
            {
                if (MessageBox.Show("Webex è già aperto (magari solo in background), continuando verrà chiuso",
                "Attenzione", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                } else {
                    Process[] procs2 = Process.GetProcessesByName("ciscowebexstart");
                    foreach (Process p in procs2)
                    {
                        p.Kill();
                    }
                    foreach (Process p in procs)
                    {
                        p.Kill();
                        Thread.Sleep(250);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "Materia Non Impostata")
            {
                Materia SelezioneProf;
                SelezioneProf = new Materia(1);
                SelezioneProf.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text != "Materia Non Impostata")
            {
                Materia SelezioneProf;
                SelezioneProf = new Materia(2);
                SelezioneProf.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text != "Materia Non Impostata")
            {
                Materia SelezioneProf;
                SelezioneProf = new Materia(3);
                SelezioneProf.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text != "Materia Non Impostata")
            {
                Materia SelezioneProf;
                SelezioneProf = new Materia(4);
                SelezioneProf.ShowDialog();
            }
        }

        private void Sync_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Aprirò i servizi online, accedi, entra nell'orario e seleziona il semestre che ti interessa, " +
                "a quel punto proverò a estrarre le informazioni necessarie e chiuderò la pagina",
                "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Program.Sync();
            }
            Form1_Load(null, EventArgs.Empty);
        }

        private void Impostazioni_Click(object sender, EventArgs e)
        {
            Settings Impostazioni;
            Impostazioni = new Settings();
            Impostazioni.ShowDialog();
            if (bool.Parse(Properties.Settings.Default["auto"].ToString()) && bool.Parse(Properties.Settings.Default["auto"].ToString()) != Sync.Enabled)
            {
                Sync_Click(null, EventArgs.Empty);
            } else {
                Form1_Load(null, EventArgs.Empty);
            }
        }
    }
}
