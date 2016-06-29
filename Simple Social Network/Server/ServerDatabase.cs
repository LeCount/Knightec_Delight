using System;
using System.Data.SQLite; //http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/

namespace ServerDBCommunication
{
    public class ServerDatabase
    {
        private SQLiteConnection DBconnection;
        //private SQLiteCommand query;

        public ServerDatabase() { }

        public ServerDatabase(string dbFile){Connect(dbFile);}

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

        internal void AddNewUser(string suggested_username, string suggested_password, string suggested_email)
        {
            return;
        }

        //public void AddUser(string uName, string pWord)
        //{
        //    query = new SQLiteCommand();
        //    query.Connection = DBconnection;
        //    query.CommandText = "INSERT INTO Login (Username, Password) VALUES (@Username, @Password)";

        //    query.CommandType = CommandType.Text;

        //    query.Parameters.AddWithValue("@Username", uName);
        //    query.Parameters.AddWithValue("@Password",pWord);

        //    query.ExecuteNonQuery();

        //    Disconnect();
        //}
    }
}
