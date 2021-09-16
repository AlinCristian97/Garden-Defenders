using System;
using System.Collections;
using System.Collections.Generic;
using General.Patterns.Singleton;
using SpawnAttackers.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnAttackers
{
    public class AttackerSpawner : MonoBehaviour
    {
        [SerializeField] private WavesConfigSO _spawnConfig;

        [SerializeField] private float _minTimeBetweenSpawns = 1f;
        [SerializeField] private float _maxTimeBetweenSpawns = 3f;

        private IEnumerator _spawnWaveCoroutine;
        
        public void StartSpawningWave(int waveNumber)
        {
            _spawnWaveCoroutine = SpawnWaveCoroutine(waveNumber);
            StartCoroutine(_spawnWaveCoroutine);
        }

        public void StopSpawningWave()
        {
            StopCoroutine(_spawnWaveCoroutine);
        }

        private IEnumerator SpawnWaveCoroutine(int waveNumber)
        {
            if (_spawnConfig != null)
            {
                if (_spawnConfig.Waves.Length == 0)
                {
                    Debug.Log($"{name}'s SpawnConfig's number of Waves is set to 0. Wave spawning stopped.");
                    yield break;
                }
                
                if (waveNumber >= _spawnConfig.Waves.Length)
                {
                    Debug.Log($"{name}'s SpawnConfig's number of Waves is less than mentioned in the SpawnManager. Wave spawning stopped.");
                    yield break;
                }
                
                if (_spawnConfig.Waves[waveNumber].Length == 0)
                {
                    Debug.Log($"{name}'s SpawnConfig's wave number {waveNumber + 1} has no attackers configured. Wave spawning stopped.");
                    yield break;
                }
                
                int attackerSlotCount = 0;
                foreach (Attacker attacker in _spawnConfig.Waves[waveNumber])
                {
                    attackerSlotCount++;

                    if (attacker == null)
                    {
                        Debug.LogWarning($"{name}'s SpawnConfig's wave {waveNumber + 1}, attacker slot {attackerSlotCount} " + 
                                         " is Empty. Skipping iteration.");
                        continue;
                    }
                    
                    float timeBetweenSpawns = Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);

                    var smallRandomSpawnPositionOffset = new Vector3(Random.Range(-0.15f, 0.3f), 0f, 0f);
                    Attacker instantiatedAttacker = Instantiate(attacker, transform.position + smallRandomSpawnPositionOffset, Quaternion.identity, transform);
                    
                    if (waveNumber + 1 == SpawnManager.Instance.NumberOfWaves)
                    {
                        SpawnManager.Instance.LastWaveAttackersList.Add(instantiatedAttacker);
                    }
                    
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }
            else
            {
                Debug.LogWarning($"{name} is missing a Spawn Config!");
            }
        }
    }
}