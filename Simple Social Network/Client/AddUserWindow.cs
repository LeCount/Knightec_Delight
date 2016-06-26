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
    }
}
