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
            type = TCP_constants.INVALID_REQUEST;
            source = null;
            destination = null;
            textAttributes = new List<string>();
            boolAttributes = new List<bool>();
        }

        private List<string> GetTextAttributes()
        {
            return textAttributes;
        }

        private void AddTextAttribute(string str)
        {
            textAttributes.Add(str);
        }

        private List<bool> GetBoolAttributes()
        {
            return boolAttributes;
        }

        private void AddBoolAttribute(bool b)
        {
            boolAttributes.Add(b);
        }
    }
}
