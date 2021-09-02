using System;
using UnityEngine;

public class RangedDefender : Defender
{
    [Header("Attacking")]
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _projectileSpawnPoint;
    
    protected override float AttackRange
    {
        get
        {
            if (Camera.main != null)
            {
                return Camera.main.orthographicSize * 2 - Tile.transform.position.x - Tile.transform.localScale.x;
            }

            Debug.LogWarning("There is no camera tagged as 'main'");
            return 0f;
        }
    }

    protected override void Attack()
    {
        Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity);
        
        Debug.Log("ranged attack");
    }
}