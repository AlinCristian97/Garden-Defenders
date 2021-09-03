using UnityEngine;

public class CombatDefenderIdleState : CombatDefenderState
{
    public CombatDefenderIdleState(CombatDefender combatDefender) : base(combatDefender)
    {
    }

    public override void Enter()
    {
        CombatDefender.Animator.SetBool("IsIdling", true);

        Debug.Log("Enter: Idle");
    }

    public override void Exit()
    {
        CombatDefender.Animator.SetBool("IsIdling", false);

        Debug.Log("Exit: Idle");
    }

    public override void Execute()
    {
        if (CombatDefender.HasTargetInAttackRange() && CombatDefender.AttackCooldownPassed())
        {
            CombatDefender.StateMachine.ChangeState(CombatDefender.States.AttackState);
        }

        Debug.Log("Execute: Idle");
    }
}