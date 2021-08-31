using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    
    
    // [Range(0f, 5f)] private float _currentSpeed = 1f;
    // private GameObject _currentTarget;
    // private Animator _animator;
    //
    // private void Awake()
    // {
    //     _animator = GetComponent<Animator>();
    // }
    //
    // private void Update()
    // {
    //     transform.Translate(Vector2.left * (_currentSpeed * Time.deltaTime));
    //     UpdateAnimationState();
    // }
    //
    // private void UpdateAnimationState()
    // {
    //     if (!_currentTarget)
    //     {
    //         _animator.SetBool("IsAttacking", false);
    //     }
    // }
    //
    // public void SetMovementSpeed(float speed)
    // {
    //     _currentSpeed = speed;
    // }
    //
    // public void Attack(GameObject target)
    // {
    //     _animator.SetBool("IsAttacking", true);
    //     _currentTarget = target;
    // }
    //
    // public void StrikeCurrentTarget(float damage)
    // {
    //     if (!_currentTarget) { return; }
    //
    //     Health health = _currentTarget.GetComponent<Health>();
    //
    //     if (health)
    //     {
    //         health.DealDamage(damage);
    //     }
    // }
}