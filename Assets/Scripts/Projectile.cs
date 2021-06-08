using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _damage = 50f;


    private void Update()
    {
        transform.Translate(Vector2.right * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var healthComponent = other.GetComponent<Health>();
        
        Debug.Log("collision");
        healthComponent.DealDamage(_damage);
    }

    private void DealDamage(float damage)
    {
        
    }
}
