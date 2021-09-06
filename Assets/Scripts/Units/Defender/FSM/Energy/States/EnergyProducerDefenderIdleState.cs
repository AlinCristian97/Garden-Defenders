using UnityEngine;

public class EnergyProducerDefenderIdleState : EnergyProducerDefenderState
{
    public EnergyProducerDefenderIdleState(EnergyProducerDefender energyProducerDefender) 
        : base(energyProducerDefender)
    {
    }

    public override void Enter()
    {
        EnergyProducerDefender.Animator.SetBool("IsIdling", true);
    }

    public override void Exit()
    {
        EnergyProducerDefender.Animator.SetBool("IsIdling", false);
    }

    public override void Execute()
    {
        if (EnergyProducerDefender.DeliverCooldownPassed())
        {
            EnergyProducerDefender.StateMachine.ChangeState(EnergyProducerDefender.States.DeliverState);
        }
    }
}