using System;
using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace General.Patterns.Singleton
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager Instance;

        #endregion
        
        [field:Header("Audio Mixer")]
        [field:SerializeField] public AudioMixer Mixer { get; private set; }
        [field:Space]
        [field:SerializeField] public AudioMixerGroup MusicGroup { get; private set; }
        [field:SerializeField] public AudioMixerGroup UIGroup { get; private set; }
        [field:SerializeField] public AudioMixerGroup SoundEffectsGroup { get; private set; }
        [field: SerializeField] public float DefaultVolume { get; private set; } = 0.2f;

        [field:Header("General Sounds")]
        [field:SerializeField] public Sound[] Music { get; private set; }
        [field:SerializeField] public Sound[] UI { get; private set; }
        [field:SerializeField] public Sound[] Miscellaneous { get; private set; }
        [field:SerializeField] public Sound[] DefenderDeathVariations { get; private set; }
        
        public IEnumerator StartFade(float duration, float targetVolume)
        {
            float currentTime = 0;
            float currentVol;
            Mixer.GetFloat("MasterVolume", out currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
                Mixer.SetFloat("MasterVolume", Mathf.Log10(newVol) * 20);
                yield return null;
            }
            yield break;
        }
        
        private void Awake()
        {
            #region Singleton

            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
            }

            #endregion

            InitializeSoundArrays();
        }

        private IEnumerator Start()
        {
            InitializeAudioVolume();

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            
            Play(Music, "BackgroundMusic");
        }

        public void InitializeAudioSourceComponentsForArray(Sound[] soundArray, AudioMixerGroup audioMixerGroup)
        {
            foreach (Sound sound in soundArray)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;

                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.Loop;
                sound.Source.outputAudioMixerGroup = audioMixerGroup;
            }
        }
        
        public void InitializeAudioSourceComponentForSound(Sound sound, AudioMixerGroup audioMixerGroup)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
            sound.Source.outputAudioMixerGroup = audioMixerGroup;
        }

        private void InitializeAudioVolume()
        {
            Mixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", DefaultVolume)) * 20 );
            Mixer.SetFloat("SoundEffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("SoundEffectsVolume", DefaultVolume)) * 20 );
            Mixer.SetFloat("UIEffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("UIEffectsVolume", DefaultVolume)) * 20 );
        }

        private void InitializeSoundArrays()
        {
            InitializeAudioSourceComponentsForArray(Music, MusicGroup);
            InitializeAudioSourceComponentsForArray(UI, UIGroup);
            InitializeAudioSourceComponentsForArray(Miscellaneous, SoundEffectsGroup);
            InitializeAudioSourceComponentsForArray(DefenderDeathVariations, SoundEffectsGroup);
        }

        public void Play(Sound[] soundsArray, string soundName)
        {
            Sound sound = Array.Find(soundsArray, sound => sound.Name == soundName);

            if (sound == null)
            {
                Debug.Log("Sound \"" + soundName + "\" not found!");
                return;
            }
            
            sound.Source.Play();
        }
        
        public void Stop(Sound[] soundsArray, string soundName)
        {
            Sound sound = Array.Find(soundsArray, sound => sound.Name == soundName);

            if (sound == null)
            {
                Debug.Log("Sound \"" + soundName + "\" not found!");
                return;
            }
            
            sound.Source.Stop();
        }

        public void PlayButtonClickSFX()
        {
            Play(UI, "ButtonClick");
        }

        public void PlayOneShot(Sound[] soundsArray, string soundName)
        {
            Sound sound = Array.Find(soundsArray, sound => sound.Name == soundName);

            if (sound == null)
            {
                Debug.Log("Sound \"" + soundName + "\" not found!");
                return;
            }

            sound.Source.PlayOneShot(sound.Clip);
        }
    }
}