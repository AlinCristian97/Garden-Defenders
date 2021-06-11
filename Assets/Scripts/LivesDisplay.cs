using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private float _baseLives = 3f;
    private float _lives;
    private Text _livesText;

    private void Start()
    {
        _lives = _baseLives - PlayerPrefsController.GetDifficulty();
        _livesText = GetComponent<Text>();
        UpdateDisplay();
        Debug.Log($"Difficulty setting currently is {PlayerPrefsController.GetDifficulty()}");
    }

    private void UpdateDisplay()
    {
        _livesText.text = _lives.ToString();
    }

    public void TakeLife()
    {
        _lives--;
        UpdateDisplay();

        if (_lives <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }
}
