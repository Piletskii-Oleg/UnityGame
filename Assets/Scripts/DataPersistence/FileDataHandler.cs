using System.IO;
using UnityEngine;

namespace DataPersistence
{
    /// <summary>
    /// Helper class to save data to a file and load from it.
    /// </summary>
    public static class FileDataHandler
    {
        /// <summary>
        /// Loads <see cref="TData"/> from a file.
        /// </summary>
        /// <param name="directory">Directory in which file is contained.</param>
        /// <param name="dataFileName">Name of the save file.</param>
        /// <returns>Data with all the data loaded from the file.</returns>
        /// <exception cref="FileNotFoundException">Throws if file was not found.</exception>
        public static TData Load<TData>(string directory, string dataFileName)
        {
            string fullPath = Path.Combine(directory, dataFileName);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException();
            }
            
            using var stream = new FileStream(fullPath, FileMode.Open);
            using var reader = new StreamReader(stream);
            string dataToLoad = reader.ReadToEnd();

            return JsonUtility.FromJson<TData>(dataToLoad);
        }

        /// <summary>
        /// Saves data to a file.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="directory">Directory in which file is to be contained.</param>
        /// <param name="dataFileName">Name of the save file.</param>
        public static void Save<TData>(TData data, string directory, string dataFileName)
        {
            string fullPath = Path.Combine(directory, dataFileName);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }

            string dataToStore = JsonUtility.ToJson(data, true);
            using var stream = new FileStream(fullPath, FileMode.Create);
            using var writer = new StreamWriter(stream);
            writer.Write(dataToStore);
        }
    }
}