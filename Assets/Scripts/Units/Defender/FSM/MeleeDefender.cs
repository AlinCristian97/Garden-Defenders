using UnityEngine;

public class MeleeDefender : Defender
{
    protected override float AttackRange => 0.75f;
    
    public override void Attack()
    {
        TriggerAttackAnimation();
    }
}