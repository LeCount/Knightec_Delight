using System.Windows.Forms;

namespace Async_TCP_client_networking
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Client(); 
        }
    }
}
