using UnityEngine;

public class DefenderIdleState : DefenderState
{
    public DefenderIdleState(Defender defender) : base(defender)
    {
    }

    public override void Enter()
    {
        Defender.Animator.SetBool("IsIdling", true);

        Debug.Log("Enter: Idle");
    }

    public override void Exit()
    {
        Defender.Animator.SetBool("IsIdling", false);

        Debug.Log("Exit: Idle");
    }

    public override void Execute()
    {
        if (Defender.HasTargetInAttackRange() && Defender.AttackCooldownPassed())
        {
            Defender.StateMachine.ChangeState(Defender.States.AttackState);
        }

        Debug.Log("Execute: Idle");
    }
}