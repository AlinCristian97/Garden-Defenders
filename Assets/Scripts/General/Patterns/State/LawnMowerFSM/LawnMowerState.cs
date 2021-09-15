namespace General.Patterns.State.LawnMowerFSM
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