using UnityEngine;

public class MeleeDefender : CombatDefender
{
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

    protected override void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }
}