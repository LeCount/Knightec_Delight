using System;
using System.Collections.Generic;

namespace SharedResources
{
    /// <summary>A serializable structure of data, to be sent over TCP.</summary>
    [Serializable]
    public class TcpMessage
    {
        /// <summary>Notation for if this message is a reply or a request.</summary>
        public int type { get; set; }

        /// <summary>Notation for what kind of message this is, and what it regards.</summary>
        public int id { get; set; }
        
        /// <summary>Who sent this message.</summary>
        public string source { get; set; }

        /// <summary>Who the message is intended for.</summary>
        public string destination { get; set; }

        /// <summary>A list of arguments.</summary>
        public List<string> text_attributes;

        /// <summary>A corresponding list (to the textAttributes) for validation purposes.</summary>
        public List<bool> bool_attributes;

        /// <summary>Default constructor; initialize a new message: id = invalid, type = invalid</summary>
        public TcpMessage()
        {
            id = TcpConst.INVALID;
            type = TcpConst.INVALID;
            source = null;
            destination = null;
            text_attributes = new List<string>();
            bool_attributes = new List<bool>();
        }

        public List<string> GetTextAttributes(){ return text_attributes;}

        public void AddTextAttribute(string str){text_attributes.Add(str);}

        public List<bool> GetBoolAttributes(){return bool_attributes;}

        public void AddBoolAttribute(bool b){bool_attributes.Add(b);}

        public TcpMessage CreateJoinRequest(string username, string password, string mailaddress)
        {
            id = TcpConst.JOIN;
            type = TcpConst.REQUEST;
            source = TcpNetworking.GetIP();
            destination = "SERVER";
            AddTextAttribute(username);
            AddTextAttribute(password);
            AddTextAttribute(mailaddress);
            return this;
        }

        public TcpMessage CreateLoginRequest(string username, string password, string code)
        {
            id = TcpConst.LOGIN;
            type = TcpConst.REQUEST;
            source = TcpNetworking.GetIP();
            destination = "SERVER";
            AddTextAttribute(username);
            AddTextAttribute(password);
            AddTextAttribute(code);
            return this;
        }

        public TcpMessage CreateDisconnectRequest()
        {
            TcpMessage msg = new TcpMessage();
            return msg;
        }

        public TcpMessage CreateGetUsersRequest()
        {
            TcpMessage msg = new TcpMessage();
            return msg;
        }

        public TcpMessage CreateAddFriendRequest()
        {
            TcpMessage msg = new TcpMessage();
            return msg;
        }

        public TcpMessage CreateGetFriendsStatusRequest()
        {
            TcpMessage msg = new TcpMessage();
            return msg;
        }

        public TcpMessage CreateGetClientDataRequest()
        {
            TcpMessage msg = new TcpMessage();
            return msg;
        }
    }
}
