using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;

namespace General.Patterns.Singleton.Implementations
{
    public class GameManager : SingletonBase<GameManager>, IGameManager
    {
       #region Observer

        public List<IObserver> Observers { get; private set; } = new List<IObserver>();

        public void AttachObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            if (Observers.Count > 0)
            {
                foreach (IObserver observer in Observers)
                {
                    observer.GetNotified();
                }
            }
        }

        #endregion
    }
}
