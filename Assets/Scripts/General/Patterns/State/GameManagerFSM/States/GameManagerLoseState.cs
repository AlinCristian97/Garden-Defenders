using System.Collections;
using General.ObjectPooling;
using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerLoseState : GameManagerState
    {
        public override void Enter()
        {
            GameManager.Instance.StartCoroutine(LoseDisplayCoroutine(0.5f));
        }
        
        private IEnumerator LoseDisplayCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.LoseCanvas, true);
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.LevelProgressionSlider, false);

            GameManager.Instance.gameObject.SetActive(false);
            SpawnManager.Instance.gameObject.SetActive(false);
            LanesGenerator.Instance.gameObject.SetActive(false);
            WarnMessageManager.Instance.gameObject.SetActive(false);
            ObjectPooler.Instance.gameObject.SetActive(false);
            
            AudioManager.Instance.StartCoroutine(PlayLoseMusic());
        }

        private IEnumerator PlayLoseMusic()
        {
            const float durationInSeconds = 1f;
            
            yield return AudioManager.Instance.StartCoroutine(AudioManager.Instance.StartFade(durationInSeconds, 0.0001f));
            AudioManager.Instance.Stop(AudioManager.Instance.Music, "BackgroundMusic");
            AudioManager.Instance.StartCoroutine(AudioManager.Instance.StartFade(durationInSeconds, 1f));
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