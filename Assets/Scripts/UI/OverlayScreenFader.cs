using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OverlayScreenFader : MonoBehaviour
    {
        [SerializeField] private float _startDelayInSeconds = 1f;
        [SerializeField] private float _fadeDuration = 2f;
    
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_startDelayInSeconds);
            FadeOut();
        }

        private void FadeOut()
        {
            GetComponent<Image>().CrossFadeAlpha(0f, _fadeDuration, false);
        }
    
        private void FadeIn()
        {
            GetComponent<Image>().CrossFadeAlpha(1f, _fadeDuration, false);
        }
    }
}
