
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using ServerDBCommunication;
using System.Linq;

namespace Async_TCP_server_networking
{
    public class Server
    {
        private Thread connectListener = null;
        private Thread transmissionListener = null;
        private Thread clientRequestExecutioner = null;

        private const string databaseFile = "serverDatabase.sqlite";
        ServerDatabase db = new ServerDatabase(databaseFile);

        private const int CONNECT_REQUEST = 1;
        private const int DISCONNECT_REQUEST = 2;
        private const int FRIEND_REQUEST = 3;
        private const int CLIENT_DATA_ACCESS_REQUEST = 4;
        private const int FORWARD_MESSAGE_REQUEST = 5;

        private const int SERVER_PORT = 8001;
        private const int BUFFER_SIZE = 100;
        private string SERVER_IP = "?";
        private TcpListener TCPListener = null;

        private ServerWindow serverWindow = null;

        private List<Socket> clientSocketList = new List<Socket>();
        private List<string> clientRequestList = new List<string>();

        Socket currentClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        

        public Server() {init();}

        private void init()
        {
            serverWindow = ServerWindow.getForm(this);
            InitialCheckOfNetworkStatus();

            SERVER_IP = GetServerIP();
            serverWindow.Text = "Server           " +
                                "#IP Address: " + SERVER_IP + "           " +
                                "#Port: " + SERVER_PORT + "           " + 
                                "#Online since: " + GetServerUpTimeStart();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            TCPListener = new TcpListener(IPAddress.Parse(SERVER_IP), SERVER_PORT);
            TCPListener.Start();

            connectListener = new Thread(ListenForConnectRequest);
            connectListener.Start();

            transmissionListener = new Thread(ListenForTransmissions);
            transmissionListener.Start();

            clientRequestExecutioner = new Thread(ExecuteClientRequest);
            clientRequestExecutioner.Start();

            serverWindow.ShowDialog();
        }

        private void ListenForTransmissions()
        {
            byte[] receiveBuffer = new byte[BUFFER_SIZE];
            String incomingMsg = "";
            serverWindow.AddServerLog("Listening for transmissions...");
            int sizeOfBuffer;

            while (true)
            {
                if (currentClientSocket.Connected)
                {
                    try
                    {
                        sizeOfBuffer = currentClientSocket.Receive(receiveBuffer);

                        for (int i = 0; i < sizeOfBuffer; i++)
                            incomingMsg = incomingMsg + (Convert.ToChar(receiveBuffer[i]));

                        //TODO
                        //Store incoming message in a list, on the server side, and let the thread "clientRequestExecutioner"
                        //handle the parsing and the execution of each request sequentially, and separately.

                        //AddClientRequest(incomingMsg);

                        serverWindow.AddServerLog(incomingMsg);
                    }
                    catch(Exception)
                    {
                        //No message received!
                    }
                }
            }
        }

        private void ListenForConnectRequest()
        {
            serverWindow.AddServerLog("Waiting for connect requests...");

            while (true)
            { 
                while (!TCPListener.Pending()){}

                //this does not mean that the client has logged in. Only that the client can communicate with the server
                currentClientSocket = TCPListener.AcceptSocket();
                clientSocketList.Add(currentClientSocket);
                serverWindow.AddServerLog("Connection accepted from " + currentClientSocket.RemoteEndPoint);
            }
        }

        public void InitialCheckOfNetworkStatus()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                serverWindow.AddServerLog("Network available");
            else
                serverWindow.AddServerLog("Network unavailable");
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

        private string GetServerUpTimeStart()
        {
            return DateTime.Now.ToString("yyyy-MM-dd, HH.mm.ss"); 
        }

        public void stopAllThreads()
        {
            connectListener.Abort();
            transmissionListener.Abort();
            Thread.Sleep(200);
        }

        private void ParseClientRequest(string incomingMessage)
        {
            int requestType = 0;
            string uName = null;
            string pWord = null;
            Socket sender = null;
            List<string> client_attributes = new List<string>();

            requestType = GetRequestType(incomingMessage);


            switch (requestType)
            {
                case CONNECT_REQUEST:
                    uName = GetUsernameFromMsg(incomingMessage);
                    pWord = GetPasswordFromMsg(incomingMessage);
                    HandleConnectionRequest(uName, pWord);
                    break;
                case DISCONNECT_REQUEST:
                    uName = GetUsernameFromMsg(incomingMessage);
                    HandleDisconnectionRequest(uName);
                    break;
                case FRIEND_REQUEST:
                    uName = GetUsernameFromMsg(incomingMessage);
                    HandleFriendRequest(sender, uName);
                    break;
                case CLIENT_DATA_ACCESS_REQUEST:
                    uName = GetUsernameFromMsg(incomingMessage);
                    client_attributes = GetAttributesToAccessFromMsg(incomingMessage);
                    HandleDataAccessRequest(uName, client_attributes);
                    break;
                case FORWARD_MESSAGE_REQUEST:
                    uName = GetUsernameFromMsg(incomingMessage);
                    HandleForwardingOfMessage(uName);
                    break;
                case default(int):
                    break;
            }
        }

        private void AddClientRequest(string incomingMessage)
        {
            throw new NotImplementedException();
        }

        private void ExecuteClientRequest()
        {
            while(true)
            {
                if(clientRequestList.Count != 0 || clientRequestList == null)
                {
                    ParseClientRequest(clientRequestList.First());
                    clientRequestList.RemoveAt(0);
                }
            }
        }

        private void HandleFriendRequest(Socket sender, string uName)
        {
            throw new NotImplementedException();
        }

        private List<string> GetAttributesToAccessFromMsg(string incomingMessage)
        {
            throw new NotImplementedException();
        }

        private string GetPasswordFromMsg(string incomingMessage)
        {
            throw new NotImplementedException();
        }

        private string GetUsernameFromMsg(string incomingMessage)
        {
            throw new NotImplementedException();
        }

        private int GetRequestType(string incomingMessage)
        {
            throw new NotImplementedException();
        }

        private void HandleConnectionRequest(string username, string password)
        {
            throw new NotImplementedException();
        }

        private void HandleDisconnectionRequest(string username)
        {
            throw new NotImplementedException();
        }

        private void HandleDataAccessRequest(string usernameToAccess, List<string> attributesToAccess)
        {
            throw new NotImplementedException();
        }

        private void HandleForwardingOfMessage(string recipentUsername)
        {
            throw new NotImplementedException();
        }


    }
}