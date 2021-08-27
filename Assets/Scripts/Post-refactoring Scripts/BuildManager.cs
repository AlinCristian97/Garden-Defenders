using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //TODO: Restrict multiple instances via Singleton
    
    public Defender DefenderToBuild { get; private set; }

    public void DeselectDefenderToBuild()
    {
        DefenderToBuild = null;
    }

    public void SetDefenderToBuild(Defender defender)
    {
        DefenderToBuild = defender;
    }

    public void BuildDefender(Vector3 buildPosition, Transform defenderTile)
    {
        if (DefenderToBuild == null) return;
        
        Instantiate(DefenderToBuild, buildPosition, Quaternion.identity, defenderTile);
        DefenderToBuild = null;
    }
}