using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSlot : MonoBehaviour
{
    public CombatDefender Defender { get; set; }
    
    [SerializeField] private Image _defenderAvatarImage;
    [SerializeField] private Text _defenderCostText;

    [SerializeField] private RectTransform _rectTransform;
    private BuildManager _buildManager;


    private void Awake()
    {
        // _rectTransform = GetComponent<RectTransform>();

        //TODO: Think of a cleaner way to select the defender
        _buildManager = FindObjectOfType<BuildManager>();
    }

    private void Start()
    {
        _defenderAvatarImage.sprite = Defender.Avatar;
        _defenderCostText.text = Defender.Cost.ToString();
    }

    public void SelectDefenderToBuild()
    {
        //TODO: Cancel if selects again same defender

        if (_buildManager.DefenderToBuild == Defender)
        {
            Debug.Log($"Deselected {Defender.name}");
            _buildManager.DeselectDefenderToBuild();
        }
        else
        {
            _buildManager.SelectDefenderToBuild(Defender);
            Debug.Log($"Selected: {Defender.name}");
        }
    }

    public float GetHeight() => _rectTransform.rect.height;
    public float GetHalfHeight() => _rectTransform.rect.height / 2f;
}