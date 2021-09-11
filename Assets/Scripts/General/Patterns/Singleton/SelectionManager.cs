using System;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class SelectionManager : MonoBehaviour, ISelectionManager
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
        
        public Defender DefenderToBuild { get; private set; }
        public Defender DefenderToSell { get; private set; }

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
            Debug.Log($"Selected: {DefenderToBuild.name}");
        }

        public void SelectDefenderToSell(Defender defender)
        {
            if (DefenderToBuild != null)
            {
                DeselectDefenderToBuild();
            }

            DefenderToSell = defender;
            
            //TODO: Move this out of here
            BuildManager.Instance.SellButton.gameObject.SetActive(true);
        }
        
        public void DeselectDefenderToBuild()
        {
            Debug.Log($"Deselected {DefenderToBuild}");
            DefenderToBuild = null;
        }

        public void DeselectDefenderToSell()
        {
            DefenderToSell = null;
            
            //TODO: Move this out of here
            BuildManager.Instance.SellButton.gameObject.SetActive(false);        }
    }
}