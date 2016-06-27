using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shared_resources
{
    [Serializable]
    public class TCP_message
    {
        public string type { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public List<string> attributes = new List<string>();

        public TCP_message(){}

        private List<string> Get_msg_attributes()
        {
            return attributes;
        }

        private void Add_msg_attribute(string str)
        {
            attributes.Add(str);
        }
    }
}
