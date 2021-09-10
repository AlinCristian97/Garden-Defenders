namespace General.Patterns.Observer
{
    public interface IObservable
    {
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void NotifyObservers();
    }
}