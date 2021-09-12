using System;
using System.Collections;
using General;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressionDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _refreshRate = 0.1f;

        private float _durationInSeconds;
        private ISpawnManager _spawnManager;

        private void Awake()
        {
            _spawnManager = SpawnManager.Instance;
        }

        private IEnumerator Start()
        {
            _durationInSeconds = _spawnManager.TimeBetweenWaves * _spawnManager.NumberOfWaves;
            InitializeSlider();
            
            yield return new WaitForSeconds(_spawnManager.StartDelay);

            StartCoroutine(UpdateProgress());
        }

        private IEnumerator UpdateProgress()    
        {
            while (true)
            {
                _slider.value = _durationInSeconds - Time.time + _spawnManager.StartDelay;
                yield return new WaitForSeconds(_refreshRate);
            }
        }

        private void InitializeSlider()
        {
            _slider.maxValue = _durationInSeconds;
            _slider.value = _slider.maxValue;
        }
    }
}
