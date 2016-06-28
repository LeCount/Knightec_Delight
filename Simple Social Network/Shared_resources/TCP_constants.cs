namespace Shared_resources
{
    public static class TCP_constants
    {
        public const int INVALID_REQUEST =                  0;
        public const int JOIN_REQUEST =                     1;
        public const int LOGIN_REQUEST =                    2;
        public const int LOGOUT_REQUEST =                   3;
        public const int GET_AVAILABLE_USERS_REQUEST =      4;
        public const int FRIEND_REQUEST =                   5;
        public const int GET_FRIENDS_STATUS_REQUEST=        6;
        public const int GET_CLIENT_DATA_ACCESS_REQUEST =   7;
        public const int FORWARD_MESSAGE_REQUEST =          8;
        public const int SERVER_MESSAGE =                   9;

        public const int SERVER_PORT =                      8001;
        public const int BUFFER_SIZE =                      1024;
        public const string DATABASE_FILE =                 "serverDB.db";
    }
}
