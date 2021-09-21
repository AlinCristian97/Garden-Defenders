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
        [SerializeField] private Image _defenderCostSymbol;
        [SerializeField] private TextMeshProUGUI _defenderNameText;
        
        [Header("Minimum Requirement")]
        [SerializeField] private GameObject _minimumRequirementWindow;
        [SerializeField] private TextMeshProUGUI _minimumRequirementText;

        [Space]
        [SerializeField] private Color32 _unchooseDefenderCardColor;
        [SerializeField] private Color32 _chooseDefenderCardColor;
        
        private UIManager _uiManager;
        private Transform _availableCardsContainer;
        private Transform _chosenCardsContainer;
        private Button _button;

        public bool IsEnabled => _button.interactable;

        private void Awake()
        {
            _uiManager = UIManager.Instance;

            _availableCardsContainer = _uiManager.AvailableCardsContainer;
            _chosenCardsContainer = _uiManager.ChosenCardsContainer;

            _button = GetComponent<Button>();

            DisableCard();
            _minimumRequirementWindow.SetActive(true);
        }

        private void Start()
        {
            _defenderAvatarImage.sprite = Defender.Avatar;
            _defenderNameText.text = Defender.Name;
            _defenderCostText.text = Defender.Cost.ToString();
            _minimumRequirementText.text = $"Available for\nlevels {Defender.MinimumLevelAvailability}+";

            EnableIfEligible();
        }
        
        public void PlayButtonClickSFX()
        {
            AudioManager.Instance.PlayButtonClickSFX();
        }

        public void DisableCard()
        {
            Color disabledColor = Color.gray;
            
            _defenderAvatarImage.color = disabledColor;
            _defenderCostSymbol.color = disabledColor;
            _defenderCostText.color = disabledColor;
            _defenderNameText.color = disabledColor;
            
            _button.interactable = false;
        }

        public void EnableIfEligible()
        {
            Color enabledColor = Color.white;

            if (Defender.MinimumLevelAvailability <= GameManager.Instance.CurrentLevel)
            {
                _defenderAvatarImage.color = enabledColor;
                _defenderCostSymbol.color = enabledColor;
                _defenderCostText.color = enabledColor;
                _defenderNameText.color = enabledColor;
                
                _button.interactable = true;
                _minimumRequirementWindow.SetActive(false);
            }
        }

        public void ToggleChooseCard()
        {
            IsChosen = IsChosen == false;
            SetAppropriateParent();
            SetAppropriateHoverColor();

            if (IsChosen)
            {
                GameManager.Instance.AddChosenDefender(Defender);
            }
            else
            {
                GameManager.Instance.RemoveChosenDefender(Defender);
            }
        }

        private void SetAppropriateHoverColor()
        {
            ColorBlock cb = _button.colors;

            cb.highlightedColor = IsChosen ? _unchooseDefenderCardColor : _chooseDefenderCardColor;
            cb.pressedColor = IsChosen ? _unchooseDefenderCardColor : _chooseDefenderCardColor;
            
            _button.colors = cb;
        }

        private void SetAppropriateParent() 
        {
            transform.SetParent(IsChosen ? _chosenCardsContainer : _availableCardsContainer);
        }
    }
}