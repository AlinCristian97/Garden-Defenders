using System;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class SelectedDefenderDisplay : MonoBehaviour, IObserver
    {
        [SerializeField] private Image _avatarIcon;
        [SerializeField] private GameObject _sellButton;
        
        [Header("Health Text")]
        [SerializeField] private TextMeshProUGUI _healthText;

        [SerializeField] private Color _veryHealthyColor;
        [SerializeField] private Color _healthyColor;
        [SerializeField] private Color _hurtColor;
        [SerializeField] private Color _veryHurtColor;

        private ISelectionManager _selectionManager;
        private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _selectionManager.AttachObserver(this);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _selectionManager = SelectionManager.Instance;
        }

        private void Start()
        {
            HideFrame();
        }

        private void OnDisable()
        {
            _selectionManager.DetachObserver(this);
        }

        public void GetNotified()
        {
            UpdateSelectionFrame();
        }
        
        private void UpdateSelectionFrame()
        {
            if (_selectionManager.DefenderToSell != null)
            {
                _avatarIcon.sprite = _selectionManager.DefenderToSell.Avatar;
                _healthText.text = $"{_selectionManager.DefenderToSell.CurrentHealth.ToString()}" +
                                   $"/{_selectionManager.DefenderToSell.MaxHealth.ToString()}";
                SetHealthColorByCurrentHealthValue(_selectionManager.DefenderToSell, _healthText);
                
                _sellButton.gameObject.SetActive(true);
                ShowFrame();
            }
            else
            {
                _sellButton.gameObject.SetActive(false);
                HideFrame();
            }
        }

        private void HideFrame()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void ShowFrame()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void SetHealthColorByCurrentHealthValue(Defender defender, TextMeshProUGUI text)
        {
            //TODO: Improve the structure of this method
            
            if (defender.CurrentHealth > defender.MaxHealth * 0.8f)
            {
                text.color = _veryHealthyColor;
            }
            else if (defender.CurrentHealth <= defender.MaxHealth * 0.8f &&
                     defender.CurrentHealth > defender.MaxHealth * 0.6f)
            {
                text.color = _healthyColor;
            }
            else if (defender.CurrentHealth <= defender.MaxHealth * 0.6f &&
                     defender.CurrentHealth > defender.MaxHealth * 0.4f)
            {
                text.color = _hurtColor;
            }
            else if (defender.CurrentHealth <= defender.MaxHealth * 0.4f)
            {
                text.color = _veryHurtColor;
            }
        }
    }
}
