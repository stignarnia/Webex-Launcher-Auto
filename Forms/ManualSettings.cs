using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Webex_Launcher_Auto.Forms
{
    public partial class ManualSettings : Form
    {
        public ManualSettings()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void ManualSettings_Load(object sender, EventArgs e)
        {
            textBox5.Text = Properties.Settings.Default["materia_1"].ToString();
            textBox10.Text = Properties.Settings.Default["materia_2"].ToString();
            textBox15.Text = Properties.Settings.Default["materia_3"].ToString();
            textBox20.Text = Properties.Settings.Default["materia_4"].ToString();

            int i, j, k;

            k = 1;
            for (j = 1; j <= 4; j++)
            {
                for (i = 4; i >= 1; i--)
                {
                    if (k != (j - 1) * 5)
                    {
                        TextBox textbox = (TextBox)Controls.Find(string.Format("textBox{0}", k), false).FirstOrDefault();
                        string nome = "prof_" + j.ToString() + "_" + i.ToString() + "_nome";
                        string cognome = "prof_" + j.ToString() + "_" + i.ToString() + "_cognome";
                        textbox.Text = Properties.Settings.Default[nome].ToString() + " " + Properties.Settings.Default[cognome].ToString();
                    }
                    else
                    {
                        i++;
                    }
                    k++;
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            textBox5.Text = "Materia Non Impostata";
            textBox10.Text = "Materia Non Impostata";
            textBox15.Text = "Materia Non Impostata";
            textBox20.Text = "Materia Non Impostata";

            int i;

            for (i = 1; i < 20; i++)
            {
                if (i != 5 && i != 10 && i != 15)
                {
                    TextBox textbox = (TextBox)Controls.Find(string.Format("textBox{0}", i), false).FirstOrDefault();
                    textbox.Text = "Nome Prof Non Impostato";
                }
            }
        }

        private void Nosave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            int i, j, k, profImpostati;
            string materia, nome, cognome;
            TextBox textbox;

            k = 1;
            for (i = 1; i <= 20; i++)
            {
                if (i == 5 || i == 10 || i == 15 || i == 20)
                {
                    textbox = (TextBox)Controls.Find(string.Format("textBox{0}", i), false).FirstOrDefault();
                    if (textbox.Text.Contains("Materia Non Impostata") || textbox.Text == "")
                    {
                        textbox.Text = "Materia Non Impostata";
                    }
                    materia = "materia_" + k.ToString();
                    Properties.Settings.Default[materia] = textbox.Text;
                    k++;
                }
            }

            k = 1;
            for (j = 1; j <= 4; j++)
            {
                for (i = 4; i >= 1; i--)
                {
                    if (k != (j - 1) * 5)
                    {
                        textbox = (TextBox)Controls.Find(string.Format("textBox{0}", k), false).FirstOrDefault();
                        nome = "prof_" + j.ToString() + "_" + i.ToString() + "_nome";
                        cognome = "prof_" + j.ToString() + "_" + i.ToString() + "_cognome";
                        string[] NomeCognome = textbox.Text.Split(' ');
                        if (textbox.Text.Contains("Nome Prof Non Impostato") || textbox.Text == "")
                        {
                            Properties.Settings.Default[nome] = "Nome Prof Non Impostato";
                            Properties.Settings.Default[cognome] = "";
                        }
                        else if (NomeCognome.Length == 2)
                        {
                            materia = "materia_" + j.ToString();
                            if (Properties.Settings.Default[materia].ToString() == "Materia Non Impostata")
                            {
                                MessageBox.Show(NomeCognome[0] + " " + NomeCognome[1] + " si riferisce a una materia non impostata. " +
                                "Il nominativo in questione non verrà salvato", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                Properties.Settings.Default[nome] = NomeCognome[0];
                                Properties.Settings.Default[cognome] = NomeCognome[1];
                            }
                        }
                        else
                        {
                            MessageBox.Show("Inserisci un nome e cognome valido per Materia " + j.ToString() + " Prof " + i.ToString()
                                + ". Il nominativo in questione non verrà salvato. Per info riapri la pagina di inserimento manuale " +
                                "e clicca su <Regole di sintassi> in alto a destra", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        i++;
                    }
                    k++;
                }
            }

            for (j = 1; j <= 4; j++)
            {
                materia = "materia_" + j.ToString();
                if (Properties.Settings.Default[materia].ToString() != "Materia Non Impostata")
                {
                    profImpostati = 4;
                    for (i = 1; i <= 4; i++)
                    {
                        nome = "prof_" + j.ToString() + "_" + i.ToString() + "_nome";
                        if (Properties.Settings.Default[nome].ToString() == "Nome Prof Non Impostato")
                        {
                            profImpostati--;
                        }
                    }
                    if (profImpostati == 0)
                    {
                        Properties.Settings.Default[materia] = "Materia Non Impostata";
                        MessageBox.Show("La materia " + j.ToString() + " non ha prof impostati. " +
                            "La materia in questione non verrà salvata", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            Close();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/stignarnia/Webex-Launcher-Auto/wiki/Regole-di-sintassi");
        }
    }
}