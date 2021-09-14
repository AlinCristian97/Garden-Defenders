using UnityEngine;

namespace FSM.States
{
    public class AttackerWalkState : AttackerState
    {
        public AttackerWalkState(Attacker attacker) : base(attacker)
        {
        }
    
        public override void Enter()
        {
            Attacker.Animator.SetBool("IsWalking", true);
        }

        public override void Exit()
        {
            Attacker.Animator.SetBool("IsWalking", false);
        }

        public override void Execute()
        {
            if (Attacker.SetTargetInAttackRange())
            {
                Attacker.StateMachine.ChangeState(Attacker.States.AttackState);
            }
            else
            {
                Attacker.transform.Translate(Vector3.left * Time.deltaTime * Attacker.MovementSpeed);
            }
        }
    }
}