using System;
using UnityEngine;

public class RangedDefender : CombatDefender
{
    [Header("Attacking")]
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _projectileSpawnPoint;

    protected override float AttackRange
    {
        get
        {
            if (Camera.main != null && Tile != null) // remove Tile null check before prod
            {
                return Camera.main.orthographicSize * 2 - Tile.transform.position.x - Tile.transform.localScale.x;
            }

            return 0f;
        }
    }

    protected override void Attack()
    {
        //TODO: Make Projectile damage Attacker
        Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity);
        
        Debug.Log("ranged attack");
    }
}