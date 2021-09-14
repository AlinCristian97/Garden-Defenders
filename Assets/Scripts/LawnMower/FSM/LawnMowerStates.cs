using FSM.States;

namespace FSM
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