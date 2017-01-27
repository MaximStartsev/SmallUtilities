using System.IO;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.MVC.Main
{
    internal static class MainModelSerializer
    {
        private const string Filename = "config.xml";
        public static MainModel Load()
        {
            if (File.Exists(Filename))
            {
                var serializer = new XmlSerializer(typeof(MainModel));
                using (var stream = File.OpenRead(Filename))
                {
                    return (MainModel)serializer.Deserialize(stream);
                }
            }
            return new MainModel();//todo set defaultvalues
        }
        public static void Save(MainModel model)
        {
            var serializer = new XmlSerializer(typeof(MainModel));
            using (var stream = File.OpenWrite(Filename))
            {
                serializer.Serialize(stream, model);
            }
        }
    }
}
