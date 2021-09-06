using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private int _damage = 50;


    private void Update()
    {
        transform.Translate(Vector2.right * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attacker"))
        {
            if (other.GetComponent<Attacker>() != null)
            {
                other.GetComponent<Attacker>().TakeDamage(_damage);
                
                Destroy(gameObject);
            }
        }
    }

    private void DealDamage(int damage)
    {
        
    }

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
}
