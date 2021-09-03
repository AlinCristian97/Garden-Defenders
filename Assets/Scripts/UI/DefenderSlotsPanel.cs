using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSlotsPanel : MonoBehaviour
{
    [SerializeField] private DefenderSlot _defenderSlot;

    //TODO: Temporary - this variable will be moved
    [SerializeField] private List<CombatDefender> _defendersList;

    private void Awake()
    {
        
    }

    private void Start()
    {
        foreach (CombatDefender defender in _defendersList)
        {
            DefenderSlot defenderSlot = Instantiate(_defenderSlot, transform);

            defenderSlot.Defender = defender;
        }
    }
}