using System.Collections;
using General.Patterns.Singleton.Interfaces;
using SpawnAttackers;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class SpawnManager : MonoBehaviour, ISpawnManager
    {
        #region Singleton

        private static SpawnManager _instance;
        
        public static SpawnManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SpawnManager>();
                }
                
                return _instance;
            }
        }

        #endregion
       
        [field: SerializeField] public int NumberOfWaves { get; private set; } = 3;
        [field:SerializeField] public float StartDelay { get; private set; }= 5f;
        [field: SerializeField] public float TimeBetweenWaves { get; private set; } = 30f;
        
        [Space]
        
        [SerializeField] private AttackerSpawner[] _attackerSpawners;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(StartDelay);
        
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

            for (int waveNumber = 0; waveNumber < NumberOfWaves; waveNumber++)
            {
                Debug.Log($"WAVE {waveNumber + 1} SPAWNED!");
                foreach (AttackerSpawner attackerSpawnPoint in _attackerSpawners)
                {
                    StartCoroutine(attackerSpawnPoint.SpawnWave(waveNumber));
                }
                
                yield return new WaitForSeconds(TimeBetweenWaves);
            }
        }
    }
}