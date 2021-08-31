using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    //TODO: Restrict multiple instances via Singleton

    [field:SerializeField] private Button SellButton { get; set; }
    
    public Defender DefenderToBuild { get; private set; }
    public Defender DefenderToSell { get; private set; }

    public void DeselectDefenderToBuild()
    {
        DefenderToBuild = null;
    }

    public void DeselectDefenderToSell()
    {
        DefenderToSell = null;
        SellButton.gameObject.SetActive(false);
    }
    
    public void SelectDefenderToBuild(Defender defender)
    {
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
        SellButton.gameObject.SetActive(true);
    }

    public void BuildDefender(Vector3 buildPosition, Transform parent)
    {
        if (DefenderToBuild == null) return;

        Instantiate(
            DefenderToBuild,
            buildPosition,
            Quaternion.identity,
            parent);
        
        DeselectDefenderToBuild();
    }

    public void SellDefender()
    {
        Destroy(DefenderToSell.Tile.CurrentDefender.gameObject);
        
        DeselectDefenderToSell();
    }
}