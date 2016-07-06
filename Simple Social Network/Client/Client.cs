using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using SharedResources;
using System.Linq;

namespace Async_TCP_client_networking
{
    /// <summary>A class responsible for asynchronous TCP communication on the client side.</summary>
    public class Client
    {
        private LoginWindow login_window = null;
        private AddUserWindow add_user_window = null;
        private OnlineUserWindow online_user_window = null;
        private Serializer s = new Serializer();

        /// <summary>Thread responsible for connecting to the server.</summary>
        private Thread server_connect = null;

        /// <summary>Thread responsible for reading incoming messages on this client's socket.</summary>
        private Thread message_read = null;

        /// <summary>A stream providing read and write operations, on a given medium.</summary>
        private Stream client_stream = null;

        /// <summary>A medium for providing client connections for TCP network services.</summary>
        private TcpClient tcp_client = new TcpClient();

        /// <summary>A byte-array based buffer, where incoming messages are stored.</summary>
        private byte[] receive_buffer = new byte[TcpConst.BUFFER_SIZE];

        private string server_ip_addr = "?";
        private string client_ip_addr = "?";

        /// <summary>Indication of whether this client is to be assumed connected to the server or not.</summary>
        private bool connected;

        /// <summary>Default client constructor.</summary>
        public Client() {Init();}

        /// <summary> Initialize the client, and start necessary threads.</summary>
        private void Init()
        {
            login_window = LoginWindow.getForm(this);
            add_user_window = AddUserWindow.getForm(this);
            online_user_window = OnlineUserWindow.getForm(this);

            client_ip_addr = GetClientIP(); 
            server_ip_addr = client_ip_addr; //this needs to be changed if a different computer is used!

            login_window.SetClientIpAddress(client_ip_addr);
            login_window.SetServerAvailability("Server status: Unavailable ");

            SetInitialNetworkStatus();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            ClientStart();

            login_window.ShowDialog();
            add_user_window.ShowDialog();
            online_user_window.ShowDialog();

            add_user_window.Visible = false;
            online_user_window.Visible = false;
        }

        /// <summary>Try until success to connect to the server.</summary>
        private void ConnectToServer()
        {
            while (!connected)
            {
                try
                {
                    tcp_client.Connect(IPAddress.Parse(server_ip_addr), TcpConst.SERVER_PORT);
                    client_stream = tcp_client.GetStream();
                    login_window.SetServerAvailability("Server status: Available");
                    connected = true;
                }

                catch (Exception)
                {
                    login_window.SetServerAvailability("Server status: Unavailable ");
                }
            }
        }

        /// <summary>Read messages from the server</summary>
        public void ClientRead()
        {
            int numOfBytesRead = 0;

            while (true)
            {
                try
                {
                    numOfBytesRead = client_stream.Read(receive_buffer, 0, TcpConst.BUFFER_SIZE);
                }
                catch (Exception) { }

                if (numOfBytesRead > 0)
                {
                    TcpMessage msg = s.DeserializeByteArray(receive_buffer);

                    if (msg.type == TcpConst.REPLY)
                        HandleServerReplies(msg);

                    numOfBytesRead = 0;
                }
            }
        }

        /// <summary>Depending on the reply that was received, handle it accordingly. </summary>
        /// <param name="msg">Received message.</param>
        private void HandleServerReplies(TcpMessage msg)
        {
            switch(msg.id)
            {
                case TcpConst.JOIN:
                    HandleJoinReply(msg);
                    break;
                case TcpConst.LOGIN:
                    HandleLoginReply(msg);
                    break;
                case TcpConst.LOGOUT:

                    break;
                case TcpConst.GET_USERS:

                    break;
                case TcpConst.ADD_FRIEND:

                    break;
                case TcpConst.GET_FRIENDS_STATUS:

                    break;
                case TcpConst.GET_CLIENT_DATA:

                    break;
                case TcpConst.SEND_MESSAGE:

                    break;
                default: break;
            }
        }

