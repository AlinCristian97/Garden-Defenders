using System.Collections.Generic;
using General.Patterns.Observer;
using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour, IObservable
    {
        #region Singleton

        private static ShopManager _instance;

        private ShopManager()
        {
        }

        public static ShopManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ShopManager>();
                }
                
                return _instance;
            }
        }

        #endregion

        #region Observer

        private readonly List<IObserver> _observers = new List<IObserver>();

        public void AttachObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            if (_observers.Count > 0)
            {
                foreach (IObserver observer in _observers)
                {
                    observer.GetNotified();
                }
            }
        }

        #endregion

        [SerializeField] private int _balance;
        
        public int Balance
        {
            get
            {
                return _balance;
            }
            private set
            {
                _balance = value;
                NotifyObservers();
            }
        }

        public void AddToBalance(int amount)
        {
            if (Balance > 0)
            {
                Balance += amount;
            }
        }

        public void RemoveFromBalance(int amount)
        {
            Balance -= amount;

            if (Balance <= 0)
            {
                Balance = 0;
            }
        }
    }
}