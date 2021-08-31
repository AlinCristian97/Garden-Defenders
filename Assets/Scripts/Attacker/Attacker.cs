using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] [Range(0.25f, 2f)] private float _movementSpeed = 1f;

    [Header("Attacking")]
    [SerializeField] [Range(0.5f, 3f)] private float _timeBetweenAttacks = 1f;
    private float _nextAttack;
    
    [Header("Defender Detection")]
    [SerializeField] private LayerMask _detectDefenderLayerMask;
    private const float ATTACK_RANGE = 0.5f;

    private Collider2D _collider;
    private Animator _animator;

    //Debug
    private Color _debugColor = Color.gray;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //TODO: Implement a simple FSM to remove these checks
        if (!IsInRangeToAttack())
        {
            Move();
        }
        else
        {
            EnterCombatMode();
        }
    }

    private void Move()
    {
        _animator.SetBool("IsInCombatMode", false);

        transform.Translate(Vector3.left * Time.deltaTime * _movementSpeed);
        _debugColor = Color.red;
    }

    private void EnterCombatMode()
    {
        //Debug
        Debug.Log("Entered Combat Mode");
        _debugColor = Color.green;
        
        _animator.SetBool("IsInCombatMode", true);

        if (CanAttack())
        {
            _nextAttack = Time.time + _timeBetweenAttacks;
            TriggerAttackAnimation();
        }
    }

    private void TriggerAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    private void Attack()
    {
        Debug.Log("just attacked!");
    }

    private bool CanAttack() => Time.time > _nextAttack;

    private bool IsInRangeToAttack()
    {        
        Vector2 startPoint = GetColliderLeftBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.left;
        Vector2 endPoint = startPoint + distance * direction;

        
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectDefenderLayerMask);

        return hit.collider != null;
    }

    //Debug
    private void OnDrawGizmos()
    {
        _collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = GetColliderLeftBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.left;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }

    private Vector2 GetColliderLeftBoundCenterPoint()
    {
        Bounds bounds = _collider.bounds;
        
        var result = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y);

        return result;
    }

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