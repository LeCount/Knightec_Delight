using Shared_resources;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    public class Client
    {
        private Thread connectToServer = null;
        //private Thread readMessages = null;

        private string server_ip_addr = "?";
        private string client_ip_addr = "?";
        private int numOfBytesRead = 0;
        private bool connected;

        private byte[] receiveBuffer = new byte[TCP_constants.BUFFER_SIZE];
        private Stream clientStream = null;
        private TcpClient TCP_Client = new TcpClient();
        private ASCIIEncoding asciiEncode = new ASCIIEncoding();
        
        private LoginWindow loginWindow = null;
        private AddUserWindow addUserWindow = null;
        

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

            InitialCheckOfNetworkStatus();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            ClientStart();

            Thread.Sleep(1000);

            TCP_message msg = new TCP_message();
            msg.type = TCP_constants.LOGIN_REQUEST;
            msg.source = client_ip_addr;
            msg.destination = "SERVER";
            Client_send(msg);

            loginWindow.ShowDialog();
            addUserWindow.ShowDialog();
            addUserWindow.Visible = false;
        }

        private void ClientStart()
        {
            connectToServer = new Thread(TryConnectToServer);
            connectToServer.Start();

            //readMessages = new Thread(Client_read);
            //readMessages.Start();
        }

        public void ClientStop()
        {
            connectToServer.Abort();
            //readMessages.Abort();
        }

        private void TryConnectToServer()
        {
            while (connected == false)
            {
                try
                {
                    TCP_Client.Connect(IPAddress.Parse(server_ip_addr), TCP_constants.SERVER_PORT);
                    
                    clientStream = TCP_Client.GetStream();
                    connected = true;
                    loginWindow.ChangeServerAvailability("Server status: Available");
                }

                catch(Exception)
                {
                    loginWindow.ChangeServerAvailability("Server status: Unavailable ");
                }
            }
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

        private void Client_read()
        {
            Serializer s = new Serializer();

            while (true)
            {
                numOfBytesRead = clientStream.Read(receiveBuffer, 0, TCP_constants.BUFFER_SIZE);

                if (numOfBytesRead > 0)
                {
                    TCP_message msg = s.Deserialize_msg(receiveBuffer);
                    MessageBox.Show("Source: " + msg.source + "Type: " + msg.type);
                }
            }
        }

        public void Client_send(TCP_message msg)
        {
            Serializer s = new Serializer();
            
            byte[] byteBuffer = s.Serialize_msg(msg);
            clientStream.Write(byteBuffer, 0, byteBuffer.Length);
        }

        public void Disconnect()
        {
            //send request to disconnect to server
            ClientStop();
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
