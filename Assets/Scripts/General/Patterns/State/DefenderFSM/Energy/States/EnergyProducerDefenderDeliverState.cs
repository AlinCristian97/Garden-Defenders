namespace General.Patterns.State.DefenderFSM
{
    public class EnergyProducerDefenderDeliverState : EnergyProducerDefenderState
    {
        public EnergyProducerDefenderDeliverState(EnergyProducerDefender energyProducerDefender) 
            : base(energyProducerDefender)
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
            if (EnergyProducerDefender.DeliverCooldownPassed())
            {
                EnergyProducerDefender.UpdateNextDeliver();
                EnergyProducerDefender.TriggerDeliverAnimation();
            }
        }
    }
}