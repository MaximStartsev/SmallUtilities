using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    /// <summary>
    /// Реализует методы для работы с бд
    /// </summary>
    class Database
    {
        private const string FILENAME = "data.sqlite";
        private static Database _instance;
        public static Database Instance
        {
            get
            {
                if (_instance != null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }
        
        public static bool IsInitialized { get; private set; }
        /// <summary>
        /// Создаёт новую базу данных SQLite и таблицы в ней
        /// </summary>
        public Database()
        {
            IsInitialized = false;
            if (!File.Exists(FILENAME))
            {
                SQLiteConnection.CreateFile(FILENAME);
                var connection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", FILENAME));
                connection.Open();
                //todo: create tables

                //Vacancy
                var vacancyProps = GetPropertiesFromType(typeof (Vacancy));
                var builder = new StringBuilder("CREATE TABLE Vacancy(");

                builder.AppendLine(")");
            }

            IsInitialized = true;
        }
        private Dictionary<string,Type> GetPropertiesFromType(Type type)
        {
            var properties = type.GetProperties()
                .Where(prop => prop.IsDefined(typeof(FieldAttribute), false));
            var dict = new Dictionary<string, Type>();
            foreach (var prop in properties)
            {
                dict.Add(prop.Name, prop.PropertyType);
            }
            return dict;
        }
    }
}
