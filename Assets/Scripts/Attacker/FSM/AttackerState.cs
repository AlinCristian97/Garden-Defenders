using General.FSM;
public abstract class AttackerState : State
{
    protected readonly Attacker Attacker;

    public AttackerState(Attacker attacker)
    {
        Attacker = attacker;
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Execute()
    {
    }
}