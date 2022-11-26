using System.IO;
using System.Xml.Serialization;

namespace Client
{
    public class Config
    {
        public int Delay { get; set; }

        public string MulticastGroup { get; set; }

        public void Serializer()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            using (Stream stream = new FileStream("clientConfig.xml", FileMode.Open, FileAccess.Read))
            {
                var config = (Config)serializer.Deserialize(stream);

                Delay = config.Delay;
                MulticastGroup = config.MulticastGroup;
            }

        }
    }
}
