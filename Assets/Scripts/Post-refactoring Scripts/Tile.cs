using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    private BuildManager _buildManager;

    private bool IsEmpty => CurrentDefender == null;
    public Defender CurrentDefender => GetComponentInChildren<Defender>();
    
    private void Awake()
    {
        _buildManager = FindObjectOfType<BuildManager>();
    }
    
    private void OnMouseDown()
    {
        if (!IsEmpty)
        {
            if (CurrentDefender == _buildManager.DefenderToSell)
            {
                _buildManager.DeselectDefenderToSell();
            }
            else
            {
                _buildManager.SelectDefenderToSell(CurrentDefender);
            }
        }
        else
        {
            if (_buildManager.DefenderToSell != null)
            {
                _buildManager.DeselectDefenderToSell();
            }
            
            HandleBuildDefender();
        }
    }

    private void HandleBuildDefender()
    {
        if (_buildManager.DefenderToBuild == null) return;

        if (IsEmpty)
        {
            _buildManager.BuildDefender(transform.position, transform);
        }
    }
}