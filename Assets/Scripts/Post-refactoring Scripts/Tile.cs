using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool _isAvailable = true;

    private BuildManager _buildManager;

    private void Awake()
    {
        _buildManager = FindObjectOfType<BuildManager>();
    }

    private void OnMouseDown()
    {
        BuildDefender(_buildManager.DefenderToBuild);
        
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    private void BuildDefender(Defender defenderToBuild)
    {
        if (defenderToBuild == null) return;
        
        Instantiate(defenderToBuild, transform.position, Quaternion.identity);
        _isAvailable = false;
        _buildManager.DefenderToBuild = null;
    }

    public void SellDefender()
    {
        
    }
}
