using System;
using System.Collections;
using Audio;
using General.Patterns.Singleton;
using UnityEngine;

public class EnergyResource : MonoBehaviour
{
    [field:SerializeField] public string AliasIdentifier { get; private set; }
    [SerializeField] private int _energyAmount = 20;
    [SerializeField] private float _lifetime = 6f;

    [Header("Disappearing Visual Cues")]
    [SerializeField] private float _lifetimeRemainingSecondsFlashTrigger = 3f;
    [SerializeField] private float _aboutToDisappearFlashFrequencyInSeconds = 0.5f;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _fadedColor;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void OnEnable()
    {        
        StartCoroutine(ExpireCoroutine());
        _spriteRenderer.color = _normalColor;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ExpireCoroutine()
    {
        yield return new WaitForSeconds(_lifetime - _lifetimeRemainingSecondsFlashTrigger);

        StartCoroutine(AboutToDisappearFlashesCoroutine());

        yield return new WaitForSeconds(_lifetimeRemainingSecondsFlashTrigger);
        
        gameObject.SetActive(false);
    }

    private IEnumerator AboutToDisappearFlashesCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            _spriteRenderer.color = _normalColor;
            yield return new WaitForSeconds(_aboutToDisappearFlashFrequencyInSeconds);
            _spriteRenderer.color = _fadedColor;
            yield return new WaitForSeconds(_aboutToDisappearFlashFrequencyInSeconds);
        }
    }

    private void OnMouseDown()
    {
        ShopManager.Instance.AddToBalance(_energyAmount);
        
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.Miscellaneous, "EnergyCollect");
        
        gameObject.SetActive(false);
    }
}