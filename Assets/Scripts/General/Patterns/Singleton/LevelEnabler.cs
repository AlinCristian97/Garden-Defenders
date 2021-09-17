using System;
using System.Collections.Generic;
using DataPersistence;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class LevelEnabler : MonoBehaviour
    {
        private LevelSlot[] _levelSlots;
        
        private void Awake()
        {
            _levelSlots = GetComponentsInChildren<LevelSlot>();
            DisableLevelSlots();
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
            for (int i = 1; i < GameProgressTrackerContainer.Instance.GameProgressTracker.HighestLevelUnlocked; i++)
            {
                _levelSlots[i].UnlockLevel();
            }
        }
    }
}