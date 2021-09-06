using System;
using System.Collections;
using System.Collections.Generic;
using SpawnAttackers;
using SpawnAttackers.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AttackerSpawner[] _attackerSpawners;
    [SerializeField] private float _playerPreparationTime = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_playerPreparationTime);
        
        LaunchSpawners();
    }

    private void LaunchSpawners()
    {
        if (_attackerSpawners.Length == 0)
        {
            Debug.LogWarning("There are no spawners assigned to the SpawnManager");
            return;
        }

        if (_attackerSpawners.Length > 0 && _attackerSpawners.Length < 5)
        {
            Debug.LogWarning("There are less than 5 spawners assigned to the SpawnManager");
        }
        
        foreach (AttackerSpawner attackerSpawnPoint in _attackerSpawners)
        {
            StartCoroutine(attackerSpawnPoint.StartSpawningWaves());
        }
    }
}