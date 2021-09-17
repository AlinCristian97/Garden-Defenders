using DataPersistence;
using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerWinState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Win");
            
            Debug.Log("Before save: " + GameProgressTrackerContainer.Instance.GameProgressTracker.HighestLevelUnlocked);
            UpdateProgress();
            SaveProgress();
            Debug.Log("After save: " + GameProgressTrackerContainer.Instance.GameProgressTracker.HighestLevelUnlocked);
            
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

        private void UpdateProgress()
        {
            GameProgressTrackerContainer.Instance.GameProgressTracker.UpdateHighestLevelUnlocked();
        }

        private void SaveProgress()
        {
            GameDataAccess.Save(GameProgressTrackerContainer.Instance.GameProgressTracker);
        }
    }
}