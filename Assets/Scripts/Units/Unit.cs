using System;
using General.FSM;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected bool _isFacingRight;
    protected Vector2 FacingDirection;
    
    // [Header("Attacking")]
    // [SerializeField] [Range(0.2f, 11.5f)] protected float attackRange;
    protected abstract float AttackRange { get; }
    
    [SerializeField] [Range(0.5f, 3f)] protected float TimeBetweenAttacks = 1f;
    protected float NextAttack;
    
    [Header("Defender Detection")]
    [SerializeField] protected LayerMask _detectTargetLayerMask;


    #region Components

    public Collider2D Collider { get; protected set; }
    public Animator Animator { get; private set; }

    #endregion

    #region FSM

    public StateMachine StateMachine { get; private set; }

    #endregion
    
    
    
    #region Unity Callbacks

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        
        StateMachine = new StateMachine();

        FacingDirection = _isFacingRight ? Vector2.right : Vector2.left;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.Execute();
    }

    #endregion

    public void UpdateNextAttack()
    {
        NextAttack = Time.time + TimeBetweenAttacks;
    }
    
    public void TriggerAttackAnimation()
    {
        Animator.SetTrigger("Attack");
    }
    
    public bool HasTargetInAttackRange()
    {        
        Vector2 startPoint = GetColliderSideBoundCenterPoint();
        
        float distance = AttackRange;
        Vector2 direction = FacingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        
        
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectTargetLayerMask);
        
        return hit.collider != null;
    }

    #region Checks

    public bool AttackCooldownPassed() => Time.time > NextAttack;

    #endregion
    
    
    #region Helper Methods

    protected Vector2 GetColliderSideBoundCenterPoint()
    {
        Bounds bounds = Collider.bounds;
        
        var result = new Vector2(bounds.center.x + (bounds.extents.x * FacingDirection.x), bounds.center.y);
    
        return result;
    }
    
    #endregion
}