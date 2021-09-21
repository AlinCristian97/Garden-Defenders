using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.DefenderFSM
{
    public class CombatDefenderDeadState : CombatDefenderState
    {
        public CombatDefenderDeadState(CombatDefender defender) : base(defender)
        {
        }

        public override void Enter()
        {
            CombatDefender.Animator.SetTrigger("Die");

            //TODO: problem. Entering dead state multiple times.
            Debug.Log("entered dead state");

            PlayDeathSFX();
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
        }

        private void PlayDeathSFX()
        {
            AudioManager.Instance.PlayClipAtPoint(
                CombatDefender.DeathSound,
                CombatDefender.transform.position);
        }
    }
}