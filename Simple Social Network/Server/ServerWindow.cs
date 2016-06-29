using System;
using System.Windows.Forms;

namespace Async_TCP_server_networking
{
    public partial class ServerWindow : Form
    {
        private static ServerWindow singletonInstance;
        private Server serverNetworking = null;

        delegate void SetTextEventHandler(string text);
        delegate void RemoveTextFromListHanler();

        public ServerWindow()
        {
            InitializeComponent();
        }

        public ServerWindow(Server s)
        {
            serverNetworking = s;
            InitializeComponent();
        }

        public static ServerWindow getForm(Server s) { return singletonInstance = new ServerWindow(s); }

        public static ServerWindow getForm() { return singletonInstance; }

        public void AddServerLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(AddServerLog);
                this.Invoke(delegateMethod, new object[] { text });
            }
            else
            {
                this.listbox_server_log.Items.Add(DateTime.Now.ToString("HH:mm:ss: ") + text);
            }
        }

        public void DisplayRequestInListbox(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetTextEventHandler delegateMethod = new SetTextEventHandler(DisplayRequestInListbox);
                this.Invoke(delegateMethod, new object[] { text });
            }
            else
            {
                this.listBox_requests.Items.Add(text);
            }
        }

        public void RemoveNextRequestText()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                RemoveTextFromListHanler delegateMethod = new RemoveTextFromListHanler(RemoveNextRequestText);
                this.Invoke(delegateMethod);
            }
            else
            {
                if(listBox_requests.Items.Count > 0)
                    this.listBox_requests.Items.RemoveAt(0);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            serverNetworking.ServerStop();
            System.Environment.Exit(1);
        }
    }
}
