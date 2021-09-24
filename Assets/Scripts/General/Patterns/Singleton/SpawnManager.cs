using System.Collections;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using SpawnAttackers;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class SpawnManager : MonoBehaviour, ISpawnManager, IObservable
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
        
        #region Observer

        public List<IObserver> Observers { get; private set; } = new List<IObserver>();

        public void AttachObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            if (Observers.Count > 0)
            {
                foreach (IObserver observer in Observers)
                {
                    observer.GetNotified();
                }
            }
        }

        #endregion

        [field: SerializeField] public int NumberOfWaves { get; private set; } = 3;
        [field: SerializeField] public float TimeBetweenWaves { get; private set; } = 30f;
        [Space]
        [SerializeField] private AttackerSpawner[] _attackerSpawners;

        public float BonusEnergyInitialValue => TimeBetweenWaves * _bonusEnergyMultiplier;

        public float TotalSpawnTimeDuration => TimeBetweenWaves * (NumberOfWaves - 1);
        private float _totalWaitedTime;
        
        private float _waveWaitTime;
        private float _bonusEnergy;
        [SerializeField] private float _bonusEnergyMultiplier = 2f;
        
        public bool HasFinishedSpawningWaves { get; private set; }
        public List<Attacker> LastWaveAttackersList { get; private set; } = new List<Attacker>();

        private IEnumerator _launchSpawnersCoroutine;
        
        public void StartSpawningAttackers()
        {
            _launchSpawnersCoroutine = LaunchSpawnersCoroutine();
            StartCoroutine(_launchSpawnersCoroutine);
        }

        public int BonusEnergy()
        {
            return Mathf.RoundToInt(_bonusEnergy);
        }

        public float TotalWaitTime() => _totalWaitedTime;

        public void StopSpawningAttackers()
        {
            StopCoroutine(_launchSpawnersCoroutine);

            foreach (AttackerSpawner attackerSpawner in _attackerSpawners)
            {
                attackerSpawner.StopSpawningWave();
            }
        }
        
        private IEnumerator LaunchSpawnersCoroutine()
        {
            _totalWaitedTime = 0f;
            
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
                NotifyObservers();
                Debug.Log($"WAVE {waveNumber + 1} SPAWNED!");
                foreach (AttackerSpawner attackerSpawnPoint in _attackerSpawners)
                {
                    attackerSpawnPoint.StartSpawningWave(waveNumber);
                }
                if (waveNumber == NumberOfWaves - 1)
                {
                    yield return null;
                }
                else
                {
                    _waveWaitTime = TimeBetweenWaves;
                    _bonusEnergy = BonusEnergyInitialValue;
                    while (_waveWaitTime > 0.0f) {
                        _waveWaitTime -= Time.deltaTime;
                        _totalWaitedTime += Time.deltaTime;
                        _bonusEnergy -= Time.deltaTime * _bonusEnergyMultiplier;
                        yield return null;
                    }
                }
            }

            HasFinishedSpawningWaves = true;
            NotifyObservers();
            if (WarnMessageManager.Instance != null)
            {
                WarnMessageManager.Instance.SpawnWarningMessage("Last wave!", 0f);
            }
        }

        public void SpawnNextWave()
        {
            _totalWaitedTime += _waveWaitTime;
            _waveWaitTime = 0f;
        }
    }
}