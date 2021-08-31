using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsDisplay : MonoBehaviour
{
    [SerializeField] private int _stars = 100;
    private Text _starsText;

    private void Awake()
    {
        _starsText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _starsText.text = _stars.ToString();
    }

    public bool HaveEnoughStars(int amount)
    {
        return _stars >= amount;
    }

    public void AddStars(int amount)
    {
        _stars += amount;
        UpdateDisplay();
    }

    public void SpendStars(int amount)
    {
        if (_stars >= amount)
        {
            _stars -= amount;
            UpdateDisplay(); 
        }
    }
}
