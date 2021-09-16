using System;
using System.Linq;
using System.Windows.Forms;

namespace Webex_Launcher_Auto.Forms
{
    public partial class Subject : Form
    {
        public int subject_id;

        public Subject(int subject)
        {
            InitializeComponent();
            CenterToScreen();
            subject_id = subject;
        }

        public void Subject_Load(object sender, EventArgs e)
        {
            int j;
            for (j = 1; j <= 4; j++)
            {
                Button button = (Button)Controls.Find(string.Format("button{0}", j), false).FirstOrDefault();
                string name = "prof_" + subject_id.ToString() + "_" + j.ToString() + "_nome";
                string surname = "prof_" + subject_id.ToString() + "_" + j.ToString() + "_cognome";
                button.Text = Properties.Settings.Default[name].ToString() + " " + Properties.Settings.Default[surname].ToString();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "name Prof Non Impostato ")
            {
                string name = "prof_" + subject_id.ToString() + "_1_nome";
                string surname = "prof_" + subject_id.ToString() + "_1_cognome";
                Program.Subject_Button_Common(name, surname);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (button2.Text != "name Prof Non Impostato ")
            {
                string name = "prof_" + subject_id.ToString() + "_2_nome";
                string surname = "prof_" + subject_id.ToString() + "_2_cognome";
                Program.Subject_Button_Common(name, surname);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (button3.Text != "name Prof Non Impostato ")
            {
                string name = "prof_" + subject_id.ToString() + "_3_nome";
                string surname = "prof_" + subject_id.ToString() + "_3_cognome";
                Program.Subject_Button_Common(name, surname);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (button4.Text != "name Prof Non Impostato ")
            {
                string name = "prof_" + subject_id.ToString() + "_4_nome";
                string surname = "prof_" + subject_id.ToString() + "_4_cognome";
                Program.Subject_Button_Common(name, surname);
            }
        }
    }
}