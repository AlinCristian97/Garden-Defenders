using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatDefender : Defender
{
    [Header("Stats")]
    [SerializeField] private bool _isFacingRight;
    private Vector2 _facingDirection;
    protected abstract float AttackRange { get; }
    [field:SerializeField] protected int Damage { get; private set; }

    [Header("Attacking")]
    private const float RANGE_START_OFFSET = 0.2f;
    [SerializeField] private LayerMask _detectTargetLayerMask;
    [SerializeField] [Range(0.2f, 3f)] private float _timeBetweenAttacks = 1f;
    private float _nextAttack;
    protected Attacker Target;

    #region FSM

    public CombatDefenderStates States { get; private set; }

    #endregion

    #region Debug

    private Color _debugColor = Color.gray;

    #endregion
    
    
    
    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        _facingDirection = _isFacingRight ? Vector2.right : Vector2.left;
        States = new CombatDefenderStates(this);
    }
    
    private void Start()
    {
        StateMachine.Initialize(States.IdleState);
    }
    
    //TODO: Delete after testing
    protected override void Update()
    {
        base.Update();
        
        HandleDebug();
    }

    #endregion

    protected abstract void Attack();

    public void UpdateNextAttack()
    {
        _nextAttack = Time.time + _timeBetweenAttacks;
    }
    
    public bool AttackCooldownPassed() => Time.time > _nextAttack;
    
    public void TriggerAttackAnimation()
    {
        Animator.SetTrigger("Attack");
    }
    
    protected Collider2D GetTargetInAttackRange()
    {        
        Vector2 startPoint = OffsetRayStartingPoint();
        
        float distance = AttackRange;
        Vector2 direction = _facingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectTargetLayerMask);

        return hit.collider == null ? null : hit.collider;
    }
    
    private Vector2 OffsetRayStartingPoint()
    {
        return transform.position + new Vector3(RANGE_START_OFFSET * _facingDirection.x, 0f, 0f);
    }

    public bool HasTargetInAttackRange() => GetTargetInAttackRange() != null;
    
    #region Animation Event Methods

    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }
    
    #endregion

    #region Helper Methods

    #endregion
    
    #region Debug

    private void OnDrawGizmos()
    {
        Collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = OffsetRayStartingPoint();
        
        float distance = AttackRange;
        Vector2 direction = _facingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }
    
    private void HandleDebug()
    {
        if (!HasTargetInAttackRange())
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