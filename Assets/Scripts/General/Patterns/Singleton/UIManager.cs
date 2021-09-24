using System;
using System.Collections.Generic;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton

        private static UIManager _instance;
        
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                }
                
                return _instance;
            }
        }

        #endregion

        public static bool ShowHealthHUD = true;
        public static bool DefaultShowHealthBarsToggleValue = true;
        public static int DefaultVideoQualityDropdownValue = 2;

        [field:SerializeField] public GameObject MainCanvas { get; private set; }
        [field:SerializeField] public GameObject SelectLevelDefendersCanvas { get; private set; }
        [field:SerializeField] public GameObject PauseCanvas { get; private set; }
        [field:SerializeField] public GameObject LoseCanvas { get; private set; }
        [field:SerializeField] public GameObject WinCanvas { get; private set; }
        [field: Space]
        [field:SerializeField] public CanvasGroup LevelProgressionSlider { get; private set; }
        [field: Space]
        [field:SerializeField] public Transform AvailableCardsContainer { get; private set; }
        [field:SerializeField] public Transform ChosenCardsContainer { get; private set; }

        private void Start()
        {
            InitializeVideoQuality();
        }

        private void InitializeVideoQuality()
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("VideoSettings", DefaultVideoQualityDropdownValue));
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
}