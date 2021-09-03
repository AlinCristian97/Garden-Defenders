using UnityEngine;

public class CombatDefenderAttackState : CombatDefenderState
{
    public CombatDefenderAttackState(CombatDefender defender) : base(defender)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter: Attack");
    }

    public override void Exit()
    {
        Debug.Log("Exit: Attack");
    }

    public override void Execute()
    {
        if (CombatDefender.AttackCooldownPassed())
        {
            CombatDefender.UpdateNextAttack();
            CombatDefender.TriggerAttackAnimation();
        }

        if (!CombatDefender.HasTargetInAttackRange())
        {
            CombatDefender.StateMachine.ChangeState(CombatDefender.States.IdleState);
        }

        Debug.Log("Execute: Attack");
    }
}