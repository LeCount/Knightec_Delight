using System;
using System.Windows.Forms;

namespace Async_TCP_server_networking
{
    /// <summary>
    /// A form for displaying server-related information: Status, events, incoming requests, etc.
    /// </summary>
    public partial class ServerWindow : Form
    {
        /// <summary>
        /// A reference to a singleton based instance of the class.
        /// </summary>
        private static ServerWindow singletonInstance;

        /// <summary>
        /// Reference to the calling server-class.
        /// </summary>
        private Server serverNetworking = null;

        /// <summary>
        /// Declaration of a delegate method to be used for call-back, for setting text in this form from another thread; 
        /// used to reference any method that has the same return-type and parameter-types.
        /// </summary>
        /// <param name="text">Delegate declaration parameter.</param>
        delegate void SetTextEventHandler(string text);

        /// <summary>
        /// Declaration of a delegate method to be used for call-back, for deleting an list-item in this form from another thread; 
        /// used to reference any method that has the same return-type and parameter-types.
        /// </summary>
        delegate void RemoveTextFromListHanler();

        /// <summary>
        /// Constructor 1; Initialize and setup the forms's components.
        /// </summary>
        private ServerWindow(){ InitializeComponent();}

        /// <summary>
        /// Constructor 2; Initialize and setup the form's components, and set reference to calling server-class.
        /// </summary>
        /// <param name="reference">Reference to the calling client-class.</param>
        private ServerWindow(Server reference)
        {
            serverNetworking = reference;
            InitializeComponent();
        }

        /// <summary>
        /// Creates a singleton instance of this class.
        /// </summary>
        /// <param name="reference">Reference to the calling server-class.</param>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static ServerWindow getForm(Server s) { return singletonInstance = new ServerWindow(s); }

        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        /// <returns>Pointer to singleton instance of this class.</returns>
        public static ServerWindow getForm() { return singletonInstance; }

        /// <summary>
        /// Add an text to the server_log listbox, denoting an event.
        /// </summary>
        /// <param name="text">Description of event.</param>
        public void AddServerLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(AddServerLog);
                Invoke(delegateMethod, new object[] { text });
            }
            else
                listbox_server_log.Items.Add(DateTime.Now.ToString("HH:mm:ss: ") + text);
        }

        /// <summary>
        /// Add an text to the server_request listbox, denoting a request from a client.
        /// </summary>
        /// <param name="text">Description of event.</param>
        public void AddServerRequest(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(AddServerRequest);
                Invoke(delegateMethod, new object[] { text });
            }
            else
                listBox_requests.Items.Add(text);
        }

        /// <summary>
        /// Remove the next item from the server_request listbox, where an item denotes a request from a client.
        /// </summary>
        public void RemoveNextRequestText()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (InvokeRequired)
            {
                RemoveTextFromListHanler delegateMethod = new RemoveTextFromListHanler(RemoveNextRequestText);
                Invoke(delegateMethod);
            }
            else
            {
                if(listBox_requests.Items.Count > 0)
                    listBox_requests.Items.RemoveAt(0);
            }
        }

        /// <summary>
        /// Overridden eventhandler, for when this form is closed or terminated.
        /// </summary>
        /// <param name="e">Information regarding the event of closing of this form.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            serverNetworking.ServerStop();
            System.Environment.Exit(1);
        }
    }
}
