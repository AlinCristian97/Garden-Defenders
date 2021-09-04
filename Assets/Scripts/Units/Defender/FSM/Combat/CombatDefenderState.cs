using General.FSM;

public abstract class CombatDefenderState : State
{
    protected readonly CombatDefender CombatDefender;

    protected CombatDefenderState(CombatDefender combatDefender)
    {
        CombatDefender = combatDefender;
    }
}