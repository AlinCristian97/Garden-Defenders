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
    [field:SerializeField] public Sprite Avatar { get; private set; }
    [field:SerializeField] public Sprite TilePreviewSprite { get; private set; }
    [field:SerializeField] public int MinimumLevelAvailability { get; private set; }
    
    private readonly Color _selectedColor = Color.cyan;
    private readonly Color _normalColor = Color.white;
    private bool _isDying;

    private ISelectionManager _selectionManager;

    public Tile Tile => GetComponentInParent<Tile>();

    protected override void OnEnable()
    {
        base.OnEnable();
        
        _selectionManager.AttachObserver(this);
    }

    protected virtual void OnDisable()
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

        gameObject.SetActive(false);
    }

    public void EnableComponents()
    {
        _isDying = false;
        
        if (HealthHUD != null && UIManager.ShowHealthHUD) //TODO: Check if required
        {
            HealthHUD.gameObject.SetActive(true);
        }

        if (StateMachine.CurrentState != null)
        {
            SetIdleState();
        } 
    }

    protected abstract void SetIdleState();

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
    
    public override void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            if (!_isDying)
            {
                StartCoroutine(ProcessDeath());
                _isDying = true;
            }
        }
    }
}