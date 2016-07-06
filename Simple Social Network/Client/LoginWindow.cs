using System;
using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    /// <summary>
    /// A form for providing the user with a login controll and a add-user-controll. 
    /// But also to provide information regarding the status of the network, the status of the server, and the ip address of the client. 
    /// </summary>
    public partial class LoginWindow : Form
    {
        /// <summary>A reference to a singleton based instance of the class.</summary>
        private static LoginWindow singleton_instance;

        /// <summary>
        /// Declaration of a delegate method to be used for call-back, for setting text in this form from another thread; 
        /// used to reference any method that has the same return-type and parameter-types.
        /// </summary>
        /// <param name="text">Delegate declaration parameter.</param>
        private delegate void SetTextEventHandler(string text);

        /// <summary>Reference to the calling client-class.</summary>
        private Client client_networking = null;

        /// <summary>Constructor 1; Initialize and setup the form's components.</summary>
        public LoginWindow() { InitializeComponent(); }

        /// <summary>Constructor 2; Initialize and setup the form's components, and set reference to calling client-class.</summary>
        /// <param name="reference">Reference to the calling client-class.</param>
        public LoginWindow(Client reference)
        {
            client_networking = reference;
            InitializeComponent();
        }

        /// <summary>Creates a singleton instance of this class.</summary>
        /// <param name="reference">Reference to the calling client-class.</param>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static LoginWindow getForm(Client c) { return singleton_instance = new LoginWindow(c); }

        /// <summary>Gets the singleton instance of this class.</summary>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static LoginWindow getForm() { return singleton_instance; }

        /// <summary>Set the network availability lable in this form.</summary>
        /// <param name="text">Text to be writen to the lable.</param>
        public void DisplayNetworkAvailability(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_network.InvokeRequired)
            {
                SetTextEventHandler delegate_method = new SetTextEventHandler(DisplayNetworkAvailability);

                //invoke the thread responsible for the lable
                Invoke(delegate_method, new object[] { text });
            }
            else
               lbl_network.Text = text;

            client_networking.GetClientIP();
        }

        /// <summary>Set the ip address lable in this form.</summary>
        /// <param name="text">Text to be writen to the lable.</param>
        public void SetClientIpAddress(string text)
        {
            // InvokeRequired  compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_ip_addr.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(SetClientIpAddress);

                //invoke the thread responsible for the lable
                Invoke(delegateMethod, new object[] { text });
            }
            else
                lbl_ip_addr.Text = "Client ip address: " + text;
        }

        /// <summary>Set the server availability lable in this form.</summary>
        /// <param name="text">Text to be writen to the lable.</param>
        public void SetServerAvailability(string text)
        {
            // InvokeRequired  compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl_ip_addr.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(SetServerAvailability);

                //invoke the thread responsible for the lable
                Invoke(delegateMethod, new object[] { text });
            }
            else
                lbl_server_connect.Text = text;
        }

        /// <summary>Overridden eventhandler, for when this form is closed or terminated.</summary>
        /// <param name="e">Information regarding the event of closing of this form.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            client_networking.Disconnect();
            System.Environment.Exit(1);
        }

        /// <summary>Eventhandler for when button "New user" is clicked.</summary>
        /// <param name="sender">The caller- class, invoking this method.</param>
        /// <param name="e">Information regarding this event.</param>
        private void btn_new_user_Click_1(object sender, EventArgs e)
        {
            Visible = false;
            client_networking.ShowAddUserWindow();
        }

        /// <summary>Eventhandler for when button "Login" is clicked.</summary>
        /// <param name="sender">The caller- class, invoking this method.</param>
        /// <param name="e">Information regarding this event.</param>
        private void btn_login_Click(object sender, EventArgs e)
        {
            if(textBox_user.Text != null && textBox_password.Text != null)
                client_networking.SendLoginRequest(textBox_user.Text, textBox_password.Text, textBox_code.Text);
        }
    }
}
