using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSlot : MonoBehaviour
{
    [SerializeField] private Defender _defender;
    [SerializeField] private Image _defenderAvatarImage;
    [SerializeField] private Text _defenderCostText;

    private void Start()
    {
        _defenderAvatarImage.sprite = _defender.Avatar;
        _defenderCostText.text = _defender.Cost.ToString();
    }

    public void Test()
    {
        Debug.Log("Clicked");
    }
}