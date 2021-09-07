using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private int _damage = 50;
    
    private Attacker _target;
    
    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (_target != null)
        {
            if (!_target.IsDead && TargetReached())
            {
                _target.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    private bool TargetReached() => transform.position.x >= _target.transform.position.x;

    public void SetDamage(int damageValue)
    {
        if (damageValue <= 0)
        {
            _damage = 0;
        }
        else
        {
            _damage = damageValue;
        }
    }

    public void SetTarget(Attacker target)
    {
        _target = target;
    }
}
