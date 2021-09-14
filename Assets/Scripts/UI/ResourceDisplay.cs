using System;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Implementations;
using General.Patterns.Singleton.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceDisplay : MonoBehaviour, IObserver
    {
        [SerializeField] private TextMeshProUGUI _amountText;

        private IShopManager _shopManager;
        
        private void OnEnable()
        {
            _shopManager.AttachObserver(this);
        }

        private void Awake()
        {
            _shopManager = ShopManager.Instance;
        }

        private void Start()
        {

            UpdateBalanceText();
        }

        public void GetNotified()
        {
            UpdateBalanceText();
        }

        private void OnDisable()
        {
            _shopManager.DetachObserver(this);
        }

        private void UpdateBalanceText()
        {
            _amountText.text = _shopManager.Balance.ToString();
        }
    }
}