using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    private bool _isAvailable = true;
    private Defender _currentDefender;
    
    private BuildManager _buildManager;

    private void Awake()
    {
        _buildManager = FindObjectOfType<BuildManager>();
    }

    private void OnMouseDown()
    {
        HandleBuildDefender();
        HandleSellDefender();
        
        GetComponent<SpriteRenderer>().color = Color.cyan;
        Debug.Log("clicked on tile");
    }

    private void HandleBuildDefender()
    {
        if (_buildManager.DefenderToBuild == null) return;

        if (_isAvailable)
        {
            _currentDefender = _buildManager.DefenderToBuild;

            _buildManager.BuildDefender(transform.position, transform);

            _isAvailable = false;
        }
    }

    private void HandleSellDefender()
    {
        //TODO: Add conditional
        
        // if sell button clicked
            SellTileDefender();
        //
    }

    public void SellTileDefender()
    {
        Destroy(_currentDefender.gameObject);
        _currentDefender = null;
    
        //refund
        _isAvailable = true;
    }
}
