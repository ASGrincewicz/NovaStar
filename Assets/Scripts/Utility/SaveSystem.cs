using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public static class SaveSystem
    {

        /// <summary>
        /// Uses a Binary Formatter and opens a files stream to save
        /// an instance of PlayerStats.
        /// </summary>
        /// <param name="playerStats"></param>
        public static void SaveRecords(PlayerStats playerStats)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "PlayerRecords";

            FileStream stream = new FileStream(path, FileMode.Create);

            SaveData data = new SaveData(playerStats);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static SaveData LoadData()
        {
            string path = Application.persistentDataPath + "PlayerRecords";
            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                SaveData data = (SaveData)formatter.Deserialize(stream);
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError($"File not found {path}");
                return null;
            }
        }
    }
}
