namespace Shared_resources
{
    public static class TCP_constant
    {
        public const int INVALID_REQUEST =                  0;
        public const int JOIN_REQUEST =                     1;
        public const int JOIN_REQUEST_REPLY =               2;
        public const int LOGIN_REQUEST =                    3;
        public const int LOGOUT_REQUEST =                   4;
        public const int GET_AVAILABLE_USERS_REQUEST =      5;
        public const int FRIEND_REQUEST =                   6;
        public const int FRIEND_REQUEST_REPLY =             7;
        public const int GET_FRIENDS_STATUS_REQUEST=        8;
        public const int GET_CLIENT_DATA_ACCESS_REQUEST =   9;
        public const int FORWARD_MESSAGE_REQUEST =          10;
        public const int SERVER_REPLY =                     11;


        public const int SERVER_PORT =                      8001;
        public const int BUFFER_SIZE =                      1024;
        public const string DATABASE_FILE =                 "serverDB.db";


        public static string GetRequestTypeAsText(int i)
        {
            switch(i)
            {
                case 1: return "JOIN REQUEST";
                case 2: return "JOIN REQUEST REPLY";
                case 3: return "LOGIN REQUEST";
                case 4: return "LOGOUT REQUEST";
                case 5: return "GET AVAILABLE USERS REQUEST";
                case 6: return "FRIEND REQUEST";
                case 7: return "FRIEND REQUEST REPLY";
                case 8: return "GET FRIENDS STATUS REQUEST";
                case 9: return "GET CLIENT DATA ACCESS REQUEST";
                case 10: return "FORWARD MESSAGE REQUEST";
                case 11: return "SERVER REPLY";
            }

            return "INVALID REQUEST";
        }
    }
}
