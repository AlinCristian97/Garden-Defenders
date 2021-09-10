using General.Patterns.FSM;

public abstract class EnergyProducerDefenderState : State
{
    protected readonly EnergyProducerDefender EnergyProducerDefender;

    protected EnergyProducerDefenderState(EnergyProducerDefender energyProducerDefender)
    {
        EnergyProducerDefender = energyProducerDefender;
    }
}