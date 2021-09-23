using System.Collections;
using General.Patterns.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinCanvas : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private CanvasGroup _panelGroup;
        private float _alphaValue;
        private const float FADE_TIME = 2f;

        private IEnumerator Start()
        {
            _backgroundImage.CrossFadeAlpha(0f, 0f, false);
            _panelGroup.alpha = 0;
            
            _backgroundImage.CrossFadeAlpha(1f, FADE_TIME, false);

            yield return new WaitForSeconds(FADE_TIME);

            yield return StartCoroutine(FadePanelIn());
        }

        private IEnumerator FadePanelIn()
        {
            while (_alphaValue <= 1)
            {
                _alphaValue += Time.deltaTime;
                _panelGroup.alpha = _alphaValue;
                
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}