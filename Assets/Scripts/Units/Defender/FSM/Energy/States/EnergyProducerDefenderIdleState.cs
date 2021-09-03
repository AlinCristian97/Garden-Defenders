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

        Debug.Log("Energy Enter: Idle");
    }

    public override void Exit()
    {
        EnergyProducerDefender.Animator.SetBool("IsIdling", false);

        Debug.Log("Energy Exit: Idle");
    }

    public override void Execute()
    {
        if (EnergyProducerDefender.DeliverCooldownPassed())
        {
            EnergyProducerDefender.StateMachine.ChangeState(EnergyProducerDefender.States.DeliverState);
        }

        Debug.Log("Energy Execute: Idle");
    }
}