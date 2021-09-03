public class CombatDefenderStates
{
    public CombatDefenderIdleState IdleState { get; }
    public CombatDefenderAttackState AttackState { get; }

    public CombatDefenderStates(CombatDefender combatDefender)
    {
        IdleState = new CombatDefenderIdleState(combatDefender);
        AttackState = new CombatDefenderAttackState(combatDefender);
    }
}