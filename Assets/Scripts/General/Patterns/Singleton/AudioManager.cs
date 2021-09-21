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

        private void Start()
        {
            InitializeAudioVolume();
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

        public void PlayClipAtPoint(Sound[] soundsArray, string soundName, Vector3 soundPosition)
        {
            Sound sound = Array.Find(soundsArray, sound => sound.Name == soundName);

            if (sound == null)
            {
                Debug.Log("Sound \"" + soundName + "\" not found!");
                return;
            }
            
            AudioSource.PlayClipAtPoint(sound.Clip, soundPosition);
        }
        
        public void PlayClipAtPoint(Sound sound, Vector3 soundPosition)
        {
            if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound.Clip, soundPosition);
            }
        }    
    }
}