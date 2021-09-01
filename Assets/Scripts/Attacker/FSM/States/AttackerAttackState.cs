using UnityEngine;

public class AttackerAttackState : AttackerState
{
    public AttackerAttackState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter: Attack");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exit: Attack");
    }

    public override void Execute()
    {
        base.Execute();
        
        if (Attacker.AttackCooldownPassed())
        {
            Attacker.UpdateNextAttack();
            Attacker.TriggerAttackAnimation();
        }

        if (!Attacker.IsNearDefender())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
        }

        Debug.Log("Execute: Attack");
    }
}