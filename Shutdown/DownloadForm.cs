using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Management;

namespace Shutdown
{
    public partial class DownloadForm : Form
    {
        private AdvancedForm fAdvanced = new AdvancedForm();
        private long Download = 0;
        private long previousBytesReceived = 0;
        private bool flag = false;
        private ushort compteur = 0;
        byte compteur2 = 0;
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
            if (Download < fAdvanced.numUpDown_Download.Value)
            {
                compteur++;
                ushort TempsRestant = (ushort)(fAdvanced.secondes - compteur);
                byte minutes = (byte)(TempsRestant / 60);
                byte secondes = (byte)(TempsRestant % 60);
                string sec = "";
                if (secondes < 10)
                {
                    sec = "0" + secondes.ToString();
                }
                else
                {
                    sec = secondes.ToString();
                }
                lbl_temps.Text = "Temps restant : " + minutes.ToString() + ":" + sec;
            }
            else
            {
                compteur = 0;
                Timer_Temps.Enabled = false;
            }

            if (compteur == fAdvanced.secondes - 15)
            {
                if (fAdvanced.cmb_OptSys.SelectedItem.ToString() == "Eteindre")
                {
                    //L'ordinateur va s'éteindre (messageBox)
                    MessageBox.Show("Téléchargement fini, l'ordinateur va s'éteindre dans 15 secondes !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (fAdvanced.cmb_OptSys.SelectedItem.ToString() == "Mettre en veille")
                {
                    //L'ordinateur va se mettre en veille (messageBox)
                    MessageBox.Show("Téléchargement fini, l'ordinateur va se mettre en veille dans 15 secondes !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (compteur == fAdvanced.secondes)
            {
                if (fAdvanced.cmb_OptSys.SelectedItem.ToString() == "Eteindre")
                {
                    //On éteint l'ordinateur
                    ManagementBaseObject mboShutdown = null;
                    ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
                    mcWin32.Get();

                    // You can't shutdown without security privileges
                    mcWin32.Scope.Options.EnablePrivileges = true;
                    ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");

                    // Flag 1 means we want to shut down the system. Use "2" to reboot.
                    mboShutdownParams["Flags"] = "1";
                    mboShutdownParams["Reserved"] = "0";
                    foreach (ManagementObject manObj in mcWin32.GetInstances())
                    {
                        mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
                    }
                }
                if (fAdvanced.cmb_OptSys.SelectedItem.ToString() == "Mettre en veille")
                {
                    //Mode Veille pour l'ordinateur
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
                Download = (nowBytesReceived - previousBytesReceived) / 1024;
            }

            previousBytesReceived = downloadRate;

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
                lbl_temps.Text = $"Temps restant : {fAdvanced.numUpDown_TempsRestant.Value}:00";
                lbl_temps.Visible = true;
            }
            lbl_debit.Text = Download.ToString() + " KB/s";
        }
    }
}