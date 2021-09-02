﻿public class AttackerStates
{
    public AttackerIdleState IdleState { get; }
    public AttackerWalkState WalkState { get; }
    public AttackerAttackState AttackState { get; }

    
    public AttackerStates(Attacker attacker)
    {
        IdleState = new AttackerIdleState(attacker);
        WalkState = new AttackerWalkState(attacker);
        AttackState = new AttackerAttackState(attacker);
    }
}