using Shared_resources;
using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    public partial class AddUserWindow : Form
    {
        private static AddUserWindow singletonInstance;
        private Client clientNetworking = null;

        public AddUserWindow(Client c)
        {
            clientNetworking = c;
            InitializeComponent();
        }

        public AddUserWindow()
        {
            InitializeComponent();
        }

        public static AddUserWindow getForm(Client c)
        {
            return singletonInstance = new AddUserWindow(c);
        }

        public static AddUserWindow getForm()
        {
            return singletonInstance;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Visible = false;
            clientNetworking.ShowLoginWindow();
        }

        private void textBox_username_TextChanged(object sender, System.EventArgs e)
        {
            this.btn_submit.Enabled = true;
        }

        private void btn_submit_Click(object sender, System.EventArgs e)
        {
            this.btn_submit.Enabled = false;

            clientNetworking.SendJoinRequest(textBox_username.Text, textBox_password.Text, textBox_mail.Text);

            this.Visible = false;
            clientNetworking.ShowLoginWindow();

            textBox_username.Clear();
            textBox_password.Clear();
            textBox_mail.Clear();
        }
    }
}
