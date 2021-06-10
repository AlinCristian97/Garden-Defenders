using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int _numberOfAttackers = 0;
    private bool _levelTimerFinished = false;

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
        }
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
