using UnityEngine;

public class EnergyProducerDefenderDeliverState : EnergyProducerDefenderState
{
    public EnergyProducerDefenderDeliverState(EnergyProducerDefender energyProducerDefender) 
        : base(energyProducerDefender)
    {
    }

    public override void Enter()
    {
        Debug.Log("Energy Enter: Attack");
    }

    public override void Exit()
    {
        Debug.Log("Energy Exit: Attack");
    }

    public override void Execute()
    {
        if (EnergyProducerDefender.DeliverCooldownPassed())
        {
            EnergyProducerDefender.UpdateNextDeliver();
            EnergyProducerDefender.TriggerDeliverAnimation();
        }

        Debug.Log("Energy Execute: Attack");
    }
}