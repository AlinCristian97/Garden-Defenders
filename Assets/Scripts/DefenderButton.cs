using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] private Defender _defenderPrefab;
    
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
