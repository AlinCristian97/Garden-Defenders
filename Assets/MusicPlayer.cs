using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefsController.GetMasterVolume();
        
        DontDestroyOnLoad(this);
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
