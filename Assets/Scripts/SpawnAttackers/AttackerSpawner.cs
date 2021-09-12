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
        [field:SerializeField] public WavesConfigSO SpawnConfig { get; private set; }
        
        [SerializeField] private float _minTimeBetweenSpawns = 1f;
        [SerializeField] private float _maxTimeBetweenSpawns = 3f;

        public IEnumerator SpawnWave(int waveNumber)
        {
            if (SpawnConfig != null)
            {
                if (SpawnConfig.Waves.Length == 0)
                {
                    Debug.Log($"{name}'s SpawnConfig's number of Waves is set to 0. Wave spawning stopped.");
                    yield break;
                }
                
                if (waveNumber >= SpawnConfig.Waves.Length)
                {
                    Debug.Log($"{name}'s SpawnConfig's number of Waves is less than mentioned in the SpawnManager. Wave spawning stopped.");
                    yield break;
                }
                
                if (SpawnConfig.Waves[waveNumber].Length == 0)
                {
                    Debug.Log($"{name}'s SpawnConfig's wave number {waveNumber + 1} has no attackers configured. Wave spawning stopped.");
                    yield break;
                }
                
                int attackerSlotCount = 0;
                foreach (Attacker attacker in SpawnConfig.Waves[waveNumber])
                {
                    attackerSlotCount++;

                    if (attacker == null)
                    {
                        Debug.LogWarning($"{name}'s SpawnConfig's wave {waveNumber + 1}, attacker slot {attackerSlotCount} " + 
                                         " is Empty. Skipping iteration.");
                        continue;
                    }
                    
                    float timeBetweenSpawns = Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
            
                    Instantiate(attacker, transform.position, Quaternion.identity, transform);
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