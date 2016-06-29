﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using ServerDBCommunication;
using System.Linq;
using Shared_resources;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Async_TCP_server_networking
{
    public class Server
    {
        private Thread connect_listener = null;
        private Thread requestListener = null;
        private Thread requestResponder = null;

        Hashtable hashTblOfSocketById = new Hashtable();
        Hashtable socket_table_by_username = new Hashtable();

        private List<Thread> all_active_client_threads = new List<Thread>();

        private TcpListener TCPListener = null;

        private ServerWindow serverWindow = null;
        private ServerDatabase db = new ServerDatabase(TCP_constant.DATABASE_FILE);

        Serializer server_serializer = new Serializer();
        private List<Socket> all_active_client_sockets = new List<Socket>();

        public Server()
        {
            init();
        }

        private void init()
        {
            serverWindow = ServerWindow.getForm(this);
            DisplayInitialNetworkStatusInLog();

            serverWindow.Text = "Server           " +
                                "#IP Address: " + TCP_networking.GetIP() + "           " +
                                "#Port: " + TCP_constant.SERVER_PORT + "           " + 
                                "#Online since: " + GetServerUpTimeStart();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);
            ServerStart();
            serverWindow.ShowDialog();
        }

        public void ServerStart()
        {
            TCPListener = new TcpListener(IPAddress.Parse(TCP_networking.GetIP()), TCP_constant.SERVER_PORT);
            TCPListener.Start();

            connect_listener = new Thread(ListenForConnectRequest);
            connect_listener.Start();
        }

        public void ServerStop()
        {
            connect_listener.Abort();
            TCPListener.Stop();

            foreach(Thread curr in all_active_client_threads)
            {
                curr.Abort();
            }

            foreach (Socket s in all_active_client_sockets)
            {
                s.Close();
            }
        }

        public void Server_send(TCP_message msg, Socket s)
        {
            byte[] byteBuffer = server_serializer.Serialize_msg(msg);
            s.Send(byteBuffer);
        }

        public void DisplayInitialNetworkStatusInLog()
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

        private void ListenForConnectRequest()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverWindow.AddServerLog("Awaiting clients to establish connection with server...");

            while (true)
            {
                while (!TCPListener.Pending()) { }     
                s = TCPListener.AcceptSocket();
                all_active_client_sockets.Add(s);
                serverWindow.AddServerLog("New client has established connection with server.");
                ListenOnSocket(s);
            }
        }

        private void ListenOnSocket(Socket s)
        {
            Thread new_socket_listener = new Thread(ListenForRequests);
            new_socket_listener.Start(s);

            serverWindow.AddServerLog("Listening for requests on remote endpoint: "
                    + "PORT: " + IPAddress.Parse(((IPEndPoint)s.RemoteEndPoint).Port.ToString())
                    + " IP: " + IPAddress.Parse(((IPEndPoint)s.RemoteEndPoint).Address.ToString()));

            all_active_client_threads.Add(new_socket_listener);
        }

        private void ListenForRequests(object client_socket)
        {
            List<TCP_message> request_list = new List<TCP_message>();
            Socket s = (Socket)client_socket; 
            int numOfBytesRead = 0;
            byte[] receiveBuffer = new byte[TCP_constant.BUFFER_SIZE];
            
            while (true)
            {
                if (s.Connected)
                {
                    try
                    {
                        numOfBytesRead = s.Receive(receiveBuffer);

                        if (numOfBytesRead > 0)
                        {
                            TCP_message msg = server_serializer.Deserialize_msg(receiveBuffer);
                            serverWindow.AddServerLog("Request received from: " + msg.source);

                            request_list.Add(msg);

                            string request_as_text = string.Format("{0} From: '{1}' To: '{2}'", TCP_constant.GetRequestTypeAsText(msg.type), msg.source, msg.destination);
                            serverWindow.DisplayRequestInListbox(request_as_text);
                        }

                        if ((request_list != null || request_list.Count > 0) && (MessageWasMeantForServer(request_list.ElementAt(0))))
                        {
                            HandleClientRequest(request_list.ElementAt(0), s);
                            request_list.RemoveAt(0);
                        }
                    }
                    catch (Exception) { }
                }
                else
                    s.Close();
            }
        }

        private bool MessageWasMeantForServer(TCP_message msg)
        {
            if (msg.type != TCP_constant.INVALID_REQUEST || msg.type != TCP_constant.SERVER_REPLY)
                return true;
            else
                return false;
        }

        private void HandleClientRequest(TCP_message msg, Socket s)
        {
            switch(msg.type)
            {
                case TCP_constant.JOIN_REQUEST:

                    HandleJoinRequest(msg, s);

                    break;
                case TCP_constant.LOGIN_REQUEST:

                    //Username exists?
                    //IP address confirmed?
                    //Password/code correct?
                    //If ip was not confirmed, add it to confirmed IP addresses.
                    //Client online allready?
                    //Accept login.
                    //Acknowledge client.
                    //Broadcast event to all other users online.

                    break;
                case TCP_constant.LOGOUT_REQUEST:

                    //Acknowledge client.
                    //Close client socket.
                    //Broadcast event to all other users online.

                    break;
                case TCP_constant.GET_AVAILABLE_USERS_REQUEST:

                    //Send list of all usernames to client.

                    break;
                case TCP_constant.FRIEND_REQUEST:

                    //Forward request to the requested client.

                    break;
                case TCP_constant.FRIEND_REQUEST_REPLY:

                    //If reply is marked 'Accept friend request', mark users as friends in database.
                    //Forward request to the requested client.

                    break;
                case TCP_constant.GET_FRIENDS_STATUS_REQUEST:

                    //Send friend list to client, marked whether they are online or not.

                    break;
                case TCP_constant.GET_CLIENT_DATA_ACCESS_REQUEST:

                    //If requested data is marked 'Public', send data to client.
                    //Else if marked private, check database if clients are friends.
                        //If so, send requested data.
                        //Else deny client access.

                    break;
                case TCP_constant.FORWARD_MESSAGE_REQUEST:

                    //Check database if clients are friends.
                    //If so, forward message to the requested client.

                    break;
                case TCP_constant.SERVER_REPLY:
                case TCP_constant.INVALID_REQUEST:
                case TCP_constant.JOIN_REQUEST_REPLY:

                    //Ignore this message.

                    break;
            }
        }

        public void HandleJoinRequest(TCP_message msg, Socket s)
        {
            //Check username, password, and email. 
            //Notyfy client if correction is needed, or acknowledge client if all is good, and request client to log in again.

            bool validJoinRequest = true; 

            TCP_message reply = new TCP_message();
            reply.type = TCP_constant.JOIN_REQUEST_REPLY;
            reply.source = "SERVER";
            reply.destination = msg.source;

            string suggested_username = (msg.textAttributes.ElementAt(0));
            string suggested_password = (msg.textAttributes.ElementAt(1));
            string suggested_email = (msg.textAttributes.ElementAt(2));

            reply.AddTextAttribute(suggested_username);
            reply.AddTextAttribute(suggested_password);
            reply.AddTextAttribute(suggested_email);

            if (UsernameIsUnique(suggested_username))
                reply.AddBoolAttribute(true);
            else
            {
                validJoinRequest = false;
                reply.AddBoolAttribute(false);
            }

            if ( PasswordFormatValidation(suggested_password) )
                reply.AddBoolAttribute(true);
            else
            {
                validJoinRequest = false;
                reply.AddBoolAttribute(false);
            }

            if (EmailValidation(suggested_email))
                reply.AddBoolAttribute(true);
            else
            {
                validJoinRequest = false;
                reply.AddBoolAttribute(false);
            }

            //Add username, password and email to database.
            //Generate a confirmation code and save it in database, and send it to the user's email.
            if (validJoinRequest == true)
                db.AddNewUser(suggested_username, suggested_password, suggested_email);

            Server_send(reply, s);
        }

        private bool UsernameIsUnique(string suggested_username)
        {
            return true;
        }

        private bool PasswordFormatValidation(string suggested_password)
        {
            return true;
        }

        private bool EmailValidation(string suggested_email)
        {
            return true;
        }

    }
}