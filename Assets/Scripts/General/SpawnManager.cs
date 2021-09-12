using System.Collections;
using SpawnAttackers;
using UnityEngine;

namespace General
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private AttackerSpawner[] _attackerSpawners;
        [SerializeField] private float _playerPreparationTime = 5f;

        [SerializeField] private float _timeBetweenWaves = 30f;

        [SerializeField] private int _numberOfWaves = 3;
        
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_playerPreparationTime);
        
            StartCoroutine(LaunchSpawners());
        }

        private IEnumerator LaunchSpawners()
        {
            if (_attackerSpawners.Length == 0)
            {
                Debug.LogWarning("There are no spawners assigned to the SpawnManager");
                yield return null;
            }

            if (_attackerSpawners.Length > 0 && _attackerSpawners.Length < 5)
            {
                Debug.LogWarning("There are less than 5 spawners assigned to the SpawnManager");
            }

            for (int waveNumber = 0; waveNumber < _numberOfWaves; waveNumber++)
            {
                Debug.Log($"WAVE {waveNumber + 1} SPAWNED!");
                foreach (AttackerSpawner attackerSpawnPoint in _attackerSpawners)
                {
                    StartCoroutine(attackerSpawnPoint.SpawnWave(waveNumber));
                }
                
                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }
    }
}