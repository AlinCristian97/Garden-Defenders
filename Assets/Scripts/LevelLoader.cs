using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float _timeToWaitInSeconds;
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
        yield return new WaitForSeconds(_timeToWaitInSeconds);
        LoadNextScene();
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }
    
    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsScreen");
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

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
