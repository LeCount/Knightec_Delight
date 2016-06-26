using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Async_TCP_client_networking
{
    public class Client
    {
        private Thread connectToServer = null;
        private Thread read_thread = null;

        private const int SERVER_PORT_NUMBER = 8001;
        private const int BUFFER_SIZE = 1024;

        private String SERVER_IP_ADDR = "?";
        private String CLIENT_IP_ADDR = "?";
        private byte[] transmittBuffer = null;
        private byte[] receiveBuffer = new byte[100];
        private Stream clientStream = null;
        private string msgFromServer = "";
        private TcpClient TCP_Client = new TcpClient();
        private ASCIIEncoding asciiEncode = new ASCIIEncoding();
        private int numOfBytesRead = 0;
        private int byteCount = 0;
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

            loginWindow.ShowDialog();
            addUserWindow.ShowDialog();
            addUserWindow.Visible = false;
        }

        private void ClientStart()
        {
            connectToServer = new Thread(TryConnectToServer);
            connectToServer.Start();
        }

        public void ClientStop()
        {
            connectToServer.Abort();
            read_thread.Abort();
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
                    read_thread = new Thread(Client_read);
                    read_thread.Start();
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
            numOfBytesRead = clientStream.Read(receiveBuffer, 0, BUFFER_SIZE);

            for (byteCount = 0; byteCount < numOfBytesRead; byteCount++)
            {
                msgFromServer = msgFromServer + Convert.ToChar(receiveBuffer[byteCount]);
            }

            if (!(msgFromServer == null || msgFromServer == ""))
            {
                msgFromServer = "Incoming message: " + '"' + msgFromServer + '"';
            }

            //TODO: Display message in window
        }

        public void Client_write(String stringToSend)
        {
            transmittBuffer = asciiEncode.GetBytes(stringToSend);
            clientStream.Write(transmittBuffer, 0, transmittBuffer.Length);
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
