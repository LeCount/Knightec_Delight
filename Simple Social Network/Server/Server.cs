using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using ServerDBCommunication;
using System.Linq;
using SharedResources;
using System.Data;
using System.Net.Mail;

namespace Async_TCP_server_networking
{
    /// <summary> A class responsible for asynchronous TCP communication on the server side. </summary>
    public class Server
    {
        /// <summary>A thread meant to listen client connect requests </summary>
        private Thread connect_listener = null;
        private TcpListener tcp_client_listener = null;
        private List<Thread> all_active_client_threads = new List<Thread>();
        private List<Socket> all_active_client_sockets = new List<Socket>();
        private ServerWindow serverWindow = null;
        private SQLiteServerDatabase db = new SQLiteServerDatabase(TcpConst.DATABASE_FILE);
        private Serializer server_serializer = new Serializer();

        public Server(){ Init();}

        /// <summary> Initialize the server, and start necessary threads. </summary>
        private void Init()
        {
            serverWindow = ServerWindow.getForm(this);

            SetInitialNetworkStatusInLog();

            serverWindow.Text = "Server           " +
                                "#IP Address: " + TcpNetworking.GetIP() + "           " +
                                "#Port: " + TcpConst.SERVER_PORT + "           " + 
                                "#Online since: " + GetServerUpTimeStart();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);
            ServerStart();
            serverWindow.ShowDialog();
        }

        /// <summary>Start listen for connect request and messages from clients </summary>
        public void ServerStart()
        {
            tcp_client_listener = new TcpListener(IPAddress.Parse(TcpNetworking.GetIP()), TcpConst.SERVER_PORT);
            tcp_client_listener.Start();

            connect_listener = new Thread(ListenForConnectRequest);
            connect_listener.Start();
        }

        /// <summary>Stop all listeners, stop all threads and close all active sockets</summary>
        public void ServerStop()
        {
            connect_listener.Abort();
            tcp_client_listener.Stop();

            foreach(Thread curr in all_active_client_threads)
            {
                curr.Abort();
            }

            foreach (Socket s in all_active_client_sockets)
            {
                s.Close();
            }
        }

        /// <summary>Send message to specific socket.</summary>
        /// <param name="msg">Message to send.</param>
        /// <param name="s">Socket to send msg to.</param>
        public void ServerSend(TcpMessage msg, Socket s)
        {
            byte[] byte_buffer = server_serializer.SerializeMsg(msg);
            s.Send(byte_buffer);
        }

