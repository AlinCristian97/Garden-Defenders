using System;
using General.Patterns.Observer;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceDisplay : MonoBehaviour, IObserver
    {
        [SerializeField] private TextMeshProUGUI _amountText;

        private void OnEnable()
        {
            ShopManager.Instance.AttachObserver(this);
        }

        private void Awake()
        {
            UpdateBalanceText();
        }

        public void GetNotified()
        {
            UpdateBalanceText();
        }

        private void OnDisable()
        {
            ShopManager.Instance.DetachObserver(this);
        }

        private void UpdateBalanceText()
        {
            _amountText.text = ShopManager.Instance.Balance.ToString();
        }
    }
}