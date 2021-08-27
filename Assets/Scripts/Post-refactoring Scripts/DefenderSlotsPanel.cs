using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSlotsPanel : MonoBehaviour
{
    [SerializeField] private DefenderSlot _defenderSlot;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(_defenderSlot, transform);
        }
    }
}