using System.IO;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainController = new MainController();
            mainController.Run();
        }
    }
}
