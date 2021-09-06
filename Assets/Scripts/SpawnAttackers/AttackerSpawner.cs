using System;
using System.Collections;
using System.Collections.Generic;
using SpawnAttackers.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnAttackers
{
    public class AttackerSpawner : MonoBehaviour
    {
        [SerializeField] private WavesConfigSO _spawnConfig;

        private float _timeBetweenWaves = 20f;
        private float _minTimeBetweenSpawns = 2f;
        private float _maxTimeBetweenSpawns = 4f;

        public IEnumerator StartSpawningWaves()
        {
            foreach (AttackersArray spawnConfigLevelWave in _spawnConfig.LevelWaves)
            {
                foreach (Attacker attacker in _spawnConfig.LevelWaves[Array.IndexOf(_spawnConfig.LevelWaves, spawnConfigLevelWave)])
                {
                    float timeBetweenSpawns = Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
                    
                    Instantiate(attacker, transform.position, Quaternion.identity, transform);
                    yield return new WaitForSeconds(timeBetweenSpawns);

                }
                
                yield return new WaitForSeconds(_timeBetweenWaves);
            }

        }
    }
}