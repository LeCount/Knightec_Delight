using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace ClientTcpCommunication
{
    public partial class LoginWindow : Form
    {
        private static LoginWindow singletonInstance;
        delegate void SetTextEventHandler(string text);
        private Client clientNetworking = null;

        public LoginWindow() { InitializeComponent(); }

        public LoginWindow(Client c)
        {
            clientNetworking = c;
            InitializeComponent();
        }

        public static LoginWindow getForm(Client c) { return singletonInstance = new LoginWindow(c); }

        public static LoginWindow getForm() { return singletonInstance; }

        public void ChangeNetworkAvailability(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lbl_network.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(ChangeNetworkAvailability);
                this.Invoke(delegateMethod, new object[] { text });
            }
            else
            {
                this.lbl_network.Text = text;
            }

            ChangeClientIpAddress("Client ip address: " + clientNetworking.GetClientIP());
        }

        public void ChangeClientIpAddress(string text)
        {
            // InvokeRequired  compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_ip_addr.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(ChangeClientIpAddress);
                this.Invoke(delegateMethod, new object[] { text });
            }
            else
            {
                this.lbl_ip_addr.Text = text;
            }
        }

        public void ChangeServerAvailability(string text)
        {
            // InvokeRequired  compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_ip_addr.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(ChangeServerAvailability);
                this.Invoke(delegateMethod, new object[] { text });
            }
            else
            {
                this.lbl_server_connect.Text = text;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void btn_new_user_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            clientNetworking.ShowAddUserWindow();
        }
    }
}
