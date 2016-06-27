using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using ServerDBCommunication;
using System.Linq;
using Shared_resources;
using System.Runtime.Serialization.Formatters.Binary;

namespace Async_TCP_server_networking
{
    public class Server
    {
        private Thread connectListener = null;
        private Thread transmissionListener = null;
        private Thread clientRequestExecutioner = null;
        private List<Socket> clientSocketList = new List<Socket>();

        private const int CONNECT_REQUEST =             1;
        private const int DISCONNECT_REQUEST =          2;
        private const int AVAILABLE_USERS_REQUEST =     3;
        private const int FRIEND_REQUEST =              4;
        private const int CLIENT_DATA_ACCESS_REQUEST =  5;
        private const int FORWARD_MESSAGE_REQUEST =     6;
        private const int SERVER_PORT =                 8001;
        private const int BUFFER_SIZE =                 1024;
        private const string DATABASE_FILE =            "serverDB.db";

        private string SERVER_IP = "?";
        private TcpListener TCPListener = null;
        
        private List<string> clientRequestList = new List<string>();
        private Socket currentClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private ServerWindow serverWindow = null;
        private ServerDatabase db = new ServerDatabase(DATABASE_FILE);

        public Server()
        {
            init();
        }

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
            ServerStart();
            serverWindow.ShowDialog();
        }

        public void ServerStart()
        {
            TCPListener = new TcpListener(IPAddress.Parse(SERVER_IP), SERVER_PORT);
            TCPListener.Start();

            connectListener = new Thread(ListenForConnectRequest);
            connectListener.Start();

            transmissionListener = new Thread(ListenForTransmissions);
            transmissionListener.Start();

            clientRequestExecutioner = new Thread(ExecuteClientRequest);
            clientRequestExecutioner.Start();
        }

        public void ServerStop()
        {
            clientRequestExecutioner.Abort();
            transmissionListener.Abort();
            connectListener.Abort();
            TCPListener.Stop();
        }

        private void ListenForTransmissions()
        {
            int numOfBytesRead = 0;
            byte[] receiveBuffer = new byte[BUFFER_SIZE];
            Serializer s = new Serializer();

            serverWindow.AddServerLog("Listening for transmissions...");

            while (true)
            {
                if (currentClientSocket.Connected)
                {
                    try
                    {
                        numOfBytesRead = currentClientSocket.Receive(receiveBuffer);

                        if (numOfBytesRead > 0)
                        {
                            TCP_message msg = s.Deserialize_msg(receiveBuffer);

                            //AddClientRequest(msg);

                            serverWindow.AddServerLog("TYPE: " + msg.type);
                            serverWindow.AddServerLog("SOURCE: " + msg.source);
                            serverWindow.AddServerLog("DESTINATION: " + msg.destination);

                            //Later, this can be displayed after message has been parsed
                        }
                    }
                    catch(Exception)
                    {
                        //No message received!
                    }
                }
                else
                {
                    currentClientSocket.Close();
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
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                    return IPA.ToString();
            }

            return "No ip address found";
        }

        private string GetServerUpTimeStart()
        {
            return DateTime.Now.ToString("yyyy-MM-dd, HH.mm.ss");
        }

        private void ParseClientRequest(string incomingMessage)
        {
            //temp variables to c that things turn out right
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
                case AVAILABLE_USERS_REQUEST:
                    HandleAvailableUsersRequest();
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

        private void HandleAvailableUsersRequest()
        {
            throw new NotImplementedException();
        }

        private void AddClientRequest(TCP_message msg)
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