using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    class Configuration
    {
        private const string FILENAME = "conf.xml";

        private static Configuration _instance;
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                    _instance.Load();
                }
                return _instance;
            }
        }

        private Configuration()
        {
            if (File.Exists(FILENAME))
            {
                Load();
            }
            else
            {
                InitializeDefaultConfiguration();
            }
        }

        private void Load()
        {

        }
        private void Save()
        {

        }
        private void InitializeDefaultConfiguration()
        {

        }
    }
}
