namespace Shared_resources
{
    public static class TCP_const
    {  
        public const int JOIN =                     1;
        public const int LOGIN =                    2;
        public const int LOGOUT =                   3;
        public const int GET_USERS =                4;
        public const int ADD_FRIEND =               5;
        public const int GET_FRIENDS_STATUS =      6;
        public const int GET_CLIENT_DATA =          7;
        public const int SEND_MESSAGE =             8;

        public const int REQUEST =                  9;
        public const int REPLY =                    10;
        public const int INVALID =                  0;

        public const int SERVER_PORT =              8001;
        public const string DATABASE_FILE =         "serverDB.db";
        public const int BUFFER_SIZE =              1024;


        public static string IntToText(int i)
        {
            switch (i)
            {
                case 1:  return "JOIN";
                case 2:  return "LOGIN";
                case 3:  return "LOGOUT";
                case 4:  return "GET USERS";
                case 5:  return "ADD FRIEND";
                case 6:  return "GET FRIENDS STATUS";
                case 7:  return "GET CLIENT DATA";
                case 8:  return "SEND MESSAGE";
                case 9:  return "SERVER REPLY";
                case 10: return "REQUEST";
                case 11: return "REPLY";
            }

            return "INVALID";
        }

        public static int TextToInt(string s)
        {
            switch (s)
            {
                case "JOIN": return 1;
                case "LOGIN": return 2;
                case "LOGOUT": return 3;
                case "GET USERS": return 4;
                case "ADD FRIEND": return 5;
                case "GET FRIENDS STATUS": return 6;
                case "GET CLIENT DATA": return 7;
                case "SEND MESSAGE": return 8;
                case "SERVER REPLY": return 9;
                case "REQUEST": return 10;
                case "REPLY": return 11;
            }

            return 0;
        }
    }
}
