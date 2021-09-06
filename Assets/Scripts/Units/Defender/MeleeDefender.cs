using UnityEngine;

public class MeleeDefender : CombatDefender
{
    private Attacker _target;

    protected override float AttackRange => 0.75f;

    protected override void Attack()
    {
        _target = GetTargetInAttackRange().GetComponent<Attacker>();
        _target.TakeDamage(Damage);
    }
}