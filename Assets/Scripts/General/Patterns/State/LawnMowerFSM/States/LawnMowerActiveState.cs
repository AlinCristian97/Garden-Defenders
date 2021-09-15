using UnityEngine;

namespace General.Patterns.State.LawnMowerFSM.States
{
    public class LawnMowerActiveState : LawnMowerState
    {
        public LawnMowerActiveState(LawnMower lawnMower) : base(lawnMower)
        {
        }

        public override void Enter()
        {
            LawnMower.Animator.SetBool("IsActive", true);
        }

        public override void Exit()
        {
            LawnMower.Animator.SetBool("IsActive", false);
        }

        public override void Execute()
        {
            LawnMower.transform.Translate(Vector3.right * Time.deltaTime * LawnMower.MovementSpeed);
        }
    }
}