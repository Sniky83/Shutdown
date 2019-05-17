using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Shutdown
{
    public partial class DownloadForm : Form
    {
        private AdvancedForm fAdvanced = new AdvancedForm();
        private long Download;
        private long previousbytesreceived = 0;
        private bool flag = false;
        private byte compteur = 0;

        public DownloadForm()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            lbl_etat.ForeColor = Color.Orange;
            lbl_etat.Text = "En attente d'un téléchargement...";
            Timer_Debit.Enabled = true;
            btn_Stop.Enabled = true;
            btn_Start.Enabled = false;
        }

        private void btn_advanced_Click(object sender, EventArgs e)
        {
            Timer_Debit.Enabled = false;
            Timer_Temps.Enabled = false;
            previousbytesreceived = 0;
            fAdvanced.ShowDialog();
            if(btn_Stop.Enabled == true)
            {
                Timer_Debit.Enabled = true;
                Timer_Temps.Enabled = true;
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            lbl_etat.ForeColor = Color.Red;
            lbl_etat.Text = "OFF";
            btn_Stop.Enabled = false;
            btn_Start.Enabled = true;
            Timer_Temps.Enabled = false;
            Timer_Debit.Enabled = false;
            lbl_temps.Visible = false;
            compteur = 0;
        }

        private void Timer_Temps_Tick(object sender, EventArgs e)
        {
            if (Download < fAdvanced.numUpDown_Download.Value)
            {
                compteur++;
                byte TempsRestant = (byte)(180 - compteur);
                byte minutes = (byte)(TempsRestant / 60);
                byte secondes = (byte)(TempsRestant % 60);
                lbl_temps.Text = "Temps restant : " + minutes.ToString() + ":" + secondes.ToString();
            }
            else
            {
                compteur = 0;
                Timer_Temps.Enabled = false;
            }

            if (compteur == 165)
            {
                if (fAdvanced.cmb_Alim.SelectedItem.ToString() == "Eteindre")
                {
                    //l'ordi va s'éteindre (messageBox)
                    MessageBox.Show("Téléchargement fini, l'ordinateur va s'éteindre dans 15 secondes !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (fAdvanced.cmb_Alim.SelectedItem.ToString() == "Mettre en veille")
                {
                    //l'ordi va se mettre en veille (messageBox)
                    MessageBox.Show("Téléchargement fini, l'ordinateur va se mettre en veille dans 15 secondes !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (compteur == 180)
            {
                if (fAdvanced.cmb_Alim.SelectedItem.ToString() == "Eteindre")
                {
                    //On éteint l'ordi
                    C_Alimentation alim = new C_Alimentation();
                    alim.Shutdown();
                }
                if (fAdvanced.cmb_Alim.SelectedItem.ToString() == "Mettre en veille")
                {
                    //Mode Veille
                    Application.SetSuspendState(PowerState.Hibernate, true, true);
                }
            }
        }

        private void Timer_Debit_Tick(object sender, EventArgs e)
        {
            NetworkInterface Nic = NetworkInterface.GetAllNetworkInterfaces()[fAdvanced.SelectedItem];

            IPv4InterfaceStatistics interfaceStats = Nic.GetIPv4Statistics();

            if (previousbytesreceived != 0)
            {
                Download = (interfaceStats.BytesReceived - previousbytesreceived) / 1024;
            }

            previousbytesreceived = NetworkInterface.GetAllNetworkInterfaces()[fAdvanced.SelectedItem].GetIPv4Statistics().BytesReceived;

            if (Download >= fAdvanced.numUpDown_Download.Value)
            {
                lbl_etat.ForeColor = Color.Green;
                lbl_etat.Text = "Téléchargement en cours...";
                lbl_temps.Visible = false;
                flag = true;
            }

            if (Download < fAdvanced.numUpDown_Download.Value && flag == true)
            {
                Timer_Temps.Enabled = true;
                lbl_etat.ForeColor = Color.Red;
                lbl_etat.Text = "Fin du téléchargement";
                flag = false;
                lbl_temps.Text = "Temps restant : 3:00";
                lbl_temps.Visible = true;
            }
            lbl_debit.Text = Download.ToString() + " KB/s";
        }
    }
}