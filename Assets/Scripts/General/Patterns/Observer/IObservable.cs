using System.Collections.Generic;

namespace General.Patterns.Observer
{
    public interface IObservable
    {
        List<IObserver> Observers { get; }
        
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void NotifyObservers();
    }
}