using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using ServerDBCommunication;
using System.Linq;
using Shared_resources;

namespace Async_TCP_server_networking
{
    public class Server
    {
        private Thread connectListener = null;
        private Thread transmissionListener = null;
        private Thread clientRequestExecutioner = null;

        private List<Socket> clientSocketList = new List<Socket>();
        private TcpListener TCPListener = null;
        private List<TCP_message> clientRequestList = new List<TCP_message>();
        private Socket currentClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private ServerWindow serverWindow = null;
        private ServerDatabase db = new ServerDatabase(TCP_constants.DATABASE_FILE);

        public Server()
        {
            init();
        }

        private void init()
        {
            serverWindow = ServerWindow.getForm(this);
            InitialNetworkStatusCheck();

            serverWindow.Text = "Server           " +
                                "#IP Address: " + TCP_networking.GetIP() + "           " +
                                "#Port: " + TCP_constants.SERVER_PORT + "           " + 
                                "#Online since: " + GetServerUpTimeStart();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);
            ServerStart();
            serverWindow.ShowDialog();
        }

        public void ServerStart()
        {
            TCPListener = new TcpListener(IPAddress.Parse(TCP_networking.GetIP()), TCP_constants.SERVER_PORT);
            TCPListener.Start();

            connectListener = new Thread(ListenForConnectRequest);
            connectListener.Start();

            transmissionListener = new Thread(ListenForRequests);
            transmissionListener.Start();

            clientRequestExecutioner = new Thread(ExecuteClientRequests);
            clientRequestExecutioner.Start();
        }

        public void ServerStop()
        {
            clientRequestExecutioner.Abort();
            transmissionListener.Abort();
            connectListener.Abort();
            TCPListener.Stop();
        }

        private void ListenForConnectRequest()
        {
            serverWindow.AddServerLog("Waiting for connect requests...");

            while (true)
            {
                while (!TCPListener.Pending()) { }
                currentClientSocket = TCPListener.AcceptSocket();
                clientSocketList.Add(currentClientSocket);
                serverWindow.AddServerLog("New socket connection occurred: " + currentClientSocket.RemoteEndPoint);
            }
        }

        private void ListenForRequests()
        {
            int numOfBytesRead = 0;
            byte[] receiveBuffer = new byte[TCP_constants.BUFFER_SIZE];
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
                            AddClientRequest(msg);
                        }
                    }
                    catch(Exception) { /**No message received!**/}
                }
                else
                {
                    currentClientSocket.Close();
                }
            }
        }

        private void AddClientRequest(TCP_message msg)
        {
            serverWindow.AddServerLog("Received request from: " + msg.source);
            clientRequestList.Add(msg);
            string newRequestText = string.Format(" Type: {0} Client: {1} Destination: {2}", msg.type, msg.source, msg.destination);
            serverWindow.DisplayRequestInListbox(newRequestText);
        }

        public void InitialNetworkStatusCheck()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
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

        private string GetServerUpTimeStart()
        {
            return DateTime.Now.ToString("yyyy-MM-dd, HH.mm.ss");
        }

        private void ExecuteClientRequests()
        {
            while (true)
            {
                if (clientRequestList.Count != 0 || clientRequestList == null)
                {
                    HandleClientRequest(clientRequestList.ElementAt(0));
                    clientRequestList.RemoveAt(0);
                    serverWindow.RemoveNextRequestText();
                }
            }
        }

        private void HandleClientRequest(TCP_message msg)
        {
            switch (msg.type)
            {
                case TCP_constants.JOIN_REQUEST:

                    break;
                case TCP_constants.LOGIN_REQUEST:

                    break;
                case TCP_constants.LOGOUT_REQUEST:

                    break;
                case TCP_constants.GET_AVAILABLE_USERS_REQUEST:

                    break;
                case TCP_constants.FRIEND_REQUEST:

                    break;
                case TCP_constants.GET_FRIENDS_STATUS_REQUEST:

                    break;
                case TCP_constants.GET_CLIENT_DATA_ACCESS_REQUEST:

                    break;
                case TCP_constants.FORWARD_MESSAGE_REQUEST:

                    break;
                case TCP_constants.SERVER_MESSAGE:
                case TCP_constants.INVALID_REQUEST:

                    break;
            }
        }

        private void HandleAvailableUsersRequest()
        {
            throw new NotImplementedException();
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