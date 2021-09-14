using System;
using General;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Implementations;
using General.Patterns.Singleton.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DefenderSlot : MonoBehaviour, IObserver
    {
        public Defender Defender { get; set; }
        private Button _button;
        
        [SerializeField] private Image _defenderAvatarImage;
        [SerializeField] private TextMeshProUGUI _defenderCostText;

        [Space] [SerializeField]
        private Color32 _deselectCurrentlySelectedDefenderColor = new Color32(227, 48, 42, 180);
        private static DefenderSlot _currentlySelectedDefenderSlot;
        
        private ISelectionManager _selectionManager;
        private IPauseManager _pauseManager;
        private IShopManager _shopManager;

        private void OnEnable()
        {
            _pauseManager.AttachObserver(this);
            _shopManager.AttachObserver(this);
            _selectionManager.AttachObserver(this);
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _selectionManager = SelectionManager.Instance;
            _pauseManager = PauseManager.Instance;
            _shopManager = ShopManager.Instance;
        }

        private void Start()
        {
            _defenderAvatarImage.sprite = Defender.Avatar;
            _defenderCostText.text = Defender.Cost.ToString();
        }

        private void OnDisable()
        {
            _pauseManager.DetachObserver(this);
            _shopManager.DetachObserver(this);
            _selectionManager.DetachObserver(this);
        }

        public void GetNotified()
        {
            HandleGamePausedChange();
            HandleBalanceChange();
            HandleDefenderToBuildHighlight();
        }

        private void HandleBalanceChange()
        {
            if (Defender.Cost > _shopManager.Balance)
            {
                DeactivateButton();
                GrayOut();
            }
            else
            {
                ActivateButton();
                ColorIn();
            }
        }

        private void GrayOut()
        {
            _defenderCostText.color = Color.gray;
            _defenderAvatarImage.color = Color.gray;
        }

        private void ColorIn()
        {
            _defenderCostText.color = Color.white;
            _defenderAvatarImage.color = Color.white;
        }

        private void HighlightSelectedDefenderSlot()
        {
            if (_currentlySelectedDefenderSlot != null)
            {
                ColorBlock cb = _button.colors;
                
                cb.normalColor = _button.colors.selectedColor;
                cb.highlightedColor = _deselectCurrentlySelectedDefenderColor;
                
                _currentlySelectedDefenderSlot._button.colors = cb;
            }
        }

        private void UnhighlightSelectedDefenderSlot()
        {
            if (_currentlySelectedDefenderSlot != null)
            {
                ColorBlock cb = _button.colors;
                
                cb.normalColor = _button.colors.normalColor;
                
                _currentlySelectedDefenderSlot._button.colors = cb;
            }
        }

        private void HandleGamePausedChange()
        {
            if (_pauseManager.GameIsPaused)
            {
                DeactivateButton();
            }
            else
            {
                ActivateButton();
            }
        }

        private void HandleDefenderToBuildHighlight()
        {
            if (_selectionManager.DefenderToBuild != null)
            {
                HighlightSelectedDefenderSlot();
            }
            else
            {
                UnhighlightSelectedDefenderSlot();
            }
        }
        
        private void ActivateButton()
        {
            _button.interactable = true;
        }

        private void DeactivateButton()
        {
            _button.interactable = false;
        }

        public void SelectDefenderToBuild()
        {
            if (_pauseManager.GameIsPaused) return;

            if (_selectionManager.DefenderToBuild == Defender)
            {
                _selectionManager.DeselectDefenderToBuild();
            }
            else
            {
                _selectionManager.DeselectDefenderToBuild();
                _currentlySelectedDefenderSlot = this;
                _selectionManager.SelectDefenderToBuild(Defender);
            }
        }
    }
}