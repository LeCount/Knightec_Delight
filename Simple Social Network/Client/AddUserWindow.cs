using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientTcpCommunication
{
    public partial class AddUserWindow : Form
    {
        private static AddUserWindow singletonInstance;

        public static AddUserWindow GetForm
        {
            get
            {
                if (singletonInstance == null || singletonInstance.IsDisposed)
                    singletonInstance = new AddUserWindow();

                return singletonInstance;
            }
        }

        public AddUserWindow()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Visible = false;
            LoginWindow.GetForm.Show();
        }
    }
}
