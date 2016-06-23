using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite; //http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
using System.Data.SqlClient;
using System.Data;

namespace ServerDBCommunication
{
    public class ServerDatabase
    {
        private SQLiteConnection DBconnection;
        private SQLiteCommand sql_command;
        private SQLiteCommand query;

        public ServerDatabase() { }

        public ServerDatabase(string dbFile)
        {
            Connect(dbFile);
        }

        private void Connect(string dbFile)
        {
            try
            {
                DBconnection = new SQLiteConnection("Data Source = serverDB.db;Version=3;");
                DBconnection.Open();
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            
        }

        public void Disconnect()
        {
            DBconnection.Close();
        }

        public void AddUser(string uName, string pWord)
        {
            query = new SQLiteCommand();
            query.Connection = DBconnection;
            query.CommandText = "INSERT INTO Login (Username, Password) VALUES (@Username, @Password)";

            query.CommandType = CommandType.Text;

            query.Parameters.AddWithValue("@Username", uName);
            query.Parameters.AddWithValue("@Password",pWord);
            
            query.ExecuteNonQuery();

            Disconnect();
        }
    }
}
