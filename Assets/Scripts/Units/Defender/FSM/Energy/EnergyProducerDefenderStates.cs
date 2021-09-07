public class EnergyProducerDefenderStates
{
    public EnergyProducerDefenderState IdleState { get; }
    public EnergyProducerDefenderState DeliverState { get; }
    public EnergyProducerDefenderDeadState DeadState { get; }

    public EnergyProducerDefenderStates(EnergyProducerDefender energyProducerDefender)
    {
        IdleState = new EnergyProducerDefenderIdleState(energyProducerDefender);
        DeliverState = new EnergyProducerDefenderDeliverState(energyProducerDefender);
        DeadState = new EnergyProducerDefenderDeadState(energyProducerDefender);
    }
}