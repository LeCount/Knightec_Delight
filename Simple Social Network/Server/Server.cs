using System.Data.SQLite;   //http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
using System;
using System.Collections.Generic;
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

        private List<Socket> socketList = new List<Socket>();
        Socket currentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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


            Thread listenConnect = new Thread(ListenForConnectRequest);
            listenConnect.Start();
            Thread.Sleep(1000);

            Thread listenMessages = new Thread(ListenForMessages);
            listenMessages.Start();

            serverWindow.ShowDialog();
        }

        private void ListenForMessages()
        {
            byte[] charArrReceive = new byte[100];
            String incomingMsg = "";
            serverWindow.AddServerLog("Listening for communications...");
            int k;

            while (true)
            {
                if (currentSocket.Connected)
                {
                    k = currentSocket.Receive(charArrReceive);

                    for (int i = 0; i < k; i++)
                        incomingMsg = incomingMsg + (Convert.ToChar(charArrReceive[i]));

                    serverWindow.AddServerLog(incomingMsg);
                }
            }
        }

        private void ListenForConnectRequest()
        {
            while(true)
            { 
                while (!myTCPListener.Pending()){}

                currentSocket = myTCPListener.AcceptSocket();
                socketList.Add(currentSocket);
                serverWindow.AddServerLog("Connection accepted from " + currentSocket.RemoteEndPoint);
            }
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