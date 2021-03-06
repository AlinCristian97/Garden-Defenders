using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.AttackerFSM.States
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
            
            if (Attacker.WalkSFXCooldownPassed())
            {
                Attacker.UpdateNextWalkSFX();
                PlayRandomWalkSFX();
            }
        }
        
        private void PlayRandomWalkSFX()
        {
            if (Attacker.WalkSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, Attacker.WalkSounds.Length);

                AudioManager.Instance.PlayOneShot(Attacker.WalkSounds, Attacker.WalkSounds[randomIndex].Name);
            }
        }
    }
}