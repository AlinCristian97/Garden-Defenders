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
        [SerializeField] private DefenderCard _defenderCardPrefab;
        [SerializeField] private Button _confirmChosenDefendersButton;
        [Space]
        [SerializeField] private GameObject _chosenDefendersContainer;
        private DefenderCard[] GetAllChosenDefenderCards =>
            _chosenDefendersContainer.GetComponentsInChildren<DefenderCard>();
        [SerializeField] private GameObject _availableDefendersContainer;
        private DefenderCard[] GetAllAvailableDefenderCards =>
            _availableDefendersContainer.GetComponentsInChildren<DefenderCard>();
        [Space]
        [SerializeField] private TextMeshProUGUI _titleText;
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

            _titleText.text = $"Choose up to <color=yellow>{GameManager.Instance.NumberOfDefenderCardsAllowed}</color> plants";
        }

        private void InitializeAvailableCards()
        {
            foreach (Defender defender in GameManager.Instance.AvailableDefendersList)
            {
                DefenderCard defenderCard = Instantiate(_defenderCardPrefab, UIManager.Instance.AvailableCardsContainer);
                defenderCard.Defender = defender;
            }
        }

        public void GetNotified()
        {
            _confirmChosenDefendersButton.interactable = GameManager.Instance.ChosenDefendersList.Count >= 2;
            _chooseMinimumText.enabled = GameManager.Instance.ChosenDefendersList.Count < 2;

            SetAppropriateTitleText();
            LockRemainingAvailableCardsIfMaxNumberOfChosenCardsReached();

        }

        private void LockRemainingAvailableCardsIfMaxNumberOfChosenCardsReached()
        {
            if (GetAllChosenDefenderCards.Length == GameManager.Instance.NumberOfDefenderCardsAllowed)
            {
                foreach (DefenderCard chosenDefenderCard in GetAllAvailableDefenderCards)
                {
                    if (chosenDefenderCard.IsEnabled)
                    {
                        chosenDefenderCard.DisableCard();
                    }
                }
            }
            else
            {
                foreach (DefenderCard chosenDefenderCard in GetAllAvailableDefenderCards)
                {
                    if (!chosenDefenderCard.IsEnabled)
                    {
                        chosenDefenderCard.EnableIfEligible();
                    }
                }
            }
        }

        private void SetAppropriateTitleText()
        {
            if (GameManager.Instance.ChosenDefendersList.Count > 0 && GameManager.Instance.ChosenDefendersList.Count < GameManager.Instance.NumberOfDefenderCardsAllowed)
            {
                if (GameManager.Instance.NumberOfDefenderCardsAllowed - GameManager.Instance.ChosenDefendersList.Count == 1)
                {
                    _titleText.text = $"<color=yellow>{GameManager.Instance.NumberOfDefenderCardsAllowed - GameManager.Instance.ChosenDefendersList.Count}</color> remaining plant";
                }
                else
                {
                    _titleText.text = $"<color=yellow>{GameManager.Instance.NumberOfDefenderCardsAllowed - GameManager.Instance.ChosenDefendersList.Count}</color> remaining plants";
                }
            }
            else if(GameManager.Instance.ChosenDefendersList.Count == GameManager.Instance.NumberOfDefenderCardsAllowed)
            {
                _titleText.text = "You have reached\nthe <color=yellow>maximum</color> number of plants for this level";
            }
            else if(GameManager.Instance.ChosenDefendersList.Count == 0)
            {
                _titleText.text = $"Choose up to <color=yellow>{GameManager.Instance.NumberOfDefenderCardsAllowed}</color> plants";
            }
        }
    }
}