using General.Patterns.FSM;

namespace FSM
{
    public abstract class LawnMowerState : State
    {
        protected readonly LawnMower LawnMower;

        protected LawnMowerState(LawnMower lawnMower)
        {
            LawnMower = lawnMower;
        }
    }
}