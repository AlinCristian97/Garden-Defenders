using System;
using General.Patterns.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefenderCard : MonoBehaviour // IObserver?
    {
        public Defender Defender { get; set; }
        public bool IsChosen { get; private set; }

        [SerializeField] private Image _defenderAvatarImage;
        [SerializeField] private TextMeshProUGUI _defenderCostText;

        private UIManager _uiManager;
        private Transform _availableCardsContainer;
        private Transform _chosenCardsContainer;

        private void Awake()
        {
            _uiManager = UIManager.Instance;

            _availableCardsContainer = _uiManager.AvailableCardsContainer;
            _chosenCardsContainer = _uiManager.ChosenCardsContainer;
        }

        private void Start()
        {
            _defenderAvatarImage.sprite = Defender.Avatar;
            _defenderCostText.text = Defender.Cost.ToString();
        }

        public void ToggleChooseCard()
        {
            IsChosen = IsChosen == false;
            SetAppropriateParent();

            if (IsChosen)
            {
                GameManager.Instance.AddChosenDefender(Defender);
            }
            else
            {
                GameManager.Instance.RemoveChosenDefender(Defender);
            }
        }

        private void SetAppropriateParent()
        {
            transform.SetParent(IsChosen ? _chosenCardsContainer : _availableCardsContainer);
        }
    }
}
