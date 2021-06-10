using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject _winLabel;
    [SerializeField] private float _waitToLoad;

    private int _numberOfAttackers = 0;
    private bool _levelTimerFinished = false;

    private void Start()
    {
        _winLabel.SetActive(false);
    }

    public void AttackerSpawned()
    {
        _numberOfAttackers++;
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
        
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void LevelTimerFinished()
    {
        _levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (AttackerSpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}
