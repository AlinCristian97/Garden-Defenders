using System.Collections;
using UnityEngine;

public abstract class Defender : Unit
{
    [field:SerializeField] public Sprite Avatar { get; private set; }
    [field:SerializeField] public int Cost { get; private set; }
    public Tile Tile => GetComponentInParent<Tile>();

    protected override IEnumerator ProcessDeath()
    {
        if (Tile.BuildManager.DefenderToSell == this)
        {
            Tile.BuildManager.DeselectDefenderToSell();
        }
        
        Tile.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        SetDeadState();
        
        float deathAnimationDuration = Animator.GetCurrentAnimatorClipInfo(0).Length;;
        yield return new WaitForSeconds(deathAnimationDuration);
        
        Tile.gameObject.layer = LayerMask.NameToLayer("Input");

        Destroy(gameObject);
    }
}