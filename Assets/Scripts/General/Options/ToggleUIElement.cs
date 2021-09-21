using General.Patterns.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace General.Options
{
    public class ToggleUIElement : MonoBehaviour
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        public void SetToggleHealthBars()
        {
            UIManager.ShowHealthHUD = _toggle.isOn;
            PlayerPrefs.SetInt("ShowHealthBars", Utilities.BoolToInt(_toggle.isOn));
        }
    }
}