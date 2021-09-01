using General.FSM;

public abstract class DefenderState : State
{
    protected readonly Defender Defender;

    protected DefenderState(Defender defender)
    {
        Defender = defender;
    }
}