using System;
using System.Collections.Generic;

namespace Shared_resources
{
    [Serializable]
    public class TCP_message
    {
        public int id { get; set; }
        public int type { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public List<string> textAttributes;
        public List<bool> boolAttributes;

        public TCP_message()
        {
            id = TCP_const.INVALID;
            type = TCP_const.INVALID;
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

        public int GetMyShitFFS()
        {
            return id;
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
