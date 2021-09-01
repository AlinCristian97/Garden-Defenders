using System;
using System.Collections;
using System.Collections.Generic;
using General.FSM;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : MonoBehaviour
{
    [Header("Stats")]
    [field:SerializeField] [Range(0.25f, 2f)] private float _movementSpeed = 1f;
    public float MovementSpeed => _movementSpeed;

    [Header("Attacking")]
    [SerializeField] [Range(0.5f, 3f)] private float _timeBetweenAttacks = 1f;
    private float _nextAttack;
    
    [Header("Defender Detection")]
    [SerializeField] private LayerMask _detectDefenderLayerMask;
    private const float ATTACK_RANGE = 0.2f;

    #region Components

    public Collider2D Collider { get; private set; }
    public Animator Animator { get; private set; }

    #endregion

    #region FSM

    public StateMachine StateMachine { get; private set; }
    public AttackerStates States { get; private set; }

    #endregion

    #region Debug

    private Color _debugColor = Color.gray;

    #endregion

    
    
    #region Unity Callbacks

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponentInChildren<Animator>();

        StateMachine = new StateMachine();
        States = new AttackerStates(this);
    }

    private void Start()
    {
        StateMachine.Initialize(States.WalkState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Execute();

        //Debug
        HandleDebug();
    }

    #endregion
    
    public void TriggerAttackAnimation()
    {
        Animator.SetTrigger("Attack");
    }

    #region Animation Event Methods

    private void Attack()
    {
        Debug.Log("just attacked!");
    }

    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }

    #endregion
    
    public void UpdateNextAttack()
    {
        _nextAttack = Time.time + _timeBetweenAttacks;
    }

    public bool AttackCooldownPassed() => Time.time > _nextAttack;

    public bool IsNearDefender()
    {        
        Vector2 startPoint = GetColliderLeftBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.left;
        Vector2 endPoint = startPoint + distance * direction;

        
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectDefenderLayerMask);

        return hit.collider != null;
    }

    #region Debug Methods

    private void OnDrawGizmos()
    {
        Collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = GetColliderLeftBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.left;
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

    private Vector2 GetColliderLeftBoundCenterPoint()
    {
        Bounds bounds = Collider.bounds;
        
        var result = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y);

        return result;
    }
    
    #endregion
    

}