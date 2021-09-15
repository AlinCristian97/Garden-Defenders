using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class ShopManager : MonoBehaviour, IShopManager
    {
        #region Singleton

        private static ShopManager _instance;
        
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