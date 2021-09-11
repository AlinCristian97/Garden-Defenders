using System;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectedDefenderDisplay : MonoBehaviour, IObserver
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private GameObject _sellButton;

        private ISelectionManager _selectionManager;
        private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _selectionManager.AttachObserver(this);
        }

        private void Awake()
        {
            _selectionManager = SelectionManager.Instance;
            _canvasGroup = GetComponent<CanvasGroup>();
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
                //TODO: Add line for avatar
                _healthText.text = _selectionManager.DefenderToSell.Health.ToString();
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
    }
}
