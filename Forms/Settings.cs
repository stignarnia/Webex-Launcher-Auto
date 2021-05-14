using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Webex_Launcher_Auto.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = Properties.Settings.Default["attesa"].ToString();
            trackBar1.Value = Int32.Parse(label2.Text);
            checkBox1.Checked = bool.Parse(Properties.Settings.Default["auto"].ToString());
            if (checkBox1.Checked == true)
            {
                manualInsert.Enabled = false;
            }
            else
            {
                manualInsert.Enabled = true;
            }
            if (Properties.Settings.Default["browser"].ToString() == "msedge")
            {
                comboBox1.SelectedItem = "Edge";
            }
            else
            {
                comboBox1.SelectedItem = Properties.Settings.Default["browser"].ToString();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/stignarnia/Webex-Launcher-Auto/wiki/Cosa-sono-i-secondi-di-attesa%3F");
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Edge")
            {
                Properties.Settings.Default["browser"] = "msedge";
            }
            else
            {
                Properties.Settings.Default["browser"] = comboBox1.SelectedItem.ToString();
            }
            Properties.Settings.Default["attesa"] = trackBar1.Value;
            Properties.Settings.Default["auto"] = checkBox1.Checked;

            Properties.Settings.Default.Save();
            Close();
        }

        private void nosave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/stignarnia/Webex-Launcher-Auto/wiki/Come-funziona-la-rilevazione-automatica-dei-nomi-dei-prof-e-delle-materie%3F");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                manualInsert.Enabled = false;
            }
            else
            {
                manualInsert.Enabled = true;
            }
        }

        private void manualInsert_Click(object sender, EventArgs e)
        {
            ManualSettings SelezioneManuale;
            SelezioneManuale = new ManualSettings();
            SelezioneManuale.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SetToDefault_Click(object sender, EventArgs e)
        {
            label2.Text = "5";
            trackBar1.Value = 5;
            checkBox1.Checked = false;
            manualInsert.Enabled = true;
            comboBox1.SelectedItem = "Chrome";
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }
    }
}