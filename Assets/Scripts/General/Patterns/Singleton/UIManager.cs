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

        [field:SerializeField] public GameObject MainCanvas { get; private set; }
        [field:SerializeField] public GameObject SelectLevelDefendersCanvas { get; private set; }
        [field:SerializeField] public GameObject PauseCanvas { get; private set; }
        [field:SerializeField] public GameObject LoseCanvas { get; private set; }
        [field:SerializeField] public GameObject WinCanvas { get; private set; }

        [field:SerializeField] public Transform AvailableCardsContainer { get; private set; }
        [field:SerializeField] public Transform ChosenCardsContainer { get; private set; }
        
        public void HideShowCanvasGroup(GameObject canvas, bool active)
        {
            canvas.SetActive(active);
        }
    }
}