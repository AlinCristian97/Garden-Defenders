using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    private const string MASTER_VOLUME_KEY = "masterVolume";
    private const string DIFFICULTY_KEY = "difficultyKey";
    
    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;
    
    private const float MIN_DIFFICULTY = 0f;
    private const float MAX_DIFFICULTY = 2f;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            Debug.Log($"Master volume set to {volume}");
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }   
        else
        {
            Debug.LogError("Difficulty level is out of range");
        }
    }

    public static float GetMasterVolume() => PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    public static float GetDifficulty() => PlayerPrefs.GetFloat(DIFFICULTY_KEY);
}
