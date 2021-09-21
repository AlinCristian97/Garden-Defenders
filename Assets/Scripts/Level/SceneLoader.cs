using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float _waitingTime;
        private int _currentSceneIndex;

        [Header("Fade Transition")]
        private Animator _transition;
        [SerializeField] private float _timeAfterEnd;
        [SerializeField] private float _timeBeforeStart;

        private void Awake()
        {
            if (GetComponentInChildren<Animator>() != null)
            {
                _transition = GetComponentInChildren<Animator>();
            }
        }

        private IEnumerator Start()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (_currentSceneIndex == 0)
            {
                StartCoroutine(WaitForTime());
            }

            yield return new WaitForSeconds(_timeBeforeStart);
            _transition.SetTrigger("End");
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(_waitingTime);
            LoadMainMenu();
        }
    
        public void LoadMainMenu()
        {
            StartCoroutine(LoadScene("MainMenuScene"));
        }

        public void RestartScene()
        {
            StartCoroutine(LoadScene(_currentSceneIndex));
        }

        public void LoadNextScene()
        {
            StartCoroutine(LoadScene(_currentSceneIndex + 1));
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_timeAfterEnd);

            SceneManager.LoadScene(sceneIndex);
        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_timeAfterEnd);

            SceneManager.LoadScene(sceneName);
        }

        private IEnumerator QuitGameCoroutine()
        {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_timeAfterEnd);

            Application.Quit();
        }

        public void LoadSpecificLevel(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));
        }

        public void LoadLoseScene()
        {
            SceneManager.LoadScene("LoseScene");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            StartCoroutine(QuitGameCoroutine());
        }
    }
}
