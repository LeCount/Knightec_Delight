using System.Collections.Generic;

namespace SharedResources
{
    public class TCP_message
    {
        private string type { get; set; }
        private string sender { get; set; }
        private string receiver { get; set; }
        private List<string> attributes = null;

        public TCP_message()
        {
            attributes = new List<string>();
        }

        private void addAttribute(string str)
        {
            attributes.Add(str);
        }

        private List<string> GetAttributes()
        {
            return attributes;
        }
    }
}
