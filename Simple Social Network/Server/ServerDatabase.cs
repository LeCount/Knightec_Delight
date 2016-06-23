using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite; //http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
using System.Data.SqlClient;

namespace ServerDBCommunication
{
    public class ServerDatabase
    {
        private SqlConnection DBconnection;
        private SQLiteCommand sql_command;
        private SqlCommand query;

        public ServerDatabase() { }

        public ServerDatabase(string dbFile)
        {
            Connect(dbFile);
        }

        private void Connect(string dbFile)
        {

            DBconnection = new SqlConnection(dbFile);
            DBconnection.Open();
        }

        public void Disconnect()
        {
            DBconnection.Close();
        }

        public void AddUser(string uName, string pWord)
        {
            query = new SqlCommand();
            query.Connection = DBconnection;
            query.CommandText = "INSERT INTO Login (Username, Password) VALUES (@Username, @Password)";

            query.Parameters.Add(new SqlParameter("@Username", System.Data.SqlDbType.VarChar).Value = uName);
            query.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.VarChar).Value = pWord);
            
            query.ExecuteNonQuery();

            Disconnect();
        }
    }
}
