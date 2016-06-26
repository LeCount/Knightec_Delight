using System.Collections.Generic;

namespace Shared_resources
{
    public class TCP_message
    {
        private string type { get; set; }
        private string source { get; set; }
        private string destination { get; set; }
        private List<string> msg_attributes = null;

        public TCP_message()
        {
            msg_attributes = new List<string>();
        }

        private List<string> Get_msg_attributes()
        {
            return msg_attributes;
        }

        private void Add_msg_attribute(string str)
        {
            msg_attributes.Add(str);
        }

    }

}
