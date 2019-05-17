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
        public int SelectedItem;
        public AdvancedForm()
        {
            InitializeComponent();
            /*foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                cmb_Interface.Items.Add(netInterface.NetworkInterfaceType);
                cmb_Interface.SelectedIndex = 0;
            }*/

            List<NetworkInterface> Interfaces = new List<NetworkInterface>();
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    Interfaces.Add(nic);
                }
            }

            foreach (NetworkInterface nic in Interfaces)
            {
                if (nic.GetIPProperties().GetIPv4Properties() != null)
                {
                    cmb_Interface.Items.Add(nic.NetworkInterfaceType);
                }
            }
            cmb_Interface.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = cmb_Interface.SelectedIndex;
        }
    }
}
