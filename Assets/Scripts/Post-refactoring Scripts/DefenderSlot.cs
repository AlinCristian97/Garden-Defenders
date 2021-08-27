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

    private BuildManager _buildManager;

    private void Awake()
    {
        //TODO: Think of a cleaner way to select the defender
        _buildManager = FindObjectOfType<BuildManager>();
    }

    private void Start()
    {
        _defenderAvatarImage.sprite = _defender.Avatar;
        _defenderCostText.text = _defender.Cost.ToString();
    }

    public void SelectDefenderToBuild()
    {
        //TODO: Cancel if selects again same defender

        if (_buildManager.DefenderToBuild == _defender)
        {
            _buildManager.DefenderToBuild = null;
        }
        else
        {
            _buildManager.SetDefenderToBuild(_defender);
            Debug.Log($"Selected: {_defender.name}");
        }
    }
}