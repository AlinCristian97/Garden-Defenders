using General.Patterns.Singleton;
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
            
            AudioManager.Instance.PlayClipAtPoint(LawnMower.EngineSound, LawnMower.transform.position);
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