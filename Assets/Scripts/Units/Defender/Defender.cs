using System;
using System.Collections;
using Audio;
using General;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

public abstract class Defender : Unit, IObserver
{
    [field:SerializeField] public int Cost { get; private set; }
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public Sprite Avatar { get; private set; }
    [field:SerializeField] public GameObject TilePreview { get; private set; }
    [field:SerializeField] public int MinimumLevelAvailability { get; private set; }
    
    private readonly Color _selectedColor = Color.cyan;
    private readonly Color _normalColor = Color.white;
    
    private ISelectionManager _selectionManager;

    public Tile Tile => GetComponentInParent<Tile>();

    private void OnEnable()
    {
        _selectionManager.AttachObserver(this);
    }

    private void OnDisable()
    {
        _selectionManager.DetachObserver(this);
    }

    protected override void Awake()
    {
        base.Awake();
        
        _selectionManager = SelectionManager.Instance;
    }

    protected override IEnumerator ProcessDeath()
    {
        if (_selectionManager.DefenderToSell == this)
        {
            _selectionManager.DeselectDefenderToSell();
        }

        Tile.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        if (HealthHUD != null)
        {
            HealthHUD.gameObject.SetActive(false);
        }
        
        SetDeadState();
        
        float deathAnimationDuration = Animator.GetCurrentAnimatorClipInfo(0).Length;;
        yield return new WaitForSeconds(deathAnimationDuration);
        
        Tile.gameObject.layer = LayerMask.NameToLayer("Input");

        Destroy(gameObject);
    }

    public void GetNotified()
    {
        if (_selectionManager.DefenderToSell != null)
        {
            SpriteRenderer.color = _selectionManager.DefenderToSell == this ? _selectedColor : _normalColor;
        }
        else
        {
            SpriteRenderer.color = _normalColor;
        }
    }
}