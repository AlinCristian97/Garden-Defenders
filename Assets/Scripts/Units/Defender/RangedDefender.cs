using System;
using General.ObjectPooling;
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
                float offset = 0.125f;
                return Camera.main.orthographicSize * 2 - Tile.transform.position.x - Tile.transform.localScale.x - offset;
            }

            return 0f;
        }
    }

    protected override void Attack()
    {
        if (GetTargetInAttackRange() == null) return;
        
        Target = GetTargetInAttackRange().GetComponent<Attacker>();

        #region Object Pooling

        GameObject instantiatedGameObject = ObjectPooler.Instance.SpawnFromPool(
            _projectile.AliasIdentifier, 
            _projectileSpawnPoint.position,
            Quaternion.identity);
        
        var instantiatedProjectile = instantiatedGameObject.GetComponent<Projectile>();
        instantiatedProjectile.SetDamage(Damage);
        instantiatedProjectile.SetTarget(Target);
        
        #endregion
    }

    protected override void SetDeadState()
    {
        if (StateMachine.CurrentState != States.DeadState)
        {
            StateMachine.ChangeState(States.DeadState);
        }
    }

    protected override void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }
}