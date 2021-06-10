using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AttackerSpawner : MonoBehaviour
{
    private bool _spawn = true;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private Attacker[] _attackerPrefabs;


    private IEnumerator Start()
    {
        while (_spawn)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        var attackerIndex = Random.Range(0, _attackerPrefabs.Length);
        
        Spawn(_attackerPrefabs[attackerIndex]);
    }

    private void Spawn(Attacker myAttacker)
    {
        Attacker newAttacker = Instantiate(myAttacker, transform.position, transform.rotation);
        newAttacker.transform.parent = transform;
    }
}
