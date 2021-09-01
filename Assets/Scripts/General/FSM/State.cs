namespace General.FSM
{
    public abstract class State
    {
        //TODO: Check if required
        protected StateMachine StateMachine;

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Execute();
    }
}