using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronus.Services
{
    public static class FileService
    {
        // Constants
        public const string SaveFileDirectory =
            "C:\\Users\\nbspangl\\Documents\\coding\\WPF\\Cronus\\CronusTests\\Data";


        /// <summary>
        /// Deletes the file located at the filepath.
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="FileNotFoundException">Thrown if the
        /// file path does not exist</exception>
        public static void DeleteFile(string filePath)
        {
            ArgumentNullException.ThrowIfNull(filePath, nameof(filePath));
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file could not " +
                    "be found", nameof(filePath));
            }

            File.Delete(filePath);
        }

        /// <summary>
        /// Returns an array of all files in the
        /// <seealso cref="SaveFileDirectory"/>
        /// </summary>
        /// <returns></returns>
        public static string[] GetSaveFiles()
        {
            return Directory.GetFiles(SaveFileDirectory);
        }
    }
}
