using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Webex_Launcher_Auto
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
                    } else {
                        i++;
                    }
                    k++;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void clear_Click(object sender, EventArgs e)
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

        private void nosave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, EventArgs e)
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
                        } else if (NomeCognome.Length == 2) {
                            materia = "materia_" + j.ToString();
                            if (Properties.Settings.Default[materia].ToString() == "Materia Non Impostata")
                            {
                                MessageBox.Show(NomeCognome[0] + " " + NomeCognome[1] + " si riferisce a una materia non impostata. " +
                                "Il nominativo in questione non verrà salvato", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            } else {
                                Properties.Settings.Default[nome] = NomeCognome[0];
                                Properties.Settings.Default[cognome] = NomeCognome[1];
                            }
                        } else {
                            MessageBox.Show("Inserisci un nome e cognome valido per Materia " + j.ToString() + " Prof " +  i.ToString() 
                                + ". Il nominativo in questione non verrà salvato. Per info riapri la pagina di inserimento manuale " +
                                "e clicca su <Regole di sintassi> in alto a destra", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    } else {
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com");
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
