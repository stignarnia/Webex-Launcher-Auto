using System;
using System.Linq;
using System.Windows.Forms;

namespace Webex_Launcher_Auto
{
    public partial class Materia : Form
    {
        public int id_materia;

        public Materia(int materia)
        {
            InitializeComponent();
            CenterToScreen();
            id_materia = materia;
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            int j;
            for (j = 1; j <= 4; j++)
            {
                Button button = (Button)Controls.Find(string.Format("button{0}", j), false).FirstOrDefault();
                string nome = "prof_" + id_materia.ToString() + "_" + j.ToString() + "_nome";
                string cognome = "prof_" + id_materia.ToString() + "_" + j.ToString() + "_cognome";
                button.Text = Properties.Settings.Default[nome].ToString() + " " + Properties.Settings.Default[cognome].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "Nome Prof Non Impostato ")
            {
                string nome = "prof_" + id_materia.ToString() + "_1_nome";
                string cognome = "prof_" + id_materia.ToString() + "_1_cognome";
                Program.Materia_Button_Common(nome, cognome);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text != "Nome Prof Non Impostato ")
            {
                string nome = "prof_" + id_materia.ToString() + "_2_nome";
                string cognome = "prof_" + id_materia.ToString() + "_2_cognome";
                Program.Materia_Button_Common(nome, cognome);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text != "Nome Prof Non Impostato ")
            {
                string nome = "prof_" + id_materia.ToString() + "_3_nome";
                string cognome = "prof_" + id_materia.ToString() + "_3_cognome";
                Program.Materia_Button_Common(nome, cognome);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text != "Nome Prof Non Impostato ")
            {
                string nome = "prof_" + id_materia.ToString() + "_4_nome";
                string cognome = "prof_" + id_materia.ToString() + "_4_cognome";
                Program.Materia_Button_Common(nome, cognome);
            }
        }
    }
}
