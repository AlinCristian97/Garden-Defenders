using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class LevelSlot : MonoBehaviour
    {
        [SerializeField] private int _level;
        [field:SerializeField] public Button Button { get; private set; }
        
        public bool IsFirstLevel { get; private set; }
        public bool Unlocked { get; private set; }
        
        [SerializeField] private TextMeshProUGUI _levelText;

        private void Awake()
        {
            if (_level == 1)
            {
                IsFirstLevel = true;
            }
            
            _levelText.text = "Level " + _level;
        }

        public void DisableButton()
        {
            Button.interactable = false;
        }

        public void EnableButton()
        {
            Button.interactable = true;
        }

        public void UnlockLevel()
        {
            Unlocked = true;
        }
    }
}