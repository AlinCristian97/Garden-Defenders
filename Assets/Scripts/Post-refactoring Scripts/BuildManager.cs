using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    //TODO: Restrict multiple instances via Singleton

    [field:SerializeField] public Button SellButton { get; private set; }
    
    public Defender DefenderToBuild { get; private set; }
    public Defender DefenderToSell { get; private set; }

    public void DeselectDefenderToBuild()
    {
        DefenderToBuild = null;
    }

    public void SetDefenderToBuild(Defender defender)
    {
        SetDefenderToSell(null);
        SellButton.gameObject.SetActive(false);
        DefenderToBuild = defender;
    }
    
    public void SetDefenderToSell(Defender defender)
    {
        DefenderToSell = defender;
    }

    public void BuildDefender(Vector3 buildPosition, Transform defenderTile)
    {
        if (DefenderToBuild == null) return;
        
        Instantiate(DefenderToBuild, buildPosition, Quaternion.identity, defenderTile);
        DefenderToBuild = null;
    }

    public void SellDefender()
    {
        DefenderToSell.GetComponentInParent<Tile>().SellDefender(DefenderToSell);
        DefenderToSell = null;
    }
}