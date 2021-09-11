using System;
using UnityEngine;

namespace General
{
    public class PauseControl : MonoBehaviour
    {
        [SerializeField] private GameObject _panelCanvas;
        
        public static bool GameIsPaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        private void Start()
        {
            ResumeGame();
        }

        public void PauseGame()
        {
            _panelCanvas.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            _panelCanvas.SetActive(false);
            GameIsPaused = false;
            Time.timeScale = 1;
        }
    }
}