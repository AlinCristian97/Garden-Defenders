using UnityEngine;

public class AttackerIdleState : AttackerState
{
    public AttackerIdleState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        Attacker.Animator.SetBool("IsIdling", true);
    }

    public override void Exit()
    {
        Attacker.Animator.SetBool("IsIdling", false);
    }

    public override void Execute()
    {
        if (Attacker.HasTargetInAttackRange() && Attacker.AttackCooldownPassed())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.AttackState);
        }

        if (!Attacker.HasTargetInAttackRange())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
        }
    }
}