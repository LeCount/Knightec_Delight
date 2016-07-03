using System;
using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    /// <summary>A form for collecting a client's username, password and email, for validation on the server-side.</summary>
    public partial class AddUserWindow : Form
    {
        /// <summary>A reference to a singleton based instance of the class.</summary>
        private static AddUserWindow singleton_instance;

        /// <summary>Reference to the calling client-class.</summary>
        private Client client_networking = null;

        /// <summary>Constructor 1; Initialize and setup the window components.</summary>
        private AddUserWindow() {InitializeComponent();}

        /// <summary>Constructor 2; Initialize and setup the form's components, and set reference to calling client-class.</summary>
        /// <param name="reference">Reference to the calling client-class.</param>
        private AddUserWindow(Client reference)
        {
            client_networking = reference;
            InitializeComponent();
        }

        /// <summary> Creates a singleton instance of this class.</summary>
        /// <param name="reference">Reference to the calling client-class.</param>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static AddUserWindow getForm(Client reference) {return singleton_instance = new AddUserWindow(reference);}

        /// <summary>Gets the singleton instance of this class.</summary>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static AddUserWindow getForm(){return singleton_instance;}

        /// <summary>Overridden eventhandler, for when this form is closed or terminated.</summary>
        /// <param name="e">Information regarding the event of closing of this form.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Visible = false;
            client_networking.ShowLoginWindow();
        }

        /// <summary> Eventhandler for when text in any of the textboxes in this form, gets changed.</summary>
        /// <param name="sender">The caller- class, invoking this method.</param>
        /// <param name="e">Information regarding this event.</param>
        private void textBox_username_TextChanged(object sender, EventArgs e) { btn_submit.Enabled = true; }

        /// <summary>Eventhandler for when button "Submit" is clicked.</summary>
        /// <param name="sender">The caller- class, invoking this method.</param>
        /// <param name="e">Information regarding this event.</param>
        private void btn_submit_Click(object sender, EventArgs e)
        {
            btn_submit.Enabled = false;

            client_networking.SendJoinRequest(textBox_username.Text, textBox_password.Text, textBox_mail.Text);

            Visible = false;
            client_networking.ShowLoginWindow();

            textBox_username.Clear();
            textBox_password.Clear();
            textBox_mail.Clear();
        }
    }
}
