using UnityEngine;

public class AttackerWalkState : AttackerState
{
    public AttackerWalkState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        Attacker.Animator.SetBool("IsWalking", true);

        // Debug.Log("Enter: Walk");
    }

    public override void Exit()
    {
        Attacker.Animator.SetBool("IsWalking", false);

        // Debug.Log("Exit: Walk");
    }

    public override void Execute()
    {
        if (Attacker.IsNearDefender())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.AttackState);
        }
        else
        {
            Attacker.transform.Translate(Vector3.left * Time.deltaTime * Attacker.MovementSpeed);
        }
        
        // Debug.Log("Execute: Walk");
    }
}