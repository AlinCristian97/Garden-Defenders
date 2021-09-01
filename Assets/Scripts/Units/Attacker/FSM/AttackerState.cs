using General.FSM;
public abstract class AttackerState : State
{
    protected readonly Attacker Attacker;

    protected AttackerState(Attacker attacker)
    {
        Attacker = attacker;
    }
}