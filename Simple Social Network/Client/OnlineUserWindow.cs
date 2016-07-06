using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    /// <summary>
    /// A form intended to display tools and information for a user that is concidered to be loged in on the server.
    /// For now, there is no design.
    /// </summary>
    public partial class OnlineUserWindow : Form
    {
        private static OnlineUserWindow singleton_instance;
        private Client client_networking = null;

        public OnlineUserWindow(){InitializeComponent();}

        public OnlineUserWindow(Client reference)
        {
            client_networking = reference;
            InitializeComponent();
        }

        /// <summary>Creates a singleton instance of this class.</summary>
        /// <param name="reference">Reference to the calling client-class.</param>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static OnlineUserWindow getForm(Client c) { return singleton_instance = new OnlineUserWindow(c); }

        /// <summary>Gets the singleton instance of this class.</summary>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static OnlineUserWindow getForm() { return singleton_instance; }
    }
}
