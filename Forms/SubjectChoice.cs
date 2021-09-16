using System;
using System.Linq;
using System.Windows.Forms;

namespace Webex_Launcher_Auto.Forms
{
    public partial class SubjectChoice : Form
    {
        public string name, surname, prof_index;

        public SubjectChoice(string prof_name, string prof_surname)
        {
            InitializeComponent();
            CenterToScreen();
            name = prof_name;
            surname = prof_surname;
        }

        private void SubjectChoice_Load(object sender, EventArgs e)
        {
            int i, j;
            string subject, prof;

            label1.Text = "\n\nChe materia insegna il prof " + name + " " + surname + "?\n(Chiudi questo dialogo per " +
                "non savare questo prof)";
            for (i = 1; i <= 4; i++)
            {
                Button button = (Button)Controls.Find(string.Format("button{0}", i), false).FirstOrDefault();
                subject = "materia_" + i.ToString();
                if (Properties.Settings.Default[subject].ToString() != "Materia Non Impostata")
                {
                    button.Text = Properties.Settings.Default[subject].ToString();
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
                    prof_index = "prof_" + j.ToString() + "_" + i.ToString() + "_nome";
                    try
                    {
                        prof = Properties.Settings.Default[prof_index].ToString();
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
                prof_index = "prof_1_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[prof_index].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[prof_index] = name;
            prof_index = "prof_1_" + i.ToString() + "_cognome";
            Properties.Settings.Default[prof_index] = surname;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                prof_index = "prof_2_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[prof_index].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[prof_index] = name;
            prof_index = "prof_2_" + i.ToString() + "_cognome";
            Properties.Settings.Default[prof_index] = surname;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                prof_index = "prof_3_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[prof_index].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[prof_index] = name;
            prof_index = "prof_3_" + i.ToString() + "_cognome";
            Properties.Settings.Default[prof_index] = surname;
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            do
            {
                i++;
                prof_index = "prof_4_" + i.ToString() + "_nome";
            } while (Properties.Settings.Default[prof_index].ToString() != "Nome Prof Non Impostato");
            Properties.Settings.Default[prof_index] = name;
            prof_index = "prof_4_" + i.ToString() + "_cognome";
            Properties.Settings.Default[prof_index] = surname;
            Close();
        }
    }
}