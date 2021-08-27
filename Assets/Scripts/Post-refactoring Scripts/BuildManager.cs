using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //TODO: Restrict multiple instances via Singleton

    public Defender DefenderToBuild { get; set; }

    public void SetDefenderToBuild(Defender defender)
    {
        DefenderToBuild = defender;
    }
}