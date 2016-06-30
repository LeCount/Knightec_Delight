using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;


namespace SharedResources
{
    public static class TCP_networking
    {
        static public string GetIP()
        {
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                    return IPA.ToString();
            }

            return "No ip address found";
        }
    }

}
