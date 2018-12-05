using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Shutdown.Properties;

namespace Shutdown
{
    public partial class Form1 : Form
    {
        byte compteur = 0;
        bool flag = false;
        byte compteurStart = 0;
        long previousbytesreceived;
        long Download;
        Form2 f2 = new Form2();

        public Form1()
        {
            InitializeComponent();

        }

        private void Shutdown()
        {
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            const short SE_PRIVILEGE_ENABLED = 2;
            const uint EWX_SHUTDOWN = 1;
            const short TOKEN_ADJUST_PRIVILEGES = 32;
            const short TOKEN_QUERY = 8;
            IntPtr hToken;
            TOKEN_PRIVILEGES tkp;

            // Get shutdown privileges...
            OpenProcessToken(Process.GetCurrentProcess().Handle,
                TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken);
            tkp.PrivilegeCount = 1;
            tkp.Privileges.Attributes = SE_PRIVILEGE_ENABLED;
            LookupPrivilegeValue("", SE_SHUTDOWN_NAME, out tkp.Privileges.pLuid);
            AdjustTokenPrivileges(hToken, false, ref tkp, 0U, IntPtr.Zero,
                  IntPtr.Zero);

            // Now we have the privileges, shutdown Windows
            ExitWindowsEx(EWX_SHUTDOWN, 0);
        }

        // Structures needed for the API calls
        private struct LUID
        {
            public int LowPart;
            public int HighPart;
        }
        private struct LUID_AND_ATTRIBUTES
        {
            public LUID pLuid;
            public int Attributes;
        }
        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public LUID_AND_ATTRIBUTES Privileges;
        }

        [DllImport("advapi32.dll")]
        static extern int OpenProcessToken(IntPtr ProcessHandle,
                             int DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            UInt32 BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);

        [DllImport("advapi32.dll")]
        static extern int LookupPrivilegeValue(string lpSystemName,
                               string lpName, out LUID lpLuid);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int ExitWindowsEx(uint uFlags, uint dwReason);

        private void button1_Click(object sender, EventArgs e)
        {
            lbl_etat.ForeColor = Color.Orange;
            lbl_etat.Text = "En attente d'un téléchargement...";
            Timer_Debit.Enabled = true;
            BTN_Stop.Enabled = true;
            BTN_Start.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbl_etat.ForeColor = Color.Red;
            lbl_etat.Text = "OFF";
            BTN_Stop.Enabled = false;
            BTN_Start.Enabled = true;
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
                    if (MessageBox.Show("Téléchargement fini, l'ordinateur va s'éteindre dans 15 secondes !", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    == DialogResult.Yes) { }
                }
                if (f2.comboBox1.SelectedItem.ToString() == "Mettre en veille")
                {
                    //l'ordi va se mettre en veille (messageBox)
                    if (MessageBox.Show("Téléchargement fini, l'ordinateur va se mettre en veille dans 15 secondes !", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    == DialogResult.Yes) { }
                }
            }

            if (compteur == 180)
            {
                if (f2.comboBox1.SelectedItem.ToString() == "Eteindre")
                {
                    //On éteint l'ordi
                    Shutdown();
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
            if (compteurStart <= 2)
            {
                compteurStart++;
                lbl_debit.Text = "Initialisation ...";
            }

            //NetworkInterfaceConnexion (attribution de la NIC de defaut)
            var Nic = (NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault
            (i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.NetworkInterfaceType != NetworkInterfaceType.Tunnel));
            IPv4InterfaceStatistics interfaceStats = Nic.GetIPv4Statistics();

            Download = (interfaceStats.BytesReceived - previousbytesreceived) / 1024;

            previousbytesreceived = NetworkInterface.GetAllNetworkInterfaces()[0].GetIPv4Statistics().BytesReceived;

            if (compteurStart == 3)
            {
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
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            f2.ShowDialog();
        }
    }
}