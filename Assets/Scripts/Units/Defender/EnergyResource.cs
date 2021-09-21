using System;
using System.Collections;
using Audio;
using General.Patterns.Singleton;
using UnityEngine;

public class EnergyResource : MonoBehaviour
{
    [SerializeField] private int _energyAmount = 20;
    [SerializeField] private float _lifetime = 6f;

    [Header("Disappearing Visual Cues")]
    [SerializeField] private float _lifetimeRemainingSecondsFlashTrigger = 3f;
    [SerializeField] private float _aboutToDisappearFlashFrequencyInSeconds = 0.5f;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _fadedColor;
    
    [Header("SFX")]
    [SerializeField] private Sound _collectSound;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(ExpireCoroutine());
    }

    private IEnumerator ExpireCoroutine()
    {
        yield return new WaitForSeconds(_lifetime - _lifetimeRemainingSecondsFlashTrigger);

        StartCoroutine(AboutToDisappearFlashesCoroutine());

        yield return new WaitForSeconds(_lifetimeRemainingSecondsFlashTrigger);
        
        Destroy(gameObject);
    }

    private IEnumerator AboutToDisappearFlashesCoroutine()
    {
        while (true)
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
        
        //Optionally add a SFX / "poof" effect or some animations
        AudioManager.Instance.PlayClipAtPoint(_collectSound, transform.position);
        
        Destroy(gameObject);
    }
}