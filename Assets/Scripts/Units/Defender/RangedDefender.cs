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
            if (Camera.main != null)
            {
                return Camera.main.orthographicSize * 2 - Tile.transform.position.x - Tile.transform.localScale.x;
            }

            return 0f;
        }
    }

    protected override void Attack()
    {
        Projectile projectile = Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity);
        projectile.SetDamage(Damage);
    }
}