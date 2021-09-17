﻿using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerWinState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Win");
            
            Debug.Log("Before save: " + LevelManager.Instance.GameProgressTracker.HighestLevelUnlocked);
            LevelManager.Instance.GameProgressTracker.UpdateHighestLevelUnlocked();
            GameDataAccess.Save(LevelManager.Instance.GameProgressTracker);
            Debug.Log("After save: " + LevelManager.Instance.GameProgressTracker.HighestLevelUnlocked);
            
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.WinCanvas, true);
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit Win");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute Win");
        }
    }
}