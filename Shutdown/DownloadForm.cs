using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Shutdown
{
    public partial class DownloadForm : Form
    {
        private byte compteur = 0;
        private bool flag = false;
        private long previousbytesreceived = 0;
        private long Download;
        private AdvencedForm f2 = new AdvencedForm();
        private C_Alimentation alim;
        //NetworkInterfaceConnexion (attribution de la NIC de defaut)
        private NetworkInterface Nic = (NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault
        (i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.NetworkInterfaceType != NetworkInterfaceType.Tunnel));
        IPv4InterfaceStatistics interfaceStats;

        public DownloadForm()
        {
            InitializeComponent();
            alim = new C_Alimentation();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            lbl_etat.ForeColor = Color.Orange;
            lbl_etat.Text = "En attente d'un téléchargement...";
            Timer_Debit.Enabled = true;
            btn_Stop.Enabled = true;
            btn_Start.Enabled = false;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            lbl_etat.ForeColor = Color.Red;
            lbl_etat.Text = "OFF";
            btn_Stop.Enabled = false;
            btn_Start.Enabled = true;
            Timer_Temps.Enabled = false;
            Timer_Debit.Enabled = false;
        }

        private void Timer_Temps_Tick(object sender, EventArgs e)
        {
            if (Download < f2.numericUpDown1.Value)
            {
                compteur++;
            }
            else
            {
                compteur = 0;
                Timer_Temps.Enabled = false;
            }

            if(compteur == 165)
            {
                if (f2.comboBox1.SelectedItem.ToString() == "Eteindre")
                {
                    //l'ordi va s'éteindre (messageBox)
                    MessageBox.Show("Téléchargement fini, l'ordinateur va s'éteindre dans 15 secondes !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (f2.comboBox1.SelectedItem.ToString() == "Mettre en veille")
                {
                    //l'ordi va se mettre en veille (messageBox)
                    MessageBox.Show("Téléchargement fini, l'ordinateur va se mettre en veille dans 15 secondes !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (compteur == 180)
            {
                if (f2.comboBox1.SelectedItem.ToString() == "Eteindre")
                {
                    //On éteint l'ordi
                    alim.Shutdown();
                }
                if (f2.comboBox1.SelectedItem.ToString() == "Mettre en veille")
                {
                    //Mode Veille
                    Application.SetSuspendState(PowerState.Hibernate, true, true);
                }
            }

            byte TempsRestant = (byte)(180 - compteur);
            byte minutes = (byte)(TempsRestant / 60);
            byte secondes = (byte)(TempsRestant % 60);
            lbl_temps.Text = "Temps restant : " + minutes.ToString() + ":" + secondes.ToString();
            lbl_temps.Visible = true;
        }

        private void Timer_Debit_Tick(object sender, EventArgs e)
        {
            interfaceStats = Nic.GetIPv4Statistics();

            if(previousbytesreceived != 0)
            {
                Download = (interfaceStats.BytesReceived - previousbytesreceived) / 1024;
            }

            previousbytesreceived = NetworkInterface.GetAllNetworkInterfaces()[0].GetIPv4Statistics().BytesReceived;

            if (Download >= f2.numericUpDown1.Value)
            {
                lbl_etat.ForeColor = Color.Green;
                lbl_etat.Text = "Téléchargement en cours";
                lbl_temps.Visible = false;
                flag = true;
            }

            if(Download < f2.numericUpDown1.Value && flag == true)
            {
                Timer_Temps.Enabled = true;
                lbl_etat.ForeColor = Color.Red;
                lbl_etat.Text = "Fin du téléchargement";
                flag = false;
            }
            lbl_debit.Text = Download.ToString() + " KB/s";
        }

        private void btn_advanced_Click(object sender, EventArgs e)
        {
            f2.ShowDialog();
        }
    }
}