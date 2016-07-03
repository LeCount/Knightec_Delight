using System;
using System.Windows.Forms;

namespace Async_TCP_server_networking
{
    public partial class SMTPClientWindow : Form
    {
        private Server server_networking = null;

        public SMTPClientWindow(Server reference)
        {
            server_networking = reference;
            InitializeComponent();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            server_networking.SetSmtpMail(textBox_mail.Text);
            server_networking.SetSmtpPassword(textBox_password.Text);
            server_networking.SetupSmtpClientOverGoogle(textBox_mail.Text, textBox_password.Text);

            if (server_networking.VerifySmtpClient(textBox_mail.Text))
                Close();
            else
                MessageBox.Show("SMPT client autentication failed. Please try again.");
        }
    }
}
