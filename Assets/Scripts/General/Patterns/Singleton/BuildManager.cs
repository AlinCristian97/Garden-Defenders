using System.Collections.Generic;
using General.ObjectPooling;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using UI;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class BuildManager : MonoBehaviour, IBuildManager
    {
        #region Singleton

        private static BuildManager _instance;
        
        public static BuildManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<BuildManager>();
                }
                
                return _instance;
            }
        }

        #endregion
        
        [SerializeField] private float _sellPenaltyPercent = 0.3f;

        private ISelectionManager _selectionManager;
        private IShopManager _shopManager;

        private void Awake()
        {
            _selectionManager = SelectionManager.Instance;
            _shopManager = ShopManager.Instance;
        }

        public void BuildDefender(Vector3 buildPosition, Transform parent)
        {
            if (_selectionManager.DefenderToBuild == null) return;

            _shopManager.RemoveFromBalance(_selectionManager.DefenderToBuild.Cost);

            #region Object Pooling

            GameObject instantiatedGameObject = ObjectPooler.Instance.SpawnFromPool(
                _selectionManager.DefenderToBuild.AliasIdentifier,
                buildPosition,
                Quaternion.identity,
                parent);
            
            var instantiatedDefender = instantiatedGameObject.GetComponent<Defender>();

            #endregion

            int spriteSortingOrderLogicOffset = 5;
            VisualsOrderInLayerAdjuster.SetYSortingOrder(instantiatedDefender.SpriteRenderer, instantiatedDefender.transform.position.y);
            instantiatedDefender.SpriteRenderer.sortingOrder += Mathf.RoundToInt(-instantiatedDefender.transform.position.x + spriteSortingOrderLogicOffset);
            ShadowOrderInLayerAdjuster.SetShadowSortingOrder(instantiatedDefender.GetComponentInChildren<Shadow>().SpriteRenderer, instantiatedDefender.SpriteRenderer);
            
            instantiatedDefender.EnableComponents();

            foreach (DefenderSlot defenderSlot in FindObjectOfType<DefenderSlotsDisplay>().GetComponentsInChildren<DefenderSlot>())
            {
                if (defenderSlot.Defender.AliasIdentifier == instantiatedDefender.AliasIdentifier)
                {
                    defenderSlot.ProcessCooldown();
                }
            }
            
            _selectionManager.DeselectDefenderToBuild();
        }

        public void SellDefender()
        {
            if (_selectionManager.DefenderToSell == null) return;

            _shopManager.AddToBalance(Mathf.CeilToInt(
                _selectionManager.DefenderToSell.Cost - 
                (_selectionManager.DefenderToSell.Cost * _sellPenaltyPercent)));

            _selectionManager.DefenderToSell.Tile.CurrentDefender.gameObject.SetActive(false);

            _selectionManager.DeselectDefenderToSell();
        }
    }
}