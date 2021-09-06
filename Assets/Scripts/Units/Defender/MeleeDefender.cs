using UnityEngine;

public class MeleeDefender : CombatDefender
{
    protected override float AttackRange => 0.65f;

    protected override void Attack()
    {
        Target = GetTargetInAttackRange().GetComponent<Attacker>();
        Target.TakeDamage(Damage);
    }
}