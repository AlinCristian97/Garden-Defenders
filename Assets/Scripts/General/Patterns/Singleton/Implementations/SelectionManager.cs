using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;

namespace General.Patterns.Singleton.Implementations
{
    public class SelectionManager : SingletonBase<SelectionManager>, ISelectionManager
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
        
        private Defender _defenderToBuild;

        public Defender DefenderToBuild
        {
            get
            {
                return _defenderToBuild;
            }
            private set
            {
                _defenderToBuild = value;
                NotifyObservers();
            }
        }
        private Defender _defenderToSell;
        
        public Defender DefenderToSell
        {
            get
            {
                return _defenderToSell;
            }
            private set
            {
                _defenderToSell = value;
                NotifyObservers();
            }
        }

        private IShopManager _shopManager;

        private void Awake()
        {
            _shopManager = ShopManager.Instance;
        }

        public void SelectDefenderToBuild(Defender defender)
        {
            if (defender.Cost > _shopManager.Balance) 
                return;
            
            if (DefenderToSell != null)
            {
                DeselectDefenderToSell();
            }
        
            DefenderToBuild = defender;
        }

        public void SelectDefenderToSell(Defender defender)
        {
            if (DefenderToBuild != null)
            {
                DeselectDefenderToBuild();
            }

            DefenderToSell = defender;
        }
        
        public void DeselectDefenderToBuild()
        {
            DefenderToBuild = null;
        }

        public void DeselectDefenderToSell()
        {
            DefenderToSell = null;
        }
    }
}