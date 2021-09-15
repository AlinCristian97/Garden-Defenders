namespace General.Patterns.State.AttackerFSM
{
    public abstract class AttackerState : State
    {
        protected readonly Attacker Attacker;

        protected AttackerState(Attacker attacker)
        {
            Attacker = attacker;
        }
    }
}