using UnityEngine;

public class AttackerIdleState : AttackerState
{
    public AttackerIdleState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Attacker.Animator.SetBool("IsIdling", true);
        
        Debug.Log("Enter: Idle");
    }

    public override void Exit()
    {
        base.Exit();
        
        Attacker.Animator.SetBool("IsIdling", false);

        Debug.Log("Exit: Idle");
    }

    public override void Execute()
    {
        base.Execute();
        
        if (Attacker.IsNearDefender() && Attacker.AttackCooldownPassed())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.AttackState);
        }

        if (!Attacker.IsNearDefender())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
        }
        
        Debug.Log("Execute: Idle");
    }
}