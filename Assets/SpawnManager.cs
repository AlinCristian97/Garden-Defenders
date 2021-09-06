using System.Collections;
using System.Collections.Generic;
using SpawnAttackers;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private AttackerSpawner[] _attackerSpawnPoints;
    
    [SerializeField] private float _timeBetweenWaves = 10f;
    private float _nextWave;
        
    public void UpdateNextWaveTime()
    {
        _nextWave = Time.time + _timeBetweenWaves;
    }
    
    public bool WaveCooldownPassed() => Time.time > _nextWave;

    private void Update()
    {
        
    }
}