using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private int _lives = 5;
    private Text _livesText;

    private void Start()
    {
        _livesText = GetComponent<Text>();
        UpdateDisplay();
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
            FindObjectOfType<LevelLoader>().LoadLoseScreen();
        }
    }
}
