
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace ServerTcpCommunication
{
    public class Server
    {
        private const int SERVER_PORT = 8001;
        private string SERVER_IP = "?";
        private TcpListener myTCPListener = null;

        private ServerWindow serverWindow = null;

        public Server()
        {
            init();
        }

        private void init()
        {
            serverWindow = ServerWindow.getForm(this);

            if(InitialCheckOfNetworkStatus())
                serverWindow.AddServerLog("Network available");
            else
                serverWindow.AddServerLog("Network unavailable");

            SERVER_IP = GetServerIP();
            serverWindow.Text = "Server           #IP Address: " + SERVER_IP + "           #Port: " + SERVER_PORT + "           #Online since: " + GetUpTimeStart();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            IPAddress ipAddr = IPAddress.Parse(SERVER_IP);

            /* Initializes the Listener */
            myTCPListener = new TcpListener(ipAddr, SERVER_PORT);

            /* Start Listeneting at the specified port */
            myTCPListener.Start();

            serverWindow.AddServerLog("Waiting for a connection...");

            Thread listen = new Thread(ListenForConnectRequest);
            listen.Start();

            serverWindow.ShowDialog();
        }

        private void ListenForConnectRequest()
        {
            while (!myTCPListener.Pending()){}

            Socket currentSocket = myTCPListener.AcceptSocket();
            serverWindow.AddServerLog("Connection accepted from " + currentSocket.RemoteEndPoint);
        }

        public bool InitialCheckOfNetworkStatus()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                serverWindow.AddServerLog("Network available");
            else
                serverWindow.AddServerLog("Network unavailable");
        }

        private string GetServerIP()
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

        private string GetUpTimeStart()
        {
            return DateTime.Now.ToString("yyyy-MM-dd, HH.mm.ss"); 
        }
    }
}
