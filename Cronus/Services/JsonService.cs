using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Models;
using Newtonsoft.Json;

namespace Cronus.Services
{
    public static class JsonService
    {
        /// <summary>
        /// Creates a <see cref="BookHeader"/> instance from a JSON
        /// file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="FileLoadException">Thrown if the string
        /// found in the JSON file cannot be deserialized</exception>
        public static BookHeader LoadBookHeaderFromJsonFile(string filePath)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(filePath);
            string jsonString = LoadJsonStringFromFile(filePath);
            return JsonConvert.DeserializeObject<BookHeader>(jsonString) ??
                throw new FileLoadException("Unable to load book header " +
                "from JSON file");
        }

        /// <summary>
        /// Reads data string from a JSON file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if
        /// the file path is not of type JSON</exception>
        public static string LoadJsonStringFromFile(string filePath)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(filePath);
            if (Path.GetExtension(filePath) != ".json")
            {
                throw new ArgumentOutOfRangeException(nameof(filePath));
            }

            return File.ReadAllText(filePath);
        }

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