        /// <summary>Checks the current status of the network and then sets the network status text accordingly.</summary>
        public void SetInitialNetworkStatusInLog()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
                serverWindow.AddServerLog("Network available");
            else
                serverWindow.AddServerLog("Network unavailable");
        }

        /// <summary>This method is invoked when the network status is changed.</summary>
        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                serverWindow.AddServerLog("Network available");
            else
                serverWindow.AddServerLog("Network unavailable");
        }

        /// <summary>Get date and time for when server started.</summary>
        private string GetServerUpTimeStart()
        {
            return DateTime.Now.ToString("yyyy-MM-dd, HH.mm.ss");
        }

        /// <summary>Wait for clients to connect. When connected, give clients a thread for the server to listen on and then add clients to the socket list.</summary>
        private void ListenForConnectRequest()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverWindow.AddServerLog("Awaiting clients to establish connection with server...");

            while (true)
            {
                while (!tcp_client_listener.Pending()) { }     
                s = tcp_client_listener.AcceptSocket();
                all_active_client_sockets.Add(s);
                serverWindow.AddServerLog("New client has established connection with server.");
                ListenOnSocket(s);
            }
        }

        /// <summary>This method is executed in it's own thread, where it listens for coms. on the addressed socket.</summary>
        /// <param name="s">Socket to listen on</param>
        private void ListenOnSocket(Socket s)
        {
            Thread new_socket_listener = new Thread(ListenForRequests);
            new_socket_listener.Start(s);

            serverWindow.AddServerLog("Listening for requests on remote endpoint: "
                    + "PORT: " + IPAddress.Parse(((IPEndPoint)s.RemoteEndPoint).Port.ToString())
                    + " IP: " + IPAddress.Parse(((IPEndPoint)s.RemoteEndPoint).Address.ToString()));

            all_active_client_threads.Add(new_socket_listener);
        }

        /// <summary>Listens for requests on the addressed socket.</summary>
        /// <param name="s">Socket to listen on</param>
        private void ListenForRequests(object client_socket)
        {
            List<TcpMessage> request_list = new List<TcpMessage>();
            Socket s = (Socket)client_socket; 
            int num_of_bytes_read = 0;
            byte[] receive_buffer = new byte[TcpConst.BUFFER_SIZE];
            
            while (true)
            {
                if (s.Connected)
                {
                    try
                    {
                        num_of_bytes_read = s.Receive(receive_buffer);
                    }
                    catch (Exception) { }

                    if (num_of_bytes_read > 0)
                    {
                        TcpMessage msg = server_serializer.DeserializeByteArray(receive_buffer);
                        serverWindow.AddServerLog("Request received from: " + msg.source);

                        request_list.Add(msg);

                        string request_as_text = string.Format("{0} REQUEST from: '{1}' to: '{2}'", TcpConst.IntToText(msg.id), msg.source, msg.destination);
                        serverWindow.AddServerRequest(request_as_text);
                    }

                    if (request_list != null && request_list.Count >= 1 && MessageWasMeantForServer( request_list.ElementAt(0) ) ) 
                    {
                        HandleClientRequest(request_list.ElementAt(0), s);
                        request_list.RemoveAt(0);
                    }

                    num_of_bytes_read = 0;
                }
                else
                    s.Close();
            }
        }

        /// <summary>Check if message was a request and that the message was not invalid.</summary>
        /// <param name="msg">Regarding message</param>
        /// <returns>True, if msg was meant for server.</returns>
        private bool MessageWasMeantForServer(TcpMessage msg)
        {
            if (msg.type == TcpConst.REQUEST && msg.id != TcpConst.INVALID)
                return true;
            else
                return false;
        }

        /// <summary>Depending on the message's type, handle it accordingly.</summary>
        /// <param name="msg">Regarding message</param>
        /// <param name="s">Regarding socket</param>
        private void HandleClientRequest(TcpMessage msg, Socket s)
        {
            switch(msg.id)
            {
                case TcpConst.JOIN:

                    HandleJoinRequest(msg, s);

                    break;
                case TcpConst.LOGIN:

                    //Username exists?
                    //IP address confirmed?
                    //Password/code correct?
                    //If ip was not confirmed, add it to confirmed IP addresses.
                    //Client online allready?
                    //Accept login.
                    //Acknowledge client.
                    //Broadcast event to all other users online.

                    break;
                case TcpConst.LOGOUT:

                    //Acknowledge client.
                    //Close client socket.
                    //Broadcast event to all other users online.

                    break;
                case TcpConst.GET_USERS:

                    //Send list of all usernames to client.

                    break;
                case TcpConst.ADD_FRIEND:

                    //Forward request to the requested client.

                    break;
                case TcpConst.GET_FRIENDS_STATUS:

                    //Send friend list to client, marked whether they are online or not.

                    break;
                case TcpConst.GET_CLIENT_DATA:

                    //If requested data is marked 'Public', send data to client.
                    //Else if marked private, check database if clients are friends.
                        //If so, send requested data.
                        //Else deny client access.

                    break;
                case TcpConst.SEND_MESSAGE:

                    //Check database if clients are friends.
                    //If so, forward message to the requested client.

                    break;
                case TcpConst.INVALID:

                    //Ignore this message.

                    break;
            }
        }

        public void HandleJoinRequest(TcpMessage msg, Socket s)
        {
            //Check username, password, and email. 
            //Notyfy client if correction is needed, or acknowledge client if all is good, and request client to log in again.

            bool validJoinRequest = true; 

            TcpMessage reply = new TcpMessage();
            reply.id = TcpConst.JOIN;
            reply.type = TcpConst.REPLY;
            reply.source = "SERVER";
            reply.destination = msg.source;

            string suggested_username = (msg.text_attributes.ElementAt(0));
            string suggested_password = (msg.text_attributes.ElementAt(1));
            string suggested_email =    (msg.text_attributes.ElementAt(2));

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

            if ( PasswordFormatIsValid(suggested_password) )
                reply.AddBoolAttribute(true);
            else
            {
                validJoinRequest = false;
                reply.AddBoolAttribute(false);
            }

            if (EmailIsValid(suggested_email))
                reply.AddBoolAttribute(true);
            else
            {
                validJoinRequest = false;
                reply.AddBoolAttribute(false);
            }

            //Add username, password and email to database.
            //Generate a confirmation code and save it in database, and send it to the user's email.
            if (validJoinRequest == true)
            {
                string code = null;
                code = GenerateConfirmationCode();
                SendEmailTo(suggested_email, "Login and give this code: " + code, "Account verification");
                db.AddNewUser(suggested_username, suggested_password, suggested_email, code);
                
            }

            ServerSend(reply, s);
        }

        /// <summary>Sends an email from my google account to the specified mailaddress.</summary>
        /// <param name="suggested_email"></param>
        /// <param name="text"></param>
        /// /// <param name="subject"></param>
        private void SendEmailTo(string suggested_email, string text, string subject)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("server.test@gmail.com", "TORSK");
            msg.To.Add("stefan.danielsson.1989@gmail.com");
            msg.Subject = "Email verfication";
            msg.Body = text;
            //msg.Priority = MailPriority.High;


            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("stefan.danielsson.1989@gmail.com", "klantarselE89");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
        }

        /// <summary>Generate a confirmation code, to be used to verify account and validate IP addresses.</summary>
        /// <returns></returns>
        private string GenerateConfirmationCode()
        {
            Random random = new Random();
            int length = 10;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghikolmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>Check if suggested username is unique.</summary>
        /// <param name="suggested_username"></param>
        /// <returns></returns>
        private bool UsernameIsUnique(string suggested_username)
        {
            return db.EntryExistsInTable(suggested_username, "Clients", "username");
            
        }

        /// <summary>Check password format.</summary>
        /// <param name="suggested_password"></param>
        /// <returns></returns>
        private bool PasswordFormatIsValid(string suggested_password)
        {
            int MIN_LENGTH = 5;
            int MAX_LENGTH = 20;

            if (suggested_password == null)
                return false;

            bool meetsLengthRequirements = suggested_password.Length >= MIN_LENGTH && suggested_password.Length <= MAX_LENGTH;

            if (!meetsLengthRequirements)
                return false;

            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            int digitCounter = 0;

            foreach (char c in suggested_password)
            {
                if (char.IsUpper(c)) hasUpperCaseLetter = true;
                else if (char.IsLower(c)) hasLowerCaseLetter = true;
                else if (char.IsDigit(c)) digitCounter ++;
            }

            if (hasUpperCaseLetter && hasLowerCaseLetter && digitCounter == 3)
                return true;
            else
                return false;
        }

        /// <summary>Check mail address format.</summary>
        /// <param name="suggested_password"></param>
        /// <returns></returns>
        private bool EmailIsValid(string suggested_email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(suggested_email);
                return addr.Address == suggested_email;
            }
            catch
            {
                return false;
            }
        }
    }
}