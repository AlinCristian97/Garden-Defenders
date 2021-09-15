using General.Patterns.State.LawnMowerFSM.States;

namespace General.Patterns.State.LawnMowerFSM
{
    public class LawnMowerStates
    {
        public LawnMowerState IdleState { get; }
        public LawnMowerState ActiveState { get; }


        public LawnMowerStates(LawnMower lawnMower)
        {
            IdleState = new LawnMowerIdleState(lawnMower);
            ActiveState = new LawnMowerActiveState(lawnMower);
        }
    }
}