﻿using System.Net;
using System.Net.Sockets;

namespace SharedResources
{
    /// <summary>A class meant to distribute TCP related methods used by both client and server.</summary>
    public static class TcpNetworking
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

    /// <summary>A class meant to distribute TCP related constants used by both client and server.</summary>
    public static class TcpConst
    {
        //Message identifiers
        public const int JOIN = 1;
        public const int LOGIN = 2;
        public const int LOGOUT = 3;
        public const int GET_USERS = 4;
        public const int ADD_FRIEND = 5;
        public const int GET_FRIENDS_STATUS = 6;
        public const int GET_CLIENT_DATA = 7;
        public const int SEND_MESSAGE = 8;

        //message types:
        public const int REQUEST = 9;
        public const int REPLY = 10;

        public const int INVALID = 0;

        public const int SERVER_PORT = 8001;
        public const string DATABASE_FILE = "serverDB.db";
        public const int BUFFER_SIZE = 1024 * 4;

        /// <summary>Convert an integer constant to its corresponding text.</summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string IntToText(int i)
        {
            switch (i)
            {
                case 1: return "JOIN";
                case 2: return "LOGIN";
                case 3: return "LOGOUT";
                case 4: return "GET USERS";
                case 5: return "ADD FRIEND";
                case 6: return "GET FRIENDS STATUS";
                case 7: return "GET CLIENT DATA";
                case 8: return "SEND MESSAGE";
                case 9: return "REQUEST";
                case 10: return "REPLY";
                default: return "INVALID";
            }
        }
    }

}
