using General.Patterns.Observer;

namespace General.Patterns.Singleton.Interfaces
{
    public interface IPauseManager : IObservable
    {
        bool GameIsPaused { get; }
        
        void PauseGame();
        void ResumeGame();
    }
}