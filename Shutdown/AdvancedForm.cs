using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutdown
{
    public partial class AdvancedForm : Form
    {
        //public int SelectedItem;
        //public byte[] tab;
        public byte index = 0;

        public AdvancedForm()
        {
            InitializeComponent();
            /*byte i = 0;
            byte j = 0;
            byte nb = 0;

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    nb++;
                }
            }

            tab = new byte[nb];

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                cmb_Interface.Items.Add(netInterface.Description);
                if (netInterface.OperationalStatus != OperationalStatus.Up)
                {
                    cmb_Interface.Items.RemoveAt(0);
                }
                else
                {
                    tab[j] = i;
                    j++;
                }
                i++;
            }
            cmb_Interface.Items.RemoveAt(j-1);
            cmb_Interface.SelectedIndex = 0;*/

            //Code pour récupérer l'interface réseau qui récup le plus de bytes
            byte nbAdaptaters = 0;
            byte nbBytesReceivedUpperZero = 0;
            NetworkInterface[] Nic = NetworkInterface.GetAllNetworkInterfaces();
            long BytesReceived = 0;

            byte x = 0;

            foreach (NetworkInterface adaptaters in NetworkInterface.GetAllNetworkInterfaces())
            {
                BytesReceived = Nic[nbAdaptaters].GetIPv4Statistics().BytesReceived;
                if(BytesReceived > 0)
                {
                    nbBytesReceivedUpperZero++;
                }
                nbAdaptaters++;
            }
            BytesReceived = 0;

            long[] stockBytesReceived = new long[nbAdaptaters];
            long[] stockBytesReceivedUpperZero = new long[nbBytesReceivedUpperZero];

            for (byte i = 0; i < nbAdaptaters; i++)
            {
                BytesReceived = Nic[i].GetIPv4Statistics().BytesReceived;
                stockBytesReceived[i] = BytesReceived;

                if (stockBytesReceived[i] > 0)
                {
                    stockBytesReceivedUpperZero[x] = stockBytesReceived[i];
                    if(x == 0 || stockBytesReceivedUpperZero[x] > stockBytesReceivedUpperZero[x-1])
                    {
                        index = i;
                        x++;
                    }
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectedItem = tab[cmb_Interface.SelectedIndex];
        }
    }
}
