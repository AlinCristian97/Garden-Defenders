using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] private Defender _defenderPrefab;

    private void Start()
    {
        LabelButtonWithCost();
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();

        if (!costText)
        {
            Debug.LogError(name + "has no cost text, add some!");
        }
        else
        {
            costText.text = _defenderPrefab.GetStarCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();

        foreach (DefenderButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(85, 85, 85, 255);
        }
        
        GetComponent<SpriteRenderer>().color = Color.white;
        
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(_defenderPrefab);
    }
}
