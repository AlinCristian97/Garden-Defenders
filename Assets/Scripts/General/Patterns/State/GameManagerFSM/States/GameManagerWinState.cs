using System.Collections;
using General.DataPersistence;
using General.ObjectPooling;
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
            ObjectPooler.Instance.gameObject.SetActive(false);

            AudioManager.Instance.StartCoroutine(PlayWinMusic());
        }
        
        private IEnumerator PlayWinMusic()
        {
            const float durationInSeconds = 1f;
            
            yield return AudioManager.Instance.StartCoroutine(AudioManager.Instance.StartFade(durationInSeconds, 0.0001f));
            AudioManager.Instance.Stop(AudioManager.Instance.Music, "BackgroundMusic");
            AudioManager.Instance.StartCoroutine(AudioManager.Instance.StartFade(durationInSeconds, 1f));
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