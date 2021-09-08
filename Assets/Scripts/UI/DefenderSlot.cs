using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSlot : MonoBehaviour
{
    public Defender Defender { get; set; }
    private Button _button;
    
    [SerializeField] private Image _defenderAvatarImage;
    [SerializeField] private Text _defenderCostText;

    [SerializeField] private RectTransform _rectTransform;
    private BuildManager _buildManager;


    private void Awake()
    {
        // _rectTransform = GetComponent<RectTransform>();

        //TODO: Think of a cleaner way to select the defender
        _buildManager = FindObjectOfType<BuildManager>();

        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _defenderAvatarImage.sprite = Defender.Avatar;
        _defenderCostText.text = Defender.Cost.ToString();
    }

    //TODO: Don't use Update for this. Refactor!
    private void Update()
    {
        if (PauseControl.GameIsPaused)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }

    public void SelectDefenderToBuild()
    {
        //TODO: Cancel if selects again same defender
        if (PauseControl.GameIsPaused) return;
        
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
}