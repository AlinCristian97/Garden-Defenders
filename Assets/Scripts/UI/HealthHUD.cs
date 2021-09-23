using System;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthHUD : MonoBehaviour, IObserver
    {
        [Header("Health Text")]
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _healthHighlights;

        [SerializeField] private Color _veryHealthyColor;
        [SerializeField] private Color _healthyColor;
        [SerializeField] private Color _hurtColor;
        [SerializeField] private Color _veryHurtColor;

        private Unit _unit;
    
        private void OnEnable()
        {
            _unit.AttachObserver(this);
            SetHealthColorByCurrentHealthValue(_unit);
            _healthBar.fillAmount = _unit.CurrentHealth / (float)_unit.MaxHealth;
        }
    
        private void Awake()
        {
            _unit = GetComponentInParent<Unit>();

            if (!UIManager.ShowHealthHUD)
            {
                HideCanvasGroup();
            }
        }

        private void Start()
        {
            SetHealthColorByCurrentHealthValue(_unit);
        }

        private void HideCanvasGroup()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void OnDisable()
        {
            _unit.DetachObserver(this);
        }

        public void GetNotified()
        {
            _healthBar.fillAmount = _unit.CurrentHealth / (float)_unit.MaxHealth;
            SetHealthColorByCurrentHealthValue(_unit);
        }

        private void SetHealthColorByCurrentHealthValue(Unit unit)
        {
            //TODO: Refactor for readability

            if (unit.CurrentHealth > unit.MaxHealth * 0.8f)
            {
                _healthBar.color = SetFullAlphaToColor(_veryHealthyColor);
                _healthHighlights.color = _veryHealthyColor;
            }
            else if (unit.CurrentHealth <= unit.MaxHealth * 0.8f &&
                     unit.CurrentHealth > unit.MaxHealth * 0.6f)
            {
                _healthBar.color = SetFullAlphaToColor(_healthyColor);
                _healthHighlights.color = _healthyColor;
            }
            else if (unit.CurrentHealth <= unit.MaxHealth * 0.6f &&
                     unit.CurrentHealth > unit.MaxHealth * 0.4f)
            {
                _healthBar.color = SetFullAlphaToColor(_hurtColor);
                _healthHighlights.color = _hurtColor;
            }
            else if (unit.CurrentHealth <= unit.MaxHealth * 0.4f)
            {
                _healthBar.color = SetFullAlphaToColor(_veryHurtColor);
                _healthHighlights.color = _veryHurtColor;
            }
        }

        private Color SetFullAlphaToColor(Color color)
        {
            return color * new Color(1f, 1f, 1f, 2f);
        }
    }
}
