public class DefenderStates
{
    public DefenderIdleState IdleState { get; }
    public DefenderAttackState AttackState { get; }

    public DefenderStates(Defender defender)
    {
        IdleState = new DefenderIdleState(defender);
        AttackState = new DefenderAttackState(defender);
    }
}