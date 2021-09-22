using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.AttackerFSM.States
{
    public class AttackerDeadState : AttackerState
    {
        public AttackerDeadState(Attacker attacker) : base(attacker)
        {
        }
    
        public override void Enter()
        {
            Attacker.Animator.SetTrigger("Die");

            const int chancePercent = 60;

            float randomNumber = Random.Range(1, 101);
            if (randomNumber < chancePercent)
            {
                PlayRandomDeathSFX();
            }
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
        }
        
        private void PlayRandomDeathSFX()
        {
            if (AudioManager.Instance.DefenderDeathVariations.Length > 0)
            {
                int randomIndex = Random.Range(0, AudioManager.Instance.DefenderDeathVariations.Length);

                AudioManager.Instance.PlayOneShot(
                    AudioManager.Instance.DefenderDeathVariations,
                    AudioManager.Instance.DefenderDeathVariations[randomIndex].Name);
            }
        }
    }
}