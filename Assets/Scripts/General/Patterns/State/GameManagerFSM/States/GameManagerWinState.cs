using DataPersistence;
using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerWinState : GameManagerState
    {
        public override void Enter()
        {
            UpdateProgress();
            SaveProgress();
            
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.WinCanvas, true);
            
            GameManager.Instance.gameObject.SetActive(false);
            SpawnManager.Instance.gameObject.SetActive(false);
            LanesGenerator.Instance.gameObject.SetActive(false);
            WarnMessageManager.Instance.gameObject.SetActive(false);

            StopBackgroundMusic();
            PlayWinMusic();
        }

        private void StopBackgroundMusic()
        {
            AudioManager.Instance.Stop(AudioManager.Instance.Music, "BackgroundMusic");
        }

        private void PlayWinMusic()
        {
            AudioManager.Instance.Play(AudioManager.Instance.Music, "WinMusic");
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
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