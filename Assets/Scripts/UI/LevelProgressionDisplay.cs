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

        private CanvasGroup _canvasGroup;
        
        private float _durationInSeconds;
        private ISpawnManager _spawnManager;
        private GameManager _gameManager;

        private void Awake()
        {
            _spawnManager = SpawnManager.Instance;
            _gameManager = GameManager.Instance;

            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private IEnumerator Start()
        {
            _durationInSeconds = _spawnManager.TimeBetweenWaves * (_spawnManager.NumberOfWaves - 1);
            Debug.Log("Duration In Seconds: " + _durationInSeconds);
            InitializeSlider();
            
            yield return new WaitForSeconds(_gameManager.GetReadyTimeInSeconds);

            StartCoroutine(UpdateProgress());
        }

        private IEnumerator UpdateProgress()    
        {
            while (true)
            {
                _slider.value += Time.deltaTime;
                yield return null;
            }
        }

        private void InitializeSlider()
        {
            _slider.maxValue = _durationInSeconds;
            _slider.value = _slider.minValue;
        }
    }
}
