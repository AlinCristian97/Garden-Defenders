using UnityEngine;

public class AttackerWalkState : AttackerState
{
    public AttackerWalkState(Attacker attacker) : base(attacker)
    {
    }
    
    public override void Enter()
    {
        Attacker.Animator.SetBool("IsWalking", true);

        Debug.Log("Z Enter: Walk");
    }

    public override void Exit()
    {
        Attacker.Animator.SetBool("IsWalking", false);

        Debug.Log("Z Exit: Walk");
    }

    public override void Execute()
    {
        if (Attacker.HasTargetInAttackRange())
        {
            Attacker.StateMachine.ChangeState(Attacker.States.AttackState);
        }
        else
        {
            Attacker.transform.Translate(Vector3.left * Time.deltaTime * Attacker.MovementSpeed);
        }
        
        Debug.Log("Z Execute: Walk");
    }
}