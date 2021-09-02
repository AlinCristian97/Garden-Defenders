using UnityEngine;

public class DefenderAttackState : DefenderState
{
    public DefenderAttackState(Defender defender) : base(defender)
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
        if (Defender.AttackCooldownPassed())
        {
            Defender.UpdateNextAttack();
            Defender.Attack();
        }

        if (!Defender.HasTargetInAttackRange())
        {
            Defender.StateMachine.ChangeState(Defender.States.IdleState);
        }

        Debug.Log("Execute: Attack");
    }
}