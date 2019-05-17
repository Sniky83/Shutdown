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
        public int[] tab = new int[10];
        public AdvancedForm()
        {
            InitializeComponent();
            byte i = 0;
            byte j = 0;
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
                    //Console.WriteLine(tab[j]);
                    j++;
                }
                i++;
            }

            /*List<NetworkInterface> Interfaces = new List<NetworkInterface>();
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                Interfaces.Add(nic);

                /*if (nic.OperationalStatus == OperationalStatus.Up)
                {
                }
            }

            foreach (NetworkInterface nic in Interfaces)
            {
                if (nic.GetIPProperties().GetIPv4Properties() != null)
                {
                    cmb_Interface.Items.Add(nic.NetworkInterfaceType);
                    Console.WriteLine();
                }
            }*/
            /*NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            Console.WriteLine("IPv4 interface information for {0}.{1}",properties.HostName, properties.DomainName);


            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.Supports(NetworkInterfaceComponent.IPv4) == false)
                {
                    continue;
                }

                //if (!adapter.Description.Equals(adapter.NetworkInterfaceType, StringComparison.OrdinalIgnoreCase))
                //{
                    //continue;
                //}
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
            /*string test = e.ToString();
            int Index = int.Parse(test);
            cmb_Interface.SelectedIndex;*/
            SelectedItem = tab[cmb_Interface.SelectedIndex];
            //Console.WriteLine(SelectedItem);
            //Console.WriteLine(tab[0]);
        }
    }
}
