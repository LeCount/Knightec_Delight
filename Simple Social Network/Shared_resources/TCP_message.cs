using System;
using System.Collections.Generic;

namespace Shared_resources
{
    [Serializable]
    public class TCP_message
    {
        public int type { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public List<string> textAttributes;
        public List<bool> boolAttributes;

        public TCP_message()
        {
            type = TCP_constant.INVALID_REQUEST;
            source = null;
            destination = null;
            textAttributes = new List<string>();
            boolAttributes = new List<bool>();
        }

        public List<string> GetTextAttributes()
        {
            return textAttributes;
        }

        public void AddTextAttribute(string str)
        {
            textAttributes.Add(str);
        }

        public List<bool> GetBoolAttributes()
        {
            return boolAttributes;
        }

        public void AddBoolAttribute(bool b)
        {
            boolAttributes.Add(b);
        }
    }
}
