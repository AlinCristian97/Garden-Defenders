using System;
using System.Collections.Generic;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton

        private static LevelManager _instance;
        
        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LevelManager>();
                }
                
                return _instance;
            }
        }

        #endregion

        [SerializeField] private GameObject _levelSlotHolder;

        private LevelSlot[] _levelSlots;

        public GameProgressTracker GameProgressTracker { get; private set; }

        private void Awake()
        {
            Debug.Log(Application.persistentDataPath);
            
            Debug.Log("B Loaded progress: " + GameProgressTrackerContainer.LoadedProgress);

            GameProgressTrackerContainer.LoadedProgress = GameDataAccess.Load();

            if (GameProgressTrackerContainer.LoadedProgress != null)
            {
                GameProgressTracker = GameProgressTrackerContainer.LoadedProgress;
            }
            else
            {
                GameProgressTracker = new GameProgressTracker();
            }
            
            Debug.Log("Game Progress Tracker: ");
            Debug.Log("With value: " + GameProgressTracker.HighestLevelUnlocked);
            
            _levelSlots = _levelSlotHolder.GetComponentsInChildren<LevelSlot>();
            DisableLevelSlots();
            
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SetLevelsUnlockedStatus();

            EnableAllUnlockedLevels();
        }

        private void DisableLevelSlots()
        {
            foreach (LevelSlot levelSlot in _levelSlots)
            {
                levelSlot.DisableButton();
            }
        }

        private void EnableAllUnlockedLevels()
        {
            foreach (LevelSlot levelSlot in _levelSlots)
            {
                if (levelSlot.IsFirstLevel)
                {
                    levelSlot.EnableButton();
                }
                
                if (!levelSlot.IsFirstLevel && levelSlot.Unlocked)
                {
                    levelSlot.EnableButton();
                }
            }
        }

        private void SetLevelsUnlockedStatus()
        {
            for (int i = 1; i < GameProgressTracker.HighestLevelUnlocked; i++)
            {
                _levelSlots[i].UnlockLevel();
            }
        }
    }
}