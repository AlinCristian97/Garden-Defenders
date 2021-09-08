using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject _winLabel;
    [SerializeField] private GameObject _loseLabel;
    [SerializeField] private float _waitToLoad;

    private int _numberOfAttackers = 0;
    private bool _levelTimerFinished = false;

    private void Start()
    {
        _winLabel.SetActive(false);
        _loseLabel.SetActive(false);
    }

    public void AttackerKilled()
    {
        _numberOfAttackers--;
        if (_numberOfAttackers <= 0 && _levelTimerFinished)
        {
            Debug.Log("End level now!");
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        _winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(_waitToLoad);
        
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

    public void LevelTimerFinished()
    {
        _levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        
    }
    
    public void HandleLoseCondition()
    {
        _loseLabel.SetActive(true);
        Time.timeScale = 0;
    }
}
