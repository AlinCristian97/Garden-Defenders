using General.Patterns.State.GameManagerFSM.States;

namespace General.Patterns.State.GameManagerFSM
{
    public class GameManagerStates
    {
        public GameManagerChooseDefendersState ChooseDefendersState { get; }
        public GameManagerGetReadyState GetReadyState  { get; }
        public GameManagerStartState StartState  { get; }
        public GameManagerOnGoingState OnGoingState  { get; }
        public GameManagerWinState WinState  { get; }
        public GameManagerLoseState LoseState  { get; }

        public GameManagerStates()
        {
            ChooseDefendersState = new GameManagerChooseDefendersState();
            GetReadyState = new GameManagerGetReadyState();
            StartState = new GameManagerStartState();
            OnGoingState = new GameManagerOnGoingState();
            WinState = new GameManagerWinState();
            LoseState = new GameManagerLoseState();
        }
    }
}