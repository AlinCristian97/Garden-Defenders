using UnityEngine;

public class MeleeDefender : CombatDefender
{
    protected override float AttackRange => 0.65f;

    protected override void Attack()
    {
        if (GetTargetInAttackRange() == null) return;

        Target = GetTargetInAttackRange().GetComponent<Attacker>();
        if (!Target.IsDead)
        {
            Target.TakeDamage(Damage);
        }
    }

    protected override void SetDeadState()
    {
        if (StateMachine.CurrentState != States.DeadState)
        {
            StateMachine.ChangeState(States.DeadState);
        }
    }
}