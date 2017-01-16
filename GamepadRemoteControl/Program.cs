using System.IO;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl
{
    class Program
    {
        private const string ConfigFilename = "config.xml";
        static void Main(string[] args)
        {
            MainModel mainModel;
            if (File.Exists(ConfigFilename))
            {
                var serializer = new XmlSerializer(typeof(MainModel));
                using (var config = File.OpenRead(ConfigFilename))
                {
                    mainModel = (MainModel)serializer.Deserialize(config);
                }
            }
            else
            {
                mainModel = new MainModel();
            }
        }
    }
}
