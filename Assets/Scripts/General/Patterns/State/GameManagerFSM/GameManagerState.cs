using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;

namespace General.Patterns.State.GameManagerFSM
{
    public abstract class GameManagerState : State
    {
        protected readonly IGameManager GameManager;

        protected GameManagerState()
        {
            GameManager = Singleton.GameManager.Instance;
        }
    }
}