using General.Patterns.Singleton;

namespace General.Patterns.State.DefenderFSM
{
    public class EnergyProducerDefenderDeadState : EnergyProducerDefenderState
    {
        public EnergyProducerDefenderDeadState(EnergyProducerDefender energyProducerDefender) 
            : base(energyProducerDefender)
        {
        }

        public override void Enter()
        {
            EnergyProducerDefender.Animator.SetTrigger("Die");

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
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.Miscellaneous, "PlantDie");
        }
    }
}