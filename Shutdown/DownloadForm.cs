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
    private partial class DownloadForm : Form
    {
        private AdvancedForm fAdvanced;
        private long download = 0;
        private long previousBytesReceived = 0;
        private bool flag = false;
        private byte compteur = 0;
        private byte index = 0;

        public DownloadForm()
        {
            InitializeComponent();
            fAdvanced = new AdvancedForm();

            //Code pour récupérer l'interface réseau qui récup le plus de bytes
            byte nbAdaptaters = 0;
            byte nbBytesReceivedUpperZero = 0;
            long BytesReceived = 0;

            NetworkInterface[] Nic = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adaptaters in NetworkInterface.GetAllNetworkInterfaces())
            {
                BytesReceived = Nic[nbAdaptaters].GetIPv4Statistics().BytesReceived;
                if (BytesReceived > 0)
                {
                    nbBytesReceivedUpperZero++;
                }
                nbAdaptaters++;
            }
            BytesReceived = 0;

            long[] stockBytesReceived = new long[nbAdaptaters];
            long[] stockBytesReceivedUpperZero = new long[nbBytesReceivedUpperZero];
            byte x = 0;

            for (byte i = 0; i < nbAdaptaters; i++)
            {
                BytesReceived = Nic[i].GetIPv4Statistics().BytesReceived;
                stockBytesReceived[i] = BytesReceived;

                if (stockBytesReceived[i] > 0)
                {
                    stockBytesReceivedUpperZero[x] = stockBytesReceived[i];

                    if (x == 0 || stockBytesReceivedUpperZero[x] > stockBytesReceivedUpperZero[x - 1])
                    {
                        index = i;
                        x++;
                    }
                }
            }
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
            download = 0;
            previousBytesReceived = 0;
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
            if (download < fAdvanced.numUpDown_Download.Value)
            {
                compteur++;
                int TempsRestant = (int)(fAdvanced.secondes - compteur);
                int minutes = (TempsRestant / 60);
                int secondes = (TempsRestant % 60);
                lbl_temps.Text = "Temps restant : " + minutes.ToString() + ":" + secondes.ToString();
            }
            else
            {
                compteur = 0;
                Timer_Temps.Enabled = false;
            }

            if (compteur == fAdvanced.secondes - 15)
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

            if (compteur == fAdvanced.secondes)
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
            NetworkInterface[] Nic = NetworkInterface.GetAllNetworkInterfaces();

            long downloadRate = Nic[index].GetIPv4Statistics().BytesReceived;

            long nowBytesReceived = downloadRate;

            if (previousBytesReceived != 0)
            {
                download = (nowBytesReceived - previousBytesReceived) / 1024;
            }

            previousBytesReceived = downloadRate;

            if (download >= fAdvanced.numUpDown_Download.Value)
            {
                lbl_etat.ForeColor = Color.Green;
                lbl_etat.Text = "Téléchargement en cours...";
                lbl_temps.Visible = false;
                flag = true;
            }

            if (download < fAdvanced.numUpDown_Download.Value && flag == true)
            {
                Timer_Temps.Enabled = true;
                lbl_etat.ForeColor = Color.Red;
                lbl_etat.Text = "Fin du téléchargement";
                flag = false;
                lbl_temps.Text = $"Temps restant : {fAdvanced.numUpDown_TempsRestant.Value}:00";
                lbl_temps.Visible = true;
            }
            lbl_debit.Text = download.ToString() + " KB/s";
        }
    }
}