using General.Patterns.Observer;

namespace General.Patterns.Singleton.Interfaces
{
    public interface IShopManager : IObservable
    {
        int Balance { get; }

        void AddToBalance(int amount);
        void RemoveFromBalance(int amount);
    }
}