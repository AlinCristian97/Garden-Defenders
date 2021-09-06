using System;
using System.Collections;
using System.Collections.Generic;
using General.FSM;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : Unit
{
    [Header("Stats")]
    [SerializeField] private bool _isFacingRight;
    private Vector2 _facingDirection;
    [field:SerializeField] [Range(0.25f, 2f)] private float _movementSpeed = 1f;
    [SerializeField] private int _damage;
    
    public float MovementSpeed => _movementSpeed;
    private static float _attackRange => 0.2f;
    
    [Header("Attacking")]
    [SerializeField] private LayerMask _detectTargetLayerMask;
    [SerializeField] [Range(0.5f, 3f)] private float _timeBetweenAttacks = 1f;
    private float _nextAttack;
    private Defender _target;

    #region FSM

    public AttackerStates States { get; private set; }

    #endregion
    
    #region Debug

    private Color _debugColor = Color.gray;

    #endregion

    

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        _facingDirection = _isFacingRight ? Vector2.right : Vector2.left;
        States = new AttackerStates(this);
    }
    
    private void Start()
    {
        StateMachine.Initialize(States.WalkState);
    }

    //TODO: Delete after testing
    protected override void Update()
    {
        base.Update();
        
        HandleDebug();
    }

    #endregion
    
    public void UpdateNextAttack()
    {
        _nextAttack = Time.time + _timeBetweenAttacks;
    }
    
    public bool AttackCooldownPassed() => Time.time > _nextAttack;

    public void TriggerAttackAnimation()
    {
        Animator.SetTrigger("Attack");
    }
    
    public bool SetTargetInAttackRange()
    {        
        Vector2 startPoint = GetColliderSideBoundCenterPoint();
        
        float distance = _attackRange;
        Vector2 direction = _facingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectTargetLayerMask);

        if (hit.collider != null)
        {
            _target = hit.collider.GetComponent<Defender>();
            return true;
        }

        return false;
    }

    #region Animation Event Methods

    private void Attack()
    {
        _target.TakeDamage(_damage);
    }

    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }

    #endregion

    #region Helper Methods

    private Vector2 GetColliderSideBoundCenterPoint()
    {
        Bounds bounds = Collider.bounds;
        
        var result = new Vector2(bounds.center.x + (bounds.extents.x * _facingDirection.x), bounds.center.y);
    
        return result;
    }

    #endregion
    
    #region Debug Methods

    private void OnDrawGizmos()
    {
        Collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = GetColliderSideBoundCenterPoint();
        
        float distance = _attackRange;
        Vector2 direction = _facingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }

    private void HandleDebug()
    {
        if (!SetTargetInAttackRange())
        {
            _debugColor = Color.red;
        }
        else
        {
            _debugColor = Color.green;
        }
    }

    #endregion
}