using System.Collections;
using General;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

public abstract class Defender : Unit
{
    [field:SerializeField] public int Cost { get; private set; }

    [field:SerializeField] public Sprite Avatar { get; private set; }
    [field:SerializeField] public GameObject TilePreview { get; private set; }
    
    private ISelectionManager _selectionManager;
    public Tile Tile => GetComponentInParent<Tile>();

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
}