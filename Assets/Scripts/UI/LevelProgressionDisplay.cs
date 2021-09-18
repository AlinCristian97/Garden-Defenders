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
        [SerializeField] private GameObject _checkpointImagePrefab;        
        [SerializeField] private Transform _checkPointsContainer;
        
        private float _durationInSeconds;
        private ISpawnManager _spawnManager;
        private GameManager _gameManager;

        private float _sliderWidth;

        private void Awake()
        {
            _spawnManager = SpawnManager.Instance;
            _gameManager = GameManager.Instance;

            _sliderWidth = _slider.GetComponent<RectTransform>().sizeDelta.x;
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
            while (_slider.value <= _durationInSeconds)
            {
                _slider.value += Time.deltaTime;
                yield return null;
            }
        }

        private void InitializeSlider()
        {
            Debug.Log(GetLeftmostPointX());
            _slider.maxValue = _durationInSeconds;
            _slider.value = _slider.minValue;
            InstantiateCheckpoints();
        }

        private void InstantiateCheckpoints()
        {
            float leftmostPoint = GetLeftmostPointX();
            float distanceBetweenCheckpoints = _sliderWidth / (_spawnManager.NumberOfWaves - 1);
            float offsetX = 0;

            for (int i = 0; i < _spawnManager.NumberOfWaves - 1; i++)
            {
                GameObject checkpoint = Instantiate(_checkpointImagePrefab, _checkPointsContainer);
                checkpoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftmostPoint + offsetX, 0f);
                if (i == 0)
                {
                    checkpoint.GetComponent<RectTransform>().anchoredPosition += new Vector2(6f, 0f);
                }
                offsetX += distanceBetweenCheckpoints;
            }
        }

        private float GetLeftmostPointX() => -_sliderWidth / 2;
    }
}
