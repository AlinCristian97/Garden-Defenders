using UnityEngine;

public class AttackerAttackState : AttackerState
{
    public AttackerAttackState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        // Debug.Log("Enter: Attack");
    }

    public override void Exit()
    {
        // Debug.Log("Exit: Attack");
    }

    public override void Execute()
    {
        if (Attacker.AttackCooldownPassed())
        {
            Attacker.UpdateNextAttack();
            Attacker.TriggerAttackAnimation();
        }

        if (!Attacker.HasTargetInAttackRange())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
        }

        // Debug.Log("Execute: Attack");
    }
}