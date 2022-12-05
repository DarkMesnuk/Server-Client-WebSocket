using System.IO;
using System.Xml.Serialization;

namespace Server
{
    public class Config
    {
        public int RangeFrom { get; set; }

        public int RangeTo { get; set; }

        public string MulticastGroup { get; set; }

        public void Serializer()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            using (Stream stream = new FileStream("serverConfig.xml", FileMode.Open, FileAccess.Read))
            {
                var config = (Config)serializer.Deserialize(stream);

                RangeFrom = config.RangeFrom;
                RangeTo = config.RangeTo;
                MulticastGroup = config.MulticastGroup;
            }

        }
    }
}
