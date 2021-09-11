﻿using System;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class SelectionManager : MonoBehaviour, ISelectionManager, IObservable
    {
        #region Singleton

        private static SelectionManager _instance;

        private SelectionManager()
        {
        }

        public static SelectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SelectionManager>();
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
        
        public Defender DefenderToBuild { get; private set; }
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