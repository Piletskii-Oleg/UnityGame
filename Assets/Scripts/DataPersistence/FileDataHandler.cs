using System.IO;
using UnityEngine;
using DataPersistence.GameDataFiles;

namespace DataPersistence
{
    public static class FileDataHandler
    {
        public static GameData Load(string directory, string dataFileName)
        {
            var fullPath = Path.Combine(directory, dataFileName);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException();
            }
            
            using var stream = new FileStream(fullPath, FileMode.Open);
            using var reader = new StreamReader(stream);
            var dataToLoad = reader.ReadToEnd();

            return JsonUtility.FromJson<GameData>(dataToLoad);
        }

        public static void Save(GameData data, string directory, string dataFileName)
        {
            var fullPath = Path.Combine(directory, dataFileName);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }

            var dataToStore = JsonUtility.ToJson(data, true);
            using var stream = new FileStream(fullPath, FileMode.Create);
            using var writer = new StreamWriter(stream);
            writer.Write(dataToStore);

            Debug.Log(fullPath);
        }
    }
}