using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace General.DataPersistence
{
    public class GameDataAccess
    {
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);
        
        public static void Save(GameProgressTracker gameProgressTracker)
        {
            string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream;

            try
            {
                if (File.Exists(dataPath))
                {
                    File.WriteAllText(dataPath, string.Empty);
                    fileStream = File.Open(dataPath, FileMode.Open);
                }
                else
                {
                    fileStream = File.Create(dataPath);
                }

                binaryFormatter.Serialize(fileStream, gameProgressTracker);
                fileStream.Close();

                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    SyncFiles();
                }
            }
            catch (Exception e)
            {
                PlatformSafeMessage("Failed to Save: " + e.Message);
            }
        }

        public static GameProgressTracker Load()
        {
            GameProgressTracker gameDetails = null;
            string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);
        
            try
            {
                if (File.Exists(dataPath))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    FileStream fileStream = File.Open(dataPath, FileMode.Open);

                    gameDetails = (GameProgressTracker)binaryFormatter.Deserialize(fileStream);
                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                PlatformSafeMessage("Failed to Load: " + e.Message);
            }

            return gameDetails;
        }

        private static void PlatformSafeMessage(string message)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                WindowAlert(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

    }
}