using Shop;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private float _sellPenaltyPercent = 0.3f;
        
        
        //TODO: Restrict multiple instances via Singleton
        [field:SerializeField] private Button SellButton { get; set; }
    
        public Defender DefenderToBuild { get; private set; }
        public Defender DefenderToSell { get; private set; }

        public void DeselectDefenderToBuild()
        {
            Debug.Log($"Deselected {DefenderToBuild}");
            DefenderToBuild = null;
        }

        public void DeselectDefenderToSell()
        {
            DefenderToSell = null;
            SellButton.gameObject.SetActive(false);
        }

        public void SelectDefenderToBuild(Defender defender)
        {
            if (defender.Cost > ShopManager.Instance.Balance) 
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
            SellButton.gameObject.SetActive(true);
        }

        public void BuildDefender(Vector3 buildPosition, Transform parent)
        {
            if (DefenderToBuild == null) return;

            ShopManager.Instance.RemoveFromBalance(DefenderToBuild.Cost);
            Debug.Log("New Balance after Buy: " + ShopManager.Instance.Balance);

            Instantiate(
                DefenderToBuild,
                buildPosition,
                Quaternion.identity,
                parent);

            DeselectDefenderToBuild();
        }

        public void SellDefender()
        {
            if (DefenderToSell == null) return;

            ShopManager.Instance.AddToBalance(Mathf.CeilToInt(DefenderToSell.Cost - (DefenderToSell.Cost * _sellPenaltyPercent)));
            Debug.Log("New Balance after Sell: " + ShopManager.Instance.Balance);

            Destroy(DefenderToSell.Tile.CurrentDefender.gameObject);

            DeselectDefenderToSell();
        }
    }
}