using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientTcpCommunication
{
    public class Client
    {
        /**something to consider: try connect timeout: http://www.splinter.com.au/opening-a-tcp-connection-in-c-with-a-custom-t/ **/

        private const int SERVER_PORT_NUMBER = 8001;
        private String SERVER_IP_ADDR = "?";
        private String CLIENT_IP_ADDR = "?";

        private LoginWindow loginWindow = null;
        private AddUserWindow addUserWindow = null;

        private Thread read_thread = null;
        private byte[] charArrSend = null;
        private byte[] charArrReceive = new byte[100];
        private Stream clientStream = null;

        private string msg = "";
        private TcpClient TCP_Client = new TcpClient();
        private ASCIIEncoding asciiEncode = new ASCIIEncoding();
        private int numOfBytesRead = 0;
        private int byteCount = 0;
        private bool connected;

        private Thread serverConnect = null;

        public Client()
        {
            init();
        }

        private void init()
        {
            loginWindow = LoginWindow.getForm(this);
            addUserWindow = AddUserWindow.getForm(this);

            CLIENT_IP_ADDR = GetClientIP();
            SERVER_IP_ADDR = CLIENT_IP_ADDR; //this needs to be changed if a different computer is used!

            loginWindow.ChangeClientIpAddress(CLIENT_IP_ADDR);

            if (InitialCheckOfNetworkStatus())
                loginWindow.ChangeNetworkAvailability("Network status: Available");
            else
                loginWindow.ChangeNetworkAvailability("Network status: Unavailable");

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            serverConnect = new Thread(TryConnectToServer);
            serverConnect.Start();

            loginWindow.ShowDialog();
            addUserWindow.ShowDialog();
            addUserWindow.Visible = false;
        }

        private void TryConnectToServer()
        {
            while (connected == false)
            {
                try
                {
                    TCP_Client.Connect(IPAddress.Parse(SERVER_IP_ADDR), SERVER_PORT_NUMBER);
                    clientStream = TCP_Client.GetStream();
                    connected = true;
                    read_thread = new Thread(new ThreadStart(Client_read));

                    loginWindow.ChangeServerAvailability("Server status: Available");
                }

                catch(Exception)
                {
                    loginWindow.ChangeServerAvailability("Server status: Unavailable ");
                }
            }
        }

        internal void StopAllThreads()
        {
            serverConnect.Abort();
        }

        public bool InitialCheckOfNetworkStatus()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                loginWindow.ChangeNetworkAvailability("Network status: Available");
            else
                loginWindow.ChangeNetworkAvailability("Network status: Unavailable");
        }

        private string SetServerIP()
        {
            throw new NotImplementedException();
        }

        public string GetClientIP()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    return IP4Address;
                }
            }

            return "No ip address found";
        }

        private void Client_read()
        {
            numOfBytesRead = clientStream.Read(charArrReceive, 0, 100);

            for (byteCount = 0; byteCount < numOfBytesRead; byteCount++)
            {
                msg = msg + Convert.ToChar(charArrReceive[byteCount]);
            }

            if (!(msg == null || msg == ""))
            {
                msg = "Incoming message: " + '"' + msg + '"';
            }
        }

        public void Client_write(String stringToSend)
        {
            charArrSend = asciiEncode.GetBytes(stringToSend);
            clientStream.Write(charArrSend, 0, charArrSend.Length);
        }

        public void Disconnect()
        {
            read_thread.Abort();
            TCP_Client.Close();
        }

        public void ShowLoginWindow()
        {
            loginWindow.Show();
        }

        public void ShowAddUserWindow()
        {
            addUserWindow.ShowDialog();
        }

    }
}
