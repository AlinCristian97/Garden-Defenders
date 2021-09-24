using System;
using System.Collections;
using General;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressionDisplay : MonoBehaviour, IObserver
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject _checkpointImagePrefab;        
        [SerializeField] private Transform _checkPointsContainer;
        [SerializeField] private GameObject _nextWaveGameObject;
        [SerializeField] private TextMeshProUGUI _bonusEnergyText;
        [SerializeField] private float _nextWaveHideTime;
        private int _bonusEnergy;

        private float _durationInSeconds;
        private ISpawnManager _spawnManager;
        private GameManager _gameManager;

        private float _sliderWidth;

        private void OnEnable()
        {
            SpawnManager.Instance.AttachObserver(this);
        }

        private void OnDisable()
        {
            SpawnManager.Instance.DetachObserver(this);
        }

        private void Awake()
        {
            _spawnManager = SpawnManager.Instance;
            _gameManager = GameManager.Instance;

            _sliderWidth = _slider.GetComponent<RectTransform>().sizeDelta.x;
        }

        private IEnumerator Start()
        {
            _durationInSeconds = _spawnManager.TimeBetweenWaves * (_spawnManager.NumberOfWaves - 1);
            InitializeSlider();
            
            yield return new WaitForSeconds(_gameManager.GetReadyTimeInSeconds);

            StartCoroutine(UpdateProgress());
            StartCoroutine(UpdateBonusEnergy());
        }

        private IEnumerator UpdateBonusEnergy()
        {
            const float updateFrequency = 3f;
            
            while (!SpawnManager.Instance.HasFinishedSpawningWaves)
            {
                _bonusEnergy = SpawnManager.Instance.BonusEnergy();
                _bonusEnergyText.text = _bonusEnergy.ToString();
                yield return new WaitForSeconds(updateFrequency);
            }
        }
        
        private IEnumerator UpdateProgress()    
        {
            while (_slider.value <= _durationInSeconds)
            {
                _slider.value = SpawnManager.Instance.TotalWaitTime();
                yield return null;
            }
        }

        public void SpawnNextWave()
        {
            ShopManager.Instance.AddToBalance(_bonusEnergy);
            SpawnManager.Instance.SpawnNextWave();

            StartCoroutine(TemporaryHideNextWaveButton());
            
            _bonusEnergy = Mathf.RoundToInt(SpawnManager.Instance.BonusEnergyInitialValue);
            _bonusEnergyText.text = _bonusEnergy.ToString();
        }

        private IEnumerator TemporaryHideNextWaveButton()
        {
            _nextWaveGameObject.SetActive(false);

            yield return new WaitForSeconds(_nextWaveHideTime);

            if (!SpawnManager.Instance.HasFinishedSpawningWaves)
            {
                _nextWaveGameObject.SetActive(true);
            }
        }

        private void InitializeSlider()
        {
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
        
        public void GetNotified()
        {
            if (SpawnManager.Instance.HasFinishedSpawningWaves)
            {
                HandleHideNextWaveButton();
            }
            else
            {
                StartCoroutine(TemporaryHideNextWaveButton());
            }
        }

        private void HandleHideNextWaveButton()
        {
            _nextWaveGameObject.SetActive(false);
        }
    }
}
