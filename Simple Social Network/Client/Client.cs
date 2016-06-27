using Shared_resources;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    public class Client
    {
        private Thread connectToServer = null;
        private Thread readMessages = null;

        private const int SERVER_PORT_NUMBER = 8001;
        private const int BUFFER_SIZE = 1024;

        private String SERVER_IP_ADDR = "?";
        private String CLIENT_IP_ADDR = "?";
        private byte[] receiveBuffer = new byte[BUFFER_SIZE];
        private Stream clientStream = null;
        private TcpClient TCP_Client = new TcpClient();
        private ASCIIEncoding asciiEncode = new ASCIIEncoding();
        private int numOfBytesRead = 0;
        private bool connected;
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

            CLIENT_IP_ADDR = GetClientIP();
            SERVER_IP_ADDR = CLIENT_IP_ADDR; //this needs to be changed if a different computer is used!

            loginWindow.DisplayClientIpAddress(CLIENT_IP_ADDR);

            InitialCheckOfNetworkStatus();

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            ClientStart();

            Thread.Sleep(1000);

            TCP_message msg = new TCP_message();
            msg.type = "CONNECT_REQUEST";
            msg.source = CLIENT_IP_ADDR;
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
                    TCP_Client.Connect(IPAddress.Parse(SERVER_IP_ADDR), SERVER_PORT_NUMBER);
                    
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
            if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
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

        public string GetClientIP()
        {
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                    return IPA.ToString();
            }

            return "No ip address found";
        }

        private void Client_read()
        {
            Serializer s = new Serializer();
            while (true)
            {
                numOfBytesRead = clientStream.Read(receiveBuffer, 0, BUFFER_SIZE);

                if (numOfBytesRead > 0)
                {
                    TCP_message msg = s.Deserialize_msg(receiveBuffer);

                    MessageBox.Show("Source: " + msg.source + "Type: " + msg.type);

                    //for (byteCount = 0; byteCount < numOfBytesRead; byteCount++)
                    //{
                    //    msgFromServer = msgFromServer + Convert.ToChar(receiveBuffer[byteCount]);
                    //}

                    //if (!(msgFromServer == null || msgFromServer == ""))
                    //{
                    //    msgFromServer = "Incoming message: " + '"' + msgFromServer + '"';
                    //}

                    //TODO: Display message in window
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
