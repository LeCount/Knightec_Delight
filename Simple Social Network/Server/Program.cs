
using System.Windows.Forms;

namespace Async_TCP_server_networking
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
