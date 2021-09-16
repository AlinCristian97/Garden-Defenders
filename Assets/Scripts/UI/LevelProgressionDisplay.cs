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
        [SerializeField] private float _refreshRate = 0.5f;

        private float _durationInSeconds;
        private ISpawnManager _spawnManager;
        private GameManager _gameManager;

        private void Awake()
        {
            _spawnManager = SpawnManager.Instance;
            _gameManager = GameManager.Instance;
        }

        private IEnumerator Start()
        {
            _durationInSeconds = _spawnManager.TimeBetweenWaves * _spawnManager.NumberOfWaves - 1;
            InitializeSlider();
            
            yield return new WaitForSeconds(_gameManager.GetReadyTimeInSeconds);

            StartCoroutine(UpdateProgress());
        }

        private IEnumerator UpdateProgress()    
        {
            while (true)
            {
                _slider.value = Time.timeSinceLevelLoad - _gameManager.GetReadyTimeInSeconds;
                yield return new WaitForSeconds(_refreshRate);
            }
        }

        private void InitializeSlider()
        {
            _slider.maxValue = _durationInSeconds;
            _slider.value = _slider.minValue;
        }
    }
}
