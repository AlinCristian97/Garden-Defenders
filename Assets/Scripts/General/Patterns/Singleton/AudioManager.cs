using System;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;

namespace General.Patterns.Singleton
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager Instance;

        #endregion

        [field:SerializeField] public Sound[] GeneralSounds { get; private set; }

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
            Play(GeneralSounds, "BackgroundMusic");
        }

        public void InitializeAudioSourceComponentsForArray(Sound[] soundArray)
        {
            foreach (Sound sound in soundArray)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;

                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.Loop;
            }
        }

        private void InitializeSoundArrays()
        {
            InitializeAudioSourceComponentsForArray(GeneralSounds);
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
            Play(GeneralSounds, "ButtonClickSFX");
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