using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SceneLoader : MonoBehaviour
    {
        
        
        [SerializeField] private float _waitingTime;
        private int _currentSceneIndex;

        private void Start()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (_currentSceneIndex == 0)
            {
                StartCoroutine(WaitForTime());
            }
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(_waitingTime);
            LoadMainMenu();
        }
    
        public void LoadMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenuScene");
        }

        public void RestartScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_currentSceneIndex);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(_currentSceneIndex + 1);
        }

        public void LoadSpecificLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadLoseScene()
        {
            SceneManager.LoadScene("LoseScene");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }
    }
}
