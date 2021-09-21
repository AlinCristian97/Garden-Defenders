using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.AttackerFSM.States
{
    public class AttackerAttackState : AttackerState
    {
        public AttackerAttackState(Attacker attacker) : base(attacker)
        {
        }
    
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
            if (Attacker.AttackCooldownPassed())
            {
                Attacker.UpdateNextAttack();
                Attacker.TriggerAttackAnimation();
            }

            if (Attacker.AttackSFXCooldownPassed())
            {
                Attacker.UpdateNextAttackSFX();
                PlayRandomAttackSFX();
            }

            if (!Attacker.SetTargetInAttackRange())
            {
                Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
            }
        }

        private void PlayRandomAttackSFX()
        {
            if (Attacker.AttackSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, Attacker.AttackSounds.Length);

                AudioManager.Instance.PlayOneShot(Attacker.AttackSounds, Attacker.AttackSounds[randomIndex].Name);
            }
        }
    }
}