using System;
using System.Linq;
using System.Windows.Forms;

namespace Webex_Launcher_Auto.Forms
{
    public partial class SubjectChoice : Form
    {
        public string nome, cognome, indice_prof;

        public SubjectChoice(string nome_prof, string cognome_prof)
        {
            InitializeComponent();
            CenterToScreen();
            nome = nome_prof;
            cognome = cognome_prof;
        }

        private void SceltaMateria_Load(object sender, EventArgs e)
        {
            int i, j;
            string materia, prof;

            label1.Text = "\n\nChe materia insegna il prof " + nome + " " + cognome + "?\n(Chiudi questo dialogo per " +
                "non savare questo prof)";
            for (i = 1; i <= 4; i++)
            {
                Button button = (Button)Controls.Find(string.Format("button{0}", i), false).FirstOrDefault();
                materia = "materia_" + i.ToString();
                if (Properties.Settings.Default[materia].ToString() != "Materia Non Impostata")
                {
                    button.Text = Properties.Settings.Default[materia].ToString();
                }
                else
                {
                    button.Enabled = false;
                    button.Visible = false;
                }
            }
            for (j = 1; j <= 4; j++)
            {
                i = 0;
                do
                {
                    i++;
                    indice_prof = "prof_" + j.ToString() + "_" + i.ToString() + "_nome";
                    try
                    {
                        prof = Properties.Settings.Default[indice_prof].ToString();
                    }
                    catch
                    {
                        Button button = (Button)Controls.Find(string.Format("button{0}", j), false).FirstOrDefault();
                        button.Enabled = false;
                        button.Visible = false;
                        label2.Visible = true;
                        break;
                    }
                } while (prof != "Nome Prof Non Impostato");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                indice_prof = "prof_1_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[indice_prof].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[indice_prof] = nome;
            indice_prof = "prof_1_" + i.ToString() + "_cognome";
            Properties.Settings.Default[indice_prof] = cognome;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                indice_prof = "prof_2_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[indice_prof].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[indice_prof] = nome;
            indice_prof = "prof_2_" + i.ToString() + "_cognome";
            Properties.Settings.Default[indice_prof] = cognome;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                indice_prof = "prof_3_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[indice_prof].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[indice_prof] = nome;
            indice_prof = "prof_3_" + i.ToString() + "_cognome";
            Properties.Settings.Default[indice_prof] = cognome;
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                indice_prof = "prof_4_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[indice_prof].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[indice_prof] = nome;
            indice_prof = "prof_4_" + i.ToString() + "_cognome";
            Properties.Settings.Default[indice_prof] = cognome;
            Close();
        }
    }
}