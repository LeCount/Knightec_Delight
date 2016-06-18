using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class ClientLoginForm : Form
    {
        private static ClientLoginForm singletonInstance;
        delegate void SetTextCallback(string text);

        public static ClientLoginForm GetForm
        {
            get
            {
                if (singletonInstance == null || singletonInstance.IsDisposed)
                    singletonInstance = new ClientLoginForm();

                return singletonInstance;
            }
        }

        public ClientLoginForm()
        {
            InitializeComponent();
            initializeComponentValues();
        }

        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }

        private void initializeComponentValues()
        {
            
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                this.lbl_network.Text = "Network status: Available";
            else
                this.lbl_network.Text = "Network status: Unavailable";

            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;

            this.lbl_ip_addr.Text = "Client ip address: " + GetIP4Address();
        }

        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                updateNetworkAvailabilityText("Network status: Available");
            }
            else
            {
                updateNetworkAvailabilityText("Network status: Unavailable");
            }
        }
        private void updateNetworkAvailabilityText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lbl_network.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(updateNetworkAvailabilityText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lbl_network.Text = text;
            }

            this.lbl_ip_addr.Text = "Client ip address: " + GetIP4Address();
        }

        private void btn_new_user_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ClientAddUserForm.GetForm.Show();
        }
    }
}
