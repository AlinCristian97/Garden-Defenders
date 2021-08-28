using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    private Defender _currentDefender;
    
    private BuildManager _buildManager;

    private bool IsAvailable => _currentDefender == null;

    private void Awake()
    {
        _buildManager = FindObjectOfType<BuildManager>();
    }

    private void OnMouseDown()
    {
        if (_currentDefender != null)
        {
            _buildManager.SetDefenderToSell(GetComponentInChildren<Defender>());
            _buildManager.SellButton.gameObject.SetActive(true);
        }
        else
        {
            _buildManager.SetDefenderToSell(null);
            _buildManager.SellButton.gameObject.SetActive(false);
            
            HandleBuildDefender();
        }
    }

    private void HandleBuildDefender()
    {
        if (_buildManager.DefenderToBuild == null) return;

        if (IsAvailable)
        {
            _currentDefender = _buildManager.DefenderToBuild;

            _buildManager.BuildDefender(transform.position, transform);
        }
    }

    public void SellDefender(Defender defender)
    {
        Destroy(defender.gameObject);
        _currentDefender = null;
    }
}
