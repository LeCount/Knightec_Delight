using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientAddUserForm : Form
    {
        private static ClientAddUserForm singletonInstance;

        public static ClientAddUserForm GetForm
        {
            get
            {
                if (singletonInstance == null || singletonInstance.IsDisposed)
                    singletonInstance = new ClientAddUserForm();

                return singletonInstance;
            }
        }

        public ClientAddUserForm()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Visible = false;
            ClientLoginForm.GetForm.Show();
        }
    }
}
