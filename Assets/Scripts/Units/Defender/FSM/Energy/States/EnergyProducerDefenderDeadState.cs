using UnityEngine;

public class EnergyProducerDefenderDeadState : EnergyProducerDefenderState
{
    public EnergyProducerDefenderDeadState(EnergyProducerDefender energyProducerDefender) 
        : base(energyProducerDefender)
    {
    }

    public override void Enter()
    {
        EnergyProducerDefender.Animator.SetTrigger("Die");
    }

    public override void Exit()
    {
    }

    public override void Execute()
    {
    }
}