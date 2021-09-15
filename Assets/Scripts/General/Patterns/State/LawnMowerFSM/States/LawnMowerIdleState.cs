using UnityEngine;

namespace General.Patterns.State.LawnMowerFSM.States
{
    public class LawnMowerIdleState : LawnMowerState
    {
        public LawnMowerIdleState(LawnMower lawnMower) : base(lawnMower)
        {
        }

        public override void Enter()
        {
            LawnMower.Animator.SetBool("IsIdling", true);
        }

        public override void Exit()
        {
            LawnMower.Animator.SetBool("IsIdling", false);
        }

        public override void Execute()
        {
            int attackerLayerMaskValue = (int) Mathf.Pow(2f, LayerMask.NameToLayer("Attacker"));

            if (LawnMower.Collider.IsTouchingLayers(attackerLayerMaskValue))
            {
                LawnMower.StateMachine.ChangeState(LawnMower.States.ActiveState);
            }
        }
    }
}