using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyProducerDefender : Defender
{
    public EnergyProducerDefenderStates States { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        
        States = new EnergyProducerDefenderStates(this);
    }
    
    private void Start()
    {
        StateMachine.Initialize(States.IdleState);
    }
    
    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }
}