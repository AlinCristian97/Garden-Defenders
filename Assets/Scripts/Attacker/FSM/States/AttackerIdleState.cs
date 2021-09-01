using UnityEngine;

public class AttackerIdleState : AttackerState
{
    public AttackerIdleState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        Attacker.Animator.SetBool("IsIdling", true);
        
        // Debug.Log("Enter: Idle");
    }

    public override void Exit()
    {
        Attacker.Animator.SetBool("IsIdling", false);

        // Debug.Log("Exit: Idle");
    }

    public override void Execute()
    {
        if (Attacker.IsNearDefender() && Attacker.AttackCooldownPassed())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.AttackState);
        }

        if (!Attacker.IsNearDefender())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
        }
        
        // Debug.Log("Execute: Idle");
    }
}