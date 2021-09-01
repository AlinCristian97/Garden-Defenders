using System;
using System.Collections;
using System.Collections.Generic;
using General.FSM;
using UnityEngine;

public class Defender : MonoBehaviour
{
    //TODO: Does this variable make sense here?
    [field:SerializeField] public Sprite Avatar { get; private set; }
    
    [field:SerializeField] public int Cost { get; private set; }
    public Tile Tile => GetComponentInParent<Tile>();
    
    [Header("Attacking")]
    [SerializeField] private Projectile _projectile;
    [SerializeField] [Range(0.5f, 3f)] private float _timeBetweenAttacks = 1f;
    private float _nextAttack;
    
    [Header("Defender Detection")]
    [SerializeField] private LayerMask _detectAttackerLayerMask;
    private const float ATTACK_RANGE = 0.5f;
    
    #region Components

    public Collider2D Collider { get; private set; }
    public Animator Animator { get; private set; }

    #endregion
    
    #region FSM

    public StateMachine StateMachine { get; private set; }
    public DefenderStates States { get; private set; }

    #endregion

    //TODO: Unserialize after testing
    [SerializeField] private bool _enemyInLane;

    #region Debug

    private Color _debugColor = Color.gray;

    #endregion
    
    

    #region Unity Callbacks

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponentInChildren<Animator>();

        StateMachine = new StateMachine();
        States = new DefenderStates(this);
    }

    private void Start()
    {
        StateMachine.Initialize(States.IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Execute();

        HandleDebug();
        // HandleShooting();
    }

    #endregion
    
    #region Animation Event Methods

    private void Attack()
    {
        Debug.Log("Defender - just attacked!");
    }

    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }

    #endregion
    
    private void HandleShooting()
    {
        if (AttackCooldownPassed() && _enemyInLane)
        {
            _nextAttack = Time.time + _timeBetweenAttacks;
            Instantiate(_projectile, transform.position, Quaternion.identity);
        }
    }
    
    public bool IsNearDefender()
    {        
        Vector2 startPoint = GetColliderRightBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.right;
        Vector2 endPoint = startPoint + distance * direction;

        
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectAttackerLayerMask);

        return hit.collider != null;
    }

    public bool AttackCooldownPassed() => Time.time > _nextAttack;
    
    public void UpdateNextAttack()
    {
        _nextAttack = Time.time + _timeBetweenAttacks;
    }
    
    public void TriggerAttackAnimation()
    {
        Animator.SetTrigger("Attack");
    }
    
    #region Debug Methods

    private void OnDrawGizmos()
    {
        Collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = GetColliderRightBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.right;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }

    private void HandleDebug()
    {
        if (!IsNearDefender())
        {
            _debugColor = Color.red;
        }
        else
        {
            _debugColor = Color.green;
        }
    }

    #endregion
    
    #region Helper Methods

    private Vector2 GetColliderRightBoundCenterPoint()
    {
        Bounds bounds = Collider.bounds;
        
        var result = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y);

        return result;
    }
    
    #endregion
}
