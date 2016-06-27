using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shared_resources
{
    public class Serializer
    {
        public byte[] StreamToByteArray(Stream stream)
        {
            byte[] byteArray = new byte[1024];
            MemoryStream memStream = new MemoryStream();
            int bit;

            while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
            {
                memStream.Write(byteArray, 0, bit);
            }

            return memStream.ToArray();
        }

        public Stream ByteArrayToStream(Byte[] byteArray)
        {
            return new MemoryStream(byteArray);
        }

        public byte[] Serialize_msg(TCP_message msg)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binFormater = new BinaryFormatter();

            try
            {
                binFormater.Serialize(memStream, msg);
            }
            catch (SerializationException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
            }

            return memStream.ToArray();
        }

        public TCP_message Deserialize_msg(byte[] byteArray)
        {
            MemoryStream memoryStream = new MemoryStream(byteArray);
            BinaryFormatter binFormater = new BinaryFormatter();

            try
            {
                return (TCP_message)binFormater.Deserialize(memoryStream);
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

        }
    }
}
