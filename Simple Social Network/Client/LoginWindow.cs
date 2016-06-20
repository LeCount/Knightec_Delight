using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace ClientTcpCommunication
{
    public partial class LoginWindow : Form
    {
        private static LoginWindow singletonInstance;
        delegate void SetTextCallback(string text);
        Client clientNetworking = null;

        public LoginWindow()
        {
            InitializeComponent();
            initializeNetworkStatus();
        }

        public LoginWindow(Client c)
        {
            clientNetworking = c;
            InitializeComponent();
            initializeNetworkStatus();
        }

        public static LoginWindow getForm(Client c)
        {
            return singletonInstance = new LoginWindow(c);
        }

        public static LoginWindow getForm()
        {
            return singletonInstance;
        }

        private void initializeNetworkStatus()
        {

            if (clientNetworking.checkInitialNetworkStatus())
                updateNetworkAvailability("Network status: Available");
            else
                updateNetworkAvailability("Network status: Unavailable");

            NetworkChange.NetworkAvailabilityChanged += clientNetworking.monitorNetworkAvailability;

            updateClientIpAddress("Client ip address: " + clientNetworking.getClientIP());
        }

        public void updateNetworkAvailability(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lbl_network.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(updateNetworkAvailability);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lbl_network.Text = text;
            }

            updateClientIpAddress("Client ip address: " + clientNetworking.getClientIP());
        }

        public void updateClientIpAddress(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_ip_addr.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(updateClientIpAddress);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lbl_ip_addr.Text = text;
            }
        }

        public void updateServerAvailability(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_ip_addr.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(updateServerAvailability);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lbl_server_connect.Text = text;
            }
        }

        private void btn_new_user_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            AddUserWindow.GetForm.Show();
        }
    }
}
