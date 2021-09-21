using UnityEngine;
using UnityEngine.Audio;

namespace General.Options
{
    public class SetVolume : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;

        public void SetMusicLevel(float sliderValue)
        {
            _mixer.SetFloat("MusicVolume", GetLogarithmicValue(sliderValue));
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
    
        public void SetSoundEffectsLevel(float sliderValue)
        {
            _mixer.SetFloat("SoundEffectsVolume", GetLogarithmicValue(sliderValue));
            PlayerPrefs.SetFloat("SoundEffectsVolume", sliderValue);
        }
    
        public void SetUIEffectsLevel(float sliderValue)
        {
            _mixer.SetFloat("UIEffectsVolume", GetLogarithmicValue(sliderValue));
            PlayerPrefs.SetFloat("UIEffectsVolume", sliderValue);
        }

        private float GetLogarithmicValue(float sliderValue) => Mathf.Log10(sliderValue) * 20;
    }
}
