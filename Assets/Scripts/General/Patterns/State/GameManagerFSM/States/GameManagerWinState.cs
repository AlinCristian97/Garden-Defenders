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
            
            UpdateProgress();
            SaveProgress();
            
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.WinCanvas, true);
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
            if (GameProgressTrackerContainer.Instance != null)
            {
                GameProgressTrackerContainer.Instance.GameProgressTracker.UpdateHighestLevelUnlocked();
            }
        }

        private void SaveProgress()
        {
            if (GameProgressTrackerContainer.Instance != null)
            {
                GameDataAccess.Save(GameProgressTrackerContainer.Instance.GameProgressTracker);
            }
        }
    }
}