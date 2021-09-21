using General;
using UnityEngine;

namespace DataPersistence
{
    public class GameProgressTrackerContainer : MonoBehaviour
    {
        #region Singleton

        public static GameProgressTrackerContainer Instance;

        #endregion

        private static GameProgressTracker _loadedProgress;
        public GameProgressTracker GameProgressTracker;

        private void Awake()
        {
            #region Singleton

            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            #endregion
            
            Debug.Log(Application.persistentDataPath);
            
            _loadedProgress = GameDataAccess.Load();
            
            GameProgressTracker = _loadedProgress ?? new GameProgressTracker();
        }
    }
}