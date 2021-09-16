using General.Patterns.Singleton;

namespace General.Patterns.State.GameManagerFSM
{
    public abstract class GameManagerState : State
    {
        protected readonly GameManager GameManager;

        protected GameManagerState()
        {
            GameManager = GameManager.Instance;
        }
    }
}