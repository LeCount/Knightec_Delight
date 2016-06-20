using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClientTcpCommunication
{
    public class Client
    {
        /**something to consider: try connect timeout: http://www.splinter.com.au/opening-a-tcp-connection-in-c-with-a-custom-t/ **/

        private const int SERVER_PORT_NUMBER = 8001;
        private String SERVER_IP_ADDR = "?";
        private String CLIENT_IP_ADDR = "?";

        private LoginWindow loginWindow = null;
   
        private Thread read_thread = null;
        private byte[] charArrSend = null;
        private byte[] charArrReceive = new byte[100];
        private Stream clientStream = null;
        
        private string msg = "";
        private TcpClient TCP_Client = new TcpClient();
        private ASCIIEncoding asciiEncode = new ASCIIEncoding();
        private int numOfBytesRead = 0;
        private int byteCount = 0;
        private bool connected;

        public Client()
        {
            init();
        }

        private void init()
        {
            Application.Run(LoginWindow.getForm(this));

            //SERVER_IP_ADDR = setServerIP();
            CLIENT_IP_ADDR = getClientIP();
            Thread serverConnect = new Thread(new ThreadStart (tryConnectToServer));
        }

        private void tryConnectToServer()
        {
            while (connected == false)
            {
                try
                {
                    TCP_Client.Connect(SERVER_IP_ADDR, SERVER_PORT_NUMBER);
                    clientStream = TCP_Client.GetStream();
                    connected = true;
                    read_thread = new Thread(new ThreadStart(client_read));
                    loginWindow.updateServerAvailability("Server status: Available");
                }
                catch (Exception e)
                {
                    loginWindow.updateServerAvailability("Server status: Unavailable " + e.Message);
                }
            }

            
        }

        public bool checkInitialNetworkStatus()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public void monitorNetworkAvailability(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                loginWindow.updateNetworkAvailability("Network status: Available");
            else
                loginWindow.updateNetworkAvailability("Network status: Unavailable");
        }

        private string setServerIP()
        {
            throw new NotImplementedException();
        }

        public string getClientIP()
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

        private void client_read()
        {
            numOfBytesRead = clientStream.Read(charArrReceive, 0, 100);

            for (byteCount = 0; byteCount < numOfBytesRead; byteCount++)
            {
                msg = msg + Convert.ToChar(charArrReceive[byteCount]);
            }

            if (!(msg == null || msg == ""))
            {
                msg = "Incoming message: " + '"' + msg + '"';
            }
        }

        public void client_write(String stringToSend)
        {
            charArrSend = asciiEncode.GetBytes(stringToSend);
            clientStream.Write(charArrSend, 0, charArrSend.Length);
        }

        public void disconnect()
        {
            read_thread.Abort();
            TCP_Client.Close();
        }
    }
}
