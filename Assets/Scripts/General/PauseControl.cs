using UnityEngine;

namespace General
{
    public class PauseControl : MonoBehaviour
    {
        public static bool GameIsPaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameIsPaused = !GameIsPaused;
                PauseGame();
            }
        }

        public void PauseGame ()
        {
            if(GameIsPaused)
            {
                Time.timeScale = 0f;
            }
            else 
            {
                Time.timeScale = 1;
            }
        }
    }
}