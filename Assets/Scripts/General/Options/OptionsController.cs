using System;
using General.Patterns.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace General.Options
{
    public class OptionsController : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundEffectsVolumeSlider;
        [SerializeField] private Slider _UIVolumeSlider;

        [Header("Video Settings")] 
        [SerializeField] private TMP_Dropdown _videoQualityDropdown;
        
        [Header("UI Settings")] 
        [SerializeField] private Toggle _showHealthBarsToggle;

        private void Start()
        {
            _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", AudioManager.Instance.DefaultVolume);
            _soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", AudioManager.Instance.DefaultVolume);
            _UIVolumeSlider.value = PlayerPrefs.GetFloat("UIEffectsVolume", AudioManager.Instance.DefaultVolume);

            _videoQualityDropdown.value = PlayerPrefs.GetInt("VideoQuality", UIManager.DefaultVideoQualityDropdownValue);
            
            _showHealthBarsToggle.isOn = Utilities.IntToBool(PlayerPrefs.GetInt("ShowHealthBars", Utilities.BoolToInt(UIManager.DefaultShowHealthBarsToggleValue)));
        }

        public void ResetToDefault()
        {
            _musicVolumeSlider.value = AudioManager.Instance.DefaultVolume;
            _soundEffectsVolumeSlider.value = AudioManager.Instance.DefaultVolume;
            _UIVolumeSlider.value = AudioManager.Instance.DefaultVolume;
            
            _videoQualityDropdown.value = UIManager.DefaultVideoQualityDropdownValue;

            _showHealthBarsToggle.isOn = UIManager.DefaultShowHealthBarsToggleValue;
        }
    }
}