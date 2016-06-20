using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace ClientTcpCommunication
{
    public partial class LoginWindow : Form
    {
        private static LoginWindow singletonInstance;
        delegate void SetTextCallback(string text);

        public LoginWindow()
        {
            MessageBox.Show("Added code from new pc! ");
            InitializeComponent();
            initializeComponentValues();
        }

        public static LoginWindow GetForm
        {
            get
            {
                if (singletonInstance == null || singletonInstance.IsDisposed)
                    singletonInstance = new LoginWindow();

                return singletonInstance;
            }
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

        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    return IP4Address;
                }
            }

            return "No ip address found";
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

        private void btn_new_user_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            AddUserWindow.GetForm.Show();
        }
    }
}
