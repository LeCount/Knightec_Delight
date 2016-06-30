using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SharedResources;
using System.Linq;

namespace Async_TCP_client_networking
{
    public class Client
    {
        private Thread serverConnect = null;
        private Thread readMessages = null;

        private string server_ip_addr = "?";
        private string client_ip_addr = "?";
        private int numOfBytesRead = 0;
        private bool connected;

        private byte[] receiveBuffer = new byte[TCP_const.BUFFER_SIZE];
        private Stream clientStream = null;
        private TcpClient TCP_Client = new TcpClient();
        private ASCIIEncoding asciiEncode = new ASCIIEncoding();
        
        private LoginWindow loginWindow = null;
        private AddUserWindow addUserWindow = null;

        Serializer s = new Serializer();
        
        public Client()
        {
            init();
        }

        private void init()
        {
            loginWindow = LoginWindow.getForm(this);
            addUserWindow = AddUserWindow.getForm(this);

            client_ip_addr = TCP_networking.GetIP();
            server_ip_addr = client_ip_addr; //this needs to be changed if a different computer is used!

            loginWindow.DisplayClientIpAddress(client_ip_addr);
            loginWindow.DisplayServerAvailability("Server status: Unavailable ");

            InitialCheckOfNetworkStatus();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            serverConnect = new Thread(ConnectToServer);
            serverConnect.Start();

            readMessages = new Thread(Client_read);
            readMessages.Start();

            loginWindow.ShowDialog();
            addUserWindow.ShowDialog();
            addUserWindow.Visible = false;
        }

        private void ConnectToServer()
        {
            while (!connected)
            {
                try
                {
                    TCP_Client.Connect(IPAddress.Parse(server_ip_addr), TCP_const.SERVER_PORT);
                    clientStream = TCP_Client.GetStream();
                    loginWindow.DisplayServerAvailability("Server status: Available");
                    connected = true;
                }

                catch (Exception)
                {
                    loginWindow.DisplayServerAvailability("Server status: Unavailable ");
                }
            }
        }

        public void Client_read()
        {
            while (true)
            {
                try
                {
                    numOfBytesRead = clientStream.Read(receiveBuffer, 0, TCP_const.BUFFER_SIZE);
                }
                catch (Exception) { }
                if (numOfBytesRead > 0)
                {
                    TCP_message msg = s.Deserialize_msg(receiveBuffer);

                    if (msg.type == TCP_const.REPLY)
                        HandleServerReply(msg);

                    numOfBytesRead = 0;
                }
            }
        }

        private void HandleServerReply(TCP_message msg)
        {
            switch(msg.id)
            {
                case TCP_const.JOIN:
                    HandleJoinReply(msg);
                    break;
                case TCP_const.LOGIN:

                    break;
                case TCP_const.LOGOUT:

                    break;
                case TCP_const.GET_USERS:

                    break;
                case TCP_const.ADD_FRIEND:

                    break;
                case TCP_const.GET_FRIENDS_STATUS:

                    break;
                case TCP_const.GET_CLIENT_DATA:

                    break;
                case TCP_const.SEND_MESSAGE:

                    break;
                default: break;
            }
        }

        private void HandleJoinReply(TCP_message msg)
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

        public void SendJoinRequest(string username, string password, string mailaddress)
        {
            TCP_message msg_send = new TCP_message();
            msg_send.id = TCP_const.JOIN;
            msg_send.type = TCP_const.REQUEST;
            msg_send.source = TCP_networking.GetIP();
            msg_send.destination = "SERVER";

            msg_send.AddTextAttribute(username);
            msg_send.AddTextAttribute(password);
            msg_send.AddTextAttribute(mailaddress);

            Client_send(msg_send);
        }

        public void InitialCheckOfNetworkStatus()
        {
            if(NetworkInterface.GetIsNetworkAvailable())
                loginWindow.DisplayNetworkAvailability("Network status: Available");
            else
                loginWindow.DisplayNetworkAvailability("Network status: Unavailable");
        }

        public void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                loginWindow.DisplayNetworkAvailability("Network status: Available");
            else
                loginWindow.DisplayNetworkAvailability("Network status: Unavailable");
        }

        private string SetServerIP()
        {
            throw new NotImplementedException();
        }

        public void Client_send(TCP_message msg)
        {   
            byte[] byteBuffer = s.Serialize_msg(msg);
            try { clientStream.Write(byteBuffer, 0, byteBuffer.Length); }
            catch (Exception) { }
        }

        public void Disconnect()
        {
            //send request to disconnect to server
            ClientStop();
        }

        public void ClientStop()
        {
            serverConnect.Abort();
            readMessages.Abort();
        }

        public void ShowLoginWindow()
        {
            loginWindow.Show();
        }

        public void ShowAddUserWindow()
        {
            addUserWindow.ShowDialog();
        }

        public void ShowOnlineUserWindow()
        {
            throw new NotImplementedException();
        }
    }
}
