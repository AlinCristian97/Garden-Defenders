using General.Patterns.Singleton.Interfaces;
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

            Defender defender = Instantiate(
                _selectionManager.DefenderToBuild,
                buildPosition,
                Quaternion.identity,
                parent);
            int spriteSortingOrderLogicOffset = 5;
            defender.SpriteRenderer.sortingOrder = Mathf.RoundToInt(-defender.transform.position.x + spriteSortingOrderLogicOffset);

            _selectionManager.DeselectDefenderToBuild();
        }

        public void SellDefender()
        {
            if (_selectionManager.DefenderToSell == null) return;

            _shopManager.AddToBalance(Mathf.CeilToInt(
                _selectionManager.DefenderToSell.Cost - 
                (_selectionManager.DefenderToSell.Cost * _sellPenaltyPercent)));

            Destroy(_selectionManager.DefenderToSell.Tile.CurrentDefender.gameObject);

            _selectionManager.DeselectDefenderToSell();
        }
    }
}