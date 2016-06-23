
using System.Windows.Forms;

namespace ServerDBCommunication
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Server();
        }
    }
}
