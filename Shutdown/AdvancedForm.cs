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
                    Console.WriteLine();
                }
            }
            /*NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            Console.WriteLine("IPv4 interface information for {0}.{1}",
                          properties.HostName, properties.DomainName);


            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.Supports(NetworkInterfaceComponent.IPv4) == false)
                {
                    continue;
                }

                if (!adapter.Description.Equals(adapter.NetworkInterfaceType, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                Console.WriteLine(adapter.Description);
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                IPv4InterfaceProperties p = adapterProperties.GetIPv4Properties();
                if (p == null)
                {
                    Console.WriteLine("No information is available for this interface.");
                    continue;
                }
                Console.WriteLine("  Index : {0}", p.Index);
            }*/

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
