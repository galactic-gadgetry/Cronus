using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cronus.Services
{
    public static class JsonService
    {
        /// <summary>
        /// Saves the object as a serialized JSON file.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        public static void SaveObject(object obj, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(obj,
                Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
