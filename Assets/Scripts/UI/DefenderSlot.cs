using System;
using General;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefenderSlot : MonoBehaviour, IObserver
    {
        public Defender Defender { get; set; }
        private Button _button;
    
        [SerializeField] private Image _defenderAvatarImage;
        [SerializeField] private Text _defenderCostText;
        
        private ISelectionManager _selectionManager;
        private IPauseManager _pauseManager;

        private void OnEnable()
        {
            _pauseManager.AttachObserver(this);
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _selectionManager = SelectionManager.Instance;
            _pauseManager = PauseManager.Instance;
        }

        private void Start()
        {
            _defenderAvatarImage.sprite = Defender.Avatar;
            _defenderCostText.text = Defender.Cost.ToString();
        }

        private void OnDisable()
        {
            _pauseManager.DetachObserver(this);
        }
        
        public void GetNotified()
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
                _selectionManager.SelectDefenderToBuild(Defender);
            }
        }
    }
}