        private void HandleJoinReply(TcpMessage msg)
        {
            bool username = msg.GetBoolAttributes().ElementAt(0);
            bool password = msg.GetBoolAttributes().ElementAt(1);
            bool mail = msg.GetBoolAttributes().ElementAt(2);

            string caption = null;
            string info = null;
            MessageBoxIcon i= MessageBoxIcon.Error;

            if (!username)
            {
                caption = "Your account was disapproved.";
                info = "The suggested username allready exist.\n" + 
                       "Please enter a new one.";
            }
            else if (!password)
            {
                caption = "Your account was disapproved.";
                info = "The suggested password has incorrect format or is not concidered safe.\n" +
                       "Please enter a new valid password, with the following criteria:\n" +
                       "(1) Minimum number of letters: 5\n" +
                       "(2) Minimum number of  numerals: 3\n" +
                       "(3) Minimal number of capital letters: 1";
            }
            else if (!mail)
            {
                caption = "Your account was disapproved.";
                info = "You have not entered a valid email.\n" +
                       "Please enter a new one.";
            }
            else
            {
                caption = "Your account has been accepted!";
                info = "Please login and enter the confirmation-code, that has been sent to your email.";
                i = MessageBoxIcon.Asterisk;
            }

            MessageBox.Show(info, caption, MessageBoxButtons.OK, i);
        }

        private void HandleLoginReply(TcpMessage msg)
        {
            bool username = msg.GetBoolAttributes().ElementAt(0);
            bool password = msg.GetBoolAttributes().ElementAt(1);
            bool code = msg.GetBoolAttributes().ElementAt(2);

            username = true;
            password = true;
            code = true;

            if (!username && !password && !code)
            {

            }
            else
            {
                online_user_window.Visible = true;
                login_window.Visible = false;
            }
        }

        public void SendJoinRequest(string username, string password, string mailaddress)
        {
            Client_send(new TcpMessage().CreateJoinRequest(username, password, mailaddress));
        }

        internal void SendLoginRequest(string username, string password, string code)
        {
            Client_send(new TcpMessage().CreateLoginRequest(username, password, code));
        }

        /// <summary>Checks the current status of the network and then sets the network status text accordingly.</summary>
        public void SetInitialNetworkStatus()
        {
            if(NetworkInterface.GetIsNetworkAvailable())
                login_window.DisplayNetworkAvailability("Network status: Available");
            else
                login_window.DisplayNetworkAvailability("Network status: Unavailable");
        }

        /// <summary>When Network availability changes, this method is invoked.</summary>
        public void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                login_window.DisplayNetworkAvailability("Network status: Available");
            else
                login_window.DisplayNetworkAvailability("Network status: Unavailable");
        }

        /// <summary>Send message from client to server over TCP.</summary>
        /// <param name="msg">Message to be sent over TCP.</param>
        public void Client_send(TcpMessage msg)
        {   
            byte[] byteBuffer = s.SerializeMsg(msg);
            try { client_stream.Write(byteBuffer, 0, byteBuffer.Length); }
            catch (Exception) { }
        }

        public void Disconnect()
        {
            //send request to disconnect to server
            ClientStop();
        }

        public void ClientStop()
        {
            server_connect.Abort();
            message_read.Abort();
        }

        public void ClientStart()
        {
            server_connect = new Thread(ConnectToServer);
            server_connect.Start();

            message_read = new Thread(ClientRead);
            message_read.Start();
        }

        public void ShowLoginWindow()
        {
            login_window.Show();
        }

        public void ShowAddUserWindow()
        {
            add_user_window.ShowDialog();
        }

        public void ShowOnlineUserWindow()
        {
            online_user_window.Show();
        }

        /// <summary>Get the IP address of this machine.</summary>
        /// <returns></returns>
        public string GetClientIP() { return TcpNetworking.GetIP(); }
    }
}
