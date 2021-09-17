using System;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefenderCardsDisplay : MonoBehaviour, IObserver
    {
        [SerializeField] private DefenderCard _defenderCard;
        [SerializeField] private Button _confirmChosenDefendersButton;
        [SerializeField] private TextMeshProUGUI _chooseMinimumText;
        

        private void OnEnable()
        {
            GameManager.Instance.AttachObserver(this);
        }
        
        private void OnDisable()
        {
            if (GameManager.Instance!= null)
            {
                GameManager.Instance.DetachObserver(this);
            }
        }

        private void Awake()
        {
            _confirmChosenDefendersButton.interactable = false;
            _chooseMinimumText.enabled = true;
        }

        private void Start()
        {
            InitializeAvailableCards();
        }

        private void InitializeAvailableCards()
        {
            foreach (Defender defender in GameManager.Instance.AvailableDefendersList)
            {
                DefenderCard defenderCard = Instantiate(_defenderCard, UIManager.Instance.AvailableCardsContainer);
                defenderCard.Defender = defender;
            }
        }

        public void GetNotified()
        {
            _confirmChosenDefendersButton.interactable = GameManager.Instance.ChosenDefendersList.Count >= 2;
            _chooseMinimumText.enabled = GameManager.Instance.ChosenDefendersList.Count < 2;
        }
    }
}
