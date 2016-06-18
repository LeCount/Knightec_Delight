using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Client
{
    public partial class ClientLoginForm : Form
    {
        private static ClientLoginForm singletonInstance;

        public static ClientLoginForm GetForm
        {
            get
            {
                if (singletonInstance == null || singletonInstance.IsDisposed)
                    singletonInstance = new ClientLoginForm();

                return singletonInstance;
            }
        }

        public ClientLoginForm()
        {
            

            InitializeComponent();
            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                MessageBox.Show("Network connected!");
            else
                MessageBox.Show("Network disconnected!");
        }



        private void btn_new_user_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ClientAddUserForm.GetForm.Show();
        }
    }
}
