using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private float _defaultVolume = 0.5f;
    
    [SerializeField] private Slider _difficultySlider;
    [SerializeField] private float _defaultDifficulty = 0f;

    private void Start()
    {
        _volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        _difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }

    private void Update()
    {
        var musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer)
        {
            musicPlayer.SetVolume(_volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found. Did you start from Splash Screen?");
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(_volumeSlider.value);
        PlayerPrefsController.SetDifficulty(_difficultySlider.value);

        FindObjectOfType<SceneLoader>().LoadMainMenu();
    }

    public void SetDefault()
    {
        _volumeSlider.value = _defaultVolume;
        _difficultySlider.value = _defaultDifficulty;
    }
}
