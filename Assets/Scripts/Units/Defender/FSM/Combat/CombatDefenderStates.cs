public class CombatDefenderStates
{
    public CombatDefenderIdleState IdleState { get; }
    public CombatDefenderAttackState AttackState { get; }
    public CombatDefenderDeadState DeadState { get; }

    public CombatDefenderStates(CombatDefender combatDefender)
    {
        IdleState = new CombatDefenderIdleState(combatDefender);
        AttackState = new CombatDefenderAttackState(combatDefender);
        DeadState = new CombatDefenderDeadState(combatDefender);
    }
}