using General.ObjectPooling;
using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerLoseState : GameManagerState
    {
        public override void Enter()
        {
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.LoseCanvas, true);

            GameManager.Instance.gameObject.SetActive(false);
            SpawnManager.Instance.gameObject.SetActive(false);
            LanesGenerator.Instance.gameObject.SetActive(false);
            WarnMessageManager.Instance.gameObject.SetActive(false);
            ObjectPooler.Instance.gameObject.SetActive(false);

            StopBackgroundMusic();
            PlayLoseMusic();
        }

        private void StopBackgroundMusic()
        {
            AudioManager.Instance.Stop(AudioManager.Instance.Music, "BackgroundMusic");
        }

        private void PlayLoseMusic()
        {
            AudioManager.Instance.Play(AudioManager.Instance.Music, "LoseMusic");
        }
        public override void Exit()
        {
        }

        public override void Execute()
        {
        }
    }
}