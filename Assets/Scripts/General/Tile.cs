using System;
using UnityEngine;

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
            else if (IsEmpty && _buildManager.DefenderToBuild != null)
            {
                _buildManager.BuildDefender(transform.position, transform);
            }
        }
    }
}