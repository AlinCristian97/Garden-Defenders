using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DefenderSlotsPanel : MonoBehaviour
{
    [SerializeField] private DefenderSlot _defenderSlot;

    //TODO: Temporary - this variable will be moved
    [SerializeField] private List<Defender> _defendersList;

    private void Awake()
    {
        
    }

    private void Start()
    {
        foreach (Defender defender in _defendersList)
        {
            DefenderSlot defenderSlot = Instantiate(_defenderSlot, transform);

            defenderSlot.Defender = defender;
        }
    }
}