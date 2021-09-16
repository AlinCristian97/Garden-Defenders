using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseCanvas : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private CanvasGroup _graveStonesGroup;
        private float _alphaValue;
        private const float FADE_TIME = 3f;

        private IEnumerator Start()
        {
            _backgroundImage.CrossFadeAlpha(0f, 0f, false);
            _titleText.CrossFadeAlpha(0f, 0f, false);
            _graveStonesGroup.alpha = 0;
            
            _backgroundImage.CrossFadeAlpha(1f, FADE_TIME, false);

            yield return new WaitForSeconds(FADE_TIME);

            _titleText.CrossFadeAlpha(1f, FADE_TIME, false);

            yield return new WaitForSeconds(FADE_TIME);

            yield return StartCoroutine(FadeGravestonesIn());
        }

        private IEnumerator FadeGravestonesIn()
        {
            while (_alphaValue <= 1)
            {
                _alphaValue += Time.deltaTime;
                _graveStonesGroup.alpha = _alphaValue;
                
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}