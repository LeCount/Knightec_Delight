using System;
using System.Data;
using System.Data.SQLite;

namespace ServerDBCommunication
{
    public class ServerDatabase
    {
        private SQLiteConnection DBconnection = null;
        private SQLiteCommand query = null;

        public ServerDatabase() {}

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

        internal void AddNewUser(string suggested_username, string suggested_password, string suggested_email, string code)
        {
            return;
        }

        public bool EntryExistsInTable(string entry, string table, string column)
        {

            query = new SQLiteCommand();
            query.Connection = DBconnection;

            SQLiteParameter param = new SQLiteParameter("@ENTRY", DbType.String) { Value = entry };

            query.Parameters.Add(param);

            query.CommandText = "SELECT COUNT (" + column.ToString() + ") FROM " + table.ToString() + " WHERE " + column.ToString() + " = @ENTRY";

            string text = query.CommandText;

            object obj = query.ExecuteScalar();
            int occurrences = Convert.ToInt32(obj);

            if (occurrences > 0)
                return false;
            else
                return true;
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
