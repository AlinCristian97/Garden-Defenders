using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanelsController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _audioPanelGroup;
    [SerializeField] private CanvasGroup _videoPanelGroup;
    [SerializeField] private CanvasGroup _uiPanelGroup;
    
    [Header("Highlight current panel button")]
    [SerializeField] private Button _audioSettingsButton;
    [SerializeField] private Button _videoSettingsButton;
    [SerializeField] private Button _uiSettingsButton;

    private void OnEnable()
    {
        if (_audioPanelGroup.alpha >= 1 - Mathf.Epsilon)
        {
            _audioSettingsButton.Select();
        }
        else if (_videoPanelGroup.alpha >= 1 - Mathf.Epsilon)
        {
            _videoSettingsButton.Select();
        }
        else if (_uiPanelGroup.alpha >= 1 - Mathf.Epsilon)
        {
            _uiSettingsButton.Select();
        }
    }

    private void Start()
    {
        InitializePanelsDisplay();
    }

    private void InitializePanelsDisplay()
    {
        HideShowCanvasGroup(_audioPanelGroup, true);
        HideShowCanvasGroup(_videoPanelGroup, false);
        HideShowCanvasGroup(_uiPanelGroup, false);
    }

    public void ShowOnlyAudioPanelGroup()
    {
        HideShowCanvasGroup(_audioPanelGroup, true);
        HideShowCanvasGroup(_videoPanelGroup, false);
        HideShowCanvasGroup(_uiPanelGroup, false);
    }
    
    public void ShowOnlyVideoPanelGroup()
    {
        HideShowCanvasGroup(_videoPanelGroup, true);
        HideShowCanvasGroup(_audioPanelGroup, false);
        HideShowCanvasGroup(_uiPanelGroup, false);
    }
    
    public void ShowOnlyUIPanelGroup()
    {
        HideShowCanvasGroup(_uiPanelGroup, true);
        HideShowCanvasGroup(_videoPanelGroup, false);
        HideShowCanvasGroup(_audioPanelGroup, false);
        
    }
    
    public void ActivateDeactivateCanvas(GameObject canvas, bool active)
    {
        canvas.SetActive(active);
    }

    public void HideShowCanvasGroup(CanvasGroup canvasGroup, bool show)
    {
        canvasGroup.alpha = Convert.ToInt32(show);
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }
}
