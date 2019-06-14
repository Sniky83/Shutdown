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
        public int secondes; //{get; private set;}

        public AdvancedForm()
        {
            InitializeComponent();
            getSecondesFromNumUpDown();
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
        }
        private void getSecondesFromNumUpDown()
        {
            secondes = (int)(numUpDown_TempsRestant.Value * 60);
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectedItem = tab[cmb_Interface.SelectedIndex];
        }

        private void NumUpDown_TempsRestant_ValueChanged(object sender, EventArgs e)
        {
            getSecondesFromNumUpDown();
        }
    }
}
